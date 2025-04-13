using DataAccess;
using DevExpress.Office.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Layout.Engine;
using NSRetail.ReportForms.Wareshouse.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Stock.StockCounting
{
    public partial class frmViewSCItems : DevExpress.XtraEditors.XtraForm
    {
        private object BranchID = null;
        private object CountingApprovalID = null;
        bool ShowPricecolumns = false;
        public frmViewSCItems(DataTable dt, bool includeMRP = false, object branchID = null, object countingApprovalID = null, bool showPricecolumns = false)
        {
            InitializeComponent();
            BranchID = branchID;
            CountingApprovalID = countingApprovalID;
            ShowPricecolumns = showPricecolumns;
            gcItems.DataSource = dt;
            gcMRP.Visible = includeMRP;
            gcStockDiff.Visible = showPricecolumns;
            gcPhyQntyCP.Visible = showPricecolumns;
            gcSysQntyCP.Visible= showPricecolumns;
            gcStockDiffValue.Visible = showPricecolumns;
            gcSheetNumber.Visible = !showPricecolumns;
            gcStockLocation.Visible = !showPricecolumns;
            gvItems.BestFitColumns();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0)
                return;
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                if (decimal.TryParse(Convert.ToString(gvItems.GetFocusedRowCellValue("PHYSICALQAUNTITY")), out decimal ivalue) && ivalue > 0)
                {
                    e.Menu.Items.Add(new DXMenuItem("View detail", new EventHandler(ViewDetail_Click)));
                    if (ShowPricecolumns)
                        e.Menu.Items.Add(new DXMenuItem("Delete item", new EventHandler(DeleteItem_Click)));
                }
            }
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to delete item", "Confirm",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            new CloudRepository().DeleteStockCounting(BranchID, gvItems.GetFocusedRowCellValue("ITEMID"));
            gvItems.DeleteRow(gvItems.FocusedRowHandle);
        }

        private void ViewDetail_Click(object sender, EventArgs e)
        {
            DataTable dt = new CountingRepository().ViewCountingDetails(BranchID,
                gvItems.GetFocusedRowCellValue("ITEMID"), CountingApprovalID);
            if (dt == null || dt.Rows.Count == 0)
                return;
            frmCountingDetails obj = new frmCountingDetails(dt);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.MinimizeBox = false;
            obj.MaximizeBox = false;
            obj.ShowDialog();
        }

        private void frmViewItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}