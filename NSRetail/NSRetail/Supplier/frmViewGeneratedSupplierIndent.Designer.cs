namespace NSRetail.Supplier
{
    partial class frmViewGeneratedSupplierIndent
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewGeneratedSupplierIndent));
            this.gvBranchDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSupplierIndent = new DevExpress.XtraGrid.GridControl();
            this.gvSupplierIndent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnProceed = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvBranchDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSupplierIndent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSupplierIndent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // gvBranchDetails
            // 
            this.gvBranchDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18});
            this.gvBranchDetails.GridControl = this.gcSupplierIndent;
            this.gvBranchDetails.Name = "gvBranchDetails";
            this.gvBranchDetails.OptionsBehavior.ReadOnly = true;
            this.gvBranchDetails.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "ITEMID";
            this.gridColumn13.FieldName = "ITEMID";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "BRANCHID";
            this.gridColumn14.FieldName = "BRANCHID";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Branch Name";
            this.gridColumn15.FieldName = "BRANCHNAME";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 0;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Branch Qty";
            this.gridColumn16.FieldName = "BRANCHQUANTITY";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 1;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Req. Branch Qty";
            this.gridColumn17.FieldName = "REQUIREDBRANCHSTOCK";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 2;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Req. Item Indent";
            this.gridColumn18.FieldName = "REQUIREDITEMINDENT";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 3;
            // 
            // gcSupplierIndent
            // 
            gridLevelNode2.LevelTemplate = this.gvBranchDetails;
            gridLevelNode2.RelationName = "FT - Branch details";
            this.gcSupplierIndent.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gcSupplierIndent.Location = new System.Drawing.Point(16, 50);
            this.gcSupplierIndent.MainView = this.gvSupplierIndent;
            this.gcSupplierIndent.Name = "gcSupplierIndent";
            this.gcSupplierIndent.Size = new System.Drawing.Size(1006, 685);
            this.gcSupplierIndent.TabIndex = 6;
            this.gcSupplierIndent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSupplierIndent,
            this.gvBranchDetails});
            // 
            // gvSupplierIndent
            // 
            this.gvSupplierIndent.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvSupplierIndent.GridControl = this.gcSupplierIndent;
            this.gvSupplierIndent.Name = "gvSupplierIndent";
            this.gvSupplierIndent.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Item Name";
            this.gridColumn1.FieldName = "ITEMNAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "SKUCODE";
            this.gridColumn2.FieldName = "SKUCODE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ITEMID";
            this.gridColumn3.FieldName = "ITEMID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "STOCKENTRYDETAILID";
            this.gridColumn4.FieldName = "STOCKENTRYDETAILID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "MRP";
            this.gridColumn5.FieldName = "MRP";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Branch Qty";
            this.gridColumn6.FieldName = "BRANCHQUANTITY";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Req. Branch Stock";
            this.gridColumn7.FieldName = "REQUIREDBRANCHSTOCK";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Calc. Indent";
            this.gridColumn8.FieldName = "CALCULATEDITEMINDENT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Final Req. Indent";
            this.gridColumn9.FieldName = "REQUIREDITEMINDENT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Desired Indent";
            this.gridColumn10.FieldName = "DESIREDITEMINDENT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Inner case Qty";
            this.gridColumn11.FieldName = "INNERCASEQTY";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Outer case Qty";
            this.gridColumn12.FieldName = "OUTERCASEQTY";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcSupplierIndent);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnProceed);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1038, 751);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(907, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            // 
            // btnProceed
            // 
            this.btnProceed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnProceed.ImageOptions.Image")));
            this.btnProceed.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnProceed.Location = new System.Drawing.Point(778, 16);
            this.btnProceed.Name = "btnProceed";
            this.btnProceed.Size = new System.Drawing.Size(117, 22);
            this.btnProceed.StyleController = this.layoutControl1;
            this.btnProceed.TabIndex = 4;
            this.btnProceed.Text = "Proceed";
            this.btnProceed.Click += new System.EventHandler(this.btnProceed_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1038, 751);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnProceed;
            this.layoutControlItem1.Location = new System.Drawing.Point(762, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(129, 34);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(129, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(129, 34);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(762, 34);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(891, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(127, 34);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(127, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(127, 34);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcSupplierIndent;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(1018, 697);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmViewGeneratedSupplierIndent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 751);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmViewGeneratedSupplierIndent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Generated Supplier Indent";
            this.Load += new System.EventHandler(this.frmViewGeneratedSupplierIndent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvBranchDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSupplierIndent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSupplierIndent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcSupplierIndent;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSupplierIndent;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnProceed;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBranchDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
    }
}