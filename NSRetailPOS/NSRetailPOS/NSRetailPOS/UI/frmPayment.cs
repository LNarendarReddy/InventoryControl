using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmPayment : XtraForm
    {
        public bool IsPaid = false;

        private Bill billObj;

        BillingRepository billingRepository = new BillingRepository();

        decimal paidAmount = 0.00M, payableAmount = 0.00M, remainingAmount = 0.00M;

        public frmPayment(Bill bill)
        {
            InitializeComponent();
            billObj = bill;
        }

        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvMOP.GridControl.BindingContext = new BindingContext();
            gvMOP.GridControl.DataSource = gcMOP.DataSource;

            decimal.TryParse(gvMOP.Columns["MOPVALUE"].SummaryItem.SummaryValue.ToString(), out paidAmount);
            remainingAmount = payableAmount - paidAmount;
            UpdateLabels();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (remainingAmount > 0.00M)
            {
                XtraMessageBox.Show($"Bill cannot be closed. Pending balance to be paid {remainingAmount}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            billObj.CustomerName = txtCustomerName.EditValue;
            billObj.CustomerNumber = txtMobileNo.EditValue;
            billObj.dtMopValues = gcMOP.DataSource as DataTable;
            IsPaid = true;
            Close();
        }

        private void frmPayment_Load(object sender, System.EventArgs e)
        {
            DataTable dtMOPs = billingRepository.GetMOPs();
            gcMOP.DataSource = dtMOPs;

            txtBillNumber.EditValue = billObj.BillNumber;
            txtItemQuantity.EditValue = billObj.Quantity;
            txtBilledAmount.EditValue = billObj.Amount;
            decimal.TryParse(billObj.Amount.ToString(), out payableAmount);
            remainingAmount = payableAmount;
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            txtPaidAmount.EditValue = paidAmount;
            txtRemainingAmount.EditValue = remainingAmount;
        }
    }
}
