using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.POS
{
    public partial class ucDayClosureList : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        List<string> buttonColumns;
        List<string> summaryColumns;

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override IEnumerable<string> ButtonColumns => buttonColumns;

        public override IEnumerable<string> TotalSummaryFields => summaryColumns;

        public ucDayClosureList()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
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

            buttonColumns = new List<string>() { "Summary", "Bills", "Items", "Refunds" };
            summaryColumns = new List<string>() { "OPENINGBALANCE", "REFUNDAMOUNT", "CLOSINGBALANCE", "CLOSINGDIFFERENCE" };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        public override DataTable GetData()
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
                //case "Void Items":
                //    DataSet dsVoidItems = new POSRepository().GetDayClosureVoidItems(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["DAYCLOSUREID"]);
                //    frmObj = new frmViewDCItems(dsVoidItems, false);
                //    break;
            }

            frmObj.ShowInTaskbar = false;
            frmObj.StartPosition = FormStartPosition.CenterScreen;
            frmObj.IconOptions.ShowIcon = false;
            frmObj.ShowDialog();
        }
    }
}
