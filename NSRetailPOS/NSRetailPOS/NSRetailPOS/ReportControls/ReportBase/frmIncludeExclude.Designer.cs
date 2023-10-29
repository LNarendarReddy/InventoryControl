namespace NSRetail.ReportForms
{
    partial class frmIncludeExclude
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIncludeExclude));
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIncExc = new DevExpress.XtraGrid.GridControl();
            this.gvIncExc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcInclude = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApplyAndSearch = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcIncExc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIncExc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gcIncExc
            // 
            this.gcIncExc.Location = new System.Drawing.Point(12, 12);
            this.gcIncExc.MainView = this.gvIncExc;
            this.gcIncExc.Name = "gcIncExc";
            this.gcIncExc.ShowOnlyPredefinedDetails = true;
            this.gcIncExc.Size = new System.Drawing.Size(633, 200);
            this.gcIncExc.TabIndex = 4;
            this.gcIncExc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIncExc});
            // 
            // gvIncExc
            // 
            this.gvIncExc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcColumnName,
            this.gcInclude,
            this.gcValue});
            this.gvIncExc.GridControl = this.gcIncExc;
            this.gvIncExc.Name = "gvIncExc";
            this.gvIncExc.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvIncExc.OptionsView.ShowGroupPanel = false;
            this.gvIncExc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvIncExc_KeyPress);
            // 
            // gcColumnName
            // 
            this.gcColumnName.Caption = "Column Name";
            this.gcColumnName.FieldName = "ColumnName";
            this.gcColumnName.Name = "gcColumnName";
            this.gcColumnName.OptionsColumn.AllowEdit = false;
            this.gcColumnName.Visible = true;
            this.gcColumnName.VisibleIndex = 0;
            this.gcColumnName.Width = 490;
            // 
            // gcInclude
            // 
            this.gcInclude.AppearanceHeader.Options.UseTextOptions = true;
            this.gcInclude.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcInclude.Caption = "Include";
            this.gcInclude.FieldName = "Included";
            this.gcInclude.Name = "gcInclude";
            this.gcInclude.Visible = true;
            this.gcInclude.VisibleIndex = 1;
            this.gcInclude.Width = 206;
            // 
            // gcValue
            // 
            this.gcValue.AppearanceCell.Options.UseTextOptions = true;
            this.gcValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcValue.AppearanceHeader.Options.UseTextOptions = true;
            this.gcValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcValue.Caption = "Value";
            this.gcValue.FieldName = "IncludedText";
            this.gcValue.Name = "gcValue";
            this.gcValue.OptionsColumn.AllowEdit = false;
            this.gcValue.Visible = true;
            this.gcValue.VisibleIndex = 2;
            this.gcValue.Width = 146;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnApplyAndSearch);
            this.layoutControl1.Controls.Add(this.gcIncExc);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(657, 272);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnApplyAndSearch
            // 
            this.btnApplyAndSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnApplyAndSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnApplyAndSearch.ImageOptions.SvgImage")));
            this.btnApplyAndSearch.Location = new System.Drawing.Point(483, 220);
            this.btnApplyAndSearch.Name = "btnApplyAndSearch";
            this.btnApplyAndSearch.Size = new System.Drawing.Size(158, 36);
            this.btnApplyAndSearch.StyleController = this.layoutControl1;
            this.btnApplyAndSearch.TabIndex = 6;
            this.btnApplyAndSearch.Text = "Apply and Search";
            this.btnApplyAndSearch.Click += new System.EventHandler(this.btnApplyAndSearch_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(657, 272);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcIncExc;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(637, 204);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 204);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(467, 48);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnApplyAndSearch;
            this.layoutControlItem3.Location = new System.Drawing.Point(467, 204);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(170, 48);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmIncludeExclude
            // 
            this.AcceptButton = this.btnApplyAndSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 272);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmIncludeExclude";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Include Exclude Settings";
            ((System.ComponentModel.ISupportInitialize)(this.gcIncExc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIncExc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcIncExc;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIncExc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gcInclude;
        private DevExpress.XtraGrid.Columns.GridColumn gcValue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton btnApplyAndSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}