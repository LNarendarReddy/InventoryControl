using DevExpress.XtraGrid.Columns;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucSales : SearchCriteriaBase
    {
        public ucSales()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TOTALSALEPRICEWOT", "Total Sale Price WOT" },
                { "TOTALSALETAX", "Total Sale Price Tax" },
                { "TOTALSALEPRICEWT", "Total Sale Price WT" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICETAX", "Sale Price Tax" },
                { "SALEQUANTITY", "Sale Quantity" },
                { "ISOFFER", "Item Offer applied" },
                { "WHSTOCK", "Warehouse Stock" },
                { "BRANCHSTOCK", "Branch Stock" },
                { "GSTCODE", "GST Code" },
                { "OFFERCODE", "Offer Code" },
                { "BASEOFFERCODE", "Base Offer Code" },
                { "BILLCREATEDBY", "Bill Created By" },
                { "CREATEDTIME", "Created Time" },
                { "ISDOORDELIVERY", "Is Door Delivery?" },
                { "CUSTOMERNAME", "Customer Name" },
                { "CUSTOMERNUMBER", "Customer #" },
                { "CUSTOMERGST", "Customer GST" },
                { "HSNCODE", "HSN Code" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME" })
                , new IncludeSettings("Item Price details", "IncludeItemPrice", new List<string>{ "ITEMCODE", "MRP", "SALEPRICE", "SALEPRICEWOT", "SALEPRICETAX", "HSNCODE" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" },true)
                , new IncludeSettings("Counter", "IncludeCounter", new List<string>{ "COUNTERNAME" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("Sub Classification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })
                , new IncludeSettings("Stock", "IncludeStock", new List<string>{ "WHSTOCK", "BRANCHSTOCK" })
                , new IncludeSettings("Is offer", "IncludeOffer", new List<string>{ "ISOFFER", "OFFERCODE", "BASEOFFERCODE" })
                , new IncludeSettings("Tax wise", "IncludeTax", new List<string>{ "GSTCODE" })                
                , new IncludeSettings("Bill details", "IncludeBillDetails", new List<string>
                    { "BILLCREATEDBY", "BILLNUMBER", "CREATEDTIME", "ISDOORDELIVERY", "CUSTOMERNAME", "CUSTOMERNUMBER", "CUSTOMERGST" })
                , new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" })
                , new IncludeSettings("SubManufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })
            };

            SetFocusControls(cmbPeriodicity, cmbItemCode, specificColumnHeaders);
        }
        private void ucSales_Load(object sender, EventArgs e)
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
                , { "UnmaskCustomerPhone", AccessUtility.HasAccess("825FBF84-8FEB-4B5F-AB33-561A4751A76A::Execute") }
            };

            return GetReportData("USP_RPT_SALES", parameters);
        }

        public override void DataBoundCompleted()
        {
            GridColumn colSaleQuantity = ResultGridView.Columns["SALEQUANTITY"];
            if (colSaleQuantity != null)
            {
                colSaleQuantity.Visible = IncludeSettingsCollection.First(x => x.ParameterName == "IncludeItem").Included ||
                    IncludeSettingsCollection.First(x => x.ParameterName == "IncludeItemPrice").Included;
            }
        }
    }
}
