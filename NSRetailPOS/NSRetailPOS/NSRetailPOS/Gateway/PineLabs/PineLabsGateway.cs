﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSRetailPOS.Gateway.PineLabs
{
    internal class PineLabsGateway : PaymentGatewayBase
    {
        HttpClient gatewayClient;
        RequestBase requestBase;
        public const string BillingAPI = "UploadBilledTransaction";
        public const string StatusAPI = "GetCloudBasedTxnStatus";
        public const string CancelAPI = "CancelTransaction";

        public override string GatewayTpye => "PineLabs";

        public int PaymentGatewayID { get; }
        public string AdditionalSettings { get; }

        public PineLabsGateway(int paymentGatewayID, string baseSettings, string additionalSettings) 
        {
            requestBase = JsonConvert.DeserializeObject<RequestBase>(baseSettings);
            RequestBase additionalPayLoad = JsonConvert.DeserializeObject<RequestBase>(additionalSettings);
            requestBase.StoreID = additionalPayLoad.StoreID;
            requestBase.ClientID = additionalPayLoad.ClientID;
            gatewayClient = new HttpClient() { BaseAddress = new Uri(requestBase.PaymentURL) };
            PaymentGatewayID = paymentGatewayID;
            AdditionalSettings = additionalSettings;
        }

        protected override async Task<bool> CancelRequest(ICancelRequest cancelRequest)
        {
            StatusUpdate("sending cancel request");
            var stringPayload = JsonConvert.SerializeObject(cancelRequest);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await gatewayClient.PostAsync(CancelAPI, httpContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ResponseBase cancelResponse = JsonConvert.DeserializeObject<ResponseBase>(await responseMessage.Content.ReadAsStringAsync());
                bool success = cancelResponse.ResponseCode == 0;
                StatusUpdate(success ? "cancel success" : cancelResponse.ResponseMessage);
                return success;
            }

            StatusUpdate($"Cancel failed: {await responseMessage.Content.ReadAsStringAsync()}");
            return false;
        }

        protected override async Task<IStatusResponse> CheckRequestStatus(IPaymentRequest paymentRequest, IStatusRequest statusRequest
            , CancellationToken token, int forceCheckCount = 0)
        {
            StatusRequest pineLabsStatusRequest = (StatusRequest)statusRequest;
            PaymentRequest pineLabsPaymentRequest = (PaymentRequest)paymentRequest;

            while (!token.IsCancellationRequested)
            {
                for (int i = 5; i > 0; i--)
                {
                    StatusUpdate($"Waiting {i} seconds to check payment status");
                    await Task.Delay(1000);
                }

                StatusUpdate("Checking payment status");
                var stringPayload = JsonConvert.SerializeObject(pineLabsStatusRequest);
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await gatewayClient.PostAsync(StatusAPI, httpContent);
                if(responseMessage.IsSuccessStatusCode)
                {
                    StatusResponse statusResponse = JsonConvert.DeserializeObject<StatusResponse>(await responseMessage.Content.ReadAsStringAsync());
                    if (statusResponse.ResponseCode == 0)
                    {
                        return statusResponse;
                    }
                    else
                    {
                        StatusUpdate(statusResponse.ResponseMessage);
                    }
                }
                else
                {
                    StatusUpdate($"Payment status check failed: {await responseMessage.Content.ReadAsStreamAsync()}");
                }
            }

            if (token.IsCancellationRequested && forceCheckCount <= 10)
            {
                StatusUpdate("user cancel initiated");
                CancelRequest cancelRequest = new CancelRequest();
                CopyValues(cancelRequest);
                cancelRequest.PlutusTransactionReferenceID = pineLabsStatusRequest.PlutusTransactionReferenceID;
                cancelRequest.Amount = pineLabsPaymentRequest.Amount;
                if (await CancelRequest(cancelRequest))
                    return null;
                else
                {
                    // override forceCount in case of user cancelled but cancel operation is failing
                    await CheckRequestStatus(paymentRequest, statusRequest, token, ++forceCheckCount);
                }
            }

            return null;
        }

        protected override async Task<IPaymentResponse> SendRequest(IPaymentRequest paymentRequest, CancellationToken token)
        {
            PaymentRequest pineLabsPaymentRequest = paymentRequest as PaymentRequest;
            StatusUpdate("Sending request to POS");
            var stringPayload = JsonConvert.SerializeObject(pineLabsPaymentRequest);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await gatewayClient.PostAsync(BillingAPI, httpContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                StatusUpdate("Processing response");
                PaymentResponse paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(await responseMessage.Content.ReadAsStringAsync());
                if (paymentResponse.ResponseCode == 0)
                {
                    StatusUpdate("Request successfully sent");
                    return paymentResponse;
                }
                else
                {
                    StatusUpdate(paymentResponse.ResponseMessage);
                    if (paymentResponse.AdditionalInfo != null && paymentResponse.AdditionalInfo.Any())
                    {
                        AdditionalInfo existingReference = paymentResponse.AdditionalInfo.FirstOrDefault(x =>
                            string.Equals(x.Tag, "ExistingReferenceId", StringComparison.OrdinalIgnoreCase));
                        if (existingReference != null)
                        {
                            paymentResponse.PlutusTransactionReferenceID = Convert.ToInt32(existingReference.value);
                            AdditionalInfo existingReferenceValue = paymentResponse.AdditionalInfo.FirstOrDefault(x =>
                                string.Equals(x.Tag, "Amount", StringComparison.OrdinalIgnoreCase));
                            pineLabsPaymentRequest.Amount = Convert.ToInt32(existingReferenceValue?.value);

                            return paymentResponse;
                        }
                    }
                } 
            }

            StatusUpdate($"Request failed: {await responseMessage.Content.ReadAsStringAsync()}");
            return null;
        }
        
        public override async Task<CompletedTransactionData> ReceivePayment(int billID, int mopID, CancellationToken token, params object[] parameters)
        {
            IsInProgress = true;
            try
            {
                PaymentRequest paymentRequest = GetPaymentRequest(parameters) as PaymentRequest;
                PaymentResponse paymentResponse = await SendRequest(paymentRequest, token) as PaymentResponse;
                if (paymentResponse == null) return null;

                StatusRequest statusRequest = new StatusRequest();
                CopyValues(statusRequest);
                statusRequest.PlutusTransactionReferenceID = paymentResponse.PlutusTransactionReferenceID;

                IStatusResponse statusResponse = await CheckRequestStatus(paymentRequest, statusRequest, token);
                if (statusResponse == null) return null;

                paymentRequest.Amount = paymentRequest.Amount / 100;
                StatusUpdate($"Payment receive completed for amount : {paymentRequest.Amount}");

                CompletedTransactionData successTransactionData = new CompletedTransactionData()
                {
                    Amount = paymentRequest.Amount,
                    MopID = mopID,
                    BillID = billID,
                    PaymentRequest = paymentRequest,
                    StatusResponse = statusResponse,
                    PaymentGatewayID = PaymentGatewayID,
                    AdditionalSettings = AdditionalSettings
                };
                return successTransactionData;
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
            }
        }

        protected override IPaymentRequest GetPaymentRequest(params object[] parameters)
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            CopyValues(paymentRequest);
            paymentRequest.TransactionNumber = parameters[0].ToString();
            paymentRequest.SequenceNumber = Convert.ToInt32(parameters[1]);
            paymentRequest.AllowedPaymentMode = (PaymentMode)parameters[2];
            paymentRequest.Amount = Convert.ToDecimal(parameters[3]) * 100; //multiply by 100
            paymentRequest.UserID = parameters[4].ToString();
            paymentRequest.AutoCancelDurationInMinutes = paymentRequest.AllowedPaymentMode == PaymentMode.UPI ? 5 : 3;
            return paymentRequest;
        }

        private void CopyValues(RequestBase target)
        {
            target.ClientID = requestBase.ClientID;
            target.MerchantID = requestBase.MerchantID;
            target.SecurityToken = requestBase.SecurityToken;
            target.AutoCancelDurationInMinutes = requestBase.AutoCancelDurationInMinutes;
            target.StoreID = requestBase.StoreID;
            target.PaymentURL = requestBase.PaymentURL;
        }
    }


    class RequestBase
    {
        public int ClientID { get; set; }

        public int MerchantID { get; set; }

        public string SecurityToken { get; set; }

        public int AutoCancelDurationInMinutes { get; set; }

        public string PaymentURL { get; set; }

        //Store level values
        public int StoreID { get; set; }
    }

    class PaymentRequest : RequestBase, IPaymentRequest
    {
        public string TransactionNumber { get; set; }
        public int SequenceNumber { get; set; }
        public PaymentMode AllowedPaymentMode { get; set; }
        public decimal Amount { get; set; }
        public string UserID { get; set; }
    }

    class StatusRequest : RequestBase, IStatusRequest
    {
        public int PlutusTransactionReferenceID { get; set; }
    }
    
    class CancelRequest : StatusRequest, ICancelRequest
    {
        public decimal Amount { get; set; }
    }

    class ResponseBase 
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public int PlutusTransactionReferenceID { get; set; }

    }

    class PaymentResponse : ResponseBase, IPaymentResponse
    {
        public List<AdditionalInfo> AdditionalInfo { get; set; }
    }

    class StatusResponse : ResponseBase, IStatusResponse
    {
        public List<AdditionalInfo> TransactionData { get; set; }
    }

    class AdditionalInfo
    {
        public string Tag { get; set; }
        public string value { get; set; }
    }

    enum PaymentMode
    {
        Card = 1,
        UPI = 10
    }
}
