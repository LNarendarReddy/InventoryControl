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
    public partial class frmBranchExpenseList : XtraForm
    {
        public frmBranchExpenseList()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            BranchExpense branchExpense = new BranchExpense
            {
                Description = gvExpenses.GetFocusedRowCellValue("EXPENSEDESC")
                , BranchExpenseID = gvExpenses.GetFocusedRowCellValue("BRANCHEXPENSEID")
                , BranchExpenseTypeID = gvExpenses.GetFocusedRowCellValue("BRANCHEXPENSETYPEID")
                , Amount = gvExpenses.GetFocusedRowCellValue("AMOUNT")
                , CreatedDate = gvExpenses.GetFocusedRowCellValue("CREATEDDATE")
            };

            if(((DateTime)branchExpense.CreatedDate).Date != DateTime.Today)
            {
                XtraMessageBox.Show("Only today's expenses can be edited", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            new frmBranchExpense(branchExpense).ShowDialog();

            if (branchExpense.IsSave) RefreshList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            BranchExpense branchExpense = new BranchExpense
            {
                BranchExpenseID = gvExpenses.GetFocusedRowCellValue("BRANCHEXPENSEID")
                , CreatedDate = gvExpenses.GetFocusedRowCellValue("CREATEDDATE")
            };

            if (((DateTime)branchExpense.CreatedDate).Date != DateTime.Today)
            {
                XtraMessageBox.Show("Only today's expenses can be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (XtraMessageBox.Show("Are you sure to delete expense?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                new OperationsRepository().DeleteBranchExpense(branchExpense);
                XtraMessageBox.Show("Branch expense deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (branchExpense.IsSave) RefreshList();
        }

        private void frmBranchExpenses_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now;
            dtpToDate.EditValue = DateTime.Now;

            RefreshList();
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            BranchExpense branchExpense = new BranchExpense();
            new frmBranchExpense(branchExpense).ShowDialog();

            if(branchExpense.IsSave) RefreshList();
        }

        private void RefreshList()
        {
            gcExpenses.DataSource = new ReportRepository().GetReportData("USP_R_BRANCHEXPENSE",
                new Dictionary<string, object>
                {
                    { "BranchIDs", Utility.branchInfo.BranchID }
                    , { "FromDate", dtpFromDate.EditValue }
                    , { "ToDate", dtpToDate.EditValue }
                });
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnViewImage_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            new frmImageViewer(new OperationsRepository().GetBranchExpenseImage(gvExpenses.GetFocusedRowCellValue("BRANCHEXPENSEID"))).ShowDialog();
        }
    }
}