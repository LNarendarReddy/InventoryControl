﻿
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.POS
{
    public partial class ucBranchRefunds : SearchCriteriaBase
    {

        private Dictionary<string, string> specificColumnHeaders;

        private List<string> buttonColumns;

        public override Dictionary<string, string> SpecificColumnHeaders => specificColumnHeaders;

        public override IEnumerable<string> ButtonColumns => buttonColumns;

        public override Control FirstControl => cmbBranch;

        public override Control LastControl => dtpToDate;

        public ucBranchRefunds()
        {
            InitializeComponent();

            specificColumnHeaders = new Dictionary<string, string>() 
                { 
                    { "BREFUNDNUMBER", "Refund Number" }
                    , { "CREATEDBY", "Created By" }
                    , { "CREATEDATE", "Created Date" }
                    , { "STATUS", "Status" }
                };

            buttonColumns = new List<string>() { "View" };
        }

        private void ucBranchRefunds_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_R_BREFUND", parameters);
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
                drFocusedRow["IsAccepted"] = true;
                //gvBRefund.SetRowCellValue(gvBRefund.FocusedRowHandle, "IsAccepted", true);
                //gvBRefund.SetRowCellValue(gvBRefund.FocusedRowHandle, "STATUS", "Accepted");
            }
        }
    }
}