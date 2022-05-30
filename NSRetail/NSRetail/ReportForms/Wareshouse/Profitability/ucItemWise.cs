
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class ucItemWise : SearchCriteriaBase
    {
        private Dictionary<string, string> specificColumnHeaders;

        public override Dictionary<string, string> SpecificColumnHeaders => specificColumnHeaders;

        public ucItemWise()
        {
            InitializeComponent();

            specificColumnHeaders = new Dictionary<string, string>() { };
        }

        private void ucItemWise_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_RPT_PROFITABILITY_SKUWISE", parameters);
        }
    }
}
