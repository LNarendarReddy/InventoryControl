namespace NSRetail
{
    partial class frmCreateOfferList
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateOfferList));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject17 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject18 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject19 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject20 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gcOffer = new DevExpress.XtraGrid.GridControl();
            this.gvOffer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbItemCode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.cmbItemCodeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOfferType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbOffertype = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcOffer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOffer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCodeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOffertype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcOffer
            // 
            this.gcOffer.Location = new System.Drawing.Point(4, 44);
            this.gcOffer.MainView = this.gvOffer;
            this.gcOffer.Name = "gcOffer";
            this.gcOffer.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbItemCode,
            this.cmbOffertype,
            this.btnDelete});
            this.gcOffer.Size = new System.Drawing.Size(1005, 558);
            this.gcOffer.TabIndex = 0;
            this.gcOffer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOffer});
            // 
            // gvOffer
            // 
            this.gvOffer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gcItemCode,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gcOfferType,
            this.gridColumn14,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gcDelete});
            this.gvOffer.GridControl = this.gcOffer;
            this.gvOffer.Name = "gvOffer";
            this.gvOffer.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvOffer.OptionsView.ShowFooter = true;
            this.gvOffer.OptionsView.ShowGroupPanel = false;
            this.gvOffer.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvOffer_ShowingEditor);
            this.gvOffer.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvOffer_InitNewRow);
            this.gvOffer.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvOffer_ValidateRow);
            this.gvOffer.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvOffer_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "OFFERID";
            this.gridColumn1.FieldName = "OFFERID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ITEMCODEID";
            this.gridColumn2.FieldName = "ITEMCODEID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gcItemCode
            // 
            this.gcItemCode.Caption = "Item Code";
            this.gcItemCode.ColumnEdit = this.cmbItemCode;
            this.gcItemCode.FieldName = "ITEMCODEID";
            this.gcItemCode.Name = "gcItemCode";
            this.gcItemCode.Visible = true;
            this.gcItemCode.VisibleIndex = 0;
            this.gcItemCode.Width = 174;
            // 
            // cmbItemCode
            // 
            this.cmbItemCode.AutoHeight = false;
            this.cmbItemCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbItemCode.Name = "cmbItemCode";
            this.cmbItemCode.PopupView = this.cmbItemCodeView;
            this.cmbItemCode.EditValueChanged += new System.EventHandler(this.cmbItemCode_EditValueChanged);
            // 
            // cmbItemCodeView
            // 
            this.cmbItemCodeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20});
            this.cmbItemCodeView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cmbItemCodeView.Name = "cmbItemCodeView";
            this.cmbItemCodeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cmbItemCodeView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "ITEMID";
            this.gridColumn16.FieldName = "ITEMID";
            this.gridColumn16.Name = "gridColumn16";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "ITEMCODEID";
            this.gridColumn17.FieldName = "ITEMCODEID";
            this.gridColumn17.Name = "gridColumn17";
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "SKU Code";
            this.gridColumn18.FieldName = "SKUCODE";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 2;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Item Code";
            this.gridColumn19.FieldName = "ITEMCODE";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 0;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Item Name";
            this.gridColumn20.FieldName = "ITEMNAME";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Item Name";
            this.gridColumn4.FieldName = "ITEMNAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 519;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Offer Name";
            this.gridColumn5.FieldName = "OFFERNAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Offer Code";
            this.gridColumn6.FieldName = "OFFERCODE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            // 
            // gcOfferType
            // 
            this.gcOfferType.Caption = "Offer Type";
            this.gcOfferType.ColumnEdit = this.cmbOffertype;
            this.gcOfferType.FieldName = "OFFERTYPEID";
            this.gcOfferType.Name = "gcOfferType";
            this.gcOfferType.Visible = true;
            this.gcOfferType.VisibleIndex = 2;
            this.gcOfferType.Width = 184;
            // 
            // cmbOffertype
            // 
            this.cmbOffertype.AutoHeight = false;
            this.cmbOffertype.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOffertype.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OFFERTYPEID", "OFFERTYPEID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OFFERTYPECODE", "OFFERTYPECODE", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OFFERTYPENAME", "OFFERTYPENAME")});
            this.cmbOffertype.Name = "cmbOffertype";
            this.cmbOffertype.ShowHeader = false;
            this.cmbOffertype.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Offer Value";
            this.gridColumn14.FieldName = "OFFERVALUE";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 3;
            this.gridColumn14.Width = 211;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Created By";
            this.gridColumn8.FieldName = "CREATEDBY";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Created Date";
            this.gridColumn9.FieldName = "CREATEDDATE";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Updated By";
            this.gridColumn10.FieldName = "UPDATEDBY";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Updated Date";
            this.gridColumn11.FieldName = "UPDATEDDATE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Deleted By";
            this.gridColumn12.FieldName = "DELETEDBY";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Deleted Date";
            this.gridColumn13.FieldName = "DELETEDDATE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            // 
            // gcDelete
            // 
            this.gcDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDelete.Caption = "Delete";
            this.gcDelete.ColumnEdit = this.btnDelete;
            this.gcDelete.Name = "gcDelete";
            this.gcDelete.Visible = true;
            this.gcDelete.VisibleIndex = 4;
            this.gcDelete.Width = 55;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoHeight = false;
            editorButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions5.SvgImage")));
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject17, serializableAppearanceObject18, serializableAppearanceObject19, serializableAppearanceObject20, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDelete_ButtonClick);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnImport);
            this.layoutControl1.Controls.Add(this.gcOffer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1241, 345, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1013, 606);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1013, 606);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcOffer;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1009, 562);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // btnImport
            // 
            this.btnImport.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnImport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnImport.ImageOptions.SvgImage")));
            this.btnImport.Location = new System.Drawing.Point(892, 4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(117, 36);
            this.btnImport.StyleController = this.layoutControl1;
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnImport;
            this.layoutControlItem2.Location = new System.Drawing.Point(888, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(121, 40);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(121, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(121, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(888, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmCreateOfferList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 606);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "frmCreateOfferList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Offer List";
            this.Load += new System.EventHandler(this.frmCreateOfferList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCreateOfferList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gcOffer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOffer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCodeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOffertype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcOffer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOffer;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gcOfferType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cmbItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView cmbItemCodeView;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cmbOffertype;
        private DevExpress.XtraGrid.Columns.GridColumn gcDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}