using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmMRP : XtraForm
    {
        ItemMRP _itemMRP;

        public ItemMRP ItemMRPObj => _itemMRP;

        public frmMRP(ItemMRP itemMRP)
        {
            InitializeComponent();
            cmbGSTCode.Properties.DataSource = Utility.GetGSTInfoList();
            cmbGSTCode.Properties.ValueMember = "GSTID";
            cmbGSTCode.Properties.DisplayMember = "GSTCODE";
            _itemMRP = itemMRP;
        }

        private void frmMRP_Load(object sender, EventArgs e)
        {
            txtMRP.EditValue = _itemMRP.MRP;
            txtSalePrice.EditValue = _itemMRP.SalePrice;
            if (_itemMRP.GSTID != null)
            {
                cmbGSTCode.EditValue = _itemMRP.GSTID;
                cmbGSTCode.Enabled = false;
            }

            chkImmediate.EditValue = _itemMRP.Immediate;
            dtLiveDate.EditValue = _itemMRP.GoLiveDateTime;

            bool isEdit = 
                !string.IsNullOrEmpty(_itemMRP.ITEMPRICEID?.ToString())
                && int.TryParse(_itemMRP.ITEMPRICEID.ToString(), out int itemPriceID) 
                && itemPriceID > 0;

            txtMRP.Enabled = !isEdit;
            //bool.TryParse(_itemMRP.MoveStatus?.ToString(), out bool isProcessed);
            //chkImmediate.Enabled = 
            //    dtLiveDate.Enabled = isProcessed;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!dxValidationProvider1.Validate()) return;

            if(!chkImmediate.Checked && dtLiveDate.EditValue == null)
            {
                XtraMessageBox.Show("Go live date is required if price is not immediate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtLiveDate.Focus();
                return;
            }

            if(decimal.TryParse(txtMRP.EditValue.ToString(), out decimal MRP) 
                && decimal.TryParse(txtSalePrice.EditValue.ToString(), out decimal salePrice)
                && MRP < salePrice)
            {
                XtraMessageBox.Show("Sale price cannot be less than MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSalePrice.Focus();
                return;
            }

            if (XtraMessageBox.Show("Do you want to continue?", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            _itemMRP.MRP = txtMRP.EditValue;
            _itemMRP.SalePrice = txtSalePrice.EditValue;
            _itemMRP.GSTID = cmbGSTCode.EditValue;
            _itemMRP.Immediate = chkImmediate.EditValue;
            _itemMRP.GoLiveDateTime = dtLiveDate.EditValue;
            _itemMRP.IsSave = true;
            try
            {
                int NewItempriceID = new ItemCodeRepository().SaveItemPrice(_itemMRP);
                this.Close();
            }
            catch(Exception ex)
            {
                new frmErrorDetails(ex).ShowDialog();
            }
        }

        private void chkImmediate_CheckedChanged(object sender, EventArgs e)
        {
            dtLiveDate.Enabled = !chkImmediate.Checked;
            dtLiveDate.EditValue = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}