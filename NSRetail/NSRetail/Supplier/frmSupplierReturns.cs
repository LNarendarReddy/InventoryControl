using DataAccess;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Entity;
using NSRetail.Reports;
using NSRetail.Supplier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Stock
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
                txtQuantity.ConfirmBarCodeScan();
                txtWeightInKgs.ConfirmBarCodeScan();

                ((frmMain)MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
                cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
                cmbSupplier.Properties.ValueMember = "DEALERID";
                cmbSupplier.Properties.DisplayMember = "DEALERNAME";

                cmbFromBranch.Properties.DataSource = Utility.GetBranchList();
                cmbFromBranch.Properties.ValueMember = "BRANCHID";
                cmbFromBranch.Properties.DisplayMember = "BRANCHNAME";

                cmbCategory.Properties.DataSource = Utility.GetCategoryList();
                cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
                cmbCategory.Properties.ValueMember = "CATEGORYID";

                cmbCategory.EditValue = Utility.CategoryID;
                cmbCategory.Enabled = Utility.CategoryID.Equals(13);
                rgPrintType.SelectedIndex = 0;
            } 
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
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
                Loadinvoicenumbers();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void cmbFromBranch_EditValueChanged(object sender, EventArgs e)
        {
            InitialLoad();
        }

        private void cmbStockEntry_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = new ItemCodeRepository().GetItemCodeByCategory(cmbCategory.EditValue);
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;
            InitialLoad();
        }

        private void Loadinvoicenumbers()
        {
            //cmbStockEntry.Properties.DataSource = new SupplierRepository().GetInvoiceNumbers(cmbSupplier.EditValue);
            //cmbStockEntry.Properties.ValueMember = "STOCKENTRYID";
            //cmbStockEntry.Properties.DisplayMember = "SUPPLIERINVOICENO";
        }

        private void InitialLoad()
        {
            try
            {
                if (cmbSupplier.EditValue == null || 
                    cmbCategory.EditValue == null || 
                    cmbFromBranch.EditValue == null)
                    return;
                supplierReturns.SupplierID = cmbSupplier.EditValue;
                supplierReturns.CategoryID = cmbCategory.EditValue;
                supplierReturns.StockEntryID = cmbStockEntry.EditValue;
                supplierReturns.BranchID = cmbFromBranch.EditValue;
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
            if(string.IsNullOrEmpty(Convert.ToString(txtItemCode.EditValue)))
                return;
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
                XtraMessageBox.Show("Item does not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.KeyChar != (char)Keys.Enter)
                return;

            if (sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null)
            {
                txtItemCode.Focus();
                return;
            }

            if (Convert.ToDecimal(txtQuantity.EditValue) <= 0)
            {
                txtItemCode.Focus();
                return;
            }

            if (!dxValidationProvider1.Validate())
                return;

            SupplierRowModel model = new SupplierRowModel
            {
                ItemCostPriceID = Convert.ToInt32(drSelectedPrice["ITEMCOSTPRICEID"]),
                ReasonID = Convert.ToInt32(cmbReasongrid),
                ItemCode = txtItemCode.Text,
                ItemName = sluItemCode.Text,
                MRP = Convert.ToDecimal(drSelectedPrice["MRP"]),
                CostPrice = Convert.ToDecimal(drSelectedPrice["COSTPRICEWT"]),
                Quantity = isOpenItem
                            ? Convert.ToDecimal(txtWeightInKgs.EditValue)
                            : Convert.ToDecimal(txtQuantity.EditValue),
            };

            InsertOrUpdateRowFromPopup(model);

            ClearItemData();
        }
        
        private void SaveSupplierReturns()
        {
            try
            {
                supplierReturns.SupplierID = cmbSupplier.EditValue;
                supplierReturns.CategoryID = cmbCategory.EditValue;
                supplierReturns.StockEntryID = cmbStockEntry.EditValue;
                supplierReturns.BranchID = cmbFromBranch.EditValue;
                supplierReturns.UserID = Utility.UserID;
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
            object SupplierReturnsDetailID = supplierRepository.SaveSupplierReturnsDetail(drDetail,Utility.UserID);
            drDetail["SUPPLIERRETURNSDETAILID"] = SupplierReturnsDetailID;
        }

        private void UpdateTotalPrice(int rowHandle)
        {
            var rowView = gvSupplierReturns.GetRow(rowHandle) as DataRowView;
            if (rowView == null) return;

            decimal quantity = Convert.ToDecimal(rowView["QUANTITY"] ?? 0);
            decimal costPrice = Convert.ToDecimal(rowView["COSTPRICE"] ?? 0);

            decimal totalPrice = quantity * costPrice;

            rowView["TOTALCOSTPRICE"] = totalPrice;
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
                gvSupplierReturns.GetFocusedRowCellValue("SUPPLIERRETURNSDETAILID"), Utility.UserID);
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
            if (isEventCall)
                return;

            if (e.Column.FieldName == "QUANTITY")
            {
                UpdateTotalPrice(e.RowHandle);
                SaveSupplierReturnsDetail(e.RowHandle);
            }
        }

        private void btnGenerateDebitNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSupplierReturns.RowCount == 0)
                    return;
                supplierRepository.GenerateDebitNote(supplierReturns.SupplierReturnsID, Utility.UserID);
                XtraMessageBox.Show("Create Note Initiated Successfully",
                        "Information!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearItemData();
                SupplierHelper.ShowSupplierDebitNote(supplierReturns.SupplierReturnsID, rgPrintType.EditValue.Equals(1));
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplierReturns.SupplierReturnsID == null || supplierReturns.SupplierReturnsID.Equals(0))
                    return;
                var result = XtraMessageBox.Show("Are sure want to discard return sheet?",
                    "Confirmation!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (!Convert.ToString(result).ToLower().Equals("yes"))
                    return;
                supplierRepository.DiscardSupplierReturns(supplierReturns.SupplierReturnsID, Utility.UserID);
                XtraMessageBox.Show("Return sheet discarded successfully.", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSupplier.EditValue = null;
                cmbStockEntry.EditValue = null;
                cmbFromBranch.EditValue = null;
                gcSupplierReturns.DataSource = null;
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnSelectSupplierItems_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.EditValue == null ||
                    cmbCategory.EditValue == null ||
                    cmbFromBranch.EditValue == null)
                return;
            DataTable dt = supplierRepository.GetSupplierItems(
                cmbSupplier.EditValue, cmbFromBranch.EditValue, cmbCategory.EditValue, supplierReturns.SupplierReturnsID);
            frmSupplierItemsByBranchStock obj = new frmSupplierItemsByBranchStock(dt);
            obj.RowSelected += OnPopupRowSelected;
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog(this);
        }

        private void OnPopupRowSelected(SupplierRowModel selected)
        {
            InsertOrUpdateRowFromPopup(selected);
        }

        private void InsertOrUpdateRowFromPopup(SupplierRowModel model)
        {
            int rowHandle = -1;
            bool isNewRow = false;
            decimal oldQuantity = 0;

            try
            {
                if (model == null || model.Quantity <= 0)
                    return;

                if (supplierReturns.SupplierReturnsID.Equals(0))
                {
                    InitialLoad();

                    if (supplierReturns.SupplierReturnsID.Equals(0))
                    {
                        var result = XtraMessageBox.Show(
                            "You are about save a new return sheet for selected supplier. Do you want to continue?",
                            "Confirmation!",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                            return;
                    }

                    SaveSupplierReturns();
                }

                gvSupplierReturns.GridControl.BindingContext = new BindingContext();
                gvSupplierReturns.GridControl.DataSource = supplierReturns.dtSupplierReturns;
                isEventCall = true;

                rowHandle = FindExistingRow(model.ItemCostPriceID, model.ReasonID);

                if (rowHandle < 0)
                {
                    gvSupplierReturns.AddNewRow();
                    rowHandle = gvSupplierReturns.FocusedRowHandle;
                    isNewRow = true;

                    gvSupplierReturns.SetRowCellValue(rowHandle, "SNO", gvSupplierReturns.RowCount + 1);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "SUPPLIERRETURNSDETAILID", -1);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "SUPPLIERRETURNSID", supplierReturns.SupplierReturnsID);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "ITEMCOSTPRICEID", model.ItemCostPriceID);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "ITEMCODE", model.ItemCode);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "ITEMNAME", model.ItemName);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "MRP", model.MRP);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "COSTPRICE", model.CostPrice);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "QUANTITY", model.Quantity);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "TOTALCOSTPRICE",model.Quantity * model.CostPrice);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "REASONID", model.ReasonID);
                }
                else
                {
                    oldQuantity = Convert.ToDecimal(gvSupplierReturns.GetRowCellValue(rowHandle, "QUANTITY"));
                    decimal newQty = oldQuantity + model.Quantity;

                    gvSupplierReturns.SetRowCellValue(rowHandle, "QUANTITY", newQty);

                    gvSupplierReturns.SetRowCellValue(rowHandle, "TOTALCOSTPRICE",
                        newQty * model.CostPrice);
                }

                isEventCall = false;
                gvSupplierReturns.GridControl.BindingContext = new BindingContext();
                gvSupplierReturns.GridControl.DataSource = supplierReturns.dtSupplierReturns;

                rowHandle = FindExistingRow(model.ItemCostPriceID, model.ReasonID);
                if (rowHandle >= 0)
                {
                    gvSupplierReturns.FocusedRowHandle = rowHandle;
                    SaveSupplierReturnsDetail(rowHandle);
                }
            }
            catch (Exception ex)
            {
                // ROLLBACK LOGIC
                if (isNewRow && rowHandle >= 0)
                {
                    gvSupplierReturns.DeleteRow(rowHandle);
                }
                else if (!isNewRow && rowHandle >= 0)
                {
                    gvSupplierReturns.SetRowCellValue(rowHandle, "QUANTITY", oldQuantity);
                    gvSupplierReturns.SetRowCellValue(rowHandle, "TOTALCOSTPRICE",
                        oldQuantity * model.CostPrice);
                }

                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private int FindExistingRow(int itemCostPriceId, Object reasonId)
        {
            for (int i = 0; i < gvSupplierReturns.RowCount; i++)
            {
                int id = Convert.ToInt32(gvSupplierReturns.GetRowCellValue(i, "ITEMCOSTPRICEID"));
                
                if (id == itemCostPriceId)
                    return i;
            }
            return -1;
        }

        private void gcSupplierReturns_Click(object sender, EventArgs e)
        {

        }
    }
}