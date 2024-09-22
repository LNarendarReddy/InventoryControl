using DataAccess;
using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.StockCounting
{
    public partial class ucConsolidatedCounting : SearchCriteriaBase
    {
        public ucConsolidatedCounting()
        {
            InitializeComponent();

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "QTYORWGHT", "Quantity or Weight in KG(s)" },
                { "USERNAME", "User name" },
            };

            SetFocusControls(cmbBranch, cmbItemCode, columnHeaders);
            MandatoryFields = new List<BaseEdit> { cmbBranch, dtpCountingDate };
            IncludeSettingsCollection = new List<IncludeSettings>
            {
                new IncludeSettings("User name", "IncludeUser", new List<string> { "USERNAME" })
            };
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue },
                { "COUNTINGDATE", dtpCountingDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
            };
            return GetReportData("USP_RPT_STOCKCOUNTING_CONSOLIDATED", parameters);
        }
    }
}
