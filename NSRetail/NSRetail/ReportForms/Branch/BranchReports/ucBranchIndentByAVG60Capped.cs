using DataAccess;
using DevExpress.Charts.Native;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static Dropbox.Api.TeamLog.TimeUnit;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucBranchIndentByAVG60Capped : SearchCriteriaBase
    {
        public ucBranchIndentByAVG60Capped()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "LASTDISPATCHDATE", "Last dispatch date" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, cmbCategory, txtIndentDays };

            HiddenColumns = new List<string>() { "AVGSALES", "NOOFDAYSSALES", "WHSTOCK" };

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

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

            return GetReportData("USP_RPT_BRANCHINDENT_AVG_60CAPPED", parameters);
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

        private void btnSaveIndent_Click(object sender, EventArgs e)
        {
            if (ResultGrid.DataSource == null) return;

            try
            {
                DataTable indentTable = ((DataTable)ResultGrid.DataSource).Copy();

                if (indentTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No data to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = XtraMessageBox.Show("Are you sure want to save indent?",
                    "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Convert.ToString(result).ToLower() == "yes")
                {
                    new IndentRepository().SaveBranchIndent(
                    0, Utility.BranchID, cmbBranch.EditValue, cmbCategory.EditValue,
                    txtIndentDays.EditValue, "6M AVG with 60% Cap", Utility.UserID, indentTable);
                    XtraMessageBox.Show("Indent saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}
