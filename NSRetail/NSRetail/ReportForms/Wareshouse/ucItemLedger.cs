﻿using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse
{
    public partial class ucItemLedger : SearchCriteriaBase
    {
        public ucItemLedger()
        {
            InitializeComponent();

            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>()
            {
                { "TRANSACTIONDATE", "Transaction Date"},
                { "CREDIT", "Credited Qty or Weight In KGs"},
                { "DEBIT", "Debited Qty or Weight In KGs"},
                { "TRANSACTIONTYPE", "Transaction Type"},
                { "RUNNINGBAL", "Running Qty or Weight In KGs"}
            };

            SetFocusControls(sluItemCode, dtpToDate, columnHeaders);
            MandatoryFields = new List<BaseEdit>() { sluItemCode, cmbBranch };
            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Branch", "IncludeBranch", new List<string>() { "BRANCHNAME" })
            };
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "ItemCodeID", sluItemCode.EditValue }
            };

            return GetReportData("USP_RPT_ITEMLEDGER", parameters);
        }
    }
}