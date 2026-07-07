using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucBRefundWHReciept : SearchCriteriaBase
    {
        public ucBRefundWHReciept()
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
                { "TOTALSPTAX", "Total SP TAX" },
                { "BREFUNDNUMBER", "Branch Refund #" },
                { "REFUNDSTATUS", "Refund Status" },
                { "APPROVEDBY", "Approved by" },
                { "APPROVEDDATE", "Approved Date" },
                { "HSNCODE", "HSN Code" },
                { "TRAYNUMBER", "Tray #" },
                { "CUSTOMERNAME", "Customer Name" },
                { "CUSTOMERNUMBER", "Customer Phone #" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" }, true)

                , new IncludeSettings("Item details", "IncludeItem", new List<string>{
                    "SKUCODE", "VENDORSKUCODE", "ITEMNAME", "ITEMCODE", "HSNCODE", "MRP", "GSTCODE",
                    "QUANTITY", "COSTPRICEWOT", "COSTPRICETAX", "COSTPRICEWT",
                    "TOTALCPWOT", "TOTALCPTAX", "TOTALCPWT",
                    "APPROVEDBY", "APPROVEDDATE", "TRAYNUMBER" })

                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "ReceivingBranch", "SourceBranch" }, true)

                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })

                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })

                , new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" })

                , new IncludeSettings("Manufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })

                , new IncludeSettings("Reason", "IncludeReason", new List<string> { "Reason" })

                , new IncludeSettings("Refund Number", "IncludeRefundNumber", new List<string> { "BREFUNDNUMBER" })
            };
        }

        private void ucBRefundWHReciept_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
                , { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_WAREHOUSE_BREFUND_RECEIPT", parameters);
        }
    }
}
