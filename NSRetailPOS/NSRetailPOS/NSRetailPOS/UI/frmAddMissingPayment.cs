using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NSRetailPOS.Gateway;
using System;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmAddMissingPayment : XtraForm
    {
        public CompletedTransactionData CompletedTransactionData { get; set; }
        public int BillID { get; }
        
        public frmAddMissingPayment(int billID, int cardMopID, int upiMopID)
        {
            InitializeComponent();
            BillID = billID;
            rgPaymentMode.Properties.Items.Add(new RadioGroupItem() { Description = "Card", Value = cardMopID });
            rgPaymentMode.Properties.Items.Add(new RadioGroupItem() { Description = "UPI", Value = upiMopID });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            CompletedTransactionData = Utility.PaymentGateway.ForceReceivePayment(
                BillID
                , int.Parse(rgPaymentMode.EditValue.ToString())
                , txtAmount.EditValue
                , txtRRN.EditValue);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}