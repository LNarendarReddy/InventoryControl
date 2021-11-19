using System;
using System.Data;
using DataAccess;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmItem : DevExpress.XtraEditors.XtraForm
    {
        Item itemObj;

        public frmItem(Item item)
        {
            InitializeComponent();
            itemObj = item;
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            chkIsEAN_CheckedChanged(null, null);

            if(Convert.ToInt32(itemObj.ItemID) > 0)
            {
                DataSet dsItemDetails = new ItemRepository().GetItem(itemObj.ItemID);
                txtItemCode.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["ITEMCODE"];
                txtItemName.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["ITEMNAME"];
                txtDescription.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["DESCRIPTION"];
                txtHSNCode.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["HSNCODE"];
                chkIsEAN.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["ISEANCODE"];
                txtEANCode.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["EANCODE"];
                txtCostPrice.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["COSTPRICE"];
                txtSalePrice.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["SALEPRICE"];
                txtMRP.EditValue = dsItemDetails.Tables["ITEM"].Rows[0]["MRP"];

                gcBarCodes.DataSource = dsItemDetails.Tables["ITEMBARCODE"];
                gcItemPrice.DataSource = dsItemDetails.Tables["ITEMBARCODEPRICE"];
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxItemValidationProvider.Validate())
                    return;
                itemObj.ItemCode = txtItemCode.EditValue;
                itemObj.ItemName = txtItemName.EditValue;
                itemObj.Description = txtDescription.EditValue;
                itemObj.HSNCode = txtHSNCode.EditValue;
                itemObj.IsEANCode = chkIsEAN.EditValue;
                itemObj.EANCode = txtEANCode.EditValue;
                itemObj.CostPrice = txtCostPrice.EditValue;
                itemObj.SalePrice = txtSalePrice.EditValue;
                itemObj.MRP = txtMRP.EditValue;
                itemObj.UserID = Utility.UserID;
                new ItemRepository().SaveItem(itemObj);
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
            txtEANCode.ReadOnly = !chkIsEAN.Checked;
            txtEANCode.Text = !chkIsEAN.Checked ? txtItemCode.Text : string.Empty;            
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            txtEANCode.Text = !chkIsEAN.Checked ? txtItemCode.Text : txtEANCode.Text;
        }
    }
}
