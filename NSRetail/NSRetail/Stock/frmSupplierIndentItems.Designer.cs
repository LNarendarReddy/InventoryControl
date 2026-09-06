namespace NSRetail.Stock
{
    partial class frmSupplierIndentItems
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
            this.gcIndentItems = new DevExpress.XtraGrid.GridControl();
            this.gvIndentItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSupplierIndentDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSKUCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSubCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMRP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCostPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnBranchQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRequiredBranchStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRequiredItemIndent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDesiredIndent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnIndentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVendorSKUCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEnteredQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcIndentItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.gcIndentItems);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1051, 549);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(959, 515);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gcIndentItems
            // 
            this.gcIndentItems.Location = new System.Drawing.Point(12, 12);
            this.gcIndentItems.MainView = this.gvIndentItems;
            this.gcIndentItems.Name = "gcIndentItems";
            this.gcIndentItems.Size = new System.Drawing.Size(1027, 499);
            this.gcIndentItems.TabIndex = 0;
            this.gcIndentItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIndentItems});
            // 
            // gvIndentItems
            // 
            this.gvIndentItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSNO,
            this.gridColumnSupplierIndentDetailID,
            this.gridColumnItemID,
            this.gridColumnSKUCode,
            this.gridColumnItemName,
            this.gridColumnSubCategory,
            this.gridColumnMRP,
            this.gridColumnCostPrice,
            this.gridColumnBranchQuantity,
            this.gridColumnRequiredBranchStock,
            this.gridColumnRequiredItemIndent,
            this.gridColumnDesiredIndent,
            this.gridColumnIndentType,
            this.gridColumnVendorSKUCode,
            this.gridColumnEnteredQuantity,
            this.gridColumnStatus});
            this.gvIndentItems.GridControl = this.gcIndentItems;
            this.gvIndentItems.Name = "gvIndentItems";
            this.gvIndentItems.OptionsBehavior.Editable = false;
            this.gvIndentItems.OptionsView.ShowFooter = true;
            this.gvIndentItems.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnSNO
            // 
            this.gridColumnSNO.Caption = "S.No";
            this.gridColumnSNO.FieldName = "SNO";
            this.gridColumnSNO.Name = "gridColumnSNO";
            this.gridColumnSNO.Visible = true;
            this.gridColumnSNO.VisibleIndex = 0;
            this.gridColumnSNO.Width = 45;
            // 
            // gridColumnSupplierIndentDetailID
            // 
            this.gridColumnSupplierIndentDetailID.Caption = "Supplier Indent Detail ID";
            this.gridColumnSupplierIndentDetailID.FieldName = "SUPPLIERINDENTDETAILID";
            this.gridColumnSupplierIndentDetailID.Name = "gridColumnSupplierIndentDetailID";
            // 
            // gridColumnItemID
            // 
            this.gridColumnItemID.Caption = "Item ID";
            this.gridColumnItemID.FieldName = "ITEMID";
            this.gridColumnItemID.Name = "gridColumnItemID";
            // 
            // gridColumnSKUCode
            // 
            this.gridColumnSKUCode.Caption = "SKU Code";
            this.gridColumnSKUCode.FieldName = "SKUCODE";
            this.gridColumnSKUCode.Name = "gridColumnSKUCode";
            this.gridColumnSKUCode.Visible = true;
            this.gridColumnSKUCode.VisibleIndex = 1;
            this.gridColumnSKUCode.Width = 90;
            // 
            // gridColumnItemName
            // 
            this.gridColumnItemName.Caption = "Item Name";
            this.gridColumnItemName.FieldName = "ITEMNAME";
            this.gridColumnItemName.Name = "gridColumnItemName";
            this.gridColumnItemName.Visible = true;
            this.gridColumnItemName.VisibleIndex = 2;
            this.gridColumnItemName.Width = 230;
            // 
            // gridColumnSubCategory
            // 
            this.gridColumnSubCategory.Caption = "Sub Category";
            this.gridColumnSubCategory.FieldName = "SUBCATEGORYNAME";
            this.gridColumnSubCategory.Name = "gridColumnSubCategory";
            this.gridColumnSubCategory.Visible = true;
            this.gridColumnSubCategory.VisibleIndex = 3;
            this.gridColumnSubCategory.Width = 120;
            // 
            // gridColumnMRP
            // 
            this.gridColumnMRP.Caption = "MRP";
            this.gridColumnMRP.DisplayFormat.FormatString = "0.00";
            this.gridColumnMRP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnMRP.FieldName = "MRP";
            this.gridColumnMRP.Name = "gridColumnMRP";
            this.gridColumnMRP.Visible = true;
            this.gridColumnMRP.VisibleIndex = 4;
            this.gridColumnMRP.Width = 70;
            // 
            // gridColumnCostPrice
            // 
            this.gridColumnCostPrice.Caption = "Cost Price WT";
            this.gridColumnCostPrice.DisplayFormat.FormatString = "0.00";
            this.gridColumnCostPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnCostPrice.FieldName = "COSTPRICEWT";
            this.gridColumnCostPrice.Name = "gridColumnCostPrice";
            this.gridColumnCostPrice.Visible = true;
            this.gridColumnCostPrice.VisibleIndex = 5;
            this.gridColumnCostPrice.Width = 90;
            // 
            // gridColumnBranchQuantity
            // 
            this.gridColumnBranchQuantity.Caption = "Branch Quantity";
            this.gridColumnBranchQuantity.FieldName = "BRANCHQUANTITY";
            this.gridColumnBranchQuantity.Name = "gridColumnBranchQuantity";
            // 
            // gridColumnRequiredBranchStock
            // 
            this.gridColumnRequiredBranchStock.Caption = "Required Branch Stock";
            this.gridColumnRequiredBranchStock.FieldName = "REQUIREDBRANCHSTOCK";
            this.gridColumnRequiredBranchStock.Name = "gridColumnRequiredBranchStock";
            // 
            // gridColumnRequiredItemIndent
            // 
            this.gridColumnRequiredItemIndent.Caption = "Required Item Indent";
            this.gridColumnRequiredItemIndent.FieldName = "REQUIREDITEMINDENT";
            this.gridColumnRequiredItemIndent.Name = "gridColumnRequiredItemIndent";
            // 
            // gridColumnDesiredIndent
            // 
            this.gridColumnDesiredIndent.Caption = "Indent Quantity";
            this.gridColumnDesiredIndent.DisplayFormat.FormatString = "0.##";
            this.gridColumnDesiredIndent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnDesiredIndent.FieldName = "DESIREDINDENT";
            this.gridColumnDesiredIndent.Name = "gridColumnDesiredIndent";
            this.gridColumnDesiredIndent.Visible = true;
            this.gridColumnDesiredIndent.VisibleIndex = 6;
            this.gridColumnDesiredIndent.Width = 95;
            // 
            // gridColumnIndentType
            // 
            this.gridColumnIndentType.Caption = "Indent Type";
            this.gridColumnIndentType.FieldName = "INDENTTYPE";
            this.gridColumnIndentType.Name = "gridColumnIndentType";
            // 
            // gridColumnVendorSKUCode
            // 
            this.gridColumnVendorSKUCode.Caption = "Vendor SKU Code";
            this.gridColumnVendorSKUCode.FieldName = "VENDORSKUCODE";
            this.gridColumnVendorSKUCode.Name = "gridColumnVendorSKUCode";
            // 
            // gridColumnEnteredQuantity
            // 
            this.gridColumnEnteredQuantity.Caption = "Invoice Quantity";
            this.gridColumnEnteredQuantity.DisplayFormat.FormatString = "0.##";
            this.gridColumnEnteredQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnEnteredQuantity.FieldName = "ENTEREDQUANTITY";
            this.gridColumnEnteredQuantity.Name = "gridColumnEnteredQuantity";
            this.gridColumnEnteredQuantity.Visible = true;
            this.gridColumnEnteredQuantity.VisibleIndex = 7;
            this.gridColumnEnteredQuantity.Width = 95;
            // 
            // gridColumnStatus
            // 
            this.gridColumnStatus.Caption = "Status";
            this.gridColumnStatus.FieldName = "STATUS";
            this.gridColumnStatus.Name = "gridColumnStatus";
            this.gridColumnStatus.Visible = true;
            this.gridColumnStatus.VisibleIndex = 8;
            this.gridColumnStatus.Width = 90;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1051, 549);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcIndentItems;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1031, 503);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 503);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(947, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            this.layoutControlItem2.Location = new System.Drawing.Point(947, 503);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(84, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(84, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(84, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmSupplierIndentItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1051, 549);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmSupplierIndentItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier Indent Items";
            this.Load += new System.EventHandler(this.frmSupplierIndentItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcIndentItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcIndentItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIndentItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSNO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSupplierIndentDetailID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSKUCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSubCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMRP;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCostPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBranchQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRequiredBranchStock;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRequiredItemIndent;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDesiredIndent;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndentType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVendorSKUCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEnteredQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
