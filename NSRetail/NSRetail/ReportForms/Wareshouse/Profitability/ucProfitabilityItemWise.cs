
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class ucProfitabilityItemWise : SearchCriteriaBase
    {
        public ucProfitabilityItemWise()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>() 
            {
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICETAX", "Sale Price Tax" },
                { "ACTUALSALEPRICE", "Actual Sale Price WT" },
                { "SALEPRICE", "Sale Price" },
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "COSTPRICETAX", "Cost Price Tax" },
                { "COSTPRICEWT", "Cost Price WT" },
                { "SALEQUANTITY", "Sale Qty or Weight-in KGs" },
                { "TOTALCOSTPRICEWOT", "Total Cost Price WOT" },
                { "TOTALCOSTPRICETAX", "Total Cost Price Tax" },
                { "TOTALCOSTPRICEWT", "Total Cost Price WT" },
                { "TOTALSALEPRICEWOT", "Total Sale Price WOT" },
                { "TOTALSALETAX", "Total Sale Price Tax" },
                { "TOTALSALEPRICEWT", "Total Sale Price WT" },
                { "PROFITMARGINWOT", "Profit Margin WOT" },
                { "PROFITMARGINPERWOT", "Profit Margin % WOT" },
                { "PROFITMARGINWT", "Profit Margin WT" },
                { "PROFITMARGINPERWT", "Profit Margin % WT" }
            };

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);

            IncludeSettingsCollection = new List<IncludeSettings> { new IncludeSettings("Include Branch Name", "IncludeBranch", new List<string> { }) };
        }

        private void ucItemWise_Load(object sender, EventArgs e)
        {
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
            return GetReportData("USP_RPT_PROFITABILITY_SKUWISE", parameters);
        }
    }
}
