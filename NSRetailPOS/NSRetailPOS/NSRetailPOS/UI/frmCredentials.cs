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
                Utility.ShowErrorMessage("data not found, please contact administrator");
                return;
            }

            if(dSUserInfo.Tables.Count == 1 && dSUserInfo.Tables[0].Columns.Count == 1)
            {
                Utility.ShowErrorMessage(dSUserInfo.Tables[0].Rows[0][0].ToString());
                return;
            }

            if(dSUserInfo.Tables[0].Rows[0]["ROLENAME"].ToString() != "Store Manager")
            {
                Utility.ShowErrorMessage("Entered user is not a store manager");
                return;
            }

            IsLoginSuccess = true;
            this.Close();
        }
    }
}
