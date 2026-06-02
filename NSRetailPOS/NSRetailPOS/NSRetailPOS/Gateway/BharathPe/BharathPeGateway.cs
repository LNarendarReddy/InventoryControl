using Newtonsoft.Json;
using NSRetailPOS.Gateway.BharathPe;
using NSRetailPOS.Gateway.PineLabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public const string InitiateJourney = "initiatetransaction";
        public const string StatusJourney = "checkstatus";
        public const string CancelJourney = "canceltransaction";

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public int PaymentGatewayID { get; }
        public string AdditionalSettings { get; }

        public override string GatewayType => "BharathPe";

        public BharathPeGateway(int paymentGatewayID, string baseSettings, string additionalSettings)
        {
            baseSettingsObj = JsonConvert.DeserializeObject<BharatPePaymentGateway>(baseSettings);
            requestSettings = JsonConvert.DeserializeObject<RequestSettings>(additionalSettings);

            IsValid = !string.IsNullOrEmpty(requestSettings?.userName) && !string.IsNullOrEmpty(requestSettings?.password);
            authTokenClient = new HttpClient();
            requestClient = new HttpClient() { BaseAddress = new Uri(baseSettingsObj.RequestURI) };

            PaymentGatewayID = paymentGatewayID;
            AdditionalSettings = additionalSettings;
        }

        private async Task<string> GetAccessToken()
        {
            var response = await authTokenClient.PostAsync(
                baseSettingsObj.AuthURI,
                baseSettingsObj.AuthSettings.GetUrlEncodedContent());

            response.EnsureSuccessStatusCode();

            string tokenResponse = await response.Content.ReadAsStringAsync();

            var tokenData = JsonConvert.DeserializeObject<AuthTokenResponse>(tokenResponse);

            return tokenData?.access_token;
        }
        
        private string BuildJourneyUrl(string journey)
        {
            return $"{baseSettingsObj.RequestURI}clientid={baseSettingsObj.AuthSettings.client_id}&journey={journey}";
        }
        
        protected IPaymentRequest GetPaymentRequest(params object[] parameters)
        {
            return new RequestSettings()
            {
                userName = requestSettings.userName,
                password = requestSettings.password,
                invoiceNo = Convert.ToString(parameters[0]),
                transactionAmount = Convert.ToDecimal(parameters[3]).ToString("0.00"),
                additionalAmount = "0.00",

                // Card = 1, UPI = 22
                transactionType = Convert.ToString(parameters[2]) == "Card"
                    ? "1"
                    : "22"
            };
        }

        protected override async Task<IPaymentResponse> SendRequest(IPaymentRequest paymentRequest,CancellationToken token)
        {
            try
            {
                StatusUpdate("Generating BharathPe token");

                string tokenValue = await GetAccessToken();

                requestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);

                StatusUpdate("Sending payment request");

                string jsonPayload = JsonConvert.SerializeObject(paymentRequest);

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await requestClient.PostAsync(BuildJourneyUrl(InitiateJourney),content);

                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    StatusUpdate($"HTTP Error : {responseString}");
                    return null;
                }

                BharatPePaymentResponse paymentResponse = JsonConvert.DeserializeObject<BharatPePaymentResponse>(responseString);

                // request level failure
                if (paymentResponse?.statusCode != "100")
                {
                    StatusUpdate(paymentResponse?.message ?? "Request failed");
                    return null;
                }

                // business failure
                if (!string.IsNullOrEmpty(paymentResponse?.data?.errorCode))
                {
                    StatusUpdate(
                        $"{paymentResponse.data.errorMessage} " +
                        $"({paymentResponse.data.errorCode})");

                    // IMPORTANT CASE
                    // Transaction already exists
                    if (paymentResponse.data.errorCode == "104")
                    {
                        StatusUpdate("Existing transaction detected");
                    }

                    return null;
                }

                // success
                if (paymentResponse?.data?.resultCode == "200")
                {
                    StatusUpdate("Transaction initiated successfully");
                    return paymentResponse;
                }

                StatusUpdate(
                    paymentResponse?.data?.resultMessage
                    ?? "Unknown error");

                return null;
            }
            catch (Exception ex)
            {
                StatusUpdate(ex.Message);
                return null;
            }
        }

        private async Task<BharatPeStatusResponse> CheckSingleStatus(BharatPeStatusRequest request)
        {
            try
            {
                StatusUpdate("Checking transaction status");

                string tokenValue = await GetAccessToken();

                requestClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenValue);

                var content = new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage response =
                    await requestClient.PostAsync(
                        BuildJourneyUrl(StatusJourney),
                        content);

                string responseString =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    StatusUpdate($"HTTP Error : {responseString}");
                    return null;
                }

                BharatPeStatusResponse statusResponse =
                    JsonConvert.DeserializeObject<BharatPeStatusResponse>(responseString);

                if (statusResponse?.statusCode != "100")
                {
                    StatusUpdate(statusResponse?.message ?? "Status check failed");
                    return null;
                }

                return statusResponse;
            }
            catch (Exception ex)
            {
                StatusUpdate(ex.Message);
                return null;
            }
        }

        protected override async Task<IStatusResponse> CheckRequestStatus(IPaymentRequest paymentRequest,
                IStatusRequest statusRequest, CancellationToken token, int forceCheckCount = 0)
        {
            BharatPeStatusRequest request =
                statusRequest as BharatPeStatusRequest;

            while (!token.IsCancellationRequested)
            {
                for (int i = 5; i > 0; i--)
                {
                    StatusUpdate($"Checking status in {i} seconds");
                    await Task.Delay(1000);
                }

                BharatPeStatusResponse statusResponse =
                    await CheckSingleStatus(request);

                if (statusResponse == null)
                    continue;

                BharatPeTransaction transaction =
                    statusResponse?.data?.transactions?.FirstOrDefault();

                if (transaction == null)
                    continue;

                switch (transaction.statusCode)
                {
                    case "2":
                    case "7":
                        StatusUpdate("Payment approved");
                        return statusResponse;

                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "8":
                        StatusUpdate($"Payment failed : {transaction.transactionStatus}");
                        return null;

                    case "1":
                        StatusUpdate("Transaction in progress");
                        continue;
                }
            }

            if (token.IsCancellationRequested)
            {
                await CancelRequest(new BharatPeCancelRequest()
                {
                    userName = request.userName,
                    referenceId = request.referenceId
                });
            }

            return null;
        }

        protected override async Task<bool> CancelRequest(ICancelRequest cancelRequest)
        {
            try
            {
                StatusUpdate("Cancelling transaction");

                string tokenValue = await GetAccessToken();

                requestClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenValue);

                var content = new StringContent(
                    JsonConvert.SerializeObject(cancelRequest),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage response =
                    await requestClient.PostAsync(
                        BuildJourneyUrl(CancelJourney),
                        content);

                string responseString =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    StatusUpdate($"HTTP Error : {responseString}");
                    return false;
                }

                BharatPeCancelResponse cancelResponse =
                    JsonConvert.DeserializeObject<BharatPeCancelResponse>(responseString);

                if (cancelResponse?.statusCode != "100")
                {
                    StatusUpdate(cancelResponse?.message ?? "Cancel request failed");
                    return false;
                }

                // CASE 1
                // Already completed
                if (cancelResponse?.data?.message?
                    .Contains("already completed",
                        StringComparison.OrdinalIgnoreCase) == true)
                {
                    StatusUpdate(cancelResponse.data.message);

                    // IMPORTANT:
                    // transaction actually succeeded
                    return false;
                }

                // CASE 2
                // Already cancelled
                if (cancelResponse?.data?.message?
                    .Contains("already cancelled",
                        StringComparison.OrdinalIgnoreCase) == true)
                {
                    StatusUpdate(cancelResponse.data.message);

                    // treat as cancelled
                    return true;
                }

                // CASE 3
                // Proper cancellation response
                if (cancelResponse?.data?.cancelNotificationResponse != null)
                {
                    var notification =
                        cancelResponse.data.cancelNotificationResponse;

                    StatusUpdate(notification.message);

                    bool success =
                        notification.responseCode == "00"
                        || string.Equals(notification.result,
                            "success",
                            StringComparison.OrdinalIgnoreCase);

                    return success;
                }

                // CASE 4
                // fallback transaction status handling
                string txnStatus =
                    cancelResponse?.data?.transactionStatus?
                        .FirstOrDefault()?.statusCode;

                if (txnStatus == "1")
                {
                    StatusUpdate("Cancellation in progress");
                    return true;
                }

                StatusUpdate(cancelResponse?.data?.message ?? "Unknown cancel response");

                return false;
            }
            catch (Exception ex)
            {
                StatusUpdate(ex.Message);
                return false;
            }
        }

        public override async Task<CompletedTransactionData> ReceivePayment(int billID, int mopID, CancellationToken token, params object[] parameters)
        {
            if (IsInProgress)
                return null;

            IsInProgress = true;

            try
            {
                await _semaphore.WaitAsync();

                RequestSettings paymentRequest =
                    GetPaymentRequest(parameters) as RequestSettings;

                BharatPePaymentResponse paymentResponse =
                    await SendRequest(paymentRequest, token) as BharatPePaymentResponse;

                if (paymentResponse == null)
                    return null;

                BharatPeStatusRequest statusRequest = new()
                {
                    userName = paymentRequest.userName,
                    referenceId = paymentResponse.data.referenceId
                };

                BharatPeStatusResponse statusResponse = await CheckRequestStatus(paymentRequest, statusRequest, token) as BharatPeStatusResponse;

                if (statusResponse == null)
                    return null;

                return new CompletedTransactionData()
                {
                    Amount = Convert.ToDecimal(paymentRequest.transactionAmount),
                    BillID = billID,
                    MopID = mopID,
                    PaymentRequest = paymentRequest,
                    StatusResponse = statusResponse,
                    PaymentGatewayID = PaymentGatewayID,
                    AdditionalSettings = AdditionalSettings
                };
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

        public override CompletedTransactionData ForceReceivePayment(int billID, int mopID, params object[] parameters)
        {
            CompletedTransactionData completedTransactionData = new()
            {
                Amount = decimal.Parse(parameters[0].ToString()),
                BillID = billID,
                MopID = mopID,
                AdditionalSettings = AdditionalSettings,
                PaymentGatewayID = PaymentGatewayID,

                StatusResponse = new BharatPeStatusResponse()
                {
                    statusCode = "100",
                    message = "Force added missing payment",

                    data = new BharatPeStatusData()
                    {
                        referenceId = parameters[1]?.ToString(),

                        transactions =
                        [
                            new BharatPeTransaction()
                            {
                                retrievalReferenceNumber = parameters[2]?.ToString(),
                                transactionId = parameters[3]?.ToString(),
                                transactionStatus = "Force added missing payment",
                                statusCode = "2",
                                amount = parameters[0].ToString()
                            }
                        ]
                    }
                }
            };

            return completedTransactionData;
        }

        public override async Task<CompletedTransactionData> VerifyPayment(int billID, int mopID, params object[] parameters)
        {
            string referenceId = parameters[0]?.ToString();

            BharatPeStatusRequest statusRequest = new()
            {
                userName = requestSettings.userName,
                referenceId = referenceId
            };

            BharatPeStatusResponse statusResponse = await CheckSingleStatus(statusRequest);

            BharatPeTransaction transaction = statusResponse?.data?.transactions?.FirstOrDefault();

            if (transaction == null)
                return null;

            switch (transaction.statusCode)
            {
                case "2":
                case "7":
                    return new CompletedTransactionData()
                    {
                        BillID = billID,
                        MopID = mopID,
                        Amount = Convert.ToDecimal(transaction.amount),
                        StatusResponse = statusResponse,
                        PaymentGatewayID = PaymentGatewayID,
                        AdditionalSettings = AdditionalSettings
                    };
            }

            return null;
        }
    }
}
