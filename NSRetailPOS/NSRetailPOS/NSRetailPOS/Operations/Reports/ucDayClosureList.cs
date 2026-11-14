using NSRetailPOS.ReportControls.ReportBase;
using System;
using System.Collections.Generic;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucDayClosureList : SearchCriteriaBase
    {
        public ucDayClosureList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch" }
                , { "COUNTERNAME", "Counter" }
                , { "CLOSUREDATE", "Closure Date" }
                , { "OPENINGBALANCE", "Billed Amount" }
                , { "REFUNDAMOUNT", "Refund Amount" }
                , { "DAYTURNOVER", "Day Turn-over" }
                , { "CLOSINGBALANCE", "Physical Amount" }
                , { "CLOSINGDIFFERENCE", "Difference" }
                , { "CLOSEDBY", "User" }
                , { "VOIDAMOUNT", "Void Amount"}
            };

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
            };
            return GetReportData("USP_R_DAYCLOSURE", parameters);
        }        
    }
}
