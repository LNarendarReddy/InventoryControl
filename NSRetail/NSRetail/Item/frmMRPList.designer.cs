namespace NSRetail
{
    partial class frmMRPList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMRPList));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnAddMRP = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gcMRPList = new DevExpress.XtraGrid.GridControl();
            this.gvMRPList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMRP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtdecimal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCostPriceWT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCostPriceWOT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGSTCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbGST = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMRPList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMRPList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdecimal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.Control.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.Control.Options.UseFont = true;
            this.layoutControl1.Controls.Add(this.btnAddMRP);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.gcMRPList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(772, 112, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(857, 370);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnAddMRP
            // 
            this.btnAddMRP.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddMRP.ImageOptions.Image")));
            this.btnAddMRP.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAddMRP.Location = new System.Drawing.Point(412, 342);
            this.btnAddMRP.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddMRP.Name = "btnAddMRP";
            this.btnAddMRP.Size = new System.Drawing.Size(184, 22);
            this.btnAddMRP.StyleController = this.layoutControl1;
            this.btnAddMRP.TabIndex = 7;
            this.btnAddMRP.Text = "Add new price";
            this.btnAddMRP.Click += new System.EventHandler(this.btnAddMRP_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(738, 342);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.ImageOptions.Image")));
            this.btnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOk.Location = new System.Drawing.Point(608, 342);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(118, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gcMRPList
            // 
            this.gcMRPList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcMRPList.Location = new System.Drawing.Point(2, 2);
            this.gcMRPList.MainView = this.gvMRPList;
            this.gcMRPList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcMRPList.Name = "gcMRPList";
            this.gcMRPList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete,
            this.cmbGST,
            this.txtdecimal,
            this.btnEdit});
            this.gcMRPList.Size = new System.Drawing.Size(853, 332);
            this.gcMRPList.TabIndex = 4;
            this.gcMRPList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMRPList});
            // 
            // gvMRPList
            // 
            this.gvMRPList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gvMRPList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMRPList.Appearance.Row.Font = new System.Drawing.Font("Arial", 8F);
            this.gvMRPList.Appearance.Row.Options.UseFont = true;
            this.gvMRPList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMRP,
            this.gcSalePrice,
            this.gridColumn3,
            this.gcCostPriceWT,
            this.gcCostPriceWOT,
            this.gridColumn1,
            this.gcGSTCode,
            this.gridColumn4,
            this.gridColumn2,
            this.gcEdit,
            this.gridColumn5});
            this.gvMRPList.DetailHeight = 404;
            this.gvMRPList.FixedLineWidth = 3;
            this.gvMRPList.GridControl = this.gcMRPList;
            this.gvMRPList.Name = "gvMRPList";
            this.gvMRPList.OptionsView.ShowGroupPanel = false;
            // 
            // gcMRP
            // 
            this.gcMRP.Caption = "MRP";
            this.gcMRP.ColumnEdit = this.txtdecimal;
            this.gcMRP.FieldName = "MRP";
            this.gcMRP.MinWidth = 23;
            this.gcMRP.Name = "gcMRP";
            this.gcMRP.OptionsColumn.AllowEdit = false;
            this.gcMRP.Visible = true;
            this.gcMRP.VisibleIndex = 0;
            this.gcMRP.Width = 95;
            // 
            // txtdecimal
            // 
            this.txtdecimal.AutoHeight = false;
            this.txtdecimal.DisplayFormat.FormatString = "n2";
            this.txtdecimal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtdecimal.EditFormat.FormatString = "n2";
            this.txtdecimal.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtdecimal.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtdecimal.MaskSettings.Set("mask", "n2");
            this.txtdecimal.Name = "txtdecimal";
            // 
            // gcSalePrice
            // 
            this.gcSalePrice.Caption = "Sale Price";
            this.gcSalePrice.ColumnEdit = this.txtdecimal;
            this.gcSalePrice.FieldName = "SALEPRICE";
            this.gcSalePrice.MinWidth = 23;
            this.gcSalePrice.Name = "gcSalePrice";
            this.gcSalePrice.OptionsColumn.AllowEdit = false;
            this.gcSalePrice.Visible = true;
            this.gcSalePrice.VisibleIndex = 1;
            this.gcSalePrice.Width = 104;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ITEMPRICEID";
            this.gridColumn3.FieldName = "ITEMPRICEID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 87;
            // 
            // gcCostPriceWT
            // 
            this.gcCostPriceWT.Caption = "Cost Price WT";
            this.gcCostPriceWT.FieldName = "COSTPRICEWT";
            this.gcCostPriceWT.Name = "gcCostPriceWT";
            this.gcCostPriceWT.Visible = true;
            this.gcCostPriceWT.VisibleIndex = 3;
            this.gcCostPriceWT.Width = 85;
            // 
            // gcCostPriceWOT
            // 
            this.gcCostPriceWOT.Caption = "Cost Price WOT";
            this.gcCostPriceWOT.FieldName = "COSTPRICEWOT";
            this.gcCostPriceWOT.Name = "gcCostPriceWOT";
            this.gcCostPriceWOT.Visible = true;
            this.gcCostPriceWOT.VisibleIndex = 4;
            this.gcCostPriceWOT.Width = 95;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "GST Code";
            this.gridColumn1.FieldName = "GSTCODE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gcGSTCode
            // 
            this.gcGSTCode.Caption = "GST Code";
            this.gcGSTCode.ColumnEdit = this.cmbGST;
            this.gcGSTCode.FieldName = "GSTID";
            this.gcGSTCode.Name = "gcGSTCode";
            this.gcGSTCode.OptionsColumn.AllowEdit = false;
            this.gcGSTCode.Visible = true;
            this.gcGSTCode.VisibleIndex = 2;
            this.gcGSTCode.Width = 104;
            // 
            // cmbGST
            // 
            this.cmbGST.AutoHeight = false;
            this.cmbGST.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGST.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GSTID", "GSTID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GSTCODE", "GSTCODE")});
            this.cmbGST.Name = "cmbGST";
            this.cmbGST.NullText = "";
            this.cmbGST.ShowHeader = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Immediate";
            this.gridColumn4.FieldName = "IMMEDIATE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 85;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Go live date & time";
            this.gridColumn2.DisplayFormat.FormatString = "g";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "GOLIVEDATE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 208;
            // 
            // gcEdit
            // 
            this.gcEdit.AppearanceCell.Options.UseTextOptions = true;
            this.gcEdit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gcEdit.AppearanceHeader.Options.UseTextOptions = true;
            this.gcEdit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcEdit.Caption = "Edit";
            this.gcEdit.ColumnEdit = this.btnEdit;
            this.gcEdit.Name = "gcEdit";
            this.gcEdit.Visible = true;
            this.gcEdit.VisibleIndex = 7;
            this.gcEdit.Width = 108;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoHeight = false;
            editorButtonImageOptions1.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            editorButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions1.SvgImage")));
            this.btnEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoHeight = false;
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDelete_ButtonClick);
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 10F);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(857, 370);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMRPList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(857, 336);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOk;
            this.layoutControlItem2.Location = new System.Drawing.Point(602, 336);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(130, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(732, 336);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(125, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnAddMRP;
            this.layoutControlItem4.Location = new System.Drawing.Point(406, 336);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(196, 34);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 336);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(406, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "MOVESTATUS";
            this.gridColumn5.FieldName = "MOVESTATUS";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // frmMRPList
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(857, 370);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMRPList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MRP List";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMRPList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMRPList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdecimal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcMRPList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMRPList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcMRP;
        private DevExpress.XtraGrid.Columns.GridColumn gcSalePrice;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gcCostPriceWT;
        private DevExpress.XtraGrid.Columns.GridColumn gcCostPriceWOT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gcGSTCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cmbGST;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtdecimal;
        private DevExpress.XtraGrid.Columns.GridColumn gcEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnAddMRP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}