using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSRetailPOS.Gateway.Mobikwik
{
    internal sealed class MobikwikGateway : PaymentGatewayBase
    {
        sealed class Settings
        {
            public string BaseUrl { get; set; }
            public string ClientId { get; set; }
            public string Mid { get; set; }
            public int PollIntervalSeconds { get; set; } = 3;
            public int PaymentTimeoutSeconds { get; set; } = 180;
        }
        public sealed class Request : IPaymentRequest
        {
            public string orderId { get; set; }
            public decimal amount { get; set; }
            public string mid { get; set; }
            public string smid { get; set; }
        }
        public sealed class Status : IStatusResponse
        {
            public int statusCode { get; set; }
            public string statusMessage { get; set; }
            public string orderId { get; set; }
            public string bankRRN { get; set; }
            public string transactionId { get; set; }
        }
        readonly Settings settings;
        readonly string additionalSettings, smid;
        readonly int gatewayID;
        readonly HttpClient client;
        readonly Uri endpoint;
        readonly bool usePlainJson;
        readonly Func<string, string> readKeyData;
        public override string GatewayType => "Mobikwik";

        public MobikwikGateway(int gatewayID, string baseSettings, string additionalSettings,
            Func<string, string> readKeyData, HttpClient client = null)
        {
            this.gatewayID = gatewayID;
            this.additionalSettings = additionalSettings;
            this.readKeyData = readKeyData;
            settings = JsonConvert.DeserializeObject<Settings>(baseSettings);
            smid = (string)JObject.Parse(additionalSettings)["smid"];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ClientId) || string.IsNullOrWhiteSpace(settings.Mid)
                || settings.Mid.Length < 3 || settings.Mid.Length > 12 || (!string.IsNullOrEmpty(smid) && (smid.Length < 4 || smid.Length > 12))) return;
            if (!Uri.TryCreate(settings.BaseUrl, UriKind.Absolute, out Uri uri) || uri.Scheme != Uri.UriSchemeHttps) return;
            if (!string.IsNullOrEmpty(uri.Query) || !string.IsNullOrEmpty(uri.Fragment) || !string.IsNullOrEmpty(uri.UserInfo)) return;
            string root = uri.GetLeftPart(UriPartial.Path).TrimEnd('/');
            usePlainJson = root.EndsWith("/merchant/testdqr", StringComparison.OrdinalIgnoreCase);
            if (!usePlainJson && !root.EndsWith("/merchant/dqr", StringComparison.OrdinalIgnoreCase)) return;
            endpoint = new Uri(root + "/");
            this.client = client ?? new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            IsValid = true;
        }

        internal sealed class RequestKeys : IDisposable
        {
            public RSA MobikwikPublic { get; }
            public RSA PartnerPrivate { get; }

            public RequestKeys(RSA mobikwikPublic, RSA partnerPrivate)
            {
                MobikwikPublic = mobikwikPublic;
                PartnerPrivate = partnerPrivate;
            }

            public void Dispose()
            {
                MobikwikPublic.Dispose();
                PartnerPrivate.Dispose();
            }
        }

        internal RequestKeys LoadKeys()
        {
            if (usePlainJson) return null;
            string json = readKeyData?.Invoke(settings.Mid);
            if (string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException("Mobikwik production keys are missing. Sync this counter and try again.");

            RSA partnerPrivate = null, mobikwikPublic = null;
            try
            {
                var data = JObject.Parse(json);
                using var partnerPublic = MobikwikCrypto.ReadKey((string)data["PartnerPublicKeyDerBase64"], false);
                partnerPrivate = MobikwikCrypto.ReadKey((string)data["PartnerPrivateKeyDerBase64"], true);
                mobikwikPublic = MobikwikCrypto.ReadKey((string)data["MobikwikPublicKeyDerBase64"], false);
                RSAParameters expected = partnerPublic.ExportParameters(false);
                RSAParameters actual = partnerPrivate.ExportParameters(false);
                if (!Enumerable.SequenceEqual(expected.Modulus, actual.Modulus)
                    || !Enumerable.SequenceEqual(expected.Exponent, actual.Exponent))
                    throw new CryptographicException();
                return new RequestKeys(mobikwikPublic, partnerPrivate);
            }
            catch (Exception ex) when (ex is JsonException || ex is ArgumentException || ex is FormatException
                || ex is CryptographicException || ex is OverflowException || ex is InvalidCastException)
            {
                partnerPrivate?.Dispose();
                mobikwikPublic?.Dispose();
                throw new InvalidOperationException("Mobikwik production keys in the local database are invalid. Check the Warehouse keys and sync this counter.");
            }
        }

        async Task<JObject> Post(string action, JObject payload, RequestKeys keys)
        {
            string body = payload.ToString(Formatting.None);
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint, action)))
            {
                LogPaymentRequest(request.RequestUri, "[Mobikwik payload omitted]");
                request.Headers.Add("clientId", settings.ClientId);
                if (keys != null)
                {
                    byte[] random = new byte[32];
                    using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(random);
                    string aesKey = Convert.ToBase64String(random);
                    request.Headers.Add("key", Convert.ToBase64String(keys.MobikwikPublic.Encrypt(Encoding.UTF8.GetBytes(aesKey), RSAEncryptionPadding.Pkcs1)));
                    body = MobikwikCrypto.EncryptBody(body, aesKey);
                }
                request.Content = new StringContent(body, Encoding.UTF8, keys == null ? "application/json" : "text/plain");
                using (var response = await client.SendAsync(request))
                {
                    LogPaymentResponse(action, response, "[Mobikwik payload omitted]");
                    if (!response.IsSuccessStatusCode)
                        StatusUpdate?.Invoke("Mobikwik " + action + " returned HTTP " + (int)response.StatusCode + ".");
                    response.EnsureSuccessStatusCode();
                    string text = await response.Content.ReadAsStringAsync();
                    if (keys != null)
                    {
                        if (!response.Headers.TryGetValues("key", out var values)) throw new InvalidOperationException("Missing encrypted response key.");
                        byte[] encryptedKey = Convert.FromBase64String(values.First());
                        byte[] responseKey = keys.PartnerPrivate.Decrypt(encryptedKey, RSAEncryptionPadding.Pkcs1);
                        try { text = MobikwikCrypto.DecryptBody(text, Encoding.UTF8.GetString(responseKey)); }
                        finally { Array.Clear(responseKey, 0, responseKey.Length); }
                    }
                    try { return JObject.Parse(text); }
                    catch (JsonException) { throw new InvalidOperationException("Invalid Mobikwik response JSON."); }
                }
            }
        }

        JObject Identity(string orderID)
        {
            var identity = new JObject { ["orderId"] = orderID, ["mid"] = settings.Mid };
            if (!string.IsNullOrWhiteSpace(smid)) identity["smid"] = smid;
            return identity;
        }
        async Task<Status> GetStatus(string reference, RequestKeys keys)
        {
            var json = await Post("txnstatus", Identity(reference), keys);
            if (json["statusCode"] == null) throw new InvalidOperationException("Missing Mobikwik status code.");
            var status = json.ToObject<Status>();
            if (!string.IsNullOrEmpty(status.orderId) && !string.Equals(status.orderId, reference, StringComparison.Ordinal))
                throw new InvalidOperationException("Mobikwik order reference mismatch.");
            if (status.statusCode >= 1000) LastRequestFinal = true;
            return status;
        }
        CompletedTransactionData Complete(int bill, int mop, string reference, decimal amount, Status status) =>
            new CompletedTransactionData { BillID = bill, MopID = mop, Amount = amount, PaymentGatewayID = gatewayID,
                AdditionalSettings = additionalSettings, StatusResponse = status,
                PaymentRequest = new Request { orderId = reference, amount = amount, mid = settings.Mid, smid = smid } };

        public override async Task<CompletedTransactionData> ReceivePayment(int billID, int mopID, CancellationToken token, params object[] parameters)
        {
            if (IsInProgress || !IsValid) return null;
            return await ReceivePayment(billID, mopID, token, Convert.ToString(parameters[0]), Convert.ToDecimal(parameters[3]), null);
        }

        internal async Task<CompletedTransactionData> ReceivePayment(int billID, int mopID, CancellationToken token,
            string reference, decimal amount, RequestKeys preparedKeys)
        {
            if (IsInProgress || !IsValid) return null;
            if (string.IsNullOrWhiteSpace(reference) || reference.Length < 12 || reference.Length > 50)
            { LastRequestFinal = true; throw new InvalidOperationException("Mobikwik orderId (bill number plus attempt suffix) must be between 12 and 50 characters."); }
            if (amount < 1 || amount > 200000 || decimal.Round(amount, 2) != amount)
            { LastRequestFinal = true; throw new InvalidOperationException("Mobikwik amount must be INR 1 to 200000 with at most two decimals."); }
            IsInProgress = true;
            LastRequestFinal = true;
            try
            {
                using var loadedKeys = preparedKeys == null ? LoadKeys() : null;
                var keys = preparedKeys ?? loadedKeys;
                var payload = Identity(reference);
                payload["amount"] = amount;
                payload["imageRequired"] = false;
                LastRequestFinal = false;
                var generated = await Post("generate", payload, keys);
                if (generated["statusCode"] == null) throw new InvalidOperationException("Missing QR generation status.");
                int code = (int)generated["statusCode"];
                if (code != 0) { LastRequestFinal = code >= 400 && code < 500; throw new InvalidOperationException("Mobikwik did not generate a QR."); }
                if ((string)generated["orderId"] != reference || string.IsNullOrWhiteSpace((string)generated["qrData"]))
                    throw new InvalidOperationException("Invalid generated QR response.");
                ProviderReferenceReady?.Invoke(reference);
                QrReady?.Invoke((string)generated["qrData"]);
                DateTime until = DateTime.UtcNow.AddSeconds(Math.Max(10, Math.Min(600, settings.PaymentTimeoutSeconds)));
                while (DateTime.UtcNow < until)
                {
                    if (token.IsCancellationRequested)
                    {
                        Status beforeCancel = null;
                        try { beforeCancel = await GetStatus(reference, keys); }
                        catch (HttpRequestException)
                        { StatusUpdate?.Invoke("Status service unavailable. Sending cancellation for the original order."); }
                        if (beforeCancel?.statusCode == 100) return Complete(billID, mopID, reference, amount, beforeCancel);
                        if (LastRequestFinal) return null;
                        var cancel = Identity(reference);
                        cancel["refundAmount"] = amount;
                        cancel["refundRefId"] = Guid.NewGuid().ToString("N");
                        RefundRequested = true;
                        RefundRequestedReady?.Invoke();
                        await Post("cancel", cancel, keys);
                        // A cancellation may initiate a refund. Leave uncertain outcomes unresolved.
                        await GetStatus(reference, keys);
                        StatusUpdate?.Invoke("Cancellation requested. Retry starts a new order; reconcile any late payment/refund on this order.");
                        return null;
                    }
                    Status status;
                    try
                    {
                        StatusUpdate?.Invoke("Checking Mobikwik payment status: " + reference);
                        status = await GetStatus(reference, keys);
                    }
                    catch (HttpRequestException) { StatusUpdate?.Invoke("Status service unavailable; retrying the same order."); status = null; }
                    if (status?.statusCode == 100) return Complete(billID, mopID, reference, amount, status);
                    if (LastRequestFinal) { StatusUpdate?.Invoke("Mobikwik order expired, failed or was refunded."); return null; }
                    if (status != null && status.statusCode >= 400 && status.statusCode < 500)
                        throw new InvalidOperationException("Mobikwik status request was rejected. Check the original order.");
                    StatusUpdate?.Invoke("Waiting for customer payment.");
                    try { await Task.Delay(TimeSpan.FromSeconds(Math.Max(1, Math.Min(30, settings.PollIntervalSeconds))), token); }
                    catch (OperationCanceledException) { }
                }
                StatusUpdate?.Invoke("Payment wait ended without confirmation. Retry starts a new order; confirm the earlier outcome on the device/customer first.");
                return null;
            }
            finally { IsInProgress = false; }
        }

        public override async Task<CompletedTransactionData> VerifyPayment(int billID, int mopID, params object[] parameters)
        {
            string reference = Convert.ToString(parameters[0]);
            using var keys = LoadKeys();
            var status = await GetStatus(reference, keys);
            return status.statusCode == 100 && !RefundRequested
                ? Complete(billID, mopID, reference, Convert.ToDecimal(parameters[1]), status) : null;
        }
        public override CompletedTransactionData ForceReceivePayment(int billID, int mopID, params object[] parameters)
        {
            string reference = Convert.ToString(parameters[1]);
            return Complete(billID, mopID, reference, Convert.ToDecimal(parameters[0]), new Status
            {
                statusCode = 100, statusMessage = "Manually linked from terminal/bank record; not gateway verified",
                orderId = reference, bankRRN = Convert.ToString(parameters[2]), transactionId = Convert.ToString(parameters[3])
            });
        }
        protected override Task<bool> CancelRequest(ICancelRequest request) => throw new NotSupportedException();
        protected override Task<IStatusResponse> CheckRequestStatus(IPaymentRequest request, IStatusRequest status, CancellationToken token, int forceCheckCount = 0) => throw new NotSupportedException();
        protected override Task<IPaymentResponse> SendRequest(IPaymentRequest request, CancellationToken token) => throw new NotSupportedException();
    }
}
