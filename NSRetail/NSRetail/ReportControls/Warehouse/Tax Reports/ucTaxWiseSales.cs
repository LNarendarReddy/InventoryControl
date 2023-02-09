using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucTaxWiseSales : SearchCriteriaBase
    {
        public ucTaxWiseSales()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "CLOSUREDATE", "Closure Date" }
                , { "GSTCODE", "GST Code" }
                , { "BILLEDVALUE", "Billed Value" }
            };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetFocusControls(cmbBranch, cmbBranch, columnHeaders);
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
            };
            return GetReportData("USP_RPT_BRANCHSALEBYGST", parameters);
        }
    }
}
