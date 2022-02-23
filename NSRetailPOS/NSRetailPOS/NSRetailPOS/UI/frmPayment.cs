using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmPayment : XtraForm
    {
        public bool IsPaid = false;

        private Bill billObj;

        BillingRepository billingRepository = new BillingRepository();

        decimal paidAmount = 0.00M, payableAmount = 0.00M, remainingAmount = 0.00M, billedAmount = 0.00M;

        public frmPayment(Bill bill)
        {
            InitializeComponent();
            billObj = bill;
            this.Text = this.Text + billObj.BillNumber;
        }

        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvMOP.GridControl.BindingContext = new BindingContext();
            gvMOP.GridControl.DataSource = gcMOP.DataSource;

            if(gvMOP.GetFocusedRowCellValue("MOPNAME").ToString().ToUpper() == "CASH"
                && decimal.TryParse(e.Value.ToString(), out decimal cashValue)
                && cashValue - Math.Round(cashValue) != 0.0M)
            {
                XtraMessageBox.Show($"Cash cannot have decimal places");
                gvMOP.SetRowCellValue(e.RowHandle, "MOPVALUE", 0.00);
                return;
            }

            decimal.TryParse(gvMOP.Columns["MOPVALUE"].SummaryItem.SummaryValue.ToString(), out paidAmount);
            remainingAmount = payableAmount - paidAmount;
            UpdateLabels();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Math.Round(remainingAmount) > 0.00M)
            {
                XtraMessageBox.Show($"Bill cannot be closed. Pending balance to be paid {remainingAmount}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            billObj.CustomerName = txtCustomerName.EditValue;
            billObj.CustomerNumber = txtMobileNo.EditValue;
            billObj.dtMopValues = gcMOP.DataSource as DataTable;
            billObj.Rounding = paidAmount - billedAmount;
            IsPaid = true;
            Close();
        }

        private void gvMOP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gvMOP.MoveNext();
            }
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gvMOP.FocusedColumn = gvMOP.Columns[2];
                gvMOP.FocusedRowHandle = 0;
                gvMOP.Focus();
                gvMOP.ShowEditor();
            }
        }

        private void frmPayment_Load(object sender, System.EventArgs e)
        {
            DataTable dtMOPs = billingRepository.GetMOPs();
            gcMOP.DataSource = dtMOPs;
            txtItemQuantity.EditValue = billObj.Quantity;
            txtBilledAmount.EditValue = billObj.Amount;
            decimal.TryParse(billObj.Amount.ToString(), out billedAmount);
            payableAmount = billedAmount;
            remainingAmount = billedAmount;
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            txtPaidAmount.EditValue = paidAmount;
            txtRemainingAmount.EditValue = $"{remainingAmount} ( Rounded value : {Math.Round(remainingAmount)} )";
        }
    }
}
