using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class frmAcceptCounting : XtraForm
    {
        object branchID;

        public frmAcceptCounting(string branchName, object branchID)
        {
            InitializeComponent();
            this.branchID = branchID;
            txtBranchName.EditValue = branchName;
        }

        private void frmAcceptCounting_Load(object sender, EventArgs e)
        {
            DataTable dtCountedCategories = new ReportRepository().GetReportData(
                        "USP_R_SC_GetCountedCategories"
                        , new Dictionary<string, object> { { "BRANCHID", branchID } });

            gcCategory.DataSource = dtCountedCategories;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCountedCategories = gcCategory.DataSource as DataTable;
                List<string> countedCategories = new List<string>();
                List<string> includedCategories = new List<string>();
                List<string> includedCategoryIDs = new List<string>();

                foreach (DataRow drRow in dtCountedCategories.Rows)
                {
                    if((bool)drRow["INCOUNTING"])
                    {
                        countedCategories.Add(drRow["CATEGORYNAME"].ToString());
                        includedCategoryIDs.Add(drRow["CATEGORYID"].ToString());
                        continue;
                    }

                    if((bool)drRow["INCLUDEINACCEPT"])
                    {
                        includedCategories.Add(drRow["CATEGORYNAME"].ToString());
                        includedCategoryIDs.Add(drRow["CATEGORYID"].ToString());
                    }
                }

                string message = $"Are you sure want to accept the below categories? {Environment.NewLine}{Environment.NewLine}";
                
                message += $"\tCounted Categories : {Environment.NewLine}{Environment.NewLine}\t\t\t\t * " + string.Join($"{Environment.NewLine}\t\t\t\t * ", countedCategories);
                if (includedCategories.Any())
                {
                    message += $"{Environment.NewLine}{Environment.NewLine}";
                    message += $"\tUser included Categories : {Environment.NewLine}{Environment.NewLine}\t\t\t\t * " + string.Join($"{Environment.NewLine}\t\t\t\t * ", includedCategories);
                }
                message += $"{Environment.NewLine}{Environment.NewLine} The system cannot be reverted beyond this point!!";
                if (XtraMessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                new CountingRepository().AcceptStockCounting(branchID, string.Join(",", includedCategoryIDs), Utility.UserID);
                new CloudRepository().InitiateCounting(branchID, false);
                XtraMessageBox.Show("Counting accepted succefully");
                Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvCategory_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = (bool)gvCategory.GetFocusedRowCellValue("INCOUNTING");
        }
    }
}