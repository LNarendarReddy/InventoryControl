using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using NSRetail.ReportForms.POS;
using NSRetail.ReportForms.Wareshouse;
using NSRetail.ReportForms.Wareshouse.SaleReports;
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

        public GridControl ResultsGrid => gcResults;

        public GridView ResultsGridView => gvResults;

        public frmReportPlaceHolder()
        {
            InitializeComponent();
        }

        ReportHolder selectedReportHolder;
        IOverlaySplashScreenHandle handle;
        BackgroundWorker bgwGetData = new BackgroundWorker();

        private void frmReportPlaceHolder_Load(object sender, EventArgs e)
        {          
            bgwGetData.DoWork += BgwGetData_DoWork;
            bgwGetData.RunWorkerCompleted += BgwGetData_RunWorkerCompleted;

            tlReport.DataSource = InitializeTreeList();
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
                column.Visible = !column.FieldName.EndsWith("ID") 
                    && (searchCriteria.HiddenColumns == null || !searchCriteria.HiddenColumns.Contains(column.FieldName));

                if (!column.Visible) continue;

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
                    column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Far;
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
                gcButtonColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (searchCriteria.Periodicity != null && gvResults.Columns.ColumnByFieldName("PERIODOCITY") != null)
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

            GridColumn gcSerialNo = gvResults.Columns.ColumnByFieldName("SNO");
            if (gcSerialNo != null)
            {
                gcSerialNo.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
                gcSerialNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            }
            GridColumn gcStatus = gvResults.Columns.ColumnByFieldName("STATUS");
            if (gcStatus != null)
            {
                gcStatus.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gcStatus.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            (searchCriteria.FirstControl ?? searchCriteria).Focus();
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
            dpTop.Text = $"{dpTop.Text} for {selectedReportHolder.ReportName}";

            (selectedReportHolder.SearchCriteriaControl.FirstControl ?? selectedReportHolder.SearchCriteriaControl).Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (selectedReportHolder == null
                || selectedReportHolder.SearchCriteriaControl == null
                || !selectedReportHolder.SearchCriteriaControl.ValidateMandatoryFields())
                return;

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

        private void gvResults_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                gvResults.MoveNext();
        }

        private void btnAction_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            selectedReportHolder.SearchCriteriaControl.ActionExecute(gvResults.FocusedColumn.Caption, gvResults.GetFocusedDataRow());
        }

        private List<ReportHolder> InitializeTreeList()
        {
            List<ReportHolder> reportList = new List<ReportHolder>();
            ReportHolder branchReports = new ReportHolder() { ReportName = "Branch Reports" };
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refunds", SearchCriteriaControl = new ucBranchRefunds() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refunds by Item", SearchCriteriaControl = new ucBranchRefundByItems() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Indent", SearchCriteriaControl = new ucBranchIndent() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch Differences", SearchCriteriaControl = new ucDispatchDifferences() });
            reportList.Add(branchReports);

            ReportHolder posReports = new ReportHolder() { ReportName = "POS Reports" };
            posReports.SubCategory.Add(new ReportHolder() { ReportName = "Day closure", SearchCriteriaControl = new ucDayClosureList() });
            posReports.SubCategory.Add(new ReportHolder() { ReportName = "Customer Returns", SearchCriteriaControl = null});
            reportList.Add(posReports);

            ReportHolder wareHouseReports = new ReportHolder() { ReportName = "Warehouse Reports" };

            ReportHolder StockReports = new ReportHolder() { ReportName = "Stock Reports" };
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Invoice List", SearchCriteriaControl = new Wareshouse.StockReports.ucInvoiceList() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch List", SearchCriteriaControl = new Wareshouse.StockReports.ucDispatchList() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch DC List", SearchCriteriaControl = new Wareshouse.StockReports.ucDispatchDCList() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Stock Summary By Branch", SearchCriteriaControl = new Wareshouse.StockReports.ucStockSummaryByBranch() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Zero Stock", SearchCriteriaControl = new Wareshouse.StockReports.ucZeroStock() });
            wareHouseReports.SubCategory.Add(StockReports);

            ReportHolder saleReports = new ReportHolder() { ReportName = "Sale Reports" };
            saleReports.SubCategory.Add(new ReportHolder() { ReportName = "Running sales", SearchCriteriaControl = new ucRunningSales() });
            saleReports.SubCategory.Add(new ReportHolder() { ReportName = "Tax Wise sales", SearchCriteriaControl = new ucTaxWiseSales() });
            saleReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Wise sales", SearchCriteriaControl = new ucItemWiseSales() });
            reportList.Add(saleReports);

            ReportHolder profitabilityReports = new ReportHolder() { ReportName = "Profitability Reports" };
            profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Periodicity", SearchCriteriaControl = new Wareshouse.Profitability.ucPeriodicity() });
            profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Wise", SearchCriteriaControl = new Wareshouse.Profitability.ucItemWise() });
            wareHouseReports.SubCategory.Add(profitabilityReports);

            ReportHolder taxReports = new ReportHolder() { ReportName = "Tax Reports" };
            taxReports.SubCategory.Add(new ReportHolder() { ReportName = "Tax break-up day wise", SearchCriteriaControl = new Wareshouse.TaxBreakUp.ucTaxBreakUpDayWise() });
            wareHouseReports.SubCategory.Add(taxReports);

            wareHouseReports.SubCategory.Add(new ReportHolder() { ReportName = "Supplier Indent", SearchCriteriaControl = new ucDealerIndent() });
            wareHouseReports.SubCategory.Add(new ReportHolder() { ReportName = "Stock Counting Sheets", SearchCriteriaControl = new ucStockCountingList() });
            wareHouseReports.SubCategory.Add(new ReportHolder() { ReportName = "Supplier Returns Sheets", SearchCriteriaControl = new ucSupplierReturnsList() });
            wareHouseReports.SubCategory.Add(new ReportHolder() { ReportName = "Supplier Indent List", SearchCriteriaControl = new ucSupplierIndentList() });

            reportList.Add(wareHouseReports);

            reportList.ForEach(x => SubscribeLastControl(x));

            return reportList;
        }

        private void SubscribeLastControl(ReportHolder rptHolder)
        {
            rptHolder.SubCategory?.ForEach(x => SubscribeLastControl(x));

            if (rptHolder?.SearchCriteriaControl?.LastControl == null) return;
            rptHolder.SearchCriteriaControl.LastControl.KeyDown += LastControl_KeyDown;
        }

        private void LastControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            btnSearch.Focus();
            e.Handled = true;
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