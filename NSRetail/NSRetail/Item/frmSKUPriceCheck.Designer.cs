namespace NSRetail
{
    partial class frmSKUPriceCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSKUPriceCheck));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.cmbBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.sluItem = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sluItemView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcItemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnLookup = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.gcPrices = new DevExpress.XtraGrid.GridControl();
            this.gvPrices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMRP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOfferCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFreeQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOfferValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBasis = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutbtnLookup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutbtnClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutgcPrices = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutlblStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.sluItemfdsa = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutgcPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutlblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemfdsa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.cmbBranch);
            this.layoutControl.Controls.Add(this.sluItem);
            this.layoutControl.Controls.Add(this.btnLookup);
            this.layoutControl.Controls.Add(this.btnClose);
            this.layoutControl.Controls.Add(this.lblStatus);
            this.layoutControl.Controls.Add(this.gcPrices);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1493, 785);
            this.layoutControl.TabIndex = 0;
            // 
            // cmbBranch
            // 
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(890, 16);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHID", "Name1", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "Name2")});
            this.cmbBranch.Properties.NullText = "";
            this.cmbBranch.Properties.ShowFooter = false;
            this.cmbBranch.Properties.ShowHeader = false;
            this.cmbBranch.Size = new System.Drawing.Size(249, 22);
            this.cmbBranch.StyleController = this.layoutControl;
            this.cmbBranch.TabIndex = 1;
            // 
            // sluItem
            // 
            this.sluItem.EnterMoveNextControl = true;
            this.sluItem.Location = new System.Drawing.Point(113, 16);
            this.sluItem.Name = "sluItem";
            this.sluItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sluItem.Properties.NullText = "";
            this.sluItem.Properties.PopupView = this.sluItemView;
            this.sluItem.Size = new System.Drawing.Size(668, 22);
            this.sluItem.StyleController = this.layoutControl;
            this.sluItem.TabIndex = 0;
            // 
            // sluItemView
            // 
            this.sluItemView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcItemID,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.sluItemView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sluItemView.Name = "sluItemView";
            this.sluItemView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sluItemView.OptionsView.ShowGroupPanel = false;
            // 
            // gcItemID
            // 
            this.gcItemID.Caption = "ITEMID";
            this.gcItemID.FieldName = "ITEMID";
            this.gcItemID.Name = "gcItemID";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "SKU Code";
            this.gridColumn2.FieldName = "SKUCODE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Item Name";
            this.gridColumn3.FieldName = "ITEMNAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Item Code";
            this.gridColumn4.FieldName = "ITEMCODE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // btnLookup
            // 
            this.btnLookup.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnLookup.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLookup.ImageOptions.SvgImage")));
            this.btnLookup.Location = new System.Drawing.Point(1147, 12);
            this.btnLookup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(163, 31);
            this.btnLookup.StyleController = this.layoutControl;
            this.btnLookup.TabIndex = 2;
            this.btnLookup.Text = "Check Price";
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnClose.ImageOptions.SvgImage")));
            this.btnClose.Location = new System.Drawing.Point(1314, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(167, 31);
            this.btnClose.StyleController = this.layoutControl;
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close (Esc)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatus.Location = new System.Drawing.Point(12, 749);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1469, 24);
            this.lblStatus.StyleController = this.layoutControl;
            this.lblStatus.TabIndex = 5;
            // 
            // gcPrices
            // 
            this.gcPrices.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcPrices.Location = new System.Drawing.Point(12, 47);
            this.gcPrices.MainView = this.gvPrices;
            this.gcPrices.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcPrices.Name = "gcPrices";
            this.gcPrices.Size = new System.Drawing.Size(1469, 698);
            this.gcPrices.TabIndex = 4;
            this.gcPrices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPrices});
            // 
            // gvPrices
            // 
            this.gvPrices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMRP,
            this.colSalePrice,
            this.colKind,
            this.colOfferCode,
            this.colName,
            this.colType,
            this.colBuyQuantity,
            this.colFreeQuantity,
            this.colOfferValue,
            this.colFinalPrice,
            this.colBasis,
            this.colDealPrice,
            this.colStart,
            this.colEnd});
            this.gvPrices.DetailHeight = 404;
            this.gvPrices.GridControl = this.gcPrices;
            this.gvPrices.GroupCount = 1;
            this.gvPrices.GroupFormat = "{0}: {1}";
            this.gvPrices.Name = "gvPrices";
            this.gvPrices.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvPrices.OptionsBehavior.AutoPopulateColumns = false;
            this.gvPrices.OptionsBehavior.Editable = false;
            this.gvPrices.OptionsEditForm.PopupEditFormWidth = 933;
            this.gvPrices.OptionsView.ShowGroupPanel = false;
            this.gvPrices.OptionsView.ShowIndicator = false;
            this.gvPrices.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colMRP, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colMRP
            // 
            this.colMRP.Caption = "MRP";
            this.colMRP.DisplayFormat.FormatString = "n2";
            this.colMRP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMRP.FieldName = "MRP";
            this.colMRP.Name = "colMRP";
            // 
            // colSalePrice
            // 
            this.colSalePrice.Caption = "Sale Price";
            this.colSalePrice.DisplayFormat.FormatString = "n2";
            this.colSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSalePrice.FieldName = "SALEPRICE";
            this.colSalePrice.Name = "colSalePrice";
            this.colSalePrice.Visible = true;
            this.colSalePrice.VisibleIndex = 0;
            // 
            // colKind
            // 
            this.colKind.Caption = "Kind";
            this.colKind.FieldName = "OFFERKIND";
            this.colKind.Name = "colKind";
            this.colKind.Visible = true;
            this.colKind.VisibleIndex = 1;
            // 
            // colOfferCode
            // 
            this.colOfferCode.Caption = "Offer Code";
            this.colOfferCode.FieldName = "OFFERCODE";
            this.colOfferCode.Name = "colOfferCode";
            this.colOfferCode.Visible = true;
            this.colOfferCode.VisibleIndex = 2;
            // 
            // colName
            // 
            this.colName.Caption = "Offer Name";
            this.colName.FieldName = "OFFERNAME";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 3;
            // 
            // colType
            // 
            this.colType.Caption = "Offer Type";
            this.colType.FieldName = "OFFERTYPE";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 4;
            // 
            // colBuyQuantity
            // 
            this.colBuyQuantity.Caption = "Buy Quantity";
            this.colBuyQuantity.DisplayFormat.FormatString = "n0";
            this.colBuyQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyQuantity.FieldName = "BUYQUANTITY";
            this.colBuyQuantity.Name = "colBuyQuantity";
            this.colBuyQuantity.Visible = true;
            this.colBuyQuantity.VisibleIndex = 5;
            // 
            // colFreeQuantity
            // 
            this.colFreeQuantity.Caption = "Free Quantity";
            this.colFreeQuantity.DisplayFormat.FormatString = "n0";
            this.colFreeQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFreeQuantity.FieldName = "FREEQUANTITY";
            this.colFreeQuantity.Name = "colFreeQuantity";
            this.colFreeQuantity.Visible = true;
            this.colFreeQuantity.VisibleIndex = 6;
            // 
            // colOfferValue
            // 
            this.colOfferValue.Caption = "Offer Value";
            this.colOfferValue.FieldName = "OFFERVALUE";
            this.colOfferValue.Name = "colOfferValue";
            this.colOfferValue.Visible = true;
            this.colOfferValue.VisibleIndex = 11;
            // 
            // colFinalPrice
            // 
            this.colFinalPrice.Caption = "Price After Offer";
            this.colFinalPrice.DisplayFormat.FormatString = "n2";
            this.colFinalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinalPrice.FieldName = "PRICEAFTEROFFER";
            this.colFinalPrice.Name = "colFinalPrice";
            this.colFinalPrice.Visible = true;
            this.colFinalPrice.VisibleIndex = 7;
            // 
            // colBasis
            // 
            this.colBasis.Caption = "Price Based On";
            this.colBasis.FieldName = "PRICEBASEDON";
            this.colBasis.Name = "colBasis";
            this.colBasis.Visible = true;
            this.colBasis.VisibleIndex = 8;
            // 
            // colDealPrice
            // 
            this.colDealPrice.Caption = "Configured Deal Price";
            this.colDealPrice.DisplayFormat.FormatString = "n2";
            this.colDealPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealPrice.FieldName = "OFFERPRICE";
            this.colDealPrice.Name = "colDealPrice";
            this.colDealPrice.Visible = true;
            this.colDealPrice.VisibleIndex = 9;
            // 
            // colStart
            // 
            this.colStart.Caption = "Start Date";
            this.colStart.DisplayFormat.FormatString = "g";
            this.colStart.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colStart.FieldName = "STARTDATE";
            this.colStart.Name = "colStart";
            this.colStart.Visible = true;
            this.colStart.VisibleIndex = 10;
            // 
            // colEnd
            // 
            this.colEnd.Caption = "End Date";
            this.colEnd.DisplayFormat.FormatString = "g";
            this.colEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colEnd.FieldName = "ENDDATE";
            this.colEnd.Name = "colEnd";
            this.colEnd.Visible = true;
            this.colEnd.VisibleIndex = 12;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutbtnLookup,
            this.layoutbtnClose,
            this.layoutgcPrices,
            this.layoutlblStatus,
            this.sluItemfdsa,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1493, 785);
            this.Root.TextVisible = false;
            // 
            // layoutbtnLookup
            // 
            this.layoutbtnLookup.Control = this.btnLookup;
            this.layoutbtnLookup.Location = new System.Drawing.Point(1135, 0);
            this.layoutbtnLookup.MaxSize = new System.Drawing.Size(167, 35);
            this.layoutbtnLookup.MinSize = new System.Drawing.Size(167, 35);
            this.layoutbtnLookup.Name = "layoutbtnLookup";
            this.layoutbtnLookup.Size = new System.Drawing.Size(167, 35);
            this.layoutbtnLookup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutbtnLookup.TextSize = new System.Drawing.Size(0, 0);
            this.layoutbtnLookup.TextVisible = false;
            // 
            // layoutbtnClose
            // 
            this.layoutbtnClose.Control = this.btnClose;
            this.layoutbtnClose.Location = new System.Drawing.Point(1302, 0);
            this.layoutbtnClose.MaxSize = new System.Drawing.Size(171, 35);
            this.layoutbtnClose.MinSize = new System.Drawing.Size(171, 35);
            this.layoutbtnClose.Name = "layoutbtnClose";
            this.layoutbtnClose.Size = new System.Drawing.Size(171, 35);
            this.layoutbtnClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutbtnClose.TextSize = new System.Drawing.Size(0, 0);
            this.layoutbtnClose.TextVisible = false;
            // 
            // layoutgcPrices
            // 
            this.layoutgcPrices.Control = this.gcPrices;
            this.layoutgcPrices.Location = new System.Drawing.Point(0, 35);
            this.layoutgcPrices.Name = "layoutgcPrices";
            this.layoutgcPrices.Size = new System.Drawing.Size(1473, 702);
            this.layoutgcPrices.TextSize = new System.Drawing.Size(0, 0);
            this.layoutgcPrices.TextVisible = false;
            // 
            // layoutlblStatus
            // 
            this.layoutlblStatus.Control = this.lblStatus;
            this.layoutlblStatus.Location = new System.Drawing.Point(0, 737);
            this.layoutlblStatus.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutlblStatus.MinSize = new System.Drawing.Size(117, 28);
            this.layoutlblStatus.Name = "layoutlblStatus";
            this.layoutlblStatus.Size = new System.Drawing.Size(1473, 28);
            this.layoutlblStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutlblStatus.TextSize = new System.Drawing.Size(0, 0);
            this.layoutlblStatus.TextVisible = false;
            // 
            // sluItemfdsa
            // 
            this.sluItemfdsa.Control = this.sluItem;
            this.sluItemfdsa.Location = new System.Drawing.Point(0, 0);
            this.sluItemfdsa.Name = "sluItemfdsa";
            this.sluItemfdsa.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.sluItemfdsa.Size = new System.Drawing.Size(777, 35);
            this.sluItemfdsa.Text = "SKU/Item Code";
            this.sluItemfdsa.TextSize = new System.Drawing.Size(85, 15);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbBranch;
            this.layoutControlItem1.Location = new System.Drawing.Point(777, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(358, 35);
            this.layoutControlItem1.Text = "Branch";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(85, 15);
            // 
            // frmSKUPriceCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 785);
            this.Controls.Add(this.layoutControl);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "frmSKUPriceCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SKU Price Check";
            this.Load += new System.EventHandler(this.frmSKUPriceCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutgcPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutlblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemfdsa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcPrices;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPrices;
        private DevExpress.XtraEditors.SimpleButton btnLookup;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutbtnLookup;
        private DevExpress.XtraLayout.LayoutControlItem layoutbtnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutgcPrices;
        private DevExpress.XtraLayout.LayoutControlItem layoutlblStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colMRP;
        private DevExpress.XtraGrid.Columns.GridColumn colSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colKind;
        private DevExpress.XtraGrid.Columns.GridColumn colOfferCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colFreeQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colFinalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colOfferValue;
        private DevExpress.XtraGrid.Columns.GridColumn colBasis;
        private DevExpress.XtraGrid.Columns.GridColumn colDealPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colStart;
        private DevExpress.XtraGrid.Columns.GridColumn colEnd;
        private DevExpress.XtraEditors.SearchLookUpEdit sluItem;
        private DevExpress.XtraGrid.Views.Grid.GridView sluItemView;
        private DevExpress.XtraLayout.LayoutControlItem sluItemfdsa;
        private DevExpress.XtraEditors.LookUpEdit cmbBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}
