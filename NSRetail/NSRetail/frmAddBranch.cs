using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entity;
using DataAccess;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmAddBranch : DevExpress.XtraEditors.XtraForm
    {
        Branch ObjBranch = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmAddBranch(Branch _ObjBranch)
        {
            InitializeComponent();
            ObjBranch = _ObjBranch;
        }

        private void frmAddBranch_Load(object sender, EventArgs e)
        {
            if(Convert.ToInt32(ObjBranch.BRANCHID) > 0)
            {
                this.Text = "Edit Branch";
                txtBranchCode.EditValue = ObjBranch.BRANCHCODE;
                txtBranchName.EditValue = ObjBranch.BRANCHNAME;
                txtAddress.EditValue = ObjBranch.ADDRESS;
                txtDescription.EditValue = ObjBranch.DESCRIPTION;
                txtPhoneNo.EditValue = ObjBranch.PHONENO;
                txtEmailID.EditValue = ObjBranch.EMAILID;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjBranch.IsSave = false;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjBranch.BRANCHCODE = txtBranchCode.EditValue;
                ObjBranch.BRANCHNAME = txtBranchName.EditValue;
                ObjBranch.DESCRIPTION = txtDescription.EditValue;
                ObjBranch.ADDRESS = txtAddress.EditValue;
                ObjBranch.PHONENO = txtPhoneNo.EditValue;
                ObjBranch.EMAILID = txtEmailID.EditValue;
                objMasterRep.SaveBranch(ObjBranch);
                ObjBranch.IsSave = true;
                this.Close();
                
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}