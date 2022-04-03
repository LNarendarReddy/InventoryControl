namespace NSRetail
{
    partial class frmViewDCItems
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
            this.gcMOP = new DevExpress.XtraGrid.GridControl();
            this.gvMOP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItems = new DevExpress.XtraGrid.GridControl();
            this.gvItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGSTCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGSTValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMOP = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMOP)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcMOP);
            this.layoutControl1.Controls.Add(this.gcItems);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1028, 645);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcMOP
            // 
            this.gcMOP.Location = new System.Drawing.Point(4, 484);
            this.gcMOP.MainView = this.gvMOP;
            this.gcMOP.Name = "gcMOP";
            this.gcMOP.Size = new System.Drawing.Size(1020, 157);
            this.gcMOP.TabIndex = 5;
            this.gcMOP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMOP});
            // 
            // gvMOP
            // 
            this.gvMOP.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvMOP.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvMOP.Appearance.Row.Options.UseTextOptions = true;
            this.gvMOP.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvMOP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.gvMOP.GridControl = this.gcMOP;
            this.gvMOP.Name = "gvMOP";
            this.gvMOP.OptionsBehavior.Editable = false;
            this.gvMOP.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "BILLMOPDETAILID";
            this.gridColumn8.FieldName = "BILLMOPDETAILID";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "MOPID";
            this.gridColumn9.FieldName = "MOPID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Mode of Payment";
            this.gridColumn10.FieldName = "MOPNAME";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 113;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "MOP Value";
            this.gridColumn11.FieldName = "MOPVALUE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 882;
            // 
            // gcItems
            // 
            this.gcItems.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcItems.Location = new System.Drawing.Point(4, 4);
            this.gcItems.MainView = this.gvItems;
            this.gcItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcItems.Name = "gcItems";
            this.gcItems.Size = new System.Drawing.Size(1020, 476);
            this.gcItems.TabIndex = 4;
            this.gcItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItems});
            // 
            // gvItems
            // 
            this.gvItems.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvItems.Appearance.FooterPanel.Options.UseFont = true;
            this.gvItems.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvItems.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gcDiscount,
            this.gcGSTCode,
            this.gcGSTValue});
            this.gvItems.DetailHeight = 404;
            this.gvItems.GridControl = this.gcItems;
            this.gvItems.Name = "gvItems";
            this.gvItems.OptionsView.ShowFooter = true;
            this.gvItems.OptionsView.ShowGroupPanel = false;
            this.gvItems.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvItems_PopupMenuShowing);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Item  Code";
            this.gridColumn1.FieldName = "ITEMCODE";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Item Name";
            this.gridColumn2.FieldName = "ITEMNAME";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 87;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "MRP";
            this.gridColumn3.FieldName = "MRP";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 87;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Sale Price";
            this.gridColumn4.FieldName = "SALEPRICE";
            this.gridColumn4.MinWidth = 23;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 87;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Quantity";
            this.gridColumn5.FieldName = "QUANTITY";
            this.gridColumn5.MinWidth = 23;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 87;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Weight In Kgs";
            this.gridColumn6.FieldName = "WEIGHTINKGS";
            this.gridColumn6.MinWidth = 23;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 87;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Billed Amount";
            this.gridColumn7.FieldName = "BILLEDAMOUNT";
            this.gridColumn7.MinWidth = 23;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BILLEDAMOUNT", "{0:0.##}")});
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 87;
            // 
            // gcDiscount
            // 
            this.gcDiscount.Caption = "Discount";
            this.gcDiscount.FieldName = "DISCOUNT";
            this.gcDiscount.MinWidth = 23;
            this.gcDiscount.Name = "gcDiscount";
            this.gcDiscount.OptionsColumn.AllowEdit = false;
            this.gcDiscount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DISCOUNT", "{0:0.##}")});
            this.gcDiscount.Visible = true;
            this.gcDiscount.VisibleIndex = 7;
            this.gcDiscount.Width = 87;
            // 
            // gcGSTCode
            // 
            this.gcGSTCode.Caption = "GST Code";
            this.gcGSTCode.FieldName = "GSTCODE";
            this.gcGSTCode.MinWidth = 23;
            this.gcGSTCode.Name = "gcGSTCode";
            this.gcGSTCode.Visible = true;
            this.gcGSTCode.VisibleIndex = 8;
            this.gcGSTCode.Width = 87;
            // 
            // gcGSTValue
            // 
            this.gcGSTValue.Caption = "GST Value";
            this.gcGSTValue.FieldName = "GSTVALUE";
            this.gcGSTValue.MinWidth = 23;
            this.gcGSTValue.Name = "gcGSTValue";
            this.gcGSTValue.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GSTVALUE", "{0:0.##}")});
            this.gcGSTValue.Visible = true;
            this.gcGSTValue.VisibleIndex = 9;
            this.gcGSTValue.Width = 87;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lciMOP});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1028, 645);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItems;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1024, 480);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lciMOP
            // 
            this.lciMOP.Control = this.gcMOP;
            this.lciMOP.Location = new System.Drawing.Point(0, 480);
            this.lciMOP.Name = "lciMOP";
            this.lciMOP.Size = new System.Drawing.Size(1024, 161);
            this.lciMOP.TextSize = new System.Drawing.Size(0, 0);
            this.lciMOP.TextVisible = false;
            // 
            // frmViewDCItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 645);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmViewDCItems";
            this.Text = "Day Closure Items";
            this.Load += new System.EventHandler(this.frmViewDCItems_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewDCItems_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMOP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gcDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn gcGSTCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcGSTValue;
        private DevExpress.XtraGrid.GridControl gcMOP;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMOP;
        private DevExpress.XtraLayout.LayoutControlItem lciMOP;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
    }
}