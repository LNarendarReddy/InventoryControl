using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucItemWiseSales : SearchCriteriaBase
    {
        public ucItemWiseSales()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "SALEQUANTITY", "Sale Quantity" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
                , { "BILLDATE", "Bill Date" }
                , { "DEALERNAME", "Supplier" }
                , { "ACTUALBILLEDAMOUNT", "Billed Amount" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            SetFocusControls(cmbBranch, chkIncludeBranch, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "IncludeBillDate", chkIncludeDate.EditValue}
                , { "IncludeBillNo", chkIncludeBillNo.EditValue }
                , { "IncludeBranch", chkIncludeBranch .EditValue }
            };
            DataTable dt = (DataTable)GetReportData("USP_RPT_ITEMWISESALE", parameters);
            if (chkIncludeBillNo.EditValue.Equals(false)) dt.Columns.Remove("BILLNUMBER");
            if (chkIncludeBranch.EditValue.Equals(false)) dt.Columns.Remove("BRANCHNAME");
            if (chkIncludeDate.EditValue.Equals(false)) dt.Columns.Remove("BILLDATE");
            return dt;
        }
    }
}
