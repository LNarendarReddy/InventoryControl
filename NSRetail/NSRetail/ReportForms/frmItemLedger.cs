using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms
{
    public partial class frmItemLedger : DevExpress.XtraEditors.XtraForm
    {
        public frmItemLedger()
        {
            InitializeComponent();
        }

        private void frmItemLedger_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            chkIncludeBranch.Checked = false;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);

            gcBrance.Visible = (bool)chkIncludeBranch.EditValue;

            Dictionary<string, object> searchCriteria = new Dictionary<string, object>()
            {
                { "BranchID",  cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "ItemCodeID", sluItemCode.EditValue }
                , { "IncludeBranch", chkIncludeBranch.EditValue }
            };
            DataSet dsResult = new ReportRepository().GetReportDataset("USP_RPT_ITEMLEDGER", searchCriteria);
            gcItems.DataSource = dsResult.Tables[0];
            SplashScreenManager.CloseOverlayForm(handle);
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }
    }
}