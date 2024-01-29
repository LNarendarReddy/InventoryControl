using DataAccess;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NSRetail.ReportForms;
using Entity;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.DXErrorProvider;

namespace NSRetail.Countning
{
    public partial class frmSliceCountingAddItems : XtraForm
    {
        public const string Criteria_TopSellingItems = "Top selling items";
        public const string Criteria_HighQuantity = "High quantity";
        public const string Criteria_HighValue = "High value items";        
        public const string Criteria_Negatives = "Negatives";

        private readonly frmSliceCounting parentForm;
        IOverlaySplashScreenHandle handle;
        BackgroundWorker bgwGetData = new BackgroundWorker();
        Dictionary<string, string> columnHeaders = new Dictionary<string, string>()
        {
            { "SKUCODE", "SKU Code" },
            { "ITEMNAME", "Item name" },
            { "CATEGORYNAME", "Category" },
            { "SUBCATEGORYNAME", "SubCategory" },
            { "CLASSIFICATIONNAME", "Classification" },
            { "SUBCLASSIFICATIONNAME", "SubClassification" },
            { "SALEAVG", "Sale (Avg)" },
            { "BRANCHNAME", "Branch name" },
            { "SALEQTYORWGHTINKGS", "Sale Qty or Wght in KG(s)" },
            { "STOCKQTYORWGHTINKGS", "Stock Qty or Wght in KG(s)" },
            { "STOCKAVG", "Stock (Avg)" },
            { "STOCKVALUE", "Stock value" },
            { "STOCKVALUEAVG", "Stock value (Avg)" },
            { "SALEPRICE", "Sale price)" },
        };

        public frmSliceCountingAddItems(frmSliceCounting parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            bgwGetData.DoWork += BgwGetData_DoWork;
            bgwGetData.RunWorkerCompleted += BgwGetData_RunWorkerCompleted;
        }

        private void frmSliceCountingAddItems_Load(object sender, EventArgs e)
        {
            DataTable dtCriteria = new DataTable();
            dtCriteria.Columns.Add("Criteria", typeof(string));
            dtCriteria.Rows.Add(Criteria_TopSellingItems);
            dtCriteria.Rows.Add(Criteria_HighQuantity);
            dtCriteria.Rows.Add(Criteria_HighValue);            
            dtCriteria.Rows.Add(Criteria_Negatives);

            cmbCriteria.Properties.DataSource = dtCriteria;
            cmbCriteria.Properties.ValueMember = "Criteria";
            cmbCriteria.Properties.DisplayMember = "Criteria";

            this.Text = $"No. of selected branches : {parentForm.SliceCountingObj.BranchIDs.ToString().Split(',').Count()} => {parentForm.SliceCountingObj.BranchText}";

            cmbCriteria_EditValueChanged(sender, e);
            gvAvailableItems_SelectionChanged(null, null);
            lblTotalRowCount.Text = "Total row count : 0";
            gvAvailableItems.OptionsSelection.MultiSelect = false;
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
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "BranchIDs", parentForm.SliceCountingObj.BranchIDs },
                    { "SelectedCriteria", parentForm.SliceCountingObj.SelectedCriteria },
                    { "SaleDays", parentForm.SliceCountingObj.SaleDays },
                };
                DataSet dsItems = new ReportRepository().GetReportDataset("USP_R_SLICECOUNTING_ITEMS", parameters);

                if (dsItems.Tables.Count > 1)
                {
                    dsItems.Relations.Add("Branch wise breakup", dsItems.Tables[0].Columns["ITEMID"], dsItems.Tables[1].Columns["ITEMID"]);
                }

                InvokeUIOperation(dsItems);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (gvAvailableItems.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("No items are selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var items = gvAvailableItems.GetSelectedRows().Select(x =>
                            new Item()
                            {
                                ItemID = gvAvailableItems.GetRowCellValue(x, "ITEMID"),
                                SKUCode = gvAvailableItems.GetRowCellValue(x, "SKUCODE"),
                                ItemName = gvAvailableItems.GetRowCellValue(x, "ITEMNAME"),
                                CategoryName = gvAvailableItems.GetRowCellValue(x, "CATEGORYNAME")
                            }).ToList();

            parentForm.CheckAndAddItem(items);
            gvAvailableItems.ClearSelection();
        }

