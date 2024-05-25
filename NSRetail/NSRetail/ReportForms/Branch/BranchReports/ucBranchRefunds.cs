using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
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
                    , { "CATEGORYNAME", "Category" }
                };

            ButtonColumns = new List<string>() { "View", "Print" };


            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
            AllowedRoles = new List<string> { "IT User" };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "SerialNumber", txtSerialNumber.EditValue }
                , { "CategoryID", Utility.CategoryID }
            };
            return GetReportData("USP_RPT_BREFUNDSHEET", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            DataTable dtItems = new POSRepository().GetBRefundDetail(drFocusedRow["BREFUNDID"], drFocusedRow["COUNTERID"]);

            switch (buttonText)
            {
                case "View":
                    frmBRefundDetail obj = new frmBRefundDetail(dtItems,
                drFocusedRow["COUNTERID"], drFocusedRow["BREFUNDID"], drFocusedRow["CATEGORYID"],
                Convert.ToBoolean(drFocusedRow["IsAcceptedID"]))
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
                    break;
                case "Print":
                    rptBRefund rpt = new rptBRefund(dtItems);
                    rpt.Parameters["Address"].Value = drFocusedRow["BRANCHNAME"];
                    rpt.Parameters["BillDate"].Value = drFocusedRow["CREATEDATE"];
                    rpt.Parameters["BillNumber"].Value = drFocusedRow["BREFUNDNUMBER"];
                    rpt.Parameters["Phone"].Value = "NA";
                    rpt.Parameters["UserName"].Value = drFocusedRow["CREATEDBY"];
                    rpt.Parameters["CounterName"].Value = drFocusedRow["COUNTERNAME"];
                    rpt.ShowPrintStatusDialog = false;
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowPreviewMarginLines = false;
                    rpt.ShowRibbonPreview();
                    break;

            }

        }
    }
}
