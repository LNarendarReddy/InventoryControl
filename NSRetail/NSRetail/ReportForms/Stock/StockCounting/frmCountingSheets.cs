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
        object CountigApprovalID = null;
        public frmCountingSheets(DataTable dt , object branchID, object countigApprovalID = null)
        {
            InitializeComponent();
            gcSheets.DataSource = dt;
            this.CountigApprovalID = countigApprovalID;
            this.BranchID = branchID;
            gvSheets.BestFitColumns();
        }

        private void btnViewItems_Click(object sender, EventArgs e)
        {
            frmViewItems obj =
                        new frmViewItems(new CountingRepository().GetStockCountingDetail(gvSheets.GetFocusedRowCellValue("STOCKCOUNTINGID")),
                        BranchID, true, CountigApprovalID);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }
        private void frmViewItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}