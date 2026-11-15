using NSRetailPOS.Operations.Branch;
using NSRetailPOS.ReportControls.ReportBase;
using System;
using System.Collections.Generic;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucBranchDayClosures : SearchCriteriaBase
    {
        public ucBranchDayClosures()
        {
            InitializeComponent();
          
            dtpFromDate.EditValue = DateTime.Now;
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtpToDate, []);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new()
            {
                { "BRANCHIDS", Utility.branchInfo.BranchID }
                , { "FROMDATE", dtpFromDate.EditValue }
                , { "ToDATE", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_BRANCHDAYCLOSURE", parameters);
        }

        private void btnAddBranchDayClosure_Click(object sender, EventArgs e)
        {
            new frmBranchDayClosure().ShowDialog();
        }
    }
}
