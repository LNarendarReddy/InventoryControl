using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DevExpress.Utils.Diagnostics.GUIResources;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class frmItemSummary : XtraForm
    {
        public frmItemSummary()
        {
            InitializeComponent();
        }

        private void frmItemSummary_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.ValueMember = "ITEMID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if(sluItemCode.EditValue == null)
            {
                XtraMessageBox.Show("Item code not set", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                sluItemCode.Focus();
                return;
            }

            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            try
            {
                gvItemSale.Columns["BILLNUMBER"].Visible = (bool)chkIncludeBillNumber.EditValue;

                Dictionary<string, object> searchCriteria = new Dictionary<string, object>()
            {
                { "BranchID",  cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "ItemID", sluItemCode.EditValue }
                , { "IncludeBillNumber", chkIncludeBillNumber.EditValue }
            };

                DataSet dsResult = new ReportRepository().GetReportDataset("USP_RPT_ITEMSUMMARY", searchCriteria);


                dsResult.Tables[0].TableName = "ITEMSUMMARY";
                if (dsResult.Tables.Count > 2)
                {
                    dsResult.Tables[1].TableName = "STOCKIN";
                }

                gcItemSummary.DataSource = dsResult.Tables[0];
                gcPurchase.DataSource = dsResult.Tables[1];
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
            gcItemSummary.ShowRibbonPrintPreview();
        }

        private void gvItemSummary_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvItemSummary.FocusedRowHandle < 0)
                return;
            e.Menu.Items.Add(new DXMenuItem("View Detail", new EventHandler(OnViewDetail_Click)));
        }

        void OnViewDetail_Click(object sender, EventArgs e)
        {
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            try
            {
                Dictionary<string, object> searchCriteria = new Dictionary<string, object>()
                    {
                        { "BranchID",  gvItemSummary.GetFocusedRowCellValue("BRANCHID") }
                        , { "FromDate", dtFromDate.EditValue }
                        , { "ToDate", dtToDate.EditValue }
                        , { "ItemID", sluItemCode.EditValue }
                        , { "IncludeBillNumber", chkIncludeBillNumber.EditValue }
                    };

                DataSet dsResult = new ReportRepository().GetReportDataset("USP_RPT_ITEMSUMMARYDETAILS", searchCriteria);
                SplashScreenManager.CloseOverlayForm(handle);
                frmItemSummaryDetail obj = new frmItemSummaryDetail(dsResult, chkIncludeBillNumber.EditValue);
                obj.ShowDialog();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseOverlayForm(handle);
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}