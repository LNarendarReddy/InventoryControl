using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucSupplierIndentList : SearchCriteriaBase
    {       
        public ucSupplierIndentList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "SUPPLIERINDENTID", "Supplier Indent ID" }
                , { "SUPPLIERID", "Supplier ID" }
                , { "DEALERNAME", "Supplier" }
                , { "INDENTDAYS", "Indent days" }
                , { "SAFETYDAYS", "Safety days" }
                , { "UPDATEDBY", "Updated User" }
                , { "UPDATEDDATE", "Updated Date" }
                , { "APPROVEDBY", "Approved User" }
                , { "APPROVEDDATE", "Approved Date" }
                , { "STATUS", "Status" }
                , { "SUPPLIERINDENTNO", "Indent #" }
                , { "MOBILENO", "Mobile #" }
            };

            ContextmenuItems = new Dictionary<string, string>
            { 
                { "View", "8238CA9D-A819-411C-A9D3-71C9030D4637" },
                { "Print && Export", "93DE3B46-DA10-4E5F-8A42-47AE2B390C15" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            
            SetFocusControls(cmbCategory, dtpToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CATEGORYID",  cmbCategory.EditValue }
                ,{ "FROMDATE",  dtpFromDate.EditValue }
                ,{ "TODATE", dtpToDate.EditValue }
            };
            return GetReportData("USP_R_SUPPLIERINDENT", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "View":
                    DealerIndent dealerIndent = new DealerIndent();
                    dealerIndent.SupplierIndentID = drFocusedRow["SUPPLIERINDENTID"];
                    dealerIndent.supplierID = drFocusedRow["SUPPLIERID"];
                    dealerIndent.IndentDays = drFocusedRow["INDENTDAYS"];
                    dealerIndent.SafetyDays = drFocusedRow["SAFETYDAYS"];
                    dealerIndent.CategoryID = drFocusedRow["CATEGORYID"];
                    dealerIndent.IndentNo = drFocusedRow["SUPPLIERINDENTNO"];
                    dealerIndent.MobileNo = drFocusedRow["MOBILENO"];
                    dealerIndent.Status = drFocusedRow["STATUS"];
                    dealerIndent.UserID = Utility.UserID;
                    dealerIndent.dtSupplierIndent = new ReportRepository().GetSupplierIndentDetail(drFocusedRow["SUPPLIERINDENTID"]);
                    frmDealerIndent frmDealerIndentobj = new frmDealerIndent(dealerIndent,
                        Convert.ToString(drFocusedRow["DEALERNAME"]));
                    frmDealerIndentobj.ShowInTaskbar = false;
                    frmDealerIndentobj.IconOptions.ShowIcon = false;
                    frmDealerIndentobj.StartPosition = FormStartPosition.CenterScreen;
                    frmDealerIndentobj.ShowDialog();
                    if (dealerIndent.IsSave && (dealerIndent.IsApproved == 1 || dealerIndent.IsApproved == 2))
                    {
                        drFocusedRow["STATUS"] = dealerIndent.IsApproved == 1 ? "APPROVED" : "REJECTED";
                        drFocusedRow["APPROVEDBY"] = Utility.FullName;
                        drFocusedRow["APPROVEDDATE"] = DateTime.Now;
                    }
                    else if (dealerIndent.IsSave)
                        drFocusedRow["STATUS"] = "DRAFT";
                    break;
                case "Print && Export":
                    DataTable dtSupplierIndent = new ReportRepository().GetSupplierIndentDetail(drFocusedRow["SUPPLIERINDENTID"]);
                    dtSupplierIndent.DefaultView.RowFilter = "DESIREDINDENT > 0";
                    rptDealerIndent rpt = new rptDealerIndent(dtSupplierIndent);
                    rpt.Parameters["IndentID"].Value = drFocusedRow["SUPPLIERINDENTID"];
                    rpt.Parameters["SupplierName"].Value = drFocusedRow["DEALERNAME"];
                    rpt.Parameters["ApprovedUser"].Value = drFocusedRow["APPROVEDBY"];
                    rpt.Parameters["CreatedDate"].Value = drFocusedRow["CREATEDDATE"];
                    rpt.Parameters["IndentNo"].Value = drFocusedRow["SUPPLIERINDENTNO"];
                    rpt.Parameters["MobileNo"].Value = drFocusedRow["MOBILENO"];
                    rpt.Parameters["UserName"].Value = drFocusedRow["CREATEDBY"];
                    rpt.Parameters["IndentStatus"].Value = drFocusedRow["STATUS"];
                    rpt.ShowRibbonPreview();
                    break;
            }
        }
    }
}
