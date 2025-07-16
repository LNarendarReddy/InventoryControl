using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Entity;
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
    public partial class frmBranchExpense : DevExpress.XtraEditors.XtraForm
    {
        BranchExpense _branchExpense;

        public frmBranchExpense(BranchExpense branchExpense)
        {
            InitializeComponent();
            _branchExpense = branchExpense;
        }

        private void frmBranchExpense_Load(object sender, EventArgs e)
        {
            txtDescription.EditValue = _branchExpense.Description;
            txtAmount.EditValue = _branchExpense.Amount;
            cmbExpenseType.EditValue = _branchExpense.BranchExpenseTypeID;

            cmbExpenseType.Properties.DataSource = new ReportRepository().GetReportData("USP_R_BRANCHEXPENSETYPE");
            cmbExpenseType.Properties.DisplayMember = "BRANCHEXPENSETYPENAME";
            cmbExpenseType.Properties.ValueMember = "BRANCHEXPENSETYPEID";

            if (!string.IsNullOrEmpty(_branchExpense.BranchExpenseID?.ToString()))
            {
                var image = new POSRepository().GetBranchExpenseImage(_branchExpense.BranchExpenseID);
                _branchExpense.BillImage = image == DBNull.Value ? null : image;
            }

            picBillImage.EditValue = _branchExpense.BillImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            if (cmbExpenseType.Text.ToLower().Equals("others") && string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                XtraMessageBox.Show("Description cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDescription.Focus();
                return;
            }

            if (!double.TryParse(txtAmount.EditValue?.ToString(), out double amount) || amount <= 0)
            {
                XtraMessageBox.Show("Amount should be greater than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAmount.Focus();
                return;
            }

            _branchExpense.Description = txtDescription.EditValue;
            _branchExpense.Amount = txtAmount.EditValue;
            _branchExpense.BranchExpenseTypeID = cmbExpenseType.EditValue;
            _branchExpense.BillImage = picBillImage.EditValue;
            _branchExpense.UserID = Utility.UserID;

            try
            {
                new ReportRepository().SaveBranchExpense(_branchExpense);
                XtraMessageBox.Show("Branch expense saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}