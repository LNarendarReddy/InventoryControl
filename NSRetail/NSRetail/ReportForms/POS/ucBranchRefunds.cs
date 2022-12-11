using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.POS
{
    public partial class ucBranchRefunds : SearchCriteriaBase
    {
        public ucBranchRefunds()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>() 
                { 
                    { "BREFUNDNUMBER", "Refund Number" }
                    , { "CREATEDBY", "Created By" }
                    , { "CREATEDATE", "Created Date" }
                    , { "QUANTITYORWEIGHTINKGS", "Qty or Weight in KGs" }
                    , { "STATUS", "Status" }
                    , { "COSTPRICEWOT", "Cost Price WOT" }
                    , { "COSTPRICETAX", "Cost Price Tax" }
                    , { "COSTPRICEWT", "Cost Price WT" }
                };

            ButtonColumns = new List<string>() { "View" };


            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_RPT_BREFUNDSHEET", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            DataTable dtItems = new POSRepository().GetBRefundDetail(drFocusedRow["BREFUNDID"], drFocusedRow["COUNTERID"]);
            frmBRefundDetail obj = new frmBRefundDetail(dtItems, drFocusedRow["COUNTERID"], drFocusedRow["BREFUNDID"], Convert.ToBoolean(drFocusedRow["IsAcceptedID"]))
            {
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            obj.ShowDialog();
            if (obj.IsSave)
            {
                drFocusedRow["STATUS"] = "Accepted";
                drFocusedRow["IsAcceptedID"] = true;                
            }
        }
    }
}
