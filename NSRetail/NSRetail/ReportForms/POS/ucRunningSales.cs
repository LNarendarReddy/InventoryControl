using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.POS
{
    public partial class ucRunningSales : SearchCriteriaBase
    {
        public ucRunningSales()
        {
            InitializeComponent();
        }

        private void ucRunningSales_Load(object sender, EventArgs e)
        {
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
            };
            return GetReportData("USP_RPT_RUNNINGSALE", parameters);
        }
    }
}
