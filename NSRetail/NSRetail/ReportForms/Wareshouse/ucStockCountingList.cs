using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse
{
    public partial class ucStockCountingList : SearchCriteriaBase
    {
        CountingRepository countingRepository = new CountingRepository();
               
        public ucStockCountingList()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "STOCKCOUNTINGID", "Stock Counting ID" }
                , { "UPDATEDBY", "Updated User" }
                , { "UPDATEDDATE", "Updated Date" }
                , { "STATUS", "Status" }
            };

            ButtonColumns = new List<string>() { "View" };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            SetFocusControls(cmbBranch, cmbBranch,columnHeaders);
        }

        private void ShowItemsForm(frmViewItems obj)
        {
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnViewConsolidatedStock_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            frmViewItems obj =
                new frmViewItems(countingRepository.GetConsolidatedItems(
                    cmbBranch.EditValue), "consolidated");
            ShowItemsForm(obj);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBranch.EditValue == null ||
                XtraMessageBox.Show("Are you sure want to accept sheets?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                new CountingRepository().AcceptStockCounting(cmbBranch.EditValue);
                XtraMessageBox.Show("Counting accepted succefully");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewDifferences_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            frmViewItems obj =
                new frmViewItems(countingRepository.GetStockCountingDiff(
                    cmbBranch.EditValue), "differences", true, cmbBranch.EditValue);
            ShowItemsForm(obj);
        }

        private void btnNotEntered_Click(object sender, EventArgs e)
        {
              if (cmbBranch.EditValue == null)
                return;
            frmViewItems obj =
                new frmViewItems(countingRepository.GetStockCountingNoteEntered(
                    cmbBranch.EditValue), "not enetered", true);
            ShowItemsForm(obj);

        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
            };
            return GetReportData("USP_R_STOCKCOUNTING", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "View":
                    frmViewItems obj =
                        new frmViewItems(countingRepository.GetStockCountingDetail(drFocusedRow["STOCKCOUNTINGID"]), "items");
                    ShowItemsForm(obj);
                    break;
            }
        }
    }
}
