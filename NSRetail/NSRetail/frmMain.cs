using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Microsoft.Win32;
using NSRetail.Countning;
using NSRetail.Login;
using NSRetail.Master;
using NSRetail.ReportForms;
using NSRetail.ReportForms.Branch.BranchReports;
using NSRetail.ReportForms.Branch.POSReports;
using NSRetail.ReportForms.Stock.StockCounting;
using NSRetail.ReportForms.Stock.StockReports;
using NSRetail.ReportForms.Stock.TransactionReports;
using NSRetail.ReportForms.Supplier.SupplierReports;
using NSRetail.ReportForms.Supplier.SupplierWiseReports;
using NSRetail.ReportForms.Wareshouse.Audit;
using NSRetail.ReportForms.Wareshouse.Profitability;
using NSRetail.ReportForms.Wareshouse.StockAndSale;
using NSRetail.ReportForms.Wareshouse.TaxBreakUp;
using NSRetail.Stock;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private void btnPrintDC_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDispatchDCPrint obj = new frmDispatchDCPrint();
            obj.ShowDialog();
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
        private void btnDealList_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmOfferList obj = new frmOfferList(true);
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }
        private void btnBaseOfferList_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBaseOfferList obj = new frmBaseOfferList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
            lblUserName.Caption = $"Logged In User : {Utility.FullName}   " +
                $"Version : {Utility.AppVersion} {Utility.VersionDate} - {ConfigurationManager.AppSettings["BuildType"]}";

            List<BarButtonItem> availableItems = new List<BarButtonItem>()
            { btnItem, btnBarCodePrint, btnOfferList, btnStockEntry, btnInvoiceList,
             btnStockDispatch, btnDispatchList, btnDCList, btnPrintDC, btnStockCounting, bbiStockSummary, bbiSyncStatus
            , btnBranch, btnBranchCouter, btnSubCategory, btnUser, btnDealer , btnModeOfPayment, btnUnitsofMeasure
            , btnTaxMaster, btnPrinterMaster, btnBranchRefund, btnDayClosure, btnRunningSale, btnCategory, btnStockAdjustment, btnSupplierReturns
            , bbiItemSummary, btnItemLedger, btnStockSummary, bbiWarehouseReports, bbiStockReports, bbiSupplierReports, bbiBranchReports
            , btnBaseOfferList, btnDealList, bbiClassification, btnProcessWHDispatch, bbiClearProcedureCache, btnCounting };

            List<RibbonPageGroup> ribbonPageGroups = new List<RibbonPageGroup>()
            { ribbonPageGroup1, ribbonPageGroup2, ribbonPageGroup3, ribbonPageGroup4, ribbonPageGroup5, ribbonPageGroup6,
             ribbonPageGroup7, ribbonPageGroup8,  ribbonPageGroup10, ribbonPageGroup11, ribbonPageGroup12, ribbonPageGroup15
            , ribbonPageGroup17, ribbonPageGroup19, ribbonPageGroup20 };

            List<RibbonPage> ribbonPages = new List<RibbonPage>()
            {
                ribbonPage1, ribbonPage3, ribbonPage2, ribbonPage4, ribbonPage6
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
                bbiItemSummary.Visibility = BarItemVisibility.Always;
                btnItemLedger.Visibility = BarItemVisibility.Always;
                btnStockSummary.Visibility = BarItemVisibility.Always;
                bbiWarehouseReports.Visibility = BarItemVisibility.Always;
                btnSupplierReturns.Visibility = BarItemVisibility.Always;
                bbiStockReports.Visibility = BarItemVisibility.Always;
                bbiSupplierReports.Visibility = BarItemVisibility.Always;
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
                bbiStockReports.Visibility = BarItemVisibility.Always;
                bbiBranchReports.Visibility = BarItemVisibility.Always;
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
                btnStockAdjustment.Visibility = BarItemVisibility.Always;
                btnSupplierReturns.Visibility = BarItemVisibility.Always;
                bbiItemSummary.Visibility = BarItemVisibility.Always;
                btnItemLedger.Visibility = BarItemVisibility.Always;
                btnStockSummary.Visibility = BarItemVisibility.Always;
                bbiSupplierReports.Visibility = BarItemVisibility.Always;
                bbiStockReports.Visibility = BarItemVisibility.Always;
                bbiBranchReports.Visibility = BarItemVisibility.Always;
            }
            else if(Utility.Role == "Stock counting user")
            {
                availableItems.ForEach(x => x.Visibility = BarItemVisibility.Never);
                revisitMenuItems = true;

                btnStockCounting.Visibility = BarItemVisibility.Always;
                btnCounting.Visibility = BarItemVisibility.Always;
                bbiWarehouseReports.Visibility = BarItemVisibility.Always;
                bbiItemSummary.Visibility = BarItemVisibility.Always;
                btnItemLedger.Visibility = BarItemVisibility.Always;
                btnStockSummary.Visibility = BarItemVisibility.Always;
            }

            if (revisitMenuItems)
            {
                ribbonPageGroups.ForEach(x => x.Visible = Enumerable.Range(0, x.ItemLinks.Count)
                    .Any(y => x.ItemLinks[y].Item.Visibility == BarItemVisibility.Always));
                ribbonPages.ForEach(x => x.Visible = Enumerable.Range(0, x.Groups.Count).Any(y => x.Groups[y].Visible));
            }
            
            bbiClearProcedureCache.Enabled = Utility.Role == "Admin" || Utility.Role == "IT Manager";
            bbiStockSlippage.Visibility = Utility.BranchID == 97 ? BarItemVisibility.Always : BarItemVisibility.Never;
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

        private void btnSupplierReturns_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSupplierReturns obj = new frmSupplierReturns();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void bbiClearProcedureCache_ItemClick(object sender, ItemClickEventArgs e)
        {
            new DataAccess.MasterRepository().ClearProcedureCache();
            XtraMessageBox.Show("Database procedure cache cleared", "Operation complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bbiBranchReports_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();

            ReportHolder branchReports = new ReportHolder() { ReportName = "Branch Reports" };
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Indent", SearchCriteriaControl = new ucBranchIndent() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Indent (AVG)", SearchCriteriaControl = new ucBranchIndentByAVG() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch List", SearchCriteriaControl = new ucDispatchList() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch DC List", SearchCriteriaControl = new ucDispatchDCList() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refund sheets", SearchCriteriaControl = new ucBranchRefunds() });
            branchReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch Differences", SearchCriteriaControl = new ucDispatchDifferences() });
            reportList.Add(branchReports);

            ReportHolder POSReports = new ReportHolder() { ReportName = "POS Reports" };
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Day closure List", SearchCriteriaControl = new ucDayClosureList() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Bills by Amount", SearchCriteriaControl = new ucBillSearchByAmount() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Customer Details", SearchCriteriaControl = new ucCustomerDetails() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "B2B Credit Bill Payments", SearchCriteriaControl = new ucCreditBillPayments() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Installed versions", SearchCriteriaControl = new ucInstalledVersions() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "HDD SNo history", SearchCriteriaControl = new ucHDDClearhistory() });
            reportList.Add(POSReports);

            ShowReportForm(reportList, "Branch");
        }
        private void bbiStockReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> stockReportList = new List<ReportHolder>();

            ReportHolder StockReports = new ReportHolder() { ReportName = "Stock Reports" };
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Current stock (with open sale)", SearchCriteriaControl = new ucCurrentStock() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Non-Moving stock", SearchCriteriaControl = new ucNonMovingStock() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Stock As on date", SearchCriteriaControl = new ucStockAsOnDate() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Zero Stock", SearchCriteriaControl = new ucZeroStock() });
            StockReports.SubCategory.Add(new ReportHolder() { ReportName = "Left over prices", SearchCriteriaControl = new ucLeftOverPrices() });
            stockReportList.Add(StockReports);

            ReportHolder transactionreports = new ReportHolder() { ReportName = "Transaction Reports" };
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Purchases", SearchCriteriaControl = new ucPurchases() });
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatches", SearchCriteriaControl = new ucBranchTransactions("D") });
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Sales", SearchCriteriaControl = new ucSales() });
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Customer Refunds", SearchCriteriaControl = new ucBranchTransactions("C") });
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refunds", SearchCriteriaControl = new ucBranchTransactions("B") });
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Day sale by Payment Method", SearchCriteriaControl = new ucSaleByMOP() });
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Stock Adjustment Report", SearchCriteriaControl = new ucStockAdjustment() });
            transactionreports.SubCategory.Add(new ReportHolder() { ReportName = "Processing slippages", SearchCriteriaControl = new ucStockSlippage() });
            stockReportList.Add(transactionreports);

            ReportHolder StockCountingReports = new ReportHolder() { ReportName = "Stock Counting Reports" };
            StockCountingReports.SubCategory.Add(new ReportHolder() { ReportName = "Sheets", SearchCriteriaControl = new ucStockCountingList() });
            StockCountingReports.SubCategory.Add(new ReportHolder() { ReportName = "Consolidated", SearchCriteriaControl = new ucConsolidatedCounting() });
            stockReportList.Add(StockCountingReports);

            ShowReportForm(stockReportList, "Stock");
        }
        private void bbiSupplierReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();

            ReportHolder supplierwisereports = new ReportHolder() { ReportName = "Supplier Wise Reports" };
            supplierwisereports.SubCategory.Add(new ReportHolder() { ReportName = "Purchases", SearchCriteriaControl = new ucPurchases() }); 
            supplierwisereports.SubCategory.Add(new ReportHolder() { ReportName = "sales", SearchCriteriaControl = new ucSupplierWiseSales("S") });
            supplierwisereports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatches", SearchCriteriaControl = new ucSupplierWiseSales("D") });
            supplierwisereports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Refunds", SearchCriteriaControl = new ucSupplierWiseSales("B") });
            supplierwisereports.SubCategory.Add(new ReportHolder() { ReportName = "Customer Refunds", SearchCriteriaControl = new ucSupplierWiseSales("C") });
            supplierwisereports.SubCategory.Add(new ReportHolder() { ReportName = "Item Margin", SearchCriteriaControl = new ucSupplierItemMargin() });
            supplierwisereports.SubCategory.Add(new ReportHolder() { ReportName = "Stock Summary", SearchCriteriaControl = new ucSupplierStockSummary() });
            reportList.Add(supplierwisereports);

            ReportHolder supplierReports = new ReportHolder() { ReportName = "Supplier Reports" };
            supplierReports.SubCategory.Add(new ReportHolder() { ReportName = "Items", SearchCriteriaControl = new ucSupplierItems() });
            supplierReports.SubCategory.Add(new ReportHolder() { ReportName = "Invoice List", SearchCriteriaControl = new ucInvoiceList() });
            supplierReports.SubCategory.Add(new ReportHolder() { ReportName = "Indent", SearchCriteriaControl = new ucDealerIndent() });
            supplierReports.SubCategory.Add(new ReportHolder() { ReportName = "Indent List", SearchCriteriaControl = new ucSupplierIndentList() });
            supplierReports.SubCategory.Add(new ReportHolder() { ReportName = "Returns Sheets", SearchCriteriaControl = new ucSupplierReturnsList() });
            reportList.Add(supplierReports);

            ShowReportForm(reportList, "Supplier");
        }
        private void bbiWarehouseReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();

            ReportHolder auditReports = new ReportHolder() { ReportName = "Audit Reports" };
            //auditReports.SubCategory.Add(new ReportHolder() { ReportName = "Stock Counting Sheets", SearchCriteriaControl = new ucStockCountingList() });
            auditReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Sale Price", SearchCriteriaControl = new ucSalePriceAudit() });
            auditReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Cost Price", SearchCriteriaControl = new ucCostPriceAudit() });            
            reportList.Add(auditReports);

            ReportHolder profitabilityReports = new ReportHolder() { ReportName = "Profitability Reports" };
            profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Periodicity", SearchCriteriaControl = new ucProfitabilityPeriodicity() });
            //profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Wise", SearchCriteriaControl = new ucProfitabilityItemWise() });
            //profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Tax Wise", SearchCriteriaControl = new ucProfitabilityTaxWise() });
            profitabilityReports.SubCategory.Add(new ReportHolder() { ReportName = "Item Margin", SearchCriteriaControl = new ucItemMargin() });
            reportList.Add(profitabilityReports);

            ReportHolder stockandsalereports = new ReportHolder() { ReportName = "Stock and Sales", SearchCriteriaControl = new ucStockAndSales() };
            reportList.Add(stockandsalereports);

            ReportHolder taxReports = new ReportHolder() { ReportName = "Tax Reports" };
            taxReports.SubCategory.Add(new ReportHolder() { ReportName = "Tax break-up day wise", SearchCriteriaControl = new ucTaxBreakUpDayWise() });
            taxReports.SubCategory.Add(new ReportHolder() { ReportName = "Tax Wise sales", SearchCriteriaControl = new ucTaxWiseSales() });
            taxReports.SubCategory.Add(new ReportHolder() { ReportName = "Tax Wise Returns", SearchCriteriaControl = new ucTaxwiseReturns() });
            reportList.Add(taxReports);

            ShowReportForm(reportList, "Warehouse");
        }

        private void ShowReportForm(List<ReportHolder> reportHolders, string header)
        {
            frmReportPlaceHolder obj = new frmReportPlaceHolder(reportHolders, header);
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnProcessWHDispatch_ItemClick(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    if (XtraMessageBox.Show("Are sure want to process WH dispatch?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //        return;
            //    SplashScreenManager.ShowForm(typeof(frmProgress), true, true);
            //    new StockRepository().ProcessWarehouseDispatch(Utility.UserID);
            //    XtraMessageBox.Show("Dispatch processed succefully!");
            //    SplashScreenManager.CloseForm();
            //}
            //catch (Exception ex)
            //{
            //    SplashScreenManager.CloseForm();
            //    ErrorManagement.ErrorMgmt.ShowError(ex);
            //}
        }

        private void btnStockAdjustment_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStockAdjustment frm = new frmStockAdjustment();
            frm.ShowInTaskbar = false;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ControlBox = false;
            frm.IconOptions.ShowIcon = false;
            frm.ShowDialog();
        }

        private void btnItemLedger_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmItemLedger obj = new frmItemLedger();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnStockSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStockSummary obj = new frmStockSummary();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnNewDeal_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbiStockSlippage_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStockSlippage obj = new frmStockSlippage();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterParent;
            obj.ShowDialog();
        }

        private void bbiClassification_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmItemClassificationList() { MdiParent = this }.Show();
        }

    }
}