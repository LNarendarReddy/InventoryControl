using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucBulkProcessing : SearchCriteriaBase
    {
        public ucBulkProcessing()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "CREATEDTIME", "Created Time" }
                , { "DESCRIPTION", "Reason" }
            };

            SetFocusControls(dtpFromDate, dtpToDate, columnHeaders);
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MandatoryFields = new List<BaseEdit> { dtpFromDate, dtpToDate };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Parent Items", "IncludeParentItems", new List<string>{  }, false)
            };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_PROCESSING", parameters);
        }
    }
}
