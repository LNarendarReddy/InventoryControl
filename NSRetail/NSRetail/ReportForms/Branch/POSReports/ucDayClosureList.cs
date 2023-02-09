using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class ucDayClosureList : SearchCriteriaBase
    {
        public ucDayClosureList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch" }
                , { "COUNTERNAME", "Counter" }
                , { "CLOSUREDATE", "Closure Date" }
                , { "OPENINGBALANCE", "Billed Amount" }
                , { "REFUNDAMOUNT", "Refund Amount" }
                , { "DAYTURNOVER", "Day Turn-over" }
                , { "CLOSINGBALANCE", "Physical Amount" }
                , { "CLOSINGDIFFERENCE", "Difference" }
                , { "CLOSEDBY", "User" }
            };

            ButtonColumns = new List<string>() { "Summary", "Bills", "Items", "Refunds","Void Items" };

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
            return GetReportData("USP_R_DAYCLOSURE", parameters);
        }        

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            XtraForm frmObj = null;
            switch (buttonText)
            {
                case "Summary":
                    DataTable dtDayClosureSummary = new POSRepository().GetDayClosureDetail(drFocusedRow["DAYCLOSUREID"], drFocusedRow["BRANCHCOUNTERID"]);
                    frmObj = new frmDayClosureSummary(dtDayClosureSummary);
                    break;
                case "Bills":
                    DataTable dtBills = new POSRepository().GetDayClosureBills(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["DAYCLOSUREID"]);
                    frmObj = new frmViewDCBills(dtBills, drFocusedRow["BRANCHCOUNTERID"]);
                    break;
                case "Items":
                    DataSet dsItems = new POSRepository().GetDayClosureItems(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["DAYCLOSUREID"]);
                    frmObj = new frmViewDCItems(dsItems, false);
                    break;
                case "Refunds":
                    DataSet dsRefunds = new POSRepository().GetDayClosureRefund(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["DAYCLOSUREID"]);
                    frmObj = new frmViewDCItems(dsRefunds, false, true);
                    break;
                case "Void Items":
                    DataSet dsVoidItems = new POSRepository().GetDayClosureVoidItems(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["DAYCLOSUREID"]);
                    frmObj = new frmViewDCItems(dsVoidItems, false);
                    break;
            }

            frmObj.ShowInTaskbar = false;
            frmObj.StartPosition = FormStartPosition.CenterScreen;
            frmObj.IconOptions.ShowIcon = false;
            frmObj.ShowDialog();
        }

        private void btnProcessDayClosures_Click(object sender, EventArgs e)
        {
            DataTable dtProcessedCount = (DataTable)GetReportData("USP_P_DAYCLOSURES", new Dictionary<string, object>());
            if(dtProcessedCount != null && dtProcessedCount.Rows.Count == 1 &&
                int.TryParse(dtProcessedCount.Rows[0][0]?.ToString(), out int count)
                && XtraMessageBox.Show($"No. of day closures processed : {count}" 
                    + (count > 0 ? "Do you want to refresh data?" : string.Empty)
                    , "Process day closure result"
                    , count == 0 ? MessageBoxButtons.OK : MessageBoxButtons.YesNo
                    , count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Question) == DialogResult.Yes)
            {
                (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
            }
        }
    }
}
