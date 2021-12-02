using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace NSRetail.Reports
{
    public partial class rptInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInvoice(DataTable dtInvoice,DataTable dtInvoiceDetail)
        {
            InitializeComponent();
            this.DataSource = dtInvoice;
            drItems.DataSource = dtInvoiceDetail;
        }

    }
}
