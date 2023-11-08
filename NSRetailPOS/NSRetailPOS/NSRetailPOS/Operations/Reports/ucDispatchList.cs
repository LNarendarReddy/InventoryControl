using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetailPOS.Data;
using NSRetailPOS.ReportControls.ReportBase;
using NSRetailPOS.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucDispatchList : SearchCriteriaBase
    {    
        public ucDispatchList()
        {
            InitializeComponent();

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.EditValue = 16;
            cmbCategory.EnterMoveNextControl = true;

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

            ButtonColumns = new List<string>() { "View"};

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
            if (drFocusedRow["STATUS"].ToString() == "Draft")
            {
                XtraMessageBox.Show("Draft bills cannot be viewed or printed. The operation is cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DataSet ds = new StockRepository().GetDispatch(drFocusedRow["STOCKDISPATCHID"]);
            if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count <= 0)
            {
                XtraMessageBox.Show("No data returned from database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            rptDispatch rpt = new rptDispatch(ds.Tables[0], ds.Tables[1]);
            rpt.ShowPrintMarginsWarning = false;
            rpt.ShowRibbonPreview();
        }
    }
}
