namespace NSRetailPOS.UI
{
    partial class frmCarryBagSelection
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraGrid.GridControl gcItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItems;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup root;
        private DevExpress.XtraLayout.LayoutControlItem gridLayoutItem;
        private DevExpress.XtraLayout.LayoutControlItem selectLayoutItem;
        private DevExpress.XtraLayout.LayoutControlItem cancelLayoutItem;
        private DevExpress.XtraLayout.EmptySpaceItem buttonSpace;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCarryBagSelection));
            this.gcItems = new DevExpress.XtraGrid.GridControl();
            this.gvItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridLayoutItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.buttonSpace = new DevExpress.XtraLayout.EmptySpaceItem();
            this.selectLayoutItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.cancelLayoutItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayoutItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectLayoutItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cancelLayoutItem)).BeginInit();
            this.SuspendLayout();
            // 
            // gcItems
            // 
            this.gcItems.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcItems.Location = new System.Drawing.Point(14, 14);
            this.gcItems.MainView = this.gvItems;
            this.gcItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcItems.Name = "gcItems";
            this.gcItems.Size = new System.Drawing.Size(982, 332);
            this.gcItems.TabIndex = 0;
            this.gcItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItems});
            // 
            // gvItems
            // 
            this.gvItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvItems.DetailHeight = 404;
            this.gvItems.GridControl = this.gcItems;
            this.gvItems.Name = "gvItems";
            this.gvItems.OptionsBehavior.Editable = false;
            this.gvItems.OptionsCustomization.AllowColumnMoving = false;
            this.gvItems.OptionsCustomization.AllowFilter = false;
            this.gvItems.OptionsCustomization.AllowGroup = false;
            this.gvItems.OptionsCustomization.AllowSort = false;
            this.gvItems.OptionsEditForm.PopupEditFormWidth = 933;
            this.gvItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvItems.OptionsView.ShowGroupPanel = false;
            this.gvItems.OptionsView.ShowIndicator = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "SKU Code";
            this.gridColumn1.FieldName = "SKUCODE";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 128;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Item Code";
            this.gridColumn2.FieldName = "ITEMCODE";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 152;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Item Name";
            this.gridColumn3.FieldName = "ITEMNAME";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 397;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "MRP";
            this.gridColumn4.FieldName = "MRP";
            this.gridColumn4.MinWidth = 23;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 163;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Selling Price";
            this.gridColumn5.FieldName = "SALEPRICE";
            this.gridColumn5.MinWidth = 23;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 163;
            // 
            // btnSelect
            // 
            this.btnSelect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSelect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSelect.ImageOptions.SvgImage")));
            this.btnSelect.Location = new System.Drawing.Point(720, 350);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(136, 40);
            this.btnSelect.StyleController = this.layoutControl;
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "&Select";
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.gcItems);
            this.layoutControl.Controls.Add(this.btnSelect);
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.root;
            this.layoutControl.Size = new System.Drawing.Size(1010, 404);
            this.layoutControl.TabIndex = 0;
            // 
            // root
            // 
            this.root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.root.GroupBordersVisible = false;
            this.root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.gridLayoutItem,
            this.buttonSpace,
            this.selectLayoutItem,
            this.cancelLayoutItem});
            this.root.Name = "root";
            this.root.Size = new System.Drawing.Size(1010, 404);
            // 
            // gridLayoutItem
            // 
            this.gridLayoutItem.Control = this.gcItems;
            this.gridLayoutItem.Location = new System.Drawing.Point(0, 0);
            this.gridLayoutItem.Name = "gridLayoutItem";
            this.gridLayoutItem.Size = new System.Drawing.Size(986, 336);
            this.gridLayoutItem.TextSize = new System.Drawing.Size(0, 0);
            this.gridLayoutItem.TextVisible = false;
            // 
            // buttonSpace
            // 
            this.buttonSpace.AllowHotTrack = false;
            this.buttonSpace.Location = new System.Drawing.Point(0, 336);
            this.buttonSpace.Name = "buttonSpace";
            this.buttonSpace.Size = new System.Drawing.Size(706, 44);
            this.buttonSpace.TextSize = new System.Drawing.Size(0, 0);
            // 
            // selectLayoutItem
            // 
            this.selectLayoutItem.Control = this.btnSelect;
            this.selectLayoutItem.Location = new System.Drawing.Point(706, 336);
            this.selectLayoutItem.MaxSize = new System.Drawing.Size(140, 44);
            this.selectLayoutItem.MinSize = new System.Drawing.Size(140, 44);
            this.selectLayoutItem.Name = "selectLayoutItem";
            this.selectLayoutItem.Size = new System.Drawing.Size(140, 44);
            this.selectLayoutItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.selectLayoutItem.TextSize = new System.Drawing.Size(0, 0);
            this.selectLayoutItem.TextVisible = false;
            // 
            // cancelLayoutItem
            // 
            this.cancelLayoutItem.Control = this.btnCancel;
            this.cancelLayoutItem.Location = new System.Drawing.Point(846, 336);
            this.cancelLayoutItem.MaxSize = new System.Drawing.Size(140, 44);
            this.cancelLayoutItem.MinSize = new System.Drawing.Size(140, 44);
            this.cancelLayoutItem.Name = "cancelLayoutItem";
            this.cancelLayoutItem.Size = new System.Drawing.Size(140, 44);
            this.cancelLayoutItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.cancelLayoutItem.TextSize = new System.Drawing.Size(0, 0);
            this.cancelLayoutItem.TextVisible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(860, 350);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 40);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            // 
            // frmCarryBagSelection
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1010, 404);
            this.Controls.Add(this.layoutControl);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(770, 280);
            this.Name = "frmCarryBagSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Carry Bags";
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayoutItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectLayoutItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cancelLayoutItem)).EndInit();
            this.ResumeLayout(false);

        }

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}
