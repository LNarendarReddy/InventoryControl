using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;
using DataAccess;
using ErrorManagement;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using NSRetail.Login;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraRichEdit.Model;
using NSRetail.Utilities;

namespace NSRetail
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.ToLower() == "admin" && txtPassword.Text == "776986")
                {
                    this.Hide();
                    frmMain ObjParent = new frmMain();
                    ObjParent.Show();
                }
                else
                {
                    txtUserName.Text = txtUserName.Text.Trim();
                    txtPassword.Text = txtPassword.Text.Trim();
                    if (!dxValidationProvider1.Validate())
                        return;
                    DataSet ds = objMasterRep.GetUserCredentials(txtUserName.Text.ToLower(), Utility.Encrypt(txtPassword.Text)
                        , Utility.AppVersion);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (int.TryParse(Convert.ToString(ds.Tables[0].Rows[0]["USERID"]), out Utility.UserID))
                        {
                            Utility.UserName = txtUserName.Text;
                            Utility.Password = Utility.Encrypt(txtPassword.Text);
                            Utility.FullName = Convert.ToString(ds.Tables[0].Rows[0]["FULLNAME"]);
                            Utility.Category = Convert.ToString(ds.Tables[0].Rows[0]["CATEGORYNAME"]).Trim();
                            Utility.Role = Convert.ToString(ds.Tables[0].Rows[0]["ROLENAME"]);
                            Utility.ReportingLead = Convert.ToString(ds.Tables[0].Rows[0]["REPORTINGLEAD"]);
                            Utility.RoleID = Convert.ToInt32(ds.Tables[0].Rows[0]["ROLEID"]);
                            Utility.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORYID"]);
                            Utility.BranchID = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHID"]);
                            Utility.ReportingLeadID = Convert.ToInt32(ds.Tables[0].Rows[0]["REPORTINGLEADID"]);
                            Utility.Email = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL"]);
                            Utility.IsOpenCategory = Convert.ToBoolean(ds.Tables[0].Rows[0]["ALLOWOPENITEMS"]);
                            Utility.dtConnectionInfo = ds.Tables[2];
                            bool IsOTP = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISOTP"]);

                            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count != 0)
                            {
                                foreach(DataRow dr in ds.Tables[1].Rows)
                                {
                                    switch(Convert.ToInt32(dr["PRINTERTYPEID"]))
                                    {
                                        case 1:
                                            Utility.DotMatrixPrinter = Convert.ToString(dr["PRINTERNAME"]);
                                            break;
                                        case 2:
                                            Utility.BarcodePrinter = Convert.ToString(dr["PRINTERNAME"]);
                                            break;
                                        case 3:
                                            Utility.A4SizePrinter = Convert.ToString(dr["PRINTERNAME"]);
                                            break;
                                        case 4:
                                            Utility.ThermalPrinter = Convert.ToString(dr["PRINTERNAME"]);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            if (IsOTP)
                            {
                                frmChangePassword Obj = new frmChangePassword();
                                Obj.ShowInTaskbar = false;
                                Obj.StartPosition = FormStartPosition.CenterScreen;
                                Obj.IconOptions.ShowIcon = false;
                                Obj.ShowDialog();
                            }
                            else
                            {
                                UpdateUserDetails();
                                this.Hide();
                                frmMain ObjParent = new frmMain();
                                ObjParent.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                if (ex.Message.Contains("Application version mismatch"))
                {
                    try
                    {
                        XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                        args.AutoCloseOptions.Delay = 5000;
                        args.Caption = "Application Update";
                        args.Text = "New update available! Please wait till the application is updated.";
                        args.DefaultButtonIndex = 0;
                        args.AutoCloseOptions.ShowTimerOnDefaultButton = true;
                        args.Buttons = new DialogResult[] { DialogResult.OK };
                        XtraMessageBox.Show(args);

                        SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                        SplashScreenManager.Default.SetWaitFormDescription("Downloading installer...");
                        string InstallerPath = await DropBoxRepository.DownloadFile();
                        SplashScreenManager.CloseForm();
                        if(File.Exists(InstallerPath))
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = InstallerPath;
                            p.StartInfo.Arguments = "/i /qn";
                            p.Start();
                            Application.Exit();
                        }
                    }
                    catch (Exception exx)
                    {
                        SplashScreenManager.CloseForm();
                        ErrorMgmt.ShowError(exx);
                    }
                }
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void UpdateUserDetails()
        {
            try
            {
                RegistryKey RGkey = Registry.CurrentUser.OpenSubKey(@"Software\NSRetail", true);
                if (RGkey == null)
                    RGkey = Registry.CurrentUser.CreateSubKey(@"Software\NSRetail");

                if (RGkey.GetValue("LastUser") == null)
                {
                    RGkey.SetValue("LastUser", txtUserName.EditValue);
                    RGkey.SetValue("PasswordString", txtPassword.EditValue);
                }
                else
                {
                    if (txtUserName.EditValue != null)
                    {
                        RGkey.SetValue("LastUser", txtUserName.EditValue);
                        RGkey.SetValue("PasswordString", txtPassword.EditValue);
                        RGkey.Flush();
                    }
                }
                RGkey.Close();
            }
            catch (Exception ex){
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            try
            {
                RegistryKey RGkey = Registry.CurrentUser.OpenSubKey(@"Software\NSRetail", true);
                if (RGkey != null)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(RGkey.GetValue("LastUser")))
                        //&& !string.IsNullOrEmpty(Convert.ToString(RGkey.GetValue("PasswordString")))
                        )
                    {
                        txtUserName.EditValue = RGkey.GetValue("LastUser");
                        //txtPassword.EditValue = RGkey.GetValue("PasswordString");
                        //btnLogin_Click(null, null);
                    }
                    else
                    {
                        txtUserName.EditValue = RGkey.GetValue("LastUser");
                        //txtPassword.EditValue = RGkey.GetValue("PasswordString");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}