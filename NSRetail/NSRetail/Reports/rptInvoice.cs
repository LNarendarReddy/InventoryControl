using System.Data;

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
