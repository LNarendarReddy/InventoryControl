namespace NSRetail
{
    partial class frmItemComposition
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.txtDestination = new DevExpress.XtraEditors.TextEdit();
            this.sluIngredient = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gvIngredientLookup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lookupSKU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookupUOM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.gcIngredients = new DevExpress.XtraGrid.GridControl();
            this.gvIngredients = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSKU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUOM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeleted = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblHelp = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.destinationItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.ingredientItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.addItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.removeItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.helpItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.buttonSpace = new DevExpress.XtraLayout.EmptySpaceItem();
            this.saveItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.cancelItem = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluIngredient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIngredientLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcIngredients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIngredients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.helpItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cancelItem)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.txtDestination);
            this.layoutControl.Controls.Add(this.sluIngredient);
            this.layoutControl.Controls.Add(this.btnAdd);
            this.layoutControl.Controls.Add(this.btnRemove);
            this.layoutControl.Controls.Add(this.gcIngredients);
            this.layoutControl.Controls.Add(this.lblHelp);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(933, 577);
            this.layoutControl.TabIndex = 0;
            // 
            // txtDestination
            // 
            this.txtDestination.EnterMoveNextControl = true;
            this.txtDestination.Location = new System.Drawing.Point(119, 14);
            this.txtDestination.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Properties.ReadOnly = true;
            this.txtDestination.Size = new System.Drawing.Size(800, 22);
            this.txtDestination.StyleController = this.layoutControl;
            this.txtDestination.TabIndex = 0;
            // 
            // sluIngredient
            // 
            this.sluIngredient.EnterMoveNextControl = true;
            this.sluIngredient.Location = new System.Drawing.Point(119, 40);
            this.sluIngredient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sluIngredient.Name = "sluIngredient";
            this.sluIngredient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sluIngredient.Properties.NullText = "Select ingredient SKU";
            this.sluIngredient.Properties.PopupView = this.gvIngredientLookup;
            this.sluIngredient.Size = new System.Drawing.Size(474, 22);
            this.sluIngredient.StyleController = this.layoutControl;
            this.sluIngredient.TabIndex = 1;
            // 
            // gvIngredientLookup
            // 
            this.gvIngredientLookup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.lookupSKU,
            this.lookupName,
            this.lookupUOM});
            this.gvIngredientLookup.DetailHeight = 404;
            this.gvIngredientLookup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvIngredientLookup.Name = "gvIngredientLookup";
            this.gvIngredientLookup.OptionsEditForm.PopupEditFormWidth = 933;
            this.gvIngredientLookup.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvIngredientLookup.OptionsView.ShowGroupPanel = false;
            // 
            // lookupSKU
            // 
            this.lookupSKU.Caption = "SKU";
            this.lookupSKU.FieldName = "SKUCODE";
            this.lookupSKU.MinWidth = 23;
            this.lookupSKU.Name = "lookupSKU";
            this.lookupSKU.Visible = true;
            this.lookupSKU.VisibleIndex = 0;
            this.lookupSKU.Width = 87;
            // 
            // lookupName
            // 
            this.lookupName.Caption = "Item Name";
            this.lookupName.FieldName = "ITEMNAME";
            this.lookupName.MinWidth = 23;
            this.lookupName.Name = "lookupName";
            this.lookupName.Visible = true;
            this.lookupName.VisibleIndex = 1;
            this.lookupName.Width = 408;
            // 
            // lookupUOM
            // 
            this.lookupUOM.Caption = "UOM";
            this.lookupUOM.FieldName = "UOM";
            this.lookupUOM.MinWidth = 23;
            this.lookupUOM.Name = "lookupUOM";
            this.lookupUOM.Visible = true;
            this.lookupUOM.VisibleIndex = 2;
            this.lookupUOM.Width = 87;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(597, 40);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(159, 22);
            this.btnAdd.StyleController = this.layoutControl;
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Tag = "9C5BB57E-65F6-44AA-9531-55E82D29C086::Update";
            this.btnAdd.Text = "&Add Ingredient";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(760, 40);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(159, 22);
            this.btnRemove.StyleController = this.layoutControl;
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Tag = "9C5BB57E-65F6-44AA-9531-55E82D29C086::Update";
            this.btnRemove.Text = "&Remove Selected";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // gcIngredients
            // 
            this.gcIngredients.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcIngredients.Location = new System.Drawing.Point(14, 66);
            this.gcIngredients.MainView = this.gvIngredients;
            this.gcIngredients.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcIngredients.Name = "gcIngredients";
            this.gcIngredients.Size = new System.Drawing.Size(905, 452);
            this.gcIngredients.TabIndex = 4;
            this.gcIngredients.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIngredients});
            // 
            // gvIngredients
            // 
            this.gvIngredients.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSKU,
            this.colName,
            this.colUOM,
            this.colDeleted});
            this.gvIngredients.DetailHeight = 404;
            this.gvIngredients.GridControl = this.gcIngredients;
            this.gvIngredients.Name = "gvIngredients";
            this.gvIngredients.OptionsBehavior.Editable = false;
            this.gvIngredients.OptionsEditForm.PopupEditFormWidth = 933;
            this.gvIngredients.OptionsView.ShowGroupPanel = false;
            this.gvIngredients.OptionsView.ShowIndicator = false;
            // 
            // colSKU
            // 
            this.colSKU.Caption = "Ingredient SKU";
            this.colSKU.FieldName = "SKUCODE";
            this.colSKU.MinWidth = 23;
            this.colSKU.Name = "colSKU";
            this.colSKU.Visible = true;
            this.colSKU.VisibleIndex = 0;
            this.colSKU.Width = 140;
            // 
            // colName
            // 
            this.colName.Caption = "Item Name";
            this.colName.FieldName = "ITEMNAME";
            this.colName.MinWidth = 23;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 513;
            // 
            // colUOM
            // 
            this.colUOM.Caption = "UOM";
            this.colUOM.FieldName = "UOM";
            this.colUOM.MinWidth = 23;
            this.colUOM.Name = "colUOM";
            this.colUOM.Visible = true;
            this.colUOM.VisibleIndex = 2;
            this.colUOM.Width = 87;
            // 
            // colDeleted
            // 
            this.colDeleted.Caption = "Deleted Item";
            this.colDeleted.FieldName = "ISDELETED";
            this.colDeleted.MinWidth = 23;
            this.colDeleted.Name = "colDeleted";
            this.colDeleted.Visible = false;
            this.colDeleted.Width = 87;
            // 
            // lblHelp
            // 
            this.lblHelp.Location = new System.Drawing.Point(14, 522);
            this.lblHelp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(414, 15);
            this.lblHelp.StyleController = this.layoutControl;
            this.lblHelp.TabIndex = 5;
            this.lblHelp.Text = "Map ingredient SKUs here. Weights will be entered during stock conversion.";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(643, 541);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 22);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 6;
            this.btnSave.Tag = "9C5BB57E-65F6-44AA-9531-55E82D29C086::Update";
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(783, 541);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 22);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.destinationItem,
            this.ingredientItem,
            this.addItem,
            this.removeItem,
            this.gridItem,
            this.helpItem,
            this.buttonSpace,
            this.saveItem,
            this.cancelItem});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(933, 577);
            this.Root.TextVisible = false;
            // 
            // destinationItem
            // 
            this.destinationItem.Control = this.txtDestination;
            this.destinationItem.Location = new System.Drawing.Point(0, 0);
            this.destinationItem.Name = "destinationItem";
            this.destinationItem.Size = new System.Drawing.Size(909, 26);
            this.destinationItem.Text = "Destination SKU";
            this.destinationItem.TextSize = new System.Drawing.Size(91, 15);
            // 
            // ingredientItem
            // 
            this.ingredientItem.Control = this.sluIngredient;
            this.ingredientItem.Location = new System.Drawing.Point(0, 26);
            this.ingredientItem.Name = "ingredientItem";
            this.ingredientItem.Size = new System.Drawing.Size(583, 26);
            this.ingredientItem.Text = "Ingredient SKU";
            this.ingredientItem.TextSize = new System.Drawing.Size(91, 15);
            // 
            // addItem
            // 
            this.addItem.Control = this.btnAdd;
            this.addItem.Location = new System.Drawing.Point(583, 26);
            this.addItem.Name = "addItem";
            this.addItem.Size = new System.Drawing.Size(163, 26);
            this.addItem.TextSize = new System.Drawing.Size(0, 0);
            this.addItem.TextVisible = false;
            // 
            // removeItem
            // 
            this.removeItem.Control = this.btnRemove;
            this.removeItem.Location = new System.Drawing.Point(746, 26);
            this.removeItem.Name = "removeItem";
            this.removeItem.Size = new System.Drawing.Size(163, 26);
            this.removeItem.TextSize = new System.Drawing.Size(0, 0);
            this.removeItem.TextVisible = false;
            // 
            // gridItem
            // 
            this.gridItem.Control = this.gcIngredients;
            this.gridItem.Location = new System.Drawing.Point(0, 52);
            this.gridItem.Name = "gridItem";
            this.gridItem.Size = new System.Drawing.Size(909, 456);
            this.gridItem.TextSize = new System.Drawing.Size(0, 0);
            this.gridItem.TextVisible = false;
            // 
            // helpItem
            // 
            this.helpItem.Control = this.lblHelp;
            this.helpItem.Location = new System.Drawing.Point(0, 508);
            this.helpItem.Name = "helpItem";
            this.helpItem.Size = new System.Drawing.Size(909, 19);
            this.helpItem.TextSize = new System.Drawing.Size(0, 0);
            this.helpItem.TextVisible = false;
            // 
            // buttonSpace
            // 
            this.buttonSpace.AllowHotTrack = false;
            this.buttonSpace.Location = new System.Drawing.Point(0, 527);
            this.buttonSpace.Name = "buttonSpace";
            this.buttonSpace.Size = new System.Drawing.Size(629, 26);
            this.buttonSpace.TextSize = new System.Drawing.Size(0, 0);
            // 
            // saveItem
            // 
            this.saveItem.Control = this.btnSave;
            this.saveItem.Location = new System.Drawing.Point(629, 527);
            this.saveItem.Name = "saveItem";
            this.saveItem.Size = new System.Drawing.Size(140, 26);
            this.saveItem.TextSize = new System.Drawing.Size(0, 0);
            this.saveItem.TextVisible = false;
            // 
            // cancelItem
            // 
            this.cancelItem.Control = this.btnCancel;
            this.cancelItem.Location = new System.Drawing.Point(769, 527);
            this.cancelItem.Name = "cancelItem";
            this.cancelItem.Size = new System.Drawing.Size(140, 26);
            this.cancelItem.TextSize = new System.Drawing.Size(0, 0);
            this.cancelItem.TextVisible = false;
            // 
            // frmItemComposition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(933, 577);
            this.Controls.Add(this.layoutControl);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(760, 440);
            this.Name = "frmItemComposition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Composition Setup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmItemComposition_FormClosing);
            this.Load += new System.EventHandler(this.frmItemComposition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDestination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluIngredient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIngredientLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcIngredients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIngredients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.helpItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cancelItem)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtDestination;
        private DevExpress.XtraEditors.SearchLookUpEdit sluIngredient;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIngredientLookup;
        private DevExpress.XtraGrid.Columns.GridColumn lookupSKU, lookupName, lookupUOM;
        private DevExpress.XtraEditors.SimpleButton btnAdd, btnRemove, btnSave, btnCancel;
        private DevExpress.XtraEditors.LabelControl lblHelp;
        private DevExpress.XtraGrid.GridControl gcIngredients;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIngredients;
        private DevExpress.XtraGrid.Columns.GridColumn colSKU, colName, colUOM, colDeleted;
        private DevExpress.XtraLayout.LayoutControlItem destinationItem, ingredientItem, addItem, removeItem;
        private DevExpress.XtraLayout.LayoutControlItem gridItem, helpItem, saveItem, cancelItem;
        private DevExpress.XtraLayout.EmptySpaceItem buttonSpace;
    }
}
