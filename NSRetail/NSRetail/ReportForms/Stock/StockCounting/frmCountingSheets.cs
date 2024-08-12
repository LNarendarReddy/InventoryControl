using DataAccess;
using DevExpress.DataAccess.Native.Web;
using DevExpress.XtraEditors;
using NSRetail.ReportForms.Wareshouse.Audit;
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
    public partial class frmCountingSheets : DevExpress.XtraEditors.XtraForm
    {
       object BranchID = null;
        public frmCountingSheets(DataTable dt , object branchID)
        {
            InitializeComponent();
            gcSheets.DataSource = dt;
            this.BranchID = branchID;
            gvSheets.BestFitColumns();
        }

        private void btnViewItems_Click(object sender, EventArgs e)
        {
            frmViewItems obj =
                        new frmViewItems(new CountingRepository().GetStockCountingDetail(gvSheets.GetFocusedRowCellValue("STOCKCOUNTINGID")), BranchID);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }
    }
}