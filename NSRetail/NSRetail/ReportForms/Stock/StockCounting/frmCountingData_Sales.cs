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
    public partial class frmCountingData_Sales : DevExpress.XtraEditors.XtraForm
    {
        public frmCountingData_Sales(DataTable dt, object SKUCode, object ItemName)
        {
            InitializeComponent();
            txtSKUCode.EditValue = SKUCode;
            txtItemName.EditValue = ItemName;
            gcCountingDetails.DataSource = dt;
        }

        private void frmCountingDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}