﻿using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Reports;
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
    public partial class frmCustomerRefund : DevExpress.XtraEditors.XtraForm
    {
        DataRow drSelectedPrice;
        bool isItemScanned;
        bool isOpenItem;
        bool isEventCall;
        DataTable dtRefund = null;
        public frmCustomerRefund()
        {
            InitializeComponent();
        }

        private void frmCustomerRefund_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = new ItemRepository().GetItemCodes();
            sluItemCode.Properties.DisplayMember = "ITEMNAME";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;

            BindDataTable();
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null) { return; }
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
            txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODE");
            isOpenItem = Convert.ToBoolean(sluItemCodeView.GetRowCellValue(rowHandle, "ISOPENITEM"));
            DataTable dtPrices = new ItemRepository().GetMRPList(sluItemCode.EditValue);
            if (dtPrices.Rows.Count > 1)
            {
                frmMRPSelection mRPSelection = new frmMRPSelection(dtPrices, txtItemCode.EditValue, sluItemCode.Text)
                { StartPosition = FormStartPosition.CenterScreen };
                mRPSelection.ShowDialog();
                if (!mRPSelection._IsSave)
                {
                    ClearItemData();
                    return;
                }

                drSelectedPrice = (mRPSelection.drSelected as DataRowView)?.Row;
            }
            else if (dtPrices.Rows.Count == 1)
            {
                drSelectedPrice = dtPrices.Rows[0];
            }

            if (drSelectedPrice == null)
            {
                return;
            }

            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];
            txtQuantity.EditValue = 1;
            if (isOpenItem)
            {
                txtWeightInKgs.Enabled = true;
                txtQuantity.EditValue = 1;
                txtQuantity.Enabled = false;
            }
            else
            {
                txtWeightInKgs.EditValue = "0.00";
                txtWeightInKgs.Enabled = false;
                txtQuantity.Enabled = true;
            }
            if (!isItemScanned)
            {
                if (isOpenItem)
                    txtWeightInKgs.Focus();
                else
                    txtQuantity.Focus();
            }
        }

        private void BindDataTable()
        {
            dtRefund = new DataTable();
            dtRefund.Columns.Add("ITEMPRICEID", typeof(int));
            dtRefund.Columns.Add("ITEMCODE", typeof(string));
            dtRefund.Columns.Add("ITEMNAME", typeof(string));
            dtRefund.Columns.Add("MRP", typeof(decimal));
            dtRefund.Columns.Add("SALEPRICE", typeof(decimal));
            dtRefund.Columns.Add("QUANTITY", typeof(int));
            dtRefund.Columns.Add("WEIGHTINKGS", typeof(decimal));
            dtRefund.Columns.Add("AMOUNT", typeof(decimal));
            gcRefund.DataSource = dtRefund;
        }

        private void ClearItemData(bool focusItemCode = true)
        {
            txtItemCode.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = 1;
            txtWeightInKgs.EditValue = 0.00;

            if (focusItemCode)
                txtItemCode.Focus();
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODE", txtItemCode.EditValue);
            if (rowHandle >= 0)
            {
                isItemScanned = true;
                sluItemCode.EditValue = null;
                sluItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODEID");
            }
            else
            {
                ClearItemData(false);
            }

            isItemScanned = false;
        }

        private void txtItemCode_Enter(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }

        private void txtItemCode_Click(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null
                || txtQuantity.EditValue.Equals(0))
            {
                txtItemCode.Focus();
                return;
            }

            if (!dxValidationProvider1.Validate())
                return;

            gvRefund.GridControl.BindingContext = new BindingContext();
            gvRefund.GridControl.DataSource = dtRefund;
            isEventCall = true;
            int rowHandle = gvRefund.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle < 0)
            {
                gvRefund.AddNewRow();
            }
            else
            {
                if (isOpenItem)
                {
                    decimal weightinKGS = Convert.ToDecimal(txtWeightInKgs.EditValue) +
                        Convert.ToDecimal(gvRefund.GetRowCellValue(rowHandle, "WEIGHTINKGS"));
                    if (weightinKGS > 0)
                        gvRefund.SetRowCellValue(rowHandle, "WEIGHTINKGS", weightinKGS);

                }
                else
                {
                    int newQuantity = Convert.ToInt32(txtQuantity.EditValue) +
                        Convert.ToInt32(gvRefund.GetRowCellValue(rowHandle, "QUANTITY"));
                    if (newQuantity > 0)
                        gvRefund.SetRowCellValue(rowHandle, "QUANTITY", newQuantity);
                }
            }
            isEventCall = false;
            gvRefund.GridControl.BindingContext = new BindingContext();
            gvRefund.GridControl.DataSource = dtRefund;

            rowHandle = gvRefund.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            ClearItemData();
            gvRefund.FocusedRowHandle = rowHandle;
        }

        private void gvRefund_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvRefund.SetRowCellValue(e.RowHandle, "ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            gvRefund.SetRowCellValue(e.RowHandle, "ITEMCODE", txtItemCode.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "ITEMNAME", sluItemCode.Text);
            gvRefund.SetRowCellValue(e.RowHandle, "MRP", txtMRP.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "SALEPRICE", txtSalePrice.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
            if(decimal.TryParse(txtSalePrice.Text,out decimal saleprice))
            {
                if (isOpenItem)
                {
                    if (decimal.TryParse(txtWeightInKgs.Text, out decimal weightinkgs))
                        gvRefund.SetRowCellValue(e.RowHandle, 
                            "AMOUNT", Math.Round(saleprice * weightinkgs,2));
                }
                else if(int.TryParse(txtQuantity.Text, out int quantity))
                {
                    gvRefund.SetRowCellValue(e.RowHandle, 
                        "AMOUNT", Math.Round(saleprice * quantity,2));
                }
            }
        }

        private void frmCustomerRefund_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                sluItemCode.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            TextEdit textedit = sender as TextEdit;
            textedit.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            new RefundRepository().InsertCRefundWOBill(dtRefund, Utility.logininfo.UserID);
            BindDataTable();
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gvRefund.DeleteRow(gvRefund.FocusedRowHandle);
        }
    }
}