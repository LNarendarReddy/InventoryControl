using DataAccess;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class frmViewItems : XtraForm
    {
        private string actionType;
        private object BranchID = null;

        public frmViewItems(DataTable dtItems, string caller ,object _BranchID = null)
        {
            InitializeComponent();
            gcItems.DataSource = dtItems;
            gcMRP.Visible = gcSalePrice.Visible = caller == "items" || caller == "consolidated";

            gcStockDiff.Visible =
                gcSystemStock.Visible =
                gcPhysicalStock.Visible = 
                caller == "differences" || caller == "not enetered";

            gcQuantity.Visible = caller == "consolidated" || caller == "items";
            gcCreatedDate.Visible = caller == "consolidated";

            //gcCostPriceWT.Visible = caller == "differences" || caller == "not enetered";
            //gcDiffStockCP.Visible = caller == "differences" || caller == "not enetered";
            //gcPhysicalStockCP.Visible = caller == "differences";
            //gcSystemStockCP.Visible = caller == "differences";

            gcPhysicalStockCPWT.Visible = caller == "differences";
            gcSystemStockCPWT.Visible = caller == "differences";
            gcStockDiffValue.Visible = caller == "differences";

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
            if (gvItems.FocusedRowHandle < 0)
                return;
            if (actionType == "differences")
                e.Menu.Items.Add(new DXMenuItem("Delete Item", new EventHandler(DeleteItem_Click)));
            if (actionType == "consolidated")
                e.Menu.Items.Add(new DXMenuItem("View detail", new EventHandler(ViewDetail_Click)));
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to delete item", "Confirm",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            new CloudRepository().DeleteStockCounting(BranchID,gvItems.GetFocusedRowCellValue("ITEMCODEID"));
            gvItems.DeleteRow(gvItems.FocusedRowHandle);
        }

        private void ViewDetail_Click(object sender, EventArgs e)
        {
            DataTable dt = new CountingRepository().ViewCountingDetails(BranchID,
                gvItems.GetFocusedRowCellValue("ITEMID"));
                if (dt == null || dt.Rows.Count == 0)
                return;
            frmCountingDetails obj = new frmCountingDetails(dt);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.MinimizeBox= false;
            obj.MaximizeBox= false;
            obj.ShowDialog();
        }
    }
}