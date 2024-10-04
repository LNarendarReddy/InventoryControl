using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucSalehourWise : SearchCriteriaBase
    {
        public ucSalehourWise()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALETAX", "Sale Price Tax" },
                { "SALEPRICEWT", "Sale Price WT" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" },
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_SALE_HOURWISE", parameters);
        }
    }
}
