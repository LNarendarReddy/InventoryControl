using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Data;
using System.Linq;
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

            Utility.SetGridFormatting(gvMOP);
            gvMOP.RowStyle += GvMOP_RowStyle;

            billObj = bill;
            Text += billObj.BillNumber;
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

            if(chkIsDoorDelivery.Checked && (txtCustomerName.EditValue == null || txtMobileNo.EditValue == null))
            {
                XtraMessageBox.Show("Enter customer details to continue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                (txtMobileNo.EditValue == null ? txtMobileNo : txtCustomerName).Focus();
                return;
            }

            billObj.CustomerName = txtCustomerName.EditValue;
            billObj.CustomerNumber = txtMobileNo.EditValue;
            billObj.IsDoorDelivery = chkIsDoorDelivery.EditValue;
            billObj.dtMopValues = gcMOP.DataSource as DataTable;
            billObj.Rounding = Math.Round(billedAmount) - billedAmount;            
            if (decimal.TryParse(gvMOP.GetRowCellValue(cashRowHandle, "MOPVALUE").ToString(), out decimal cashValue) && cashValue > 0)
            {
                billObj.TenderedCash = cashValue;
                billObj.TenderedChange = remainingAmount + Math.Round(billedAmount) - billedAmount;
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

        private void gvMOP_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selectedPayment = rgPaymentOptions.EditValue;
            if (selectedPayment.Equals("Multiple")
                || (selectedPayment.Equals("CASH") && gvMOP.GetRowCellValue(gvMOP.FocusedRowHandle, "MOPNAME").Equals("CASH"))) 
            { 
                return; 
            }

            e.Cancel = true;
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            DataTable dtMOPs = billingRepository.GetMOPs();
            gcMOP.DataSource = dtMOPs;

            gcMOP.Refresh();

            foreach (DataRow drMOP in dtMOPs.Rows)
            {
                rgPaymentOptions.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem() { Description = drMOP["MOPNAME"].ToString() });
            }

            rgPaymentOptions.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem() { Description = "Multiple" });
            rgPaymentOptions.Properties.Items.ToList().ForEach(x => x.Value = x.Description);

            txtCustomerName.EditValue = billObj.CustomerName;
            txtMobileNo.EditValue = billObj.CustomerNumber;
            chkIsDoorDelivery.EditValue = billObj.IsDoorDelivery;
            rgPaymentOptions.EditValue = billObj.PaymentMode;
            txtItemQuantity.EditValue = billObj.Quantity;
            txtBilledAmount.EditValue = billObj.Amount;
            decimal.TryParse(billObj.Amount.ToString(), out billedAmount);
            payableAmount = billedAmount;
            remainingAmount = billedAmount;
            cashRowHandle = gvMOP.LocateByValue("MOPNAME", "Cash");
            cashRowHandle = cashRowHandle < 0 ? gvMOP.LocateByValue("MOPNAME", "CASH") : cashRowHandle; 
            UpdateLabels();

            if (!billObj.PaymentMode.Equals("Multiple") && !billObj.PaymentMode.Equals("CASH"))
            {
                gvMOP.SetRowCellValue(gvMOP.LocateByValue("MOPNAME", billObj.PaymentMode), "MOPVALUE", billObj.Amount);
                btnOk.Focus();
            }
            else
            {
                gvMOP.FocusedColumn = gvMOP.Columns[2];
                gvMOP.FocusedRowHandle = cashRowHandle;
                gvMOP.Focus();
                gvMOP.ShowEditor();
                //gvMOP.RefreshRowCell(cashRowHandle, gvMOP.Columns["MOPVALUE"]);
            }

        }

        private void GvMOP_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.State.HasFlag(DevExpress.XtraGrid.Views.Base.GridRowCellState.FocusedAndGridFocused))
                gvMOP.ShowEditor();
        }

        private void UpdateLabels()
        {
            txtPaidAmount.EditValue = paidAmount;
            decimal roundedRemaining = remainingAmount < 0.00m ? 0 : Math.Round(remainingAmount);
            txtRemainingAmount.EditValue = $"{Math.Round(remainingAmount)} ( Rounded value : {roundedRemaining} )";
        }
    }
}
