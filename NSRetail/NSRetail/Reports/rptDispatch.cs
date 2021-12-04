using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetail.Reports
{
    public partial class rptDispatch : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDispatch(DataTable dtDispatch,DataTable dtItems)
        {
            InitializeComponent();
            this.DataSource = dtDispatch;
            drItems.DataSource = dtItems;
        }

    }
}
