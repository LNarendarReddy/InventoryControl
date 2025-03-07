﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
        public bool IsPaid = false, IsDiscarded = false;

        private Bill billObj;

        BillingRepository billingRepository = new BillingRepository();

        decimal paidAmount = 0.00M, payableAmount = 0.00M, remainingAmount = 0.00M, billedAmount = 0.00M;

        int cashRowHandle = -1, b2bCreditRowHandle = -1;

        public frmPayment(Bill bill, bool canClose = true)
        {
            InitializeComponent();

            Utility.SetGridFormatting(gvMOP);
            gvMOP.RowStyle += GvMOP_RowStyle;

            billObj = bill;
            Text += billObj.BillNumber;

            btnCancel.Enabled = canClose;
            this.CloseBox = canClose;
            btnDiscardBill.Enabled = !canClose;
        }

        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {   
            gvMOP.RefreshData();
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
            billObj.CustomerGST = txtCustomerGST.EditValue;
            billObj.IsDoorDelivery = chkIsDoorDelivery.EditValue;

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

            if(decimal.TryParse(gvMOP.GetRowCellValue(b2bCreditRowHandle, "MOPVALUE").ToString(), out decimal b2bCreditValue) && b2bCreditValue > 0
                && (txtCustomerName.EditValue == null || txtMobileNo.EditValue == null || txtCustomerGST.EditValue == null))
            {
                XtraMessageBox.Show("Customer Name, number & GST are required for B2B billing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerGST.EditValue == null ? txtCustomerGST : null)?.Focus();
                (txtMobileNo.EditValue == null ? txtMobileNo : null)?.Focus();
                (txtCustomerName.EditValue == null ? txtCustomerName : null)?.Focus();
                return;
            }

            billObj.dtMopValues = gcMOP.DataSource as DataTable;
            
            IsPaid = true;
            Close();
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
            var selectedPayment = rgPaymentOptions.EditValue;
            if (selectedPayment.Equals("Multiple")
                || (selectedPayment.Equals("CASH") && gvMOP.GetRowCellValue(gvMOP.FocusedRowHandle, "MOPNAME").Equals("CASH"))) 
            { 
                return; 
            }
            e.Cancel = true;
        }

        private void btnDiscardBill_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to discard bill?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            IsDiscarded = true;
            this.Close();
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            DataTable dtMOPs = billingRepository.GetMOPs();
            gcMOP.DataSource = dtMOPs;

            gcMOP.Refresh();

            txtPaymentValue.ValidateOnEnterKey = true;
            txtPaymentValue.Validating += TxtPaymentValue_Validating;

            foreach (DataRow drMOP in dtMOPs.Rows)
            {
                rgPaymentOptions.Properties.Items.Add(new RadioGroupItem() { Description = drMOP["MOPNAME"].ToString() });
            }

            rgPaymentOptions.Properties.Items.Add(new RadioGroupItem() { Description = "Multiple" });
            rgPaymentOptions.Properties.Items.ToList().ForEach(x => x.Value = x.Description);

            txtCustomerName.EditValue = billObj.CustomerName;
            txtMobileNo.EditValue = billObj.CustomerNumber;
            txtCustomerGST.EditValue = billObj.CustomerGST;
            chkIsDoorDelivery.EditValue = billObj.IsDoorDelivery;
            rgPaymentOptions.EditValue = billObj.PaymentMode;
            txtItemQuantity.EditValue = billObj.Quantity;
            txtBilledAmount.EditValue = billObj.Amount;
            decimal.TryParse(billObj.Amount.ToString(), out billedAmount);
            payableAmount = billedAmount;
            remainingAmount = billedAmount;
            cashRowHandle = gvMOP.LocateByValue("MOPNAME", "Cash");
            cashRowHandle = cashRowHandle < 0 ? gvMOP.LocateByValue("MOPNAME", "CASH") : cashRowHandle;
            b2bCreditRowHandle = gvMOP.LocateByValue("MOPNAME", "B2B Credit");

            UpdateLabels();

            if (!billObj.PaymentMode.Equals("Multiple") && !billObj.PaymentMode.Equals("CASH"))
            {
                int rowhandle = gvMOP.LocateByValue("MOPNAME", billObj.PaymentMode);
                gvMOP.SetRowCellValue(rowhandle, "MOPVALUE", billObj.Amount);
                gvMOP.FocusedRowHandle = rowhandle;
                btnOk.Focus();
            }
            else
            {
                gvMOP.FocusedColumn = gvMOP.Columns[2];
                gvMOP.FocusedRowHandle = cashRowHandle;
                gvMOP.Focus();
                gvMOP.ShowEditor();
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
    }
}
