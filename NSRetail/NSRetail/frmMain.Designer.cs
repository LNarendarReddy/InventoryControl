namespace NSRetail
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject21 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject22 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject23 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject24 = new DevExpress.Utils.SerializableAppearanceObject();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnBranch = new DevExpress.XtraBars.BarButtonItem();
            this.btnBranchCouter = new DevExpress.XtraBars.BarButtonItem();
            this.btnUser = new DevExpress.XtraBars.BarButtonItem();
            this.btnCategory = new DevExpress.XtraBars.BarButtonItem();
            this.btnDealer = new DevExpress.XtraBars.BarButtonItem();
            this.btnTaxMaster = new DevExpress.XtraBars.BarButtonItem();
            this.btnModeOfPayment = new DevExpress.XtraBars.BarButtonItem();
            this.btnUnitsofMeasure = new DevExpress.XtraBars.BarButtonItem();
            this.btnItem = new DevExpress.XtraBars.BarButtonItem();
            this.btnLogout = new DevExpress.XtraBars.BarButtonItem();
            this.btnChangePassword = new DevExpress.XtraBars.BarButtonItem();
            this.btnBarCodePrint = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrinterMaster = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.btnStockEntry = new DevExpress.XtraBars.BarButtonItem();
            this.btnStockDispatch = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnBranch,
            this.btnBranchCouter,
            this.btnUser,
            this.btnCategory,
            this.btnDealer,
            this.btnTaxMaster,
            this.btnModeOfPayment,
            this.btnUnitsofMeasure,
            this.btnItem,
            this.btnLogout,
            this.btnChangePassword,
            this.btnBarCodePrint,
            this.btnPrinterMaster,
            this.btnStockEntry,
            this.btnStockDispatch});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 20;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.ribbonPage3});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.ribbonControl1.Size = new System.Drawing.Size(1178, 161);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnBranch
            // 
            this.btnBranch.Caption = "Branch";
            this.btnBranch.Id = 1;
            this.btnBranch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBranch.ImageOptions.SvgImage")));
            this.btnBranch.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBranch.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnBranch.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBranch.ItemAppearance.Normal.Options.UseFont = true;
            this.btnBranch.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBranch.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnBranch.Name = "btnBranch";
            this.btnBranch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBranch_ItemClick);
            // 
            // btnBranchCouter
            // 
            this.btnBranchCouter.Caption = "Branch Counter";
            this.btnBranchCouter.Id = 2;
            this.btnBranchCouter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBranchCouter.ImageOptions.SvgImage")));
            this.btnBranchCouter.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBranchCouter.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnBranchCouter.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBranchCouter.ItemAppearance.Normal.Options.UseFont = true;
            this.btnBranchCouter.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBranchCouter.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnBranchCouter.Name = "btnBranchCouter";
            this.btnBranchCouter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBranchCouter_ItemClick);
            // 
            // btnUser
            // 
            this.btnUser.Caption = "User";
            this.btnUser.Id = 3;
            this.btnUser.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUser.ImageOptions.SvgImage")));
            this.btnUser.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUser.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnUser.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUser.ItemAppearance.Normal.Options.UseFont = true;
            this.btnUser.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUser.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnUser.Name = "btnUser";
            this.btnUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUser_ItemClick);
            // 
            // btnCategory
            // 
            this.btnCategory.Caption = "Category";
            this.btnCategory.Id = 4;
            this.btnCategory.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCategory.ImageOptions.SvgImage")));
            this.btnCategory.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCategory.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnCategory.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCategory.ItemAppearance.Normal.Options.UseFont = true;
            this.btnCategory.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCategory.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCategory_ItemClick);
            // 
            // btnDealer
            // 
            this.btnDealer.Caption = "Dealer";
            this.btnDealer.Id = 5;
            this.btnDealer.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDealer.ImageOptions.SvgImage")));
            this.btnDealer.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnDealer.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnDealer.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnDealer.ItemAppearance.Normal.Options.UseFont = true;
            this.btnDealer.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnDealer.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnDealer.Name = "btnDealer";
            this.btnDealer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDealer_ItemClick);
            // 
            // btnTaxMaster
            // 
            this.btnTaxMaster.Caption = "Tax";
            this.btnTaxMaster.Id = 6;
            this.btnTaxMaster.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnTaxMaster.ImageOptions.SvgImage")));
            this.btnTaxMaster.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnTaxMaster.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnTaxMaster.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnTaxMaster.ItemAppearance.Normal.Options.UseFont = true;
            this.btnTaxMaster.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnTaxMaster.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnTaxMaster.Name = "btnTaxMaster";
            this.btnTaxMaster.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTaxMaster_ItemClick);
            // 
            // btnModeOfPayment
            // 
            this.btnModeOfPayment.Caption = "Mode Of Payment";
            this.btnModeOfPayment.Id = 7;
            this.btnModeOfPayment.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnModeOfPayment.ImageOptions.SvgImage")));
            this.btnModeOfPayment.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnModeOfPayment.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnModeOfPayment.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnModeOfPayment.ItemAppearance.Normal.Options.UseFont = true;
            this.btnModeOfPayment.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnModeOfPayment.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnModeOfPayment.Name = "btnModeOfPayment";
            this.btnModeOfPayment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModeOfPayment_ItemClick);
            // 
            // btnUnitsofMeasure
            // 
            this.btnUnitsofMeasure.Caption = "Units Of Measurement";
            this.btnUnitsofMeasure.Id = 8;
            this.btnUnitsofMeasure.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUnitsofMeasure.ImageOptions.SvgImage")));
            this.btnUnitsofMeasure.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUnitsofMeasure.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnUnitsofMeasure.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUnitsofMeasure.ItemAppearance.Normal.Options.UseFont = true;
            this.btnUnitsofMeasure.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUnitsofMeasure.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnUnitsofMeasure.Name = "btnUnitsofMeasure";
            this.btnUnitsofMeasure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUnitsofMeasure_ItemClick);
            // 
            // btnItem
            // 
            this.btnItem.Caption = "Item";
            this.btnItem.Id = 10;
            this.btnItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnItem.ImageOptions.SvgImage")));
            this.btnItem.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnItem.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnItem.ItemAppearance.Normal.Options.UseFont = true;
            this.btnItem.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnItem.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnItem.Name = "btnItem";
            this.btnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItem_ItemClick);
            // 
            // btnLogout
            // 
            this.btnLogout.Caption = "Log out";
            this.btnLogout.Id = 13;
            this.btnLogout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLogout.ImageOptions.SvgImage")));
            this.btnLogout.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnLogout.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnLogout.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnLogout.ItemAppearance.Normal.Options.UseFont = true;
            this.btnLogout.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnLogout.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLogout_ItemClick);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Caption = "Change Password";
            this.btnChangePassword.Id = 14;
            this.btnChangePassword.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnChangePassword.ImageOptions.SvgImage")));
            this.btnChangePassword.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnChangePassword.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnChangePassword.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnChangePassword.ItemAppearance.Normal.Options.UseFont = true;
            this.btnChangePassword.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnChangePassword.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChangePassword_ItemClick);
            // 
            // btnBarCodePrint
            // 
            this.btnBarCodePrint.Caption = "Print Barcode";
            this.btnBarCodePrint.Id = 16;
            this.btnBarCodePrint.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBarCodePrint.ImageOptions.SvgImage")));
            this.btnBarCodePrint.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBarCodePrint.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnBarCodePrint.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBarCodePrint.ItemAppearance.Normal.Options.UseFont = true;
            this.btnBarCodePrint.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBarCodePrint.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnBarCodePrint.Name = "btnBarCodePrint";
            this.btnBarCodePrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBarCodePrint_ItemClick);
            // 
            // btnPrinterMaster
            // 
            this.btnPrinterMaster.Caption = "Printer Master";
            this.btnPrinterMaster.Id = 17;
            this.btnPrinterMaster.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPrinterMaster.ImageOptions.SvgImage")));
            this.btnPrinterMaster.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnPrinterMaster.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnPrinterMaster.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnPrinterMaster.ItemAppearance.Normal.Options.UseFont = true;
            this.btnPrinterMaster.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnPrinterMaster.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnPrinterMaster.Name = "btnPrinterMaster";
            this.btnPrinterMaster.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrinterMaster_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup4});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Masters";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBranch);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBranchCouter);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnUser);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnCategory);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDealer);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnModeOfPayment);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnUnitsofMeasure);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnTaxMaster);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBarCodePrint);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnPrinterMaster);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Masters";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageGroupAlignment.Far;
            this.ribbonPageGroup4.ItemLinks.Add(this.btnChangePassword);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnLogout);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Profile";
            // 
            // ribbonPage3
            // 
            this.ribbonPage3.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.ribbonPage3.Appearance.Options.UseFont = true;
            this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.ribbonPage3.Name = "ribbonPage3";
            this.ribbonPage3.Text = "Operations";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnItem);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Item";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnStockEntry);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnStockDispatch);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Stock";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            editorButtonImageOptions6.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions6.SvgImage")));
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject21, serializableAppearanceObject22, serializableAppearanceObject23, serializableAppearanceObject24, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 735);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1178, 24);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // btnStockEntry
            // 
            this.btnStockEntry.Caption = "Stock Entry";
            this.btnStockEntry.Id = 18;
            this.btnStockEntry.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnStockEntry.ImageOptions.SvgImage")));
            this.btnStockEntry.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnStockEntry.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnStockEntry.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnStockEntry.ItemAppearance.Normal.Options.UseFont = true;
            this.btnStockEntry.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnStockEntry.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnStockEntry.Name = "btnStockEntry";
            this.btnStockEntry.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStockEntry_ItemClick);
            // 
            // btnStockDispatch
            // 
            this.btnStockDispatch.Caption = "Stock Dispatch";
            this.btnStockDispatch.Id = 19;
            this.btnStockDispatch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnStockDispatch.ImageOptions.SvgImage")));
            this.btnStockDispatch.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnStockDispatch.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnStockDispatch.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnStockDispatch.ItemAppearance.Normal.Options.UseFont = true;
            this.btnStockDispatch.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnStockDispatch.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnStockDispatch.Name = "btnStockDispatch";
            this.btnStockDispatch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStockDispatch_ItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 759);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Victory Bazars";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.BarButtonItem btnBranch;
        private DevExpress.XtraBars.BarButtonItem btnBranchCouter;
        private DevExpress.XtraBars.BarButtonItem btnUser;
        private DevExpress.XtraBars.BarButtonItem btnCategory;
        private DevExpress.XtraBars.BarButtonItem btnDealer;
        private DevExpress.XtraBars.BarButtonItem btnTaxMaster;
        private DevExpress.XtraBars.BarButtonItem btnModeOfPayment;
        private DevExpress.XtraBars.BarButtonItem btnUnitsofMeasure;
        private DevExpress.XtraBars.BarButtonItem btnItem;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem btnLogout;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarButtonItem btnChangePassword;
        private DevExpress.XtraBars.BarButtonItem btnBarCodePrint;
        private DevExpress.XtraBars.BarButtonItem btnPrinterMaster;
        private DevExpress.XtraBars.BarButtonItem btnStockEntry;
        private DevExpress.XtraBars.BarButtonItem btnStockDispatch;
    }
}

