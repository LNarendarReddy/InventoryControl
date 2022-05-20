using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmViewItems : DevExpress.XtraEditors.XtraForm
    {
        public frmViewItems(DataTable dtItems, string caller, bool Diff = false)
        {
            InitializeComponent();
            gcItems.DataSource = dtItems;
            gcMRP.Visible = !Diff;
            gcSalePrice.Visible = !Diff;
            gcQuantity.Visible = !Diff;
            gcPhysicalStock.Visible = Diff;
            gcSystemStock.Visible = Diff;
            gcStockDiff.Visible = Diff;
            gcCostPriceWT.Visible = caller == "differences" || caller == "not enetered";
            gcDiffStockCP.Visible = caller == "differences" || caller == "not enetered";
            gcPhysicalStockCP.Visible = caller == "differences";
            gcSystemStockCP.Visible = caller == "differences";
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