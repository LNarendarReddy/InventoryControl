namespace NSRetailPOS.UI
{
    partial class frmStockInDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockInDetail));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddStock = new DevExpress.XtraEditors.SimpleButton();
            this.gcDispatchDetail = new DevExpress.XtraGrid.GridControl();
            this.gvDispatchDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtApprovedDate = new DevExpress.XtraEditors.DateTimeOffsetEdit();
            this.txtApprovedBy = new DevExpress.XtraEditors.TextEdit();
            this.txtDispatchNumber = new DevExpress.XtraEditors.TextEdit();
            this.dtCreatedDate = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDispatchDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispatchDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtApprovedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApprovedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDispatchNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreatedDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreatedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.Control.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.Control.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.ControlDisabled.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseFont = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseFont = true;
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnAddStock);
            this.layoutControl1.Controls.Add(this.gcDispatchDetail);
            this.layoutControl1.Controls.Add(this.dtApprovedDate);
            this.layoutControl1.Controls.Add(this.txtApprovedBy);
            this.layoutControl1.Controls.Add(this.txtDispatchNumber);
            this.layoutControl1.Controls.Add(this.dtCreatedDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1149, 599);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.Location = new System.Drawing.Point(1044, 570);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Cancel";
            // 
            // btnAddStock
            // 
            this.btnAddStock.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddStock.ImageOptions.Image")));
            this.btnAddStock.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAddStock.Location = new System.Drawing.Point(918, 570);
            this.btnAddStock.Name = "btnAddStock";
            this.btnAddStock.Size = new System.Drawing.Size(116, 22);
            this.btnAddStock.StyleController = this.layoutControl1;
            this.btnAddStock.TabIndex = 9;
            this.btnAddStock.Text = "Accept";
            this.btnAddStock.Click += new System.EventHandler(this.btnAddStock_Click);
            // 
            // gcDispatchDetail
            // 
            this.gcDispatchDetail.Location = new System.Drawing.Point(4, 68);
            this.gcDispatchDetail.MainView = this.gvDispatchDetail;
            this.gcDispatchDetail.Name = "gcDispatchDetail";
            this.gcDispatchDetail.Size = new System.Drawing.Size(1141, 495);
            this.gcDispatchDetail.TabIndex = 8;
            this.gcDispatchDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDispatchDetail});
            // 
            // gvDispatchDetail
            // 
            this.gvDispatchDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gvDispatchDetail.GridControl = this.gcDispatchDetail;
            this.gvDispatchDetail.Name = "gvDispatchDetail";
            this.gvDispatchDetail.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "STOCKDISPATCHDETAILID";
            this.gridColumn1.FieldName = "STOCKDISPATCHDETAILID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Item Name";
            this.gridColumn2.FieldName = "ITEMNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 200;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Item Code";
            this.gridColumn3.FieldName = "ITEMCODE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 99;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "MRP";
            this.gridColumn4.FieldName = "MRP";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 62;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Sale Price";
            this.gridColumn5.FieldName = "SALEPRICE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 62;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "GST Code";
            this.gridColumn6.FieldName = "GSTCODE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 62;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Tray Number";
            this.gridColumn7.FieldName = "TRAYNUMBER";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 62;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Dispatch Quantity";
            this.gridColumn8.FieldName = "DISPATCHQUANTITY";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 62;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Received Quantity";
            this.gridColumn9.FieldName = "RECEIVEDQUANTITY";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            this.gridColumn9.Width = 76;
            // 
            // dtApprovedDate
            // 
            this.dtApprovedDate.EditValue = null;
            this.dtApprovedDate.Enabled = false;
            this.dtApprovedDate.Location = new System.Drawing.Point(692, 39);
            this.dtApprovedDate.Name = "dtApprovedDate";
            this.dtApprovedDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtApprovedDate.Size = new System.Drawing.Size(450, 22);
            this.dtApprovedDate.StyleController = this.layoutControl1;
            this.dtApprovedDate.TabIndex = 7;
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.Enabled = false;
            this.txtApprovedBy.Location = new System.Drawing.Point(120, 39);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.Size = new System.Drawing.Size(449, 22);
            this.txtApprovedBy.StyleController = this.layoutControl1;
            this.txtApprovedBy.TabIndex = 6;
            // 
            // txtDispatchNumber
            // 
            this.txtDispatchNumber.Enabled = false;
            this.txtDispatchNumber.Location = new System.Drawing.Point(120, 7);
            this.txtDispatchNumber.Name = "txtDispatchNumber";
            this.txtDispatchNumber.Size = new System.Drawing.Size(449, 22);
            this.txtDispatchNumber.StyleController = this.layoutControl1;
            this.txtDispatchNumber.TabIndex = 4;
            // 
            // dtCreatedDate
            // 
            this.dtCreatedDate.EditValue = null;
            this.dtCreatedDate.Enabled = false;
            this.dtCreatedDate.Location = new System.Drawing.Point(692, 7);
            this.dtCreatedDate.Name = "dtCreatedDate";
            this.dtCreatedDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtCreatedDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtCreatedDate.Properties.DisplayFormat.FormatString = "";
            this.dtCreatedDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtCreatedDate.Properties.EditFormat.FormatString = "";
            this.dtCreatedDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtCreatedDate.Properties.MaskSettings.Set("mask", "");
            this.dtCreatedDate.Size = new System.Drawing.Size(450, 22);
            this.dtCreatedDate.StyleController = this.layoutControl1;
            this.dtCreatedDate.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 10F);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1149, 599);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtDispatchNumber;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(572, 32);
            this.layoutControlItem1.Text = "Dispatch Number";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(101, 16);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 563);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(911, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dtCreatedDate;
            this.layoutControlItem2.Location = new System.Drawing.Point(572, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(573, 32);
            this.layoutControlItem2.Text = "Created Date";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(101, 16);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtApprovedBy;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(572, 32);
            this.layoutControlItem3.Text = "Approved By";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(101, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dtApprovedDate;
            this.layoutControlItem4.Location = new System.Drawing.Point(572, 32);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(573, 32);
            this.layoutControlItem4.Text = "Approved Date";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(101, 16);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcDispatchDetail;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1145, 499);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnAddStock;
            this.layoutControlItem6.Location = new System.Drawing.Point(911, 563);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem6.Size = new System.Drawing.Size(126, 32);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClose;
            this.layoutControlItem7.Location = new System.Drawing.Point(1037, 563);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem7.Size = new System.Drawing.Size(108, 32);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // frmStockInDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1149, 599);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmStockInDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock-In detail";
            this.Load += new System.EventHandler(this.frmStockInDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDispatchDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispatchDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtApprovedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApprovedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDispatchNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreatedDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreatedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtDispatchNumber;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateTimeOffsetEdit dtApprovedDate;
        private DevExpress.XtraEditors.TextEdit txtApprovedBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnAddStock;
        private DevExpress.XtraGrid.GridControl gcDispatchDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDispatchDetail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.DateEdit dtCreatedDate;
    }
}