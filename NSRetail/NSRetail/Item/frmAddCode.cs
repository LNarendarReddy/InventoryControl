using Entity;
using System;

namespace NSRetail
{
    public partial class frmAddCode : DevExpress.XtraEditors.XtraForm
    {
        ItemNewCode itemCodeObj;

        public frmAddCode(ItemNewCode itemNewCode)
        {
            itemCodeObj = itemNewCode;
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!dxAddSKUValidationProvider.Validate())
                return;
            itemCodeObj.Code = txtCode.EditValue;
            itemCodeObj.IsSave = true;
            this.Close();
        }

        private void frmAddCode_Load(object sender, EventArgs e)
        {
            if(itemCodeObj.isSKUorItem)
            {
                layoutControlItem6.ContentVisible = true;
                this.Text = "Add SKU Code";
                lcCode.Text = "SKU Code";
            }
            else
            {
                layoutControlItem6.ContentVisible = false;
                this.Text = "Add Item Code";
                lcCode.Text = "Item Code";
            }
        }
    }
}
