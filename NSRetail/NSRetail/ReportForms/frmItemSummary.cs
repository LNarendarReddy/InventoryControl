using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.ReportForms
{
    public partial class frmItemSummary : XtraForm
    {
        public frmItemSummary()
        {
            InitializeComponent();
        }

        private void frmItemSummary_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if(sluItemCode.EditValue == null)
            {
                XtraMessageBox.Show("Item code not set", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                sluItemCode.Focus();
                return;
            }

            Dictionary<string, object> searchCriteria = new Dictionary<string, object>()
            {
                { "BranchID",  cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "ItemCodeID", sluItemCode.EditValue }
                , { "SelectedTab", tabControlResult.SelectedTabPage.Text }
            };

            DataTable dtResult = new ReportRepository().GetReportData("USP_RPT_ITEMSUMMARY", searchCriteria);
            tabControlResult.SelectedTabPage.Controls.OfType<GridControl>().First().DataSource = dtResult;
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            tabControlResult.SelectedTabPage.Controls.OfType<GridControl>().First().ShowRibbonPrintPreview();
        }
    }
}