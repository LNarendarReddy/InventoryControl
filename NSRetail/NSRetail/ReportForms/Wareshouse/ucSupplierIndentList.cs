using DataAccess;
using DevExpress.XtraReports.UI;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse
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
                , { "FROMDATE", "From Date" }
                , { "TODATE", "To Date" }
                , { "UPDATEDBY", "Updated User" }
                , { "UPDATEDDATE", "Updated User" }
                , { "APPROVEDBY", "Approved User" }
                , { "APPROVEDDATE", "Approved User" }
                , { "STATUS", "Status" }
            };

            ButtonColumns = new List<string>() { "View", "Print&Export" };

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            
            SetFocusControls(cmbCategory, dtpToDate, columnHeaders);

        }

        public override DataTable GetData()
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
                    dealerIndent.FromDate = drFocusedRow["FROMDATE"];
                    dealerIndent.ToDate = drFocusedRow["TODATE"];
                    dealerIndent.CategoryID = drFocusedRow["CATEGORYID"];
                    dealerIndent.UserID = Utility.UserID;
                    dealerIndent.dtSupplierIndent = new ReportRepository().GetSupplierIndentDetail(drFocusedRow["SUPPLIERINDENTID"]);
                    frmDealerIndent frmDealerIndentobj = new frmDealerIndent(dealerIndent,
                        Convert.ToString(drFocusedRow["DEALERNAME"]));
                    frmDealerIndentobj.ShowInTaskbar = false;
                    frmDealerIndentobj.IconOptions.ShowIcon = false;
                    frmDealerIndentobj.StartPosition = FormStartPosition.CenterScreen;
                    frmDealerIndentobj.ShowDialog();
                    if (dealerIndent.IsSave && dealerIndent.IsApproved)
                    {
                        drFocusedRow["STATUS"] = "APPROVED";
                        drFocusedRow["APPROVEDBY"] = Utility.FullName;
                        drFocusedRow["APPROVEDDATE"] = DateTime.Now;
                    }
                    else if (dealerIndent.IsSave && !dealerIndent.IsApproved)
                        drFocusedRow["STATUS"] = "DRAFT";
                    break;
                case "Print&Export":
                    if (Convert.ToString(drFocusedRow["STATUS"]) != "APPROVED")
                        return;
                    rptDealerIndent rpt = new rptDealerIndent(new ReportRepository().GetSupplierIndentDetail(
                                    drFocusedRow["SUPPLIERINDENTID"]));
                    rpt.Parameters["IndentID"].Value = drFocusedRow["SUPPLIERINDENTID"];
                    rpt.Parameters["SupplierName"].Value = drFocusedRow["DEALERNAME"];
                    rpt.Parameters["ApprovedUser"].Value = drFocusedRow["APPROVEDBY"];
                    rpt.Parameters["UserName"].Value = Utility.FullName;
                    rpt.ShowRibbonPreview();
                    break;
            }
        }
    }
}
