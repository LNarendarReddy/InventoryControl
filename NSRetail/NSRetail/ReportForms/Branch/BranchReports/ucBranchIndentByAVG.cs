using DataAccess;
using DevExpress.XtraEditors;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucBranchIndentByAVG : SearchCriteriaBase
    {
        public ucBranchIndentByAVG()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "AVGSALEQUANTITY", "Average Sale Quantity" }
                , { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, cmbCategory, txtIndentDays };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            txtIndentDays.EditValue = 1;

            SetFocusControls(cmbBranch, txtIndentDays, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
                , { "NoOfDays", txtIndentDays.EditValue}
            };

            return GetReportData("USP_RPT_BRANCHINDENT_AVG", parameters);
        }

        private void btnPrintToDM_Click(object sender, EventArgs e)
        {
            DotMatrixPrintHelper.PrintBranchIndent(
                cmbBranch.Text
                , cmbCategory.Text
                , "Average Indent"
                , txtIndentDays.Text
                , DotMatrixPrintHelper.GetDataTableWYSIWYG(ResultGridView));
        }
    }
}
