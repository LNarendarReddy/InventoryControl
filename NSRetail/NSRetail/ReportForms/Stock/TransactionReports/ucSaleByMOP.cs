using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucSaleByMOP : SearchCriteriaBase
    {
        public ucSaleByMOP()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "MOPNAME", "Payment Method" },
                { "BILLDATE", "Bill Date" },
                { "AMOUNT", "Paid Amount" },
            };

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_DAYSALE_BY_MOP", parameters);
        }
    }
}
