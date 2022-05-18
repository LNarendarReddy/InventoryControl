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
    public partial class frmViewReturnItems : DevExpress.XtraEditors.XtraForm
    {
        public frmViewReturnItems(DataTable dtItems, object SupplierName, object IndentID)
        {
            InitializeComponent();
            this.Text = $"{Text} - {SupplierName} - {IndentID}";
            gcSupplierReturns.DataSource = dtItems;
        }

        private void frmViewReturnItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}