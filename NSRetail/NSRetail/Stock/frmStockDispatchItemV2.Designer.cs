namespace NSRetail.Stock
{
    partial class frmStockDispatchItemV2
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView cmbLookupView;
        private DevExpress.XtraGrid.Columns.GridColumn colLookupSKUCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLookupItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLookupItemName;
        private DevExpress.XtraEditors.TextEdit txtItemName;
        private DevExpress.XtraEditors.LookUpEdit cmbTrayNumber;
        private DevExpress.XtraEditors.SimpleButton btnAddTray;
        private DevExpress.XtraEditors.SimpleButton btnDeleteTray;
        private DevExpress.XtraEditors.TextEdit txtMRP;
        private DevExpress.XtraEditors.TextEdit txtSalePrice;
        private DevExpress.XtraEditors.TextEdit txtQuantity;
        private DevExpress.XtraEditors.TextEdit txtWeightInKgs;
        private DevExpress.XtraEditors.TextEdit txtWarehouseStock;
        private DevExpress.XtraEditors.TextEdit txtBranchStock;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup groupItemDetails;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutMRP;
        private DevExpress.XtraLayout.LayoutControlItem layoutSalePrice;
        private DevExpress.XtraLayout.LayoutControlItem layoutBranchStock;
        private DevExpress.XtraLayout.LayoutControlItem layoutQuantity;
        private DevExpress.XtraLayout.LayoutControlItem layoutWeight;
        private DevExpress.XtraLayout.LayoutControlItem layoutWarehouseStock;
        private DevExpress.XtraLayout.EmptySpaceItem emptyButtons;
        private DevExpress.XtraLayout.LayoutControlItem layoutCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockDispatchItemV2));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbItemCode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cmbLookupView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLookupSKUCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLookupItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLookupItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtItemName = new DevExpress.XtraEditors.TextEdit();
            this.cmbTrayNumber = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAddTray = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteTray = new DevExpress.XtraEditors.SimpleButton();
            this.txtMRP = new DevExpress.XtraEditors.TextEdit();
            this.txtSalePrice = new DevExpress.XtraEditors.TextEdit();
            this.txtBranchStock = new DevExpress.XtraEditors.TextEdit();
            this.txtQuantity = new DevExpress.XtraEditors.TextEdit();
            this.txtWeightInKgs = new DevExpress.XtraEditors.TextEdit();
            this.txtWarehouseStock = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.groupItemDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutMRP = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSalePrice = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutQuantity = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutWeight = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBranchStock = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutWarehouseStock = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptyButtons = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutTrayNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAddTray = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDeleteTray = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLookupView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrayNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMRP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalePrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranchStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKgs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupItemDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMRP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSalePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBranchStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWarehouseStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTrayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAddTray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDeleteTray)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbItemCode);
            this.layoutControl1.Controls.Add(this.txtItemName);
            this.layoutControl1.Controls.Add(this.cmbTrayNumber);
            this.layoutControl1.Controls.Add(this.btnAddTray);
            this.layoutControl1.Controls.Add(this.btnDeleteTray);
            this.layoutControl1.Controls.Add(this.txtMRP);
            this.layoutControl1.Controls.Add(this.txtSalePrice);
            this.layoutControl1.Controls.Add(this.txtBranchStock);
            this.layoutControl1.Controls.Add(this.txtQuantity);
            this.layoutControl1.Controls.Add(this.txtWeightInKgs);
            this.layoutControl1.Controls.Add(this.txtWarehouseStock);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(796, 77, 650, 400);
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(748, 318);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbItemCode
            // 
            this.cmbItemCode.EnterMoveNextControl = true;
            this.cmbItemCode.Location = new System.Drawing.Point(134, 130);
            this.cmbItemCode.Name = "cmbItemCode";
            this.cmbItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbItemCode.Properties.NullText = "";
            this.cmbItemCode.Properties.PopupView = this.cmbLookupView;
            this.cmbItemCode.Size = new System.Drawing.Size(225, 22);
            this.cmbItemCode.StyleController = this.layoutControl1;
            this.cmbItemCode.TabIndex = 1;
            this.cmbItemCode.EditValueChanged += new System.EventHandler(this.cmbItemCode_EditValueChanged);
            // 
            // cmbLookupView
            // 
            this.cmbLookupView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLookupSKUCode,
            this.colLookupItemCode,
            this.colLookupItemName});
            this.cmbLookupView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cmbLookupView.Name = "cmbLookupView";
            this.cmbLookupView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cmbLookupView.OptionsView.ShowGroupPanel = false;
            // 
            // colLookupSKUCode
            // 
            this.colLookupSKUCode.Caption = "SKU/Item Code";
            this.colLookupSKUCode.FieldName = "SKUCODE";
            this.colLookupSKUCode.Name = "colLookupSKUCode";
            this.colLookupSKUCode.Visible = true;
            this.colLookupSKUCode.VisibleIndex = 0;
            this.colLookupSKUCode.Width = 120;
            // 
            // colLookupItemCode
            // 
            this.colLookupItemCode.Caption = "EAN Code";
            this.colLookupItemCode.FieldName = "ITEMCODE";
            this.colLookupItemCode.Name = "colLookupItemCode";
            this.colLookupItemCode.Visible = true;
            this.colLookupItemCode.VisibleIndex = 1;
            this.colLookupItemCode.Width = 130;
            // 
            // colLookupItemName
            // 
            this.colLookupItemName.Caption = "Item Name";
            this.colLookupItemName.FieldName = "ITEMNAME";
            this.colLookupItemName.Name = "colLookupItemName";
            this.colLookupItemName.Visible = true;
            this.colLookupItemName.VisibleIndex = 2;
            this.colLookupItemName.Width = 260;
            // 
            // txtItemName
            // 
            this.txtItemName.EnterMoveNextControl = true;
            this.txtItemName.Location = new System.Drawing.Point(481, 130);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Properties.ReadOnly = true;
            this.txtItemName.Size = new System.Drawing.Size(243, 22);
            this.txtItemName.StyleController = this.layoutControl1;
            this.txtItemName.TabIndex = 5;
            // 
            // cmbTrayNumber
            // 
            this.cmbTrayNumber.EnterMoveNextControl = true;
            this.cmbTrayNumber.Location = new System.Drawing.Point(138, 52);
            this.cmbTrayNumber.Name = "cmbTrayNumber";
            this.cmbTrayNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTrayNumber.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TRAYNUMBER", "Tray Number")});
            this.cmbTrayNumber.Properties.NullText = "";
            this.cmbTrayNumber.Properties.ShowHeader = false;
            this.cmbTrayNumber.Size = new System.Drawing.Size(334, 22);
            this.cmbTrayNumber.StyleController = this.layoutControl1;
            this.cmbTrayNumber.TabIndex = 0;
            // 
            // btnAddTray
            // 
            this.btnAddTray.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAddTray.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddTray.ImageOptions.SvgImage")));
            this.btnAddTray.Location = new System.Drawing.Point(480, 45);
            this.btnAddTray.Name = "btnAddTray";
            this.btnAddTray.Size = new System.Drawing.Size(120, 36);
            this.btnAddTray.StyleController = this.layoutControl1;
            this.btnAddTray.TabIndex = 11;
            this.btnAddTray.Text = "Add Tray";
            this.btnAddTray.ToolTip = "Add tray";
            this.btnAddTray.Click += new System.EventHandler(this.btnAddTray_Click);
            // 
            // btnDeleteTray
            // 
            this.btnDeleteTray.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnDeleteTray.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteTray.ImageOptions.SvgImage")));
            this.btnDeleteTray.Location = new System.Drawing.Point(604, 45);
            this.btnDeleteTray.Name = "btnDeleteTray";
            this.btnDeleteTray.Size = new System.Drawing.Size(120, 36);
            this.btnDeleteTray.StyleController = this.layoutControl1;
            this.btnDeleteTray.TabIndex = 12;
            this.btnDeleteTray.Text = "Delete Tray";
            this.btnDeleteTray.ToolTip = "Delete selected tray";
            this.btnDeleteTray.Click += new System.EventHandler(this.btnDeleteTray_Click);
            // 
            // txtMRP
            // 
            this.txtMRP.EnterMoveNextControl = true;
            this.txtMRP.Location = new System.Drawing.Point(134, 164);
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Properties.ReadOnly = true;
            this.txtMRP.Size = new System.Drawing.Size(225, 22);
            this.txtMRP.StyleController = this.layoutControl1;
            this.txtMRP.TabIndex = 6;
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.EnterMoveNextControl = true;
            this.txtSalePrice.Location = new System.Drawing.Point(481, 164);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Properties.ReadOnly = true;
            this.txtSalePrice.Size = new System.Drawing.Size(243, 22);
            this.txtSalePrice.StyleController = this.layoutControl1;
            this.txtSalePrice.TabIndex = 7;
            // 
            // txtBranchStock
            // 
            this.txtBranchStock.EnterMoveNextControl = true;
            this.txtBranchStock.Location = new System.Drawing.Point(134, 198);
            this.txtBranchStock.Name = "txtBranchStock";
            this.txtBranchStock.Properties.ReadOnly = true;
            this.txtBranchStock.Size = new System.Drawing.Size(225, 22);
            this.txtBranchStock.StyleController = this.layoutControl1;
            this.txtBranchStock.TabIndex = 8;
            // 
            // txtQuantity
            // 
            this.txtQuantity.EnterMoveNextControl = true;
            this.txtQuantity.Location = new System.Drawing.Point(134, 232);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtQuantity.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtQuantity.Properties.MaskSettings.Set("mask", "n0");
            this.txtQuantity.Size = new System.Drawing.Size(225, 22);
            this.txtQuantity.StyleController = this.layoutControl1;
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.EditValueChanged += new System.EventHandler(this.txtQuantity_EditValueChanged);
            // 
            // txtWeightInKgs
            // 
            this.txtWeightInKgs.EnterMoveNextControl = true;
            this.txtWeightInKgs.Location = new System.Drawing.Point(481, 232);
            this.txtWeightInKgs.Name = "txtWeightInKgs";
            this.txtWeightInKgs.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtWeightInKgs.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtWeightInKgs.Properties.MaskSettings.Set("mask", "n3");
            this.txtWeightInKgs.Size = new System.Drawing.Size(243, 22);
            this.txtWeightInKgs.StyleController = this.layoutControl1;
            this.txtWeightInKgs.TabIndex = 3;
            // 
            // txtWarehouseStock
            // 
            this.txtWarehouseStock.EnterMoveNextControl = true;
            this.txtWarehouseStock.Location = new System.Drawing.Point(481, 198);
            this.txtWarehouseStock.Name = "txtWarehouseStock";
            this.txtWarehouseStock.Properties.ReadOnly = true;
            this.txtWarehouseStock.Size = new System.Drawing.Size(243, 22);
            this.txtWarehouseStock.StyleController = this.layoutControl1;
            this.txtWarehouseStock.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Location = new System.Drawing.Point(616, 270);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 36);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Add Item";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(492, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Close";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.groupItemDetails,
            this.emptyButtons,
            this.layoutCancel,
            this.layoutSave,
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(748, 318);
            this.Root.TextVisible = false;
            // 
            // groupItemDetails
            // 
            this.groupItemDetails.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupItemDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemCode,
            this.layoutItemName,
            this.layoutMRP,
            this.layoutSalePrice,
            this.layoutQuantity,
            this.layoutWeight,
            this.layoutBranchStock,
            this.layoutWarehouseStock});
            this.groupItemDetails.Location = new System.Drawing.Point(0, 85);
            this.groupItemDetails.Name = "groupItemDetails";
            this.groupItemDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.groupItemDetails.Size = new System.Drawing.Size(728, 173);
            this.groupItemDetails.Text = "Item Details";
            // 
            // layoutItemCode
            // 
            this.layoutItemCode.Control = this.cmbItemCode;
            this.layoutItemCode.Location = new System.Drawing.Point(0, 0);
            this.layoutItemCode.Name = "layoutItemCode";
            this.layoutItemCode.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutItemCode.Size = new System.Drawing.Size(347, 34);
            this.layoutItemCode.Text = "EAN Code/Name";
            this.layoutItemCode.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutItemName
            // 
            this.layoutItemName.Control = this.txtItemName;
            this.layoutItemName.Location = new System.Drawing.Point(347, 0);
            this.layoutItemName.Name = "layoutItemName";
            this.layoutItemName.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutItemName.Size = new System.Drawing.Size(365, 34);
            this.layoutItemName.Text = "Item Name";
            this.layoutItemName.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutMRP
            // 
            this.layoutMRP.Control = this.txtMRP;
            this.layoutMRP.Location = new System.Drawing.Point(0, 34);
            this.layoutMRP.Name = "layoutMRP";
            this.layoutMRP.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutMRP.Size = new System.Drawing.Size(347, 34);
            this.layoutMRP.Text = "MRP";
            this.layoutMRP.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutSalePrice
            // 
            this.layoutSalePrice.Control = this.txtSalePrice;
            this.layoutSalePrice.Location = new System.Drawing.Point(347, 34);
            this.layoutSalePrice.Name = "layoutSalePrice";
            this.layoutSalePrice.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutSalePrice.Size = new System.Drawing.Size(365, 34);
            this.layoutSalePrice.Text = "Sale Price";
            this.layoutSalePrice.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutQuantity
            // 
            this.layoutQuantity.Control = this.txtQuantity;
            this.layoutQuantity.Location = new System.Drawing.Point(0, 102);
            this.layoutQuantity.Name = "layoutQuantity";
            this.layoutQuantity.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutQuantity.Size = new System.Drawing.Size(347, 34);
            this.layoutQuantity.Text = "Quantity";
            this.layoutQuantity.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutWeight
            // 
            this.layoutWeight.Control = this.txtWeightInKgs;
            this.layoutWeight.Location = new System.Drawing.Point(347, 102);
            this.layoutWeight.Name = "layoutWeight";
            this.layoutWeight.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutWeight.Size = new System.Drawing.Size(365, 34);
            this.layoutWeight.Text = "Weight In KGs";
            this.layoutWeight.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutBranchStock
            // 
            this.layoutBranchStock.Control = this.txtBranchStock;
            this.layoutBranchStock.Location = new System.Drawing.Point(0, 68);
            this.layoutBranchStock.Name = "layoutBranchStock";
            this.layoutBranchStock.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutBranchStock.Size = new System.Drawing.Size(347, 34);
            this.layoutBranchStock.Text = "Branch Stock";
            this.layoutBranchStock.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutWarehouseStock
            // 
            this.layoutWarehouseStock.Control = this.txtWarehouseStock;
            this.layoutWarehouseStock.Location = new System.Drawing.Point(347, 68);
            this.layoutWarehouseStock.Name = "layoutWarehouseStock";
            this.layoutWarehouseStock.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutWarehouseStock.Size = new System.Drawing.Size(365, 34);
            this.layoutWarehouseStock.Text = "Warehouse Stock";
            this.layoutWarehouseStock.TextSize = new System.Drawing.Size(98, 15);
            // 
            // emptyButtons
            // 
            this.emptyButtons.AllowHotTrack = false;
            this.emptyButtons.Location = new System.Drawing.Point(0, 258);
            this.emptyButtons.Name = "emptyButtons";
            this.emptyButtons.Size = new System.Drawing.Size(480, 40);
            this.emptyButtons.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutCancel
            // 
            this.layoutCancel.Control = this.btnCancel;
            this.layoutCancel.Location = new System.Drawing.Point(480, 258);
            this.layoutCancel.Name = "layoutCancel";
            this.layoutCancel.Size = new System.Drawing.Size(124, 40);
            this.layoutCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutCancel.TextVisible = false;
            // 
            // layoutSave
            // 
            this.layoutSave.Control = this.btnSave;
            this.layoutSave.Location = new System.Drawing.Point(604, 258);
            this.layoutSave.Name = "layoutSave";
            this.layoutSave.Size = new System.Drawing.Size(124, 40);
            this.layoutSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutSave.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutTrayNumber,
            this.layoutAddTray,
            this.layoutDeleteTray});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(728, 85);
            this.layoutControlGroup1.Text = "Tray Information";
            // 
            // layoutTrayNumber
            // 
            this.layoutTrayNumber.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutTrayNumber.Control = this.cmbTrayNumber;
            this.layoutTrayNumber.Location = new System.Drawing.Point(0, 0);
            this.layoutTrayNumber.Name = "layoutTrayNumber";
            this.layoutTrayNumber.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutTrayNumber.Size = new System.Drawing.Size(456, 40);
            this.layoutTrayNumber.Text = "Tray Number";
            this.layoutTrayNumber.TextSize = new System.Drawing.Size(98, 15);
            // 
            // layoutAddTray
            // 
            this.layoutAddTray.Control = this.btnAddTray;
            this.layoutAddTray.Location = new System.Drawing.Point(456, 0);
            this.layoutAddTray.Name = "layoutAddTray";
            this.layoutAddTray.Size = new System.Drawing.Size(124, 40);
            this.layoutAddTray.TextSize = new System.Drawing.Size(0, 0);
            this.layoutAddTray.TextVisible = false;
            // 
            // layoutDeleteTray
            // 
            this.layoutDeleteTray.Control = this.btnDeleteTray;
            this.layoutDeleteTray.Location = new System.Drawing.Point(580, 0);
            this.layoutDeleteTray.Name = "layoutDeleteTray";
            this.layoutDeleteTray.Size = new System.Drawing.Size(124, 40);
            this.layoutDeleteTray.TextSize = new System.Drawing.Size(0, 0);
            this.layoutDeleteTray.TextVisible = false;
            // 
            // frmStockDispatchItemV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(748, 318);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockDispatchItemV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispatch Item Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStockDispatchItemV2_FormClosed);
            this.Load += new System.EventHandler(this.frmStockDispatchItemV2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLookupView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrayNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMRP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalePrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranchStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKgs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupItemDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMRP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSalePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBranchStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWarehouseStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTrayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAddTray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDeleteTray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControlItem layoutTrayNumber;
        private DevExpress.XtraLayout.LayoutControlItem layoutAddTray;
        private DevExpress.XtraLayout.LayoutControlItem layoutDeleteTray;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    }
}
