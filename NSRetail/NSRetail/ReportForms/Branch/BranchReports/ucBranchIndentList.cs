using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Model;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucBranchIndentList : SearchCriteriaBase
    {
        public ucBranchIndentList()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHINDENTID", "Branch Indent #" }
                , { "BRANCHNAME", "Branch Name" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "DISPATCHNUMBER", "Dispatch Number" }
                , { "INDENTSTATUS", "Indent Status" }
                , { "NOOFDAYS", "No of Indent Days" }
                , { "DISPATCHBY", "Dispatch by" }

            };

            ForceShowColumns = new List<string>(){ { "BRANCHINDENTID"} };

            ContextmenuItems = new Dictionary<string, string>
            {
                {"View Items", "320AFE45-004A-4514-A86B-776C0F85489F" },
                {"Discard", "E6F25975-53F5-4021-89AA-C7B7CFB4FCFE" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbCategory, dtpToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CategoryID", cmbCategory.EditValue}
                , { "IsMobileCall", false}
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_BRANCHINDENTLIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            try
            {
                switch (buttonText)
                {
                    case "View Items":
                        DataTable dtItems = new IndentRepository().GetIndentItems(drFocusedRow["BRANCHINDENTID"], drFocusedRow["STOCKDISPATCHID"]);

                        frmBranchIndentDetail obj = new frmBranchIndentDetail(dtItems)
                        {
                            ShowInTaskbar = false,
                            StartPosition = FormStartPosition.CenterScreen
                        };
                        obj.ShowDialog();
                        break;
                    case "Discard":
                        int rowsAffected = new IndentRepository().DiscardIndent(drFocusedRow["BRANCHINDENTID"], Utility.UserID);
                        if (rowsAffected == 0)
                            throw new Exception("Something went wrong");
                        ResultGridView.DeleteRow(ResultGridView.FocusedRowHandle);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

    }
}
