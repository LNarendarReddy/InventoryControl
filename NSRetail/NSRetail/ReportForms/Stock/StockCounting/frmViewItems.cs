using DataAccess;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Internal;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Stock.StockCounting
{
    public partial class frmViewItems : XtraForm
    {
        bool ShowViewdetail = true;
        private object BranchID = null;

        public frmViewItems(DataTable dtItems, object branchID, bool showViewdetail = true)
        {
            InitializeComponent();
            this.BranchID = branchID;
            this.ShowViewdetail = showViewdetail;
            gcItems.DataSource = dtItems;
            gvItems.BestFitColumns();
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
            if (gvItems.FocusedRowHandle < 0 || !ShowViewdetail)
                return;
            e.Menu.Items.Add(new DXMenuItem("View detail", new EventHandler(ViewDetail_Click)));
        }

        private void ViewDetail_Click(object sender, EventArgs e)
        {
            DataTable dt = new CountingRepository().ViewCountingDetails(BranchID,
                gvItems.GetFocusedRowCellValue("ITEMID"), null);
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