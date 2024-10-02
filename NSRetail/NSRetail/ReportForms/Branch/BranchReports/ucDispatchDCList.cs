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

            ContextmenuItems = new Dictionary<string, string>
            { 
                { "View items", "CE8B8A21-12C8-4AFD-B246-750110BDE248" },
                //{ "Print to DM", "DFEE4755-1975-4BA0-9478-F9BAA6E25C5E" } 
                { "View items with CP", "62331050-12E0-46BD-997F-48BF7B755F84" }
            };

            cmbCategory.Properties.DataSource = Utility.GetCategoryList();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.EditValue = 13;


            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtpToDate, columnHeaders);
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

            rptDispatchDC rpt;

            switch (buttonText)
            {
                case "View items":
                    rpt = new rptDispatchDC(ds.Tables[0], ds.Tables[1], false);
                    rpt.ShowRibbonPreview();
                    break;
                case "View items with CP":
                    rpt = new rptDispatchDC(ds.Tables[0], ds.Tables[1], true);
                    rpt.ShowRibbonPreview();
                    break;
                case "Print to DM":
                    Utilities.DotMatrixPrintHelper.PrintDC(ds);
                    break;

            }
        }
    }
}
