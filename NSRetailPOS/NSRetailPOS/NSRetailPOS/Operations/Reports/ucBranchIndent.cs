using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using NSRetailPOS.Data;
using NSRetailPOS.ReportControls.ReportBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Reports
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
            HiddenColumns = new List<string> { "BRANCHSTOCK" };

            DataTable dtBranch = new DataTable();
            dtBranch.Columns.Add("BRANCHID");
            dtBranch.Columns.Add("BRANCHNAME");
            dtBranch.Rows.Add(Utility.branchInfo.BranchID, Utility.branchInfo.BranchName);

            cmbBranch.Properties.DataSource = dtBranch;
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = Utility.branchInfo.BranchID;

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory(false);
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            EditableColumns = new List<string>() { "INDENTQUANTITY" };

            txtIndentDays.EditValue = 7;
            txtSafetyDays.EditValue = 0;

            SetFocusControls(cmbBranch, txtSafetyDays, columnHeaders);
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

    }
}
