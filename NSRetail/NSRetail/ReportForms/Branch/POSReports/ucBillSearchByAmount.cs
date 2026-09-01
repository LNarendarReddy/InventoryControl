using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using NSRetail.Utilities;
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
                { "BILLMOPNAME", "Bill Payment Method" },
                { "CREATEDTIME", "Finished time" },
                { "BILLMODE", "Bill Mode" },
                { "CUSTOMERNAME", "Customer Name" },
                { "CUSTOMERNUMBER", "Customer #" },
                { "CUSTOMERGST", "Customer GST" },
                { "CREATEDBY", "Finished user" },
                { "CREATEDDATE", "Finished Date" },
                { "AMOUNT", "Amount" },
                { "COUNTVALUE", "Count" },
                { "PAYMENTMODE", "Payment Mode" },
                { "AVERAGEVALUE", "Average" }
            };

            ContextmenuItems = new Dictionary<string, string>
            { 
                {"Items", "1414895B-9B4D-4413-BC82-D5410EB7DAC9" }, 
                {"Print" , "4B326209-0095-413D-9DC6-6148E2A516DE" },
                {"Print A4" , "4B326209-0095-413D-9DC6-6148E2A516DE" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>() { "CREATEDDATE" }, true),
                new IncludeSettings("Branch", "IncludeBranch", new List<string>() { "BRANCHNAME" }, true),
                new IncludeSettings("Counter", "IncludeBranchCounter", new List<string>() { "COUNTERNAME" }, true),
                new IncludeSettings("Billed by user", "IncludeUser", new List<string>() { "CREATEDBY" }, true),
                new IncludeSettings("Bill Mode", "IncludeBillMode", new List<string>() { "BILLMODE" }, true),
                new IncludeSettings("Bill details (bill # & customer info)", "IncludeBillDetails"
                    , new List<string>() { "BILLNUMBER", "CUSTOMERNAME", "CUSTOMERGST", "CUSTOMERNUMBER"
                        , "CREATEDTIME", "BILLMOPNAME" }, true),
                new IncludeSettings("Payment Mode", "IncludePaymentMode", new List<string>() 
                    { "PAYMENTMODE", "Cash", "Card", "PayTM", "Sodexo", "UPI", "BharathPay", "B2BCredit", "B2CCredit", "QRPay", "Diff." }, false),
                new IncludeSettings("Count & Avg.", "IncludeCountAndAvg", new List<string>() { "COUNTVALUE", "AVERAGEVALUE" }, false)
            };

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
                , { "UnmaskCustomerPhone", AccessUtility.HasAccess("41EA5157-D4F5-44CD-808D-47208F1B81D9::Execute")}
            };

            return GetReportData("USP_RPT_BILLNUMINFO", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            if (!drFocusedRow.Table.Columns.Contains("BRANCHCOUNTERID")
                || !drFocusedRow.Table.Columns.Contains("BILLID"))
            {
                XtraMessageBox.Show("Bill details not available, inlcude bill details and search to proceed"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

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
            else if(buttonText == "Print" || buttonText == "Print A4")
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "BillID", drFocusedRow["BILLID"] }
                    , { "BranchCounterID", drFocusedRow["BRANCHCOUNTERID"] }
                };
                DataSet dsBillDetails = new ReportRepository().GetReportDataset("USP_RPT_POS_BILL_PRINT", parameters);

                bool isA4Print = buttonText == "Print A4";
                frmBillOverrideOptions overrideOptions = new frmBillOverrideOptions(
                    dsBillDetails.Tables[0].Rows[0]["CUSTOMERNAME"],
                    dsBillDetails.Tables[0].Rows[0]["CUSTOMERNUMBER"],
                    null,
                    isA4Print);
                if (overrideOptions.ShowDialog() != DialogResult.OK) return;
                
                dsBillDetails.Tables[0].Rows[0]["CUSTOMERNAME"] = overrideOptions.CustomerName;
                dsBillDetails.Tables[0].Rows[0]["CUSTOMERNUMBER"] = overrideOptions.CustomerNumber;

                XtraReport rpt;
                if (isA4Print)
                    rpt = new rptBillA4(dsBillDetails.Tables[1], dsBillDetails.Tables[2]);
                else
                    rpt = new rptBill(dsBillDetails.Tables[1], dsBillDetails.Tables[2]);
                SetBillParameters(rpt, dsBillDetails.Tables[0].Rows[0], !isA4Print);
                if (isA4Print)
                {
                    rpt.Parameters["CustomerAddress"].Value = overrideOptions.CustomerAddress;
                    rpt.Parameters["ShipToName"].Value = overrideOptions.ShipToName;
                    rpt.Parameters["ShipToNumber"].Value = overrideOptions.ShipToNumber;
                    rpt.Parameters["ShipToAddress"].Value = overrideOptions.ShipToAddress;
                }

                rpt.ShowRibbonPreview();
            }
        }

        private static void SetBillParameters(XtraReport rpt, DataRow drBill, bool isDuplicate)
        {
            rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
            rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
            rpt.Parameters["FSSAI"].Value = "10114004000548";
            rpt.Parameters["Address"].Value = drBill["ADDRESS"];
            rpt.Parameters["BillDate"].Value = drBill["BILLCLOSEDDATE"];
            rpt.Parameters["BillNumber"].Value = drBill["BILLNUMBER"];
            rpt.Parameters["CustomerName"].Value = drBill["CUSTOMERNAME"];
            rpt.Parameters["CustomerNumber"].Value = drBill["CUSTOMERNUMBER"];
            rpt.Parameters["CustomerGST"].Value = drBill["CUSTOMERGST"];
            rpt.Parameters["TenderedCash"].Value = drBill["TENDEREDCASH"];
            rpt.Parameters["TenderedChange"].Value = drBill["TENDEREDCHANGE"];
            rpt.Parameters["IsDoorDelivery"].Value = drBill["ISDOORDELIVERY"];
            rpt.Parameters["BranchName"].Value = drBill["BRANCHNAME"];
            rpt.Parameters["CounterName"].Value = drBill["COUNTERNAME"];
            rpt.Parameters["Phone"].Value = drBill["PHONENO"];
            rpt.Parameters["UserName"].Value = drBill["CREATEDBY"];
            rpt.Parameters["RoundingFactor"].Value = drBill["ROUNDING"];
            rpt.Parameters["IsIGSTBill"].Value = drBill["IsIGSTBill"];
            rpt.Parameters["IsDuplicate"].Value = isDuplicate;
        }
    }
}
