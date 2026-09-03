using DataAccess;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmItemOffers : DevExpress.XtraEditors.XtraForm
    {
        private readonly object itemID;
        private readonly string itemName;

        public frmItemOffers(object itemID, string skuCode, string itemName)
        {
            InitializeComponent();

            this.itemID = itemID;
            this.itemName = itemName;
            Text = $"Offers - {skuCode} - {itemName}";
        }

        private void frmItemOffers_Load(object sender, EventArgs e)
        {
            try
            {
                gcOffers.DataSource = new ItemCodeRepository().GetOffersByItem(itemID);
                gvOffers.BestFitColumns();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void gvOffers_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest != DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
                return;

            e.Allow = false;
            gvOffers.FocusedRowHandle = e.HitInfo.RowHandle;
            pmOffers.ShowPopup(gcOffers.PointToScreen(e.Point));
        }

        private void bbiPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gcOffers.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }
    }
}
