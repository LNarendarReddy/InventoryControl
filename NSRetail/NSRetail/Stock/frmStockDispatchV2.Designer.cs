namespace NSRetail.Stock
{
    partial class frmStockDispatchV2
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl lblFromBranchValue;
        private DevExpress.XtraEditors.LabelControl lblToBranchValue;
        private DevExpress.XtraEditors.LabelControl lblCategoryValue;
        private DevExpress.XtraEditors.SimpleButton btnDispatchInfo;
        private DevExpress.XtraEditors.SimpleButton btnAddItem;
        private DevExpress.XtraEditors.SimpleButton btnDispatch;
        private DevExpress.XtraEditors.SimpleButton btnDiscard;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gcDispatch;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDispatch;
        private DevExpress.XtraGrid.Columns.GridColumn colSKUCode;
        private DevExpress.XtraGrid.Columns.GridColumn colItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colMRP;
        private DevExpress.XtraGrid.Columns.GridColumn colSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colTrayNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup groupDispatchInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutFromBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutToBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutDispatchInfoButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutAddItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutDispatch;
        private DevExpress.XtraLayout.LayoutControlItem layoutDiscard;
        private DevExpress.XtraLayout.LayoutControlItem layoutClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptyCommands;
        private DevExpress.XtraLayout.LayoutControlItem layoutGrid;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockDispatchV2));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblFromBranchValue = new DevExpress.XtraEditors.LabelControl();
            this.lblToBranchValue = new DevExpress.XtraEditors.LabelControl();
            this.lblCategoryValue = new DevExpress.XtraEditors.LabelControl();
            this.btnDispatchInfo = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnDispatch = new DevExpress.XtraEditors.SimpleButton();
            this.btnDiscard = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.gcDispatch = new DevExpress.XtraGrid.GridControl();
            this.gvDispatch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSKUCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMRP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrayNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.groupDispatchInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutFromBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutToBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCategory = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDispatchInfoButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAddItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDispatch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDiscard = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptyCommands = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDispatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupDispatchInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFromBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutToBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDispatchInfoButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAddItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDispatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDiscard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblFromBranchValue);
            this.layoutControl1.Controls.Add(this.lblToBranchValue);
            this.layoutControl1.Controls.Add(this.lblCategoryValue);
            this.layoutControl1.Controls.Add(this.btnDispatchInfo);
            this.layoutControl1.Controls.Add(this.btnAddItem);
            this.layoutControl1.Controls.Add(this.btnDispatch);
            this.layoutControl1.Controls.Add(this.btnDiscard);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.gcDispatch);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1150, 720);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblFromBranchValue
            // 
            this.lblFromBranchValue.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblFromBranchValue.Appearance.Options.UseFont = true;
            this.lblFromBranchValue.Appearance.Options.UseTextOptions = true;
            this.lblFromBranchValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblFromBranchValue.AutoEllipsis = true;
            this.lblFromBranchValue.Location = new System.Drawing.Point(131, 46);
            this.lblFromBranchValue.Name = "lblFromBranchValue";
            this.lblFromBranchValue.Size = new System.Drawing.Size(251, 16);
            this.lblFromBranchValue.StyleController = this.layoutControl1;
            this.lblFromBranchValue.TabIndex = 0;
            // 
            // lblToBranchValue
            // 
            this.lblToBranchValue.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblToBranchValue.Appearance.Options.UseFont = true;
            this.lblToBranchValue.Appearance.Options.UseTextOptions = true;
            this.lblToBranchValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblToBranchValue.AutoEllipsis = true;
            this.lblToBranchValue.Location = new System.Drawing.Point(500, 46);
            this.lblToBranchValue.Name = "lblToBranchValue";
            this.lblToBranchValue.Size = new System.Drawing.Size(250, 16);
            this.lblToBranchValue.StyleController = this.layoutControl1;
            this.lblToBranchValue.TabIndex = 1;
            // 
            // lblCategoryValue
            // 
            this.lblCategoryValue.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblCategoryValue.Appearance.Options.UseFont = true;
            this.lblCategoryValue.Appearance.Options.UseTextOptions = true;
            this.lblCategoryValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblCategoryValue.AutoEllipsis = true;
            this.lblCategoryValue.Location = new System.Drawing.Point(868, 46);
            this.lblCategoryValue.Name = "lblCategoryValue";
            this.lblCategoryValue.Size = new System.Drawing.Size(257, 16);
            this.lblCategoryValue.StyleController = this.layoutControl1;
            this.lblCategoryValue.TabIndex = 2;
            // 
            // btnDispatchInfo
            // 
            this.btnDispatchInfo.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnDispatchInfo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDispatchInfo.ImageOptions.SvgImage")));
            this.btnDispatchInfo.Location = new System.Drawing.Point(391, 79);
            this.btnDispatchInfo.Name = "btnDispatchInfo";
            this.btnDispatchInfo.Size = new System.Drawing.Size(142, 36);
            this.btnDispatchInfo.StyleController = this.layoutControl1;
            this.btnDispatchInfo.TabIndex = 3;
            this.btnDispatchInfo.Text = "Dispatch Info";
            this.btnDispatchInfo.Click += new System.EventHandler(this.btnDispatchInfo_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAddItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddItem.ImageOptions.SvgImage")));
            this.btnAddItem.Location = new System.Drawing.Point(537, 79);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(142, 36);
            this.btnAddItem.StyleController = this.layoutControl1;
            this.btnAddItem.TabIndex = 4;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnDispatch
            // 
            this.btnDispatch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnDispatch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDispatch.ImageOptions.SvgImage")));
            this.btnDispatch.Location = new System.Drawing.Point(683, 79);
            this.btnDispatch.Name = "btnDispatch";
            this.btnDispatch.Size = new System.Drawing.Size(150, 36);
            this.btnDispatch.StyleController = this.layoutControl1;
            this.btnDispatch.TabIndex = 5;
            this.btnDispatch.Text = "Dispatch";
            this.btnDispatch.Click += new System.EventHandler(this.btnDispatch_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnDiscard.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDiscard.ImageOptions.SvgImage")));
            this.btnDiscard.Location = new System.Drawing.Point(837, 79);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(155, 36);
            this.btnDiscard.StyleController = this.layoutControl1;
            this.btnDiscard.TabIndex = 6;
            this.btnDiscard.Text = "Discard Dispatch";
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnClose.ImageOptions.SvgImage")));
            this.btnClose.Location = new System.Drawing.Point(996, 79);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(142, 36);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gcDispatch
            // 
            this.gcDispatch.Location = new System.Drawing.Point(12, 119);
            this.gcDispatch.MainView = this.gvDispatch;
            this.gcDispatch.Name = "gcDispatch";
            this.gcDispatch.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete});
            this.gcDispatch.Size = new System.Drawing.Size(1126, 589);
            this.gcDispatch.TabIndex = 8;
            this.gcDispatch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDispatch});
            // 
            // gvDispatch
            // 
            this.gvDispatch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSKUCode,
            this.colItemCode,
            this.colItemName,
            this.colMRP,
            this.colSalePrice,
            this.colQuantity,
            this.colWeight,
            this.colTrayNumber,
            this.colDelete});
            this.gvDispatch.GridControl = this.gcDispatch;
            this.gvDispatch.Name = "gvDispatch";
            this.gvDispatch.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDispatch.OptionsView.ShowFooter = true;
            this.gvDispatch.OptionsView.ShowGroupPanel = false;
            // 
            // colSKUCode
            // 
            this.colSKUCode.Caption = "SKU/Item Code";
            this.colSKUCode.FieldName = "SKUCODE";
            this.colSKUCode.Name = "colSKUCode";
            this.colSKUCode.OptionsColumn.AllowEdit = false;
            this.colSKUCode.Visible = true;
            this.colSKUCode.VisibleIndex = 0;
            this.colSKUCode.Width = 120;
            // 
            // colItemCode
            // 
            this.colItemCode.Caption = "EAN Code";
            this.colItemCode.FieldName = "ITEMCODE";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.OptionsColumn.AllowEdit = false;
            this.colItemCode.Visible = true;
            this.colItemCode.VisibleIndex = 1;
            this.colItemCode.Width = 130;
            // 
            // colItemName
            // 
            this.colItemName.Caption = "Item Name";
            this.colItemName.FieldName = "ITEMNAME";
            this.colItemName.Name = "colItemName";
            this.colItemName.OptionsColumn.AllowEdit = false;
            this.colItemName.Visible = true;
            this.colItemName.VisibleIndex = 2;
            this.colItemName.Width = 260;
            // 
            // colMRP
            // 
            this.colMRP.Caption = "MRP";
            this.colMRP.DisplayFormat.FormatString = "n2";
            this.colMRP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMRP.FieldName = "MRP";
            this.colMRP.Name = "colMRP";
            this.colMRP.OptionsColumn.AllowEdit = false;
            this.colMRP.Visible = true;
            this.colMRP.VisibleIndex = 3;
            this.colMRP.Width = 90;
            // 
            // colSalePrice
            // 
            this.colSalePrice.Caption = "Sale Price";
            this.colSalePrice.DisplayFormat.FormatString = "n2";
            this.colSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSalePrice.FieldName = "SALEPRICE";
            this.colSalePrice.Name = "colSalePrice";
            this.colSalePrice.OptionsColumn.AllowEdit = false;
            this.colSalePrice.Visible = true;
            this.colSalePrice.VisibleIndex = 4;
            this.colSalePrice.Width = 90;
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.DisplayFormat.FormatString = "n0";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "DISPATCHQUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 5;
            this.colQuantity.Width = 90;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "Weight in Kgs";
            this.colWeight.DisplayFormat.FormatString = "n3";
            this.colWeight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWeight.FieldName = "WEIGHTINKGS";
            this.colWeight.Name = "colWeight";
            this.colWeight.OptionsColumn.AllowEdit = false;
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 6;
            this.colWeight.Width = 110;
            // 
            // colTrayNumber
            // 
            this.colTrayNumber.Caption = "Tray #";
            this.colTrayNumber.FieldName = "TRAYNUMBER";
            this.colTrayNumber.Name = "colTrayNumber";
            this.colTrayNumber.OptionsColumn.AllowEdit = false;
            this.colTrayNumber.Visible = true;
            this.colTrayNumber.VisibleIndex = 7;
            this.colTrayNumber.Width = 110;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.btnDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 8;
            this.colDelete.Width = 70;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDelete_ButtonClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.groupDispatchInfo,
            this.layoutDispatchInfoButton,
            this.layoutAddItem,
            this.layoutDispatch,
            this.layoutDiscard,
            this.layoutClose,
            this.layoutGrid,
            this.emptyCommands});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1150, 720);
            this.Root.TextVisible = false;
            // 
            // groupDispatchInfo
            // 
            this.groupDispatchInfo.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.groupDispatchInfo.AppearanceItemCaption.Options.UseFont = true;
            this.groupDispatchInfo.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupDispatchInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutFromBranch,
            this.layoutToBranch,
            this.layoutCategory});
            this.groupDispatchInfo.Location = new System.Drawing.Point(0, 0);
            this.groupDispatchInfo.Name = "groupDispatchInfo";
            this.groupDispatchInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.groupDispatchInfo.Size = new System.Drawing.Size(1130, 67);
            this.groupDispatchInfo.Text = "Dispatch Information";
            // 
            // layoutFromBranch
            // 
            this.layoutFromBranch.Control = this.lblFromBranchValue;
            this.layoutFromBranch.Location = new System.Drawing.Point(0, 0);
            this.layoutFromBranch.Name = "layoutFromBranch";
            this.layoutFromBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutFromBranch.Size = new System.Drawing.Size(369, 28);
            this.layoutFromBranch.Text = "From Branch : ";
            this.layoutFromBranch.TextSize = new System.Drawing.Size(94, 16);
            // 
            // layoutToBranch
            // 
            this.layoutToBranch.Control = this.lblToBranchValue;
            this.layoutToBranch.Location = new System.Drawing.Point(369, 0);
            this.layoutToBranch.Name = "layoutToBranch";
            this.layoutToBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutToBranch.Size = new System.Drawing.Size(368, 28);
            this.layoutToBranch.Text = "To Branch : ";
            this.layoutToBranch.TextSize = new System.Drawing.Size(94, 16);
            // 
            // layoutCategory
            // 
            this.layoutCategory.Control = this.lblCategoryValue;
            this.layoutCategory.Location = new System.Drawing.Point(737, 0);
            this.layoutCategory.Name = "layoutCategory";
            this.layoutCategory.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutCategory.Size = new System.Drawing.Size(375, 28);
            this.layoutCategory.Text = "Category : ";
            this.layoutCategory.TextSize = new System.Drawing.Size(94, 16);
            // 
            // layoutDispatchInfoButton
            // 
            this.layoutDispatchInfoButton.Control = this.btnDispatchInfo;
            this.layoutDispatchInfoButton.Location = new System.Drawing.Point(379, 67);
            this.layoutDispatchInfoButton.MaxSize = new System.Drawing.Size(146, 40);
            this.layoutDispatchInfoButton.MinSize = new System.Drawing.Size(146, 40);
            this.layoutDispatchInfoButton.Name = "layoutDispatchInfoButton";
            this.layoutDispatchInfoButton.Size = new System.Drawing.Size(146, 40);
            this.layoutDispatchInfoButton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutDispatchInfoButton.TextSize = new System.Drawing.Size(0, 0);
            this.layoutDispatchInfoButton.TextVisible = false;
            // 
            // layoutAddItem
            // 
            this.layoutAddItem.Control = this.btnAddItem;
            this.layoutAddItem.Location = new System.Drawing.Point(525, 67);
            this.layoutAddItem.MaxSize = new System.Drawing.Size(146, 40);
            this.layoutAddItem.MinSize = new System.Drawing.Size(146, 40);
            this.layoutAddItem.Name = "layoutAddItem";
            this.layoutAddItem.Size = new System.Drawing.Size(146, 40);
            this.layoutAddItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutAddItem.TextSize = new System.Drawing.Size(0, 0);
            this.layoutAddItem.TextVisible = false;
            // 
            // layoutDispatch
            // 
            this.layoutDispatch.Control = this.btnDispatch;
            this.layoutDispatch.Location = new System.Drawing.Point(671, 67);
            this.layoutDispatch.MaxSize = new System.Drawing.Size(154, 40);
            this.layoutDispatch.MinSize = new System.Drawing.Size(154, 40);
            this.layoutDispatch.Name = "layoutDispatch";
            this.layoutDispatch.Size = new System.Drawing.Size(154, 40);
            this.layoutDispatch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutDispatch.TextSize = new System.Drawing.Size(0, 0);
            this.layoutDispatch.TextVisible = false;
            // 
            // layoutDiscard
            // 
            this.layoutDiscard.Control = this.btnDiscard;
            this.layoutDiscard.Location = new System.Drawing.Point(825, 67);
            this.layoutDiscard.MaxSize = new System.Drawing.Size(159, 40);
            this.layoutDiscard.MinSize = new System.Drawing.Size(159, 40);
            this.layoutDiscard.Name = "layoutDiscard";
            this.layoutDiscard.Size = new System.Drawing.Size(159, 40);
            this.layoutDiscard.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutDiscard.TextSize = new System.Drawing.Size(0, 0);
            this.layoutDiscard.TextVisible = false;
            // 
            // layoutClose
            // 
            this.layoutClose.Control = this.btnClose;
            this.layoutClose.Location = new System.Drawing.Point(984, 67);
            this.layoutClose.MaxSize = new System.Drawing.Size(146, 40);
            this.layoutClose.MinSize = new System.Drawing.Size(146, 40);
            this.layoutClose.Name = "layoutClose";
            this.layoutClose.Size = new System.Drawing.Size(146, 40);
            this.layoutClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutClose.TextSize = new System.Drawing.Size(0, 0);
            this.layoutClose.TextVisible = false;
            // 
            // layoutGrid
            // 
            this.layoutGrid.Control = this.gcDispatch;
            this.layoutGrid.Location = new System.Drawing.Point(0, 107);
            this.layoutGrid.Name = "layoutGrid";
            this.layoutGrid.Size = new System.Drawing.Size(1130, 593);
            this.layoutGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutGrid.TextVisible = false;
            // 
            // emptyCommands
            // 
            this.emptyCommands.AllowHotTrack = false;
            this.emptyCommands.Location = new System.Drawing.Point(0, 67);
            this.emptyCommands.Name = "emptyCommands";
            this.emptyCommands.Size = new System.Drawing.Size(379, 40);
            this.emptyCommands.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmStockDispatchV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 720);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmStockDispatchV2";
            this.Text = "Stock Dispatch New";
            this.Load += new System.EventHandler(this.frmStockDispatchV2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDispatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupDispatchInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFromBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutToBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDispatchInfoButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAddItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDispatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDiscard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyCommands)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
