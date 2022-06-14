using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucDispatchDCList : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        List<string> buttonColumns;
        public ucDispatchDCList()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "DISPATCHDCID", "DispatchDCID" }
                , { "DISPATCHDCNUMBER", "DC Number" }
                , { "BRANCHCODE", "Branch Code" }
                , { "BRANCHNAME", "Branch Name" }
                , { "CATEGORYNAME", "Category" }
                , { "CREATEDBY", "User Name" }
                , { "CREATEDDATE", "Created Date" }
                , { "STATUS", "Status" }
            };

            buttonColumns = new List<string>() { "View" };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.EditValue = 13;


            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override IEnumerable<string> ButtonColumns => buttonColumns;

        public override Control FirstControl => cmbBranch;

        public override Control LastControl => dtpToDate;

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_R_DISPATCHDCLIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "View":
                    DataSet ds = new StockRepository().GetDispatchDC(drFocusedRow["DISPATCHDCID"]);
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        rptDispatchDC rpt = new rptDispatchDC(ds.Tables[0], ds.Tables[1]);
                        rpt.ShowPrintMarginsWarning = false;
                        rpt.ShowRibbonPreview();
                    }
                    break;
            }
        }
    }
}
