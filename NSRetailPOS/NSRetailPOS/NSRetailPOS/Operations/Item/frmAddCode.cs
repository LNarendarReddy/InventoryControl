using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Linq;

namespace NSRetailPOS.Operations.Items
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
            itemCodeObj.Code = txtCode.Text;
            itemCodeObj.IsSave = true;
            this.Close();
        }

        private void frmAddCode_Load(object sender, EventArgs e)
        {
            if (itemCodeObj.isSKUorItem)
            {
                layoutControlItem6.ContentVisible = true;
                this.Text = "Add SKU Code";
                lcCode.Text = "SKU Code";
                txtCode.Properties.MaxLength = 10;
                txtCode.Properties.MaskSettings.MaskExpression = string.Concat(Enumerable.Repeat("#", 10));
            }
            else
            {
                layoutControlItem6.ContentVisible = false;
                this.Text = "Add Item Code";
                lcCode.Text = "Item Code";
                txtCode.Properties.MaxLength = 20;
                txtCode.Properties.MaskSettings.MaskExpression = string.Concat(Enumerable.Repeat("#", 20));
            }
        }

        private void btnGenerateSKU_Click(object sender, EventArgs e)
        {
            txtCode.Text = new ItemCodeRepository().GetNextSKUCode();
        }
    }
}
