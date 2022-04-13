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
using NSRetailPOS.Data;

namespace NSRetailPOS.UI
{
    public partial class frmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        POSRepository objPOSRep = new POSRepository();
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
                if (Utility.Encrypt(txtOPassword.Text) != Convert.ToString(Utility.loginInfo.Password))
                    throw new Exception("Invalid Old Password");
                if(txtNPassword.Text != txtcPassword.Text)
                    throw new Exception("Both Passwords Should be same");
                objPOSRep.ChangePassword(Utility.loginInfo.UserID, Utility.Encrypt(txtNPassword.Text));
                Application.Restart();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}