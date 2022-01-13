using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetailPOS.Reports
{
    public partial class rptBRefund : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBRefund(DataTable dtItems)
        {
            InitializeComponent();
            this.drItems.DataSource = dtItems;  
        }

    }
}
