namespace NSRetail.Supplier
{
    partial class frmGenerateSupplierIndent
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.luManufacturer = new DevExpress.XtraEditors.LookUpEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnGenerate = new DevExpress.XtraEditors.SimpleButton();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.luBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.txtSafetyDays = new DevExpress.XtraEditors.TextEdit();
            this.luIndentType = new DevExpress.XtraEditors.LookUpEdit();
            this.luSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.luCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafetyDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luIndentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.luManufacturer);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnGenerate);
            this.layoutControl1.Controls.Add(this.txtLog);
            this.layoutControl1.Controls.Add(this.luBranch);
            this.layoutControl1.Controls.Add(this.txtSafetyDays);
            this.layoutControl1.Controls.Add(this.luIndentType);
            this.layoutControl1.Controls.Add(this.luSupplier);
            this.layoutControl1.Controls.Add(this.luCategory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(713, 427);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // luManufacturer
            // 
            this.luManufacturer.Location = new System.Drawing.Point(106, 16);
            this.luManufacturer.Name = "luManufacturer";
            this.luManufacturer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luManufacturer.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MANUFACTURERID", "MANUFACTURERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MANUFACTURERNAME", "Manufacturer")});
            this.luManufacturer.Properties.NullText = "";
            this.luManufacturer.Size = new System.Drawing.Size(591, 22);
            this.luManufacturer.StyleController = this.layoutControl1;
            this.luManufacturer.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::NSRetail.Properties.Resources.cancel_16x164;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(535, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            // 
            // btnGenerate
            // 
            this.btnGenerate.ImageOptions.Image = global::NSRetail.Properties.Resources.play_16x161;
            this.btnGenerate.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGenerate.Location = new System.Drawing.Point(362, 389);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(161, 22);
            this.btnGenerate.StyleController = this.layoutControl1;
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtLog
            // 
            this.txtLog.EnterMoveNextControl = true;
            this.txtLog.Location = new System.Drawing.Point(16, 220);
            this.txtLog.Name = "txtLog";
            this.txtLog.Properties.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(681, 157);
            this.txtLog.StyleController = this.layoutControl1;
            this.txtLog.TabIndex = 8;
            // 
            // luBranch
            // 
            this.luBranch.EnterMoveNextControl = true;
            this.luBranch.Location = new System.Drawing.Point(106, 186);
            this.luBranch.Name = "luBranch";
            this.luBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHID", "BRANCHID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "Branch Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHCODE", "Branch Code")});
            this.luBranch.Properties.NullText = "";
            this.luBranch.Size = new System.Drawing.Size(591, 22);
            this.luBranch.StyleController = this.layoutControl1;
            this.luBranch.TabIndex = 7;
            // 
            // txtSafetyDays
            // 
            this.txtSafetyDays.EnterMoveNextControl = true;
            this.txtSafetyDays.Location = new System.Drawing.Point(106, 118);
            this.txtSafetyDays.Name = "txtSafetyDays";
            this.txtSafetyDays.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSafetyDays.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtSafetyDays.Properties.MaskSettings.Set("mask", "d");
            this.txtSafetyDays.Properties.UseMaskAsDisplayFormat = true;
            this.txtSafetyDays.Size = new System.Drawing.Size(591, 22);
            this.txtSafetyDays.StyleController = this.layoutControl1;
            this.txtSafetyDays.TabIndex = 6;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Value is mandatory";
            this.dxValidationProvider1.SetValidationRule(this.txtSafetyDays, conditionValidationRule1);
            // 
            // luIndentType
            // 
            this.luIndentType.EnterMoveNextControl = true;
            this.luIndentType.Location = new System.Drawing.Point(106, 152);
            this.luIndentType.Name = "luIndentType";
            this.luIndentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luIndentType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ENUMID", "ENUMID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ENUMVALUE", "Indent Type")});
            this.luIndentType.Properties.NullText = "";
            this.luIndentType.Size = new System.Drawing.Size(591, 22);
            this.luIndentType.StyleController = this.layoutControl1;
            this.luIndentType.TabIndex = 5;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Value is mandatory";
            this.dxValidationProvider1.SetValidationRule(this.luIndentType, conditionValidationRule2);
            this.luIndentType.EditValueChanged += new System.EventHandler(this.luIndentType_EditValueChanged);
            // 
            // luSupplier
            // 
            this.luSupplier.EnterMoveNextControl = true;
            this.luSupplier.Location = new System.Drawing.Point(106, 50);
            this.luSupplier.Name = "luSupplier";
            this.luSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luSupplier.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERID", "DEALERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERNAME", "Supplier"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GSTIN", "GSTIN")});
            this.luSupplier.Properties.NullText = "";
            this.luSupplier.Size = new System.Drawing.Size(591, 22);
            this.luSupplier.StyleController = this.layoutControl1;
            this.luSupplier.TabIndex = 4;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "Value is mandatory";
            this.dxValidationProvider1.SetValidationRule(this.luSupplier, conditionValidationRule3);
            // 
            // luCategory
            // 
            this.luCategory.EnterMoveNextControl = true;
            this.luCategory.Location = new System.Drawing.Point(106, 84);
            this.luCategory.Name = "luCategory";
            this.luCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYID", "CATEGORYID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYNAME", "Category")});
            this.luCategory.Properties.NullText = "";
            this.luCategory.Size = new System.Drawing.Size(591, 22);
            this.luCategory.StyleController = this.layoutControl1;
            this.luCategory.TabIndex = 4;
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "Value is mandatory";
            this.dxValidationProvider1.SetValidationRule(this.luCategory, conditionValidationRule4);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem3,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(713, 427);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.luSupplier;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(693, 34);
            this.layoutControlItem1.Text = "Supplier";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(78, 15);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 373);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(346, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.luIndentType;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 136);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(693, 34);
            this.layoutControlItem2.Text = "Item Selection";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(78, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.luBranch;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 170);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(693, 34);
            this.layoutControlItem4.Text = "Branch";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(78, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtLog;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 204);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(693, 169);
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnGenerate;
            this.layoutControlItem6.Location = new System.Drawing.Point(346, 373);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(173, 34);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnCancel;
            this.layoutControlItem7.Location = new System.Drawing.Point(519, 373);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(174, 34);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtSafetyDays;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 102);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(693, 34);
            this.layoutControlItem3.Text = "Safety days";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(78, 15);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.luCategory;
            this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem8.CustomizationFormText = "Supplier";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem8.Size = new System.Drawing.Size(693, 34);
            this.layoutControlItem8.Text = "Category";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(78, 15);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.luManufacturer;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem9.Size = new System.Drawing.Size(693, 34);
            this.layoutControlItem9.Text = "Manufacturer";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(78, 15);
            // 
            // frmGenerateSupplierIndent
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(713, 427);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmGenerateSupplierIndent";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Supplier Indent";
            this.Load += new System.EventHandler(this.frmGenerateSupplierIndent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.luManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafetyDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luIndentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnGenerate;
        private DevExpress.XtraEditors.MemoEdit txtLog;
        private DevExpress.XtraEditors.LookUpEdit luBranch;
        private DevExpress.XtraEditors.TextEdit txtSafetyDays;
        private DevExpress.XtraEditors.LookUpEdit luIndentType;
        private DevExpress.XtraEditors.LookUpEdit luSupplier;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.LookUpEdit luCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.LookUpEdit luManufacturer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}