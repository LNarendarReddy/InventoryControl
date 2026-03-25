using System.Data;

namespace NSRetail.Stock
{
    public partial class frmInvoiceItems : DevExpress.XtraEditors.XtraForm
    {
        public frmInvoiceItems(DataTable dt)
        {
            InitializeComponent();
            gcStockEntry.DataSource = dt;
            gvStockEntry.BestFitColumns();
        }

        private void gvStockEntry_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void frmInvoiceItems_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
            }
        }
    }
}