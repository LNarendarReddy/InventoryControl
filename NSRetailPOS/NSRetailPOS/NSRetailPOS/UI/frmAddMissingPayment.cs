using DevExpress.Data.Controls.ExpressionEditor;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NSRetailPOS.Gateway;
using System;
using System.Web.Util;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmAddMissingPayment : XtraForm
    {
        public CompletedTransactionData CompletedTransactionData { get; set; }
        public int BillID { get; }
        public string ReferenceId { get; }
        private readonly PaymentGatewayBase gateway;
        private readonly PaymentAttempt attempt;

        public frmAddMissingPayment(PaymentAttempt attempt, PaymentGatewayBase gateway)
        {
            InitializeComponent();
            this.attempt = attempt;
            this.gateway = gateway;
            BillID = attempt.BillID;
            rgPaymentMode.Properties.Items.Add(new RadioGroupItem(attempt.MopID,
                Utility.CounterPayments.Find(attempt.MopID)?.Name ?? "Original Payment Mode"));
            rgPaymentMode.EditValue = attempt.MopID;
            rgPaymentMode.Enabled = false;
            ReferenceId = attempt.ProviderReference;
            txtAmount.EditValue = attempt.Amount;
            txtAmount.Properties.ReadOnly = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            if (!string.Equals(Convert.ToString(Utility.loginInfo.RoleName), "Store Admin", StringComparison.OrdinalIgnoreCase)) return;
            if (XtraMessageBox.Show(this, "Confirm this payment was received according to the terminal or bank record?",
                "Confirm Received Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            CompletedTransactionData = gateway.ForceReceivePayment(BillID,
                int.Parse(rgPaymentMode.EditValue.ToString()), txtAmount.EditValue,
                ReferenceId, txtRRN.EditValue, txtTransactionId.EditValue);
            if (CompletedTransactionData == null) return;
            CompletedTransactionData.AttemptID = attempt.AttemptID;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
