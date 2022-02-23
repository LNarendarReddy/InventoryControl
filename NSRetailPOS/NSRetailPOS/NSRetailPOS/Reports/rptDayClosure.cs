using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetailPOS.Reports
{
    public partial class rptDayClosure : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDayClosure(DataSet ds)
        {
            InitializeComponent();
            this.DataSource = ds.Tables[0];
            drDenominations.DataSource = ds.Tables[1];
            drMOP.DataSource = ds.Tables[2];
            drFooter.DataSource = ds.Tables[3];
        }
    }
}
