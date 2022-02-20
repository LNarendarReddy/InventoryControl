using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
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
    public partial class frmConfiguration : DevExpress.XtraEditors.XtraForm
    {
        POSRepository ObjPOSRep = new POSRepository();
        public frmConfiguration()
        {
            InitializeComponent();
        }

        private void frmConfiguration_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = ObjPOSRep.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember  = "BRANCHNAME";

            cmbCounter.Properties.DataSource = ObjPOSRep. GetCounterList();
            cmbCounter.Properties.ValueMember = "COUNTERID";
            cmbCounter.Properties.DisplayMember = "COUNTERNAME";
            cmbCounter.CascadingOwner = cmbBranch;
            cmbCounter.Properties.CascadingMember = "BRANCHID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjPOSRep.SavePOSConfiguration(cmbBranch.EditValue, cmbCounter.EditValue);

                SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                Utility.branchinfo.BranchCounterID = cmbCounter.EditValue;
                Utility.branchinfo.BranchID = cmbBranch.EditValue;
                Utility.StartSync(null, true);
                SplashScreenManager.CloseForm();
                Application.Restart();
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {

        }

        private void btnAttachDatabase_Click(object sender, EventArgs e)
        {

        }
    }
}