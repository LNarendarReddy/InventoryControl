using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.POS
{
    public partial class ucCustomerReturns : SearchCriteriaBase
    {
        public ucCustomerReturns()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "REFUNDDATE", "Refund Date" }
                , { "BILLNUMBER", "Bill Number" }
                , { "REFUNDQUANTITY", "Quantity" }
                , { "REFUNDWEIGHTINKGS", "Weight In Kgs" }
                , { "REFUNDAMOUNT", "Refund Amount" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetFocusControls(cmbBranch, chkIncludeBranch, columnHeaders);
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "IncludeBillNo", chkIncludeBillNumber.EditValue }
                , { "IncludeBranch", chkIncludeBranch.EditValue }
                , { "IncludeRefundDate", chkIncludeDate.EditValue }
            };
            DataTable dt = (DataTable)GetReportData("USP_RPT_CUSTOMERRETURNS", parameters);

            if (chkIncludeBillNumber.EditValue.Equals(false)) dt.Columns.Remove("BILLNUMBER");
            if (chkIncludeBranch.EditValue.Equals(false)) dt.Columns.Remove("BRANCHNAME");
            if (chkIncludeDate.EditValue.Equals(false)) dt.Columns.Remove("REFUNDDATE");
            return dt;
        }
    }
}
