﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucItemWiseSales : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override Control FirstControl => cmbBranch;
        public override Control LastControl => chkIncludeBranch;

        public ucItemWiseSales()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "SALEQUANTITY", "Sale Quantity" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
                , { "BILLDATE", "Bill Date" }
                , { "DEALERNAME", "Supplier" }
                , { "ACTUALBILLEDAMOUNT", "Billed Amount" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }

        public override DataTable GetData()
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
            DataTable dt = GetReportData("USP_RPT_ITEMWISESALE", parameters);
            if (chkIncludeBillNo.EditValue.Equals(false)) dt.Columns.Remove("BILLNUMBER");
            if (chkIncludeBranch.EditValue.Equals(false)) dt.Columns.Remove("BRANCHNAME");
            if (chkIncludeDate.EditValue.Equals(false)) dt.Columns.Remove("BILLDATE");
            return dt;
        }
    }
}