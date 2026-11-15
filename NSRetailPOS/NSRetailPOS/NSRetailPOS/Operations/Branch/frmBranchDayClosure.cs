using DevExpress.XtraEditors;
using ErrorManagement;
using NSRetailPOS.Data;
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
    public partial class frmBranchDayClosure : DevExpress.XtraEditors.XtraForm
    {
        public frmBranchDayClosure()
        {
            InitializeComponent();
        }

        private void frmBranchDayClosure_Load(object sender, EventArgs e)
        {
            dtpClosureDate.EditValue = DateTime.Now;
            DataTable dtMOPs = new ReportRepository().GetReportData("[USP_R_MOP]");
            dtMOPs.DefaultView.RowFilter = "MOPID <> 1";
            dtMOPs = dtMOPs.DefaultView.ToTable();
            dtMOPs.Columns.Add(new DataColumn("MOPVALUE", typeof(double)) { DefaultValue = 0.00 });

            gcMOPs.DataSource = dtMOPs;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to save? This operation cannot be undone!!"
                , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                DataTable dtMOPs = gcMOPs.DataSource as DataTable;
                Utility.CleanDataTable(dtMOPs, ["MOPID", "MOPVALUE"]);

                DataTable dtRespone = new ReportRepository().GetReportData("USP_C_BRANCHDAYCLOSURE",
                    new Dictionary<string, object>()
                    {
                        { "BranchID", Utility.branchInfo.BranchID },
                        { "ClosureDate", dtpClosureDate.EditValue },
                        { "dtMopValues", dtMOPs },
                        { "UserID", Utility.loginInfo.UserID }
                    });

                if (dtRespone == null || dtRespone.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Something went wrong");
                    return;
                }

                if (int.TryParse(dtRespone.Rows[0][0].ToString(), out int _))
                {
                    XtraMessageBox.Show("Saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                XtraMessageBox.Show(dtRespone.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }
    }
}