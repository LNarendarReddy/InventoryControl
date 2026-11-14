using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.ReportForms;
using NSRetailPOS.ReportControls.ReportBase;
using System;
using System.Collections.Generic;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucBillSearchByAmount : SearchCriteriaBase
    {
        public ucBillSearchByAmount()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new()
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

            IncludeSettingsCollection =
            [
                new("Date", "IncludeDate", ["CREATEDDATE"], true),
                new("Branch", "IncludeBranch", ["BRANCHNAME"], true),
                new("Counter", "IncludeBranchCounter", ["COUNTERNAME"], true),
                new("Billed by user", "IncludeUser", ["CREATEDBY"], true),
                new("Bill Mode", "IncludeBillMode", ["BILLMODE"], true),
                new("Bill details (bill # & customer info)", "IncludeBillDetails"
                    , [ "BILLNUMBER", "CUSTOMERNAME", "CUSTOMERGST", "CUSTOMERNUMBER"
                        , "CREATEDTIME", "BILLMOPNAME" ], true),
                new("Payment Mode", "IncludePaymentMode", ["PAYMENTMODE", "Cash", "Card", "PayTM", "Sodexo", "UPI", "BharathPay", "B2BCredit", "B2CCredit", "QRPay", "Diff."], false),
                new("Count & Avg.", "IncludeCountAndAvg", ["COUNTVALUE", "AVERAGEVALUE"], false)
            ];

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
                , { "UnmaskCustomerPhone", false}
                , { "IsPOS", true }
            };

            return GetReportData("USP_RPT_BILLNUMINFO", parameters);
        }
    }
}
