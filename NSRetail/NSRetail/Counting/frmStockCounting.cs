using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace NSRetail
{
    [Obsolete]
    public partial class frmStockCounting : XtraForm
    {
        CountingRepository countingRepository = new CountingRepository();
        public frmStockCounting()
        {
            InitializeComponent();
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }
        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvStockCounting.FocusedRowHandle < 0)
                return;
            frmViewItems obj =
                new frmViewItems(countingRepository.GetStockCountingDetail(
                    gvStockCounting.GetFocusedRowCellValue("STOCKCOUNTINGID")), "items");
            ShowItemsForm(obj);
        }

        private void btnViewDifferences_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            frmViewItems obj =
                new frmViewItems(countingRepository.GetStockCountingDiff(
                    cmbBranch.EditValue), "differences", true ,cmbBranch.EditValue);
            ShowItemsForm(obj);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            gcStockCounting.DataSource = new CountingRepository().GetStockCounting(cmbBranch.EditValue);
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

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBranch.EditValue == null ||
                XtraMessageBox.Show("Are you sure want to accept sheets?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                new CountingRepository().AcceptStockCounting(cmbBranch.EditValue);
                btnView_Click(null, null);
                XtraMessageBox.Show("Counting accepted succefully");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
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

        private void ShowItemsForm(frmViewItems obj)
        {
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }
    }
}