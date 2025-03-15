using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucBranchIndent : SearchCriteriaBase
    {
        public ucBranchIndent()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "SALEQUANTITY", "Sale Quantity" }
                , { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "LASTDISPATCHDATE", "Last dispatch date" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, cmbCategory, txtIndentDays };

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            EditableColumns = new List<string>() { "INDENTQUANTITY" };

            txtIndentDays.EditValue = 7;
            txtSafetyDays.EditValue = 0;

            SetFocusControls(cmbBranch, txtSafetyDays, columnHeaders);
            btnPrintToDM.Visible = false;
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "IndentDays", txtIndentDays.EditValue }
                , { "SafetyStockDays", txtSafetyDays.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            return GetReportData("USP_R_BRANCHINDENT", parameters);
        }

        public override void DataBoundCompleted()
        {
            GridColumn gcIndentDaysSaleQuantity = ResultGridView.Columns.ColumnByFieldName("INDENTDAYSSALEQUANTITY");
            if (gcIndentDaysSaleQuantity != null)
            {
                gcIndentDaysSaleQuantity.Caption = $"Last {txtIndentDays.EditValue} days sale";
            }
        }

        private void btnPrintToDM_Click(object sender, EventArgs e)
        {
            DotMatrixPrintHelper.PrintBranchIndent(
                cmbBranch.Text
                , cmbCategory.Text
                , txtIndentDays.Text
                , txtSafetyDays.Text
                , DotMatrixPrintHelper.GetDataTableWYSIWYG(ResultGridView));
        }
    }
}
