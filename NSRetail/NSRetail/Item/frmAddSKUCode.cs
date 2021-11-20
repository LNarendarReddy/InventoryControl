using Entity;
using System;

namespace NSRetail
{
    public partial class frmAddSKUCode : DevExpress.XtraEditors.XtraForm
    {
        ItemSKUCode itemSKUCodeObj;

        public frmAddSKUCode(ItemSKUCode itemSKUCode)
        {
            itemSKUCodeObj = itemSKUCode;
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!dxAddSKUValidationProvider.Validate())
                return;
            itemSKUCodeObj.ItemName = txtItemName.EditValue;
            itemSKUCodeObj.Description = txtDescription.EditValue;
            itemSKUCodeObj.SKUCode = txtSKUCode.EditValue;
            itemSKUCodeObj.IsSave = true;
            this.Close();
        }
    }
}
