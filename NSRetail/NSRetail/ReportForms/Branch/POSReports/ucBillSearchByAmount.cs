
using DataAccess;
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
                { "CREATEDTIME", "Created Time" }
            };

            ButtonColumns = new List<string>() { "Items" };
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
            DataSet dsItems = new POSRepository().GetBillDetailByID(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["BILLID"]);
            frmViewDCItems obj = new frmViewDCItems(dsItems, true)
            {
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }
    }
}