        private void InvokeUIOperation(object reportData)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => InvokeUIOperation(reportData)));
                return;
            }

            gvAvailableItems.Columns.Clear();
            gcAvailableItems.DataSource = reportData;

            if (((DataSet)reportData).Tables.Count > 0) 
            {
                gcAvailableItems.DataMember = ((DataSet)reportData).Tables[0].TableName;
            }

            gvAvailableItems.OptionsSelection.MultiSelect = gvAvailableItems.RowCount > 0;
            FormatGridViewColumns(gvAvailableItems);
            gvAvailableItems_ColumnFilterChanged(null, null);
            gvAvailableItems_SelectionChanged(null, null);
            lblTotalRowCount.Text = $"Total row count : {gvAvailableItems.RowCount}";
        }

        private void gvAvailableItems_ColumnFilterChanged(object sender, EventArgs e)
        {
            txtTopRows.EditValue = gvAvailableItems.RowCount;
            txtTopRows.Properties.MaxValue = gvAvailableItems.RowCount;
            txtSelectRandom.Properties.MaxValue = gvAvailableItems.RowCount;
        }

        private void gvAvailableItems_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            var childDetail = gvAvailableItems.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            childDetail.OptionsSelection.MultiSelect = false;
            FormatGridViewColumns(childDetail);
        }

        private void FormatGridViewColumns(GridView gridView)
        {
            foreach (GridColumn column in gridView.Columns)
            {
                column.Visible = !column.FieldName.EndsWith("ID");

                if (!column.Visible) continue;
               

                if (columnHeaders.ContainsKey(column.FieldName))
                    column.Caption = columnHeaders[column.FieldName];

                if (column.Summary.Count == 0 && frmReportPlaceHolder.SummableTypes.Contains(column.ColumnType))
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
                    column.OptionsColumn.AllowEdit = false;

                if (column.ColumnType == typeof(TimeSpan))
                {
                    column.DisplayFormat.FormatType = FormatType.DateTime;
                    column.DisplayFormat.FormatString = "t";
                }
            }
        }

        private void gvAvailableItems_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            lblSelectedRowCount.Text = $"Selected record count : {gvAvailableItems.SelectedRowsCount}";            
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            try
            {
                int randCount = int.Parse(txtSelectRandom.EditValue?.ToString());
                int randLimit = int.Parse(txtTopRows.EditValue?.ToString());

                Random random = new Random();
                for (int i = 0; i < randCount;)
                {
                    int selection = random.Next(randLimit);
                    if (gvAvailableItems.IsRowSelected(selection)) continue;

                    gvAvailableItems.SelectRow(selection);
                    i++;
                }
            }
            catch(Exception ex) { ErrorMgmt.ShowError(ex); }
        }

        private void cmbCriteria_EditValueChanged(object sender, EventArgs e)
        {
            lciSaleDays.Visibility = 
                cmbCriteria.EditValue != null && cmbCriteria.EditValue.Equals(Criteria_TopSellingItems)
                    ? LayoutVisibility.Always
                    : LayoutVisibility.Never;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dxValidationProvider1.RemoveControlError(txtTopSellingSaleDays);

            if (!dxValidationProvider1.Validate()) return;

            if (cmbCriteria.EditValue.Equals(Criteria_TopSellingItems))
            {
                dxValidationProvider1.SetValidationRule(txtTopSellingSaleDays
                    , new ConditionValidationRule(Criteria_TopSellingItems, ConditionOperator.IsNotBlank) { ErrorText = "Mandatory", ErrorType = ErrorType.Critical });
            }
            
            if (!dxValidationProvider1.Validate()) return;

            BuildObject();

            handle = SplashScreenManager.ShowOverlayForm(this);
            bgwGetData.RunWorkerAsync();
        }


        private void BuildObject()
        {
            parentForm.SliceCountingObj.SelectedCriteria = cmbCriteria.EditValue;
            parentForm.SliceCountingObj.SaleDays = txtTopSellingSaleDays.EditValue;
        }
    }
}
