using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSRetailPOS.Gateway;

public static class MappedPaymentChecks
{
    static Assembly app = typeof(PaymentGatewayBase).Assembly;
    static Type crypto = app.GetType("NSRetailPOS.Gateway.Mobikwik.MobikwikCrypto");
    static void Assert(bool value, string message) { if (!value) throw new Exception(message); }
    static object Crypto(string name, params object[] args) { return crypto.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, args); }
    static string Settings(string keys, bool test)
    {
        return JsonConvert.SerializeObject(new { BaseUrl = "https://mobikwik.test.invalid", ClientId = "TEST-CLIENT", Mid = "MID123",
            UseTestEndpoints = test, MbkPublicKeyPath = System.IO.Path.Combine(keys, "public.der"),
            PartnerPrivateKeyPath = System.IO.Path.Combine(keys, "private.der"), PollIntervalSeconds = 1 });
    }
    static PaymentGatewayBase Gateway(Stub handler, string keys, bool test)
    {
        return (PaymentGatewayBase)Activator.CreateInstance(app.GetType("NSRetailPOS.Gateway.Mobikwik.MobikwikGateway"),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null,
            new object[] { 31, Settings(keys, test), "{}", new HttpClient(handler) }, CultureInfo.InvariantCulture);
    }
    sealed class Stub : HttpMessageHandler
    {
        public int Calls, GenerateCalls, StatusCalls;
        public bool Encrypted, Refunded, BadOrder, GenerateError, NetworkFailure, Transient;
        public int StatusCode = 100;
        public string Reference, Keys;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            Calls++;
            Assert(request.RequestUri.Scheme == "https", "HTTPS must be used.");
            Assert(request.Headers.GetValues("clientId").Single() == "TEST-CLIENT", "clientId header mismatch.");
            string body = await request.Content.ReadAsStringAsync();
            if (Encrypted)
            {
                Assert(request.Content.Headers.ContentType.MediaType == "text/plain", "Encrypted content type mismatch.");
                using (var rsa = (RSACryptoServiceProvider)Crypto("ReadKey", System.IO.Path.Combine(Keys, "private.der"), true))
                {
                    var key = Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(request.Headers.GetValues("key").Single()), false));
                    body = (string)Crypto("DecryptBody", body, key);
                }
                Assert(request.RequestUri.AbsolutePath.Contains("/merchant/dqr/"), "Encrypted endpoint mismatch.");
            }
            else Assert(request.RequestUri.AbsolutePath.Contains("/merchant/testdqr/"), "Test endpoint mismatch.");
            var payload = JObject.Parse(body);
            Assert((string)payload["mid"] == "MID123", "Merchant ID mismatch.");
            string action = request.RequestUri.Segments.Last();
            JObject result;
            if (action == "generate")
            {
                GenerateCalls++;
                if (NetworkFailure) throw new HttpRequestException("Simulated connection loss.");
                Reference = (string)payload["orderId"];
                Assert(Reference.Length >= 12 && Reference.Length <= 50, "Invalid order length.");
                Assert((decimal)payload["amount"] == 10.25m, "Generation amount mismatch.");
                result = new JObject { ["statusCode"] = GenerateError ? 400 : 0,
                    ["orderId"] = BadOrder ? "DIFFERENT-ORDER" : Reference,
                    ["qrData"] = "upi://pay?pa=test@bank&am=10.25&cu=INR" };
            }
            else if (action == "cancel")
            {
                Assert((decimal)payload["refundAmount"] == 10.25m, "Cancellation amount mismatch.");
                Refunded = true;
                result = new JObject { ["statusCode"] = "0", ["statusMessage"] = "Refund initiated" };
            }
            else
            {
                StatusCalls++;
                int code = Refunded ? 1002 : Transient && StatusCalls == 1 ? 503 : StatusCode;
                result = new JObject { ["statusCode"] = code, ["orderId"] = (string)payload["orderId"], ["bankRRN"] = "TEST-RRN" };
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            string responseText = result.ToString(Formatting.None);
            if (Encrypted)
            {
                string key = "response-key-test";
                responseText = (string)Crypto("EncryptBody", responseText, key);
                using (var rsa = (RSACryptoServiceProvider)Crypto("ReadKey", System.IO.Path.Combine(Keys, "public.der"), false))
                    response.Headers.Add("key", Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(key), false)));
            }
            response.Content = new StringContent(responseText);
            return response;
        }
    }
    static CompletedTransactionData Receive(PaymentGatewayBase gateway, CancellationToken token)
    {
        return gateway.ReceivePayment(7, 1012, token, "0123456789abcdef0123456789abcdef", 1, 10, 10.25m, 9).GetAwaiter().GetResult();
    }
    public static void Run(string keys)
    {
        string json = "{\"text\":\"INR \\u20b9 test\"}";
        string cipher = (string)Crypto("EncryptBody", json, "test-key");
        Assert((string)Crypto("DecryptBody", cipher, "test-key") == json, "AES round trip failed.");
        Assert(cipher != (string)Crypto("EncryptBody", json, "test-key"), "IV must be random.");
        foreach (bool encrypted in new[] { false, true })
        {
            var handler = new Stub { Keys = keys, Encrypted = encrypted };
            var gateway = Gateway(handler, keys, !encrypted);
            bool qr = false;
            gateway.QrReady = data => qr = data.StartsWith("upi://pay?");
            string reference = null;
            gateway.ProviderReferenceReady = value => reference = value;
            var completed = Receive(gateway, CancellationToken.None);
            Assert(qr && reference != null && completed != null && completed.Amount == 10.25m
                && completed.PaymentGatewayID == 31 && completed.MopID == 1012, "Successful payment identity mismatch.");
            Assert(handler.GenerateCalls == 1 && !gateway.IsInProgress, "Generation/state mismatch.");
        }
        var retry = new Stub { Keys = keys, Transient = true };
        Assert(Receive(Gateway(retry, keys, true), CancellationToken.None) != null && retry.GenerateCalls == 1 && retry.StatusCalls == 2,
            "Status retries must reuse the same order.");
        foreach (int code in new[] { 1000, 1001, 1002 })
        {
            var handler = new Stub { Keys = keys, StatusCode = code };
            var gateway = Gateway(handler, keys, true);
            Assert(Receive(gateway, CancellationToken.None) == null && gateway.LastRequestFinal, "Terminal order must not count as paid.");
        }
        var cancelHandler = new Stub { Keys = keys, StatusCode = 0 };
        var canceledGateway = Gateway(cancelHandler, keys, true);
        bool persistedRefund = false;
        canceledGateway.RefundRequestedReady = () => persistedRefund = true;
        Assert(Receive(canceledGateway, new CancellationToken(true)) == null && persistedRefund && canceledGateway.LastRequestFinal,
            "Cancellation/refund must not count as received.");
        var refundHandler = new Stub { Keys = keys, StatusCode = 100 };
        var refundGateway = Gateway(refundHandler, keys, true);
        refundGateway.RefundRequested = true;
        Assert(refundGateway.VerifyPayment(7, 1012, "0123456789abcdef0123456789abcdef", 10.25m).GetAwaiter().GetResult() == null,
            "A refund-pending order must not be marked paid.");
        foreach (int scenario in new[] { 0, 1, 2 })
        {
            var handler = new Stub { Keys = keys, BadOrder = scenario == 0, GenerateError = scenario == 1, NetworkFailure = scenario == 2 };
            var gateway = Gateway(handler, keys, true);
            bool failed = false;
            try { Receive(gateway, CancellationToken.None); } catch { failed = true; }
            Assert(failed && !gateway.IsInProgress && handler.GenerateCalls == 1, "Failed generation must never be duplicated.");
            if (scenario == 2) Assert(!gateway.LastRequestFinal, "Network uncertainty must remain unresolved.");
        }
        var table = new DataTable();
        table.Columns.Add("MOPID", typeof(int)); table.Columns.Add("MOPNAME", typeof(string));
        table.Columns.Add("ISGATEWAYCONFIGURABLE", typeof(bool)); table.Columns.Add("PAYMENTGATEWAYINFOID", typeof(int));
        table.Columns.Add("PAYMENTGATEWAYINFOTYPE", typeof(string)); table.Columns.Add("PAYMENTGATEWAYCONFIGDATA", typeof(string));
        table.Columns.Add("PAYMENTGATEWAYADDITIONALCONFIG", typeof(string));
        table.Rows.Add(1, "CASH", false, DBNull.Value, "", "", "");
        table.Rows.Add(1011, "QR Pay", false, DBNull.Value, "", "", "");
        const string pine = "{\"MerchantID\":1,\"SecurityToken\":\"test\",\"PaymentURL\":\"https://pine.test.invalid/\"}";
        table.Rows.Add(2, "Renamed Card", true, 11, "PineLabs", pine, "{\"ClientID\":1,\"StoreID\":1}");
        table.Rows.Add(1012, "MOBIKWIK", true, 12, "PineLabs", pine, "{\"ClientID\":2,\"StoreID\":2}");
        table.Rows.Add(2000, "Bad Gateway", true, 13, "Unknown", "{}", "{}");
        var config = new CounterPaymentConfiguration(); config.Load(table);
        Assert(config.Modes.Count() == 5 && config.Find(1009) == null, "Unavailable MOP must not be invented.");
        Assert(!config.Find(1011).RequiresGateway && config.Find(1011).Gateway == null, "Printed QR remains manual.");
        Assert(config.Find(2).Gateway != config.Find(1012).Gateway && config.Find(1012).Gateway.GatewayType == "PineLabs",
            "Provider must follow the mapping, not the MOP label.");
        Assert(config.Find(2000).RequiresGateway && config.Find(2000).Gateway == null, "Invalid gateway must not fall back to manual.");
        var original = new PaymentAttempt { GatewayID = 11, GatewayType = "PineLabs", BaseSettings = pine,
            AdditionalSettings = "{\"ClientID\":1,\"StoreID\":1}" };
        table.Rows[2]["PAYMENTGATEWAYINFOID"] = 99; config.Load(table);
        var recovered = original.CreateGateway().ForceReceivePayment(7, 2, 10.25m, "ref", "rrn", "transaction");
        Assert(recovered.PaymentGatewayID == 11, "Recovery must use the original gateway snapshot.");
    }
}
