using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
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
                DataSet dSUserInfo = objPOSRep.GetUserInfo(Utility.branchinfo.BranchID,
                    Utility.branchinfo.BranchCounterID,txtUserName.EditValue, Utility.Encrypt(Convert.ToString(txtPassword.EditValue)));
                if(dSUserInfo != null && dSUserInfo.Tables.Count > 0 &&
                    dSUserInfo.Tables[0].Rows.Count > 0)
                {
                    int IValue = 0;
                    if (int.TryParse(Convert.ToString(dSUserInfo.Tables[0].Rows[0][0]), out IValue))
                    {
                        Utility.logininfo.UserID = IValue;
                        Utility.logininfo.UserName = dSUserInfo.Tables[0].Rows[0]["USERNAME"];
                        Utility.logininfo.Password = dSUserInfo.Tables[0].Rows[0]["PASSWORDSTRING"];
                        Utility.logininfo.UserFullName = dSUserInfo.Tables[0].Rows[0]["FULLNAME"];
                        Utility.branchinfo.BranchID = dSUserInfo.Tables[0].Rows[0]["BRANCHID"];
                        Utility.branchinfo.BranchName = dSUserInfo.Tables[0].Rows[0]["BRANCHNAME"];
                        Utility.branchinfo.BranchCode = dSUserInfo.Tables[0].Rows[0]["BRANCHCODE"];
                        Utility.branchinfo.BranchAddress = dSUserInfo.Tables[0].Rows[0]["ADDRESS"];
                        Utility.branchinfo.PhoneNumber = dSUserInfo.Tables[0].Rows[0]["PHONENO"];
                        Utility.branchinfo.LandLine = dSUserInfo.Tables[0].Rows[0]["LANDLINE"];
                        Utility.branchinfo.BranchCounterID = dSUserInfo.Tables[1].Rows[0]["COUNTERID"];
                        Utility.branchinfo.BranchCounterName = dSUserInfo.Tables[1].Rows[0]["COUNTERNAME"];
                        bool ISOTP = Convert.ToBoolean(dSUserInfo.Tables[0].Rows[0]["ISOTP"]);
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
    }
}