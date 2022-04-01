namespace NSRetail
{
    partial class frmDayClosureSummary
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
            this.gcDayClosureSummary = new DevExpress.XtraGrid.GridControl();
            this.gvDayClosureSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDayClosureSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDayClosureSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcDayClosureSummary);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(537, 455);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcDayClosureSummary
            // 
            this.gcDayClosureSummary.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gcDayClosureSummary.Location = new System.Drawing.Point(4, 4);
            this.gcDayClosureSummary.MainView = this.gvDayClosureSummary;
            this.gcDayClosureSummary.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcDayClosureSummary.Name = "gcDayClosureSummary";
            this.gcDayClosureSummary.Size = new System.Drawing.Size(529, 447);
            this.gcDayClosureSummary.TabIndex = 4;
            this.gcDayClosureSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDayClosureSummary});
            // 
            // gvDayClosureSummary
            // 
            this.gvDayClosureSummary.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gvDayClosureSummary.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDayClosureSummary.Appearance.Row.Font = new System.Drawing.Font("Arial", 8F);
            this.gvDayClosureSummary.Appearance.Row.Options.UseFont = true;
            this.gvDayClosureSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvDayClosureSummary.DetailHeight = 404;
            this.gvDayClosureSummary.GridControl = this.gcDayClosureSummary;
            this.gvDayClosureSummary.Name = "gvDayClosureSummary";
            this.gvDayClosureSummary.OptionsView.ShowFooter = true;
            this.gvDayClosureSummary.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "MOPID";
            this.gridColumn1.FieldName = "MOPID";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mode Of Payment";
            this.gridColumn2.FieldName = "MOPNAME";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 251;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Closure Value";
            this.gridColumn3.FieldName = "CLOSUREVALUE";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CLOSUREVALUE", "{0:0.##}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 134;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(537, 455);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcDayClosureSummary;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(533, 451);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmDayClosureSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 455);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmDayClosureSummary";
            this.Text = "Day Closure Summary";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDayClosureSummary_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDayClosureSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDayClosureSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcDayClosureSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDayClosureSummary;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}