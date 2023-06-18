using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmLogin : XtraForm
    {
        POSRepository objPOSRep = new POSRepository();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                DataSet dSUserInfo = objPOSRep.GetUserInfo(Utility.branchInfo.BranchID,
                    Utility.branchInfo.BranchCounterID,txtUserName.EditValue, Utility.Encrypt(Convert.ToString(txtPassword.EditValue))
                    , Utility.GetHDDSerialNumber());
                if(dSUserInfo != null && dSUserInfo.Tables.Count > 0 &&
                    dSUserInfo.Tables[0].Rows.Count > 0)
                {
                    int IValue = 0;
                    if (int.TryParse(Convert.ToString(dSUserInfo.Tables[0].Rows[0][0]), out IValue))
                    {
                        Utility.loginInfo.UserID = IValue;
                        Utility.loginInfo.UserName = dSUserInfo.Tables[0].Rows[0]["USERNAME"];
                        Utility.loginInfo.Password = dSUserInfo.Tables[0].Rows[0]["PASSWORDSTRING"];
                        Utility.loginInfo.UserFullName = dSUserInfo.Tables[0].Rows[0]["FULLNAME"];
                        Utility.loginInfo.RoleName = dSUserInfo.Tables[0].Rows[0]["ROLENAME"];
                        Utility.branchInfo.BranchID = dSUserInfo.Tables[0].Rows[0]["BRANCHID"];
                        Utility.branchInfo.BranchName = dSUserInfo.Tables[0].Rows[0]["BRANCHNAME"];
                        Utility.branchInfo.BranchCode = dSUserInfo.Tables[0].Rows[0]["BRANCHCODE"];
                        Utility.branchInfo.BranchAddress = dSUserInfo.Tables[0].Rows[0]["ADDRESS"];
                        Utility.branchInfo.PhoneNumber = dSUserInfo.Tables[0].Rows[0]["PHONENO"];
                        Utility.branchInfo.LandLine = dSUserInfo.Tables[0].Rows[0]["LANDLINE"];
                        Utility.branchInfo.BranchCounterID = dSUserInfo.Tables[1].Rows[0]["COUNTERID"];
                        Utility.branchInfo.BranchCounterName = dSUserInfo.Tables[1].Rows[0]["COUNTERNAME"];
                        Utility.branchInfo.MultiEditThreshold =
                            dSUserInfo.Tables[0].Rows[0]["MULTIEDITTHRESHOLD"] != null && 
                            int.TryParse(dSUserInfo.Tables[0].Rows[0]["MULTIEDITTHRESHOLD"].ToString(), out int multiEditThreshold) 
                            ? multiEditThreshold : 0;

                        // for now force to 10 rs
                        Utility.branchInfo.MultiEditThreshold = 10;

                        bool ISOTP = Convert.ToBoolean(dSUserInfo.Tables[0].Rows[0]["ISOTP"]);
                        Utility.DBVersion = Convert.ToString(dSUserInfo.Tables[0].Rows[0]["DBVersion"]);
                        if (ISOTP)
                        {
                            frmChangePassword Obj = new frmChangePassword();
                            Obj.ShowInTaskbar = false;
                            Obj.StartPosition = FormStartPosition.CenterScreen;
                            Obj.IconOptions.ShowIcon = false;
                            Obj.ShowDialog();
                        }
                        else
                        {
                            new frmMain().Show();
                            this.Hide();
                        }
                    }
                    else
                        throw new Exception(Convert.ToString(dSUserInfo.Tables[0].Rows[0][0]));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFullSync_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
            if(!Utility.StartSync(null,true))Application.Exit();
            SplashScreenManager.CloseForm();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Utility.ActiveForm = this;
            lblBranch.Text = Convert.ToString(Utility.branchInfo.BranchName);
            lblCounter.Text = $"Counter : {Convert.ToString(Utility.branchInfo.BranchCounterName)}";
        }
    }
}