using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetail.Reports
{
    public partial class rptDispatchDC : DevExpress.XtraReports.UI.XtraReport
    {

        public rptDispatchDC(DataTable dtDC, DataTable dtItems, bool showCP)
        {
            InitializeComponent();
            this.DataSource = dtDC;
            this.drItems.DataSource = dtItems;
            this.drSummary.DataSource = dtItems;
            this.ShowPrintMarginsWarning = false;
            this.ShowCP.Value = showCP;
            //xrTableCell1.WidthF =  showCP ? 230 : 650;
            //xrTableCell7.BoundsF = new RectangleF(xrTableCell7.BoundsF.X + 400, xrTableCell7.BoundsF.Y, xrTableCell7.BoundsF.Width, xrTableCell7.BoundsF.Height);
            //xrTableCell8.WidthF = showCP ? 230 : 650;
        }
    }
}
