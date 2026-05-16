using System.Collections.Generic;
using System.Net.Http;

namespace NSRetailPOS.Gateway.BharathPe
{
    public class Models
    {

    }

    public class BharatPePaymentGateway
    {
        public AuthSettings AuthSettings { get; set; }
        public RequestSettings RequestSettings { get; set; }
        public string AuthURI = "https://tauth.mod91.io/realms/mif/protocol/openid-connect/token";
        public string RequestURI = "https://thermes.mod91.io/mif/v1/process?";

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

    public class RequestSettings : IPaymentRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string invoiceNo { get; set; }
        public string transactionAmount { get; set; }
        public string additionalAmount { get; set; }
        public string transactionType { get; set; }
        public string referenceId { get; set; }
    }

    public class AuthTokenResponse
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public int refresh_expires_in { get; set; }

        public string token_type { get; set; }

        public string scope { get; set; }
    }

    public class BharatPeBaseResponse
    {
        public string statusCode { get; set; }
        public string message { get; set; }
    }

    public class BharatPePaymentResponse : BharatPeBaseResponse, IPaymentResponse
    {
        public BharatPePaymentData data { get; set; }
    }

    public class BharatPePaymentData
    {
        public string resultCode { get; set; }
        public string resultMessage { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public string referenceId { get; set; }
        public string qrString { get; set; }
    }

    public class BharatPeStatusRequest : IStatusRequest
    {
        public string userName { get; set; }
        public string referenceId { get; set; }
    }

    public class BharatPeCancelRequest : ICancelRequest
    {
        public string userName { get; set; }
        public string referenceId { get; set; }
    }

    public class BharatPeStatusResponse : BharatPeBaseResponse, IStatusResponse
    {
        public BharatPeStatusData data { get; set; }
    }

    public class BharatPeStatusData
    {
        public List<BharatPeTransaction> transactions { get; set; }
        public string referenceId { get; set; }
    }

    public class BharatPeTransaction
    {
        public string statusCode { get; set; }
        public string transactionStatus { get; set; }
        public string amount { get; set; }
        public string retrievalReferenceNumber { get; set; }
        public string approvalCode { get; set; }
        public string cardType { get; set; }
        public string transactionId { get; set; }
    }
    public class BharatPeCancelResponse : BharatPeBaseResponse
    {
        public BharatPeCancelData data { get; set; }
    }

    public class BharatPeCancelData
    {
        public string message { get; set; }

        public string referenceId { get; set; }

        public BharatPeCancelNotificationResponse cancelNotificationResponse { get; set; }

        public List<BharatPeTransactionStatus> transactionStatus { get; set; }
    }

    public class BharatPeCancelNotificationResponse
    {
        public string message { get; set; }

        public string notificationIdentifier { get; set; }

        public string responseCode { get; set; }

        public string result { get; set; }

        public string webTransactionId { get; set; }
    }

    public class BharatPeTransactionStatus
    {
        public string statusCode { get; set; }
    }
}
