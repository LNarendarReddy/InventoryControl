namespace NSRetailPOS.Operations
{
    partial class frmOpetations
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpetations));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiReports = new DevExpress.XtraBars.BarButtonItem();
            this.btnStockEntry = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefreshData = new DevExpress.XtraBars.BarButtonItem();
            this.btnStockIn = new DevExpress.XtraBars.BarButtonItem();
            this.btnBranchRefund = new DevExpress.XtraBars.BarButtonItem();
            this.btnItem = new DevExpress.XtraBars.BarButtonItem();
            this.btnSupplierReturns = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBranchExpenses = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.bbiReports,
            this.btnStockEntry,
            this.btnRefreshData,
            this.btnStockIn,
            this.btnBranchRefund,
            this.btnItem,
            this.btnSupplierReturns,
            this.bbiBranchExpenses});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 10;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1295, 157);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // bbiReports
            // 
            this.bbiReports.Caption = "Reports";
            this.bbiReports.Id = 1;
            this.bbiReports.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiReports.ImageOptions.SvgImage")));
            this.bbiReports.Name = "bbiReports";
            this.bbiReports.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiReports_ItemClick);
            // 
            // btnStockEntry
            // 
            this.btnStockEntry.Caption = "Stock Entry";
            this.btnStockEntry.Id = 2;
            this.btnStockEntry.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnStockEntry.ImageOptions.SvgImage")));
            this.btnStockEntry.Name = "btnStockEntry";
            this.btnStockEntry.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStockEntry_ItemClick);
            // 
            // btnRefreshData
            // 
            this.btnRefreshData.Caption = "Refresh Data";
            this.btnRefreshData.Id = 4;
            this.btnRefreshData.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRefreshData.ImageOptions.SvgImage")));
            this.btnRefreshData.Name = "btnRefreshData";
            this.btnRefreshData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefreshData_ItemClick);
            // 
            // btnStockIn
            // 
            this.btnStockIn.Caption = "Stock In";
            this.btnStockIn.Id = 5;
            this.btnStockIn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnStockIn.ImageOptions.SvgImage")));
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStockIn_ItemClick);
            // 
            // btnBranchRefund
            // 
            this.btnBranchRefund.Caption = "Branch Refund";
            this.btnBranchRefund.Id = 6;
            this.btnBranchRefund.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBranchRefund.ImageOptions.SvgImage")));
            this.btnBranchRefund.Name = "btnBranchRefund";
            this.btnBranchRefund.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBranchRefund_ItemClick);
            // 
            // btnItem
            // 
            this.btnItem.Caption = "Item List";
            this.btnItem.Id = 7;
            this.btnItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnItem.ImageOptions.SvgImage")));
            this.btnItem.Name = "btnItem";
            this.btnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItem_ItemClick);
            // 
            // btnSupplierReturns
            // 
            this.btnSupplierReturns.Caption = "Supplier Returns";
            this.btnSupplierReturns.Id = 8;
            this.btnSupplierReturns.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSupplierReturns.ImageOptions.SvgImage")));
            this.btnSupplierReturns.Name = "btnSupplierReturns";
            this.btnSupplierReturns.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSupplierReturns_ItemClick);
            // 
            // bbiBranchExpenses
            // 
            this.bbiBranchExpenses.Caption = "Branch Expenses";
            this.bbiBranchExpenses.Id = 9;
            this.bbiBranchExpenses.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiBranchExpenses.ImageOptions.SvgImage")));
            this.bbiBranchExpenses.Name = "bbiBranchExpenses";
            this.bbiBranchExpenses.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBranchExpenses_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4,
            this.ribbonPageGroup2,
            this.ribbonPageGroup5,
            this.ribbonPageGroup1,
            this.ribbonPageGroup3});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Operations";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.btnItem);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Item";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnStockEntry);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnStockIn);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnBranchRefund);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnSupplierReturns);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Stock";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.bbiBranchExpenses);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "Branch Expenses";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiReports);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Reports";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageGroupAlignment.Far;
            this.ribbonPageGroup3.ItemLinks.Add(this.btnRefreshData);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 626);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1295, 22);
            // 
            // frmOpetations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 648);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.Image = global::NSRetailPOS.Properties.Resources.Victory_Bazars_Logo_New;
            this.IsMdiContainer = true;
            this.Name = "frmOpetations";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "NS Retail POS Opetations";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem bbiReports;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btnStockEntry;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem btnRefreshData;
        private DevExpress.XtraBars.BarButtonItem btnStockIn;
        private DevExpress.XtraBars.BarButtonItem btnBranchRefund;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarButtonItem btnItem;
        private DevExpress.XtraBars.BarButtonItem btnSupplierReturns;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.BarButtonItem bbiBranchExpenses;
    }
}