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
    public partial class frmBranch : DevExpress.XtraEditors.XtraForm
    {
        Branch ObjBranch = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmBranch(Branch _ObjBranch)
        {
            InitializeComponent();
            ObjBranch = _ObjBranch;
        }

        private void frmAddBranch_Load(object sender, EventArgs e)
        {
            cmbSupervisor.Properties.DataSource = objMasterRep.GetUser();
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
                cmbSupervisor .EditValue = ObjBranch. SUPERVISERID;
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