using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucLoginHistory : SearchCriteriaBase
    {
        public ucLoginHistory()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "FULLNAME", "Full name" }
                , { "USERNAME", "User name" }
                , { "LOGINDATE", "Login date" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(dtpFromDate, dtpToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue },
                { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_LOGINHISTORY", parameters);
        }
    }
}
