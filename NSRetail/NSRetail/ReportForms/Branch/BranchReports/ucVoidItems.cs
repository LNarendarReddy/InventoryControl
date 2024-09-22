using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Branch
{
    public partial class ucVoidItems : SearchCriteriaBase
    {
        public ucVoidItems()
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
                { "DELETEDTIME", "Deleted time" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("User", "IncludeUser", new List<string>{ "BILLCREATEDBY"}, true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>
                        { 
                            "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE", "SALEPRICEWOT", "SALEPRICETAX", "SALEQUANTITY"
                            , "HSNCODE", "GSTCODE", "ISOFFER", "OFFERCODE", "BASEOFFERCODE", "CREATEDTIME", "DELETEDTIME"
                        })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" },true)
                , new IncludeSettings("Counter", "IncludeCounter", new List<string>{ "COUNTERNAME" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("Sub Classification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })                
                , new IncludeSettings("Bill details", "IncludeBillDetails", new List<string>
                    { "BILLNUMBER", "ISDOORDELIVERY", "CUSTOMERNAME", "CUSTOMERNUMBER", "CUSTOMERGST" })
            };

            HiddenColumns = new List<string>() { "" };

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
            };

            return GetReportData("USP_RPT_VOIDITEMS", parameters);
        }

    }
}
