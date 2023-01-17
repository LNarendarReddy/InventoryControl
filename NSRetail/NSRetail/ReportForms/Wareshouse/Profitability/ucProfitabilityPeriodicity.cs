using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class ucProfitabilityPeriodicity : SearchCriteriaBase
    {
        public ucProfitabilityPeriodicity()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {               
                { "TOTALCOSTPRICEWOT", "Total Cost Price WOT" },
                { "TOTALCOSTPRICETAX", "Total Cost Price Tax" },
                { "TOTALCOSTPRICEWT", "Total Cost Price WT" },
                { "TOTALSALEPRICEWOT", "Total Sale Price WOT" },
                { "TOTALSALETAX", "Total Sale Price Tax" },
                { "TOTALSALEPRICEWT", "Total Sale Price WT" },
                { "PROFITMARGINWOT", "Profit Margin WOT" },
                { "PROFITMARGINPERWOT", "Profit Margin % WOT" },
                { "PROFITMARGINWT", "Profit Margin WT" },
                { "PROFITMARGINPERWT", "Profit Margin % WT" },
                { "PERIODOCITY", "Periodocity" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbBranch.Properties.DataSource = masterRepo.GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            cmbCategory.Properties.DataSource = masterRepo.GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate);
            SetFocusControls(cmbPeriodicity, dtpToDate,specificColumnHeaders);
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
            };

            return GetReportData("USP_RPT_PROFITABILITY_PERIODICITY", parameters);
        }
    }
}
