using DataAccess;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using ErrorManagement;
using Microsoft.Win32;
using NSRetail.Countning;
using NSRetail.Login;
using NSRetail.Master;
using NSRetail.Master.User;
using NSRetail.ReportForms;
using NSRetail.ReportForms.Branch;
using NSRetail.ReportForms.Branch.BranchReports;
using NSRetail.ReportForms.Branch.POSReports;
using NSRetail.ReportForms.Stock.StockCounting;
using NSRetail.ReportForms.Stock.StockReports;
using NSRetail.ReportForms.Stock.TransactionReports;
using NSRetail.ReportForms.Supplier.SupplierReports;
using NSRetail.ReportForms.Supplier.SupplierWiseReports;
using NSRetail.ReportForms.Wareshouse.Audit;
using NSRetail.ReportForms.Wareshouse.Differences;
using NSRetail.ReportForms.Wareshouse.Profitability;
using NSRetail.ReportForms.Wareshouse.StockAndSale;
using NSRetail.ReportForms.Wareshouse.TaxBreakUp;
using NSRetail.Stock;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
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

        public static bool skipAccessRefresh = false;

        public event EventHandler RefreshBaseLineData;

        public Timer UpTime = new Timer() { Interval = 1000 };
        public Timer SyncTime = new Timer() { Interval = 1000 };

        private int syncTimeCounter;
        private int upTimeCounter;
        private int upTimeExceededCounter;
        private bool messageBoxOpen = false;
        private readonly int syncInterval = 2 * 60 * 60;
        private readonly int maxUpTime = 8 * 60 * 60;

        public void BeginTimer()
        {
            syncTimeCounter = syncInterval;
            UpTime.Tick += UpTime_Tick;
            SyncTime.Tick += SyncTime_Tick;
            UpTime.Start();
            SyncTime.Start();
        }

        private void SyncTime_Tick(object sender, EventArgs e)
        {
            syncTimeCounter--;
            if (syncTimeCounter <= 0)
            {
                skipAccessRefresh = true;
                Utility.FillBaseLine();
                skipAccessRefresh = false;
                syncTimeCounter = syncInterval;
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(syncTimeCounter);
            lblSyncTime.Caption = $"Next app sync in : {timeSpan}";
            lblSyncTime.ItemAppearance.Normal.ForeColor = syncTimeCounter < 600 ? Color.Red : Color.Empty;
        }

        private void UpTime_Tick(object sender, EventArgs e)
        {
            upTimeCounter++;
            TimeSpan timeSpan = TimeSpan.FromSeconds(upTimeCounter);
            lblUpTime.Caption = $"Total up time : {timeSpan}";
            if (upTimeCounter > maxUpTime)
            {
                lblUpTime.ItemAppearance.Normal.ForeColor = Color.Red;
                if (upTimeExceededCounter <= 0)
                {
                    upTimeExceededCounter = 2 * 60;
                    if (!messageBoxOpen)
                    {
                        messageBoxOpen = true;
                        XtraMessageBox.Show($"Application running for last {timeSpan}, please relaunch for uninterrupted usage", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        messageBoxOpen = false;
                    }
                }
                upTimeExceededCounter--;
            }
        }

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

        public void bbiRefreshData_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(frmProgress), true, true);
            Utility.FillBaseLine();            
            RefreshBaseLineData?.Invoke(null, null);
            //AccessUtility.SetStatusByAccess(rpOperations, rpReports, rpOffers, rpUserAccess, rpAdmin, rpItemExtras);
            AccessUtility.SetStatusByAccess(ribbonControl1);
            SplashScreenManager.CloseForm();
            syncTimeCounter = syncInterval;
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
            //AccessUtility.SetStatusByAccess(rpOperations, rpReports, rpOffers, rpUserAccess, rpAdmin, rpItemExtras);
            AccessUtility.SetStatusByAccess(ribbonControl1);
            FillFinYears();
            BeginTimer();
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

            ReportHolder branchReports = new ReportHolder("13CDACA3-DD86-4DC3-A22A-0FA1381D0CA2") { ReportName = "Branch Reports" };
            branchReports.SubCategory.Add(new ReportHolder("16EAA8C7-7C36-4296-A386-F5270AA2992B") { ReportName = "Branch Indent (2M)", SearchCriteriaControl = new ucBranchIndent() });
            branchReports.SubCategory.Add(new ReportHolder("20F8D1C7-EA5A-40E8-833B-EE18D5E23321") { ReportName = "Branch Indent (6M)", SearchCriteriaControl = new ucBranchIndentByAVG() });
            branchReports.SubCategory.Add(new ReportHolder("20F8D1C7-EA5A-40E8-833B-EE18D5E23321") { ReportName = "Branch Indent (6M + 60% capped)", SearchCriteriaControl = new ucBranchIndentByAVG60Capped() });
            branchReports.SubCategory.Add(new ReportHolder("CB3F9A60-9E1B-4FF8-BFF9-FD8DDD368AD7") { ReportName = "Branch Indent List", SearchCriteriaControl = new ucBranchIndentList() });
            branchReports.SubCategory.Add(new ReportHolder("3F7D220A-A33D-4488-804D-8B7252BD9840") { ReportName = "Dispatch List", SearchCriteriaControl = new ucDispatchList() });
            branchReports.SubCategory.Add(new ReportHolder("C345745A-14F3-45CC-887C-83F1BCB9FBCA") { ReportName = "Dispatch DC List", SearchCriteriaControl = new ucDispatchDCList() });
            branchReports.SubCategory.Add(new ReportHolder("E9379B7D-94A5-452F-AB66-26BFC0EC1A86") { ReportName = "Branch Refund sheets", SearchCriteriaControl = new ucBranchRefunds() });
            branchReports.SubCategory.Add(new ReportHolder("691353C7-AB84-4B5E-B771-48514728780F") { ReportName = "Dispatch Differences", SearchCriteriaControl = new ucDispatchDifferences() });
            branchReports.SubCategory.Add(new ReportHolder("2CA0BFD7-6A3D-4E7C-9A1E-1ADCCEB827F0") { ReportName = "Expenses", SearchCriteriaControl = new ucBranchExpenseList() });
            reportList.Add(branchReports);

            ReportHolder POSReports = new ReportHolder("B5304348-4CB3-45A7-BA77-EB6597A080EE") { ReportName = "POS Reports" };
            POSReports.SubCategory.Add(new ReportHolder("32E47050-3D12-4E84-92D7-DB8CAFF0E734") { ReportName = "Day closure List", SearchCriteriaControl = new ucDayClosureList() });
            POSReports.SubCategory.Add(new ReportHolder("24CFD73A-1E90-40B6-8858-9B54D8B0DAA1") { ReportName = "Customer Refunds By Bill #", SearchCriteriaControl = new ucCRefundSheets() });
            POSReports.SubCategory.Add(new ReportHolder("3FAFD637-7559-407F-A795-ABFD217A1959") { ReportName = "Bills by Amount", SearchCriteriaControl = new ucBillSearchByAmount() });
            POSReports.SubCategory.Add(new ReportHolder("744A7572-FB6D-4CEF-A06B-234D1966F248") { ReportName = "Customer Details", SearchCriteriaControl = new ucCustomerDetails() });
            POSReports.SubCategory.Add(new ReportHolder("A4FD9F89-D28A-41DF-8DD9-83CB6C0C55B3") { ReportName = "B2B Credit Bill Payments", SearchCriteriaControl = new ucCreditBillPayments() });
            POSReports.SubCategory.Add(new ReportHolder("E1D65F8D-E918-41BF-A6BB-3D7BDAF3C1B8") { ReportName = "Installed versions", SearchCriteriaControl = new ucInstalledVersions() });
            POSReports.SubCategory.Add(new ReportHolder("54C8D668-1B98-43AD-BFA0-B828357A4101") { ReportName = "HDD SNo history", SearchCriteriaControl = new ucHDDClearhistory() });
            reportList.Add(POSReports);

            ReportHolder VoidReports = new ReportHolder("F29F47AA-857C-4725-A2B7-BABBF90187FA") { ReportName = "Void Data Analysis" };
            VoidReports.SubCategory.Add(new ReportHolder("8E9023EE-4FD7-4A88-B784-D2A6985512A3") { ReportName = "Bills", SearchCriteriaControl = new ucVoidDataAnalysis_Bills() });
            VoidReports.SubCategory.Add(new ReportHolder("D2288844-91ED-4E60-9AA6-E4B75562C513") { ReportName = "Items", SearchCriteriaControl = new ucVoidDataAnalysis_Bills() });
            VoidReports.SubCategory.Add(new ReportHolder("FB32CA0D-4159-4141-92D8-13C7C8023889") { ReportName = "Items_Old", SearchCriteriaControl = new ucVoidItems() });
            reportList.Add(VoidReports);

            ShowReportForm(reportList, "Branch");
        }
        private void bbiStockReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> stockReportList = new List<ReportHolder>();

            ReportHolder StockReports = new ReportHolder("070F937D-47E4-4D18-92CE-FB0B09E05C86") { ReportName = "Stock Reports" };
            StockReports.SubCategory.Add(new ReportHolder("A363141E-CB16-473F-A419-EB9A1F6568E2") { ReportName = "Stock summary by Price", SearchCriteriaControl = new ucStockSummaryByPrice() });
            StockReports.SubCategory.Add(new ReportHolder("3E3249DF-4A87-45C5-8FAD-8BC507A7F47F") { ReportName = "Current stock (with open sale)", SearchCriteriaControl = new ucCurrentStock() });
            StockReports.SubCategory.Add(new ReportHolder("16C9400E-5954-4DAE-861C-1F0E79074041") { ReportName = "Non-Moving stock", SearchCriteriaControl = new ucNonMovingStock() });
            StockReports.SubCategory.Add(new ReportHolder("F6ADE590-D672-4522-B54C-552C349C77F8") { ReportName = "Stock As on date", SearchCriteriaControl = new ucStockAsOnDate() });
            StockReports.SubCategory.Add(new ReportHolder("D8D70589-B1D1-40DB-AF85-351C4A576774") { ReportName = "Zero Stock", SearchCriteriaControl = new ucZeroStock() });
            StockReports.SubCategory.Add(new ReportHolder("17EBA4EE-96C4-4C8E-84F9-11D1EE9ADB73") { ReportName = "Left over prices", SearchCriteriaControl = new ucLeftOverPrices() });
            stockReportList.Add(StockReports);

            ReportHolder transactionreports = new ReportHolder("24EFE702-B7A6-4723-83C6-509B50D3031E") { ReportName = "Transaction Reports" };
            transactionreports.SubCategory.Add(new ReportHolder("47074E29-6645-49B1-894D-C7F3D17912AA") { ReportName = "Purchases", SearchCriteriaControl = new ucPurchases() });
            transactionreports.SubCategory.Add(new ReportHolder("0A4AA7FA-890B-4887-BE24-01C864AD92E8") { ReportName = "Dispatches", SearchCriteriaControl = new ucBranchTransactions("D") });
            transactionreports.SubCategory.Add(new ReportHolder("209ADE22-1126-4169-8733-4EF32533EC41") { ReportName = "Sales", SearchCriteriaControl = new ucSales() });
            transactionreports.SubCategory.Add(new ReportHolder("4CD45D75-C4DA-4A84-B533-4383D60D4097") { ReportName = "Customer Refunds", SearchCriteriaControl = new ucBranchTransactions("C") });
            transactionreports.SubCategory.Add(new ReportHolder("0E1F8A05-8676-48D6-8B4A-132D2DDAE578") { ReportName = "Branch Refunds", SearchCriteriaControl = new ucBranchTransactions("B") });
            transactionreports.SubCategory.Add(new ReportHolder("DF7845F3-FE41-41B7-A302-C63C5E4AF514") { ReportName = "Supplier Refunds", SearchCriteriaControl = new ucSupplierRefunds() });

            transactionreports.SubCategory.Add(new ReportHolder("BDD489E4-BDE8-4C20-B5BC-E483A944879B") { ReportName = "Day sale by Payment Method", SearchCriteriaControl = new ucSaleByMOP() });
            transactionreports.SubCategory.Add(new ReportHolder("E77D80D7-9A10-4B3B-AFF5-BD81A4081118") { ReportName = "Stock Adjustment Report", SearchCriteriaControl = new ucStockAdjustment() });
            stockReportList.Add(transactionreports);

            ReportHolder processingReports = new ReportHolder("3E5CB3D5-B334-4D24-B897-C97B4EFF8AA6") { ReportName = "Bulk Processing Reports" };
            processingReports.SubCategory.Add(new ReportHolder("884FDED9-ED29-49ED-84D1-A68EF1EAC794") { ReportName = "Bulk Processing", SearchCriteriaControl = new ucBulkProcessing() });
            processingReports.SubCategory.Add(new ReportHolder("B10365BB-7DF0-42C0-A921-2C00C7406EF6") { ReportName = "Processing slippages", SearchCriteriaControl = new ucStockSlippage() });
            processingReports.SubCategory.Add(new ReportHolder("C13DCD05-3CDA-44F9-ACE5-15C299CB5AD7") { ReportName = "Processing Indent", SearchCriteriaControl = new ucProcessingIndent() });
            processingReports.SubCategory.Add(new ReportHolder("C13DCD05-3CDA-44F9-ACE5-15C299CB5AD7") { ReportName = "Processing Indent by Dispatch", SearchCriteriaControl = new ucProcessingIndentByDispatch() });
            stockReportList.Add(processingReports);


            ReportHolder StockCountingReports = new ReportHolder("FD85BDF9-79BB-4357-8695-0A5372625842") { ReportName = "Stock Counting Reports" };
            StockCountingReports.SubCategory.Add(new ReportHolder("D37864CF-D2DB-446B-8852-159D9AC12BCF") { ReportName = "Sheets", SearchCriteriaControl = new ucStockCountingList() });
            StockCountingReports.SubCategory.Add(new ReportHolder("FEB85EBD-E9FF-4A88-AE74-20C2C403CE3D") { ReportName = "Counting Approvals", SearchCriteriaControl = new ucCountingApprovals() });
            //StockCountingReports.SubCategory.Add(new ReportHolder() { ReportName = "Consolidated", SearchCriteriaControl = new ucConsolidatedCounting() });
            stockReportList.Add(StockCountingReports);

            ShowReportForm(stockReportList, "Stock");
        }
        private void bbiSupplierReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();

            ReportHolder supplierwisereports = new ReportHolder("70686424-1D18-41EE-8426-3168074B098D") { ReportName = "Supplier Wise Reports(Not Accurate)" };
            supplierwisereports.SubCategory.Add(new ReportHolder("C66FADA5-BD21-477C-A58D-5EB9FDCBA463") { ReportName = "Purchases", SearchCriteriaControl = new ucPurchases() }); 
            supplierwisereports.SubCategory.Add(new ReportHolder("78BB09BD-CA0D-4580-B6B5-489DBCC26389") { ReportName = "sales", SearchCriteriaControl = new ucSupplierWiseSales("S") });
            supplierwisereports.SubCategory.Add(new ReportHolder("00B713D0-9B65-453C-ACFF-A1371DFCF07E") { ReportName = "Dispatches", SearchCriteriaControl = new ucSupplierWiseSales("D") });
            supplierwisereports.SubCategory.Add(new ReportHolder("E0605413-A0F0-4234-8D79-16564497EF4A") { ReportName = "Branch Refunds", SearchCriteriaControl = new ucSupplierWiseSales("B") });
            supplierwisereports.SubCategory.Add(new ReportHolder("3416A690-1410-4C68-9EAB-E74212B91E62") { ReportName = "Customer Refunds", SearchCriteriaControl = new ucSupplierWiseSales("C") });
            supplierwisereports.SubCategory.Add(new ReportHolder("D59950BA-B3CD-409B-B558-706EA7875A55") { ReportName = "Item Margin", SearchCriteriaControl = new ucSupplierItemMargin() });
            supplierwisereports.SubCategory.Add(new ReportHolder("3DDB8D32-2168-4CA5-B31B-4F03B45D503F") { ReportName = "Stock Summary", SearchCriteriaControl = new ucSupplierStockSummary() });
            reportList.Add(supplierwisereports);

            ReportHolder supplierReports = new ReportHolder("DB8EBF98-6672-4B9D-9ECC-B9A94B2BFC69") { ReportName = "Supplier Reports" };
            supplierReports.SubCategory.Add(new ReportHolder("06AD8BBA-C34F-4739-9B19-C8AA55F89624") { ReportName = "Items", SearchCriteriaControl = new ucSupplierItems() });
            supplierReports.SubCategory.Add(new ReportHolder("F01FA427-4A85-4600-B27A-5A31278D8FA5") { ReportName = "Invoice List", SearchCriteriaControl = new ucInvoiceList() });
            supplierReports.SubCategory.Add(new ReportHolder("7A2064BB-F6AE-4E4C-8DE2-2EC267F71134") { ReportName = "Indent", SearchCriteriaControl = new ucDealerIndent() });
            supplierReports.SubCategory.Add(new ReportHolder("8A514101-90D5-4240-940C-AA49D85A5BDC") { ReportName = "Indent List", SearchCriteriaControl = new ucSupplierIndentList() });
            supplierReports.SubCategory.Add(new ReportHolder("65627E0A-E8A2-4D6B-9FB4-6CBFD9EDC296") { ReportName = "Returns Sheets", SearchCriteriaControl = new ucSupplierReturnsList() });
            reportList.Add(supplierReports);

            ShowReportForm(reportList, "Supplier");
        }
        private void bbiWarehouseReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();

            ReportHolder auditReports = new ReportHolder("4DF4990B-D9C0-4064-8D6D-0BED4B28DD83") { ReportName = "Audit Reports" };
            auditReports.SubCategory.Add(new ReportHolder("F175740E-D7CE-4294-808B-54A32A475EEB") { ReportName = "Item Sale Price", SearchCriteriaControl = new ucSalePriceAudit() });
            auditReports.SubCategory.Add(new ReportHolder("23AC4A6A-CD23-4597-8C32-812A3D757396") { ReportName = "Item Cost Price", SearchCriteriaControl = new ucCostPriceAudit() });      
            auditReports.SubCategory.Add(new ReportHolder("602C0AD1-DB66-415F-812B-F8739F162312") { ReportName = "Deleted Item codes", SearchCriteriaControl = new ucDeletedItemCodes() });      
            auditReports.SubCategory.Add(new ReportHolder("602C0AD1-DB66-415F-812B-F8739F162312") { ReportName = "Deleted Item prices", SearchCriteriaControl = new ucDeletedItemPrices() });
            auditReports.SubCategory.Add(new ReportHolder("CA340805-E65A-4FCA-8BF7-6A75630703E1") { ReportName = "Offer threshold", SearchCriteriaControl = new ucOfferThreshold() });
            auditReports.SubCategory.Add(new ReportHolder("D876AE08-B34A-4BE4-972D-2EFA6044FEFC") { ReportName = "Offer status", SearchCriteriaControl = new ucOfferStatus() });
            reportList.Add(auditReports);

            ReportHolder differenceReports = new ReportHolder("671AB139-0564-4D24-B5B3-F5D859EBEDEC") { ReportName = "Difference Reports" };
            differenceReports.SubCategory.Add(new ReportHolder("AC133FF5-DDDD-425C-B214-D26AF5E4BF6D") { ReportName = "Indent vs Dispatch", SearchCriteriaControl = new ucIndentDispatchDifferences() });
            differenceReports.SubCategory.Add(new ReportHolder("AC133FF5-DDDD-425C-B214-D26AF5E4BF6D") { ReportName = "Indent vs Dispatch Sufficieny (today)", SearchCriteriaControl = new ucDispatchVsIndentSufficiency() });
            //differenceReports.SubCategory.Add(new ReportHolder("4113BB0C-15C1-463B-938E-4DFA9D12A41C") { ReportName = "Item Margin", SearchCriteriaControl = new ucItemMargin() });
            reportList.Add(differenceReports);

            ReportHolder profitabilityReports = new ReportHolder("25939730-C13A-4939-B49F-0AAABCBBE467") { ReportName = "Profitability Reports" };
            profitabilityReports.SubCategory.Add(new ReportHolder("A4E43E52-34D0-44E8-9FCC-87E72362FB98") { ReportName = "Periodicity", SearchCriteriaControl = new ucProfitabilityPeriodicity() });
            profitabilityReports.SubCategory.Add(new ReportHolder("4113BB0C-15C1-463B-938E-4DFA9D12A41C") { ReportName = "Item Margin", SearchCriteriaControl = new ucItemMargin() });
            profitabilityReports.SubCategory.Add(new ReportHolder("019FFF15-A38E-411F-8A8A-5CBAB422856A") { ReportName = "Purchases vs Sales", SearchCriteriaControl = new ucPurchasesVsSales() });
            profitabilityReports.SubCategory.Add(new ReportHolder("4940D2A8-3DED-4285-A90E-7D4E7ADD1243") { ReportName = "Dispatches vs Sales", SearchCriteriaControl = new ucDispatchVsSales() });
            reportList.Add(profitabilityReports);

            ReportHolder stockandsalereports = new ReportHolder("D436829B-9B91-46F7-99D7-48CBD753D832") { ReportName = "Stock and Sales", SearchCriteriaControl = new ucStockAndSales() };
            reportList.Add(stockandsalereports);

            ReportHolder taxReports = new ReportHolder("DAD95EAE-153C-446C-8929-8D9F889DD866") { ReportName = "Tax Reports" };
            taxReports.SubCategory.Add(new ReportHolder("AC4338A1-7967-4042-8848-C915F7FF99DD") { ReportName = "Tax break-up day wise", SearchCriteriaControl = new ucTaxBreakUpDayWise() });
            taxReports.SubCategory.Add(new ReportHolder("D2658E11-DB33-42CE-89D1-1DA6AE616E3C") { ReportName = "Tax Wise sales", SearchCriteriaControl = new ucTaxWiseSales() });
            taxReports.SubCategory.Add(new ReportHolder("A39E96F4-F704-4587-B042-E83AC1B57290") { ReportName = "Tax Wise Returns", SearchCriteriaControl = new ucTaxwiseReturns() });
            taxReports.SubCategory.Add(new ReportHolder("36D2C1F5-8890-47F0-B2EE-4AD94F2ECFA3") { ReportName = "Tax break-up payment mode wise", SearchCriteriaControl = new ucTaxBreakUpPaymentWise() });
            taxReports.SubCategory.Add(new ReportHolder("C4DE504D-91F7-4D2C-B98B-88C522A9E197") { ReportName = "HSN Wise sales", SearchCriteriaControl = new ucHSNWiseSale() });
            reportList.Add(taxReports);

            ShowReportForm(reportList, "Warehouse");
        }

        private void ShowReportForm(List<ReportHolder> reportHolders, string header)
        {
            try
            {
                frmReportPlaceHolder obj = new frmReportPlaceHolder(reportHolders, header);
                obj.ShowInTaskbar = false;
                obj.WindowState = FormWindowState.Maximized;
                obj.IconOptions.ShowIcon = false;
                obj.MdiParent = this;
                obj.Show();
            }

            catch(Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
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

        private void FY_Select(object sender, ItemClickEventArgs e)
        {
            if(this.MdiChildren.Count() > 0 
                && XtraMessageBox.Show("All open windows will be closed before changing the database, do you want to close them automatically? Any unsaved changes will be lost"
                , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            this.MdiChildren.ToList().ForEach(x => x.Close());

            SQLCon.ChangeConnection(e.Item.Name);
            lblUserName.Caption = $"Logged In User : {Utility.FullName}   " +
                $"Version : {Utility.AppVersion} {Utility.VersionDate} - {ConfigurationManager.AppSettings["BuildType"]} - {e.Item.Caption}";
            
            skipAccessRefresh = e.Item.Name != "NSRetail";
            bbiRefreshData_ItemClick(sender, e);
            skipAccessRefresh = false;
        }

        private void FillFinYears()
        {
            foreach (DataRow drFY in Utility.dtConnectionInfo.Rows)
            {
                BarButtonItem barButtonItem = new BarButtonItem() { Name = drFY["DATABASENAME"].ToString(), Caption = drFY["DISPLAYNAME"].ToString() };
                barButtonItem.ItemClick += FY_Select;
                puFinYear.ItemLinks.Add(barButtonItem);
            }

            lblUserName.Caption = $"Logged In User : {Utility.FullName}   " +
               $"Version : {Utility.AppVersion} {Utility.VersionDate} - {ConfigurationManager.AppSettings["BuildType"]} - {Utility.dtConnectionInfo.Rows[0]["DISPLAYNAME"]}";
        }

        private void bbiAddProcessing_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmAddProcessing() { MdiParent = this }.Show();
        }

        private void bbiSliceCounting_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmSliceCounting() { MdiParent = this }.Show();
        }

        private void bbiClearLeftSales_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to clear test data?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            new frmClearLeftSales() {  MdiParent = this }.Show();
        }

        private void bbiBuildInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmBuildInfo().ShowDialog();
        }

        private void bbiRoles_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmRoleList() { MdiParent= this }.Show();
        }

        private void btnBrand_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBrand obj = new frmBrand();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnManufacturer_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmManufacturer obj = new frmManufacturer();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }
    }
}