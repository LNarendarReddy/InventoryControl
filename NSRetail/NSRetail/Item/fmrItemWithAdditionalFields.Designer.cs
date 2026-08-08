namespace NSRetail
{
    partial class fmrItemWithAdditionalFields
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
            this.gcItemList = new DevExpress.XtraGrid.GridControl();
            this.gvItemList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbBrand = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbManufa = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbRefundPath = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbIndentCategory = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbManufa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbRefundPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbIndentCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcItemList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1130, 591);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcItemList
            // 
            this.gcItemList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcItemList.Location = new System.Drawing.Point(12, 12);
            this.gcItemList.MainView = this.gvItemList;
            this.gcItemList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcItemList.Name = "gcItemList";
            this.gcItemList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricmbBrand,
            this.ricmbManufa,
            this.ricmbIndentCategory,
            this.ricmbRefundPath});
            this.gcItemList.Size = new System.Drawing.Size(1106, 567);
            this.gcItemList.TabIndex = 5;
            this.gcItemList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemList});
            // 
            // gvItemList
            // 
            this.gvItemList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gvItemList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvItemList.Appearance.Row.Font = new System.Drawing.Font("Arial", 8F);
            this.gvItemList.Appearance.Row.Options.UseFont = true;
            this.gvItemList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn7,
            this.gridColumn5,
            this.gridColumn12,
            this.gridColumn15,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn25,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn8});
            this.gvItemList.DetailHeight = 404;
            this.gvItemList.GridControl = this.gcItemList;
            this.gvItemList.Name = "gvItemList";
            this.gvItemList.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.gvItemList.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.gvItemList.OptionsSelection.MultiSelect = true;
            this.gvItemList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvItemList.OptionsView.ShowFooter = true;
            this.gvItemList.OptionsView.ShowGroupPanel = false;
            this.gvItemList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvItemList_CustomDrawCell);
            this.gvItemList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvItemList_FocusedRowChanged);
            this.gvItemList.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvItemList_FocusedColumnChanged);
            this.gvItemList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvItemList_CellValueChanged);
            this.gvItemList.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvItemList_CellValueChanging);
            this.gvItemList.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvItemList_PopupMenuShowing);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ITEMID";
            this.gridColumn1.FieldName = "ITEMID";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Item Name";
            this.gridColumn2.FieldName = "ITEMNAME";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 157;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "SKU/Item Code";
            this.gridColumn7.FieldName = "SKUCODE";
            this.gridColumn7.MinWidth = 23;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 87;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Category";
            this.gridColumn5.FieldName = "CATEGORYNAME";
            this.gridColumn5.MinWidth = 23;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 87;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Sub Category";
            this.gridColumn12.FieldName = "SUBCATEGORYNAME";
            this.gridColumn12.MinWidth = 23;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 87;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "HSN Code";
            this.gridColumn15.FieldName = "HSNCODE";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Brand";
            this.gridColumn18.ColumnEdit = this.ricmbBrand;
            this.gridColumn18.FieldName = "BRANDID";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 8;
            // 
            // ricmbBrand
            // 
            this.ricmbBrand.AutoHeight = false;
            this.ricmbBrand.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbBrand.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANDID", "BRANDID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANDNAME", "BRANDNAME")});
            this.ricmbBrand.Name = "ricmbBrand";
            this.ricmbBrand.NullText = "";
            this.ricmbBrand.ShowHeader = false;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Manufacturer";
            this.gridColumn19.ColumnEdit = this.ricmbManufa;
            this.gridColumn19.FieldName = "MANUFACTURERID";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 9;
            // 
            // ricmbManufa
            // 
            this.ricmbManufa.AutoHeight = false;
            this.ricmbManufa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbManufa.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MANUFACTURERID", "MANUFACTURERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MANUFACTURERNAME", "MANUFACTURERNAME")});
            this.ricmbManufa.Name = "ricmbManufa";
            this.ricmbManufa.NullText = "";
            this.ricmbManufa.ShowHeader = false;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "Refund path";
            this.gridColumn25.ColumnEdit = this.ricmbRefundPath;
            this.gridColumn25.FieldName = "REFUNDPATHID";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 10;
            // 
            // ricmbRefundPath
            // 
            this.ricmbRefundPath.AutoHeight = false;
            this.ricmbRefundPath.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbRefundPath.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("REFUNDPATHID", "REFUNDPATHID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("REFUNDPATHTEXT", "REFUNDPATHTEXT")});
            this.ricmbRefundPath.Name = "ricmbRefundPath";
            this.ricmbRefundPath.NullText = "";
            this.ricmbRefundPath.ShowHeader = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Indent Category";
            this.gridColumn3.ColumnEdit = this.ricmbIndentCategory;
            this.gridColumn3.FieldName = "SUPPLIERINDENTITEMTYPEID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // ricmbIndentCategory
            // 
            this.ricmbIndentCategory.AutoHeight = false;
            this.ricmbIndentCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbIndentCategory.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ENUMID", "ENUMID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ENUMVALUE", "ENUMVALUE")});
            this.ricmbIndentCategory.Name = "ricmbIndentCategory";
            this.ricmbIndentCategory.NullText = "";
            this.ricmbIndentCategory.ShowHeader = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Inner Case Qnty";
            this.gridColumn4.FieldName = "INNERCASEQTY";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Outer Case Qnty";
            this.gridColumn6.FieldName = "OUTERCASEQTY";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Product Rank";
            this.gridColumn8.FieldName = "PRODUCTRANK";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1130, 591);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItemList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1110, 571);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // fmrItemWithAdditionalFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 591);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "fmrItemWithAdditionalFields";
            this.Text = "Item list with additional fileds";
            this.Load += new System.EventHandler(this.fmrItemWithAdditionalFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbManufa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbRefundPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbIndentCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcItemList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ricmbBrand;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ricmbManufa;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ricmbRefundPath;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ricmbIndentCategory;
    }
}
