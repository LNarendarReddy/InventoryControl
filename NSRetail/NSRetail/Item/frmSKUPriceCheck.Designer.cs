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
            this.txtScanCode = new DevExpress.XtraEditors.TextEdit();
            this.cmbBranch = new DevExpress.XtraEditors.LookUpEdit();
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
            this.layouttxtScanCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutcmbBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutbtnLookup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutbtnClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutgcPrices = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutlblStatus = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScanCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layouttxtScanCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutcmbBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutgcPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutlblStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.txtScanCode);
            this.layoutControl.Controls.Add(this.cmbBranch);
            this.layoutControl.Controls.Add(this.btnLookup);
            this.layoutControl.Controls.Add(this.btnClose);
            this.layoutControl.Controls.Add(this.lblStatus);
            this.layoutControl.Controls.Add(this.gcPrices);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1493, 785);
            this.layoutControl.TabIndex = 0;
            // 
            // txtScanCode
            // 
            this.txtScanCode.Location = new System.Drawing.Point(113, 16);
            this.txtScanCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Properties.MaxLength = 100;
            this.txtScanCode.Size = new System.Drawing.Size(511, 22);
            this.txtScanCode.StyleController = this.layoutControl;
            this.txtScanCode.TabIndex = 0;
            this.txtScanCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanCode_KeyDown);
            // 
            // cmbBranch
            // 
            this.cmbBranch.Location = new System.Drawing.Point(733, 16);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "Branch", 23, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cmbBranch.Properties.NullText = "Select branch";
            this.cmbBranch.Size = new System.Drawing.Size(370, 22);
            this.cmbBranch.StyleController = this.layoutControl;
            this.cmbBranch.TabIndex = 1;
            this.cmbBranch.EditValueChanged += new System.EventHandler(this.cmbBranch_EditValueChanged);
            // 
            // btnLookup
            // 
            this.btnLookup.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnLookup.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLookup.ImageOptions.SvgImage")));
            this.btnLookup.Location = new System.Drawing.Point(1111, 12);
            this.btnLookup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(182, 31);
            this.btnLookup.StyleController = this.layoutControl;
            this.btnLookup.TabIndex = 2;
            this.btnLookup.Text = "Check Price";
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnClose.ImageOptions.SvgImage")));
            this.btnClose.Location = new System.Drawing.Point(1297, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(184, 31);
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
            this.lblStatus.TabIndex = 4;
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
            this.gvPrices.OptionsView.ColumnAutoWidth = true;
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
            this.layouttxtScanCode,
            this.layoutcmbBranch,
            this.layoutbtnLookup,
            this.layoutbtnClose,
            this.layoutgcPrices,
            this.layoutlblStatus});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1493, 785);
            this.Root.TextVisible = false;
            // 
            // layouttxtScanCode
            // 
            this.layouttxtScanCode.Control = this.txtScanCode;
            this.layouttxtScanCode.Location = new System.Drawing.Point(0, 0);
            this.layouttxtScanCode.Name = "layouttxtScanCode";
            this.layouttxtScanCode.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layouttxtScanCode.Size = new System.Drawing.Size(620, 35);
            this.layouttxtScanCode.Text = "SKU / Itemcode";
            this.layouttxtScanCode.TextSize = new System.Drawing.Size(85, 15);
            // 
            // layoutcmbBranch
            // 
            this.layoutcmbBranch.Control = this.cmbBranch;
            this.layoutcmbBranch.Location = new System.Drawing.Point(620, 0);
            this.layoutcmbBranch.Name = "layoutcmbBranch";
            this.layoutcmbBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutcmbBranch.Size = new System.Drawing.Size(479, 35);
            this.layoutcmbBranch.Text = "Branch";
            this.layoutcmbBranch.TextSize = new System.Drawing.Size(85, 15);
            // 
            // layoutbtnLookup
            // 
            this.layoutbtnLookup.Control = this.btnLookup;
            this.layoutbtnLookup.Location = new System.Drawing.Point(1099, 0);
            this.layoutbtnLookup.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutbtnLookup.MinSize = new System.Drawing.Size(117, 35);
            this.layoutbtnLookup.Name = "layoutbtnLookup";
            this.layoutbtnLookup.Size = new System.Drawing.Size(186, 35);
            this.layoutbtnLookup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutbtnLookup.TextSize = new System.Drawing.Size(0, 0);
            this.layoutbtnLookup.TextVisible = false;
            // 
            // layoutbtnClose
            // 
            this.layoutbtnClose.Control = this.btnClose;
            this.layoutbtnClose.Location = new System.Drawing.Point(1285, 0);
            this.layoutbtnClose.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutbtnClose.MinSize = new System.Drawing.Size(117, 35);
            this.layoutbtnClose.Name = "layoutbtnClose";
            this.layoutbtnClose.Size = new System.Drawing.Size(188, 35);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtScanCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layouttxtScanCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutcmbBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutbtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutgcPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutlblStatus)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcPrices;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPrices;
        private DevExpress.XtraEditors.TextEdit txtScanCode;
        private DevExpress.XtraEditors.LookUpEdit cmbBranch;
        private DevExpress.XtraEditors.SimpleButton btnLookup;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraLayout.LayoutControlItem layouttxtScanCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutcmbBranch;
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
    }
}
