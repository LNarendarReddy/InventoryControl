using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucRunningSales : SearchCriteriaBase
    {        
        public ucRunningSales()
        {
            InitializeComponent();

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetFocusControls(cmbBranch, chkIncludeCategory, new Dictionary<string, string>());
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "IncludeBranch", chkIncludeBranch .EditValue }
                , { "IncludeCounter", chkIncludeCounter.EditValue }
                , { "IncludeBillNo", chkIncludeBillNo.EditValue }
                , { "IncludeItem", chkIncludeItem.EditValue }
                , { "IncludeCategory", chkIncludeCategory.EditValue }
            };
            return GetReportData("USP_RPT_RUNNINGSALE", parameters);
        }
    }
}
