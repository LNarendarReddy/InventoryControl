using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucDispatchDifferences : SearchCriteriaBase
    {        
        public ucDispatchDifferences()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "RECEIVEDQUANTITY", "Recieved Quantity" }
                , { "STOCKDIFF", "Stock Difference" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, cmbCategory, dtFromDate, dtToDate };

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            return GetReportData("USP_RPT_DISPATCHDIFF", parameters);
        }
    }
}
