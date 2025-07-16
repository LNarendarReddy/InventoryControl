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
    public partial class ucBranchExpenseList : SearchCriteriaBase
    {
        public ucBranchExpenseList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHEXPENSETYPENAME", "Expense Type" }
                , { "EXPENSEDESC", "Description" }
                , { "AMOUNT", "Amount" }
                , { "CREATEDBY", "Created by" }
                , { "UPDATEDBY", "Updated by" }
                , { "UPDATEDDATE", "Updated Date" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, dtFromDate, dtToDate };
            ContextmenuItems = new Dictionary<string, string> 
            {
                { "Expense Image", string.Empty } ,
                { "Edit", string.Empty },
                { "Delete", string.Empty }
            };

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
            };

            return GetReportData("USP_R_BRANCHEXPENSE", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "Expense Image":
                    object image = new POSRepository().GetBranchExpenseImage(drFocusedRow["BRANCHEXPENSEID"]);
                    new frmImageViewer(image) { Text = "Expense Image" }.ShowDialog();
                    break;

                case "Edit":
                    BranchExpense branchExpense = new BranchExpense
                    {
                        Description = drFocusedRow["EXPENSEDESC"],
                        BranchExpenseID = drFocusedRow["BRANCHEXPENSEID"],
                        BranchExpenseTypeID = drFocusedRow["BRANCHEXPENSETYPEID"],
                        Amount = drFocusedRow["AMOUNT"],
                        BranchID = drFocusedRow["BRANCHID"],
                        CreatedDate = drFocusedRow["CREATEDDATE"]
                    };

                    new frmBranchExpense(branchExpense).ShowDialog();

                    if (branchExpense.IsSave)
                        XtraMessageBox.Show("Refresh grid to get updated values", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case "Delete":
                    BranchExpense branchExpenseToDelete = new BranchExpense
                    {
                        BranchExpenseID = drFocusedRow["BRANCHEXPENSEID"],
                        UserID = Utility.UserID
                    };

                    if (XtraMessageBox.Show("Are you sure to delete expense?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        != DialogResult.Yes) return;

                    try
                    {
                        new ReportRepository().DeleteBranchExpense(branchExpenseToDelete);
                        XtraMessageBox.Show("Branch expense deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (branchExpenseToDelete.IsSave)
                        XtraMessageBox.Show("Refresh grid to get updated values", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
