using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Branch
{
    public partial class frmBranchExpense : XtraForm
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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!dxValidationProvider1.Validate()) return;

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

            try
            {
                new OperationsRepository().SaveBranchExpense(_branchExpense);
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