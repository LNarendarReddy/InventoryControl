using Entity;
using System;

namespace NSRetail
{
    public partial class frmAddBranchPrice : DevExpress.XtraEditors.XtraForm
    {
        BranchItemPrice branchItemPrice = null;
        public frmAddBranchPrice(BranchItemPrice  _branchItemPrice)
        {
            InitializeComponent();
            branchItemPrice = _branchItemPrice;

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!dxValidationProvider1.Validate())
            return; 
            branchItemPrice.BRANCHID = cmbBranch.EditValue;
            branchItemPrice.SALEPRICE = txtSalePrice.EditValue;
            branchItemPrice.BRANCHNAME = cmbBranch.Text;
            branchItemPrice.IsSave = true;
            this.Close();
        }
    }
}