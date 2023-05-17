using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
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

        public frmReportPlaceHolder(List<ReportHolder> reportHolders, string header)
        {
            InitializeComponent();

            reportHolders.ForEach(x => SubscribeLastControl(x));
            tlReport.DataSource = reportHolders;
            tlReport.ChildListFieldName = "SubCategory";
            dpLeft.Text = header + " Reports";
        }

        ReportHolder selectedReportHolder;
        IOverlaySplashScreenHandle handle;
        BackgroundWorker bgwGetData = new BackgroundWorker();

        private void frmReportPlaceHolder_Load(object sender, EventArgs e)
        {          
            bgwGetData.DoWork += BgwGetData_DoWork;
            bgwGetData.RunWorkerCompleted += BgwGetData_RunWorkerCompleted;
                        
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
            try
            {
                object datasource = selectedReportHolder.SearchCriteriaControl.GetData();
                InvokeUIOperation(datasource);
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void InvokeUIOperation(object dtReportData)
        {
            if (InvokeRequired) 
            {
                BeginInvoke((Action)(() => InvokeUIOperation(dtReportData))); 
                return; 
            }

            gvResults.Columns.Clear();
            gcResults.DataSource = dtReportData;

            if(selectedReportHolder.SearchCriteriaControl.IsDataSet)
            {
                gcResults.DataMember = ((DataSet)dtReportData).Tables[0].TableName;
            }
            
            if (dtReportData == null) return;
            int recCount = (selectedReportHolder.SearchCriteriaControl.IsDataSet 
                ? ((DataSet)dtReportData).Tables[0]
                : ((DataTable)dtReportData)).Rows.Count;

            lblRecordCount.Text = $"Record count : {recCount}";
            SearchCriteriaBase searchCriteria = selectedReportHolder.SearchCriteriaControl;
            FormatGridViewColumns(gvResults);

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
                    case "Hourly":
                        dtpPeriodicity.MaskSettings.MaskExpression = "t";
                        break;
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

            searchCriteria.DataBoundCompleted();

            Control ctrlToFocus = searchCriteria.FirstControl ?? searchCriteria;
            CheckedComboBoxEdit branchComboBox = ctrlToFocus as CheckedComboBoxEdit;
            searchCriteria.RemoveCheckedComboBoxEnter(branchComboBox);
            searchCriteria.Focus();
            ctrlToFocus.Focus();
            searchCriteria.AddCheckedComboBoxEnter(branchComboBox);
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
            selectedReportHolder.SearchCriteriaControl.Location = new System.Drawing.Point(2, 2);

            btnSearch.Enabled = true;
            btnReport.Enabled = true;            
            dpTop.Text = $"{dpTop.Text} for {selectedReportHolder.ReportName}";
            lcIncludeColumns.Visibility = selectedReportHolder.SearchCriteriaControl.ShowIncludeSetting ? LayoutVisibility.Always : LayoutVisibility.Never;
            (selectedReportHolder.SearchCriteriaControl.FirstControl ?? selectedReportHolder.SearchCriteriaControl).Focus();
        }

        public void btnSearch_Click(object sender, EventArgs e)
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

        private void frmReportPlaceHolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedReportHolder?.SearchCriteriaControl == null)
            {
                return;
            }

            if(e.KeyCode == Keys.F2)
            {
                (selectedReportHolder?.SearchCriteriaControl?.FirstControl ?? selectedReportHolder?.SearchCriteriaControl)?.Focus();
            }
            else if(e.KeyCode == Keys.F3 && selectedReportHolder.SearchCriteriaControl.ShowIncludeSetting)
            {
                if(new frmIncludeExclude(selectedReportHolder.SearchCriteriaControl.IncludeSettingsCollection).ShowDialog()
                    == DialogResult.OK)
                {
                    btnSearch_Click(null, null);
                }

                e.Handled = true;
            }
        }

        private void gvResults_ColumnFilterChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = $"Record count : {gvResults.RowCount}";
        }

        private void gvResults_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            var childDetail = gvResults.GetDetailView(e.RowHandle, e.RelationIndex);
            FormatGridViewColumns(childDetail as GridView);
        }

        private void FormatGridViewColumns(GridView gridView)
        {
            SearchCriteriaBase searchCriteria = selectedReportHolder.SearchCriteriaControl;

            foreach (GridColumn column in gridView.Columns)
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

                if (column.Summary.Count == 0 && summableTypes.Contains(column.ColumnType))
                {
                    GridColumnSummaryItem siTotal = new GridColumnSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        FieldName = column.FieldName,
                        DisplayFormat = "{0:#.##}"
                    };

                    column.Summary.Add(siTotal);
                    gridView.OptionsView.ShowFooter = true;
                    column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Far;
                }

                if (column.FieldName == "ITEMCODE" || column.FieldName == "SKUCODE")
                    column.OptionsColumn.ReadOnly = true;
                else
                    column.OptionsColumn.AllowEdit = searchCriteria.EditableColumns != null && searchCriteria.EditableColumns.Contains(column.FieldName);

                if (column.ColumnType == typeof(TimeSpan))
                {
                    column.DisplayFormat.FormatType = FormatType.DateTime;
                    column.DisplayFormat.FormatString = "t";
                }
            }
        }

        private void tlReport_CustomRowFilter(object sender, DevExpress.XtraTreeList.CustomRowFilterEventArgs e)
        {
            if (e.Row is ReportHolder)
            {
                e.Visible = (e.Row as ReportHolder).HasAccess;
                e.Handled = true;
            }
        }
    }

    public class ReportHolder
    {
        public ReportHolder()
        {
            SubCategory = new List<ReportHolder>();
        }

        public string ReportName { get; set; }

        public SearchCriteriaBase SearchCriteriaControl { get; set; }

        public List<ReportHolder> SubCategory { get; set; }

        public bool HasAccess => (SearchCriteriaControl != null && SearchCriteriaControl.HasAccess) || SubCategory.Any(x => x.HasAccess);
    }

}