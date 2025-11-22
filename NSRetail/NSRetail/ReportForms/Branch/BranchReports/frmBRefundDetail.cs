using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class frmBRefundDetail : DevExpress.XtraEditors.XtraForm
    {
        object BRID = null;
        public bool IsSave = false;
        SupplierRepository SupplierRepository = new SupplierRepository();
        public frmBRefundDetail(DataTable dtItems,object _BRID, bool IsAccepted = false)
        {
            InitializeComponent();
            btnSave.Enabled = !IsAccepted;
            gvItems.OptionsBehavior.Editable = !IsAccepted;
            gcItems.DataSource = dtItems;
            BRID = _BRID;
            gvItems.PopupMenuShowing += gvItems_PopupMenuShowing;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBRefundDetail_Load(object sender, EventArgs e)
        {
            cmbReason.DataSource = SupplierRepository.GetReason();
            cmbReason.ValueMember = "REASONID";
            cmbReason.DisplayMember = "REASONNAME";

            cmbSupplier.DataSource = SupplierRepository.GetSupplier();
            cmbSupplier.ValueMember = "DEALERID";
            cmbSupplier.DisplayMember = "DEALERNAME";
        }

        private void gvItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (gvItems.FocusedColumn == gcReason)
                {
                    SupplierRepository.UpdateBRReason(
                        gvItems.GetRowCellValue(e.RowHandle, "BRDID"),
                        gvItems.GetRowCellValue(e.RowHandle, "REASONID"),
                        Utility.UserID);

                    gcItems.DataSource = new POSRepository().GetBRefundDetail(BRID);

                }
                else if (gvItems.FocusedColumn == gcDescription)
                {
                    SupplierRepository.UpdateBRDescription(
                        gvItems.GetRowCellValue(e.RowHandle, "BRDID"),
                        gvItems.GetRowCellValue(e.RowHandle, "REFUNDDESCRIPTION"),
                        Utility.UserID);
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void gvItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                gvItems.MoveNext();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to accept refund sheet", "Confirmation!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                DataTable dt = (gcItems.DataSource as DataTable).Copy();
                dt.Columns.Remove("SNO");
                dt.Columns.Remove("ITEMCODE");
                dt.Columns.Remove("ITEMNAME");
                dt.Columns.Remove("MRP");
                dt.Columns.Remove("SALEPRICE");
                dt.Columns.Remove("REFUNDDESCRIPTION");
                dt.Columns.Remove("CATEGORYNAME");
                new POSRepository().AcceptBRefund(BRID, Utility.UserID, dt);
                IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0)
                return;
            e.Menu.Items.Add(new DXMenuItem("View Report", new EventHandler(ViewReport_Click)));

            if (!btnSave.Enabled || gvItems.GetFocusedRowCellValue("QUANTITY").Equals(1))
                return;
            e.Menu.Items.Add(new DXMenuItem("Split Quantity", new EventHandler(SplitQuantity_Click)));
        }

        private void ViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void SplitQuantity_Click(object sender, EventArgs e)
        {
            SplitQuantity splitQuantity = new SplitQuantity();
            splitQuantity.BaseQuantity = gvItems.GetFocusedRowCellValue("QUANTITY");
            splitQuantity.BaseReason = gvItems.GetFocusedRowCellValue("REASONID");
            frmSplitQuantity obj = new frmSplitQuantity(splitQuantity);
            obj.ShowDialog();
            if(splitQuantity._IsSave)
            {
                SupplierRepository.SplitBRQuantity(
                        gvItems.GetFocusedRowCellValue("BRDID"),
                        splitQuantity.BaseQuantity,splitQuantity.BaseReason,
                        splitQuantity.DevidedQuantity, splitQuantity.DevidedReason,
                        Utility.UserID, gvItems.RowCount + 1);

                gcItems.DataSource = new POSRepository().GetBRefundDetail(BRID);
            }
        }
    }
}