using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class frmDispatchtrayinfo : DevExpress.XtraEditors.XtraForm
    {
        public frmDispatchtrayinfo(DataTable dt )
        {
            InitializeComponent();
            gcItems.DataSource = dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell
                && gvItems.FocusedRowHandle >= 0)
            {
                e.Menu.Items.Add(new DXMenuItem("View items", new EventHandler(OnViewItems_Click)));
                e.Menu.Items.Add(new DXMenuItem("Delete", new EventHandler(OnDelete_Click)));
            }
        }

        void OnViewItems_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gvItems.GetDataRow(gvItems.FocusedRowHandle);
                if (dr["ITEMCOUNT"].Equals(0))
                    throw new Exception("This tray does not contain any items.");
                frmTrayItems frm = new frmTrayItems(new StockRepository().GetItemsByTray(dr["TRAYINFOID"]));
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        void OnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gvItems.GetDataRow(gvItems.FocusedRowHandle);
                if (!dr["ITEMCOUNT"].Equals(0))
                    throw new Exception("Deletion not allowed: This tray still contains stock.");
                new StockRepository().DeleteTrayInfo(dr["STOCKDISPATCHID"], dr["TRAYINFOID"], Utility.UserID);
                gvItems.DeleteRow(gvItems.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}