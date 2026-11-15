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
        private readonly bool allowAmountEdit;
        private readonly decimal maxAmount;

        public frmAddMissingPayment(PaymentAttempt attempt, PaymentGatewayBase gateway,
            bool allowAmountEdit = false, decimal maxAmount = decimal.MaxValue)
        {
            InitializeComponent();
            this.attempt = attempt;
            this.gateway = gateway;
            this.allowAmountEdit = allowAmountEdit;
            this.maxAmount = maxAmount;
            BillID = attempt.BillID;
            rgPaymentMode.Properties.Items.Add(new RadioGroupItem(attempt.MopID,
                Utility.CounterPayments.Find(attempt.MopID)?.Name ?? "Original Payment Mode"));
            rgPaymentMode.EditValue = attempt.MopID;
            rgPaymentMode.Enabled = false;
            ReferenceId = string.IsNullOrWhiteSpace(attempt.ProviderReference) ? attempt.RequestReference : attempt.ProviderReference;
            txtAmount.EditValue = attempt.Amount;
            txtAmount.Properties.ReadOnly = !allowAmountEdit;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            decimal amount = Convert.ToDecimal(txtAmount.EditValue);
            if (amount <= 0 || amount > maxAmount || decimal.Round(amount, 2) != amount
                || (!allowAmountEdit && amount != attempt.Amount))
            { Utility.ShowErrorMessage("Enter a valid received amount within the remaining bill balance."); return; }
            string rrn = Convert.ToString(txtRRN.EditValue).Trim();
            string transactionId = Convert.ToString(txtTransactionId.EditValue).Trim();
            if (string.IsNullOrWhiteSpace(rrn) || string.IsNullOrWhiteSpace(transactionId) || transactionId.Length > 100)
            { Utility.ShowErrorMessage("Enter the bank RRN and transaction ID (maximum 100 characters)."); return; }
            string warning = "Confirm this payment was received according to the terminal or bank record?"
                + Environment.NewLine + "This manually records receipt; it does not verify payment with the gateway.";
            if (attempt.RefundRequested) warning += Environment.NewLine + "A refund was requested for this order and may still complete. Confirm the funds were retained before linking.";
            if (XtraMessageBox.Show(this, warning,
                "Confirm Received Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            CompletedTransactionData = gateway.ForceReceivePayment(BillID,
                attempt.MopID, amount,
                attempt.ManualEntryWithoutRequest ? transactionId : ReferenceId, rrn, transactionId);
            if (CompletedTransactionData == null) return;
            attempt.Amount = amount;
            attempt.IsManualLink = true;
            attempt.ManualLinkedBy = Convert.ToInt32(Utility.loginInfo.UserID);
            attempt.ManualLinkedAtUtc = DateTime.UtcNow;
            attempt.ManualRRN = rrn;
            attempt.ManualTransactionID = transactionId;
            if (attempt.ManualEntryWithoutRequest) attempt.ProviderReference = transactionId;
            CompletedTransactionData.AttemptID = attempt.AttemptID;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
