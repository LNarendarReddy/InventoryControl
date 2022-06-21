using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.TaxBreakUp
{
    public partial class ucTaxBreakUpDayWise : SearchCriteriaBase
    {
        public ucTaxBreakUpDayWise()
        {
            InitializeComponent();

            dtFromDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            dtToDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>() 
            {
                { "DAYSEQ", "Date" },
                { "TOTALBILLEDAMOUNTWOT", "Total Billed Amt w/o Tax" },
                { "TOTALTAX", "Total Tax" },
                { "TOTALBILLEDAMOUNTWT", "Total Billed Amt with Tax" }
            };

            SetFocusControls(dtFromDate, dtToDate, specificColumnHeaders);
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
            };
                        
            return GetReportData("USP_RPT_TAXBREAKUP_DAYWISE", parameters);
        }
    }
}
