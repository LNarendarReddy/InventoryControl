using DataAccess;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class frmItemLedger : DevExpress.XtraEditors.XtraForm
    {
        ReportRepository reportRepository = new ReportRepository();
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
            chkIncludeItem.Checked = false;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            try
            {
                if (!dxValidationProvider1.Validate()) return;

                gcBrance.Visible = (bool)chkIncludeBranch.EditValue;

                Dictionary<string, object> searchCriteria = new Dictionary<string, object>()
            {
                { "BranchID",  cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "ItemID", sluSKUCode.EditValue }
                , { "IncludeBranch", chkIncludeBranch.EditValue }
                , { "IncludeItem", chkIncludeItem.EditValue }
            };
                DataSet dsResult = reportRepository.GetReportDataset("USP_RPT_ITEMLEDGER1", searchCriteria);
                gcItems.DataSource = dsResult.Tables[0];
                DataTable dt = reportRepository.GetStockSummaryByID(sluSKUCode.EditValue, cmbBranch.EditValue);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtQuantity.EditValue = dt.Rows[0][0];
                    txtWeightinKGs.EditValue = dt.Rows[0][1];
                }
                else
                {
                    txtQuantity.EditValue = null;
                    txtWeightinKGs.EditValue = null;
                }
                gcSKUCode.Visible = Convert.ToBoolean(chkIncludeItem.CheckState);
                gcItemName.Visible = Convert.ToBoolean(chkIncludeItem.CheckState);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseOverlayForm(handle);
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
            SplashScreenManager.CloseOverlayForm(handle);
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void cmbBranch_EditValueChanged(object sender, EventArgs e)
        {
            if(cmbBranch.EditValue.Equals(97))
            {
                chkIncludeItem.Enabled = true;
            }
            else
            {
                chkIncludeItem.Checked = false;
                chkIncludeItem.Enabled = false;
            }
        }
    }
}