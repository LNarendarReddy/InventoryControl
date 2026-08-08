namespace NSRetail
{
    partial class frmItemHistory
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewReport = new DevExpress.XtraEditors.SimpleButton();
            this.gcItemHistory = new DevExpress.XtraGrid.GridControl();
            this.gvItemHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcActionDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcActionBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcChangedColumns = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSKUCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSubCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcParentSKUCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcParentItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUOM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcClassificationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSubClassificationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBrandName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcManufacturerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUQCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHSNCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGSTCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcVendorSKUCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSeasonalityIDs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRefundPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSupplierIndentItemType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnViewReport);
            this.layoutControl1.Controls.Add(this.gcItemHistory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1100, 600);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(983, 562);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(853, 562);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(118, 22);
            this.btnViewReport.StyleController = this.layoutControl1;
            this.btnViewReport.TabIndex = 5;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // gcItemHistory
            // 
            this.gcItemHistory.Location = new System.Drawing.Point(12, 12);
            this.gcItemHistory.MainView = this.gvItemHistory;
            this.gcItemHistory.Name = "gcItemHistory";
            this.gcItemHistory.Size = new System.Drawing.Size(1076, 542);
            this.gcItemHistory.TabIndex = 4;
            this.gcItemHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemHistory});
            // 
            // gvItemHistory
            // 
            this.gvItemHistory.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gvItemHistory.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvItemHistory.Appearance.Row.Font = new System.Drawing.Font("Arial", 8F);
            this.gvItemHistory.Appearance.Row.Options.UseFont = true;
            this.gvItemHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcAction,
            this.gcActionDate,
            this.gcActionBy,
            this.gcChangedColumns,
            this.gcSKUCode,
            this.gcItemName,
            this.gcDescription,
            this.gcCategoryName,
            this.gcSubCategoryName,
            this.gcParentSKUCode,
            this.gcParentItemName,
            this.gcUOM,
            this.gcClassificationName,
            this.gcSubClassificationName,
            this.gcBrandName,
            this.gcManufacturerName,
            this.gcUQCName,
            this.gcHSNCode,
            this.gcGSTCode,
            this.gcVendorSKUCode,
            this.gcSeasonalityIDs,
            this.gcRefundPath,
            this.gcSupplierIndentItemType});
            this.gvItemHistory.GridControl = this.gcItemHistory;
            this.gvItemHistory.Name = "gvItemHistory";
            this.gvItemHistory.OptionsBehavior.Editable = false;
            this.gvItemHistory.OptionsCustomization.AllowFilter = false;
            this.gvItemHistory.OptionsCustomization.AllowSort = false;
            this.gvItemHistory.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.gvItemHistory.OptionsView.ShowFooter = true;
            this.gvItemHistory.OptionsView.ShowGroupPanel = false;
            // 
            // gcAction
            // 
            this.gcAction.Caption = "Action";
            this.gcAction.FieldName = "Action";
            this.gcAction.Name = "gcAction";
            this.gcAction.Visible = true;
            this.gcAction.VisibleIndex = 2;
            // 
            // gcActionDate
            // 
            this.gcActionDate.Caption = "Action Date";
            this.gcActionDate.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt";
            this.gcActionDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcActionDate.FieldName = "Action Date";
            this.gcActionDate.Name = "gcActionDate";
            this.gcActionDate.Visible = true;
            this.gcActionDate.VisibleIndex = 3;
            // 
            // gcActionBy
            // 
            this.gcActionBy.Caption = "Action By";
            this.gcActionBy.FieldName = "Action By";
            this.gcActionBy.Name = "gcActionBy";
            this.gcActionBy.Visible = true;
            this.gcActionBy.VisibleIndex = 4;
            // 
            // gcChangedColumns
            // 
            this.gcChangedColumns.Caption = "Changed Columns";
            this.gcChangedColumns.FieldName = "Changed Columns";
            this.gcChangedColumns.Name = "gcChangedColumns";
            this.gcChangedColumns.Visible = true;
            this.gcChangedColumns.VisibleIndex = 5;
            // 
            // gcSKUCode
            // 
            this.gcSKUCode.Caption = "SKU Code";
            this.gcSKUCode.FieldName = "SKUCODE";
            this.gcSKUCode.Name = "gcSKUCode";
            this.gcSKUCode.Visible = true;
            this.gcSKUCode.VisibleIndex = 0;
            // 
            // gcItemName
            // 
            this.gcItemName.Caption = "Item Name";
            this.gcItemName.FieldName = "ITEMNAME";
            this.gcItemName.Name = "gcItemName";
            this.gcItemName.Visible = true;
            this.gcItemName.VisibleIndex = 1;
            // 
            // gcDescription
            // 
            this.gcDescription.Caption = "Description";
            this.gcDescription.FieldName = "DESCRIPTION";
            this.gcDescription.Name = "gcDescription";
            // 
            // gcCategoryName
            // 
            this.gcCategoryName.Caption = "Category";
            this.gcCategoryName.FieldName = "CATEGORYNAME";
            this.gcCategoryName.Name = "gcCategoryName";
            // 
            // gcSubCategoryName
            // 
            this.gcSubCategoryName.Caption = "Sub Category";
            this.gcSubCategoryName.FieldName = "SUBCATEGORYNAME";
            this.gcSubCategoryName.Name = "gcSubCategoryName";
            // 
            // gcParentSKUCode
            // 
            this.gcParentSKUCode.Caption = "Parent SKU Code";
            this.gcParentSKUCode.FieldName = "PARENTSKUCODE";
            this.gcParentSKUCode.Name = "gcParentSKUCode";
            // 
            // gcParentItemName
            // 
            this.gcParentItemName.Caption = "Parent Item Name";
            this.gcParentItemName.FieldName = "PARENTITEMNAME";
            this.gcParentItemName.Name = "gcParentItemName";
            // 
            // gcUOM
            // 
            this.gcUOM.Caption = "UOM";
            this.gcUOM.FieldName = "UOM";
            this.gcUOM.Name = "gcUOM";
            // 
            // gcClassificationName
            // 
            this.gcClassificationName.Caption = "Classification";
            this.gcClassificationName.FieldName = "CLASSIFICATIONNAME";
            this.gcClassificationName.Name = "gcClassificationName";
            // 
            // gcSubClassificationName
            // 
            this.gcSubClassificationName.Caption = "Sub Classification";
            this.gcSubClassificationName.FieldName = "SUBCLASSIFICATIONNAME";
            this.gcSubClassificationName.Name = "gcSubClassificationName";
            // 
            // gcBrandName
            // 
            this.gcBrandName.Caption = "Brand";
            this.gcBrandName.FieldName = "BRANDNAME";
            this.gcBrandName.Name = "gcBrandName";
            // 
            // gcManufacturerName
            // 
            this.gcManufacturerName.Caption = "Manufacturer";
            this.gcManufacturerName.FieldName = "MANUFACTURERNAME";
            this.gcManufacturerName.Name = "gcManufacturerName";
            // 
            // gcUQCName
            // 
            this.gcUQCName.Caption = "UQC";
            this.gcUQCName.FieldName = "UQCNAME";
            this.gcUQCName.Name = "gcUQCName";
            // 
            // gcHSNCode
            // 
            this.gcHSNCode.Caption = "HSN Code";
            this.gcHSNCode.FieldName = "HSNCODE";
            this.gcHSNCode.Name = "gcHSNCode";
            // 
            // gcGSTCode
            // 
            this.gcGSTCode.Caption = "GST Code";
            this.gcGSTCode.FieldName = "GSTCODE";
            this.gcGSTCode.Name = "gcGSTCode";
            // 
            // gcVendorSKUCode
            // 
            this.gcVendorSKUCode.Caption = "Vendor SKU Code";
            this.gcVendorSKUCode.FieldName = "VENDORSKUCODE";
            this.gcVendorSKUCode.Name = "gcVendorSKUCode";
            // 
            // gcSeasonalityIDs
            // 
            this.gcSeasonalityIDs.Caption = "Seasonality";
            this.gcSeasonalityIDs.FieldName = "SEASONALITYIDS";
            this.gcSeasonalityIDs.Name = "gcSeasonalityIDs";
            // 
            // gcRefundPath
            // 
            this.gcRefundPath.Caption = "Refund Path";
            this.gcRefundPath.FieldName = "REFUNDPATHTEXT";
            this.gcRefundPath.Name = "gcRefundPath";
            // 
            // gcSupplierIndentItemType
            // 
            this.gcSupplierIndentItemType.Caption = "Supplier Indent Item Type";
            this.gcSupplierIndentItemType.FieldName = "SUPPLIERINDENTITEMTYPE";
            this.gcSupplierIndentItemType.Name = "gcSupplierIndentItemType";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1100, 600);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItemHistory;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1080, 546);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 546);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(837, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnViewReport;
            this.layoutControlItem2.Location = new System.Drawing.Point(837, 546);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(130, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            this.layoutControlItem3.Location = new System.Drawing.Point(967, 546);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(113, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmItemHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmItemHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item History";
            this.Load += new System.EventHandler(this.frmItemHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItemHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcItemHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemHistory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnViewReport;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gcAction;
        private DevExpress.XtraGrid.Columns.GridColumn gcActionDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcActionBy;
        private DevExpress.XtraGrid.Columns.GridColumn gcChangedColumns;
        private DevExpress.XtraGrid.Columns.GridColumn gcSKUCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gcDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gcCategoryName;
        private DevExpress.XtraGrid.Columns.GridColumn gcSubCategoryName;
        private DevExpress.XtraGrid.Columns.GridColumn gcParentSKUCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcParentItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gcUOM;
        private DevExpress.XtraGrid.Columns.GridColumn gcClassificationName;
        private DevExpress.XtraGrid.Columns.GridColumn gcSubClassificationName;
        private DevExpress.XtraGrid.Columns.GridColumn gcBrandName;
        private DevExpress.XtraGrid.Columns.GridColumn gcManufacturerName;
        private DevExpress.XtraGrid.Columns.GridColumn gcUQCName;
        private DevExpress.XtraGrid.Columns.GridColumn gcHSNCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcGSTCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcVendorSKUCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcSeasonalityIDs;
        private DevExpress.XtraGrid.Columns.GridColumn gcRefundPath;
        private DevExpress.XtraGrid.Columns.GridColumn gcSupplierIndentItemType;
    }
}
