using DataAccess;
using DevExpress.XtraEditors;
using NSRetail.ReportForms.Branch.POSReports;
using NSRetail.ReportForms.Wareshouse.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Stock.StockCounting
{
    public partial class ucCountingApprovals : SearchCriteriaBase
    {
        public ucCountingApprovals()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch" }
            };

            ContextmenuItems = new List<string>() { "Sheets", "Items", "Item By MRP"};

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
            };
            return GetReportData("USP_R_COUNTINGAPPROVAL", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            XtraForm frmObj = null;
            switch (buttonText)
            {
                case "Sheets":
                    DataTable dtCountingSheets = new CountingRepository().GetStockCountingByAID(drFocusedRow["COUNTINGAPPROVALID"]);
                    frmObj = new frmCountingSheets(dtCountingSheets, drFocusedRow["BRANCHID"], drFocusedRow["COUNTINGAPPROVALID"]);
                    break;
                case "Items":
                    DataTable dtItems = new CountingRepository().GetStockCountingItemsByAID(drFocusedRow["COUNTINGAPPROVALID"], false);
                    frmObj = new frmViewSCItems(dtItems, false, drFocusedRow["BRANCHID"], drFocusedRow["COUNTINGAPPROVALID"]);
                    break;
                case "Item By MRP":
                    DataTable dt = new CountingRepository().GetStockCountingItemsByAID(drFocusedRow["COUNTINGAPPROVALID"], true);
                    frmObj = new frmViewSCItems(dt, true, drFocusedRow["BRANCHID"], drFocusedRow["COUNTINGAPPROVALID"]);
                    break;
            }

            frmObj.ShowInTaskbar = false;
            frmObj.StartPosition = FormStartPosition.CenterScreen;
            frmObj.IconOptions.ShowIcon = false;
            frmObj.ShowDialog();
        }
    }
}
