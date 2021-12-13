namespace NSRetail
{
    partial class frmAddCode
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddCode));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.RootLayout = new DevExpress.XtraLayout.LayoutControl();
            this.btnGenerateSKU = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxAddSKUValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RootLayout)).BeginInit();
            this.RootLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxAddSKUValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // RootLayout
            // 
            this.RootLayout.Appearance.Control.Font = new System.Drawing.Font("Arial", 8F);
            this.RootLayout.Appearance.Control.Options.UseFont = true;
            this.RootLayout.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 8F);
            this.RootLayout.Appearance.ControlDisabled.Options.UseFont = true;
            this.RootLayout.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 8F);
            this.RootLayout.Appearance.ControlDropDown.Options.UseFont = true;
            this.RootLayout.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 8F);
            this.RootLayout.Appearance.ControlDropDownHeader.Options.UseFont = true;
            this.RootLayout.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 8F);
            this.RootLayout.Appearance.ControlFocused.Options.UseFont = true;
            this.RootLayout.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 8F);
            this.RootLayout.Appearance.ControlReadOnly.Options.UseFont = true;
            this.RootLayout.Controls.Add(this.btnGenerateSKU);
            this.RootLayout.Controls.Add(this.btnApply);
            this.RootLayout.Controls.Add(this.btnCancel);
            this.RootLayout.Controls.Add(this.txtCode);
            this.RootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RootLayout.Location = new System.Drawing.Point(0, 0);
            this.RootLayout.Name = "RootLayout";
            this.RootLayout.Root = this.Root;
            this.RootLayout.Size = new System.Drawing.Size(518, 104);
            this.RootLayout.TabIndex = 0;
            this.RootLayout.Text = "layoutControl1";
            // 
            // btnGenerateSKU
            // 
            this.btnGenerateSKU.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateSKU.ImageOptions.Image")));
            this.btnGenerateSKU.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGenerateSKU.Location = new System.Drawing.Point(136, 52);
            this.btnGenerateSKU.Name = "btnGenerateSKU";
            this.btnGenerateSKU.Size = new System.Drawing.Size(139, 36);
            this.btnGenerateSKU.StyleController = this.RootLayout;
            this.btnGenerateSKU.TabIndex = 9;
            this.btnGenerateSKU.Text = "Generate SKU Code";
            this.btnGenerateSKU.Click += new System.EventHandler(this.btnGenerateSKU_Click);
            // 
            // btnApply
            // 
            this.btnApply.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.ImageOptions.Image")));
            this.btnApply.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnApply.Location = new System.Drawing.Point(279, 52);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(111, 36);
            this.btnApply.StyleController = this.RootLayout;
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(394, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 36);
            this.btnCancel.StyleController = this.RootLayout;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(106, 20);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtCode.Size = new System.Drawing.Size(392, 20);
            this.txtCode.StyleController = this.RootLayout;
            this.txtCode.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "The value cannot be empty";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxAddSKUValidationProvider.SetValidationRule(this.txtCode, conditionValidationRule1);
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 8F);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCode,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(518, 104);
            this.Root.TextVisible = false;
            // 
            // lcCode
            // 
            this.lcCode.Control = this.txtCode;
            this.lcCode.Location = new System.Drawing.Point(0, 0);
            this.lcCode.Name = "lcCode";
            this.lcCode.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.lcCode.Size = new System.Drawing.Size(498, 40);
            this.lcCode.Text = "SKU/ITEM Code";
            this.lcCode.TextSize = new System.Drawing.Size(74, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(382, 40);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(116, 44);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnApply;
            this.layoutControlItem5.Location = new System.Drawing.Point(267, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(115, 44);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 40);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(124, 44);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnGenerateSKU;
            this.layoutControlItem6.Location = new System.Drawing.Point(124, 40);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(143, 44);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // frmAddCode
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(518, 104);
            this.Controls.Add(this.RootLayout);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmAddCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Code";
            this.Load += new System.EventHandler(this.frmAddCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RootLayout)).EndInit();
            this.RootLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxAddSKUValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl RootLayout;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnGenerateSKU;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlItem lcCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxAddSKUValidationProvider;
    }
}