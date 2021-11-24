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

        public frmItemCode(Item item)
        {
            InitializeComponent();
            itemObj = item;
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            chkIsEAN_CheckedChanged(null, null);
            BindDataSource(false);
                       
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
                
                new ItemCodeRepository().SaveItemCode(itemObj);
                
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
            //txtItemCode.ReadOnly = !chkIsEAN.Checked;
            //if (!chkIsEAN.Checked)
            //{                
            //    txtItemCode.EditValue = sluSKUCode.Text;
            //}

        }

        private void searchLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            bool hasValue = sluSKUCode.EditValue != null && (int)sluSKUCode.EditValue > 0 && searchLookUpEdit1View.GetFocusedDataRow() != null;

            txtItemName.EditValue = hasValue ? searchLookUpEdit1View.GetFocusedDataRow()["ITEMNAME"] : string.Empty;
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
            drNewSKUCode["ITEMID"] = 0;
            drNewSKUCode["SKUCODE"] = itemNewCodeObj.Code;
            dtItems.Rows.Add(drNewSKUCode);
            searchLookUpEdit1View.RefreshData();
            sluSKUCode.EditValue = 0;
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
            luGST.Properties.DataSource = Utility.GetGSTBaseline();
            luGST.Properties.DisplayMember = "GSTCODE";
            luGST.Properties.ValueMember = "GSTID";
            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.DisplayMember = "ITEMCODE";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            if(refresh)
            {
                sluItemCode.EditValue = selectedItemCode;
                sluSKUCode.EditValue = selectedSKU;
                luGST.EditValue = selectedGST;
            }
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(sluItemCode.EditValue) > 0)
            {
                DataTable dtItemDetails = new ItemCodeRepository().GetItemCode(sluItemCode.EditValue);

                txtHSNCode.EditValue = dtItemDetails.Rows[0]["HSNCODE"];
                chkIsEAN.EditValue = dtItemDetails.Rows[0]["ISEAN"];
                sluSKUCode.EditValue = dtItemDetails.Rows[0]["ITEMID"];
                
                txtItemName.EditValue = dtItemDetails.Rows[0]["ITEMNAME"];
                //txtDescription.EditValue = dtItemDetails.Rows[0]["DESCRIPTION"];
                txtCostPrice.EditValue = dtItemDetails.Rows[0]["COSTPRICE"];
                txtSalePrice.EditValue = dtItemDetails.Rows[0]["SALEPRICE"];
                txtMRP.EditValue = dtItemDetails.Rows[0]["MRP"];
                itemObj.ItemID = dtItemDetails.Rows[0]["ITEMID"];
                luGST.EditValue = dtItemDetails.Rows[0]["GSTID"];
                Text = "Edit Item - " + sluItemCode.EditValue;
            }
        }

        private void btnAddItemCode_Click(object sender, EventArgs e)
        {
            ItemNewCode itemNewCodeObj = new ItemNewCode() { isSKUorItem = false };
            new frmAddCode(itemNewCodeObj).ShowDialog();
            if (!itemNewCodeObj.IsSave)
            {
                return;
            }

            DataTable dtItemCodes = Utility.GetItemCodeList();
            DataRow drNewItemCode = dtItemCodes.NewRow();
            drNewItemCode["ITEMCODEID"] = 0;
            drNewItemCode["ITEMCODE"] = itemNewCodeObj.Code;
            dtItemCodes.Rows.Add(drNewItemCode);
            gridView1.RefreshData();
            sluItemCode.EditValue = 0;
        }
    }
}
