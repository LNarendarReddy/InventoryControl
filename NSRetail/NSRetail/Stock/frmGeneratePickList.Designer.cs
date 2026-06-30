namespace NSRetail.Stock
{
    partial class frmGeneratePickList
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
            DevExpress.XtraPivotGrid.DataSourceColumnBinding dataSourceColumnBinding1 = new DevExpress.XtraPivotGrid.DataSourceColumnBinding();
            DevExpress.XtraPivotGrid.DataSourceColumnBinding dataSourceColumnBinding2 = new DevExpress.XtraPivotGrid.DataSourceColumnBinding();
            DevExpress.XtraPivotGrid.DataSourceColumnBinding dataSourceColumnBinding3 = new DevExpress.XtraPivotGrid.DataSourceColumnBinding();
            DevExpress.XtraPivotGrid.DataSourceColumnBinding dataSourceColumnBinding4 = new DevExpress.XtraPivotGrid.DataSourceColumnBinding();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.gcPickList = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.btnGeneratePickList = new DevExpress.XtraEditors.SimpleButton();
            this.luCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.luSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPickList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.luSupplier);
            this.layoutControl1.Controls.Add(this.btnPrint);
            this.layoutControl1.Controls.Add(this.gcPickList);
            this.layoutControl1.Controls.Add(this.btnGeneratePickList);
            this.layoutControl1.Controls.Add(this.luCategory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1287, 563);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(1184, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 22);
            this.btnPrint.StyleController = this.layoutControl1;
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // gcPickList
            // 
            this.gcPickList.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField1,
            this.pivotGridField2,
            this.pivotGridField3,
            this.pivotGridField4});
            this.gcPickList.Location = new System.Drawing.Point(12, 46);
            this.gcPickList.Name = "gcPickList";
            this.gcPickList.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.Optimized;
            this.gcPickList.OptionsPrint.PageSettings.Landscape = true;
            this.gcPickList.OptionsPrint.PageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.gcPickList.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.gcPickList.OptionsView.ShowDataHeaders = false;
            this.gcPickList.OptionsView.ShowFilterHeaders = false;
            this.gcPickList.Size = new System.Drawing.Size(1263, 505);
            this.gcPickList.TabIndex = 6;
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.Caption = "Item Name";
            dataSourceColumnBinding1.ColumnName = "ITEMNAME";
            this.pivotGridField1.DataBinding = dataSourceColumnBinding1;
            this.pivotGridField1.Name = "pivotGridField1";
            this.pivotGridField1.Options.AllowEdit = false;
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField2.AreaIndex = 1;
            this.pivotGridField2.Caption = "Item Code";
            dataSourceColumnBinding2.ColumnName = "ITEMCODE";
            this.pivotGridField2.DataBinding = dataSourceColumnBinding2;
            this.pivotGridField2.Name = "pivotGridField2";
            this.pivotGridField2.Options.AllowEdit = false;
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField3.AreaIndex = 0;
            this.pivotGridField3.Caption = "Branch Code";
            dataSourceColumnBinding3.ColumnName = "BRANCHCODE";
            this.pivotGridField3.DataBinding = dataSourceColumnBinding3;
            this.pivotGridField3.Name = "pivotGridField3";
            this.pivotGridField3.Options.AllowEdit = false;
            this.pivotGridField3.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.DisplayText;
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField4.AreaIndex = 0;
            this.pivotGridField4.CellFormat.FormatString = "d";
            this.pivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dataSourceColumnBinding4.ColumnName = "AVAILABLEQUANTITY";
            this.pivotGridField4.DataBinding = dataSourceColumnBinding4;
            this.pivotGridField4.Name = "pivotGridField4";
            this.pivotGridField4.Options.AllowEdit = false;
            this.pivotGridField4.ValueFormat.FormatString = "d";
            this.pivotGridField4.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // btnGeneratePickList
            // 
            this.btnGeneratePickList.Location = new System.Drawing.Point(1058, 16);
            this.btnGeneratePickList.Name = "btnGeneratePickList";
            this.btnGeneratePickList.Size = new System.Drawing.Size(114, 22);
            this.btnGeneratePickList.StyleController = this.layoutControl1;
            this.btnGeneratePickList.TabIndex = 5;
            this.btnGeneratePickList.Text = "Generate picklist";
            this.btnGeneratePickList.Click += new System.EventHandler(this.btnGeneratePickList_Click);
            // 
            // luCategory
            // 
            this.luCategory.Location = new System.Drawing.Point(77, 16);
            this.luCategory.Name = "luCategory";
            this.luCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYID", "CategoryID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYNAME", "Category")});
            this.luCategory.Properties.NullText = "";
            this.luCategory.Size = new System.Drawing.Size(244, 22);
            this.luCategory.StyleController = this.layoutControl1;
            this.luCategory.TabIndex = 4;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "The value is mandatory";
            this.dxValidationProvider1.SetValidationRule(this.luCategory, conditionValidationRule2);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1287, 563);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.luCategory;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(317, 34);
            this.layoutControlItem1.Text = "Category";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(49, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnGeneratePickList;
            this.layoutControlItem2.Location = new System.Drawing.Point(1042, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(126, 34);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(126, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(126, 34);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcPickList;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1267, 509);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(937, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(105, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnPrint;
            this.layoutControlItem4.Location = new System.Drawing.Point(1168, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(99, 34);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(99, 34);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(99, 34);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // luSupplier
            // 
            this.luSupplier.Location = new System.Drawing.Point(394, 16);
            this.luSupplier.Name = "luSupplier";
            this.luSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luSupplier.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERID", "DEALERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERNAME", "Supplier Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GSTIN", "Supplier GSTIN")});
            this.luSupplier.Properties.NullText = "";
            this.luSupplier.Size = new System.Drawing.Size(547, 22);
            this.luSupplier.StyleController = this.layoutControl1;
            this.luSupplier.TabIndex = 8;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "The value is mandatory";
            this.dxValidationProvider1.SetValidationRule(this.luSupplier, conditionValidationRule1);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.luSupplier;
            this.layoutControlItem5.Location = new System.Drawing.Point(317, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(620, 34);
            this.layoutControlItem5.Text = "Supplier";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(49, 15);
            // 
            // frmGeneratePickList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 563);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmGeneratePickList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Pick List";
            this.Load += new System.EventHandler(this.frmGeneratePickList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPickList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraPivotGrid.PivotGridControl gcPickList;
        private DevExpress.XtraEditors.SimpleButton btnGeneratePickList;
        private DevExpress.XtraEditors.LookUpEdit luCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LookUpEdit luSupplier;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}