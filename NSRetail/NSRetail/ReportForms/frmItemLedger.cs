using DataAccess;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Data;

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
            sluSKUCode.Properties.DataSource = Utility.GetItemCodeList();
            sluSKUCode.Properties.ValueMember = "ITEMID";
            sluSKUCode.Properties.DisplayMember = "ITEMNAME";

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
                , { "ItemID", sluSKUCode.EditValue }
                , { "IncludeBranch", chkIncludeBranch.EditValue }
            };
            DataSet dsResult = new ReportRepository().GetReportDataset("USP_RPT_ITEMLEDGER1", searchCriteria);
            gcItems.DataSource = dsResult.Tables[0];
            SplashScreenManager.CloseOverlayForm(handle);
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }
    }
}