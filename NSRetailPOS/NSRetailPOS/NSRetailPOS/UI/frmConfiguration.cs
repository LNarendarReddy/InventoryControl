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
            Utility.ActiveForm = this;
            if (!Utility.ValidateTimeZone())
            {
                XtraMessageBox.Show($"This system installed in different time zone!" +
                    $"{Environment.NewLine}Please correct the timezone to continue or contact your administrator.");
                this.Close();
            }
            else
            {
                try
                {
                    DataTable dt = ObjPOSRep.GetBranchList(Utility.AppVersion, objSyncRepository.GetDBVersion());
                    cmbBranch.Properties.DataSource = dt;
                    cmbBranch.Properties.ValueMember = "BRANCHID";
                    cmbBranch.Properties.DisplayMember = "BRANCHNAME";


                    if (!System.IO.Directory.Exists(System.IO.Path.Combine(Application.UserAppDataPath, "DBFiles")))
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Application.UserAppDataPath, "DBFiles"));
                    if (!System.IO.Directory.Exists(System.IO.Path.Combine(Application.UserAppDataPath, "AppFiles")))
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Application.UserAppDataPath, "AppFiles"));
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                Utility.branchInfo.BranchCounterID = cmbCounter.EditValue;
                Utility.branchInfo.BranchID = cmbBranch.EditValue;
                string HDDSno = Utility.GetHDDSerialNumber();
                objCloudRepository.CheckOrAddHDDSerialNumber(Utility.branchInfo.BranchCounterID, HDDSno);
                if(!await Utility.StartSync(true, true))
                {
                    Application.Exit();
                    return;
                }
                objSyncRepository.SaveHDDSNo(HDDSno);
                DataSet dsRestoreData = objCloudRepository.GetDaySequence(cmbCounter.EditValue);
                objSyncRepository.ImportDaySequence(dsRestoreData);

                DataRowView drSelectedCounter = cmbCounter.GetSelectedDataRow() as DataRowView;
                ObjPOSRep.SavePOSConfiguration(cmbBranch.EditValue, cmbCounter.EditValue
                    , drSelectedCounter["DAYCLOSUREID"], drSelectedCounter["BRANCHREFUNDID"]);

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

        private void cmbBranch_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            cmbCounter.Properties.DataSource = ObjPOSRep.GetCounterList(cmbBranch.EditValue);
            cmbCounter.Properties.ValueMember = "COUNTERID";
            cmbCounter.Properties.DisplayMember = "COUNTERNAME";
        }
    }
}