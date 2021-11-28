using System;
using System.Data;
using System.Windows.Forms;
using DataAccess;
using DevExpress.XtraGrid;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmItemCode : DevExpress.XtraEditors.XtraForm
    {
        Item itemObj;

        public frmItemCode(Item item)
        {
            InitializeComponent();
            itemObj = item;
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            chkIsEAN_CheckedChanged(null, null);
            BindDataSource(false);
            if(Convert.ToInt32(itemObj.ItemCodeID) > 0)
            {
                // setting the item code calls the get proc and loads all values
                sluItemCode.EditValue = itemObj.ItemCodeID;
            }
            else if(!string.IsNullOrEmpty(Convert.ToString(itemObj.ItemCode)))
            {
                int rowHandle = gvItemCode.LocateByValue("ITEMCODE", itemObj.ItemCode);
                
                // if the scanned item is already existing, go into edit mode
                if (rowHandle != GridControl.InvalidRowHandle)
                {
                    gvItemCode.FocusedRowHandle = rowHandle;
                    itemObj.ItemCodeID = gvItemCode.GetFocusedRowCellValue("ITEMCODEID");
                }
                else
                {
                    DataTable dtItemCodes = Utility.GetItemCodeList();
                    DataRow drNewItemCode = dtItemCodes.NewRow();
                    drNewItemCode["ITEMCODE"] = itemObj.ItemCode;
                    itemObj.ItemCodeID = -1;
                    drNewItemCode["ITEMCODEID"] = itemObj.ItemCodeID;
                    dtItemCodes.Rows.Add(drNewItemCode);
                }

                sluItemCode.EditValue = itemObj.ItemCodeID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxItemValidationProvider.Validate())
                    return;
                itemObj.ItemCode = sluItemCode.Text;                
                itemObj.ItemCodeID = sluItemCode.EditValue;
                itemObj.HSNCode = txtHSNCode.EditValue;
                itemObj.IsEAN = chkIsEAN.EditValue;
                itemObj.SKUCode = sluSKUCode.Text;
                itemObj.ItemID = sluSKUCode.EditValue;
                itemObj.ItemName = txtItemName.EditValue;
                //itemObj.Description = txtDescription.EditValue;
                itemObj.CostPrice = txtCostPrice.EditValue;
                itemObj.SalePrice = txtSalePrice.EditValue;
                itemObj.MRP = txtMRP.EditValue;
                itemObj.UserID = Utility.UserID;
                itemObj.GSTID = luGST.EditValue;
                itemObj.CategoryID = gluCategory.EditValue;
                
                new ItemCodeRepository().SaveItemCode(itemObj);

                // refresh values back in data tables

                DataTable dtItemSKUList = Utility.GetItemSKUList();
                DataTable dtItemCodeList = Utility.GetItemCodeList();

                int itemSKURowHandle, itemCodeRowHandle;
                itemSKURowHandle = gvItemSKU.LocateByValue("SKUCODE", itemObj.SKUCode);
                itemCodeRowHandle = gvItemCode.LocateByValue("ITEMCODE", itemObj.ItemCode);

                if(itemSKURowHandle != GridControl.InvalidRowHandle)
                {
                    DataRow drItemSKU = dtItemSKUList.Rows[itemSKURowHandle];
                    drItemSKU["ITEMID"] = itemObj.ItemID;
                    drItemSKU["ITEMNAME"] = itemObj.ItemName;
                }

                if (itemCodeRowHandle != GridControl.InvalidRowHandle)
                {
                    DataRow drItemCode = dtItemCodeList.Rows[itemCodeRowHandle];
                    drItemCode["ITEMID"] = itemObj.ItemID;
                    drItemCode["ITEMNAME"] = itemObj.ItemName;
                    drItemCode["ITEMCODEID"] = itemObj.ItemCodeID;
                    drItemCode["SKUCODE"] = itemObj.SKUCode;
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
            //txtItemCode.ReadOnly = !chkIsEAN.Checked;
            //if (!chkIsEAN.Checked)
            //{                
            //    txtItemCode.EditValue = sluSKUCode.Text;
            //}

        }

        private void searchLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            bool hasValue = sluSKUCode.EditValue != null && (int)sluSKUCode.EditValue > 0 && gvItemSKU.GetFocusedDataRow() != null;

            txtItemName.EditValue = hasValue ? gvItemSKU.GetFocusedDataRow()["ITEMNAME"] : string.Empty;
            //txtDescription.EditValue = hasValue ? searchLookUpEdit1View.GetFocusedDataRow()["DESCRIPTION"] : string.Empty;
            //txtItemCode.EditValue = hasValue && !chkIsEAN.Checked ? sluSKUCode.Text : txtItemCode.EditValue;
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
            object selectedSKU = null, selectedGST = null, selectedItemCode = null;
            if(refresh)
            {
                selectedSKU = sluSKUCode.EditValue;
                selectedItemCode = sluItemCode.EditValue;
                selectedGST = luGST.EditValue;
            }

            sluSKUCode.Properties.DataSource = Utility.GetItemSKUList();
            sluSKUCode.Properties.DisplayMember = "SKUCODE";
            sluSKUCode.Properties.ValueMember = "ITEMID";
            
            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.DisplayMember = "ITEMCODE";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            luGST.Properties.DataSource = Utility.GetGSTBaseline();
            luGST.Properties.DisplayMember = "GSTCODE";
            luGST.Properties.ValueMember = "GSTID";

            gluCategory.Properties.DataSource = new MasterRepository().GetCategory();
            gluCategory.Properties.DisplayMember = "CATEGORYNAME";
            gluCategory.Properties.ValueMember = "CATEGORYID";

            if (refresh)
            {
                sluItemCode.EditValue = selectedItemCode;
                sluSKUCode.EditValue = selectedSKU;
                luGST.EditValue = selectedGST;
            }

            if(!refresh)
            {
                // force grid view data source to be opened even before popup is invoked
                gvItemCode.GridControl.BindingContext = new BindingContext();
                gvItemCode.GridControl.DataSource = sluItemCode.Properties.DataSource;

                gvItemSKU.GridControl.BindingContext = new BindingContext();
                gvItemSKU.GridControl.DataSource = sluSKUCode.Properties.DataSource;
            }
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(sluItemCode.EditValue) <= 0)
            {
                return;
            }

            DataSet dsItemDetails = new ItemCodeRepository().GetItemCode(sluItemCode.EditValue);

            DataTable dtItemDetails = dsItemDetails.Tables["ITEMCODEDETAIL"];

            txtHSNCode.EditValue = dtItemDetails.Rows[0]["HSNCODE"];
            chkIsEAN.EditValue = dtItemDetails.Rows[0]["ISEAN"];
            sluSKUCode.EditValue = dtItemDetails.Rows[0]["ITEMID"];
            txtItemName.EditValue = dtItemDetails.Rows[0]["ITEMNAME"];
            Text = "Edit Item - " + txtItemName.Text;
            itemObj.ItemID = dtItemDetails.Rows[0]["ITEMID"];
            //txtDescription.EditValue = dtItemDetails.Rows[0]["DESCRIPTION"];
            gluCategory.EditValue = dtItemDetails.Rows[0]["CATEGORYID"];

            DataTable dtItemCodePrices = dsItemDetails.Tables["ITEMCODEPRICES"];
            DataRow selectedPrice = dtItemCodePrices.Rows[0];
            
            if(dtItemCodePrices.Rows.Count > 1)
            {
                frmMRPList frmMRPList = new frmMRPList(dtItemCodePrices);
                frmMRPList.ShowDialog();
                if (!frmMRPList._IsSave)
                {
                    return;
                }

                selectedPrice = (frmMRPList.drSelected as DataRowView).Row;
            }

            txtCostPrice.EditValue = selectedPrice["COSTPRICE"];
            txtSalePrice.EditValue = selectedPrice["SALEPRICE"];
            txtMRP.EditValue = selectedPrice["MRP"];
            luGST.EditValue = selectedPrice["GSTID"];
        }

        private void frmItemCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(itemObj.IsSave)
            {
                return;
            }

            // remove items added to data tables but not saved
            DataTable dtItemSKUList = Utility.GetItemSKUList();
            DataTable dtItemCodeList = Utility.GetItemCodeList();

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
    }
}
