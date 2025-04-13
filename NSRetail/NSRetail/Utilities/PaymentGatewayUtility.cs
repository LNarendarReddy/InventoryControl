using DevExpress.PivotGrid.DataCalculation;
using DevExpress.Utils.Text;
using DevExpress.XtraRichEdit.Import.OpenXml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NSRetail.Utilities
{
    public enum PamentMode
    {
        Card = 1,
        UPI = 10
    }
    public class PaymentGatewayUtility
    {
        public static string PaymentURL = "https://instore-ism.uat.pinelabs.com/api/CloudBasedIntegration/V1/UploadBilledTransaction";

        public static async Task PostAsyncwithdict()
        {
            var client = new HttpClient();
            var values = new Dictionary<string, string> 
            { 
                { "TransactionNumber", "1" },
                { "SequenceNumber", "1" },
                { "AllowedPaymentMode", "1" },
                { "ClientID", "1013073" },
                { "Amount", "1" },
                { "UserID", "Naren" },
                { "MerchantID", "29895" },
                { "StoreID", "10737583" },
                { "SecurityToken", "08b1b2ff-6d0d-4611-a757-394b6972f080" },
                { "AutoCancelDurationInMinutes", "5" }
            };
            HttpContent content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(PaymentURL, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }

        static int tNumber = 100;
        public static async Task PostAsyncWithJson()
        {
            var client = new HttpClient();
            tNumber++;
            var stringPayload = JsonConvert.SerializeObject(new RequestPayload() { TransactionNumber = Convert.ToString(tNumber) });
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(PaymentURL, httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
    }
    
    public class RequestPayload
    {

        private string _TransactionNumber = "1235";
        private int _SequenceNumber = 5;
        private PamentMode _AllowedPaymentMode = PamentMode.Card;
        private int _ClientID = 1013073;
        private int _Amount = 100;
        private string _UserID = "Naren";
        private int _MerchantID = 29895;
        private int _StoreID = 10737583;
        private string _SecurityToken = "08b1b2ff-6d0d-4611-a757-394b6972f080";
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
        public PamentMode AllowedPaymentMode
        {
            get { return _AllowedPaymentMode; }
            set { _AllowedPaymentMode = value; }
        }
        public int ClientID 
        {
            get { return _ClientID; }
            private set { _ClientID = value; }
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
        public int MerchantID
        {
            get { return _MerchantID; }
            private set { _MerchantID = value; }
        }
        public int StoreID
        {
            get { return _StoreID; }
            private set { _StoreID = value; }
        }
        public string SecurityToken
        {
            get { return _SecurityToken; }
            private set { _SecurityToken = value; }
        }
        public int AutoCancelDurationInMinutes
        {
            get { return _AutoCancelDurationInMinutes; }
            private set { _AutoCancelDurationInMinutes = value; }
        }
    }
}
