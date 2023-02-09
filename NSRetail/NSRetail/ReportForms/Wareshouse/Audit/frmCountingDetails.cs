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

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class frmCountingDetails : DevExpress.XtraEditors.XtraForm
    {
        public frmCountingDetails(DataTable dt)
        {
            InitializeComponent();
            txtSKUCode.EditValue = dt.Rows[0]["SKUCODE"];
            txtItemName.EditValue = dt.Rows[0]["ITEMNAME"];
            gcCountingDetails.DataSource = dt;
        }

        private void frmCountingDetails_Load(object sender, EventArgs e)
        {
            
        }

        private void frmCountingDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}