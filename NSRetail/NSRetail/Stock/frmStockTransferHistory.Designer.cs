namespace NSRetail.Stock
{
    partial class frmStockTransferHistory
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl.Name = "layoutControl";
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Root.Name = "Root";
            this.gcHistory = new DevExpress.XtraGrid.GridControl();
            this.gcHistory.Name = "gcHistory";
            this.gvHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvHistory.Name = "gvHistory";
            this.btnDetails = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetails.Name = "btnDetails";
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose.Name = "btnClose";
            this.gridItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridItem.Name = "gridItem";
            this.detailsItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.detailsItem.Name = "detailsItem";
            this.closeItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.closeItem.Name = "closeItem";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailsItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeItem)).BeginInit();
            this.layoutControl.SuspendLayout();
            this.SuspendLayout();
            this.layoutControl.Controls.Add(this.gcHistory);
            this.layoutControl.Controls.Add(this.btnDetails);
            this.layoutControl.Controls.Add(this.btnClose);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Root = this.Root;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Size = new System.Drawing.Size(1100,600);
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { this.gridItem, this.detailsItem, this.closeItem });
            this.gcHistory.MainView = this.gvHistory;
            this.gcHistory.ViewCollection.Add(this.gvHistory);
            this.gvHistory.GridControl = this.gcHistory;
            this.gvHistory.OptionsBehavior.Editable = false;
            this.gvHistory.OptionsView.ColumnAutoWidth = false;
            this.gvHistory.OptionsView.ShowAutoFilterRow = true;
            this.gvHistory.OptionsView.ShowGroupPanel = false;
            this.gvHistory.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvHistory_PopupMenuShowing);
            this.btnDetails.Text = "View Details";
            this.btnDetails.StyleController = this.layoutControl;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            this.btnClose.Text = "Close";
            this.btnClose.StyleController = this.layoutControl;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.gridItem.Control = this.gcHistory;
            this.gridItem.TextVisible = false;
            this.gridItem.Location = new System.Drawing.Point(0,0);
            this.gridItem.Size = new System.Drawing.Size(1080,552);
            this.detailsItem.Control = this.btnDetails;
            this.detailsItem.TextVisible = false;
            this.detailsItem.Location = new System.Drawing.Point(0,552);
            this.detailsItem.Size = new System.Drawing.Size(540,28);
            this.closeItem.Control = this.btnClose;
            this.closeItem.TextVisible = false;
            this.closeItem.Location = new System.Drawing.Point(540,552);
            this.closeItem.Size = new System.Drawing.Size(540,28);
            this.Controls.Add(this.layoutControl);
            this.ClientSize = new System.Drawing.Size(1100,600);
            this.MinimumSize = new System.Drawing.Size(800,450);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.CancelButton = this.btnClose;
            this.Load += new System.EventHandler(this.frmStockTransferHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailsItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeItem)).EndInit();
            this.layoutControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHistory;
        private DevExpress.XtraEditors.SimpleButton btnDetails, btnClose;
        private DevExpress.XtraLayout.LayoutControlItem gridItem, detailsItem, closeItem;
    }
}
