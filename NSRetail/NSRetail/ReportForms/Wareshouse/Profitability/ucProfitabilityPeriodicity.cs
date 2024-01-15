using System;
using System.Collections.Generic;

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
                { "PERIODICITY", "Periodicity" },
                { "ACTUALSALEPRICEWOT", "Actual Sale Price WOT" },
                { "ACTUALSALEPRICETAX", "Actual Sale Price Tax" },
                { "ACTUALSALEPRICE", "Actual Sale Price" },
                { "GSTCODE", "GST Code" },
                { "SALEQUANTITY", "Sale Qty" },
                { "DEALERNAME", "Supplier Name" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODICITY" }, true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ 
                        "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE"
                        , "ACTUALSALEPRICE", "ACTUALSALEPRICEWOT", "ACTUALSALEPRICETAX", "SALEQUANTITY" 
                        , "AVGCOSTPRICEWOT", "AVGCOSTPRICETAX", "AVGCOSTPRICEWT"})
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" }, true)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("Sub Classification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })
                , new IncludeSettings("Supplier", "IncludeSupplier", new List<string>{ "DEALERNAME" })
                , new IncludeSettings("Tax wise", "IncludeTax", new List<string>{ "GSTCODE" })
            };

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate);
            SetFocusControls(cmbPeriodicity, dtpToDate, specificColumnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
            };

            return GetReportData("USP_RPT_PROFITABILITY_PERIODICITY", parameters);
        }
    }
}
