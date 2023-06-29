namespace NSRetail
{
    partial class frmViewReturnItems
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
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnGenerateCreditNote = new DevExpress.XtraEditors.SimpleButton();
            this.gcSupplierReturns = new DevExpress.XtraGrid.GridControl();
            this.gvSupplierReturns = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReturnstatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbReturnStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSupplierReturns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSupplierReturns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturnStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnPrint);
            this.layoutControl1.Controls.Add(this.btnGenerateCreditNote);
            this.layoutControl1.Controls.Add(this.gcSupplierReturns);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1151, 374, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1103, 635);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(962, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(129, 22);
            this.btnPrint.StyleController = this.layoutControl1;
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnGenerateCreditNote
            // 
            this.btnGenerateCreditNote.Location = new System.Drawing.Point(803, 12);
            this.btnGenerateCreditNote.Name = "btnGenerateCreditNote";
            this.btnGenerateCreditNote.Size = new System.Drawing.Size(155, 22);
            this.btnGenerateCreditNote.StyleController = this.layoutControl1;
            this.btnGenerateCreditNote.TabIndex = 11;
            this.btnGenerateCreditNote.Text = "Generate Credit Note";
            this.btnGenerateCreditNote.Click += new System.EventHandler(this.btnGenerateCreditNote_Click);
            // 
            // gcSupplierReturns
            // 
            this.gcSupplierReturns.Location = new System.Drawing.Point(12, 38);
            this.gcSupplierReturns.MainView = this.gvSupplierReturns;
            this.gcSupplierReturns.Name = "gcSupplierReturns";
            this.gcSupplierReturns.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbReturnStatus});
            this.gcSupplierReturns.Size = new System.Drawing.Size(1079, 585);
            this.gcSupplierReturns.TabIndex = 10;
            this.gcSupplierReturns.TabStop = false;
            this.gcSupplierReturns.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSupplierReturns});
            // 
            // gvSupplierReturns
            // 
            this.gvSupplierReturns.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvSupplierReturns.Appearance.FooterPanel.Options.UseFont = true;
            this.gvSupplierReturns.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvSupplierReturns.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvSupplierReturns.Appearance.Row.Options.UseFont = true;
            this.gvSupplierReturns.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn11,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn14,
            this.gridColumn13,
            this.gridColumn15,
            this.gcSelect,
            this.gcReturnstatus});
            this.gvSupplierReturns.GridControl = this.gcSupplierReturns;
            this.gvSupplierReturns.Name = "gvSupplierReturns";
            this.gvSupplierReturns.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvSupplierReturns.OptionsView.ShowFooter = true;
            this.gvSupplierReturns.OptionsView.ShowGroupPanel = false;
            this.gvSupplierReturns.OptionsView.ShowViewCaption = true;
            this.gvSupplierReturns.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gridView_CustomSummaryCalculate);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "SUPPLIERRETURNSDETAILID";
            this.gridColumn4.FieldName = "SUPPLIERRETURNSDETAILID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "SUPPLIERRETURNSID";
            this.gridColumn11.FieldName = "SUPPLIERRETURNSID";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "ITEMCOSTPRICEID";
            this.gridColumn5.FieldName = "ITEMCOSTPRICEID";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Item Name";
            this.gridColumn7.FieldName = "ITEMNAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 259;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Item Code";
            this.gridColumn8.FieldName = "ITEMCODE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 120;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "MRP";
            this.gridColumn9.FieldName = "MRP";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 65;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Sale Price";
            this.gridColumn10.FieldName = "SALEPRICE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 76;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Quantity";
            this.gridColumn12.FieldName = "QUANTITY";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QUANTITY", "{0:0.##}")});
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
            this.gridColumn12.Width = 64;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Cost Price";
            this.gridColumn14.FieldName = "COSTPRICE";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            this.gridColumn14.Width = 78;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Weight In Kgs";
            this.gridColumn13.FieldName = "WEIGHTINKGS";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 6;
            this.gridColumn13.Width = 90;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Total CP Value";
            this.gridColumn15.FieldName = "TOTALCOSTPRICE";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTALCOSTPRICE", "{0:0.##}")});
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 7;
            this.gridColumn15.Width = 106;
            // 
            // gcSelect
            // 
            this.gcSelect.AppearanceHeader.Options.UseTextOptions = true;
            this.gcSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcSelect.Caption = "Select";
            this.gcSelect.FieldName = "SELECTED";
            this.gcSelect.Name = "gcSelect";
            this.gcSelect.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "SELECTED", "", ((short)(1)))});
            this.gcSelect.Visible = true;
            this.gcSelect.VisibleIndex = 8;
            this.gcSelect.Width = 124;
            // 
            // gcReturnstatus
            // 
            this.gcReturnstatus.Caption = "Return Status";
            this.gcReturnstatus.ColumnEdit = this.cmbReturnStatus;
            this.gcReturnstatus.FieldName = "RETURNSTATUS";
            this.gcReturnstatus.Name = "gcReturnstatus";
            this.gcReturnstatus.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "RETURNSTATUS", "", ((short)(2)))});
            this.gcReturnstatus.Width = 149;
            // 
            // cmbReturnStatus
            // 
            this.cmbReturnStatus.AutoHeight = false;
            this.cmbReturnStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReturnStatus.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RETURNSTATUSID", "RETURNSTATUSID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RETURNSTATUSNAME", "RETURNSTATUSNAME")});
            this.cmbReturnStatus.Name = "cmbReturnStatus";
            this.cmbReturnStatus.ShowHeader = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1103, 635);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcSupplierReturns;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1083, 589);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnGenerateCreditNote;
            this.layoutControlItem2.Location = new System.Drawing.Point(791, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(159, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(791, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnPrint;
            this.layoutControlItem3.Location = new System.Drawing.Point(950, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(133, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmViewReturnItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 635);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Name = "frmViewReturnItems";
            this.Text = "View Return Items";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewReturnItems_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSupplierReturns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSupplierReturns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturnStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcSupplierReturns;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSupplierReturns;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnGenerateCreditNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gcSelect;
        private DevExpress.XtraGrid.Columns.GridColumn gcReturnstatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cmbReturnStatus;
    }
}