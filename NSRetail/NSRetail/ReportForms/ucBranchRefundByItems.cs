using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms
{
    public partial class ucBranchRefundByItems : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public ucBranchRefundByItems()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
            {
                { "BREFUNDNUMBER", "Branch Refund number" }
            };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            deFromDate.EditValue = DateTime.Now.AddDays(-7);
            deToDate.EditValue = DateTime.Now;
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", deFromDate.EditValue }
                , { "ToDate", deToDate.EditValue }
            };
            return GetReportData("USP_RPT_ITEMWISEBRANCHREFUND", parameters);
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;
    }
}
