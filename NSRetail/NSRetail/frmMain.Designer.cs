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
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnBranch = new DevExpress.XtraBars.BarButtonItem();
            this.btnBranchCouter = new DevExpress.XtraBars.BarButtonItem();
            this.btnUser = new DevExpress.XtraBars.BarButtonItem();
            this.btnCategory = new DevExpress.XtraBars.BarButtonItem();
            this.btnSupplier = new DevExpress.XtraBars.BarButtonItem();
            this.btnTaxMaster = new DevExpress.XtraBars.BarButtonItem();
            this.btnModeOfPayment = new DevExpress.XtraBars.BarButtonItem();
            this.btnUnitsofMeasure = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
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
            this.btnSupplier,
            this.btnTaxMaster,
            this.btnModeOfPayment,
            this.btnUnitsofMeasure,
            this.barButtonItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 11;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.ribbonPage3});
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
            // btnSupplier
            // 
            this.btnSupplier.Caption = "Supplier";
            this.btnSupplier.Id = 5;
            this.btnSupplier.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSupplier.ImageOptions.SvgImage")));
            this.btnSupplier.ItemAppearance.Hovered.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSupplier.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnSupplier.ItemAppearance.Normal.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSupplier.ItemAppearance.Normal.Options.UseFont = true;
            this.btnSupplier.ItemAppearance.Pressed.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSupplier.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnSupplier.Name = "btnSupplier";
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
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Masters";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBranch);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBranchCouter);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnUser);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnCategory);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSupplier);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnTaxMaster);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnModeOfPayment);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnUnitsofMeasure);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Masters";
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
            // ribbonPage3
            // 
            this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.ribbonPage3.Name = "ribbonPage3";
            this.ribbonPage3.Text = "Operations";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Item";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Item";
            this.barButtonItem1.Id = 10;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Stock";
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
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem btnSupplier;
        private DevExpress.XtraBars.BarButtonItem btnTaxMaster;
        private DevExpress.XtraBars.BarButtonItem btnModeOfPayment;
        private DevExpress.XtraBars.BarButtonItem btnUnitsofMeasure;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
    }
}

