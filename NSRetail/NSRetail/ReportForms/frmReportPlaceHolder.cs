using DevExpress.XtraGrid.Columns;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace NSRetail.ReportForms
{
    public partial class frmReportPlaceHolder : DevExpress.XtraEditors.XtraForm
    {
        public frmReportPlaceHolder()
        {
            InitializeComponent();
        }

        ReportHolder selectedReportHolder;
        IOverlaySplashScreenHandle handle;
        BackgroundWorker bgwGetData = new BackgroundWorker();

        private void frmReportPlaceHolder_Load(object sender, EventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();
            ReportHolder posReports = new ReportHolder() { ReportName = "POS Reports" };
            posReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refunds by Item", SearchCriteriaControl = new ucBranchRefundByItems() });
            reportList.Add(posReports);

            ReportHolder itemReports = new ReportHolder() { ReportName = "Item Reports" };
            itemReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Wise sales", SearchCriteriaControl = null });
            itemReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Wise sales 2", SearchCriteriaControl = null });
            reportList.Add(itemReports);

            bgwGetData.DoWork += BgwGetData_DoWork;
            bgwGetData.RunWorkerCompleted += BgwGetData_RunWorkerCompleted;

            tlReport.DataSource = reportList;
            tlReport.ChildListFieldName = "SubCategory";
            tlReport.ExpandAll();
        }

        private void BgwGetData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        private void BgwGetData_DoWork(object sender, DoWorkEventArgs e)
        {
            InvokeUIOperation(selectedReportHolder.SearchCriteriaControl.GetData());
        }

        private void InvokeUIOperation(DataTable dtReportData)
        {
            if (InvokeRequired) 
            {
                BeginInvoke((Action)(() => InvokeUIOperation(dtReportData))); 
                return; 
            }

            gcResults.DataSource = dtReportData;

            foreach (GridColumn column in gvResults.Columns)
            {
                column.Visible = !column.FieldName.EndsWith("ID");

                // first set the generic headers if available
                if (selectedReportHolder.SearchCriteriaControl.GenericColumnHeaders.ContainsKey(column.FieldName))
                    column.Caption = selectedReportHolder.SearchCriteriaControl.GenericColumnHeaders[column.FieldName];

                // override generic header if a specific header is available
                if (selectedReportHolder.SearchCriteriaControl.SpecificColumnHeaders.ContainsKey(column.FieldName))
                    column.Caption = selectedReportHolder.SearchCriteriaControl.SpecificColumnHeaders[column.FieldName];
            }
        }

        private void tlReport_SelectionChanged(object sender, EventArgs e)
        {
            pcSearchCriteria.Controls.Clear();
            btnSearch.Enabled = false;
            btnReport.Enabled = false;
            gcResults.DataSource = null;

            selectedReportHolder = tlReport.GetFocusedRow() as ReportHolder;
            if (selectedReportHolder == null || selectedReportHolder.SearchCriteriaControl == null) return;

            pcSearchCriteria.Controls.Add(selectedReportHolder.SearchCriteriaControl);
            selectedReportHolder.SearchCriteriaControl.Location = new System.Drawing.Point(5, 5);

            btnSearch.Enabled = true;
            btnReport.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (selectedReportHolder == null || selectedReportHolder.SearchCriteriaControl == null) return;

            handle = SplashScreenManager.ShowOverlayForm(this);
            bgwGetData.RunWorkerAsync();            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            gcResults.ShowRibbonPrintPreview();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    class ReportHolder
    {
        public ReportHolder()
        {
            SubCategory = new List<ReportHolder>();
        }

        public string ReportName { get; set; }

        public SearchCriteriaBase SearchCriteriaControl { get; set; }

        public List<ReportHolder> SubCategory { get; set; }
    }

}