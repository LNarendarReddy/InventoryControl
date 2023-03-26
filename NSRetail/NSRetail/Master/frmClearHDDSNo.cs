using DevExpress.XtraEditors;
using Entity;
using System;
using System.Windows.Forms;

namespace NSRetail.Master
{
    public partial class frmClearHDDSNo : XtraForm
    {
        object counterID;

        public frmClearHDDSNo(object branchName, object counter, object counterID)
        {
            InitializeComponent();
            txtBranchName.EditValue = branchName;
            txtCounterName.EditValue = counter;
            this.counterID = counterID;
        }

        private void btnClearHDDSNo_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate() || 
                XtraMessageBox.Show("Are you sure you want to clear HDD Serial #?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Counter counter = new Counter() { COUNTERID = counterID, UserID = Utility.UserID, Description = txtReason.EditValue };
            try
            {
                new DataAccess.MasterRepository().ClearCounterHDDSNo(counter);
                XtraMessageBox.Show("HDD SNo cleared successfully");
                Close();
            }
            catch(Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}