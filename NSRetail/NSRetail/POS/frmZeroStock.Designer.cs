namespace NSRetail
{
    partial class frmZeroStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZeroStock));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.btbGenerate = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewReport = new DevExpress.XtraEditors.SimpleButton();
            this.gcItems = new DevExpress.XtraGrid.GridControl();
            this.gvItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.cmbBranch);
            this.layoutControl1.Controls.Add(this.btbGenerate);
            this.layoutControl1.Controls.Add(this.btnViewReport);
            this.layoutControl1.Controls.Add(this.gcItems);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1270, 512, 650, 400);
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1079, 736);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbBranch
            // 
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(58, 7);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHID", "BRANCHID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "Branch")});
            this.cmbBranch.Size = new System.Drawing.Size(237, 22);
            this.cmbBranch.StyleController = this.layoutControl1;
            this.cmbBranch.TabIndex = 0;
            // 
            // btbGenerate
            // 
            this.btbGenerate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btbGenerate.ImageOptions.Image")));
            this.btbGenerate.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btbGenerate.Location = new System.Drawing.Point(305, 7);
            this.btbGenerate.Name = "btbGenerate";
            this.btbGenerate.Size = new System.Drawing.Size(140, 22);
            this.btbGenerate.StyleController = this.layoutControl1;
            this.btbGenerate.TabIndex = 1;
            this.btbGenerate.Text = "Generate";
            this.btbGenerate.Click += new System.EventHandler(this.btbGenerate_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnViewReport.ImageOptions.Image")));
            this.btnViewReport.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnViewReport.Location = new System.Drawing.Point(455, 7);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(137, 22);
            this.btnViewReport.StyleController = this.layoutControl1;
            this.btnViewReport.TabIndex = 2;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // gcItems
            // 
            this.gcItems.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcItems.Location = new System.Drawing.Point(4, 36);
            this.gcItems.MainView = this.gvItems;
            this.gcItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcItems.Name = "gcItems";
            this.gcItems.Size = new System.Drawing.Size(1071, 696);
            this.gcItems.TabIndex = 3;
            this.gcItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItems});
            // 
            // gvItems
            // 
            this.gvItems.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvItems.Appearance.FooterPanel.Options.UseFont = true;
            this.gvItems.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvItems.Appearance.GroupFooter.Options.UseFont = true;
            this.gvItems.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvItems.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gvItems.DetailHeight = 404;
            this.gvItems.GridControl = this.gcItems;
            this.gvItems.Name = "gvItems";
            this.gvItems.OptionsBehavior.Editable = false;
            this.gvItems.OptionsView.ShowFooter = true;
            this.gvItems.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Branch Name";
            this.gridColumn1.FieldName = "BRANCHNAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "SKU Code";
            this.gridColumn2.FieldName = "SKUCODE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Item Name";
            this.gridColumn3.FieldName = "ITEMNAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Item Code";
            this.gridColumn4.FieldName = "ITEMCODE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1079, 736);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItems;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1075, 700);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnViewReport;
            this.layoutControlItem2.Location = new System.Drawing.Point(448, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(147, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(147, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(147, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btbGenerate;
            this.layoutControlItem3.Location = new System.Drawing.Point(298, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(150, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(150, 32);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(150, 32);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbBranch;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(298, 32);
            this.layoutControlItem4.Text = "Branch";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(39, 15);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(595, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(480, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Category";
            this.gridColumn5.FieldName = "CATEGORYNAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Sub Category";
            this.gridColumn6.FieldName = "SUBCATEGORYNAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Branch Stock";
            this.gridColumn7.FieldName = "BRANCHSTOCK";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Warehouse Stock";
            this.gridColumn8.FieldName = "WAREHOUSESTOCK";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // frmZeroStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 736);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmZeroStock";
            this.Text = "Zero Stock";
            this.Load += new System.EventHandler(this.frmZeroStock_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmZeroStock_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
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
        private DevExpress.XtraEditors.LookUpEdit cmbBranch;
        private DevExpress.XtraEditors.SimpleButton btbGenerate;
        private DevExpress.XtraEditors.SimpleButton btnViewReport;
        private DevExpress.XtraGrid.GridControl gcItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItems;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}