using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using NSRetail.ReportForms.Branch.POSReports;
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
    public partial class ucVoidDataAnalysis_Bills : SearchCriteriaBase
    {
        public ucVoidDataAnalysis_Bills()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "BRANCHNAME", "Branch" },
                { "COUNTERNAME", "Counter" },
                { "BILLNUMBER", "Bill Number" },
                { "BILLEDBY", "Billed By" },
                { "CREATEDDATE", "Bill Opened Time" },
                { "BILLCLOSEDDATE", "Bill Closed Time" },
                { "ISDOORDELIVERY", "Is Door Delivery?" },
                { "CUSTOMERNAME", "Customer Name" },
                { "CUSTOMERNUMBER", "Customer #" },
                { "CUSTOMERGST", "Customer GST" },
                { "BILLSTATUS", "Bill Status" },
                { "BILLEDITEMCOUNT", "Billed Item Count" },
                { "VOIDITEMCOUNT", "Void Item Count" },
                { "BILLEDITEMVALUE", "Billed Item Value" },
                { "VOIDITEMVALUE", "Void Item Value" }
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                { "Items", "" }
            };

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
        }

        private void ucVoidDataAnalysis_Bills_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_VOIDDATAANALYSIS_Bills", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "Items":
                    DataSet dsItems = new POSRepository().GetBillDetailByID(drFocusedRow["COUNTERID"], drFocusedRow["BILLID"], true);
                    frmViewDCItems obj = new frmViewDCItems(dsItems, true, false, true);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    break;
            }
        }
    }
}
