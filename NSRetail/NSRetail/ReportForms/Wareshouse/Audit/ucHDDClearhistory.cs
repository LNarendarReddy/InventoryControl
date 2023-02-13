using DataAccess;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucHDDClearhistory : SearchCriteriaBase
    {
        public ucHDDClearhistory()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "CLEAREDBY", "Cleared By" }
                , { "CLEAREDDATE", "Cleared on" }
                , { "CLEAREDTIME", "Cleared at" }
                , { "REASON", "Reason" }
            };

            SetFocusControls(null, null, columnHeaders);
        }

        public override object GetData()
        {
            return new ReportRepository().GetReportData("USP_RPT_HDDCLEARHISTORY", null, true);
        }
    }
}
