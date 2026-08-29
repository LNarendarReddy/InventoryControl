using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucDealsTrackingReport : SearchCriteriaBase
    {
        public ucDealsTrackingReport()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "PERIODICITY", "Periodicity" },
                { "DEALNAME", "Deal Name" },
                { "DEALCODE", "Deal Code" },
                { "DEALTYPENAME", "Deal Type" },
                { "STARTDATE", "Start Date" },
                { "ENDDATE", "End Date" },
                { "SOLDQUANTITY", "Sold Qty" },
                { "SOLDWEIGHTINKGS", "Sold Weight in Kgs" },
                { "DEALSALEVALUE", "Deal Sale Value" },
                { "DISCOUNT", "Discount" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" }
            };

            HiddenColumns = new List<string>()
            {
                "DEALID",
                "DEALTYPEID",
                "ITEMID",
                "ITEMPRICEID",
                "ITEMCODEID"
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODICITY" }, false),
                new IncludeSettings("Deal", "IncludeDeal", new List<string>{ "DEALNAME", "DEALCODE", "DEALTYPENAME", "STARTDATE", "ENDDATE" }, true),
                new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE" }, true),
                new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" }, false),
                new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" }),
                new IncludeSettings("Manufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            BindDealTypes();
            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate);
            SetFocusControls(cmbPeriodicity, cmbDealType, specificColumnHeaders);
        }

        private void BindDealTypes()
        {
            DataTable dtDealTypes = new OfferRepository().GetOfferType(2);
            DataRow drAll = dtDealTypes.NewRow();
            drAll["OFFERTYPEID"] = 0;
            drAll["OFFERTYPENAME"] = "All";
            dtDealTypes.Rows.InsertAt(drAll, 0);

            cmbDealType.Properties.DataSource = dtDealTypes;
            cmbDealType.Properties.ValueMember = "OFFERTYPEID";
            cmbDealType.Properties.DisplayMember = "OFFERTYPENAME";
            cmbDealType.EditValue = 0;
            cmbDealType.EnterMoveNextControl = true;
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue },
                { "FromDate", dtpFromDate.EditValue },
                { "ToDate", dtpToDate.EditValue },
                { "Periodicity", cmbPeriodicity.EditValue },
                { "DealTypeID", cmbDealType.EditValue }
            };

            return GetReportData("USP_RPT_DEALS_TRACKING", parameters);
        }
    }
}
