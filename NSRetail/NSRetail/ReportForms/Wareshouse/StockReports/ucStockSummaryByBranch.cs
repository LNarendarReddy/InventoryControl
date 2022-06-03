using DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucStockSummaryByBranch : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public ucStockSummaryByBranch()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "STOCKSUMMARYID", "Stock SummaryID" }
                , { "BRANCHNAME", "Branch Name" }
                , { "SKUCODE", "SKU Code" }
                , { "ITEMCODE", "Item Code" }
                , { "ITEMNAME", "Item Name" }
                , { "QUANTITY", "Quantity" }
                , { "INTRANSITQUANTITY", "In-transit Quantity" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
                , { "INTRANSITWEIGHTINKGS", "In-Transit Weight In Kgs" }
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
        }
        
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override Control FirstControl => luBranch;

        public override Control LastControl => sluItem;

        public override DataTable GetData()
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
