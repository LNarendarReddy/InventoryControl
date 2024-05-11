namespace NSRetail.Reports
{
    partial class rptBarcode
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.rptDetail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode1 = new DevExpress.XtraReports.UI.XRBarCode();
            this.PackedDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.BatchNumber = new DevExpress.XtraReports.Parameters.Parameter();
            this.taskbarAssistant1 = new DevExpress.Utils.Taskbar.TaskbarAssistant();
            this.CategoryID = new DevExpress.XtraReports.Parameters.Parameter();
            this.IsOpenCategory = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // rptDetail
            // 
            this.rptDetail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrTable1,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrBarCode1});
            this.rptDetail.HeightF = 100.4963F;
            this.rptDetail.MultiColumn.ColumnCount = 2;
            this.rptDetail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.rptDetail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.rptDetail.Name = "rptDetail";
            // 
            // xrLabel5
            // 
            this.xrLabel5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'PKD:\' + ?PackedDate"),
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif(?IsOpenCategory,true ,false )")});
            this.xrLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(123.6859F, 67.50624F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(74.80315F, 14.96063F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "PKD.12/2021";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(5.575657F, 82.46687F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(192.9134F, 12.99213F);
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'MRP: \' + [MRP] + \' - VB Sale Price : \' + [SalePrice]")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.78693402318075911D;
            // 
            // xrLabel3
            // 
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemCode]"),
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "WidthF", "Iif(?IsOpenCategory, 118 , 192)\n")});
            this.xrLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(5.575628F, 67.50624F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(118.1102F, 14.96063F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "xrLabel1";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel3.WordWrap = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemName]")});
            this.xrLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(5.575657F, 21.72749F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(192.9134F, 12.99213F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "xrLabel1";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel2.WordWrap = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(5.57563F, 8.73536F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(192.9134F, 12.99213F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VICTORY BAZARS";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrBarCode1
            // 
            this.xrBarCode1.AutoModule = true;
            this.xrBarCode1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemCode]")});
            this.xrBarCode1.LocationFloat = new DevExpress.Utils.PointFloat(9.8425F, 34.71962F);
            this.xrBarCode1.Name = "xrBarCode1";
            this.xrBarCode1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrBarCode1.ShowText = false;
            this.xrBarCode1.SizeF = new System.Drawing.SizeF(183F, 32.78662F);
            this.xrBarCode1.StylePriority.UsePadding = false;
            this.xrBarCode1.Symbology = code128Generator1;
            // 
            // PackedDate
            // 
            this.PackedDate.AllowNull = true;
            this.PackedDate.Description = "PackedDate";
            this.PackedDate.Name = "PackedDate";
            this.PackedDate.Visible = false;
            // 
            // BatchNumber
            // 
            this.BatchNumber.AllowNull = true;
            this.BatchNumber.Description = "BatchNumber";
            this.BatchNumber.Name = "BatchNumber";
            this.BatchNumber.Visible = false;
            // 
            // CategoryID
            // 
            this.CategoryID.Description = "CategoryID";
            this.CategoryID.Name = "CategoryID";
            this.CategoryID.Type = typeof(int);
            this.CategoryID.ValueInfo = "0";
            this.CategoryID.Visible = false;
            // 
            // IsOpenCategory
            // 
            this.IsOpenCategory.AllowNull = true;
            this.IsOpenCategory.Description = "IsOpenCategory";
            this.IsOpenCategory.Name = "IsOpenCategory";
            this.IsOpenCategory.Type = typeof(bool);
            this.IsOpenCategory.ValueInfo = "True";
            this.IsOpenCategory.Visible = false;
            // 
            // rptBarcode
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.rptDetail});
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 100;
            this.PageWidth = 400;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.PackedDate,
            this.BatchNumber,
            this.CategoryID,
            this.IsOpenCategory});
            this.SnapGridSize = 9.84252F;
            this.Version = "21.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand rptDetail;
        private DevExpress.XtraReports.UI.XRBarCode xrBarCode1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.Parameters.Parameter PackedDate;
        private DevExpress.XtraReports.Parameters.Parameter BatchNumber;
        private DevExpress.Utils.Taskbar.TaskbarAssistant taskbarAssistant1;
        private DevExpress.XtraReports.Parameters.Parameter CategoryID;
        private DevExpress.XtraReports.Parameters.Parameter IsOpenCategory;
    }
}
