using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Wareshouse.StockAndSale
{
    public partial class ucStockAndSales : SearchCriteriaBase
    {
        public ucStockAndSales()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "OPENINGQTYORWGHT", "Opening Qty/Wght" },
                { "DISPATCHQTYORWGHT", "Dispatch Qty/Wght" },
                { "BRQTYORWGHT", "Branch refund Qty/Wght" },
                { "SALEQTYORWGHT", "Sale Qty/Wght" },
                { "CLOSINGQTYORWGHT", "Closing Qty/Wght" },
                { "OPENINGCPWOT", "Opening CP WOT" },
                { "DISPATCHCPWOT", "Dispatch CP WOT" },
                { "BRCPWOT", "Branch refund CP WOT" },
                { "SALECPWOT", "Sale CP WOT" },
                { "CLOSINGCPWOT", "Closing CP WOT" },
                { "GSTCODE", "Tax %" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" },
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Item details", "IncludeItem", 
                    new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE", "COSTPRICEWOT", "OPENINGQTYORWGHT"
                        , "DISPATCHQTYORWGHT" , "BRQTYORWGHT" , "SALEQTYORWGHT" , "CLOSINGQTYORWGHT"})
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" },true)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("Sub Classification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })
                , new IncludeSettings("Tax wise", "IncludeGST", new List<string>{ "GSTCODE" })
                , new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" })
                , new IncludeSettings("SubManufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })
            };

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
        }

        private void ucStockAndSales_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_STOCK_AND_SALES", parameters);
        }
    }
}
