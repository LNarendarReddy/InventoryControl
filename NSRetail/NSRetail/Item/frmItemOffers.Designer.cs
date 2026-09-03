namespace NSRetail
{
    partial class frmItemOffers
    {
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.gcOffers = new DevExpress.XtraGrid.GridControl();
            this.gvOffers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHSNCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMRP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOfferKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOfferCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOfferName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOfferType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBaseOfferCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBaseOfferName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOfferItemType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNumberOfPieces = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPriceBasedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOfferPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bbiPreview = new DevExpress.XtraBars.BarButtonItem();
            this.pmOffers = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcOffers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOffers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmOffers)).BeginInit();
            this.SuspendLayout();
            // 
            // gcOffers
            // 
            this.gcOffers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOffers.Location = new System.Drawing.Point(0, 0);
            this.gcOffers.MainView = this.gvOffers;
            this.gcOffers.Name = "gcOffers";
            this.gcOffers.Size = new System.Drawing.Size(1184, 561);
            this.gcOffers.TabIndex = 0;
            this.gcOffers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOffers});
            // 
            // gvOffers
            // 
            this.gvOffers.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gvOffers.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvOffers.Appearance.Row.Font = new System.Drawing.Font("Arial", 8F);
            this.gvOffers.Appearance.Row.Options.UseFont = true;
            this.gvOffers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcItemCode,
            this.gcItemName,
            this.gcHSNCode,
            this.gcMRP,
            this.gcSalePrice,
            this.gcOfferKind,
            this.gcOfferCode,
            this.gcOfferName,
            this.gcOfferType,
            this.gcStartDate,
            this.gcEndDate,
            this.gcBaseOfferCode,
            this.gcBaseOfferName,
            this.gcOfferItemType,
            this.gcNumberOfPieces,
            this.gcPriceBasedOn,
            this.gcOfferPrice});
            this.gvOffers.GridControl = this.gcOffers;
            this.gvOffers.Name = "gvOffers";
            this.gvOffers.OptionsBehavior.Editable = false;
            this.gvOffers.OptionsView.ShowGroupPanel = false;
            this.gvOffers.OptionsView.ShowIndicator = false;
            this.gvOffers.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvOffers_PopupMenuShowing);
            // 
            // gcItemCode
            // 
            this.gcItemCode.Caption = "EAN Code";
            this.gcItemCode.FieldName = "ITEMCODE";
            this.gcItemCode.Name = "gcItemCode";
            this.gcItemCode.Visible = true;
            this.gcItemCode.VisibleIndex = 0;
            // 
            // gcItemName
            // 
            this.gcItemName.Caption = "Item Name";
            this.gcItemName.FieldName = "ITEMNAME";
            this.gcItemName.Name = "gcItemName";
            this.gcItemName.Visible = true;
            this.gcItemName.VisibleIndex = 1;
            // 
            // gcHSNCode
            // 
            this.gcHSNCode.Caption = "HSN Code";
            this.gcHSNCode.FieldName = "HSNCODE";
            this.gcHSNCode.Name = "gcHSNCode";
            // 
            // gcMRP
            // 
            this.gcMRP.Caption = "MRP";
            this.gcMRP.DisplayFormat.FormatString = "n2";
            this.gcMRP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcMRP.FieldName = "MRP";
            this.gcMRP.Name = "gcMRP";
            this.gcMRP.Visible = true;
            this.gcMRP.VisibleIndex = 3;
            // 
            // gcSalePrice
            // 
            this.gcSalePrice.Caption = "Sale Price";
            this.gcSalePrice.DisplayFormat.FormatString = "n2";
            this.gcSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcSalePrice.FieldName = "SALEPRICE";
            this.gcSalePrice.Name = "gcSalePrice";
            // 
            // gcOfferKind
            // 
            this.gcOfferKind.Caption = "Type";
            this.gcOfferKind.FieldName = "OFFERKIND";
            this.gcOfferKind.Name = "gcOfferKind";
            // 
            // gcOfferCode
            // 
            this.gcOfferCode.Caption = "Offer Code";
            this.gcOfferCode.FieldName = "OFFERCODE";
            this.gcOfferCode.Name = "gcOfferCode";
            this.gcOfferCode.Visible = true;
            this.gcOfferCode.VisibleIndex = 6;
            // 
            // gcOfferName
            // 
            this.gcOfferName.Caption = "Offer Name";
            this.gcOfferName.FieldName = "OFFERNAME";
            this.gcOfferName.Name = "gcOfferName";
            this.gcOfferName.Visible = true;
            this.gcOfferName.VisibleIndex = 7;
            // 
            // gcOfferType
            // 
            this.gcOfferType.Caption = "Offer Type";
            this.gcOfferType.FieldName = "OFFERTYPE";
            this.gcOfferType.Name = "gcOfferType";
            this.gcOfferType.Visible = true;
            this.gcOfferType.VisibleIndex = 8;
            // 
            // gcStartDate
            // 
            this.gcStartDate.Caption = "Start Date";
            this.gcStartDate.DisplayFormat.FormatString = "dd-MM-yyyy";
            this.gcStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcStartDate.FieldName = "STARTDATE";
            this.gcStartDate.Name = "gcStartDate";
            this.gcStartDate.Visible = true;
            this.gcStartDate.VisibleIndex = 9;
            // 
            // gcEndDate
            // 
            this.gcEndDate.Caption = "End Date";
            this.gcEndDate.DisplayFormat.FormatString = "dd-MM-yyyy";
            this.gcEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcEndDate.FieldName = "ENDDATE";
            this.gcEndDate.Name = "gcEndDate";
            this.gcEndDate.Visible = true;
            this.gcEndDate.VisibleIndex = 10;
            // 
            // gcBaseOfferCode
            // 
            this.gcBaseOfferCode.Caption = "Base Offer Code";
            this.gcBaseOfferCode.FieldName = "BASEOFFERCODE";
            this.gcBaseOfferCode.Name = "gcBaseOfferCode";
            // 
            // gcBaseOfferName
            // 
            this.gcBaseOfferName.Caption = "Base Offer Name";
            this.gcBaseOfferName.FieldName = "BASEOFFERNAME";
            this.gcBaseOfferName.Name = "gcBaseOfferName";
            this.gcBaseOfferName.Visible = true;
            this.gcBaseOfferName.VisibleIndex = 11;
            // 
            // gcOfferItemType
            // 
            this.gcOfferItemType.Caption = "Offer Item";
            this.gcOfferItemType.FieldName = "OFFERITEMTYPE";
            this.gcOfferItemType.Name = "gcOfferItemType";
            this.gcOfferItemType.Visible = true;
            this.gcOfferItemType.VisibleIndex = 12;
            // 
            // gcNumberOfPieces
            // 
            this.gcNumberOfPieces.Caption = "Offer Threshold";
            this.gcNumberOfPieces.FieldName = "NUMBEROFPIECES";
            this.gcNumberOfPieces.Name = "gcNumberOfPieces";
            // 
            // gcPriceBasedOn
            // 
            this.gcPriceBasedOn.Caption = "Price Based On";
            this.gcPriceBasedOn.FieldName = "PRICEBASEDONTEXT";
            this.gcPriceBasedOn.Name = "gcPriceBasedOn";
            this.gcPriceBasedOn.Visible = true;
            this.gcPriceBasedOn.VisibleIndex = 14;
            // 
            // gcOfferPrice
            // 
            this.gcOfferPrice.Caption = "Offer Price";
            this.gcOfferPrice.DisplayFormat.FormatString = "n2";
            this.gcOfferPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcOfferPrice.FieldName = "OFFERPRICE";
            this.gcOfferPrice.Name = "gcOfferPrice";
            this.gcOfferPrice.Visible = true;
            this.gcOfferPrice.VisibleIndex = 15;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiPreview});
            this.barManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1184, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 561);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1184, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 561);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1184, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 561);
            // 
            // bbiPreview
            // 
            this.bbiPreview.Caption = "Preview";
            this.bbiPreview.Id = 0;
            this.bbiPreview.Name = "bbiPreview";
            this.bbiPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPreview_ItemClick);
            // 
            // pmOffers
            // 
            this.pmOffers.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPreview)});
            this.pmOffers.Manager = this.barManager1;
            this.pmOffers.Name = "pmOffers";
            // 
            // frmItemOffers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.gcOffers);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmItemOffers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Offers";
            this.Load += new System.EventHandler(this.frmItemOffers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcOffers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOffers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmOffers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcOffers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOffers;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gcHSNCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcMRP;
        private DevExpress.XtraGrid.Columns.GridColumn gcSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcOfferKind;
        private DevExpress.XtraGrid.Columns.GridColumn gcOfferCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcOfferName;
        private DevExpress.XtraGrid.Columns.GridColumn gcOfferType;
        private DevExpress.XtraGrid.Columns.GridColumn gcStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcBaseOfferCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcBaseOfferName;
        private DevExpress.XtraGrid.Columns.GridColumn gcOfferItemType;
        private DevExpress.XtraGrid.Columns.GridColumn gcNumberOfPieces;
        private DevExpress.XtraGrid.Columns.GridColumn gcPriceBasedOn;
        private DevExpress.XtraGrid.Columns.GridColumn gcOfferPrice;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiPreview;
        private DevExpress.XtraBars.PopupMenu pmOffers;
    }
}
