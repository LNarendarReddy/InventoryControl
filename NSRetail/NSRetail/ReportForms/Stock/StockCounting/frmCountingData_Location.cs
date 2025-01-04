using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using NSRetail.Login;
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
    public partial class frmCountingData_Location : DevExpress.XtraEditors.XtraForm
    {
        object branchID = null;
        public frmCountingData_Location(DataTable dt, object _branchId)
        {
            InitializeComponent();
            branchID = _branchId;
            gcItems.DataSource = dt;
            gvItems.BestFitColumns();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
            e.Menu.Items.Add(new DXMenuItem("View sale data", new EventHandler(ViewSaleData_Click)));
            if (decimal.TryParse(Convert.ToString(gvItems.GetFocusedRowCellValue("COUNTEDQUANTITY")), out decimal ivalue) && ivalue > 0)
            {
                e.Menu.Items.Add(new DXMenuItem("View counting detail", new EventHandler(ViewDetail_Click)));
            }
        }

        private void ViewDetail_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Loading...");
                dt = new CountingRepository().ViewCountingDetails(branchID,
                    gvItems.GetFocusedRowCellValue("ITEMID"), null);
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
            }
            if (dt == null || dt.Rows.Count == 0)
                return;
            frmCountingDetails obj = new frmCountingDetails(dt);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.MinimizeBox = false;
            obj.MaximizeBox = false;
            obj.ShowDialog();

        }

        private void ViewSaleData_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Loading...");
                dt = new CountingRepository().getCountingData_Sales(branchID,
                    gvItems.GetFocusedRowCellValue("ITEMID"),
                    gvItems.GetFocusedRowCellValue("FIRSTCOUNTINGDATE"),
                    gvItems.GetFocusedRowCellValue("LASTCOUNTNGDATE"));
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
            }
            if (dt == null || dt.Rows.Count == 0)
                return;
            frmCountingData_Sales obj = new frmCountingData_Sales(dt, gvItems.GetFocusedRowCellValue("SKUCODE"),
                gvItems.GetFocusedRowCellValue("ITEMNAME"));
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.MinimizeBox = false;
            obj.MaximizeBox = false;
            obj.ShowDialog();
        }
    }
}