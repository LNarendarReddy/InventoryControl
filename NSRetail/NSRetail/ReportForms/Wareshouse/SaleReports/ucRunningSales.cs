﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucRunningSales : SearchCriteriaBase
    {
        public override Control FirstControl => cmbBranch;
        public override Control LastControl => chkIncludeCategory;
        public ucRunningSales()
        {
            InitializeComponent();
        }

        private void ucRunningSales_Load(object sender, EventArgs e)
        {
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
                , { "IncludeBranch", chkIncludeBranch .EditValue }
                , { "IncludeCounter", chkIncludeCounter.EditValue }
                , { "IncludeBillNo", chkIncludeBillNo.EditValue }
                , { "IncludeItem", chkIncludeItem.EditValue }
                , { "IncludeCategory", chkIncludeCategory.EditValue }
            };
            return GetReportData("USP_RPT_RUNNINGSALE", parameters);
        }
    }
}