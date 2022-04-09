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
    }
}