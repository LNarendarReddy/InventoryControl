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

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "QTYORWGHT", "Quantity or Weight in KG(s)" }
            };

            SetFocusControls(cmbBranch, cmbBranch, columnHeaders);
            AllowedRoles = new List<string> { "Stock counting user" };
            MandatoryFields = new List<BaseEdit> { cmbBranch, dtpCountingDate };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue },
                { "COUNTINGDATE", dtpCountingDate.EditValue }
            };
            return GetReportData("USP_RPT_STOCKCOUNTING_CONSOLIDATED", parameters);
        }
    }
}
