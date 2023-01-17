using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucBranchTransactions : SearchCriteriaBase
    {
        private string ReportType = string.Empty;
        private Dictionary<string, string> procedures = new Dictionary<string, string>()
        {
            { "D","USP_RPT_DISPATCH"}
            ,{ "B","USP_RPT_BREFUND"}
            ,{ "C","USP_RPT_CREFUND"}
        };
        public ucBranchTransactions(string _ReportType)
        {
            ReportType = _ReportType;
            InitializeComponent();
            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "COSTPRICEWT", "Cost Price WT" },
                { "COSTPRICETAX", "cost Price Tax" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICEWT", "Sale Price WT" },
                { "SALEPRICETAX", "Sale Price TAX" },
                { "TOTALCPEWOT", "Total CP WOT" },
                { "TOTALCPWT", "Total CP WT" },
                { "TOTALCPTAX", "Total CP TAX" },
                { "TOTALSPWOT", "Total SP WOT" },
                { "TOTALSPWT", "Total SP WT" },
                { "TOTALSPTAX", "Total SP TAX" }
                , { "BREFUNDNUMBER", "Branch Refund #" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP",
                    "COSTPRICEWOT", "COSTPRICEWT", "COSTPRICETAX", "SALEPRICEWOT", "SALEPRICEWT", "SALEPRICETAX", "QUANTITY" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" },true)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
            };

            if (ReportType == "B")
            {
                IncludeSettingsCollection.Add(new IncludeSettings("Reason", "IncludeReason", new List<string> { "Reason" }));
                IncludeSettingsCollection.Add(new IncludeSettings("Refund Number", "IncludeRefundNumber", new List<string> { "BREFUNDNUMBER" }));
            }

            SetFocusControls(cmbPeriodicity, dtpToDate, specificColumnHeaders);
        }
        private void ucDispatches_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbBranch.Properties.DataSource = masterRepo.GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);
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

            return GetReportData(procedures[ReportType], parameters);
        }
    }
}
