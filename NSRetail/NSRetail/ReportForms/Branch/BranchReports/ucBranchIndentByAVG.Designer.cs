namespace NSRetail.ReportForms.Branch.BranchReports
{
    partial class ucBranchIndentByAVG
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBranchIndentByAVG));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.luManfacturer = new DevExpress.XtraEditors.LookUpEdit();
            this.chkIsDSD = new DevExpress.XtraEditors.CheckEdit();
            this.txtIndentDays = new DevExpress.XtraEditors.TextEdit();
            this.cmbCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSaveIndent = new DevExpress.XtraEditors.SimpleButton();
            this.btnGenerateSupplierIndent = new DevExpress.XtraEditors.SimpleButton();
            this.cmbDealer = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luManfacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDSD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndentDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDealer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.luManfacturer);
            this.layoutControl1.Controls.Add(this.chkIsDSD);
            this.layoutControl1.Controls.Add(this.txtIndentDays);
            this.layoutControl1.Controls.Add(this.cmbCategory);
            this.layoutControl1.Controls.Add(this.cmbBranch);
            this.layoutControl1.Controls.Add(this.btnSaveIndent);
            this.layoutControl1.Controls.Add(this.btnGenerateSupplierIndent);
            this.layoutControl1.Controls.Add(this.cmbDealer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1028, 81);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // luManfacturer
            // 
            this.luManfacturer.Location = new System.Drawing.Point(173, 46);
            this.luManfacturer.Name = "luManfacturer";
            this.luManfacturer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luManfacturer.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MANUFACTURERID", "MANUFACTURERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MANUFACTURERNAME", "Manufacturer Name")});
            this.luManfacturer.Properties.NullText = "";
            this.luManfacturer.Size = new System.Drawing.Size(284, 22);
            this.luManfacturer.StyleController = this.layoutControl1;
            this.luManfacturer.TabIndex = 6;
            this.luManfacturer.EditValueChanged += new System.EventHandler(this.luManfacturer_EditValueChanged);
            // 
            // chkIsDSD
            // 
            this.chkIsDSD.Location = new System.Drawing.Point(11, 46);
            this.chkIsDSD.Name = "chkIsDSD";
            this.chkIsDSD.Properties.Caption = "Is DSD";
            this.chkIsDSD.Size = new System.Drawing.Size(66, 19);
            this.chkIsDSD.StyleController = this.layoutControl1;
            this.chkIsDSD.TabIndex = 5;
            this.chkIsDSD.CheckedChanged += new System.EventHandler(this.chkIsDSD_CheckedChanged);
            // 
            // txtIndentDays
            // 
            this.txtIndentDays.Location = new System.Drawing.Point(553, 11);
            this.txtIndentDays.Name = "txtIndentDays";
            this.txtIndentDays.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtIndentDays.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtIndentDays.Properties.MaskSettings.Set("mask", "d");
            this.txtIndentDays.Properties.UseMaskAsDisplayFormat = true;
            this.txtIndentDays.Size = new System.Drawing.Size(50, 22);
            this.txtIndentDays.StyleController = this.layoutControl1;
            this.txtIndentDays.TabIndex = 4;
            // 
            // cmbCategory
            // 
            this.cmbCategory.EnterMoveNextControl = true;
            this.cmbCategory.Location = new System.Drawing.Point(324, 11);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYID", "CATEGORYID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYNAME", "Category Name")});
            this.cmbCategory.Properties.NullText = "";
            this.cmbCategory.Size = new System.Drawing.Size(133, 22);
            this.cmbCategory.StyleController = this.layoutControl1;
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.EditValueChanged += new System.EventHandler(this.cmbCategory_EditValueChanged);
            // 
            // cmbBranch
            // 
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(95, 11);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHID", "BRANCHID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "Branch Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHCODE", "Branch Code", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cmbBranch.Properties.NullText = "";
            this.cmbBranch.Size = new System.Drawing.Size(133, 22);
            this.cmbBranch.StyleController = this.layoutControl1;
            this.cmbBranch.TabIndex = 0;
            this.cmbBranch.EditValueChanged += new System.EventHandler(this.cmbBranch_EditValueChanged);
            // 
            // btnSaveIndent
            // 
            this.btnSaveIndent.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveIndent.ImageOptions.Image")));
            this.btnSaveIndent.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSaveIndent.Location = new System.Drawing.Point(615, 11);
            this.btnSaveIndent.Name = "btnSaveIndent";
            this.btnSaveIndent.Size = new System.Drawing.Size(103, 23);
            this.btnSaveIndent.StyleController = this.layoutControl1;
            this.btnSaveIndent.TabIndex = 4;
            this.btnSaveIndent.Text = "Save Indent";
            this.btnSaveIndent.Click += new System.EventHandler(this.btnSaveIndent_Click);
            // 
            // btnGenerateSupplierIndent
            // 
            this.btnGenerateSupplierIndent.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateSupplierIndent.ImageOptions.Image")));
            this.btnGenerateSupplierIndent.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGenerateSupplierIndent.Location = new System.Drawing.Point(829, 46);
            this.btnGenerateSupplierIndent.Name = "btnGenerateSupplierIndent";
            this.btnGenerateSupplierIndent.Size = new System.Drawing.Size(188, 24);
            this.btnGenerateSupplierIndent.StyleController = this.layoutControl1;
            this.btnGenerateSupplierIndent.TabIndex = 4;
            this.btnGenerateSupplierIndent.Tag = "51AE564E-80C0-4739-AED1-50B688D5A76F::Execute";
            this.btnGenerateSupplierIndent.Text = "Generate Supplier Indent";
            this.btnGenerateSupplierIndent.Click += new System.EventHandler(this.btnGenerateSupplierIndent_Click);
            // 
            // cmbDealer
            // 
            this.cmbDealer.EnterMoveNextControl = true;
            this.cmbDealer.Location = new System.Drawing.Point(553, 46);
            this.cmbDealer.Name = "cmbDealer";
            this.cmbDealer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDealer.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERID", "DEALERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERNAME", "Dealer Name")});
            this.cmbDealer.Properties.NullText = "";
            this.cmbDealer.Size = new System.Drawing.Size(264, 22);
            this.cmbDealer.StyleController = this.layoutControl1;
            this.cmbDealer.TabIndex = 0;
            this.cmbDealer.EditValueChanged += new System.EventHandler(this.cmbDealer_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1028, 81);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbBranch;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(229, 35);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(229, 35);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(229, 35);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Branch";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbCategory;
            this.layoutControlItem2.Location = new System.Drawing.Point(229, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(229, 35);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(229, 35);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(229, 35);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Category";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtIndentDays;
            this.layoutControlItem3.Location = new System.Drawing.Point(458, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(146, 35);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(146, 35);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(146, 35);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "No of Days";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSaveIndent;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(604, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(115, 35);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(115, 35);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(414, 35);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkIsDSD;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(78, 36);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(78, 36);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(78, 36);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.luManfacturer;
            this.layoutControlItem6.Location = new System.Drawing.Point(78, 35);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(380, 36);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(380, 36);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(380, 36);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "Manufacturer";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 15);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnGenerateSupplierIndent;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem7.Location = new System.Drawing.Point(818, 35);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(200, 36);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(200, 36);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(200, 36);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem5";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cmbDealer;
            this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem8.CustomizationFormText = "Dealer";
            this.layoutControlItem8.Location = new System.Drawing.Point(458, 35);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(360, 36);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(360, 36);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem8.Size = new System.Drawing.Size(360, 36);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "Dealer";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(72, 15);
            // 
            // ucBranchIndentByAVG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucBranchIndentByAVG";
            this.Size = new System.Drawing.Size(1028, 81);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.luManfacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDSD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndentDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDealer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtIndentDays;
        private DevExpress.XtraEditors.LookUpEdit cmbCategory;
        private DevExpress.XtraEditors.LookUpEdit cmbBranch;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnSaveIndent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LookUpEdit luManfacturer;
        private DevExpress.XtraEditors.CheckEdit chkIsDSD;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnGenerateSupplierIndent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.LookUpEdit cmbDealer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}
