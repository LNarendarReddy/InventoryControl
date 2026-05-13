using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSRetailPOS.Gateway
{
    internal class BharathPeGateway : PaymentGatewayBase
    {

        HttpClient authTokenClient;
        HttpClient requestClient;
        BharatPePaymentGateway baseSettingsObj;
        RequestSettings requestSettings;
        public const string BillingAPI = "UploadBilledTransaction";
        public const string StatusAPI = "GetCloudBasedTxnStatus";
        public const string CancelAPI = "CancelTransaction";
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public int PaymentGatewayID { get; }
        public string AdditionalSettings { get; }

        public override string GatewayType => "BharathPe";

        public BharathPeGateway(int paymentGatewayID, string baseSettings, string additionalSettings)
        {
            baseSettingsObj = JsonConvert.DeserializeObject<BharatPePaymentGateway>(baseSettings);
            requestSettings = JsonConvert.DeserializeObject<RequestSettings>(additionalSettings);
            
            IsValid = !string.IsNullOrEmpty(requestSettings?.UserName) && !string.IsNullOrEmpty(requestSettings?.Password);
            authTokenClient = new HttpClient();
            requestClient = new HttpClient() { BaseAddress = new Uri(baseSettingsObj.RequestURI) };

            PaymentGatewayID = paymentGatewayID;
            AdditionalSettings = additionalSettings;
        }


        public override async Task<CompletedTransactionData> ReceivePayment(int billID, int mopID, CancellationToken token, params object[] parameters)
        {
            // Avoid parallel requests
            if (IsInProgress) return null;

            IsInProgress = true;
            try
            {
                await _semaphore.WaitAsync();

                //get token
                var response = await authTokenClient.PostAsync(baseSettingsObj.AuthURI, baseSettingsObj.AuthSettings.GetUrlEncodedContent());

                // Ensure we got a 2xx success code
                response.EnsureSuccessStatusCode();

                Dictionary<string, string> keyValues = new()
                {
                    { "clientid", baseSettingsObj.AuthSettings.client_id },
                    { "journey", "initiatetransaction" },
                };

                string jsonPayload = JsonConvert.SerializeObject(baseSettingsObj.RequestSettings);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // 4. Add Headers
                // Replace "YOUR_TOKEN_HERE" with the actual bearer token value
                requestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_TOKEN_HERE");


                // 5. Execute the POST request
                response = await requestClient.PostAsync(baseSettingsObj.RequestURI, content);

                // 6. Read and display the response
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response: {responseString}");



                return null;
            }
            catch (Exception ex)
            {
                StatusUpdate?.Invoke(ex.Message);
                Error?.Invoke(ex);
                return null;
            }
            finally
            {
                IsInProgress = false;
                _semaphore.Release();
            }
        }

        protected override Task<bool> CancelRequest(ICancelRequest cancelRequest)
        {
            return null;
        }

        protected override Task<IStatusResponse> CheckRequestStatus(IPaymentRequest paymentRequest, IStatusRequest statusRequest, CancellationToken token, int forceCheckCount = 0)
        {
            return null;
        }

        protected IPaymentRequest GetPaymentRequest(params object[] parameters)
        {
            return null;
        }

        protected override Task<IPaymentResponse> SendRequest(IPaymentRequest paymentRequest, CancellationToken token)
        {
            return null;
        }

        public class BharatPePaymentGateway
        {
            public AuthSettings AuthSettings { get; set; }
            public RequestSettings RequestSettings { get; set; }
            public string AuthURI = "https://auth.mod91.io/realms/mif/protocol/openid-connect/token";
            public string RequestURI = "https://hermes.mod91.io/mif/v1/process?";

            public BharatPePaymentGateway()
            {
                AuthSettings = new AuthSettings();
                RequestSettings = new RequestSettings();
            }
        }

        public class AuthSettings
        {
            public string grant_type = "client_credentials";
            public string client_id { get; set; }
            public string client_secret { get; set; }

            public FormUrlEncodedContent GetUrlEncodedContent()
            {
                var values = new Dictionary<string, string>
                {
                    { "client_id", client_id },
                    { "grant_type", grant_type},
                    { "client_secret", client_secret }
                };

                return new FormUrlEncodedContent(values);
            }
        }

        public class RequestSettings
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string InvoiceNo { get; set; }
            public string TransactionAmount { get; set; }
            public string AdditionalAmount { get; set; }
            public string TransactionType { get; set; }
            public string ReferenceId { get; set; }
        }
    }
}
