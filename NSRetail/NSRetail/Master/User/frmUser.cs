using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;

namespace NSRetail
{
    public partial class frmUser : XtraForm
    {
        User ObjUser = null;
        UserRepository objUserRep = new UserRepository();
        public frmUser(User _ObjUser)
        {
            InitializeComponent();
            ObjUser = _ObjUser;
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            try
            {
                cmbRole.Properties.DataSource = objUserRep.GetRoles();
                cmbRole.Properties.DisplayMember = "ROLENAME";
                cmbRole.Properties.ValueMember = "ROLEID";

                cmbBranch.Properties.DataSource = Utility.GetBranchList();
                cmbBranch.Properties.DisplayMember = "BRANCHNAME";
                cmbBranch.Properties.ValueMember = "BRANCHID";

                cmbCategory.Properties.DataSource = Utility.GetCategoryList();
                cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
                cmbCategory.Properties.ValueMember = "CATEGORYID";

                cmbReportingLead.Properties.DataSource = objUserRep.GetUser();
                cmbReportingLead.Properties.DisplayMember = "USERNAME";
                cmbReportingLead.Properties.ValueMember = "USERID";

                if (Convert.ToInt32(ObjUser.USERID) > 0)
                {
                    this.Text = "Edit User";
                    txtUserName.EditValue = ObjUser.USERNAME;
                    txtFullName.EditValue = ObjUser.FULLNAME;
                    txtPhoneNumber.EditValue = ObjUser.CNUMBER;
                    txtEmail.EditValue = ObjUser.EMAIL;
                    cmbRole.EditValue = ObjUser.ROLEID;
                    cmbReportingLead.EditValue = ObjUser.REPORTINGLEADID;
                    cmbBranch.EditValue = ObjUser.BRANCHID;
                    cmbCategory.EditValue = ObjUser.CATEGORYID;
                    rgGenderr.SelectedIndex = Convert.ToInt32(ObjUser.GENDER);
                    cmbSubcategory.EditValue = ObjUser.SUBCATEGORYID;
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjUser.USERNAME = txtUserName.EditValue;
                ObjUser.FULLNAME =  txtFullName.EditValue;
                ObjUser.CNUMBER = txtPhoneNumber.EditValue;
                ObjUser.EMAIL = txtEmail.EditValue;
                ObjUser.GENDER = rgGenderr.EditValue;
                ObjUser.PASSWORDSTRING = Utility.Encrypt("Password@1234");
                ObjUser.BRANCHID = cmbBranch.EditValue;
                ObjUser.ROLEID = cmbRole.EditValue;
                ObjUser.CATEGORYID = cmbCategory.EditValue;
                ObjUser.REPORTINGLEADID = cmbReportingLead.EditValue;
                ObjUser.CUSERID = Utility.UserID;
                ObjUser.SUBCATEGORYID = cmbSubcategory.EditValue;
                objUserRep.SaveUser(ObjUser);
                ObjUser.IsSave = true;
                this.Close();

            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjUser.IsSave = false;
            this.Close();
        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            cmbSubcategory.Properties.DataSource = new MasterRepository().GetSubCategory(cmbCategory.EditValue);
            cmbSubcategory.Properties.DisplayMember = "SUBCATEGORYNAME";
            cmbSubcategory.Properties.ValueMember = "SUBCATEGORYID";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cmbSubcategory.EditValue = null;
        }
    }
}