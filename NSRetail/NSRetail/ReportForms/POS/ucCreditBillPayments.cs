using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using NSRetail.POS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.POS
{
    public partial class ucCreditBillPayments : SearchCriteriaBase
    {
        public ucCreditBillPayments()
        {
            InitializeComponent();
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            MasterRepository masterRepo = new MasterRepository();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "CUSTOMERNAME", "Customer name" },
                { "CUSTOMERNUMBER", "Customer #" },
                { "CUSTOMERGST", "Customer GST #" },
                { "MOPVALUE", "Credit Amount" },
                { "STATUSTEXT", "Status" },
                { "STATUSCHANGEDBY", "Status changed by" },
                { "STATUSCHANGEDDATE", "Status changed date" },
                { "BILLEDBY", "Billed by" },
                { "DESCRIPTION", "Description" }
            };

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
            HiddenColumns = new List<string> { "STATUS" };
            ButtonColumns = new List<string> { "Edit" };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_CREDITBILLPAYMENTS", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            if (buttonText != "Edit") return;

            CreditBillPayment creditBillPaymentObj = new CreditBillPayment()
            {
                BillNumber = drFocusedRow["BILLNUMBER"]
                , CreditBillPaymentID = drFocusedRow["CREDITBILLPAYMENTID"]
                , CustomerName = drFocusedRow["CUSTOMERNAME"]
                , CustomerNumber = drFocusedRow["CUSTOMERNUMBER"]
                , CustomerGST = drFocusedRow["CUSTOMERGST"]
                , MOPValue = drFocusedRow["MOPVALUE"]
                , Status = drFocusedRow["STATUS"]
                , Description = drFocusedRow["DESCRIPTION"]
            };

            frmCreditBillPayment creditBillPaymentForm = new frmCreditBillPayment(creditBillPaymentObj);
            creditBillPaymentForm.ShowDialog();

            if(creditBillPaymentObj.IsSave && XtraMessageBox.Show("Do you want to refresh the report?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                == DialogResult.Yes)
            {
                (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
            }
        }
    }
}
