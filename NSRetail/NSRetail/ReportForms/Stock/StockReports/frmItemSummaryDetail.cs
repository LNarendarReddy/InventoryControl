using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class frmItemSummaryDetail : DevExpress.XtraEditors.XtraForm
    {
        public frmItemSummaryDetail(DataSet ds, object includeBillNumber)
        {
            InitializeComponent();
            gcDispatches.DataSource = ds.Tables[0];
            gcBRefunds.DataSource = ds.Tables[1];
            gcSales.DataSource = ds.Tables[2];
            gcCRefunds.DataSource = ds.Tables[3];
            gvSales.Columns["BILLNUMBER"].Visible = (bool)includeBillNumber;
            gvCRefunds.Columns["BILLNUMBER"].Visible = (bool)includeBillNumber;
        }

        private void frmItemSummaryDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}