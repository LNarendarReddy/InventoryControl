﻿using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms
{
    public partial class ucBranchIndent : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        List<BaseEdit> mandatoryFields;

        public ucBranchIndent()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "SALEQUANTITY", "Sale Quantity" }
                , { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
            };

            mandatoryFields = new List<BaseEdit>() { cmbBranch, cmbCategory, dtFromDate, dtToDate };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            return GetReportData("USP_R_BRANCHINDENT", parameters);
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override IEnumerable<BaseEdit> MandatoryFields => mandatoryFields;

        public override Control FirstControl => cmbBranch;

        public override Control LastControl => dtToDate;
    }
}