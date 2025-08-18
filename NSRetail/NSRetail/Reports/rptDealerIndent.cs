using System.Data;

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
