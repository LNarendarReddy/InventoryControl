using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmViewItems : DevExpress.XtraEditors.XtraForm
    {
        public frmViewItems(DataTable dtItems,bool Diff= false)
        {
            InitializeComponent();
            gcItems.DataSource = dtItems;
            gcMRP.Visible = !Diff;
            gcSalePrice.Visible = !Diff;
            gcQuantity.Visible = !Diff;
            gcPhysicalStock.Visible = Diff;
            gcSystemStock.Visible = Diff;
            gcStockDiff.Visible = Diff;

        }

        private void frmViewItems_Load(object sender, EventArgs e)
        {

        }

        private void frmViewItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }
    }
}