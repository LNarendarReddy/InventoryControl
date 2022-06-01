using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using DevExpress.XtraSplashScreen;
using Entity;
using NSRetail.ReportForms.POS;
using NSRetail.ReportForms.Wareshouse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.ReportForms
{
    public partial class frmReportPlaceHolder : XtraForm
    {
        List<Type> summableTypes = new List<Type>() { typeof(int), typeof(double), typeof(decimal), typeof(float) };

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
            ReportHolder branchReports = new ReportHolder() { ReportName = "Branch Reports" };
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refunds by Item", SearchCriteriaControl = new ucBranchRefundByItems() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Indent", SearchCriteriaControl = new ucBranchIndent() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch Differences", SearchCriteriaControl = new ucDispatchDifferences() });
            reportList.Add(branchReports);

            ReportHolder posReports = new ReportHolder() { ReportName = "POS Reports" };
            posReports.SubCategory.Add(new ReportHolder() { ReportName = "Day closure", SearchCriteriaControl = new ucDayClosureList() });
            posReports.SubCategory.Add(new ReportHolder() { ReportName = "Running sales", SearchCriteriaControl = new ucRunningSales() });
            posReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refunds", SearchCriteriaControl = new ucBranchRefunds() });
            reportList.Add(posReports);

            ReportHolder wareHouseReports = new ReportHolder() { ReportName = "Warehouse Reports" };
            wareHouseReports.SubCategory.Add(new ReportHolder() { ReportName = "Dealer Indent", SearchCriteriaControl = new ucDealerIndent() });
            ReportHolder profitabilityReports = new ReportHolder() { ReportName = "Profitability Reports" };
            profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Periodicity", SearchCriteriaControl = new Wareshouse.Profitability.ucPeriodicity() });
            profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Wise", SearchCriteriaControl = new Wareshouse.Profitability.ucItemWise() });
            wareHouseReports.SubCategory.Add(profitabilityReports);
            reportList.Add(wareHouseReports);

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
            if (handle == null) return;
            SplashScreenManager.CloseOverlayForm(handle);
            handle = null;
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

            if (dtReportData == null) return;
            
            lblRecordCount.Text = $"Record count : {dtReportData.Rows.Count}";
            SearchCriteriaBase searchCriteria = selectedReportHolder.SearchCriteriaControl;

            foreach (GridColumn column in gvResults.Columns)
            {
                column.Visible = !column.FieldName.EndsWith("ID");

                // first set the generic headers if available
                if (searchCriteria.GenericColumnHeaders != null && searchCriteria.GenericColumnHeaders.ContainsKey(column.FieldName))
                    column.Caption = searchCriteria.GenericColumnHeaders[column.FieldName];

                // override generic header if a specific header is available
                if (searchCriteria.SpecificColumnHeaders != null && searchCriteria.SpecificColumnHeaders.ContainsKey(column.FieldName))
                    column.Caption = searchCriteria.SpecificColumnHeaders[column.FieldName];

                //if(searchCriteria.TotalSummaryFields != null && column.Summary.Count == 0 && searchCriteria.TotalSummaryFields.Contains(column.FieldName))
                if(column.Summary.Count == 0 && summableTypes.Contains(column.ColumnType))
                {                    
                    GridColumnSummaryItem siTotal = new GridColumnSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        FieldName = column.FieldName,
                        DisplayFormat = "{0:#.##}"
                    };

                    column.Summary.Add(siTotal);
                    gvResults.OptionsView.ShowFooter = true;
                }

                column.OptionsColumn.AllowEdit = searchCriteria.EditableColumns != null && searchCriteria.EditableColumns.Contains(column.FieldName);
            }

            foreach (string buttonColumn in searchCriteria.ButtonColumns)
            {
                if (gvResults.Columns.ColumnByFieldName(buttonColumn) != null) continue;

                GridColumn gcButtonColumn = new GridColumn()
                {
                    Name = buttonColumn,
                    Caption = buttonColumn,
                    FieldName = buttonColumn,
                    VisibleIndex = gvResults.Columns.Count,
                    ColumnEdit = btnAction
                };
                gvResults.Columns.Add(gcButtonColumn);
            }

            if(searchCriteria.Periodicity != null && gvResults.Columns.ColumnByFieldName("PERIODOCITY") != null)
            {
                GridColumn periodicityColumn = gvResults.Columns.ColumnByFieldName("PERIODOCITY");

                dtpPeriodicity.MaskSettings.MaskManagerType = typeof(DateTime);
                dtpPeriodicity.MaskSettings.UseMaskAsDisplayFormat = true;
                switch (searchCriteria.Periodicity.EditValue.ToString())
                {
                    case "Daily":
                        dtpPeriodicity.MaskSettings.MaskExpression = "d";
                        break;
                    case "Monthly":
                        dtpPeriodicity.MaskSettings.MaskExpression = "y";
                        break;
                    case "Yearly":
                        dtpPeriodicity.MaskSettings.MaskExpression = "yyyy";
                        break;
                }
                periodicityColumn.ColumnEdit = dtpPeriodicity;

            }
        }

        private void tlReport_SelectionChanged(object sender, EventArgs e)
        {
            pcSearchCriteria.Controls.Clear();
            btnSearch.Enabled = false;
            btnReport.Enabled = false;
            gcResults.DataSource = null;
            lblRecordCount.Text = string.Empty;
            dpTop.Text = "Search Criteria";
            gvResults.RefreshData();
            gvResults.Columns.Clear();

            selectedReportHolder = tlReport.GetFocusedRow() as ReportHolder;
            if (selectedReportHolder == null || selectedReportHolder.SearchCriteriaControl == null) return;

            pcSearchCriteria.Controls.Add(selectedReportHolder.SearchCriteriaControl);
            selectedReportHolder.SearchCriteriaControl.Location = new System.Drawing.Point(5, 5);

            btnSearch.Enabled = true;
            btnReport.Enabled = true;
            lcibtnSave.Visibility = selectedReportHolder.ReportName == "Dealer Indent" ? 
                DevExpress.XtraLayout.Utils.LayoutVisibility.Always : 
                DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            dpTop.Text = $"{dpTop.Text} for {selectedReportHolder.ReportName}";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (selectedReportHolder == null || selectedReportHolder.SearchCriteriaControl == null) return;
                        
            var layoutControlOfSearch = selectedReportHolder.SearchCriteriaControl.Controls.OfType<LayoutControl>().First();
            var missingValues = selectedReportHolder.SearchCriteriaControl.MandatoryFields?
                                    .Where(x => x.EditValue == null)
                                    .Select(x => $"{Environment.NewLine}\t* " + layoutControlOfSearch.GetItemByControl(x).Text);

            if(missingValues != null && missingValues.Any())
            {
                XtraMessageBox.Show("Please select the values for : " + Environment.NewLine + string.Join(string.Empty, missingValues)
                    , "Mandatoy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                selectedReportHolder.SearchCriteriaControl.MandatoryFields.First(x => x.EditValue == null).Focus();
                return;
            }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var layoutControlOfSearch = selectedReportHolder.SearchCriteriaControl.Controls.OfType<LayoutControl>().First();
                var missingValues = selectedReportHolder.SearchCriteriaControl.MandatoryFields?
                                        .Where(x => x.EditValue == null)
                                        .Select(x => $"{Environment.NewLine}\t* " + layoutControlOfSearch.GetItemByControl(x).Text);

                if (missingValues != null && missingValues.Any())
                {
                    XtraMessageBox.Show("Please select the values for : " + Environment.NewLine + string.Join(string.Empty, missingValues)
                        , "Mandatoy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    selectedReportHolder.SearchCriteriaControl.MandatoryFields.First(x => x.EditValue == null).Focus();
                    return;
                }

                if (gvResults.RowCount == 0)
                    return;

                DealerIndent dealerIndent = new DealerIndent();
                dealerIndent.supplierID = (selectedReportHolder.SearchCriteriaControl as ucDealerIndent).cmbDealer.EditValue;
                dealerIndent.FromDate = (selectedReportHolder.SearchCriteriaControl as ucDealerIndent).dtFromDate.EditValue;
                dealerIndent.ToDate = (selectedReportHolder.SearchCriteriaControl as ucDealerIndent).dtToDate.EditValue;
                dealerIndent.CategoryID = (selectedReportHolder.SearchCriteriaControl as ucDealerIndent).cmbCategory.EditValue;
                dealerIndent.UserID = Utility.UserID;
                dealerIndent.dtSupplierIndent = ((DataTable)gcResults.DataSource).Copy();
                new ReportRepository().SaveSupplierIndent(dealerIndent);
                gcResults.DataSource = null;
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void gvResults_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                gvResults.MoveNext();
        }

        private void btnAction_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            selectedReportHolder.SearchCriteriaControl.ActionExecute(gvResults.FocusedColumn.Caption, gvResults.GetFocusedDataRow());
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