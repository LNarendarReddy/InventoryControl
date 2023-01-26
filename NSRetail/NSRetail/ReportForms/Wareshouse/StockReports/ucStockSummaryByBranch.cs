using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucStockSummaryByBranch : SearchCriteriaBase
    {
        public ucStockSummaryByBranch()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "STOCKSUMMARYID", "Stock SummaryID" }
                , { "BRANCHNAME", "Branch Name" }
                , { "SKUCODE", "SKU Code" }
                , { "ITEMCODE", "Item Code" }
                , { "ITEMNAME", "Item Name" }
                , { "QUANTITY", "Quantity" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
                , { "CATEGORYNAME", "Category" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "MRP", "MRP" }
                , { "COSTPRICEWT", "Costprice WT" }
                , { "TOTALCOSTPRICEWT", "Total Costprice WT" }
                , { "SALEPRICE", "Saleprice" }
                , { "TOTALSALEPRICE", "Total Saleprice WT" }
            };

            luBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            luBranch.Properties.ValueMember = "BRANCHID";
            luBranch.Properties.DisplayMember = "BRANCHNAME";
            luBranch.EditValue = 0;

            sluItem.Properties.DataSource = Utility.GetItemSKUList();
            sluItem.Properties.ValueMember = "ITEMID";
            sluItem.Properties.DisplayMember = "ITEMNAME";

            SetFocusControls(luBranch, sluItem, columnHeaders);

            IncludeSettingsCollection = new List<IncludeSettings> { new IncludeSettings("Include EAN Code", "IncludeEAN", new List<string> { "ITEMCODE" }) };

        }


        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", luBranch.EditValue }
                , { "ItemID", sluItem.EditValue }
            };
            return GetReportData("USP_R_STOCKSUMMARY", parameters);
        }
    }
}
