﻿using DataAccess;
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

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            dtFromDate.EditValue = DateTime.Now.AddDays(-30);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }

        public override DataTable GetData()
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