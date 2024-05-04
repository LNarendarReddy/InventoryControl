namespace NSRetailPOS.UI
{
    partial class frmPriceCheck
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcItemOfferList = new DevExpress.XtraGrid.GridControl();
            this.gvItemOfferList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcItemPriceList = new DevExpress.XtraGrid.GridControl();
            this.gvItemPriceList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sluItemData = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sluItemDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ITEMCODEID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtItemCode = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemOfferList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemOfferList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemPriceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemPriceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcItemOfferList);
            this.layoutControl1.Controls.Add(this.gcItemPriceList);
            this.layoutControl1.Controls.Add(this.sluItemData);
            this.layoutControl1.Controls.Add(this.txtItemCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(829, 602);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcItemOfferList
            // 
            this.gcItemOfferList.Location = new System.Drawing.Point(24, 374);
            this.gcItemOfferList.MainView = this.gvItemOfferList;
            this.gcItemOfferList.Name = "gcItemOfferList";
            this.gcItemOfferList.Size = new System.Drawing.Size(781, 204);
            this.gcItemOfferList.TabIndex = 7;
            this.gcItemOfferList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemOfferList});
            // 
            // gvItemOfferList
            // 
            this.gvItemOfferList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gvItemOfferList.GridControl = this.gcItemOfferList;
            this.gvItemOfferList.Name = "gvItemOfferList";
            this.gvItemOfferList.OptionsBehavior.ReadOnly = true;
            this.gvItemOfferList.OptionsView.ShowGroupPanel = false;
            // 
            // gcItemPriceList
            // 
            this.gcItemPriceList.Location = new System.Drawing.Point(24, 122);
            this.gcItemPriceList.MainView = this.gvItemPriceList;
            this.gcItemPriceList.Name = "gcItemPriceList";
            this.gcItemPriceList.Size = new System.Drawing.Size(781, 203);
            this.gcItemPriceList.TabIndex = 6;
            this.gcItemPriceList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemPriceList});
            // 
            // gvItemPriceList
            // 
            this.gvItemPriceList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn5});
            this.gvItemPriceList.GridControl = this.gcItemPriceList;
            this.gvItemPriceList.Name = "gvItemPriceList";
            this.gvItemPriceList.OptionsBehavior.ReadOnly = true;
            this.gvItemPriceList.OptionsView.ShowGroupPanel = false;
            // 
            // sluItemData
            // 
            this.sluItemData.Location = new System.Drawing.Point(337, 49);
            this.sluItemData.Name = "sluItemData";
            this.sluItemData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sluItemData.Properties.NullText = "";
            this.sluItemData.Properties.PopupView = this.sluItemDataView;
            this.sluItemData.Size = new System.Drawing.Size(464, 20);
            this.sluItemData.StyleController = this.layoutControl1;
            this.sluItemData.TabIndex = 5;
            this.sluItemData.EditValueChanged += new System.EventHandler(this.sluItemData_EditValueChanged);
            // 
            // sluItemDataView
            // 
            this.sluItemDataView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ITEMCODEID,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.sluItemDataView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sluItemDataView.Name = "sluItemDataView";
            this.sluItemDataView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sluItemDataView.OptionsView.ShowGroupPanel = false;
            // 
            // ITEMCODEID
            // 
            this.ITEMCODEID.Caption = "ITEMCODEID";
            this.ITEMCODEID.FieldName = "ITEMCODEID";
            this.ITEMCODEID.Name = "ITEMCODEID";
            this.ITEMCODEID.Visible = true;
            this.ITEMCODEID.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ITEMCODE";
            this.gridColumn2.FieldName = "ITEMCODE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ITEMNAME";
            this.gridColumn3.FieldName = "ITEMNAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "SKUCODE";
            this.gridColumn4.FieldName = "SKUCODE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // txtItemCode
            // 
            this.txtItemCode.EnterMoveNextControl = true;
            this.txtItemCode.Location = new System.Drawing.Point(92, 49);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(169, 20);
            this.txtItemCode.StyleController = this.layoutControl1;
            this.txtItemCode.TabIndex = 4;
            this.txtItemCode.Enter += new System.EventHandler(this.txtItemCode_Enter);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(829, 602);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(809, 77);
            this.layoutControlGroup1.Text = "Item details";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtItemCode;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(245, 32);
            this.layoutControlItem1.Text = "Item Code";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sluItemData;
            this.layoutControlItem2.Location = new System.Drawing.Point(245, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(540, 32);
            this.layoutControlItem2.Text = "Item Name";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 77);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(809, 252);
            this.layoutControlGroup2.Text = "Price list";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcItemPriceList;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(785, 207);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 329);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(809, 253);
            this.layoutControlGroup3.Text = "Offer list";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcItemOfferList;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(785, 208);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "MRP";
            this.gridColumn1.FieldName = "MRP";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Sale price";
            this.gridColumn5.FieldName = "SALEPRICE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Offer type";
            this.gridColumn6.FieldName = "OFFERTYPENAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Offer Code";
            this.gridColumn7.FieldName = "OFFERCODE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Offer value";
            this.gridColumn8.FieldName = "OFFERVALUE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // frmPriceCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 602);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmPriceCheck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Price & offer details";
            this.Load += new System.EventHandler(this.frmPriceCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItemOfferList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemOfferList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemPriceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemPriceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluItemDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcItemOfferList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemOfferList;
        private DevExpress.XtraGrid.GridControl gcItemPriceList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemPriceList;
        private DevExpress.XtraEditors.SearchLookUpEdit sluItemData;
        private DevExpress.XtraGrid.Views.Grid.GridView sluItemDataView;
        private DevExpress.XtraEditors.TextEdit txtItemCode;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn ITEMCODEID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}