using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataAccess;
using Entity;
using ErrorManagement;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using NSRetail.Master.User;
using System.Web.Security;
using NSRetail.Utilities;

namespace NSRetail
{
    public partial class frmUserList : XtraForm
    {
        UserRepository objUserRep = new UserRepository();
        User ObjUser = null;
        public frmUserList()
        {
            InitializeComponent();
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            try
            {
                gcUser.DataSource = objUserRep.GetUser();
                gvUser.Columns["USERSTATUS"].FilterInfo = new ColumnFilterInfo("USERSTATUS = 'ACTIVE'");

                AccessUtility.SetStatusByAccess(btnNew);
                AccessUtility.SetStatusByAccess(gcEdit, gcDelete, gcEditAccess);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            ObjUser = new User();
            frmUser obj = new frmUser(ObjUser);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
            if (ObjUser.IsSave)
            {
                gcUser.DataSource = objUserRep.GetUser();
                Utility.Setfocus(gvUser, "USERID", ObjUser.USERID);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvUser.FocusedRowHandle >= 0)
                {
                    ObjUser = new User();
                    ObjUser.USERID = gvUser.GetFocusedRowCellValue("USERID");
                    ObjUser.ROLEID = gvUser.GetFocusedRowCellValue("ROLEID");
                    ObjUser.REPORTINGLEADID = gvUser.GetFocusedRowCellValue("REPORTINGLEADID");
                    ObjUser.CATEGORYID = gvUser.GetFocusedRowCellValue("CATEGORYID");
                    ObjUser.BRANCHID = gvUser.GetFocusedRowCellValue("BRANCHID");
                    ObjUser.USERNAME = gvUser.GetFocusedRowCellValue("USERNAME");
                    ObjUser.FULLNAME = gvUser.GetFocusedRowCellValue("FULLNAME");
                    ObjUser.CNUMBER = gvUser.GetFocusedRowCellValue("CNUMBER");
                    ObjUser.EMAIL = gvUser.GetFocusedRowCellValue("EMAIL");
                    ObjUser.GENDER = gvUser.GetFocusedRowCellValue("GENDER");
                    frmUser obj = new frmUser(ObjUser);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (ObjUser.IsSave)
                    {
                        gcUser.DataSource = objUserRep. GetUser();
                        Utility.Setfocus(gvUser, "USERID", ObjUser.USERID);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvUser.FocusedRowHandle < 0 || XtraMessageBox.Show("Are you sure want to delete?", "Confirm!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                ObjUser = new User();
                ObjUser.USERID = gvUser.GetFocusedRowCellValue("USERID");
                ObjUser.CUSERID = Utility.UserID;
                objUserRep.DeleteUser(ObjUser);
                gvUser.SetFocusedRowCellValue("USERSTATUS", "IN-ACTIVE");
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcUser.ShowRibbonPrintPreview();
        }
        private void gvUser_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Add(new DXMenuItem("Reset Password", new EventHandler(OnResetPassword_Click)));
        }
        void OnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvUser.FocusedRowHandle >= 0)
                {
                    new UserRepository().ResetPassword(gvUser.GetFocusedRowCellValue("USERID"),
                        Utility.Encrypt("Password@1234"));
                    XtraMessageBox.Show($"Password reset successfully done {Environment.NewLine}Please login with 'Password@1234'");
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnEditAccess_Click(object sender, EventArgs e)
        {
            new frmAccessList(null, gvUser.GetFocusedRowCellValue("USERID"), $"Access permissions for User : {gvUser.GetFocusedRowCellValue("USERNAME")}").ShowDialog();
        }
    }
}