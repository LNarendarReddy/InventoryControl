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

namespace NSRetail.ReportForms.Stock.StockCounting
{
    public partial class frmCountingData : DevExpress.XtraEditors.XtraForm
    {
        private object branchid = 0;
        public bool isSave = false;
        public frmCountingData(DataTable dt, object _branchid)
        {
            InitializeComponent();
            branchid = _branchid;
            gcItems.DataSource = dt;
            gvItems.BestFitColumns();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            isSave = true;
            this.Close();
        }
    }
}