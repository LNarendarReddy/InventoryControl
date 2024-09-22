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
                , { "VOIDAMOUNT", "Void Amount"}
            };

            ContextmenuItems = new Dictionary<string, string> 
            { 
                { "Summary", "C500371E-309E-43CE-AAAE-C9D269790606" }, 
                { "Bills", "4A5FF1E8-AD9B-4F1A-B65F-9FE3989D2CF3" }, 
                { "Items", "6B55A259-F03E-4D6E-9F89-A01B8F4AF3A5" }, 
                { "Refunds", "BC42D348-24DA-462A-BF53-445F092A73B4" },
                { "Void Items", "FA0424F9-56F0-4104-ADBC-FCE826647663" } 
            };

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
                    frmObj = new frmViewDCItems(dsItems, false, false);
                    break;
                case "Refunds":
                    DataSet dsRefunds = new POSRepository().GetDayClosureRefund(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["DAYCLOSUREID"]);
                    frmObj = new frmViewDCItems(dsRefunds, false, true);
                    break;
                case "Void Items":
                    DataSet dsVoidItems = new POSRepository().GetDayClosureVoidItems(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["DAYCLOSUREID"]);
                    frmObj = new frmViewDCItems(dsVoidItems, false, false, true);
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
