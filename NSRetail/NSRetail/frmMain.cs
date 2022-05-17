using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;
using Microsoft.Win32;
using NSRetail.Login;
using NSRetail.Master;
using NSRetail.ReportForms;
using NSRetail.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmMain : RibbonForm
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
            Utilities.SerialPortListener.ListenSerialPort(this);
            Utility.FillBaseLine();
            SplashScreenManager.CloseForm();

            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle();
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Office 2019 Black";
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.UpdateStyleSettings();
        }

        private void btnBranch_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBranchList obj = new frmBranchList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCategoryList obj = new frmCategoryList();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmItemCodeList()
            {
                ShowInTaskbar = false,
                MdiParent = this,
                StartPosition = FormStartPosition.CenterParent,
                WindowState = FormWindowState.Maximized
            }.Show();
        }

        private void btnUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmUserList obj = new frmUserList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnChangePassword_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmChangePassword obj = new frmChangePassword();
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterParent;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnLogout_ItemClick(object sender, ItemClickEventArgs e)
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

        private void btnDealer_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDealerList obj = new frmDealerList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnBranchCouter_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCounterList obj = new frmCounterList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnModeOfPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmModeofPayment obj = new frmModeofPayment();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnUnitsofMeasure_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmUOMList obj = new frmUOMList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnTaxMaster_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmGSTList obj = new frmGSTList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnBarCodePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBarCodePrint obj = new frmBarCodePrint();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnPrinterMaster_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmPrinterMaster obj = new frmPrinterMaster();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnStockEntry_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStockEntry obj = new frmStockEntry();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnStockDispatch_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStockDispatch obj = new frmStockDispatch();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiRefreshData_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(frmProgress), true, true);
            Utility.FillBaseLine();
            RefreshBaseLineData?.Invoke(null, null);
            SplashScreenManager.CloseForm();
        }

        private void btnSubCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSubCategoryList obj = new frmSubCategoryList();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnInvoiceList_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmInvoiceList obj = new frmInvoiceList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnDispatchList_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDispatchList obj = new frmDispatchList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiStockSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmStockSummary() { MdiParent = this }.Show();
        }

        private void btnPrintDC_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDispatchDCPrint obj = new frmDispatchDCPrint();
            obj.ShowDialog();
        }

        private void btnDCList_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDispatchDCList obj = new frmDispatchDCList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiSyncStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Utilities.frmSyncStatus() { MdiParent = this }.Show();
        }

        private void btnItemGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmItemGroupList obj = new frmItemGroupList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnOfferList_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmOfferList obj = new frmOfferList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnStockCounting_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStockCounting obj = new frmStockCounting();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnBranchRefund_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBRefundList obj = new frmBRefundList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnDayClosure_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDayClosureList obj = new frmDayClosureList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUserName.Caption = $"Logged In User : { Utility.FullName}   Version : { Utility.AppVersion } (11-05-2022)";

            List<BarButtonItem> availableItems = new List<BarButtonItem>()
            { btnItem, btnBarCodePrint, btnItemGroup, btnOfferList, btnStockEntry, btnInvoiceList,
             btnStockDispatch, btnDispatchList, btnDCList, btnPrintDC, btnStockCounting, bbiStockSummary, bbiSyncStatus
            , btnBranch, btnBranchCouter, btnSubCategory, btnUser, btnDealer , btnModeOfPayment, btnUnitsofMeasure
            , btnTaxMaster, btnPrinterMaster, btnBranchRefund, btnDayClosure, btnRunningSale, btnCategory};

            List<RibbonPageGroup> ribbonPageGroups = new List<RibbonPageGroup>()
            { ribbonPageGroup1, ribbonPageGroup2, ribbonPageGroup3, ribbonPageGroup4, ribbonPageGroup5, ribbonPageGroup6,
            ribbonPageGroup7, ribbonPageGroup8, ribbonPageGroup9, ribbonPageGroup10, ribbonPageGroup11, ribbonPageGroup12
            , ribbonPageGroup13};

            List<RibbonPage> ribbonPages = new List<RibbonPage>()
            {
                ribbonPage1, ribbonPage3, ribbonPage4, ribbonPage5
            };

            bool revisitMenuItems = false;

            if (Utility.Role == "Division Manager")
            {
                availableItems.ForEach(x => x.Visibility = BarItemVisibility.Never);
                revisitMenuItems = true;

                btnBarCodePrint.Visibility = BarItemVisibility.Always;
                btnItem.Visibility = BarItemVisibility.Always;
                btnStockDispatch.Visibility = BarItemVisibility.Always;
                btnDispatchList.Visibility = BarItemVisibility.Always;
                bbiStockSummary.Visibility = BarItemVisibility.Always;
                btnPrintDC.Visibility = BarItemVisibility.Always;
                btnDCList.Visibility = BarItemVisibility.Always;
            }
            else if (Utility.Role == "Division User")
            {
                availableItems.ForEach(x => x.Visibility = BarItemVisibility.Never);
                revisitMenuItems = true;

                btnBarCodePrint.Visibility = BarItemVisibility.Always;
                btnItem.Visibility = BarItemVisibility.Always;
                btnStockDispatch.Visibility = BarItemVisibility.Always;
                btnDispatchList.Visibility = BarItemVisibility.Always;
                bbiStockSummary.Visibility = BarItemVisibility.Always;
            }
            else if (Utility.Role == "IT User")
            {
                availableItems.ForEach(x => x.Visibility = BarItemVisibility.Never);
                revisitMenuItems = true;

                btnItem.Visibility = BarItemVisibility.Always;
                btnBarCodePrint.Visibility = BarItemVisibility.Always;
                btnStockEntry.Visibility = BarItemVisibility.Always;
                btnInvoiceList.Visibility = BarItemVisibility.Always;
                btnStockDispatch.Visibility = BarItemVisibility.Always;
                btnDispatchList.Visibility = BarItemVisibility.Always;
                btnDCList.Visibility = BarItemVisibility.Always;
                btnPrintDC.Visibility = BarItemVisibility.Always;
                bbiStockSummary.Visibility = BarItemVisibility.Always;
                bbiSyncStatus.Visibility = BarItemVisibility.Always;
                btnDealer.Visibility = BarItemVisibility.Always;
            }

            if (revisitMenuItems)
            {
                ribbonPageGroups.ForEach(x => x.Visible = Enumerable.Range(0, x.ItemLinks.Count)
                    .Any(y => x.ItemLinks[y].Item.Visibility == BarItemVisibility.Always));
                ribbonPages.ForEach(x => x.Visible = Enumerable.Range(0, x.Groups.Count).Any(y => x.Groups[y].Visible));
            }
            ribbonPageGroup9.Visible = true;
            btnStockCounting.Visibility = BarItemVisibility.Always;
        }

        private void btnRunningSale_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmRunningSales obj = new frmRunningSales();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnTaxWiseSales_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmTaxwisesale obj = new frmTaxwisesale();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnZeroStock_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmZeroStock obj = new frmZeroStock();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnItemWiseSales_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmItemwiseSales obj = new frmItemwiseSales();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmReportPlaceHolder obj = new frmReportPlaceHolder();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiItemSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmItemSummary obj = new frmItemSummary();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnCounting_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCounting obj = new frmCounting();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnDealerIndent_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDealerIndentList obj = new frmDealerIndentList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }
    }
}
