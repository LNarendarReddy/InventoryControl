namespace NSRetail
{
    partial class frmItemHistory
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewReport = new DevExpress.XtraEditors.SimpleButton();
            this.gcItemHistory = new DevExpress.XtraGrid.GridControl();
            this.gvItemHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnViewReport);
            this.layoutControl1.Controls.Add(this.gcItemHistory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1100, 600);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(979, 566);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(849, 566);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(116, 22);
            this.btnViewReport.StyleController = this.layoutControl1;
            this.btnViewReport.TabIndex = 5;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // gcItemHistory
            // 
            this.gcItemHistory.Location = new System.Drawing.Point(12, 12);
            this.gcItemHistory.MainView = this.gvItemHistory;
            this.gcItemHistory.Name = "gcItemHistory";
            this.gcItemHistory.Size = new System.Drawing.Size(1076, 540);
            this.gcItemHistory.TabIndex = 4;
            this.gcItemHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemHistory});
            // 
            // gvItemHistory
            // 
            this.gvItemHistory.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gvItemHistory.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvItemHistory.Appearance.Row.Font = new System.Drawing.Font("Arial", 8F);
            this.gvItemHistory.Appearance.Row.Options.UseFont = true;
            this.gvItemHistory.GridControl = this.gcItemHistory;
            this.gvItemHistory.Name = "gvItemHistory";
            this.gvItemHistory.OptionsBehavior.Editable = false;
            this.gvItemHistory.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.gvItemHistory.OptionsView.ShowAutoFilterRow = true;
            this.gvItemHistory.OptionsView.ShowFooter = true;
            this.gvItemHistory.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1100, 600);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItemHistory;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1080, 544);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 544);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(837, 36);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnViewReport;
            this.layoutControlItem2.Location = new System.Drawing.Point(837, 544);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(130, 36);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            this.layoutControlItem3.Location = new System.Drawing.Point(967, 544);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(113, 36);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmItemHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmItemHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item History";
            this.Load += new System.EventHandler(this.frmItemHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItemHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcItemHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemHistory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnViewReport;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}
