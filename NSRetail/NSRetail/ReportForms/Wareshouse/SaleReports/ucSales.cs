using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucSales : SearchCriteriaBase
    {
        private Dictionary<string, string> specificColumnHeaders;
        public override Dictionary<string, string> SpecificColumnHeaders => specificColumnHeaders;

        public override Control FirstControl => cmbPeriodicity;

        public override Control LastControl => dtpToDate;

        public ucSales()
        {
            InitializeComponent();

            specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TOTALSALEPRICEWOT", "Total Sale Price WOT" },
                { "TOTALSALETAX", "Total Sale Price Tax" },
                { "TOTALSALEPRICEWT", "Total Sale Price WT" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICETAX", "Sale Price Tax" },
                { "SALEQUANTITY", "Sale Quantity" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
            };
        }

        private void ucSales_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbBranch.Properties.DataSource = masterRepo.GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetPeriodicty(cmbPeriodicity, true);
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
            };

            return GetReportData("USP_RPT_SALES", parameters);
        }
    }
}
