using DataAccess;
using DevExpress.XtraEditors;
using Entity;
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
    public partial class frmAddBranchPrice : DevExpress.XtraEditors.XtraForm
    {
        BranchItemPrice branchItemPrice = null;
        public frmAddBranchPrice(BranchItemPrice  _branchItemPrice)
        {
            InitializeComponent();
            branchItemPrice = _branchItemPrice;

            DataTable dtBranch = Utility.GetBranchList();
            DataView dvBranch = dtBranch.Copy().DefaultView;
            dvBranch.RowFilter = $"ISWAREHOUSE = 0";
            cmbBranch.Properties.DataSource = dvBranch;
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