using DataAccess;
using DevExpress.PivotGrid.OLAP.AdoWrappers;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucDispatchList : SearchCriteriaBase
    {    
        public ucDispatchList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "STOCKDISPATCHID", "Stock DispatchID" }
                , { "DISPATCHNUMBER", "Dispatch Number" }
                , { "BRANCHCODE", "Branch Code" }
                , { "BRANCHNAME", "Branch Name" }
                , { "CATEGORYNAME", "Category" }
                , { "CREATEDBY", "User Name" }
                , { "CREATEDDATE", "Created Date" }
                , { "STATUS", "Status" }
            };

            ButtonColumns = new List<string>() { "View", "Print to DM", "Discard" };

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
            return GetReportData("USP_R_DISPATCHLIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            DataSet ds = new StockRepository().GetDispatch(drFocusedRow["STOCKDISPATCHID"], 
                drFocusedRow["STATUS"].ToString() == "Draft" ? true : false);
            if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data returned from database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            switch (buttonText)
            {
                case "View":
                    rptDispatch rpt = new rptDispatch(ds.Tables[0], ds.Tables[1]);
                    rpt.Parameters["stReportHeader"].Value = $"BRANCH DISPATCH REPORT({Convert.ToString(drFocusedRow["STATUS"])})";
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowRibbonPreview();
                    break;
                case "Print to DM":
                    Utilities.DotMatrixPrintHelper.PrintDispatch(ds);
                    break;
                case "Discard":
                    if (XtraMessageBox.Show("Are you sure want to discard dispatch?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes
                || Convert.ToInt32(Convert.ToString(drFocusedRow["STOCKDISPATCHID"])) == 0)
                        return;
                    new StockRepository().DiscardStockDispatch(Convert.ToString(drFocusedRow["STOCKDISPATCHID"]), Utility.UserID);
                    drFocusedRow.Table.Rows.Remove(drFocusedRow);
                    //(ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
                    break;

            }
        }
    }
}
