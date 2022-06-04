using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.TaxBreakUp
{
    public partial class ucTaxBreakUpDayWise : SearchCriteriaBase
    {
        private Dictionary<string, string> specificColumnHeaders;

        public override Control FirstControl => dtFromDate;

        public override Control LastControl => dtToDate;

        public override Dictionary<string, string> SpecificColumnHeaders => specificColumnHeaders;

        public ucTaxBreakUpDayWise()
        {
            InitializeComponent();

            dtFromDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            dtToDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);

            specificColumnHeaders = new Dictionary<string, string>() 
            {
                { "DAYSEQ", "Date" },
                { "TOTALBILLEDAMOUNTWOT", "Total Billed Amt w/o Tax" },
                { "TOTALTAX", "Total Tax" },
                { "TOTALBILLEDAMOUNTWT", "Total Billed Amt with Tax" }
            };
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
