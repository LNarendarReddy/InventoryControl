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

        int cashRowHandle = -1;

        public frmPayment(Bill bill)
        {
            InitializeComponent();
            billObj = bill;
            this.Text = this.Text + billObj.BillNumber;
            this.gvMOP.Appearance.FocusedCell.BackColor = System.Drawing.Color.SaddleBrown;
            this.gvMOP.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvMOP.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvMOP.Appearance.FocusedCell.Options.UseFont = true;
            this.gvMOP.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvMOP.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvMOP.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvMOP.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMOP.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.FooterPanel.Options.UseFont = true;
            this.gvMOP.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMOP.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.Row.Options.UseFont = true;
        }

        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvMOP.GridControl.BindingContext = new BindingContext();
            gvMOP.GridControl.DataSource = gcMOP.DataSource;

            //if(gvMOP.GetFocusedRowCellValue("MOPNAME").ToString().ToUpper() == "CASH"
            //    && decimal.TryParse(e.Value.ToString(), out decimal cashValue)
            //    && cashValue - Math.Round(cashValue) != 0.0M)
            //{
            //    XtraMessageBox.Show($"Cash cannot have decimal places");
            //    gvMOP.SetRowCellValue(e.RowHandle, "MOPVALUE", 0.00);
            //    return;
            //}

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
            billObj.Rounding = Math.Round(billedAmount) - billedAmount;
            if (decimal.TryParse(gvMOP.GetRowCellValue(cashRowHandle, "MOPVALUE").ToString(), out decimal cashValue))
            {
                gvMOP.SetRowCellValue(cashRowHandle, "MOPVALUE", cashValue + Math.Round(remainingAmount));
            }
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
            cashRowHandle = gvMOP.LocateByValue("MOPNAME", "Cash");
            cashRowHandle = cashRowHandle < 0 ? gvMOP.LocateByValue("MOPNAME", "CASH") : cashRowHandle; 
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            txtPaidAmount.EditValue = paidAmount;
            decimal roundedRemaining = remainingAmount < 0.00m ? 0 : Math.Round(remainingAmount);
            txtRemainingAmount.EditValue = $"{Math.Round(remainingAmount)} ( Rounded value : {roundedRemaining} )";
        }
    }
}
