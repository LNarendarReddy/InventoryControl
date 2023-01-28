using DataAccess;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucStockAdjustment : SearchCriteriaBase
    {
        public ucStockAdjustment()
        {
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

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
        }

        private void ucStockAdjustment_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "ItemCodeID", sluItemCode.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_RPT_STOCKADJUSTMENT", parameters);
        }
    }
}
