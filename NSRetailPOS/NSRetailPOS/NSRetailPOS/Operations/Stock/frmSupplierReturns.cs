using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Stock
{
    public partial class frmSupplierReturns : DevExpress.XtraEditors.XtraForm
    {
        DataRow drSelectedPrice;
        bool isItemScanned;
        bool isOpenItem;
        bool isEventCall;
        SupplierReturns supplierReturns = null;
        SupplierRepository supplierRepository = new SupplierRepository();
        public frmSupplierReturns()
        {
            InitializeComponent();
            supplierReturns = new SupplierReturns();
        }
        private void frmSupplierReturns_Load(object sender, EventArgs e)
        {
            try
            {
                txtQuantity.ConfirmLargeNumber();
                txtWeightInKgs.ConfirmLargeNumber();

                cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
                cmbSupplier.Properties.ValueMember = "DEALERID";
                cmbSupplier.Properties.DisplayMember = "DEALERNAME";

                DataView dvCategory = new MasterRepository().GetCategory().Copy().DefaultView;
                dvCategory.RowFilter = "CATEGORYNAME <> 'ALL'";
                cmbCategory.Properties.DataSource = dvCategory;
                cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
                cmbCategory.Properties.ValueMember = "CATEGORYID";

                cmbReasongrid.DataSource = new SupplierRepository().GetReason();
                cmbReasongrid.ValueMember = "REASONID";
                cmbReasongrid.DisplayMember = "REASONNAME";

                cmbReason.Properties.DataSource = new SupplierRepository().GetReason();
                cmbReason.Properties.ValueMember = "REASONID";
                cmbReason.Properties.DisplayMember = "REASONNAME";
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex);
            }
        }
        private void FrmStockDispatch_RefreshBaseLineData(object sender, EventArgs e)
        {
            object selectedValue = sluItemCode.EditValue;
            sluItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered().Copy();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMCODE";
            sluItemCode.EditValue = selectedValue;
        }
        private void cmbSupplier_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                InitialLoad();
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex);
            }
        }
        private void InitialLoad()
        {
            try
            {
                if (cmbSupplier.EditValue == null || cmbCategory.EditValue == null)
                    return;
                supplierReturns.SupplierID = cmbSupplier.EditValue;
                supplierReturns.CategoryID = cmbCategory.EditValue;
                supplierRepository.GetInitialLoad(supplierReturns);
                gcSupplierReturns.DataSource = supplierReturns.dtSupplierReturns;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
;        }
        private void ClearItemData(bool focusItemCode = true)
        {
            txtItemCode.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtCostPrice.EditValue = null;
            txtQuantity.EditValue = 1;
            txtWeightInKgs.EditValue = 0.00;

            if (focusItemCode)
                txtItemCode.Focus();
        }
        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODE", txtItemCode.EditValue);

            if (rowHandle < 0)
            {
                List<int> rowHandles = sluItemCodeView.LocateAllRowsByValue("SKUCODE", txtItemCode.EditValue);
                if (rowHandles.Count == 1)
                {
                    rowHandle = rowHandles.First();
                }
                else if (rowHandles.Count > 1)
                {
                    sluItemCode.ShowPopup();
                    sluItemCodeView.FindFilterText = txtItemCode.EditValue.ToString();
                    isItemScanned = true;
                }
            }

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
        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null) { return; }
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
            txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODE");
            isOpenItem = Convert.ToBoolean(sluItemCodeView.GetRowCellValue(rowHandle, "ISOPENITEM"));

            DataTable dtCPList = new ItemCodeRepository().GetCostPriceList(sluItemCode.EditValue);
            if (dtCPList.Rows.Count > 1)
            {
                frmCostPriceList obj = new frmCostPriceList(dtCPList);
                obj.ShowInTaskbar = false;
                obj.IconOptions.ShowIcon = false;
                obj.StartPosition = FormStartPosition.CenterScreen;
                obj.ShowDialog();
                if (obj._IsSave)
                {
                    drSelectedPrice = obj.drSelected;
                }
            }
            else if (dtCPList.Rows.Count > 0)
                drSelectedPrice = dtCPList.Rows[0];
            if (drSelectedPrice == null)
            {
                return;
            }
            txtCostPrice.EditValue = drSelectedPrice["COSTPRICEWT"];
            txtMRP.EditValue = drSelectedPrice["MRP"];
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
        private void txtItemCode_Click(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }
        private void txtItemCode_Enter(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)Keys.Enter)
                    return;

                if (sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null
                    || txtQuantity.EditValue.Equals(0))
                {
                    txtItemCode.Focus();
                    return;
                }

                if (!dxValidationProvider1.Validate())
                    return;

                if (supplierReturns.SupplierReturnsID.Equals(0))
                    SaveSupplierReturns();

                gvSupplierReturns.GridControl.BindingContext = new BindingContext();
                gvSupplierReturns.GridControl.DataSource = supplierReturns.dtSupplierReturns;
                isEventCall = true;

                int rowHandle = Getrowhandle();
                if (rowHandle < 0)
                {
                    gvSupplierReturns.AddNewRow();
                }
                else
                {

                    if (isOpenItem)
                    {
                        decimal weightinKGS = Convert.ToDecimal(txtWeightInKgs.EditValue) +
                            Convert.ToDecimal(gvSupplierReturns.GetRowCellValue(rowHandle, "WEIGHTINKGS"));
                        if (weightinKGS > 0)
                            gvSupplierReturns.SetRowCellValue(rowHandle, "WEIGHTINKGS", weightinKGS);
                    }
                    else
                    {
                        int newQuantity = Convert.ToInt32(txtQuantity.EditValue) +
                        Convert.ToInt32(gvSupplierReturns.GetRowCellValue(rowHandle, "QUANTITY"));
                        if (newQuantity > 0)
                            gvSupplierReturns.SetRowCellValue(rowHandle, "QUANTITY", newQuantity);
                    }
                }
                isEventCall = false;
                gvSupplierReturns.GridControl.BindingContext = new BindingContext();
                gvSupplierReturns.GridControl.DataSource = supplierReturns.dtSupplierReturns;

                rowHandle = Getrowhandle();
                if (rowHandle >= 0)
                {
                    SaveSupplierReturnsDetail(rowHandle);
                }
                ClearItemData();
                gvSupplierReturns.FocusedRowHandle = rowHandle;
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex);
            }
        }
        private int Getrowhandle()
        {
            int rowHandle = -1;
            IList source = (IList)ListBindingHelper.GetList(gcSupplierReturns.DataSource, gcSupplierReturns.DataMember);
            PropertyDescriptorCollection coll = ListBindingHelper.GetListItemProperties(gcSupplierReturns.DataSource);
            PropertyDescriptor desc1 = coll["ITEMCOSTPRICEID"];
            PropertyDescriptor desc2 = coll["REASONID"];
            foreach (object row in source)
            {
                object val1 = desc1.GetValue(row);
                object val2 = desc2.GetValue(row);
                if (val1.Equals(drSelectedPrice["ITEMCOSTPRICEID"]) && val2.Equals(cmbReason.EditValue))
                {
                    rowHandle = gvSupplierReturns.GetRowHandle(source.IndexOf(row));
                    break;
                }
            }
            return rowHandle;
        }
        private void SaveSupplierReturns()
        {
            try
            {
                supplierReturns.SupplierID = cmbSupplier.EditValue;
                supplierReturns.CategoryID = cmbCategory.EditValue;
                supplierReturns.UserID = Utility.loginInfo.UserID;
                supplierRepository.SaveSupplierReturns(supplierReturns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveSupplierReturnsDetail(int rowHandle)
        {
            DataRow drDetail = (gvSupplierReturns.GetRow(rowHandle) as DataRowView).Row;
            object SupplierReturnsDetailID = 
                supplierRepository.SaveSupplierReturnsDetail(drDetail,Utility.loginInfo.UserID, Utility.branchInfo.BranchID);
            drDetail["SUPPLIERRETURNSDETAILID"] = SupplierReturnsDetailID;
        }
        private void gvSupplierReturns_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "SNO", gvSupplierReturns.RowCount + 1);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "SUPPLIERRETURNSDETAILID", -1);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "SUPPLIERRETURNSID", supplierReturns.SupplierReturnsID);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "ITEMCOSTPRICEID", drSelectedPrice["ITEMCOSTPRICEID"]);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "ITEMCODE", txtItemCode.EditValue);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "ITEMNAME", sluItemCode.Text);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "MRP", drSelectedPrice["MRP"]);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "SALEPRICE", drSelectedPrice["SALEPRICE"]);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "COSTPRICE", drSelectedPrice["COSTPRICEWT"]);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
            gvSupplierReturns.SetRowCellValue(e.RowHandle, "REASONID", cmbReason.EditValue);
        }
        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            TextEdit textedit = sender as TextEdit;
            textedit.SelectAll();
        }
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvSupplierReturns.FocusedRowHandle < 0 ||
                XtraMessageBox.Show("Are you sure want to delete item", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            supplierRepository.DeleteSupplierReturnsDetail(
                gvSupplierReturns.GetFocusedRowCellValue("SUPPLIERRETURNSDETAILID"), Utility.loginInfo.UserID);
            gvSupplierReturns.DeleteRow(gvSupplierReturns.FocusedRowHandle);
        }
        private void frmSupplierReturns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                sluItemCode.Focus();
            }
        }
        private void gvSupplierReturns_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY" || isEventCall) return;
            SaveSupplierReturnsDetail(e.RowHandle);
        }

        private void btnFreeze_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSupplierReturns.RowCount == 0)
                    return;
                supplierRepository.FreezeSupplierReturns(supplierReturns.SupplierReturnsID, 
                    Utility.loginInfo.UserID);
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            InitialLoad();

            sluItemCode.Properties.DataSource = 
                new ItemCodeRepository().GetItemCodeByCategory(cmbCategory.EditValue);
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;
        }
    }
}