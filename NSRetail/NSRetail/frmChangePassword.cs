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
using Microsoft.Win32;
using DataAccess;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        User ObjUser = new User();
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                txtOPassword.Text = txtOPassword.Text.Trim();
                txtNPassword.Text = txtNPassword.Text.Trim();
                txtcPassword.Text = txtcPassword.Text.Trim();
                if (!dxValidationProvider1.Validate())
                    return;
                if (Utility.Encrypt(txtOPassword.Text) != Utility.Password)
                    throw new Exception("Invalid Old Password");
                if(txtNPassword.Text != txtcPassword.Text)
                    throw new Exception("Both Passwords Should be same");
                ObjUser.USERID = Utility.UserID;
                ObjUser.PASSWORDSTRING = Utility.Encrypt(txtNPassword.Text);
                DataTable dt = ObjMasterRep.ChangePassword(ObjUser);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (int.TryParse(Convert.ToString(dt.Rows[0]["USERID"]), out Utility.UserID))
                    {
                        Utility.Password = Convert.ToString(dt.Rows[0]["Passwordstring"]);
                        RegistryKey RGkey = Registry.CurrentUser.OpenSubKey(@"Software\NSRetail", true);
                        if (RGkey == null)
                            RGkey = Registry.CurrentUser.CreateSubKey(@"Software\NSRetail");
                        if (RGkey.GetValue("PasswordString") == null)
                            RGkey.SetValue("PasswordString", Utility.Password);
                        else
                        {
                            RGkey.SetValue("PasswordString", Utility.Password);
                            RGkey.Flush();
                        }
                        RGkey.Close();
                    }
                }
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