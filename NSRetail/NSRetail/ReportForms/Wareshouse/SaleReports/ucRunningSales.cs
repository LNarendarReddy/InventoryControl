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
            SetFocusControls(cmbBranch, chkIncludeCategory, new Dictionary<string, string>());
            BindBranch(cmbBranch);
        }

        public override object GetData()
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
