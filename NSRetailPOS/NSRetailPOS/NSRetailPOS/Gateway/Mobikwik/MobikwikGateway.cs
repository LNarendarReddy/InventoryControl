using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
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
            public bool UseTestEndpoints { get; set; }
            public string MbkPublicKeyPath { get; set; }
            public string PartnerPrivateKeyPath { get; set; }
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
        }
        readonly Settings settings;
        readonly string additionalSettings, smid;
        readonly int gatewayID;
        readonly HttpClient client;
        readonly Uri endpoint;
        public override string GatewayType => "Mobikwik";

        public MobikwikGateway(int gatewayID, string baseSettings, string additionalSettings, HttpClient client = null)
        {
            this.gatewayID = gatewayID;
            this.additionalSettings = additionalSettings;
            settings = JsonConvert.DeserializeObject<Settings>(baseSettings);
            smid = (string)JObject.Parse(additionalSettings)["smid"];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ClientId) || string.IsNullOrWhiteSpace(settings.Mid)
                || settings.Mid.Length < 3 || settings.Mid.Length > 12 || (!string.IsNullOrEmpty(smid) && (smid.Length < 4 || smid.Length > 12))) return;
            if (!Uri.TryCreate(settings.BaseUrl, UriKind.Absolute, out Uri uri) || uri.Scheme != Uri.UriSchemeHttps) return;
            string root = settings.BaseUrl.TrimEnd('/');
            if (!root.EndsWith("/walletapis", StringComparison.OrdinalIgnoreCase)) root += "/walletapis";
            endpoint = new Uri(root + "/merchant/" + (settings.UseTestEndpoints ? "testdqr/" : "dqr/"));
            if (!settings.UseTestEndpoints)
            {
                if (!File.Exists(settings.MbkPublicKeyPath) || !File.Exists(settings.PartnerPrivateKeyPath)) return;
                using (MobikwikCrypto.ReadKey(settings.MbkPublicKeyPath, false)) { }
                using (MobikwikCrypto.ReadKey(settings.PartnerPrivateKeyPath, true)) { }
            }
            this.client = client ?? new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            IsValid = true;
        }

        async Task<JObject> Post(string action, JObject payload)
        {
            string body = payload.ToString(Formatting.None);
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint, action)))
            {
                request.Headers.Add("clientId", settings.ClientId);
                if (!settings.UseTestEndpoints)
                {
                    byte[] random = new byte[32];
                    using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(random);
                    string aesKey = Convert.ToBase64String(random);
                    using (var rsa = MobikwikCrypto.ReadKey(settings.MbkPublicKeyPath, false))
                        request.Headers.Add("key", Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(aesKey), false)));
                    body = MobikwikCrypto.EncryptBody(body, aesKey);
                }
                request.Content = new StringContent(body, Encoding.UTF8, settings.UseTestEndpoints ? "application/json" : "text/plain");
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    string text = await response.Content.ReadAsStringAsync();
                    if (!settings.UseTestEndpoints)
                    {
                        if (!response.Headers.TryGetValues("key", out var values)) throw new InvalidOperationException("Missing encrypted response key.");
                        string header = System.Linq.Enumerable.First(values);
                        using (var rsa = MobikwikCrypto.ReadKey(settings.PartnerPrivateKeyPath, true))
                            text = MobikwikCrypto.DecryptBody(text, Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(header), false)));
                    }
                    return JObject.Parse(text);
                }
            }
        }

        JObject Identity(string orderID) => new JObject { ["orderId"] = orderID, ["mid"] = settings.Mid, ["smid"] = smid };
        async Task<Status> GetStatus(string reference)
        {
            var json = await Post("txnstatus", Identity(reference));
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
            string reference = Convert.ToString(parameters[0]);
            decimal amount = Convert.ToDecimal(parameters[3]);
            if (amount < 1 || amount > 200000 || decimal.Round(amount, 2) != amount || reference.Length < 12 || reference.Length > 50)
            { LastRequestFinal = true; throw new InvalidOperationException("Mobikwik amount must be INR 1 to 200000 with at most two decimals."); }
            IsInProgress = true;
            try
            {
                var payload = Identity(reference);
                payload["amount"] = amount;
                payload["imageRequired"] = false;
                var generated = await Post("generate", payload);
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
                        var beforeCancel = await GetStatus(reference);
                        if (beforeCancel.statusCode == 100) return Complete(billID, mopID, reference, amount, beforeCancel);
                        if (LastRequestFinal) return null;
                        var cancel = Identity(reference);
                        cancel["refundAmount"] = amount;
                        cancel["refundRefId"] = Guid.NewGuid().ToString("N");
                        RefundRequested = true;
                        RefundRequestedReady?.Invoke();
                        await Post("cancel", cancel);
                        // A cancellation may initiate a refund. Leave uncertain outcomes unresolved.
                        await GetStatus(reference);
                        StatusUpdate?.Invoke("Cancellation requested. Verify the final order status before collecting again.");
                        return null;
                    }
                    Status status;
                    try { status = await GetStatus(reference); }
                    catch (HttpRequestException) { StatusUpdate?.Invoke("Status service unavailable; retrying the same order."); status = null; }
                    if (status?.statusCode == 100) return Complete(billID, mopID, reference, amount, status);
                    if (LastRequestFinal) { StatusUpdate?.Invoke("Mobikwik order expired, failed or was refunded."); return null; }
                    if (status != null && status.statusCode >= 400 && status.statusCode < 500)
                        throw new InvalidOperationException("Mobikwik status request was rejected. Check the original order.");
                    StatusUpdate?.Invoke("Waiting for customer payment.");
                    try { await Task.Delay(TimeSpan.FromSeconds(Math.Max(1, Math.Min(30, settings.PollIntervalSeconds))), token); }
                    catch (OperationCanceledException) { }
                }
                StatusUpdate?.Invoke("Payment wait ended. Check the original order before trying again.");
                return null;
            }
            finally { IsInProgress = false; }
        }

        public override async Task<CompletedTransactionData> VerifyPayment(int billID, int mopID, params object[] parameters)
        {
            string reference = Convert.ToString(parameters[0]);
            var status = await GetStatus(reference);
            return status.statusCode == 100 && !RefundRequested
                ? Complete(billID, mopID, reference, Convert.ToDecimal(parameters[1]), status) : null;
        }
        protected override Task<bool> CancelRequest(ICancelRequest request) => throw new NotSupportedException();
        protected override Task<IStatusResponse> CheckRequestStatus(IPaymentRequest request, IStatusRequest status, CancellationToken token, int forceCheckCount = 0) => throw new NotSupportedException();
        protected override Task<IPaymentResponse> SendRequest(IPaymentRequest request, CancellationToken token) => throw new NotSupportedException();
    }
}
