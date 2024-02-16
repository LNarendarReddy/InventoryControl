using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucOfferReport : SearchCriteriaBase
    {
        public ucOfferReport()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "PERIODICITY", "Periodicity" },
                { "ACTUALSALEPRICE", "Actual Sale Price" },
                { "OFFERTYPENAME", "Offer Type" },
                { "OFFERNAME", "Offer Name" },
                { "STARTDATE", "Start Date" },
                { "ENDDATE", "End Date" },
                { "ISACTIVE", "Is Active" },
                { "OFFERTHRESHOLD", "Threshold" },
                { "PERIODSALEQUANTITY", "Sale Qty" },
            };

            HiddenColumns = new List<string>() { "TOTALSALEQTYORWGHT" };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODICITY" }, false)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{
                        "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE"
                        , "ACTUALSALEPRICE", "OFFERTHRESHOLD" }, true)
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" }, false)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("Sub Classification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })
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

            return GetReportData("USP_RPT_OFFER_THRESHOLD", parameters);
        }
    }
}
