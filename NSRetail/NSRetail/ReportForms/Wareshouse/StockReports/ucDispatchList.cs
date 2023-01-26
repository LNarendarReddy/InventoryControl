using DataAccess;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.StockReports
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

            ButtonColumns = new List<string>() { "View", "Print to DM" };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
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
            return GetReportData("USP_R_DISPATCHLIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            DataSet ds = new StockRepository().GetDispatch(drFocusedRow["STOCKDISPATCHID"]);
            if (ds == null && ds.Tables.Count < 2)
            {
                return;
            }

            switch (buttonText)
            {
                case "View":
                    rptDispatch rpt = new rptDispatch(ds.Tables[0], ds.Tables[1]);
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowRibbonPreview();
                    break;
                case "Print to DM":
                    Utilities.DotMatrixPrintHelper.PrintDispatch(ds);
                    break;

            }
        }
    }
}
