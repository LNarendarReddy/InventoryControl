using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using System;
using System.Data;
using System.Management;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmConfiguration : XtraForm
    {
        POSRepository ObjPOSRep = new POSRepository();
        CloudRepository objCloudRepository = new CloudRepository();
        SyncRepository objSyncRepository = new SyncRepository();

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
                DataRowView drSelectedCounter = cmbCounter.GetSelectedDataRow() as DataRowView;
                ObjPOSRep.SavePOSConfiguration(cmbBranch.EditValue, cmbCounter.EditValue
                    , drSelectedCounter["DAYCLOSUREID"], drSelectedCounter["BRANCHREFUNDID"]);

                SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                Utility.branchInfo.BranchCounterID = cmbCounter.EditValue;
                Utility.branchInfo.BranchID = cmbBranch.EditValue;
                string HDDSno = Utility.GetHDDSerialNumber();
                objCloudRepository.CheckOrAddHDDSerialNumber(Utility.branchInfo.BranchCounterID, HDDSno);
                Utility.StartSync(null, true);
                objSyncRepository.SaveHDDSNo(HDDSno);
                DataSet dsRestoreData = objCloudRepository.GetDaySequence(cmbCounter.EditValue);
                objSyncRepository.ImportDaySequence(dsRestoreData);

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