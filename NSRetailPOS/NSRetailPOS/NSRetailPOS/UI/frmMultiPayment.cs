using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmMultiPayment : DevExpress.XtraEditors.XtraForm
    {
        BillingRepository billingRepository = new BillingRepository();
        public bool IsPaid = false, IsDiscarded = false, canClose = true;
        private Bill billObj;
        decimal paidAmount = 0.00M, payableAmount = 0.00M, remainingAmount = 0.00M, billedAmount = 0.00M;

        int cashRowHandle = -1, b2bCreditRowHandle = -1;

        public frmMultiPayment(Bill bill, bool canClose = true)
        {
            InitializeComponent();
            billObj = bill;
            Text += billObj.BillNumber;
            this.canClose = canClose;
            this.CloseBox = canClose;
            this.btnCancel.Enabled = canClose;
            btnDiscardBill.Enabled = !canClose;
        }

        private void frmMultiPayment_Load(object sender, EventArgs e)
        {
            DataTable dtMOPs = billingRepository.GetMOPs();

            foreach (DataRow drMOP in dtMOPs.Rows)
            {
                rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = drMOP["MOPNAME"].ToString() });
            }

            rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = "Multiple" });
            rgPaymentModes.Properties.Items.ToList().ForEach(x => x.Value = x.Description);

            txtBilledAmount.EditValue = billObj.Amount;

            gcMOP.DataSource = dtMOPs;

            gcMOP.Refresh();

            txtPaymentValue.ValidateOnEnterKey = true;
            txtPaymentValue.Validating += TxtPaymentValue_Validating;

            rgSaleType.EditValue = false;
            rgPaymentModes.EditValue = "CASH";

            decimal.TryParse(billObj.Amount.ToString(), out billedAmount);
            payableAmount = billedAmount;
            remainingAmount = billedAmount;
            cashRowHandle = gvMOP.LocateByValue("MOPNAME", "Cash");
            cashRowHandle = cashRowHandle < 0 ? gvMOP.LocateByValue("MOPNAME", "CASH") : cashRowHandle;
            b2bCreditRowHandle = gvMOP.LocateByValue("MOPNAME", "B2B Credit");

            UpdateLabels();

        }

        private void btnDiscardBill_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to discard bill?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            IsDiscarded = true;
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(rgSaleType.EditValue) && (txtCustomerName.EditValue == null || txtCustomerPhone.EditValue == null))
            {
                XtraMessageBox.Show("Customer Name and number are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerName.EditValue == null ? txtCustomerName : txtCustomerPhone).Focus();
                return;
            }

            if (txtCustomerPhone.EditValue != null && txtCustomerPhone.EditValue.ToString().Length != 10)
            {
                XtraMessageBox.Show("Customer number should be 10 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerPhone.Focus();
                return;
            }

            if (rgPaymentModes.EditValue.Equals("B2B Credit")
                && (txtCustomerName.EditValue == null || txtCustomerPhone.EditValue == null || txtCustomerGST.EditValue == null))
            {
                XtraMessageBox.Show("Customer Name, number & GST are required for B2B billing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerGST.EditValue == null ? txtCustomerGST : null)?.Focus();
                (txtCustomerPhone.EditValue == null ? txtCustomerPhone : null)?.Focus();
                (txtCustomerName.EditValue == null ? txtCustomerName : null)?.Focus();
                return;
            }

            if (Math.Round(remainingAmount) > 0.00M)
            {
                XtraMessageBox.Show($"Bill cannot be closed. Pending balance to be paid {remainingAmount}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (decimal.TryParse(gvMOP.GetRowCellValue(cashRowHandle, "MOPVALUE").ToString(), out decimal cashValue) && cashValue > 0)
            {
                billObj.Rounding = billObj.PaymentMode.Equals("CASH") ? Math.Round(billedAmount) - billedAmount : 0.00M;
                billObj.TenderedCash = cashValue;
                billObj.TenderedChange = remainingAmount + Math.Round(billedAmount) - billedAmount;
                gvMOP.FocusedRowHandle = cashRowHandle;
                gvMOP.SetRowCellValue(cashRowHandle, "MOPVALUE", cashValue + Math.Round(remainingAmount));
                gvMOP.CloseEditor();
                gvMOP.UpdateCurrentRow();
            }

            if (Utility.PaymentGateway != null)
            {
                // request gateway
            }

            billObj.IsDoorDelivery = rgSaleType.EditValue;
            billObj.CustomerName = txtCustomerName.EditValue;
            billObj.CustomerNumber = txtCustomerPhone.EditValue;
            billObj.CustomerGST = txtCustomerGST.EditValue;
            billObj.PaymentMode = rgPaymentModes.EditValue;
            billObj.dtMopValues = gvMOP.DataSource as DataTable;

            IsPaid = true;
            Close();
        }

        private void rgPaymentModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int rowhandle = 0; rowhandle < gvMOP.RowCount; rowhandle++)
            {
                gvMOP.SetRowCellValue(rowhandle, "MOPVALUE", 0);
            }

            if (!rgPaymentModes.Text.Equals("Multiple") && !rgPaymentModes.Text.Equals("CASH"))
            {
                int rowhandle = gvMOP.LocateByValue("MOPNAME", rgPaymentModes.Text);
                gvMOP.SetRowCellValue(rowhandle, "MOPVALUE", billObj.Amount);
            }
        }

        private void rgPaymentModes_Leave(object sender, EventArgs e)
        {
            
        }

        private void gcMOP_Enter(object sender, EventArgs e)
        {
            if (rgPaymentModes.EditValue == null) rgPaymentModes.Focus();

            if (rgPaymentModes.Text.Equals("Multiple") || rgPaymentModes.Text.Equals("CASH"))
            {
                gvMOP.FocusedColumn = gvMOP.Columns[2];
                gvMOP.FocusedRowHandle = cashRowHandle;
                //gvMOP.Focus();
                //gvMOP.ShowEditor();
            }
            else
            {
                btnApply.Focus();
            }
        }

        private void TxtPaymentValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BaseEdit baseEdit = sender as BaseEdit;
            string textValue = baseEdit?.EditValue != null ? baseEdit.EditValue.ToString() : string.Empty;
            if (textValue.Length > 4 && XtraMessageBox.Show($"Large number detected, possible barcode scan detected, Do you want to accept the value {textValue}"
                    , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                baseEdit.EditValue = null;
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
        
        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvMOP.RefreshData();
            decimal.TryParse(gvMOP.Columns["MOPVALUE"].SummaryItem.SummaryValue.ToString(), out paidAmount);
            remainingAmount = payableAmount - paidAmount;
            UpdateLabels();
        }

        private void gvMOP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
        }

        private void gvMOP_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selectedPayment = rgPaymentModes.EditValue;
            if (selectedPayment.Equals("Multiple")
                || (selectedPayment.Equals("CASH") && gvMOP.GetRowCellValue(gvMOP.FocusedRowHandle, "MOPNAME").Equals("CASH")))
            {
                return;
            }
            e.Cancel = true;
        }
    }
}