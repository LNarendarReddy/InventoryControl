using DataAccess;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucDispatchDCList : SearchCriteriaBase
    {
        public ucDispatchDCList()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "DISPATCHDCID", "DispatchDCID" }
                , { "DISPATCHDCNUMBER", "DC Number" }
                , { "BRANCHCODE", "Branch Code" }
                , { "BRANCHNAME", "Branch Name" }
                , { "CATEGORYNAME", "Category" }
                , { "CREATEDBY", "User Name" }
                , { "CREATEDDATE", "Created Date" }
                , { "STATUS", "Status" }
            };

            ContextmenuItems = new List<string>() { "View", "Print to DM" };
            cmbCategory.Properties.DataSource = Utility.GetCategoryList();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.EditValue = 13;


            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtpToDate, columnHeaders);
            AllowedRoles = new List<string> { "Division User", "IT User" };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_R_DISPATCHDCLIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            DataSet ds = new StockRepository().GetDispatchDC(drFocusedRow["DISPATCHDCID"]);
            if (ds == null || ds.Tables.Count < 2)
            {
                return;
            }

            switch (buttonText)
            {
                case "View":
                    rptDispatchDC rpt = new rptDispatchDC(ds.Tables[0], ds.Tables[1]);
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowRibbonPreview();
                    break;
                case "Print to DM":
                    Utilities.DotMatrixPrintHelper.PrintDC(ds);
                    break;

            }
        }
    }
}
