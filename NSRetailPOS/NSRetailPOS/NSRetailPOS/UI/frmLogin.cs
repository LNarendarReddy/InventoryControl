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

        private static frmLogin instance;

        public static frmLogin Instance
        {
            get
            {
                return instance = instance ?? new frmLogin();
            }
        }

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
                        Utility.loginInfo.CategoryID = dSUserInfo.Tables[0].Rows[0]["CATEGORYID"];
                        Utility.branchInfo.BranchID = dSUserInfo.Tables[0].Rows[0]["BRANCHID"];
                        Utility.branchInfo.BranchName = dSUserInfo.Tables[0].Rows[0]["BRANCHNAME"];
                        Utility.branchInfo.BranchCode = dSUserInfo.Tables[0].Rows[0]["BRANCHCODE"];
                        Utility.branchInfo.BranchAddress = dSUserInfo.Tables[0].Rows[0]["ADDRESS"];
                        Utility.branchInfo.PhoneNumber = dSUserInfo.Tables[0].Rows[0]["PHONENO"];
                        Utility.branchInfo.LandLine = dSUserInfo.Tables[0].Rows[0]["LANDLINE"];
                        Utility.branchInfo.BranchCounterID = dSUserInfo.Tables[1].Rows[0]["COUNTERID"];
                        Utility.branchInfo.BranchCounterName = dSUserInfo.Tables[1].Rows[0]["COUNTERNAME"];
                        Utility.branchInfo.MultiEditThreshold =
                            !Utility.loginInfo.RoleName.Equals("Store Admin") &&
                            dSUserInfo.Tables[0].Rows[0]["MULTIEDITTHRESHOLD"] != null && 
                            int.TryParse(dSUserInfo.Tables[0].Rows[0]["MULTIEDITTHRESHOLD"].ToString(), out int multiEditThreshold) 
                            ? multiEditThreshold : 0;
                        bool ISOTP = Convert.ToBoolean(dSUserInfo.Tables[0].Rows[0]["ISOTP"]);
                        Utility.DBVersion = Convert.ToString(dSUserInfo.Tables[0].Rows[0]["DBVersion"]);
                        Utility.branchInfo.FilterMRPByStock = dSUserInfo.Tables[0].Rows[0]["FILTERMRPBYSTOCK"] != DBNull.Value 
                            && bool.Parse(dSUserInfo.Tables[0].Rows[0]["FILTERMRPBYSTOCK"].ToString());
                        Utility.branchInfo.EnableDraftBills = int.Parse(dSUserInfo.Tables[0].Rows[0]["ENABLEDRAFTBILLS"].ToString());
                        GatewayInfo.ClientID = Convert.ToString(dSUserInfo.Tables[1].Rows[0]["CLIENTID"]);
                        GatewayInfo. MerchantID = Convert.ToString(dSUserInfo.Tables[1].Rows[0]["MERCHANTID"]);
                        GatewayInfo.StoreID = Convert.ToString(dSUserInfo.Tables[1].Rows[0]["STOREID"]);
                        GatewayInfo.SecurityToken = Convert.ToString(dSUserInfo.Tables[1].Rows[0]["SECURITYTOKEN"]);
                        GatewayInfo.AutoCancelDurationInMinutes = int.Parse(dSUserInfo.Tables[1].Rows[0]["CANCELATIONDURATION"].ToString());
                        GatewayInfo.PaymentUrl = Convert.ToString(dSUserInfo.Tables[1].Rows[0]["PAYMENTURL"]);
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
                            frmMain.Instance.Show();
                            txtPassword.Text = string.Empty;
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

        private async void btnFullSync_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
            if(!await Utility.StartSync(true, true))Application.Exit();
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