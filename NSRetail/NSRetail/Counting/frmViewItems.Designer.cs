namespace NSRetail
{
    partial class frmViewItems
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnViewReport = new DevExpress.XtraEditors.SimpleButton();
            this.gcItems = new DevExpress.XtraGrid.GridControl();
            this.gvItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcStockCountingDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemPriceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemCodeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSubCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMRP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPhysicalStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSystemStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStockDiff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnViewReport);
            this.layoutControl1.Controls.Add(this.gcItems);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1140, 692);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(956, 8);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(176, 22);
            this.btnViewReport.StyleController = this.layoutControl1;
            this.btnViewReport.TabIndex = 5;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // gcItems
            // 
            this.gcItems.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcItems.Location = new System.Drawing.Point(4, 38);
            this.gcItems.MainView = this.gvItems;
            this.gcItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcItems.Name = "gcItems";
            this.gcItems.Size = new System.Drawing.Size(1132, 650);
            this.gcItems.TabIndex = 4;
            this.gcItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItems});
            // 
            // gvItems
            // 
            this.gvItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcStockCountingDetailID,
            this.gcItemPriceId,
            this.gcItemCodeID,
            this.gcItemCode,
            this.gcItemName,
            this.gcCategory,
            this.gcSubCategory,
            this.gcMRP,
            this.gcSalePrice,
            this.gcQuantity,
            this.gcPhysicalStock,
            this.gcSystemStock,
            this.gcStockDiff});
            this.gvItems.DetailHeight = 404;
            this.gvItems.GridControl = this.gcItems;
            this.gvItems.Name = "gvItems";
            this.gvItems.OptionsBehavior.Editable = false;
            this.gvItems.OptionsView.ShowFooter = true;
            this.gvItems.OptionsView.ShowGroupPanel = false;
            // 
            // gcStockCountingDetailID
            // 
            this.gcStockCountingDetailID.Caption = "STOCKCOUNTINGDETAILID";
            this.gcStockCountingDetailID.FieldName = "STOCKCOUNTINGDETAILID";
            this.gcStockCountingDetailID.Name = "gcStockCountingDetailID";
            this.gcStockCountingDetailID.Width = 87;
            // 
            // gcItemPriceId
            // 
            this.gcItemPriceId.Caption = "ITEMPRICEID";
            this.gcItemPriceId.FieldName = "ITEMPRICEID";
            this.gcItemPriceId.Name = "gcItemPriceId";
            this.gcItemPriceId.Width = 87;
            // 
            // gcItemCodeID
            // 
            this.gcItemCodeID.Caption = "ITEMCODEID";
            this.gcItemCodeID.FieldName = "ITEMCODEID";
            this.gcItemCodeID.Name = "gcItemCodeID";
            // 
            // gcItemCode
            // 
            this.gcItemCode.Caption = "ITEMCODE";
            this.gcItemCode.FieldName = "ITEMCODE";
            this.gcItemCode.Name = "gcItemCode";
            this.gcItemCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ITEMCODE", "{0}")});
            this.gcItemCode.Visible = true;
            this.gcItemCode.VisibleIndex = 0;
            this.gcItemCode.Width = 122;
            // 
            // gcItemName
            // 
            this.gcItemName.Caption = "ITEMNAME";
            this.gcItemName.FieldName = "ITEMNAME";
            this.gcItemName.Name = "gcItemName";
            this.gcItemName.Visible = true;
            this.gcItemName.VisibleIndex = 1;
            this.gcItemName.Width = 385;
            // 
            // gcCategory
            // 
            this.gcCategory.Caption = "Category";
            this.gcCategory.FieldName = "CATEGORYNAME";
            this.gcCategory.Name = "gcCategory";
            this.gcCategory.Visible = true;
            this.gcCategory.VisibleIndex = 4;
            // 
            // gcSubCategory
            // 
            this.gcSubCategory.Caption = "Sub Category";
            this.gcSubCategory.FieldName = "SUBCATEGORYNAME";
            this.gcSubCategory.Name = "gcSubCategory";
            this.gcSubCategory.Visible = true;
            this.gcSubCategory.VisibleIndex = 5;
            // 
            // gcMRP
            // 
            this.gcMRP.Caption = "MRP";
            this.gcMRP.FieldName = "MRP";
            this.gcMRP.Name = "gcMRP";
            this.gcMRP.Visible = true;
            this.gcMRP.VisibleIndex = 2;
            this.gcMRP.Width = 87;
            // 
            // gcSalePrice
            // 
            this.gcSalePrice.Caption = "SALEPRICE";
            this.gcSalePrice.FieldName = "SALEPRICE";
            this.gcSalePrice.Name = "gcSalePrice";
            this.gcSalePrice.Visible = true;
            this.gcSalePrice.VisibleIndex = 3;
            this.gcSalePrice.Width = 87;
            // 
            // gcQuantity
            // 
            this.gcQuantity.Caption = "QUANTITY";
            this.gcQuantity.FieldName = "QUANTITY";
            this.gcQuantity.Name = "gcQuantity";
            this.gcQuantity.Visible = true;
            this.gcQuantity.VisibleIndex = 6;
            this.gcQuantity.Width = 87;
            // 
            // gcPhysicalStock
            // 
            this.gcPhysicalStock.Caption = "Physical stock";
            this.gcPhysicalStock.FieldName = "PHYSICALSTOCK";
            this.gcPhysicalStock.Name = "gcPhysicalStock";
            this.gcPhysicalStock.Visible = true;
            this.gcPhysicalStock.VisibleIndex = 7;
            // 
            // gcSystemStock
            // 
            this.gcSystemStock.Caption = "System Stock";
            this.gcSystemStock.FieldName = "SYSTEMSTOCK";
            this.gcSystemStock.Name = "gcSystemStock";
            this.gcSystemStock.Visible = true;
            this.gcSystemStock.VisibleIndex = 8;
            // 
            // gcStockDiff
            // 
            this.gcStockDiff.Caption = "Stock Diff";
            this.gcStockDiff.FieldName = "STOCKDIFF";
            this.gcStockDiff.Name = "gcStockDiff";
            this.gcStockDiff.Visible = true;
            this.gcStockDiff.VisibleIndex = 9;
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
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1140, 692);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItems;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1136, 654);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(948, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnViewReport;
            this.layoutControlItem2.Location = new System.Drawing.Point(948, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(188, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmViewItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 692);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmViewItems";
            this.Text = "Stock Counting Items";
            this.Load += new System.EventHandler(this.frmViewItems_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewItems_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcStockCountingDetailID;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemPriceId;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gcMRP;
        private DevExpress.XtraGrid.Columns.GridColumn gcSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemCodeID;
        private DevExpress.XtraGrid.Columns.GridColumn gcCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcSubCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcPhysicalStock;
        private DevExpress.XtraGrid.Columns.GridColumn gcSystemStock;
        private DevExpress.XtraGrid.Columns.GridColumn gcStockDiff;
        private DevExpress.XtraEditors.SimpleButton btnViewReport;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}