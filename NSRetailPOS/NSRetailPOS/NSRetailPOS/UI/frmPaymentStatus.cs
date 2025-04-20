using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmPaymentStatus : DevExpress.XtraEditors.XtraForm
    {
        BillingUploadResponse billingUploadResponse = null;
        double amount = 0;
        public bool isSuccess = false;
        public frmPaymentStatus(BillingUploadResponse _billingUploadResponse, double _amount, object transacitonType)
        {
            InitializeComponent();
            this.Text = transacitonType.Equals("CARD") ? $"Card transaction status: {_amount.ToString("n2")}" :
                $"UPI transaction status: {_amount.ToString("n2")}";
            billingUploadResponse = _billingUploadResponse;
            amount = _amount;
        }

        private async void btnPaymentStatus_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Checking transaction status...");
                result = await GetPaymentStatus();
            }
            catch (Exception) { }
            finally { SplashScreenManager.CloseForm(); }
            if (result)
            {
                XtraMessageBox.Show("The transaction has been approved and processed.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isSuccess = true;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("The transaction is still in progress, please try again.",
                    "In-Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Canceling transaction...");
                result = await CancelPayment();
            }
            catch (Exception){}
            finally { SplashScreenManager.CloseForm(); }
            if (result)
            {
                XtraMessageBox.Show("The transaction has been canceled.",
                    "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("The transaction is still in progress,please try again.",
                    "In-Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task<bool> GetPaymentStatus()
        {
            try
            {
                StatusPayload statusPayload = new StatusPayload();
                statusPayload.PlutusTransactionReferenceID = billingUploadResponse.PlutusTransactionReferenceID;
                string statusresult = await PaymentGatewayHelper.PostAsyncWithJson(statusPayload,
                    GatewayInfo.PaymentUrl + GatewayInfo.StatusAPI);
                BillingUploadResponse paymentStatusResponse = JsonConvert.DeserializeObject<BillingUploadResponse>(statusresult);
                return paymentStatusResponse.ResponseMessage.Contains("TXN APPROVED");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> CancelPayment()
        {
            try
            {
                CancelPayload cancelPayload = new CancelPayload();
                cancelPayload.PlutusTransactionReferenceID = billingUploadResponse.PlutusTransactionReferenceID;
                cancelPayload.Amount = Convert.ToInt32(100 * amount);
                string statusresult = await PaymentGatewayHelper.PostAsyncWithJson(cancelPayload,
                    GatewayInfo.PaymentUrl + GatewayInfo.CancelAPI);
                BillingUploadResponse CancelResponse = JsonConvert.DeserializeObject<BillingUploadResponse>(statusresult);
                return CancelResponse.ResponseMessage.Contains("APPROVED");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmPaymentStatus_Load(object sender, EventArgs e)
        {

        }

        private void frmPaymentStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                btnCancel_Click(null, null);
            }
        }
    }
}