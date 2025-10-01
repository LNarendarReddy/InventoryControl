using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmChangeGST : DevExpress.XtraEditors.XtraForm
    {
        ItemGST _itemGST;
        public ItemGST ItemGSTobj => _itemGST;

        public frmChangeGST(ItemGST itemGST)
        {
            InitializeComponent();
            cmbGSTCode.Properties.DataSource = Utility.GetGSTInfoList();
            cmbGSTCode.Properties.ValueMember = "GSTID";
            cmbGSTCode.Properties.DisplayMember = "GSTCODE";
            _itemGST = itemGST;
        }

        private void frmChangeGST_Load(object sender, EventArgs e)
        {
            txtSKUCode.EditValue = _itemGST.SKUCODE;
            txtItemName.EditValue = _itemGST.ItemName;
            cmbGSTCode.EditValue = _itemGST.GSTID;
            chkImmediate.EditValue = _itemGST.Immediate;
            dtLiveDate.EditValue = _itemGST.GoLiveDateTime;

            txtSKUCode.Enabled = false; 
            txtItemName.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbGSTCode.EditValue == null)
            {
                XtraMessageBox.Show("GST code is mandatory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbGSTCode.Focus();
                return;
            }

            if (!chkImmediate.Checked && dtLiveDate.EditValue == null)
            {
                XtraMessageBox.Show("Go live date is required if price is not immediate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtLiveDate.Focus();
                return;
            }

            if (XtraMessageBox.Show("Do you want to continue?", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            _itemGST.GSTID = cmbGSTCode.EditValue;
            _itemGST.GSTCode = cmbGSTCode.Text;
            _itemGST.Immediate = chkImmediate.EditValue;
            _itemGST.GoLiveDateTime = dtLiveDate.EditValue;
            try
            {
                int NewItempriceID = new ItemCodeRepository().SaveItemGST(_itemGST);
                _itemGST.IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                new frmErrorDetails(ex).ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkImmediate_CheckedChanged(object sender, EventArgs e)
        {
            dtLiveDate.Enabled = !chkImmediate.Checked;
            dtLiveDate.EditValue = null;
        }
    }
}