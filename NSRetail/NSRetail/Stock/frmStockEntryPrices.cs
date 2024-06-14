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

namespace NSRetail.Stock
{
    public partial class frmStockEntryPrices : DevExpress.XtraEditors.XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;
        public frmStockEntryPrices(DataTable dt)
        {
            InitializeComponent();
            gcCPList.DataSource = dt;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _IsSave = true;
                drSelected = gvCPList.GetFocusedRow();
                this.Close();
            }
            catch (Exception) { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}