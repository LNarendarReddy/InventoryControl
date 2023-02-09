namespace NSRetail.ReportForms.Stock.StockReports
{
    partial class frmItemSummaryDetail
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
            this.tcDetail = new DevExpress.XtraTab.XtraTabControl();
            this.tbDispatch = new DevExpress.XtraTab.XtraTabPage();
            this.gcDispatches = new DevExpress.XtraGrid.GridControl();
            this.gvDispatches = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tbSales = new DevExpress.XtraTab.XtraTabPage();
            this.gcSales = new DevExpress.XtraGrid.GridControl();
            this.gvSales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tbBRefunds = new DevExpress.XtraTab.XtraTabPage();
            this.gcBRefunds = new DevExpress.XtraGrid.GridControl();
            this.gvBRefunds = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tbCRefunds = new DevExpress.XtraTab.XtraTabPage();
            this.gcCRefunds = new DevExpress.XtraGrid.GridControl();
            this.gvCRefunds = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.tcDetail)).BeginInit();
            this.tcDetail.SuspendLayout();
            this.tbDispatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDispatches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispatches)).BeginInit();
            this.tbSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSales)).BeginInit();
            this.tbBRefunds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBRefunds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBRefunds)).BeginInit();
            this.tbCRefunds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCRefunds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCRefunds)).BeginInit();
            this.SuspendLayout();
            // 
            // tcDetail
            // 
            this.tcDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDetail.Location = new System.Drawing.Point(0, 0);
            this.tcDetail.Name = "tcDetail";
            this.tcDetail.SelectedTabPage = this.tbDispatch;
            this.tcDetail.Size = new System.Drawing.Size(1048, 624);
            this.tcDetail.TabIndex = 0;
            this.tcDetail.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbDispatch,
            this.tbSales,
            this.tbBRefunds,
            this.tbCRefunds});
            // 
            // tbDispatch
            // 
            this.tbDispatch.Controls.Add(this.gcDispatches);
            this.tbDispatch.Name = "tbDispatch";
            this.tbDispatch.Size = new System.Drawing.Size(1046, 599);
            this.tbDispatch.Text = "Dispatches";
            // 
            // gcDispatches
            // 
            this.gcDispatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDispatches.Location = new System.Drawing.Point(0, 0);
            this.gcDispatches.MainView = this.gvDispatches;
            this.gcDispatches.Name = "gcDispatches";
            this.gcDispatches.Size = new System.Drawing.Size(1046, 599);
            this.gcDispatches.TabIndex = 0;
            this.gcDispatches.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDispatches});
            // 
            // gvDispatches
            // 
            this.gvDispatches.GridControl = this.gcDispatches;
            this.gvDispatches.Name = "gvDispatches";
            this.gvDispatches.OptionsBehavior.Editable = false;
            this.gvDispatches.OptionsView.ShowFooter = true;
            this.gvDispatches.OptionsView.ShowGroupPanel = false;
            // 
            // tbSales
            // 
            this.tbSales.Controls.Add(this.gcSales);
            this.tbSales.Name = "tbSales";
            this.tbSales.Size = new System.Drawing.Size(1046, 599);
            this.tbSales.Text = "Sales";
            // 
            // gcSales
            // 
            this.gcSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSales.Location = new System.Drawing.Point(0, 0);
            this.gcSales.MainView = this.gvSales;
            this.gcSales.Name = "gcSales";
            this.gcSales.Size = new System.Drawing.Size(1046, 599);
            this.gcSales.TabIndex = 0;
            this.gcSales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSales});
            // 
            // gvSales
            // 
            this.gvSales.GridControl = this.gcSales;
            this.gvSales.Name = "gvSales";
            this.gvSales.OptionsBehavior.Editable = false;
            this.gvSales.OptionsView.ShowFooter = true;
            this.gvSales.OptionsView.ShowGroupPanel = false;
            // 
            // tbBRefunds
            // 
            this.tbBRefunds.Controls.Add(this.gcBRefunds);
            this.tbBRefunds.Name = "tbBRefunds";
            this.tbBRefunds.Size = new System.Drawing.Size(1046, 599);
            this.tbBRefunds.Text = "Branch Returns";
            // 
            // gcBRefunds
            // 
            this.gcBRefunds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBRefunds.Location = new System.Drawing.Point(0, 0);
            this.gcBRefunds.MainView = this.gvBRefunds;
            this.gcBRefunds.Name = "gcBRefunds";
            this.gcBRefunds.Size = new System.Drawing.Size(1046, 599);
            this.gcBRefunds.TabIndex = 0;
            this.gcBRefunds.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBRefunds});
            // 
            // gvBRefunds
            // 
            this.gvBRefunds.GridControl = this.gcBRefunds;
            this.gvBRefunds.Name = "gvBRefunds";
            this.gvBRefunds.OptionsBehavior.Editable = false;
            this.gvBRefunds.OptionsView.ShowFooter = true;
            this.gvBRefunds.OptionsView.ShowGroupPanel = false;
            // 
            // tbCRefunds
            // 
            this.tbCRefunds.Controls.Add(this.gcCRefunds);
            this.tbCRefunds.Name = "tbCRefunds";
            this.tbCRefunds.Size = new System.Drawing.Size(1046, 599);
            this.tbCRefunds.Text = "Customer Returns";
            // 
            // gcCRefunds
            // 
            this.gcCRefunds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCRefunds.Location = new System.Drawing.Point(0, 0);
            this.gcCRefunds.MainView = this.gvCRefunds;
            this.gcCRefunds.Name = "gcCRefunds";
            this.gcCRefunds.Size = new System.Drawing.Size(1046, 599);
            this.gcCRefunds.TabIndex = 0;
            this.gcCRefunds.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCRefunds});
            // 
            // gvCRefunds
            // 
            this.gvCRefunds.GridControl = this.gcCRefunds;
            this.gvCRefunds.Name = "gvCRefunds";
            this.gvCRefunds.OptionsBehavior.Editable = false;
            this.gvCRefunds.OptionsView.ShowFooter = true;
            this.gvCRefunds.OptionsView.ShowGroupPanel = false;
            // 
            // frmItemSummaryDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 624);
            this.Controls.Add(this.tcDetail);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "frmItemSummaryDetail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Summary Detail";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmItemSummaryDetail_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tcDetail)).EndInit();
            this.tcDetail.ResumeLayout(false);
            this.tbDispatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDispatches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispatches)).EndInit();
            this.tbSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSales)).EndInit();
            this.tbBRefunds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBRefunds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBRefunds)).EndInit();
            this.tbCRefunds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCRefunds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCRefunds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tcDetail;
        private DevExpress.XtraTab.XtraTabPage tbDispatch;
        private DevExpress.XtraTab.XtraTabPage tbSales;
        private DevExpress.XtraTab.XtraTabPage tbBRefunds;
        private DevExpress.XtraTab.XtraTabPage tbCRefunds;
        private DevExpress.XtraGrid.GridControl gcDispatches;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDispatches;
        private DevExpress.XtraGrid.GridControl gcSales;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSales;
        private DevExpress.XtraGrid.GridControl gcBRefunds;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBRefunds;
        private DevExpress.XtraGrid.GridControl gcCRefunds;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCRefunds;
    }
}