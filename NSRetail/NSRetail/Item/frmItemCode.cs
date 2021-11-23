using System;
using System.Data;
using DataAccess;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmItemCode : DevExpress.XtraEditors.XtraForm
    {
        Item itemObj;
        DataTable dtItems;

        public frmItemCode(Item item, DataTable dtItems)
        {
            InitializeComponent();
            itemObj = item;
            this.dtItems = dtItems;
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            chkIsEAN_CheckedChanged(null, null);
            sluSKUCode.Properties.DataSource = dtItems;
            sluSKUCode.Properties.DisplayMember = "SKUCODE";
            sluSKUCode.Properties.ValueMember = "ITEMID";
            luGST.Properties.DataSource = Utility.GetGSTBaseline();
            luGST.Properties.DisplayMember = "GSTCODE";
            luGST.Properties.ValueMember = "GSTID";

            if (Convert.ToInt32(itemObj.ItemCodeID) > 0)
            {
                DataTable dtItemDetails = new ItemCodeRepository().GetItemCode(itemObj.ItemCodeID);
                                
                txtHSNCode.EditValue = dtItemDetails.Rows[0]["HSNCODE"];
                chkIsEAN.EditValue = dtItemDetails.Rows[0]["ISEAN"];
                sluSKUCode.EditValue = dtItemDetails.Rows[0]["ITEMID"];
                txtItemCode.EditValue = dtItemDetails.Rows[0]["ITEMCODE"];
                txtItemName.EditValue = dtItemDetails.Rows[0]["ITEMNAME"];
                txtDescription.EditValue = dtItemDetails.Rows[0]["DESCRIPTION"];
                txtCostPrice.EditValue = dtItemDetails.Rows[0]["COSTPRICE"];
                txtSalePrice.EditValue = dtItemDetails.Rows[0]["SALEPRICE"];
                txtMRP.EditValue = dtItemDetails.Rows[0]["MRP"];
                itemObj.ItemID = dtItemDetails.Rows[0]["ITEMID"];
                luGST.EditValue = dtItemDetails.Rows[0]["GSTID"];
                Text = "Edit Item - " + txtItemCode.EditValue;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxItemValidationProvider.Validate())
                    return;
                itemObj.ItemCode = txtItemCode.EditValue;                
                itemObj.HSNCode = txtHSNCode.EditValue;
                itemObj.IsEAN = chkIsEAN.EditValue;
                itemObj.SKUCode = sluSKUCode.Text;
                itemObj.ItemID = sluSKUCode.EditValue;
                itemObj.ItemName = txtItemName.EditValue;
                itemObj.Description = txtDescription.EditValue;
                itemObj.CostPrice = txtCostPrice.EditValue;
                itemObj.SalePrice = txtSalePrice.EditValue;
                itemObj.MRP = txtMRP.EditValue;
                itemObj.UserID = Utility.UserID;
                itemObj.GSTID = luGST.EditValue;
                
                new ItemCodeRepository().SaveItemCode(itemObj);
                
                // re assign the item id in case of new SKU
                dtItems.Rows[dtItems.Rows.Count - 1]["ITEMID"] = itemObj.ItemID;
                dtItems.Rows[dtItems.Rows.Count - 1]["ITEMNAME"] = itemObj.ItemName;
                dtItems.Rows[dtItems.Rows.Count - 1]["DESCRIPTION"] = itemObj.Description;

                itemObj.IsSave = true;
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
            txtItemCode.ReadOnly = !chkIsEAN.Checked;
            if (!chkIsEAN.Checked)
            {                
                txtItemCode.EditValue = sluSKUCode.Text;
            }

        }

        private void searchLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            bool hasValue = sluSKUCode.EditValue != null && (int)sluSKUCode.EditValue > 0 && searchLookUpEdit1View.GetFocusedDataRow() != null;

            txtItemName.EditValue = hasValue ? searchLookUpEdit1View.GetFocusedDataRow()["ITEMNAME"] : string.Empty;
            txtDescription.EditValue = hasValue ? searchLookUpEdit1View.GetFocusedDataRow()["DESCRIPTION"] : string.Empty;
            txtItemCode.EditValue = hasValue && !chkIsEAN.Checked ? sluSKUCode.Text : txtItemCode.EditValue;
        }

        private void btnAddSKU_Click(object sender, EventArgs e)
        {
            ItemSKUCode itemSKUCodeObj = new ItemSKUCode();
            new frmAddSKUCode(itemSKUCodeObj).ShowDialog();
            if (!itemSKUCodeObj.IsSave)
            {
                return;
            }

            DataRow drNewSKUCode = dtItems.NewRow();
            drNewSKUCode["ITEMID"] = 0;
            drNewSKUCode["SKUCODE"] = itemSKUCodeObj.SKUCode;
            dtItems.Rows.Add(drNewSKUCode);
            searchLookUpEdit1View.RefreshData();
            sluSKUCode.EditValue = 0;
        }
    }
}
