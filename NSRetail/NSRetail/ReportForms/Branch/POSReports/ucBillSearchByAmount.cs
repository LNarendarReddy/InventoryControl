using DataAccess;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class ucBillSearchByAmount : SearchCriteriaBase
    {
        public ucBillSearchByAmount()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "MOPNAME", "Payment Method" },
                { "CREATEDTIME", "Created time" },
                { "BILLMODE", "Bill Mode" },
                { "CUSTOMERNAME", "Customer Name" },
                { "CUSTOMERNUMBER", "Customer #" },
                { "BILLCLOSEDBY", "Finished user" },
                { "BILLCLOSEDDATE", "Finished time" },
            };

            ContextmenuItems = new List<string>() { "Items", "Print" };
            SetFocusControls(cmbBranch, txtCutOffAmt, specificColumnHeaders);
        }

        private void ucBillSearchByAmount_Load(object sender, EventArgs e)
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
                , { "CutOff", txtCutOffAmt.EditValue }
            };

            return GetReportData("USP_RPT_BILLNUMINFO", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            if (buttonText == "Items")
            {
                DataSet dsItems = new POSRepository().GetBillDetailByID(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["BILLID"]);
                frmViewDCItems obj = new frmViewDCItems(dsItems, true)
                {
                    ShowInTaskbar = false,
                    StartPosition = FormStartPosition.CenterScreen
                };
                obj.IconOptions.ShowIcon = false;
                obj.ShowDialog();
            }
            else if(buttonText == "Print")
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "BillID", drFocusedRow["BILLID"] }
                    , { "BranchCounterID", drFocusedRow["BRANCHCOUNTERID"] }
                };
                DataSet dsBillDetails = new ReportRepository().GetReportDataset("USP_RPT_POS_BILL_PRINT", parameters);
                
                rptBill rpt = new rptBill(dsBillDetails.Tables[1], dsBillDetails.Tables[2]);
                rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
                rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = dsBillDetails.Tables[0].Rows[0]["ADDRESS"];
                rpt.Parameters["BillDate"].Value = dsBillDetails.Tables[0].Rows[0]["CREATEDDATE"];
                rpt.Parameters["BillNumber"].Value = dsBillDetails.Tables[0].Rows[0]["BILLNUMBER"];
                rpt.Parameters["CustomerName"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERNAME"];
                rpt.Parameters["CustomerNumber"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERNUMBER"];
                rpt.Parameters["CustomerGST"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERGST"];
                rpt.Parameters["TenderedCash"].Value = dsBillDetails.Tables[0].Rows[0]["TENDEREDCASH"];
                rpt.Parameters["TenderedChange"].Value = dsBillDetails.Tables[0].Rows[0]["TENDEREDCHANGE"];
                rpt.Parameters["IsDoorDelivery"].Value = dsBillDetails.Tables[0].Rows[0]["ISDOORDELIVERY"];
                rpt.Parameters["BranchName"].Value = dsBillDetails.Tables[0].Rows[0]["BRANCHNAME"];
                rpt.Parameters["CounterName"].Value = dsBillDetails.Tables[0].Rows[0]["COUNTERNAME"];
                rpt.Parameters["Phone"].Value = dsBillDetails.Tables[0].Rows[0]["PHONENO"];
                rpt.Parameters["UserName"].Value = dsBillDetails.Tables[0].Rows[0]["CREATEDBY"];
                rpt.Parameters["RoundingFactor"].Value = dsBillDetails.Tables[0].Rows[0]["ROUNDING"];
                rpt.Parameters["IsDuplicate"].Value = true;
                rpt.ShowRibbonPreview();
            }
        }
    }
}
