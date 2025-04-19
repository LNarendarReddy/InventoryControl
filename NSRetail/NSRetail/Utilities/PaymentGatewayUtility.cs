using DevExpress.CodeParser;
using DevExpress.PivotGrid.DataCalculation;
using DevExpress.Utils.Text;
using DevExpress.XtraRichEdit.Import.OpenXml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NSRetail.Utilities
{
    public enum PaymentMode
    {
        Card = 1,
        UPI = 10
    }
    public class PaymentGatewayUtility
    {
        public static async void TestPayment(DataRow dr, PaymentMode paymentMode)
        {
            RequestPayload payload = new RequestPayload();
            payload.TransactionNumber = 
                Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("+", "").Replace("/", "").
                Replace("=", "").Substring(0, 10);
            payload.SequenceNumber = 1;
            payload.AllowedPaymentMode = paymentMode;
            payload.ClientID = Convert.ToString(dr["CLIENTID"]);
            payload.Amount = 100;
            payload.UserID = Utility.FullName;
            payload.MerchantID = Convert.ToString(dr["MERCHANTID"]);
            payload.StoreID = Convert.ToString(dr["STOREID"]);
            payload.SecurityToken = Convert.ToString(dr["SECURITYTOKEN"]);
            payload.AutoCancelDurationInMinutes =
                Convert.ToInt32(dr["CANCELATIONDURATION"]);
            var result = await PaymentGatewayUtility.PostAsyncWithJson(payload,Convert.ToString(dr["PAYMENTURL"]));
        }

        public static async Task<string> PostAsyncWithJson(RequestPayload requestPayload, string PaymentURL)
        {
            var client = new HttpClient();
            var stringPayload = JsonConvert.SerializeObject(requestPayload);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(PaymentURL, httpContent);
            return await response.Content.ReadAsStringAsync();
        }
    }

    public class RequestPayload
    {
        private string _TransactionNumber = string.Empty;
        private int _SequenceNumber = 0;
        private PaymentMode _AllowedPaymentMode;
        private string _ClientID = string.Empty;
        private int _Amount = 100;
        private string _UserID = string.Empty;
        private string _MerchantID = string.Empty;
        private string _StoreID = string.Empty;
        private string _SecurityToken = string.Empty;
        private int _AutoCancelDurationInMinutes = 5;

        public string TransactionNumber
        {
            get { return _TransactionNumber; }
            set { _TransactionNumber = value; }
        }
        public int SequenceNumber
        {
            get { return _SequenceNumber; }
            set { _SequenceNumber = value; }
        }
        public PaymentMode AllowedPaymentMode
        {
            get { return _AllowedPaymentMode; }
            set { _AllowedPaymentMode = value; }
        }
        public string ClientID
        {
            get { return _ClientID; }
            set { _ClientID = value; }
        }
        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string MerchantID
        {
            get { return _MerchantID; }
            set { _MerchantID = value; }
        }
        public string StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        public string SecurityToken
        {
            get { return _SecurityToken; }
            set { _SecurityToken = value; }
        }
        public int AutoCancelDurationInMinutes
        {
            get { return _AutoCancelDurationInMinutes; }
            set { _AutoCancelDurationInMinutes = value; }
        }
    }
}
