using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class frmCreditBillPayment : XtraForm
    {
        CreditBillPayment _creditBillPayment;

        public frmCreditBillPayment(CreditBillPayment creditBillPayment)
        {
            InitializeComponent();
            _creditBillPayment = creditBillPayment;
        }

        private void frmCreditBillPayment_Load(object sender, EventArgs e)
        {
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("STATUS", typeof(bool));
            dtStatus.Columns.Add("STATUSTEXT", typeof(string));

            dtStatus.Rows.Add(false, "Open");
            dtStatus.Rows.Add(true, "Closed");

            cmbStatus.Properties.DataSource = dtStatus;
            cmbStatus.Properties.ValueMember = "STATUS";
            cmbStatus.Properties.DisplayMember = "STATUSTEXT";

            txtBillNumber.EditValue = _creditBillPayment.BillNumber;
            txtCustomerName.EditValue = _creditBillPayment.CustomerName;
            txtCustomerNumber.EditValue = _creditBillPayment.CustomerNumber;
            txtCustomerGST.EditValue = _creditBillPayment.CustomerGST;
            txtCreditAmount.EditValue = _creditBillPayment.MOPValue;
            cmbStatus.EditValue = _creditBillPayment.Status;
            txtDescription.EditValue = _creditBillPayment.Description;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _creditBillPayment.UserID = Utility.UserID;
            _creditBillPayment.Status = cmbStatus.EditValue;
            _creditBillPayment.Description = txtDescription.EditValue;

            new POSRepository().SaveCreditBillPayment(_creditBillPayment);
            if(_creditBillPayment.IsSave)
            {
                XtraMessageBox.Show("Saved successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }
    }
}