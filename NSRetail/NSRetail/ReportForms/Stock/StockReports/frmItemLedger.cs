using DataAccess;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

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
            if (!dxValidationProvider1.Validate()) return;
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            gcBrance.Visible = (bool)chkIncludeBranch.EditValue;
            gcItems.DataSource = null;
            txtQuantity.EditValue = null;
                        
            this.peMatched.EditValue = Properties.Resources.warning_16x16;

            try
            {
                Dictionary<string, object> searchCriteria = new Dictionary<string, object>()
                {
                    { "BranchID",  cmbBranch.EditValue }
                    , { "FromDate", dtFromDate.EditValue }
                    , { "ToDate", dtToDate.EditValue }
                    , { "ItemID", sluSKUCode.EditValue }
                    , { "IncludeBranch", chkIncludeBranch.EditValue }
                    , { "IncludeItem", chkIncludeItem.EditValue }
                };
                
                DataTable dt = reportRepository.GetStockSummaryByID(sluSKUCode.EditValue, cmbBranch.EditValue);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtQuantity.EditValue = Convert.ToDecimal(!Convert.ToBoolean(dt.Rows[0]["ISOPENITEM"].ToString()) ? dt.Rows[0][0] : dt.Rows[0][1]);
                }

                DataTable dtResult = reportRepository.GetReportData("USP_RPT_ITEMLEDGER1", searchCriteria);
                int lastRowHandle = 0;
                if (dtResult.Rows.Count > 0)
                {
                    lastRowHandle = dtResult.Rows.Count - 1;
                    if (lastRowHandle > 0)
                    {
                        if ((DateTime)dtResult.Rows[lastRowHandle]["TRANSACTIONDATE"] == DateTime.Now.Date
                            && dtResult.Rows[lastRowHandle]["TRANSACTIONTYPE"].ToString() == "Sale")
                        {
                            lastRowHandle--;
                        }
                    }

                    bool isMatched = txtQuantity.EditValue.Equals(Convert.ToDecimal(dtResult.Rows[lastRowHandle]["RUNNINGBAL"]));
                    this.peMatched.EditValue = isMatched ? Properties.Resources.apply_16x16 : Properties.Resources.cancel_16x16;
                    dtResult.Rows[lastRowHandle]["ISMATCHED"] = isMatched;
                }

                gcItems.DataSource = dtResult;
                gvItems.FocusedRowHandle = gvItems.GetRowHandle(lastRowHandle);

                gcSKUCode.Visible = Convert.ToBoolean(chkIncludeItem.CheckState);
                gcItemName.Visible = Convert.ToBoolean(chkIncludeItem.CheckState);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseOverlayForm(handle);
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
            finally 
            {
                SplashScreenManager.CloseOverlayForm(handle);
            }
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

        private void gvItems_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column != gcRunningBal || string.IsNullOrEmpty(gvItems.GetRowCellValue(e.RowHandle, "ISMATCHED").ToString())) return;

            bool isMatched = Convert.ToBoolean(gvItems.GetRowCellValue(e.RowHandle, "ISMATCHED"));

            e.Appearance.ForeColor = isMatched ? Color.Green : Color.Red;
            e.Appearance.Font = new Font("Arial", 12, FontStyle.Bold);
            //e.Handled = true;
        }
    }
}