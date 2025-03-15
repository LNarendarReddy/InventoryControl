using DataAccess;
using DevExpress.XtraEditors;
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
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                {"View Items", "" }
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
            }

        }

    }
}
