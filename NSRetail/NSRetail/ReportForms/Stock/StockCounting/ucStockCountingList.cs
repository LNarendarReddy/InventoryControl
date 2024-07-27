using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace NSRetail.ReportForms.Wareshouse.Audit
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
                , { "CREATEDBY", "Created By" }
                , { "CREATEDDATE", "Created Date" }
                , { "SubmittedBy", "Submitted By" }
                , { "SubmittedDate", "Submitted Date" }
                , { "ACCEPTEDBY", "Accepted By" }
                , { "STOCKACCEPTEDDATE", "Accepted Date" }
                , { "ArchivedBy", "Archived By" }
                , { "ArchivedDate", "Archived Date" }
                , { "STATUS", "Status" }
            };

            ButtonColumns = new List<string>() { "View" };

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            SetFocusControls(cmbBranch, cmbBranch,columnHeaders);
            AllowedRoles = new List<string> { "Stock counting user" };
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
                    cmbBranch.EditValue), "consolidated", cmbBranch.EditValue);
            ShowItemsForm(obj);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;

            new frmAcceptCounting(cmbBranch.Text, cmbBranch.EditValue).ShowDialog();
        }

        private void btnViewDifferences_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            frmViewItems obj =
                new frmViewItems(countingRepository.GetStockCountingDiff(
                    cmbBranch.EditValue), "differences", cmbBranch.EditValue);
            ShowItemsForm(obj);
        }

        private void btnNotEntered_Click(object sender, EventArgs e)
        {
              if (cmbBranch.EditValue == null)
                return;
            frmViewItems obj =
                new frmViewItems(countingRepository.GetStockCountingNoteEntered(
                    cmbBranch.EditValue), "not enetered", cmbBranch.EditValue);
            ShowItemsForm(obj);

        }

        public override object GetData()
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
