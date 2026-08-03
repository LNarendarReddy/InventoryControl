namespace NSRetail.Reports
{
    partial class rptPickList
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new DevExpress.XtraReports.UI.XRLabel();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.TitleStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // label1
            // 
            this.label1.Font = new DevExpress.Drawing.DXFont("Arial", 14F);
            this.label1.LocationFloat = new DevExpress.Utils.PointFloat(3.973643E-05F, 0F);
            this.label1.Name = "label1";
            this.label1.SizeF = new System.Drawing.SizeF(1129F, 30.01302F);
            this.label1.StyleName = "TitleStyle";
            this.label1.StylePriority.UseFont = false;
            this.label1.StylePriority.UseTextAlignment = false;
            this.label1.Text = "Pick list";
            this.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Detail
            // 
            this.Detail.Name = "Detail";
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.label1});
            this.TopMargin.HeightF = 30.01302F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 30F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // TitleStyle
            // 
            this.TitleStyle.Font = new DevExpress.Drawing.DXFont("Arial", 18F);
            this.TitleStyle.Name = "TitleStyle";
            this.TitleStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // rptPickList
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.HorizontalContentSplitting = DevExpress.XtraPrinting.HorizontalContentSplitting.Smart;
            this.Landscape = true;
            this.Margins = new DevExpress.Drawing.DXMargins(20F, 20F, 30.01302F, 30F);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.TitleStyle});
            this.Version = "22.2";
            this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRControlStyle TitleStyle;
    }
}
