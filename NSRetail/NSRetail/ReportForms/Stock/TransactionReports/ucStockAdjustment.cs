using DataAccess;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.TransactionReports
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
                { "COSTPRICETAX", "Cost Price Tax" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICEWT", "Sale Price WT" },
                { "SALEPRICETAX", "Sale Price TAX" },
                { "TOTALCPEWOT", "Total CP WOT" },
                { "TOTALCPWT", "Total CP WT" },
                { "TOTALCPTAX", "Total CP TAX" },
                { "TOTALSPWOT", "Total SP WOT" },
                { "TOTALSPWT", "Total SP WT" },
                { "TOTALSPTAX", "Total SP TAX" },
                { "CREATEDBY", "Created By" },
                { "PREVQUANTITY", "Prev. Qty" },
                { "ADJQUANTITY", "Adjusted Qty" },
                { "AFTERQUANTITY", "After Adjustment Qty" },
                { "CURQUANTITY", "Current Qty" },
                { "CREATETIME", "Created Time" },
                { "STOCKADJUSTMENTTYPE", "Stock Adj. type" },
                { "DESCRIPTION", "Stock Adj. description" },
                { "GSTCODE", "GST Code" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Time", "IncludeTime", new List<string>{ "CREATETIME" })
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ 
                    "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP",
                    "COSTPRICEWOT", "COSTPRICEWT", "COSTPRICETAX", "SALEPRICEWOT", "SALEPRICEWT", "SALEPRICETAX"
                    , "PREVQUANTITY", "ADJQUANTITY", "AFTERQUANTITY", "CURQUANTITY" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" },true)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Created-By", "IncludeCreatedBy", new List<string>{ "CREATEDBY" })
                , new IncludeSettings("Stock Adj. type & Description", "IncludeDescription", new List<string>{ "STOCKADJUSTMENTTYPE", "DESCRIPTION" })
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
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "ItemCodeID", sluItemCode.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
            };
            return GetReportData("USP_RPT_STOCKADJUSTMENT", parameters);
        }
    }
}
