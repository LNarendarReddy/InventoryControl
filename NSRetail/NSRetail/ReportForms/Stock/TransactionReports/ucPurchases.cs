using DataAccess;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucPurchases : SearchCriteriaBase
    {
        public ucPurchases()
        {
            InitializeComponent();
            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TOTALCPEWOT", "Total Cost Price WOT" },
                { "TOTALTAX", "Total Tax" },
                { "TOTALCPWT", "Total Cost Price WT" },
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "COSTPRICETAX", "Tax" },
                { "COSTPRICEWT", "Cost Price WT" },
                { "FINALPRICE", "Final Price" },
                { "PURCHASEQUANTITY", "Purchase Quantity" },
                { "GSTCODE", "GST Code" },
                { "SUPPLIERINVOICENO", "Invoice #" },
                { "DISCOUNTFLAT", "Discount flat" },
                { "DISCOUNTPERCENTAGE", "Discount %" },
                { "SCHEMEFLAT", "Scheme flat" },
                { "SCHEMEPERCENTAGE", "Scheme %" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" },
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "COSTPRICEWOT", "COSTPRICEWT", "COSTPRICETAX", "PURCHASEQUANTITY" })
                , new IncludeSettings("Supplier", "IncludeSupplier", new List<string>{ "DEALERNAME", "SUPPLIERINVOICENO" },true)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Tax wise", "IncludeTax", new List<string>{ "GSTCODE" })
                , new IncludeSettings("Discount & scheme details", "IncludeDiscount", new List<string> { "DISCOUNTFLAT", "DISCOUNTPERCENTAGE", "SCHEMEFLAT", "SCHEMEPERCENTAGE" })
                , new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" })
                , new IncludeSettings("SubManufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })
            };

            SetFocusControls(cmbPeriodicity, cmbItemCode, specificColumnHeaders);
        }

        private void ucPurchases_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbSupplier.Properties.DataSource = masterRepo.GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);            
        }
        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbSupplier.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
                , { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_PURCHASES", parameters);
        }
    }
}
