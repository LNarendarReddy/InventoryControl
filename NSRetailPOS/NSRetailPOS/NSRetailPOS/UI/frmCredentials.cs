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
    public partial class frmCredentials : XtraForm
    {
        public bool IsLoginSuccess { get; private set; }

        public frmCredentials()
        {
            InitializeComponent();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if(!dxValidationProvider1.Validate()) return;

            DataSet dSUserInfo = new POSRepository().GetUserInfo(Utility.branchInfo.BranchID,
                    Utility.branchInfo.BranchCounterID, txtUserName.EditValue, Utility.Encrypt(Convert.ToString(txtPassword.EditValue))
                    , Utility.GetHDDSerialNumber());

            if(dSUserInfo.Tables.Count == 0)
            {
                XtraMessageBox.Show("data not found, please contact administrator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(dSUserInfo.Tables.Count == 1 && dSUserInfo.Tables[0].Columns.Count == 1)
            {
                XtraMessageBox.Show(dSUserInfo.Tables[0].Rows[0][0].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if(dSUserInfo.Tables[0].Rows[0]["ROLENAME"].ToString() != "Store Manager")
            {
                XtraMessageBox.Show("Entered user is not a store manager", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            IsLoginSuccess = true;
            this.Close();
        }
    }
}
