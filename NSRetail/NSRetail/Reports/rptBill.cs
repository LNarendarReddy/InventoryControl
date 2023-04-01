using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetail.Reports
{
    public partial class rptBill : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBill(DataTable dtItems,DataTable dtMOP)
        {
            InitializeComponent();
            drFooter.DataSource = dtItems;
            drItems.DataSource = dtItems;
            drGST.DataSource = dtItems;
            drMOP.DataSource = dtMOP;
        }

    }
}
