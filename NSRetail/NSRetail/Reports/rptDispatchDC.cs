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
        public rptDispatchDC(DataTable dtDC,DataTable dtItems)
        {
            InitializeComponent();
            this.DataSource = dtDC;
            this.drItems.DataSource = dtItems;
            this.drSummary.DataSource = dtItems;
        }

    }
}
