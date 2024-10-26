namespace NSRetail.ReportForms.Branch.POSReports
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
            this.gcQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBilledAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGSTCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGSTValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBillNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gcDeletedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDeletedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMOP = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
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
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1175, 731);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcMOP
            // 
            this.gcMOP.Location = new System.Drawing.Point(4, 548);
            this.gcMOP.MainView = this.gvMOP;
            this.gcMOP.Name = "gcMOP";
            this.gcMOP.Size = new System.Drawing.Size(1167, 179);
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
            this.gvMOP.DetailHeight = 397;
            this.gvMOP.GridControl = this.gcMOP;
            this.gvMOP.Name = "gvMOP";
            this.gvMOP.OptionsBehavior.Editable = false;
            this.gvMOP.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "BILLMOPDETAILID";
            this.gridColumn8.FieldName = "BILLMOPDETAILID";
            this.gridColumn8.MinWidth = 23;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 86;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "MOPID";
            this.gridColumn9.FieldName = "MOPID";
            this.gridColumn9.MinWidth = 23;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 86;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Mode of Payment";
            this.gridColumn10.FieldName = "MOPNAME";
            this.gridColumn10.MinWidth = 23;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 129;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "MOP Value";
            this.gridColumn11.FieldName = "MOPVALUE";
            this.gridColumn11.MinWidth = 23;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 1008;
            // 
            // gcItems
            // 
            this.gcItems.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(7, 3, 7, 3);
            this.gcItems.Location = new System.Drawing.Point(4, 4);
            this.gcItems.MainView = this.gvItems;
            this.gcItems.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcItems.Name = "gcItems";
            this.gcItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1});
            this.gcItems.Size = new System.Drawing.Size(1167, 540);
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
            this.gcQuantity,
            this.gridColumn6,
            this.gcBilledAmount,
            this.gcDiscount,
            this.gcGSTCode,
            this.gcGSTValue,
            this.gcBillNumber,
            this.gcCreatedBy,
            this.gcCreatedDate,
            this.gcDeletedBy,
            this.gcDeletedDate});
            this.gvItems.DetailHeight = 458;
            this.gvItems.GridControl = this.gcItems;
            this.gvItems.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvItems.Name = "gvItems";
            this.gvItems.OptionsBehavior.Editable = false;
            this.gvItems.OptionsView.ShowFooter = true;
            this.gvItems.OptionsView.ShowGroupPanel = false;
            this.gvItems.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvItems_PopupMenuShowing);
            this.gvItems.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gvItems_CustomSummaryCalculate);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Item  Code";
            this.gridColumn1.FieldName = "ITEMCODE";
            this.gridColumn1.MinWidth = 26;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 99;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Item Name";
            this.gridColumn2.FieldName = "ITEMNAME";
            this.gridColumn2.MinWidth = 26;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 99;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "MRP";
            this.gridColumn3.FieldName = "MRP";
            this.gridColumn3.MinWidth = 26;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 99;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Sale Price";
            this.gridColumn4.FieldName = "SALEPRICE";
            this.gridColumn4.MinWidth = 26;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 99;
            // 
            // gcQuantity
            // 
            this.gcQuantity.Caption = "Billed Quantity";
            this.gcQuantity.FieldName = "QUANTITY";
            this.gcQuantity.MinWidth = 26;
            this.gcQuantity.Name = "gcQuantity";
            this.gcQuantity.OptionsColumn.AllowEdit = false;
            this.gcQuantity.Visible = true;
            this.gcQuantity.VisibleIndex = 4;
            this.gcQuantity.Width = 99;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Weight In Kgs";
            this.gridColumn6.FieldName = "WEIGHTINKGS";
            this.gridColumn6.MinWidth = 26;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 99;
            // 
            // gcBilledAmount
            // 
            this.gcBilledAmount.Caption = "Billed Amount";
            this.gcBilledAmount.FieldName = "BILLEDAMOUNT";
            this.gcBilledAmount.MinWidth = 26;
            this.gcBilledAmount.Name = "gcBilledAmount";
            this.gcBilledAmount.OptionsColumn.AllowEdit = false;
            this.gcBilledAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BILLEDAMOUNT", "SUM={0:0.##}")});
            this.gcBilledAmount.Visible = true;
            this.gcBilledAmount.VisibleIndex = 6;
            this.gcBilledAmount.Width = 99;
            // 
            // gcDiscount
            // 
            this.gcDiscount.Caption = "Discount";
            this.gcDiscount.FieldName = "DISCOUNT";
            this.gcDiscount.MinWidth = 26;
            this.gcDiscount.Name = "gcDiscount";
            this.gcDiscount.OptionsColumn.AllowEdit = false;
            this.gcDiscount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DISCOUNT", "{0:0.##}")});
            this.gcDiscount.Width = 99;
            // 
            // gcGSTCode
            // 
            this.gcGSTCode.Caption = "GST Code";
            this.gcGSTCode.FieldName = "GSTCODE";
            this.gcGSTCode.MinWidth = 26;
            this.gcGSTCode.Name = "gcGSTCode";
            this.gcGSTCode.Width = 99;
            // 
            // gcGSTValue
            // 
            this.gcGSTValue.Caption = "GST Value";
            this.gcGSTValue.FieldName = "GSTVALUE";
            this.gcGSTValue.MinWidth = 26;
            this.gcGSTValue.Name = "gcGSTValue";
            this.gcGSTValue.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GSTVALUE", "{0:0.##}")});
            this.gcGSTValue.Width = 99;
            // 
            // gcBillNumber
            // 
            this.gcBillNumber.Caption = "Bill Number";
            this.gcBillNumber.FieldName = "BILLNUMBER";
            this.gcBillNumber.MinWidth = 23;
            this.gcBillNumber.Name = "gcBillNumber";
            this.gcBillNumber.Width = 86;
            // 
            // gcCreatedBy
            // 
            this.gcCreatedBy.Caption = "Billed User";
            this.gcCreatedBy.FieldName = "CREATEDBY";
            this.gcCreatedBy.MinWidth = 23;
            this.gcCreatedBy.Name = "gcCreatedBy";
            this.gcCreatedBy.Width = 86;
            // 
            // gcCreatedDate
            // 
            this.gcCreatedDate.Caption = "Billed Date";
            this.gcCreatedDate.ColumnEdit = this.repositoryItemDateEdit1;
            this.gcCreatedDate.FieldName = "CREATEDDATE";
            this.gcCreatedDate.MinWidth = 23;
            this.gcCreatedDate.Name = "gcCreatedDate";
            this.gcCreatedDate.Width = 86;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "G";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "G";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.MaskSettings.Set("mask", "G");
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // gcDeletedBy
            // 
            this.gcDeletedBy.Caption = "Delete By";
            this.gcDeletedBy.FieldName = "DELETEDBY";
            this.gcDeletedBy.MinWidth = 23;
            this.gcDeletedBy.Name = "gcDeletedBy";
            this.gcDeletedBy.Width = 86;
            // 
            // gcDeletedDate
            // 
            this.gcDeletedDate.Caption = "Deleted Date";
            this.gcDeletedDate.ColumnEdit = this.repositoryItemDateEdit1;
            this.gcDeletedDate.FieldName = "DELETEDDATE";
            this.gcDeletedDate.MinWidth = 23;
            this.gcDeletedDate.Name = "gcDeletedDate";
            this.gcDeletedDate.Width = 86;
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
            this.Root.Size = new System.Drawing.Size(1175, 731);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItems;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1171, 544);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lciMOP
            // 
            this.lciMOP.Control = this.gcMOP;
            this.lciMOP.Location = new System.Drawing.Point(0, 544);
            this.lciMOP.Name = "lciMOP";
            this.lciMOP.Size = new System.Drawing.Size(1171, 183);
            this.lciMOP.TextSize = new System.Drawing.Size(0, 0);
            this.lciMOP.TextVisible = false;
            // 
            // frmViewDCItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 731);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
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
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gcQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gcBilledAmount;
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
        private DevExpress.XtraGrid.Columns.GridColumn gcBillNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gcCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn gcCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcDeletedBy;
        private DevExpress.XtraGrid.Columns.GridColumn gcDeletedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
    }
}