using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetail.Reports
{
    public partial class rptCRefund : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCRefund(DataTable dtItems)
        {
            InitializeComponent();
            drItems.DataSource = dtItems;
        }

    }
}
