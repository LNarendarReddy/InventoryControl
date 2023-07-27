using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Wareshouse.TaxBreakUp
{
    public partial class ucTaxBreakUpPaymentWise : SearchCriteriaBase
    {
        public ucTaxBreakUpPaymentWise()
        {
            InitializeComponent();

            dtFromDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            dtToDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "BILLDATE", "Date" },
                { "MOPNAME", "Payment mode" },
            };

            SetFocusControls(dtFromDate, dtToDate, specificColumnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
            };

            return GetReportData("USP_RPT_GST_BY_PAYMENT_MODE", parameters);
        }
    }
}
