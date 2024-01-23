using System;
using System.Data;
using System.Windows.Forms;
using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmItemCode : DevExpress.XtraEditors.XtraForm
    {
        Item itemObj;
        GridView gvItemCode;
        bool isLoading, isEditMode;

        public frmItemCode(Item item)
        {
            InitializeComponent();
            itemObj = item;
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            frmItemCodeList parentForm = Owner as frmItemCodeList;
            gvItemCode = parentForm.ItemCodeListGridView;

            chkIsEAN_CheckedChanged(null, null);
            BindDataSource(false);

            // setting the item code calls the get proc and loads all values
            txtItemCode.EditValue = itemObj.ItemCode;
            luSubCategory.CascadingOwner = gluCategory;
            luSubCategory.Properties.CascadingMember = "CATEGORYID";

            cmbClassification.CascadingOwner = gluCategory;
            cmbClassification.Properties.CascadingMember = "CATEGORYID";

            cmbSubClassification.CascadingOwner = cmbClassification;
            cmbSubClassification.Properties.CascadingMember = "CLASSIFICATIONID";

            txtItemCode_Properties_Leave(null, null);

            btnSave.Enabled = Utility.Role != "Division Manager" && Utility.Role != "Division User";
            if (!string.IsNullOrEmpty(Convert.ToString(itemObj.ItemCode)))
            {
                txtMRP.Enabled = false;
                txtSalePrice.Enabled = false; 
                txtCostPriceWOT.Enabled = false;
                txtCostPriceWT.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxItemValidationProvider.Validate())
                    return;
                if (decimal.TryParse(Convert.ToString(txtMRP.EditValue), out decimal MRP) &&
                    decimal.TryParse(Convert.ToString(txtCostPriceWT.EditValue), out decimal CostPriceWT) &&
                    decimal.TryParse(Convert.ToString(txtCostPriceWOT.EditValue), out decimal CostPriceWOT) &&
                    decimal.TryParse(Convert.ToString(txtSalePrice.EditValue), out decimal salePrice))
                {
                    string message = string.Empty;

                    message = MRP < salePrice ? "MRP cannot be less than sale price" : message;
                    message = salePrice < CostPriceWT ? "Cost Price cannot be greater than sale price" : message;
                    message = CostPriceWOT == 0 ? "Cost price with out tax cannot be zero" : message;
                    message = CostPriceWT == 0 ? "Cost price with tax cannot be zero" : message;
                    message = CostPriceWOT > CostPriceWT ? "Cost price with tax less than cost price without tax" : message;

                    if (!string.IsNullOrEmpty(message))
                    {
                        XtraMessageBox.Show(message);
                        return;
                    }
                }

                itemObj.ItemCode = txtItemCode.Text;                
                itemObj.HSNCode = txtHSNCode.EditValue;
                itemObj.IsEAN = chkIsEAN.EditValue;
                itemObj.SKUCode = sluSKUCode.Text;
                itemObj.ItemID = sluSKUCode.EditValue;
                itemObj.ItemName = txtItemName.EditValue;
                itemObj.CostPriceWT = txtCostPriceWT.EditValue;
                itemObj.CostPriceWOT = txtCostPriceWOT.EditValue;
                itemObj.SalePrice = txtSalePrice.EditValue;
                itemObj.MRP = txtMRP.EditValue;
                itemObj.UserID = Utility.UserID;
                itemObj.GSTID = luGST.EditValue;
                itemObj.CategoryID = gluCategory.EditValue;
                itemObj.CategoryName = gluCategory.Text;
                itemObj.SubCategoryID = luSubCategory.EditValue;
                itemObj.SubCategoryName = luSubCategory.Text;
                itemObj.IsOpenItem = chkIsOpenItem.EditValue;
                itemObj.ParentItemID = sluParentItem.EditValue;
                itemObj.UOMID = luUOM.EditValue;
                itemObj.FreeItemCodeID = sluFreeItem.EditValue;
                itemObj.ClassificationID = cmbClassification.EditValue;
                itemObj.SubClassificationID = cmbSubClassification.EditValue;

                new ItemCodeRepository().SaveItemCode(itemObj);

                // refresh values back in data tables

                DataTable dtItemSKUList = Utility.GetItemSKUList();
                DataTable dtItemCodeList = Utility.GetItemCodeListFiltered();

                int itemSKURowHandle, itemCodeRowHandle;
                itemSKURowHandle = gvItemSKU.LocateByValue("SKUCODE", itemObj.SKUCode);
                itemCodeRowHandle = gvItemCode.LocateByValue("ITEMCODE", itemObj.ItemCode);

                if(itemSKURowHandle != GridControl.InvalidRowHandle)
                {
                    DataRow drItemSKU = dtItemSKUList.Rows[itemSKURowHandle];
                    drItemSKU["ITEMID"] = itemObj.ItemID;
                    drItemSKU["ITEMNAME"] = itemObj.ItemName;
                }
                else
                {
                    DataRow drNew = dtItemSKUList.NewRow();
                    drNew["ITEMID"] = itemObj.ItemID;
                    drNew["ITEMNAME"] = itemObj.ItemName;
                    dtItemSKUList.Rows.Add(drNew);
                }


                if (itemCodeRowHandle != GridControl.InvalidRowHandle)
                {
                    gvItemCode.SetRowCellValue(itemCodeRowHandle, "ITEMID", itemObj.ItemID);
                    gvItemCode.SetRowCellValue(itemCodeRowHandle, "ITEMNAME", itemObj.ItemName);
                    gvItemCode.SetRowCellValue(itemCodeRowHandle, "ITEMCODEID", itemObj.ItemCodeID);
                    gvItemCode.SetRowCellValue(itemCodeRowHandle, "SKUCODE", itemObj.SKUCode);
                    gvItemCode.SetRowCellValue(itemCodeRowHandle, "CATEGORYNAME", itemObj.CategoryName);
                    gvItemCode.SetRowCellValue(itemCodeRowHandle, "SUBCATEGORYNAME", itemObj.SubCategoryName);

                    //DataRow drItemCode = dtItemCodeList.Rows[itemCodeRowHandle];
                    //drItemCode["ITEMID"] = itemObj.ItemID;
                    //drItemCode["ITEMNAME"] = itemObj.ItemName;
                    //drItemCode["ITEMCODEID"] = itemObj.ItemCodeID;
                    //drItemCode["SKUCODE"] = itemObj.SKUCode;
                    //drItemCode["CATEGORYNAME"] = itemObj.CategoryName;
                    //drItemCode["SUBCATEGORYNAME"] = itemObj.SubCategoryName;
                }
                else
                {
                    DataRow drNewItemCode = dtItemCodeList.NewRow();
                    drNewItemCode["ITEMID"] = itemObj.ItemID;
                    drNewItemCode["ITEMNAME"] = itemObj.ItemName;
                    drNewItemCode["ITEMCODE"] = itemObj.ItemCode;
                    drNewItemCode["ITEMCODEID"] = itemObj.ItemCodeID;
                    drNewItemCode["SKUCODE"] = itemObj.SKUCode;
                    drNewItemCode["CATEGORYNAME"] = itemObj.CategoryName;
                    drNewItemCode["SUBCATEGORYNAME"] = itemObj.SubCategoryName;
                    drNewItemCode["CREATEDBY"] = Utility.FullName;
                    drNewItemCode["CREATEDDATE"] = DateTime.Now;
                    dtItemCodeList.Rows.Add(drNewItemCode);
                }

                itemObj.IsSave = true;
                itemObj.IsNewToggleSwitched = tsCreateNew.EditValue;
                this.Close();

            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void chkIsEAN_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            txtItemCode.ReadOnly = !chkIsEAN.Checked;
            if (!chkIsEAN.Checked)
            {
                txtItemCode.EditValue = sluSKUCode.Text;
            }
        }

        private void searchLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            bool hasValue = sluSKUCode.EditValue != null && (int)sluSKUCode.EditValue > 0 && gvItemSKU.GetFocusedDataRow() != null;

            txtItemName.EditValue = hasValue ? gvItemSKU.GetFocusedDataRow()["ITEMNAME"] : string.Empty;
            txtItemCode.EditValue = hasValue && !chkIsEAN.Checked ? gvItemSKU.GetFocusedDataRow()["SKUCODE"] : txtItemCode.EditValue;
        }

        private void btnAddSKU_Click(object sender, EventArgs e)
        {
            ItemNewCode itemNewCodeObj = new ItemNewCode() { isSKUorItem = true };
            new frmAddCode(itemNewCodeObj).ShowDialog();
            if(!itemNewCodeObj.IsSave)
            {
                return;
            }

            DataTable dtItems = Utility.GetItemSKUList();
            DataRow drNewSKUCode = dtItems.NewRow();
            drNewSKUCode["ITEMID"] = -1;
            drNewSKUCode["SKUCODE"] = itemNewCodeObj.Code;
            dtItems.Rows.Add(drNewSKUCode);
            gvItemSKU.RefreshData();
            sluSKUCode.EditValue = -1;
        }

        private void BindDataSource(bool refresh)
        {
            object selectedSKU = null, selectedGST = null, selectedItemCode = null, selectedParentItemID = null;
            MasterRepository masterRepository = new MasterRepository();
            ReportRepository reportRepository = new ReportRepository();

            if (refresh)
            {
                selectedSKU = sluSKUCode.EditValue;
                selectedItemCode = txtItemCode.EditValue;
                selectedGST = luGST.EditValue;
                selectedParentItemID = sluParentItem.EditValue;
            }

            sluSKUCode.Properties.DataSource = Utility.GetItemSKUList();
            sluSKUCode.Properties.DisplayMember = "SKUCODE";
            sluSKUCode.Properties.ValueMember = "ITEMID";
            
            luGST.Properties.DataSource = Utility.GetGSTInfoList();
            luGST.Properties.DisplayMember = "GSTCODE";
            luGST.Properties.ValueMember = "GSTID";

            DataView dvCategory = Utility.GetCategoryList().Copy().DefaultView;
            dvCategory.RowFilter = "CATEGORYNAME <> 'ALL'";
            gluCategory.Properties.DataSource = dvCategory;
            gluCategory.Properties.DisplayMember = "CATEGORYNAME";
            gluCategory.Properties.ValueMember = "CATEGORYID";
            if(dvCategory.Count == 1)
            {
                gluCategory.EditValue = dvCategory[0]["CATEGORYID"];
            }

            sluParentItem.Properties.DataSource = Utility.GetParentItemList();
            sluParentItem.Properties.DisplayMember = "ITEMNAME";
            sluParentItem.Properties.ValueMember = "ITEMID";

            luUOM.Properties.DataSource = masterRepository.GetUOM().DefaultView;
            luUOM.Properties.DisplayMember = "DISPLAYVALUE";
            luUOM.Properties.ValueMember = "UOMID";

            luSubCategory.Properties.DataSource = masterRepository.GetSubCategory();
            luSubCategory.Properties.DisplayMember = "SUBCATEGORYNAME";
            luSubCategory.Properties.ValueMember = "SUBCATEGORYID";

            cmbClassification.Properties.DataSource = reportRepository.GetReportData("USP_R_ITEMCLASSIFICATION");
            cmbClassification.Properties.DisplayMember = "CLASSIFICATIONNAME";
            cmbClassification.Properties.ValueMember = "CLASSIFICATIONID";

            cmbSubClassification.Properties.DataSource = reportRepository.GetReportData("USP_R_ITEMSUBCLASSIFICATION");
            cmbSubClassification.Properties.DisplayMember = "SUBCLASSIFICATIONNAME";
            cmbSubClassification.Properties.ValueMember = "SUBCLASSIFICATIONID";

            sluFreeItem.Properties.DataSource = Utility.GetItemCodeListFiltered();
            sluFreeItem.Properties.DisplayMember = "ITEMNAME";
            sluFreeItem.Properties.ValueMember = "ITEMCODEID";

            if (refresh)
            {
                txtItemCode.EditValue = selectedItemCode;
                sluSKUCode.EditValue = selectedSKU;
                luGST.EditValue = selectedGST;
                sluParentItem.EditValue = selectedParentItemID;
            }

            if(!refresh)
            {
                //// force grid view data source to be opened even before popup is invoked
                //gvItemCode.GridControl.BindingContext = new BindingContext();
                //gvItemCode.GridControl.DataSource = txtItemCode.Properties.DataSource;

                gvItemSKU.GridControl.BindingContext = new BindingContext();
                gvItemSKU.GridControl.DataSource = sluSKUCode.Properties.DataSource;
            }
        }

        private void frmItemCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(itemObj.IsSave)
            {
                return;
            }

            // remove items added to data tables but not saved
            DataTable dtItemSKUList = Utility.GetItemSKUList();
            DataTable dtItemCodeList = Utility.GetItemCodeListFiltered();

            int itemSKURowHandle, itemCodeRowHandle;
            itemSKURowHandle = gvItemSKU.LocateByValue("ITEMID", -1);
            itemCodeRowHandle = gvItemCode.LocateByValue("ITEMCODEID", -1);

            if (itemSKURowHandle != GridControl.InvalidRowHandle)
            {
                dtItemSKUList.Rows.RemoveAt(itemSKURowHandle);
            }

            if (itemCodeRowHandle != GridControl.InvalidRowHandle)
            {
                dtItemCodeList.Rows.RemoveAt(itemCodeRowHandle);
            }
        }

        private void chkIsOpenItem_CheckedChanged(object sender, EventArgs e)
        {
            if(isLoading) return;

            //sluParentItem.Enabled = !Convert.ToBoolean(chkIsOpenItem.EditValue);
            sluParentItem.EditValue = null;
        }

        private void ClearUI()
        {
            txtHSNCode.EditValue = null;
            chkIsEAN.EditValue = true;
            sluSKUCode.EditValue = null;
            txtItemName.EditValue = null;
            Text = "New Item";
            itemObj.ItemID = null;
            gluCategory.EditValue = null;
            txtCostPriceWT.EditValue = null;
            txtSalePrice.EditValue = null;
            txtMRP.EditValue = null;
            luGST.EditValue = null;
            chkIsOpenItem.EditValue = null;
            sluParentItem.EditValue = null;
            luSubCategory.EditValue = null;
            txtCostPriceWOT.EditValue = null;
            luUOM.EditValue = null;
            sluFreeItem.EditValue = null;
            cmbClassification.EditValue = null;
            cmbSubClassification.EditValue = null;
            isEditMode = false;
        }

        private void txtItemCode_Properties_Leave(object sender, EventArgs e)
        {
            // do not load again unless code changes
            if (isEditMode && itemObj.ItemCode.Equals(txtItemCode.EditValue)) return;

            if (string.IsNullOrEmpty(Convert.ToString(txtItemCode.EditValue)))
            {
                ClearUI();
                return;
            }

            int rowHandle = gvItemCode.LocateByValue("ITEMCODE", txtItemCode.EditValue);

            // if the scanned item is already existing, go into edit mode
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                gvItemCode.FocusedRowHandle = rowHandle;
                itemObj.ItemCodeID = gvItemCode.GetFocusedRowCellValue("ITEMCODEID");
            }
            else
            {
                ClearUI();
                return;
            }

            DataSet dsItemDetails = new ItemCodeRepository().GetItemCode(itemObj.ItemCodeID, Utility.CategoryID);

            DataTable dtItemDetails = dsItemDetails.Tables["ITEMCODEDETAIL"];

            if(dtItemDetails.Rows.Count == 0)
            {
                ClearUI();
                return;
            }

            isLoading = true;
            txtHSNCode.EditValue = dtItemDetails.Rows[0]["HSNCODE"];
            chkIsEAN.EditValue = dtItemDetails.Rows[0]["ISEAN"];
            sluSKUCode.EditValue = dtItemDetails.Rows[0]["ITEMID"];
            txtItemName.EditValue = dtItemDetails.Rows[0]["ITEMNAME"];
            Text = "Edit Item - " + txtItemName.Text;
            itemObj.ItemID = dtItemDetails.Rows[0]["ITEMID"];
            itemObj.ItemCode = dtItemDetails.Rows[0]["ITEMCODE"];
            //txtDescription.EditValue = dtItemDetails.Rows[0]["DESCRIPTION"];
            gluCategory.EditValue = dtItemDetails.Rows[0]["CATEGORYID"];
            chkIsOpenItem.EditValue = dtItemDetails.Rows[0]["ISOPENITEM"];
            sluParentItem.EditValue = dtItemDetails.Rows[0]["PARENTITEMID"];
            luSubCategory.EditValue = dtItemDetails.Rows[0]["SUBCATEGORYID"];
            luUOM.EditValue = dtItemDetails.Rows[0]["UOMID"];
            sluFreeItem.EditValue = dtItemDetails.Rows[0]["FREEITEMCODEID"];
            cmbClassification.EditValue = dtItemDetails.Rows[0]["CLASSIFICATIONID"];
            cmbSubClassification.EditValue = dtItemDetails.Rows[0]["SUBCLASSIFICATIONID"];

            DataTable dtItemCodePrices = dsItemDetails.Tables["ITEMCODEPRICES"];
            DataRow selectedPrice = dtItemCodePrices.Rows[0];

            if (dtItemCodePrices.Rows.Count > 1)
            {
                frmMRPList frmMRPList = new frmMRPList(dtItemCodePrices, itemObj.ItemCodeID, showCostPrice: true);
                frmMRPList.ShowDialog();
                if (!frmMRPList._IsSave)
                {
                    this.Close();
                    return;
                }

                selectedPrice = (frmMRPList.drSelected as DataRowView).Row;
            }

            txtCostPriceWT.EditValue = selectedPrice["COSTPRICEWT"];
            txtCostPriceWOT.EditValue = selectedPrice["COSTPRICEWOT"];
            txtSalePrice.EditValue = selectedPrice["SALEPRICE"];
            txtMRP.EditValue = selectedPrice["MRP"];
            luGST.EditValue = selectedPrice["GSTID"];

            isLoading = false;
            isEditMode = true;
        }

        private void sluParentItem_Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            luUOM.Enabled = !Convert.ToBoolean(chkIsOpenItem.EditValue) && sluParentItem.EditValue != null;
            if (luUOM.Enabled)
            {
                DataView dvUOM = luUOM.Properties.DataSource as DataView;
                dvUOM.RowFilter = "BASEUOMID = " + (sluParentItem.GetSelectedDataRow() as DataRowView)["UOMID"];
            }
        }

        private void gluCategory_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView drCategory = gluCategory.GetSelectedDataRow() as DataRowView;
            bool allowOpenItems = drCategory != null && Convert.ToBoolean(drCategory["ALLOWOPENITEMS"]);
            chkIsOpenItem.Enabled = allowOpenItems;
            sluParentItem.Enabled = allowOpenItems;

            if (isLoading) return;

            luSubCategory.EditValue = null;
            cmbClassification.EditValue = null;
        }

        private void luGST_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            isLoading = true;
            decimal costPriceWithTax = Convert.ToDecimal(txtCostPriceWT.EditValue);
            txtCostPriceWOT.EditValue = costPriceWithTax - costPriceWithTax * (luGST.GetSelectedDataRow() as GSTInfo).TAXPercent;
            isLoading = false;
        }

        private void txtCostPriceWT_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading || luGST.EditValue == null) return;

            isLoading = true;
            decimal costPriceWithTax = Convert.ToDecimal(txtCostPriceWT.EditValue);
            txtCostPriceWOT.EditValue = costPriceWithTax - costPriceWithTax * (luGST.GetSelectedDataRow() as GSTInfo).TAXPercent;
            isLoading = false;
        }

        private void sluParentItem_EditValueChanged(object sender, EventArgs e)
        {
            bool cpEnabled = sluParentItem.EditValue == null || sluParentItem.EditValue.Equals(itemObj.ItemID);


        }

        private void cmbClassification_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            cmbSubClassification.EditValue = null;
        }

        private void txtCostPriceWOT_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading || luGST.EditValue == null) return;

            isLoading = true;
            decimal costPriceWithoutTax = Convert.ToDecimal(txtCostPriceWOT.EditValue);
            txtCostPriceWT.EditValue = costPriceWithoutTax + costPriceWithoutTax * (luGST.GetSelectedDataRow() as GSTInfo).TAXPercent;
            isLoading = false;
        }
    }
}
