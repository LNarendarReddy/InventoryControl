using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using ErrorManagement;
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

            ContextmenuItems = new Dictionary<string, string>
            { 
                {"View", "6512E156-9D65-4C12-A925-6F6F215D99EB" }, 
                {"Print", "FDF97A4E-9DAD-4505-8211-77B6273C424F" },
                { "Delete (draft only)", "91CE5798-AD02-4FB1-A1EE-5D901DC2F26B" }
            };

            HiddenColumns = new List<string> { "ISDRAFT" };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            IncludeSettingsCollection = new List<IncludeSettings>
            {
                { new IncludeSettings("Drafts", "IncludeDrafts", new List<string>(), false) }
            };

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);            
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
            return GetReportData("USP_RPT_BREFUNDSHEET_1", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            if (buttonText == "Delete (draft only)")
            {
                DeleteBranchRefund(drFocusedRow);
                return;
            }

            DataTable dtItems = new POSRepository().GetBRefundDetail(drFocusedRow["BRID"]);

            switch (buttonText)
            {
                case "View":
                    frmBRefundDetail obj = new frmBRefundDetail(dtItems, drFocusedRow["BRID"], Convert.ToBoolean(drFocusedRow["IsAcceptedID"]))
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
                    rpt.ShowPrintStatusDialog = false;
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowPreviewMarginLines = false;
                    rpt.ShowRibbonPreview();
                    break;                
            }
        }

        private void DeleteBranchRefund(DataRow drFocusedRow)
        {
            if (drFocusedRow["ISDRAFT"].Equals(true))
            {
                XtraMessageBox.Show("Only draft branch refunds can be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (XtraMessageBox.Show($"Are you sure you want to deleted Branch refund - {drFocusedRow["BREFUNDNUMBER"]}?"
                , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                object returnValue = new DataRepository().ExecuteScalar("POS_USP_D_BREFUND_1", true, new Dictionary<string, object>
                    {
                        { "BRID", drFocusedRow["BRID"] },
                        { "UserID", Utility.UserID }
                    });

                if (!int.TryParse(returnValue.ToString(), out int rowsAffected))
                {
                    XtraMessageBox.Show(returnValue.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ResultGridView.DeleteRow(ResultGridView.FocusedRowHandle);

                XtraMessageBox.Show("Branch refund deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }
    }
}
 