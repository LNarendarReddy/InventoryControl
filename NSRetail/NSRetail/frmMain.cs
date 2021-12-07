using DevExpress.XtraSplashScreen;
using Microsoft.Win32;
using NSRetail.Login;
using NSRetail.Master;
using NSRetail.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public event EventHandler RefreshBaseLineData;

        protected override void OnLoad(EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(frmProgress), true, true);
            base.OnLoad(e);
            Utility.FillBaseLine();
            SplashScreenManager.CloseForm();
        }

        private void btnBranch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBranchList obj = new frmBranchList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnCategory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCategoryList obj = new frmCategoryList();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmItemCodeList() { ShowInTaskbar = false, MdiParent = this,
                StartPosition = FormStartPosition.CenterParent,
            WindowState = FormWindowState.Maximized}.Show();
        }

        private void btnUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmUserList obj = new frmUserList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnChangePassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmChangePassword obj = new frmChangePassword();
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterParent;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RegistryKey RGkey = Registry.CurrentUser.OpenSubKey(@"Software\NSRetail", true);
                if (RGkey == null)
                    RGkey = Registry.CurrentUser.CreateSubKey(@"Software\NSRetail");
                RGkey.SetValue("PasswordString", string.Empty);
                RGkey.Close();
                Application.Exit();
            }
            catch (Exception ex) { }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnDealer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDealerList obj = new frmDealerList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnBranchCouter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCounterList obj = new frmCounterList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnModeOfPayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmModeofPayment obj = new frmModeofPayment();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnUnitsofMeasure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmUOMList obj = new frmUOMList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnTaxMaster_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGSTList obj = new frmGSTList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnBarCodePrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBarCodePrint obj = new frmBarCodePrint();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnPrinterMaster_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrinterMaster obj = new frmPrinterMaster();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnStockEntry_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmStockEntry obj = new frmStockEntry();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnStockDispatch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmStockDispatch obj = new frmStockDispatch();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiRefreshData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(frmProgress ), true, true);
            Utility.FillBaseLine();
            RefreshBaseLineData?.Invoke(null, null);
            SplashScreenManager.CloseForm();
        }

        private void btnSubCategory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSubCategoryList obj = new frmSubCategoryList();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnInvoiceList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmInvoiceList obj = new frmInvoiceList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnDispatchList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDispatchList obj = new frmDispatchList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiStockSummary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmStockSummary() { MdiParent = this }.Show();
        }
    }
}
