using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS
{
    public enum PaymentMode
    {
        Card = 1,
        UPI = 10
    }
    public class PaymentGatewayHelper
    {
        public static async Task TestPaymentAsync(PaymentMode paymentMode,string paymentUrl,int sequenceNumber = 1, int amountInSmallestUnit = 1)
        {
            var payload = new RequestPayload
            {
                TransactionNumber = GenerateTransactionNumber(),
                SequenceNumber = sequenceNumber,
                AllowedPaymentMode = paymentMode,
                Amount = amountInSmallestUnit * 100
            };

            string result = string.Empty;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Uploading transaction...");
                result = await PostAsyncWithJson(payload, $"{paymentUrl}{GatewayInfo.BillingAPI}");
            }
            catch (Exception ex){ throw ex; }
            finally { SplashScreenManager.CloseForm(); }
            var billingResponse = JsonConvert.DeserializeObject<BillingUploadResponse>(result);
            if (billingResponse.ResponseMessage.Contains("INVALID STORE"))
            {
                XtraMessageBox.Show("Store ID is not recognized. Please contact your administrator.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmPaymentStatus frm = new frmPaymentStatus(billingResponse, amountInSmallestUnit, "CARD");
            frm.ShowDialog();
        }

        private static string GenerateTransactionNumber()
        {
            return Guid.NewGuid().ToString("N").Substring(0,10).ToUpper();
        }

        public static async Task<string> PostAsyncWithJson(BasePaylod requestPayload, string PaymentURL)
        {
            var client = new HttpClient();
            var stringPayload = JsonConvert.SerializeObject(requestPayload);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(PaymentURL, httpContent);
            return await response.Content.ReadAsStringAsync();
        }
    }


    public static class GatewayInfo
    {
        public static string ClientID { get; set; }
        public static string MerchantID { get; set; }
        public static string StoreID { get; set; }
        public static string SecurityToken { get; set; }
        public static int AutoCancelDurationInMinutes { get; set; }
        public static string PaymentUrl { get; set; }

        public static string BillingAPI { get { return "UploadBilledTransaction"; } private set; }
        public static string StatusAPI { get { return "GetCloudBasedTxnStatus"; } private set; }
        public static string CancelAPI { get { return "CancelTransaction"; } private set; }
    }
}
