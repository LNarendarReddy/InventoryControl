using DevExpress.Charts.Native;
using DevExpress.Drawing.Internal.Fonts.Interop;
using DevExpress.Export.Xl;
using DevExpress.Utils.About;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using NSRetailLiteApp.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Helpers
{
    internal class BillHelper
    {
        public XtraReport GetBill(Bill currentBill)
        {
            XtraReport report = CreateReport();
            report.Parameters["CIN"].Value = "U51390AP2022PTC121579";
            report.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
            report.Parameters["FSSAI"].Value = "10114004000548";
            report.Parameters["Address"].Value = "10114004000548";
            report.Parameters["Phone"].Value = "10114004000548";
            report.Parameters["BillNumber"].Value = currentBill.BillNumber;
            report.Parameters["BillDate"].Value = DateTime.Now;
            report.Parameters["CounterName"].Value = "10114004000548";
            report.Parameters["UserName"].Value = "10114004000548";
            report.Parameters["BranchName"].Value = "10114004000548";
            report.Parameters["RoundingFactor"].Value = currentBill.Rounding;
            report.Parameters["IsDuplicate"].Value = false;
            report.Parameters["CustomerName"].Value = currentBill.CustomerName;
            report.Parameters["CustomerNumber"].Value = currentBill.CustomerMobile;
            report.Parameters["TenderedCash"].Value = currentBill.TenderedCash;
            report.Parameters["TenderedChange"].Value = currentBill.TenderedChange;
            report.Parameters["IsDoorDelivery"].Value = currentBill.IsDoorDelivery;
            report.Parameters["CustomerGST"].Value = currentBill.CustomerGST;
            
            report.Bands.Add(CreateItemDetailReportBand(currentBill));
            report.Bands.Add(CreateGSTDetailReportBand(currentBill));

            return report;
        }

        public XtraReport CreateReport()
        {
            XtraReport report = null;
            try
            {
                report = new XtraReport()
                {
                    StyleSheet = {
            new XRControlStyle() { Name = "Title", Font = new DevExpress.Drawing.DXFont("Arial", 10f, DevExpress.Drawing.DXFontStyle.Bold) },
            new XRControlStyle() { Name = "ReportHeader", Font = new DevExpress.Drawing.DXFont("Arial", 9f, DevExpress.Drawing.DXFontStyle.Bold) },
            new XRControlStyle() { Name = "ColumnHeader", Font = new DevExpress.Drawing.DXFont("Arial", 7f, DevExpress.Drawing.DXFontStyle.Bold) },
            new XRControlStyle() { Name = "PlainText", Font = new DevExpress.Drawing.DXFont("Arial", 7f, DevExpress.Drawing.DXFontStyle.Regular) },
            new XRControlStyle() { Name = "ReportFooter", Font = new DevExpress.Drawing.DXFont("Arial", 8f, DevExpress.Drawing.DXFontStyle.Bold) },
                    },
                    DisplayName = "Result file",
                    PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom,
                    PageWidth = 300,
                    Margins = new DevExpress.Drawing.DXMargins(15, 9, 10, 7),
                    Parameters = {
                    new Parameter() {Name = "CIN",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "GSTIN",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "FSSAI",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "Address",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "Phone",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "BillNumber",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "BillDate",Type = typeof(DateTime),Visible = false },
                    new Parameter() {Name = "CounterName",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "UserName",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "BranchName",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "RoundingFactor",Type = typeof(decimal),Visible = false },
                    new Parameter() {Name = "IsDuplicate",Type = typeof(bool),Visible = false },
                    new Parameter() {Name = "CustomerName",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "CustomerNumber",Type = typeof(string),Visible = false },
                    new Parameter() {Name = "TenderedCash",Type = typeof(decimal),Visible = false },
                    new Parameter() {Name = "TenderedChange",Type = typeof(decimal),Visible = false },
                    new Parameter() {Name = "IsDoorDelivery",Type = typeof(bool),Visible = false },
                    new Parameter() {Name = "CustomerGST",Type = typeof(string),Visible = false },
                    }
                };
                DetailBand detail = new DetailBand();
                detail.HeightF = 0F;
                report.Bands.Add(detail);
                report = CreateReportHeader(report);
            }
            catch (Exception ex)
            {
                throw;
            }
            return report;
        }

        public XtraReport CreateReportHeader(XtraReport report)
        {
            try
            {
                ReportHeaderBand reportHeaderBand = new ReportHeaderBand() { HeightF = 0 };

                XRTableCell Title = new XRTableCell() {
                    Font = new DevExpress.Drawing.DXFont("Arial", 10F, DevExpress.Drawing.DXFontStyle.Bold),
                    Text = "VICTORY BAZARS PRIVATE LIMITED",
                    Weight = 1D,
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                    Multiline = true,
                };
                Title.StylePriority.UseBorders = false;
                Title.StylePriority.UseFont = false;
                XRTableRow row1 = new XRTableRow() { Weight = 0.70982146228041088D };
                row1.Cells.AddRange(new XRTableCell[] { Title });

                XRTableCell celladdress = new XRTableCell()
                {
                    Weight = 1D,
                    Multiline = true,
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                };
                celladdress.StylePriority.UseFont = false;
                celladdress.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "?Address + \' Ph : \' + ?Phone"));
                XRTableRow row2 = new XRTableRow() { Weight = 0.70982137202194839D };
                row2.Cells.AddRange(new XRTableCell[] { celladdress });

                XRTableCell cellCIN = new XRTableCell() 
                { 
                    Weight = 1D,
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                    Multiline = true,
                    Name = "xrTableCell61",
                };
                cellCIN.StylePriority.UseFont = false;
                cellCIN.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'CIN :' + ?CIN"));
                XRTableRow row3 = new XRTableRow() { Weight = 0.70982146228041088D, Borders = DevExpress.XtraPrinting.BorderSide.Bottom };
                row3.StylePriority.UseBorders = false;
                row3.Cells.AddRange(new XRTableCell[] { cellCIN });

                XRTableCell cellGSTIN = new XRTableCell() 
                {
                    Weight = 0.5D,
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                };
                cellGSTIN.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'GSTIN-' + ?GSTIN"));
                cellGSTIN.StylePriority.UseFont = false;
                cellGSTIN.StylePriority.UseTextAlignment = false;

                XRTableCell cellFSSAI = new XRTableCell()
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Weight = 0.5D,
                };
                cellFSSAI.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'FSSAI-' + ?FSSAI"));
                cellFSSAI.StylePriority.UseFont = false;
                cellFSSAI.StylePriority.UseTextAlignment = false;

                XRTableRow row4 = new XRTableRow() { Weight = 0.70982146228041088D };
                row4.Cells.AddRange(new XRTableCell[] { cellGSTIN, cellFSSAI });

                XRTableCell cellTaxInvoice = new XRTableCell()
                {
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                    Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold),
                    Multiline = true,
                    Text = "TAX INVOICE",
                    Weight = 1D
                };
                cellTaxInvoice.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", 
                    "Text", 
                    "Iif(?IsDuplicate = false, 'TAX INVOICE', 'TAX INVOICE (DUPLICATE)')"));
                cellTaxInvoice.StylePriority.UseBorders = false;
                cellTaxInvoice.StylePriority.UseFont = false;
                XRTableRow row5 = new XRTableRow() { Weight = 0.70982143339711146D };
                row5.Cells.AddRange(new XRTableCell[] { cellTaxInvoice });

                XRTableCell cellDoorDelivery = new XRTableCell()
                {
                    Borders = DevExpress.XtraPrinting.BorderSide.None,
                    Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold),
                    Multiline = true,
                    Text = "DOOR DELIVERY",
                    Weight = 1D
                };
                cellDoorDelivery.StylePriority.UseBorders = false;
                cellDoorDelivery.StylePriority.UseFont = false;
                XRTableRow row6 = new XRTableRow() { Weight = 0.70982143339711157D };
                row6.Cells.AddRange(new XRTableCell[] { cellDoorDelivery });

                XRTableCell cellBillnumber = new XRTableCell()
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Weight = 1D
                };
                cellBillnumber.StylePriority.UseFont = false;
                cellBillnumber.StylePriority.UseTextAlignment = false;
                cellBillnumber.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'Bill No : ' + ?BillNumber"));
                XRTableRow row7 = new XRTableRow() { Weight = 1D };
                row7.Cells.AddRange(new XRTableCell[] { cellBillnumber });

                XRTableCell cellBillDate = new XRTableCell()
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Weight = 0.5D
                };
                cellBillDate.StylePriority.UseFont = false;
                cellBillDate.StylePriority.UseTextAlignment = false;
                cellBillDate.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'Date : ' + ?BillDate"));

                XRTableCell cellUserName = new XRTableCell() 
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    Weight = 0.5D,
                };
                cellUserName.StylePriority.UseFont = false;
                cellUserName.StylePriority.UseTextAlignment = false;
                cellUserName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "'User : ' + ?UserName"));
                XRTableRow row8 = new XRTableRow() 
                {
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                    Weight = 0.56785711637241709D
                };
                row8.StylePriority.UseBorders = false;
                row8.Cells.AddRange(new XRTableCell[] { cellBillDate, cellUserName });

                XRTable tableReportHeader = new XRTable()
                {
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 71.875F),
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                    SizeF = new System.Drawing.SizeF(275F, 119F),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
                };
                tableReportHeader.StylePriority.UseTextAlignment = false;
                tableReportHeader.Rows.AddRange(new XRTableRow[] { row1,row2, row3, row4, row5, row6, row7, row8 });
                reportHeaderBand.Controls.Add(tableReportHeader);
                report.Bands.Add(reportHeaderBand);
            }
            catch (Exception ex) { throw; }
            return report;
        }

        public DetailReportBand CreateItemDetailReportBand(Bill dtitems)
        {
            DetailReportBand stdDetail = new DetailReportBand();
            stdDetail.DataSource = dtitems;
            stdDetail.Bands.AddRange(new Band[] {
                CreateItemReportHeader(),
                CreateItemGroupHeader(),
                CreateItemsDetail(),CreateItemDetailReportFooter() });
            return stdDetail;
        }

        public ReportHeaderBand CreateItemReportHeader()
        {
            ReportHeaderBand band = null;
            try
            {
                band = new ReportHeaderBand() { HeightF = 24F };

                XRTableCell cellItemName = new XRTableCell() { Weight = 2.9999999999999947D, Text = "Item Description / HSN Code", Multiline = true };
                XRTableRow row1 = new XRTableRow() { Weight = 0.7407407407407407D };
                row1.Cells.AddRange(new XRTableCell[] { cellItemName });

                XRTableCell cellMRP = new XRTableCell() { Weight = 0.87272727272727035D, Text = "cellMRP" };
                XRTableCell CellUnitRate = new XRTableCell() { Weight = 0.54545454545454541D, Text = "Unit Rate" };
                XRTableCell cellQnty = new XRTableCell() { Weight = 0.76363637750798985D, Text = "Qnty" };
                XRTableCell cellAmount = new XRTableCell() { Weight = 0.81818180431019194D, Text = "Amount" };

                XRTableRow row2 = new XRTableRow() { Weight = 0.74074074074074092D };
                row2.Cells.AddRange(new XRTableCell[] { cellMRP, CellUnitRate, cellQnty, cellAmount });

                XRTable tableReportHeader = new XRTable()
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7f, DevExpress.Drawing.DXFontStyle.Bold),
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F),
                    WidthF = 750F,
                    StyleName = "Title",
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    SizeF = new System.Drawing.SizeF(275F, 24F)
                };
                tableReportHeader.StylePriority.UseFont = false;
                tableReportHeader.StylePriority.UseTextAlignment = false;
                tableReportHeader.Rows.AddRange(new XRTableRow[] { row1, row2 });
                band.Controls.Add(tableReportHeader);
            }
            catch (Exception ex) { throw; }
            return band;
        }

        public GroupHeaderBand CreateItemGroupHeader()
        {
            GroupHeaderBand groupHeader = null;
            try
            {
                groupHeader = new GroupHeaderBand()
                {
                    HeightF = 0F,
                    WidthF = 750F,
                    Name = "groupHeader",
                    Level = 0,
                    GroupUnion = GroupUnion.WholePage
                };

                groupHeader.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CGSTDESC", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

                XRTableCell cellgroupheader = new XRTableCell() { Weight = 0.91696970159357294D };
                cellgroupheader.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", 
                    "Text", 
                    "[GSTCODE] + \': CGST @ \' + [CGSTDESC] + \'% SGST @ \' + [SGSTDESC] + \'% CESS @ \' + [" +"CESSDESC] + \'%\'")});
                
                XRTableRow row1 = new XRTableRow() { Weight = 0.696594427244582D };
                row1.Cells.AddRange(new XRTableCell[] { cellgroupheader });

                XRTable tableReportHeader = new XRTable()
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7f, DevExpress.Drawing.DXFontStyle.Bold),
                    Name = "tableReportHeader",
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F),
                    WidthF = 750F,
                    StyleName = "Title",
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    SizeF = new System.Drawing.SizeF(275F, 15F)
                };

                tableReportHeader.StylePriority.UseFont = false;
                tableReportHeader.StylePriority.UseTextAlignment = false;
                tableReportHeader.Rows.AddRange(new XRTableRow[] { row1 });
                groupHeader.Controls.Add(tableReportHeader);
            }
            catch (Exception ex){throw;}
            return groupHeader;
        }

        public DetailBand CreateItemsDetail()
        {
            DetailBand stddetail = null;
            try
            {
                stddetail = new DetailBand()
                {
                    HeightF = 30F,
                    Name = "stddetail"
                };

                XRTableCell cellItemName = new XRTableCell() { Weight = 1D };
                cellItemName.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", "[ITEMNAME] + Iif(IsNull([HSNCODE],\'\' ) == \'\',\'\' , \' / \' + [HSNCODE]) ")});
                cellItemName.Name = "xrTableCell21";
                cellItemName.Weight = 3D;

                XRTableRow row1 = new XRTableRow() {};
                row1.Cells.AddRange(new XRTableCell[] { cellItemName });

                XRTableCell cellMRP = new XRTableCell() { Weight = 1D };
                cellMRP.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", "[MRP]")});
                cellMRP.Weight = 0.87272727272727035D;


                XRTableCell CellUnitRate = new XRTableCell() { Weight = 1D };
                CellUnitRate.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", "[BILLEDAMOUNT] / (Iif([ISOPENITEM] = True,[WEIGHTINKGS] , [QUANTITY]))")});
                CellUnitRate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                CellUnitRate.Weight = 0.54545454545454541D;

                XRTableCell cellQnty = new XRTableCell() { Weight = 1D };
                cellQnty.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", "Iif([ISOPENITEM] = True,[WEIGHTINKGS] , [QUANTITY])")});
                cellQnty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                cellQnty.TextFormatString = "{0:N2}";
                cellQnty.Weight = 0.76363637750798985D;

                XRTableCell cellAmount = new XRTableCell() { Weight = 1D };
                cellAmount.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", "[BILLEDAMOUNT]")});
                cellAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                cellAmount.Weight = 0.81818180431019194D;

                XRTableRow row2 = new XRTableRow() { Weight = 1D };
                row2.Cells.AddRange(new XRTableCell[] { cellMRP, CellUnitRate, cellQnty, cellAmount });

                XRTable tableStdDetail = new XRTable()
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7f, DevExpress.Drawing.DXFontStyle.Regular),
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F),
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                    SizeF = new System.Drawing.SizeF(275F, 28F),
                    StyleName = "Normal",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
                };
                tableStdDetail.StylePriority.UseFont = false;
                tableStdDetail.StylePriority.UseTextAlignment = false;
                stddetail.Controls.Add(tableStdDetail);
            }
            catch (Exception ex)
            {
                throw;
            }
            return stddetail;
        }

        public ReportFooterBand CreateItemDetailReportFooter()
        {
            ReportFooterBand? band = null;
            try
            {
                band = new ReportFooterBand() { HeightF = 45F };

                XRLine xrLine1 = new XRLine()
                {
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 39F),
                    SizeF = new System.Drawing.SizeF(275F, 3F),
                };

                XRLine xrLine2 = new XRLine()
                {
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 42F),
                    SizeF = new System.Drawing.SizeF(275F, 3F)
                };

                XRTable xrTable4 = new XRTable()
                {
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F, DevExpress.Drawing.DXFontStyle.Regular),
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 3F),
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                    SizeF = new System.Drawing.SizeF(275F, 36F),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
                };
                xrTable4.StylePriority.UseFont = false;
                xrTable4.StylePriority.UseTextAlignment = false;

                XRTableRow xrTableRow9 = new XRTableRow()
                {
                    BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash,
                    Borders = DevExpress.XtraPrinting.BorderSide.Top,
                    Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold),
                    Weight = 0.60606060606060619D
                };
                xrTableRow9.StylePriority.UseBorderDashStyle = false;
                xrTableRow9.StylePriority.UseBorders = false;
                xrTableRow9.StylePriority.UseFont = false;

                XRSummary xrSummary1 = new XRSummary();
                xrSummary1.Running = SummaryRunning.Report;
                XRTableCell xrTableCell25 = new XRTableCell()
                {
                    Multiline = true,
                    Summary = xrSummary1,
                    Text = "Qnty",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Weight = 0.87272727966308439D,
                };
                xrTableCell25.StylePriority.UseTextAlignment = false;
                xrTableCell25.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", "\'Items : \' + sumCount()")});


                XRSummary xrSummary2 = new XRSummary();
                xrSummary2.Running = SummaryRunning.Report;
                XRTableCell xrTableCell48 = new XRTableCell()
                {
                    Multiline = true,
                    Summary = xrSummary2,
                    Text = "xrTableCell48",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    Weight = 1.3090909160267212D,
                };
                xrTableCell48.StylePriority.UseTextAlignment = false;
                xrTableCell48.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", "\'Qnty : \' + sumSum([QUANTITY])")});

                XRSummary xrSummary3 = new XRSummary();
                xrSummary3.Running = SummaryRunning.Report;
                XRTableCell xrTableCell27 = new XRTableCell()
                {
                    Multiline = true,
                    Summary = xrSummary3,
                    Text = "Amount",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    Weight = 0.81818180431019172D
                };
                xrTableCell27.StylePriority.UseTextAlignment = false;
                xrTableCell27.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", "Text", " sumSum([BILLEDAMOUNT])")});

                xrTableRow9.Cells.AddRange(new XRTableCell[] { xrTableCell25, xrTableCell48, xrTableCell27 });

                XRTableRow xrTableRow21 = new XRTableRow()
                {
                    BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash,
                    Borders = DevExpress.XtraPrinting.BorderSide.None,
                    Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold),
                    Weight = 0.60606060606060619D
                };
                xrTableRow21.StylePriority.UseBorderDashStyle = false;
                xrTableRow21.StylePriority.UseBorders = false;
                xrTableRow21.StylePriority.UseFont = false;

                XRTableCell xrTableCell4 = new XRTableCell()
                {
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Weight = 0.87272727966308439D
                };
                xrTableCell4.StylePriority.UseTextAlignment = false;

                XRTableCell xrTableCell47 = new XRTableCell()
                {
                    Multiline = true,
                    Text = "Rounding : ",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    Weight = 1.3090909160267212D
                };
                xrTableCell47.StylePriority.UseTextAlignment = false;

                XRTableCell xrTableCell50 = new XRTableCell()
                {
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    TextFormatString = "{0:N2}",
                    Weight = 0.81818180431019172D,
                };
                xrTableCell50.StylePriority.UseTextAlignment = false;
                xrTableCell50.ExpressionBindings.AddRange(new ExpressionBinding[] {
                        new ExpressionBinding("BeforePrint", "Text", "IsNull(?RoundingFactor,0 )")});

                xrTableRow21.Cells.AddRange(new XRTableCell[] {xrTableCell4,xrTableCell47,xrTableCell50});

                XRTableRow xrTableRow22 = new XRTableRow()
                {
                    BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash,
                    Borders = DevExpress.XtraPrinting.BorderSide.None,
                    Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold),
                    Weight = 0.6060606060606063D
                };
                xrTableRow22.StylePriority.UseBorderDashStyle = false;
                xrTableRow22.StylePriority.UseBorders = false;
                xrTableRow22.StylePriority.UseFont = false;

                XRTableCell xrTableCell51 = new XRTableCell()
                {
                    Multiline = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Weight = 0.87272727966308439D,
                };
                xrTableCell51.StylePriority.UseTextAlignment = false;

                XRTableCell xrTableCell52 = new XRTableCell()
                {
                    Multiline = true,
                    Text = "Total Amount : ",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    Weight = 1.3090909160267212D
                };
                xrTableCell52.StylePriority.UseTextAlignment = false;

                XRSummary xrSummary4 = new XRSummary();
                xrSummary4.Running = SummaryRunning.Report;

                XRTableCell xrTableCell53 = new XRTableCell()
                {
                    Multiline = true,
                    Summary = xrSummary4,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    Weight = 0.81818180431019172D
                };
                xrTableCell53.StylePriority.UseTextAlignment = false;
                xrTableCell53.ExpressionBindings.AddRange(new ExpressionBinding[] {
                    new ExpressionBinding("BeforePrint", 
                    "Text", 
                    " sumSum([BILLEDAMOUNT]) + IsNull(?RoundingFactor,0 )")});

                xrTableRow22.Cells.AddRange(new XRTableCell[] { xrTableCell51, xrTableCell52, xrTableCell53 });
                band.Controls.AddRange(new XRControl[] { xrLine2, xrLine1, xrTable4 });
            }
            catch (Exception ex)
            {
                throw;
            }
            return band;
        }

        public DetailReportBand CreateGSTDetailReportBand(Bill dtitems)
        {
            DetailReportBand drGST = new DetailReportBand();
            drGST.DataSource = dtitems;
            DetailBand Detail1 = new DetailBand() { HeightF = 0F };
            drGST.Bands.AddRange(new Band[] {Detail1, CreateGSTReportHeader(), CreateGSTReportFooter(), CreateGSTGroupHeader()});
            drGST.Level = 1;
            return drGST;
        }

        public ReportHeaderBand CreateGSTReportHeader()
        {
            ReportHeaderBand ReportHeader2 = null;
            try
            {
                ReportHeader2 = new ReportHeaderBand() { HeightF = 24F };

                XRTable xrTable5 = new XRTable()
                {
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F),
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                    SizeF = new System.Drawing.SizeF(275F, 24F),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
                };
                xrTable5.StylePriority.UseTextAlignment = false;

                XRTableRow xrTableRow14 = new XRTableRow() 
                {
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F),
                    Weight = 0.69230769230769229D,
                };
                xrTableRow14.StylePriority.UseBorders = false;
                xrTableRow14.StylePriority.UseFont = false;

                XRTableCell xrTableCell30 = new XRTableCell() 
                {
                    Multiline = true,
                    Text = "<-----GST Breakup Details(Amount In INR)----->",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Weight = 3D
                };
                xrTableCell30.StylePriority.UseTextAlignment = false;

                xrTableRow14.Cells.AddRange(new XRTableCell[] { xrTableCell30 });

                XRTableRow xrTableRow8 = new XRTableRow() 
                {
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                    Font = new DevExpress.Drawing.DXFont("Arial", 7F),
                    Name = "xrTableRow8",
                    Weight = 0.69230769230769229D
                };
                xrTableRow8.StylePriority.UseBorders = false;
                xrTableRow8.StylePriority.UseFont = false;

                XRTableCell xrTableCell22 = new XRTableCell()
                {
                    Multiline = true,
                    Text = "GST Code",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Weight = 0.5D
                };
                xrTableCell22.StylePriority.UseTextAlignment = false;


                xrTableRow8.Cells.AddRange(new XRTableCell[] {xrTableCell22,
                    new XRTableCell(){Multiline = true,Text = "Taxable Amount",Weight = 0.5D},
                    new XRTableCell(){Multiline = true,Text = "CGST",Weight = 0.5D},
                    new XRTableCell(){Multiline = true,Text = "SGST",Weight = 0.5D},
                    new XRTableCell(){Multiline = true,Text = "CESS",Weight = 0.5D},
                    new XRTableCell(){Multiline = true,Text = "Total Amount",Weight = 0.5D},
                });

                xrTable5.Rows.AddRange(new XRTableRow[] {xrTableRow14, xrTableRow8});

                ReportHeader2.Controls.AddRange(new XRControl[] {xrTable5});
            }
            catch (Exception ex) { throw; }
            return ReportHeader2;
        }

        public ReportFooterBand CreateGSTReportFooter()
        {
            ReportFooterBand ReportFooter = new ReportFooterBand() { Expanded = false, HeightF = 21F };
            XRLine xrLine4 = new XRLine()
            {
                LocationFloat = new DevExpress.Utils.PointFloat(0F, 18F),
                SizeF = new System.Drawing.SizeF(275F, 3F)
            };

            XRLine xrLine3 = new XRLine() 
            {
                LocationFloat = new DevExpress.Utils.PointFloat(0F, 15F),
                SizeF = new System.Drawing.SizeF(275F, 3F)
            };

            XRTable xrTable7 = new XRTable() 
            {
                LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F),
                Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                SizeF = new System.Drawing.SizeF(275F, 15F),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };
            xrTable7.StylePriority.UseTextAlignment = false;

            XRTableRow xrTableRow15 = new XRTableRow() 
            {
                Borders = DevExpress.XtraPrinting.BorderSide.Top,
                Font = new DevExpress.Drawing.DXFont("Arial", 7F),
                Weight = 0.721153846153846D
            };
            xrTableRow15.StylePriority.UseBorders = false;
            xrTableRow15.StylePriority.UseFont = false;

            XRTableCell xrTableCell31 = new XRTableCell() 
            {
                Multiline = true,
                Text = "Total : ",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                Weight = 0.5D
            };
            xrTableCell31.StylePriority.UseTextAlignment = false;

            XRSummary xrSummary5 = new XRSummary();
            xrSummary5.Running = SummaryRunning.Report;
            XRTableCell xrTableCell38 = new XRTableCell()
            {
                Summary = xrSummary5,
                Text = "Taxable Amount",
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell38.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([BILLEDAMOUNT] - [GSTVALUE])")});

            XRSummary xrSummary6 = new XRSummary();
            xrSummary6.Running = SummaryRunning.Report;
            XRTableCell xrTableCell39 = new XRTableCell()
            {
                Summary = xrSummary6,
                Text = "CGST",
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell39.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([CGST])")});

            XRSummary xrSummary7 = new XRSummary();
            xrSummary7.Running = SummaryRunning.Report;
            XRTableCell xrTableCell40 = new XRTableCell()
            {
                Summary = xrSummary7,
                Text = "SGST",
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell40.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([SGST])")});

            XRSummary xrSummary8 = new XRSummary();
            xrSummary8.Running = SummaryRunning.Report;
            XRTableCell xrTableCell41 = new XRTableCell()
            {
                Summary = xrSummary8,
                Weight = 0.5D,
                WordWrap = false,
            };
            xrTableCell41.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([CESS])")});

            XRSummary xrSummary9 = new XRSummary();
            xrSummary9.Running = SummaryRunning.Report;
            XRTableCell xrTableCell42 = new XRTableCell()
            {
                Summary = xrSummary9,
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell42.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([BILLEDAMOUNT])")});

            xrTableRow15.Cells.AddRange(new XRTableCell[] {xrTableCell31,xrTableCell38,xrTableCell39,xrTableCell40,xrTableCell41,xrTableCell42});

            xrTable7.Rows.AddRange(new XRTableRow[] {xrTableRow15});

            ReportFooter.Controls.AddRange(new XRControl[] { xrLine4,xrLine3,xrTable7});
            return ReportFooter;
        }

        public GroupHeaderBand CreateGSTGroupHeader()
        {
            GroupHeaderBand GroupHeader1 = new GroupHeaderBand() { HeightF = 12F };
            GroupHeader1.GroupFields.AddRange(new GroupField[] {new GroupField("CGSTDESC", XRColumnSortOrder.Ascending)});

            XRTable xrTable6 =  new XRTable() 
            {
                LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F),
                Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F),
                SizeF = new System.Drawing.SizeF(275F, 12F),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            };
            xrTable6.StylePriority.UseTextAlignment = false;

            XRTableRow xrTableRow16 = new XRTableRow() 
            {
                Font = new DevExpress.Drawing.DXFont("Arial", 7F),
                Weight = 0.9230769230769228D,
            };
            xrTableRow16.StylePriority.UseFont = false;

            XRTableCell xrTableCell32 = new XRTableCell() 
            {
                Multiline = true,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                Weight = 0.5D,
            };
            xrTableCell32.StylePriority.UseTextAlignment = false;
            xrTableCell32.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "[GSTCODE]")});

            XRSummary xrSummary10 = new XRSummary();
            xrSummary10.Running = SummaryRunning.Group;
            XRTableCell xrTableCell33 = new XRTableCell()
            {
                Summary = xrSummary10,
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell33.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([BILLEDAMOUNT] - [GSTVALUE])")});

            XRSummary xrSummary11 = new XRSummary();
            xrSummary11.Running = SummaryRunning.Group;
            XRTableCell xrTableCell34 = new XRTableCell()
            {
                Summary = xrSummary11,
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell34.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([CGST])")});

            XRSummary xrSummary12 = new XRSummary();
            xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            XRTableCell xrTableCell35 = new XRTableCell()
            {
                Summary = xrSummary12,
                Weight = 0.5D,
                WordWrap = false,
            };
            xrTableCell35.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([SGST])")});

            XRSummary xrSummary13 = new XRSummary();
            xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            XRTableCell xrTableCell36 = new XRTableCell()
            {
                Summary = xrSummary13,
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell36.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([CESS])")});

            XRSummary xrSummary14 = new XRSummary();
            xrSummary14.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            XRTableCell xrTableCell37 = new XRTableCell()
            {
                Summary = xrSummary14,
                Weight = 0.5D,
                WordWrap = false
            };
            xrTableCell37.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "sumSum([BILLEDAMOUNT])")});


            xrTableRow16.Cells.AddRange(new XRTableCell[] {xrTableCell32,xrTableCell33,xrTableCell34,xrTableCell35,xrTableCell36,xrTableCell37});

            xrTable6.Rows.AddRange(new XRTableRow[] {xrTableRow16});
            GroupHeader1.Controls.AddRange(new XRControl[] {xrTable6});
            return GroupHeader1;
        }
    }
}
