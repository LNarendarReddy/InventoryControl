using System;
using DevExpress.XtraEditors;
using Entity;
using DataAccess;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmBranch : XtraForm
    {
        Branch ObjBranch = null;
        MasterRepository objMasterRep = new MasterRepository();
        UserRepository objUserRep = new UserRepository();
        public frmBranch(Branch _ObjBranch)
        {
            InitializeComponent();
            ObjBranch = _ObjBranch;
        }

        private void frmAddBranch_Load(object sender, EventArgs e)
        {
            cmbState.Properties.DataSource = objMasterRep.GetStates();
            cmbState.Properties.ValueMember = "STATEID";
            cmbState.Properties.DisplayMember = "STATENAME";

            cmbSupervisor.Properties.DataSource = objUserRep.GetUser();
            cmbSupervisor.Properties.ValueMember = "USERID";
            cmbSupervisor.Properties.DisplayMember = "USERNAME";
            if (Convert.ToInt32(ObjBranch.BRANCHID) > 0)
            {
                this.Text = "Edit Branch";
                txtBranchCode.EditValue = ObjBranch.BRANCHCODE;
                txtBranchName.EditValue = ObjBranch.BRANCHNAME;
                txtAddress.EditValue = ObjBranch.ADDRESS;
                txtPhoneNo.EditValue = ObjBranch.PHONENO;
                txtLandLine.EditValue = ObjBranch.LANDLINE;
                txtEmailID.EditValue = ObjBranch.EMAILID;
                cmbSupervisor.EditValue = ObjBranch.SUPERVISERID;
                cmbState.EditValue = ObjBranch.STATEID;
                if (Convert.ToBoolean(ObjBranch.ISWAREHOUSE))
                    chkIsWarehouse.Checked = true;
                else
                    chkIsWarehouse.Checked = false;
                chkIsWarehouse.Enabled = false;
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
                ObjBranch.ADDRESS = txtAddress.EditValue;
                ObjBranch.STATEID = cmbState.EditValue;
                ObjBranch.PHONENO = txtPhoneNo.EditValue;
                ObjBranch.LANDLINE = txtLandLine.EditValue;
                ObjBranch.EMAILID = txtEmailID.EditValue;
                ObjBranch.ISWAREHOUSE = Convert.ToBoolean(chkIsWarehouse.CheckState);
                ObjBranch. SUPERVISERID = cmbSupervisor.EditValue;
                ObjBranch.UserID = Utility.UserID;
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