using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.POS
{
    public partial class ucCustomerDetails : SearchCriteriaBase
    {
        public ucCustomerDetails()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch" }
                , { "BRANCHCODE", "Branch Code" }
                , { "CUSTOMERNAME", "Customer Name" }
                , { "CUSTOMERNUMBER", "Customer Number" }
                , { "NOOFBILLS", "No. of Bills" }                
            };

            dtFromDate.EditValue = DateTime.Now.AddDays(-30);
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
            return GetReportData("USP_RPT_POS_CUSTOMERDETAILS", parameters);
        }
    }
}
