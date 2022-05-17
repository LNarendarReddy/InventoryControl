using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetail
{
    public partial class rptDealerIndent : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDealerIndent(DataTable dtItems)
        {
            InitializeComponent();
            this.DataSource = dtItems;
        }

    }
}
