using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class ucBranchDayClosures : SearchCriteriaBase
    {
        public ucBranchDayClosures()
        {
            InitializeComponent();
            
            dtpFromDate.EditValue = DateTime.Now.AddDays(-1);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtpToDate, new Dictionary<string, string>());
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "BRANCHIDS", cmbBranch.EditValue }
                , { "FROMDATE", dtpFromDate.EditValue }
                , { "ToDATE", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_BRANCHDAYCLOSURE", parameters);
        }
    }
}
