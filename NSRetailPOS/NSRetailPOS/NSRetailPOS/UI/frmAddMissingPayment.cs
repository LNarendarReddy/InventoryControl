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

        public frmAddMissingPayment(int billID, int cardMopID, int upiMopID, string referenceId = null, 
            int? selectedMOPid = null, decimal? amount = null)
        {
            InitializeComponent();
            BillID = billID;
            rgPaymentMode.Properties.Items.Add(new RadioGroupItem() { Description = "Card", Value = cardMopID });
            rgPaymentMode.Properties.Items.Add(new RadioGroupItem() { Description = "UPI", Value = upiMopID });
            ReferenceId = referenceId;
            
            if (selectedMOPid.HasValue)
                rgPaymentMode.EditValue = selectedMOPid.Value;

            if (amount.HasValue && amount.Value > 0)
                txtAmount.EditValue = amount.Value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            CompletedTransactionData = Utility.PaymentGateway.ForceReceivePayment(BillID, 
                int.Parse(rgPaymentMode.EditValue.ToString()), txtAmount.EditValue,
                ReferenceId, txtRRN.EditValue, txtTransactionId.EditValue);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}