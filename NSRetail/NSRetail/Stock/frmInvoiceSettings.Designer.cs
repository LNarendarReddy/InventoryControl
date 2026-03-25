namespace NSRetail.Stock
{
    partial class frmInvoiceSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInvoiceSettings));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblSupplierInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnContinue = new DevExpress.XtraEditors.SimpleButton();
            this.rgLorryFrightMode = new DevExpress.XtraEditors.RadioGroup();
            this.rgPriceEntryMethod = new DevExpress.XtraEditors.RadioGroup();
            this.rgInvoiceType = new DevExpress.XtraEditors.RadioGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgLorryFrightMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgPriceEntryMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgInvoiceType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblSupplierInfo);
            this.layoutControl1.Controls.Add(this.btnContinue);
            this.layoutControl1.Controls.Add(this.rgLorryFrightMode);
            this.layoutControl1.Controls.Add(this.rgPriceEntryMethod);
            this.layoutControl1.Controls.Add(this.rgInvoiceType);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(872, 120, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(592, 323);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblSupplierInfo
            // 
            this.lblSupplierInfo.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            this.lblSupplierInfo.Appearance.Options.UseFont = true;
            this.lblSupplierInfo.Appearance.Options.UseTextOptions = true;
            this.lblSupplierInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSupplierInfo.Location = new System.Drawing.Point(250, 20);
            this.lblSupplierInfo.Name = "lblSupplierInfo";
            this.lblSupplierInfo.Size = new System.Drawing.Size(92, 18);
            this.lblSupplierInfo.StyleController = this.layoutControl1;
            this.lblSupplierInfo.TabIndex = 0;
            this.lblSupplierInfo.Text = "labelControl1";
            // 
            // btnContinue
            // 
            this.btnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnContinue.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnContinue.ImageOptions.SvgImage")));
            this.btnContinue.Location = new System.Drawing.Point(423, 265);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(157, 36);
            this.btnContinue.StyleController = this.layoutControl1;
            this.btnContinue.TabIndex = 4;
            this.btnContinue.Text = "Continue";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // rgLorryFrightMode
            // 
            this.rgLorryFrightMode.EnterMoveNextControl = true;
            this.rgLorryFrightMode.Location = new System.Drawing.Point(125, 195);
            this.rgLorryFrightMode.Name = "rgLorryFrightMode";
            this.rgLorryFrightMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Add Charges to Invoice Amount"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Deduct Charges from Invoice Amount")});
            this.rgLorryFrightMode.Size = new System.Drawing.Size(455, 66);
            this.rgLorryFrightMode.StyleController = this.layoutControl1;
            this.rgLorryFrightMode.TabIndex = 3;
            // 
            // rgPriceEntryMethod
            // 
            this.rgPriceEntryMethod.EnterMoveNextControl = true;
            this.rgPriceEntryMethod.Location = new System.Drawing.Point(125, 125);
            this.rgPriceEntryMethod.Name = "rgPriceEntryMethod";
            this.rgPriceEntryMethod.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Cost Price Excluding Tax"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Cost Price Including Tax")});
            this.rgPriceEntryMethod.Size = new System.Drawing.Size(455, 66);
            this.rgPriceEntryMethod.StyleController = this.layoutControl1;
            this.rgPriceEntryMethod.TabIndex = 2;
            // 
            // rgInvoiceType
            // 
            this.rgInvoiceType.EnterMoveNextControl = true;
            this.rgInvoiceType.Location = new System.Drawing.Point(125, 55);
            this.rgInvoiceType.Name = "rgInvoiceType";
            this.rgInvoiceType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Regular Invoice"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Composition Invoice")});
            this.rgInvoiceType.Size = new System.Drawing.Size(455, 66);
            this.rgInvoiceType.StyleController = this.layoutControl1;
            this.rgInvoiceType.TabIndex = 1;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(592, 323);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.rgPriceEntryMethod;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 113);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 70);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(167, 70);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(572, 70);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Price Entry Method";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(101, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.rgLorryFrightMode;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 183);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 70);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(167, 70);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(572, 70);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Lorry Freight Mode";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(101, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnContinue;
            this.layoutControlItem4.Location = new System.Drawing.Point(411, 253);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(161, 50);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rgInvoiceType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 43);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 70);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(167, 70);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(572, 70);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Invoice Type";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(101, 15);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 253);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(411, 50);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem5.Control = this.lblSupplierInfo;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 15);
            this.layoutControlItem5.Size = new System.Drawing.Size(572, 43);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmInvoiceSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 323);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmInvoiceSettings";
            this.ShowInTaskbar = false;
            this.Text = "Invoice Settings";
            this.Load += new System.EventHandler(this.frmInvoiceSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgLorryFrightMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgPriceEntryMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgInvoiceType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.RadioGroup rgPriceEntryMethod;
        private DevExpress.XtraEditors.RadioGroup rgInvoiceType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.RadioGroup rgLorryFrightMode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnContinue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LabelControl lblSupplierInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}