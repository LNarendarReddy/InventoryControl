using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            var timer = Stopwatch.StartNew();
            string outcome = "Failed";
            Log.Information("POS login started for branch {BranchID}, counter {CounterID}",
                Utility.branchInfo.BranchID, Utility.branchInfo.BranchCounterID);
            try
            {
                if (!LoginTiming.Measure("Validate login form", () => dxValidationProvider1.Validate()))
                {
                    outcome = "Validation rejected";
                    return;
                }
                string diskSerial = LoginTiming.Measure("Read disk serial", Utility.GetHDDSerialNumber);
                DataSet dSUserInfo = LoginTiming.Measure("Validate credentials in local POS database", () =>
                    objPOSRep.GetUserInfo(Utility.branchInfo.BranchID, Utility.branchInfo.BranchCounterID,
                        txtUserName.EditValue, Utility.Encrypt(Convert.ToString(txtPassword.EditValue)), diskSerial));

                if (dSUserInfo == null || dSUserInfo.Tables.Count == 0 || dSUserInfo.Tables[0].Rows.Count == 0)
                {
                    outcome = "No credentials result";
                    return;
                }

                if (!int.TryParse(Convert.ToString(dSUserInfo.Tables[0].Rows[0][0]), out int IValue))
                    throw new Exception(Convert.ToString(dSUserInfo.Tables[0].Rows[0][0]));

                try
                {
                    new ReportRepository().ExecuteNonQuery("USP_C_LOGINHISTORY", true, new Dictionary<string, object>()
                    {
                        { "UserID", IValue }, { "ApplicationID", 24 }
                    }, commandTimeOut: 5, logLoginTiming: true);
                }
                catch { } // Do nothing in case of no connection

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

                var paymentModes = LoginTiming.Measure("Read counter payment modes from local POS database",
                    () => new BillingRepository().GetCounterPaymentModes());
                LoginTiming.Measure("Load counter payment modes", () => Utility.CounterPayments.Load(paymentModes));

                if (ISOTP)
                {
                    frmChangePassword Obj = new frmChangePassword();
                    Obj.ShowInTaskbar = false;
                    Obj.StartPosition = FormStartPosition.CenterScreen;
                    Obj.IconOptions.ShowIcon = false;
                    outcome = "Password change required";
                    Obj.ShowDialog();
                }
                else
                {
                    var billingForm = LoginTiming.Measure("Get billing window", () => frmMain.Instance);
                    LoginTiming.Measure("Show billing window", () => billingForm.Show());
                    outcome = "Billing window shown";
                    txtPassword.Text = string.Empty;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Log.Warning("POS login stopped after {ElapsedMs} ms ({ErrorType})", timer.ElapsedMilliseconds, ex.GetType().Name);
                Utility.ShowError(ex);
            }
            finally
            {
                Log.Information("POS login ended after {ElapsedMs} ms with outcome {Outcome}", timer.ElapsedMilliseconds, outcome);
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
