﻿namespace NSRetail.ReportForms.Branch.POSReports
{
    partial class frmViewDCBills
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewDCBills));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcBills = new DevExpress.XtraGrid.GridControl();
            this.gvBills = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnViewItems = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gcItemValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMOPValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcBills);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1136, 660);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcBills
            // 
            this.gcBills.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcBills.Location = new System.Drawing.Point(4, 4);
            this.gcBills.MainView = this.gvBills;
            this.gcBills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcBills.Name = "gcBills";
            this.gcBills.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnViewItems,
            this.repositoryItemDateEdit1});
            this.gcBills.Size = new System.Drawing.Size(1128, 652);
            this.gcBills.TabIndex = 4;
            this.gcBills.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBills});
            // 
            // gvBills
            // 
            this.gvBills.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn16,
            this.gridColumn9,
            this.gridColumn10,
            this.gcItemValue,
            this.gcMOPValue,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn17,
            this.gridColumn18});
            this.gvBills.DetailHeight = 404;
            this.gvBills.GridControl = this.gcBills;
            this.gvBills.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ITEMAMOUNT", this.gcItemValue, "{0:0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MOPAMOUNT", this.gcMOPValue, "{0:0.##}")});
            this.gvBills.Name = "gvBills";
            this.gvBills.OptionsView.ShowFooter = true;
            this.gvBills.OptionsView.ShowGroupPanel = false;
            this.gvBills.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvBills_PopupMenuShowing);
            this.gvBills.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gvBills_CustomSummaryCalculate);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "BILLID";
            this.gridColumn1.FieldName = "BILLID";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "BRANCHCOUNTERID";
            this.gridColumn2.FieldName = "BRANCHCOUNTERID";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Width = 87;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "DAYCLOSUREID";
            this.gridColumn3.FieldName = "DAYCLOSUREID";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Width = 87;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Bill Number";
            this.gridColumn4.FieldName = "BILLNUMBER";
            this.gridColumn4.MinWidth = 23;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 175;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Customer Name";
            this.gridColumn5.FieldName = "CUSTOMERNAME";
            this.gridColumn5.MinWidth = 23;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Width = 87;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Customer Mobile";
            this.gridColumn6.FieldName = "CUSTOMERNUMBER";
            this.gridColumn6.MinWidth = 23;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Width = 87;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Billing User";
            this.gridColumn7.FieldName = "CREATEDBY";
            this.gridColumn7.MinWidth = 23;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 122;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Created Time";
            this.gridColumn8.ColumnEdit = this.repositoryItemDateEdit1;
            this.gridColumn8.FieldName = "CREATEDDATE";
            this.gridColumn8.MinWidth = 23;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 84;
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
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Closure Date";
            this.gridColumn16.ColumnEdit = this.repositoryItemDateEdit1;
            this.gridColumn16.FieldName = "UPDATEDDATE";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Width = 107;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Bill Status";
            this.gridColumn9.FieldName = "BILLSTATUS";
            this.gridColumn9.MinWidth = 23;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 98;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "View Items";
            this.gridColumn10.ColumnEdit = this.btnViewItems;
            this.gridColumn10.MinWidth = 23;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 100;
            // 
            // btnViewItems
            // 
            this.btnViewItems.AutoHeight = false;
            editorButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions1.SvgImage")));
            this.btnViewItems.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnViewItems.Name = "btnViewItems";
            this.btnViewItems.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnViewItems.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnViewItems_ButtonClick);
            // 
            // gcItemValue
            // 
            this.gcItemValue.Caption = "Item Value";
            this.gcItemValue.FieldName = "ITEMAMOUNT";
            this.gcItemValue.Name = "gcItemValue";
            this.gcItemValue.OptionsColumn.AllowEdit = false;
            this.gcItemValue.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "ITEMAMOUNT", "{0:0.##}")});
            this.gcItemValue.Visible = true;
            this.gcItemValue.VisibleIndex = 1;
            this.gcItemValue.Width = 99;
            // 
            // gcMOPValue
            // 
            this.gcMOPValue.Caption = "MOP Value";
            this.gcMOPValue.FieldName = "MOPAMOUNT";
            this.gcMOPValue.Name = "gcMOPValue";
            this.gcMOPValue.OptionsColumn.AllowEdit = false;
            this.gcMOPValue.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MOPAMOUNT", "{0:0.##}")});
            this.gcMOPValue.Visible = true;
            this.gcMOPValue.VisibleIndex = 2;
            this.gcMOPValue.Width = 100;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Difference";
            this.gridColumn11.FieldName = "BillDIFF";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            this.gridColumn11.Width = 94;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "TENDEREDCASH";
            this.gridColumn12.FieldName = "TENDEREDCASH";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "TENDEREDCHANGE";
            this.gridColumn13.FieldName = "TENDEREDCHANGE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Bill Mode";
            this.gridColumn14.FieldName = "ISDOORDELIVERY";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Bill Status ID";
            this.gridColumn15.FieldName = "BILLSTATUSID";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Finished User";
            this.gridColumn17.FieldName = "BILLCLOSEDBY";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 7;
            this.gridColumn17.Width = 138;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Finished time";
            this.gridColumn18.FieldName = "BILLCLOSEDDATE";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 8;
            this.gridColumn18.Width = 93;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1136, 660);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcBills;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1132, 656);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmViewDCBills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 660);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmViewDCBills";
            this.Text = "Day Closure Bills";
            this.Load += new System.EventHandler(this.frmViewDCBills_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewDCBills_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcBills;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBills;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnViewItems;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemValue;
        private DevExpress.XtraGrid.Columns.GridColumn gcMOPValue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
    }
}