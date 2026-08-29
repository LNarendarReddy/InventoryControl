using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class ucOfferThreshold : SearchCriteriaBase
    {
        public ucOfferThreshold()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {               
                { "OFFERTYPENAME", "Offer Type" },
                { "OFFERNAME", "Offer Name" },
                { "STARTDATE", "Start Date" },
                { "ENDDATE", "End Date" },
                { "OFFERTHRESHOLD", "Threshold" },
                { "SALEQUANTITY", "Sale Qty" },
                { "BALANCESALE", "Balance Sale" },
                { "OFFERVALUE", "Offer Value" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" },
            };

            SetFocusControls(null, null, specificColumnHeaders);
        }

        public override object GetData()
        {
            return GetReportData("USP_RPT_OFFER_THRESHOLD", null);
        }
    }
}
