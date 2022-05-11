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

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
            };
            return GetReportData("USP_RPT_ITEMWISEBRANCHREFUND", parameters);
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;
    }
}
