namespace NSRetail.Supplier
{
    partial class frmViewReturnDetail
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.gcSupplierReturns = new DevExpress.XtraGrid.GridControl();
            this.gvSupplierReturns = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbReason = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSupplierReturns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSupplierReturns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnPrint);
            this.layoutControl1.Controls.Add(this.gcSupplierReturns);
            this.layoutControl1.Controls.Add(this.cmbSupplier);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1151, 374, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1103, 635);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(997, 596);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(887, 596);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 22);
            this.btnPrint.StyleController = this.layoutControl1;
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // gcSupplierReturns
            // 
            this.gcSupplierReturns.Location = new System.Drawing.Point(12, 12);
            this.gcSupplierReturns.MainView = this.gvSupplierReturns;
            this.gcSupplierReturns.Name = "gcSupplierReturns";
            this.gcSupplierReturns.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbReason});
            this.gcSupplierReturns.Size = new System.Drawing.Size(1079, 577);
            this.gcSupplierReturns.TabIndex = 0;
            this.gcSupplierReturns.TabStop = false;
            this.gcSupplierReturns.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSupplierReturns});
            // 
            // gvSupplierReturns
            // 
            this.gvSupplierReturns.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvSupplierReturns.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvSupplierReturns.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvSupplierReturns.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvSupplierReturns.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvSupplierReturns.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvSupplierReturns.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvSupplierReturns.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvSupplierReturns.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvSupplierReturns.Appearance.FooterPanel.Options.UseFont = true;
            this.gvSupplierReturns.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvSupplierReturns.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvSupplierReturns.Appearance.Row.Options.UseFont = true;
            this.gvSupplierReturns.Appearance.SelectedRow.BackColor = System.Drawing.Color.White;
            this.gvSupplierReturns.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvSupplierReturns.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvSupplierReturns.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvSupplierReturns.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.gvSupplierReturns.GridControl = this.gcSupplierReturns;
            this.gvSupplierReturns.Name = "gvSupplierReturns";
            this.gvSupplierReturns.OptionsBehavior.Editable = false;
            this.gvSupplierReturns.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvSupplierReturns.OptionsSelection.MultiSelect = true;
            this.gvSupplierReturns.OptionsView.ShowFooter = true;
            this.gvSupplierReturns.OptionsView.ShowGroupPanel = false;
            this.gvSupplierReturns.OptionsView.ShowViewCaption = true;
            this.gvSupplierReturns.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvSupplierReturns_PopupMenuShowing);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "S No";
            this.gridColumn1.FieldName = "SNO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 55;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "SUPPLIERRETURNSDETAILID";
            this.gridColumn2.FieldName = "SUPPLIERRETURNSDETAILID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "SUPPLIERRETURNSID";
            this.gridColumn3.FieldName = "SUPPLIERRETURNSID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "ITEMCOSTPRICEID";
            this.gridColumn4.FieldName = "ITEMCOSTPRICEID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Item Code";
            this.gridColumn5.FieldName = "ITEMCODE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 106;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Item Name";
            this.gridColumn6.FieldName = "ITEMNAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 323;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "MRP";
            this.gridColumn7.FieldName = "MRP";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 56;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Sale Price";
            this.gridColumn8.FieldName = "SALEPRICE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Quantity";
            this.gridColumn9.FieldName = "QUANTITY";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 61;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Cost Price";
            this.gridColumn10.FieldName = "COSTPRICE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Weight In KGs";
            this.gridColumn11.FieldName = "WEIGHTINKGS";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Branch";
            this.gridColumn12.FieldName = "BRANCHNAME";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            this.gridColumn12.Width = 91;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Created By";
            this.gridColumn13.FieldName = "CREATEDBY";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "BR Approved Date";
            this.gridColumn14.FieldName = "BRAPPOVEDDATE";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 12;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "UPDATEDBY";
            this.gridColumn15.FieldName = "UPDATEDBY";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "UPDATEDDATE";
            this.gridColumn16.FieldName = "UPDATEDDATE";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Refund Type";
            this.gridColumn20.FieldName = "REFUNDTYPE";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 8;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Total";
            this.gridColumn21.FieldName = "TOTALCOSTPRICE";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTALCOSTPRICE", "{0:0.##}")});
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 6;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Refund Date";
            this.gridColumn17.FieldName = "BRCREATEDDATE";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 11;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Reason";
            this.gridColumn18.ColumnEdit = this.cmbReason;
            this.gridColumn18.FieldName = "REASONID";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 9;
            // 
            // cmbReason
            // 
            this.cmbReason.AutoHeight = false;
            this.cmbReason.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReason.Name = "cmbReason";
            this.cmbReason.NullText = "";
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "BR Number";
            this.gridColumn19.FieldName = "BREFUNDNUMBER";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 10;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.EnterMoveNextControl = true;
            this.cmbSupplier.Location = new System.Drawing.Point(190, 597);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSupplier.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERID", "DEALERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERNAME", "DEALERNAME")});
            this.cmbSupplier.Properties.NullText = "";
            this.cmbSupplier.Properties.ShowHeader = false;
            this.cmbSupplier.Size = new System.Drawing.Size(322, 22);
            this.cmbSupplier.StyleController = this.layoutControl1;
            this.cmbSupplier.TabIndex = 2;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1103, 635);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcSupplierReturns;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1083, 581);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnPrint;
            this.layoutControlItem3.Location = new System.Drawing.Point(872, 581);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(110, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(110, 32);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(110, 34);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(982, 581);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(101, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(101, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(101, 34);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(508, 581);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(364, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbSupplier;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "Supplier";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 581);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(508, 34);
            this.layoutControlItem4.Text = "Select supplier to move items";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(162, 15);
            // 
            // frmViewReturnDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1103, 635);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmViewReturnDetail";
            this.Text = "Supplier Return Detail";
            this.Load += new System.EventHandler(this.frmViewReturnDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSupplierReturns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSupplierReturns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraGrid.GridControl gcSupplierReturns;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSupplierReturns;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cmbReason;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.LookUpEdit cmbSupplier;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}