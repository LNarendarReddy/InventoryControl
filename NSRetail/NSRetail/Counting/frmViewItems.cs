using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmViewItems : XtraForm
    {
        private string actionType;
        private object BranchID = null;

        public frmViewItems(DataTable dtItems, string caller, bool Diff = false, object _BranchID = null)
        {
            InitializeComponent();
            gcItems.DataSource = dtItems;
            gcMRP.Visible = !Diff;
            gcSalePrice.Visible = !Diff;
            gcQuantity.Visible = !Diff;
            gcPhysicalStock.Visible = Diff;
            gcSystemStock.Visible = Diff;
            gcStockDiff.Visible = Diff;
            gcCostPriceWT.Visible = caller == "differences" || caller == "not enetered";
            gcDiffStockCP.Visible = caller == "differences" || caller == "not enetered";
            gcPhysicalStockCP.Visible = caller == "differences";
            gcSystemStockCP.Visible = caller == "differences";
            actionType = caller;
            BranchID = _BranchID;
        }

        private void frmViewItems_Load(object sender, EventArgs e)
        {

        }

        private void frmViewItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }
        
        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0 || actionType != "differences")
                return;
            e.Menu.Items.Add(new DXMenuItem("Delete Item", new EventHandler(DeleteItem_Click)));
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to delete item", "Confirm",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            new CloudRepository().DeleteStockCounting(BranchID,gvItems.GetFocusedRowCellValue("ITEMCODEID"));
            gvItems.DeleteRow(gvItems.FocusedRowHandle);
        }
    }
}