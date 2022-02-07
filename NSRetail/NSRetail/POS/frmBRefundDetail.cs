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
    public partial class frmBRefundDetail : DevExpress.XtraEditors.XtraForm
    {
        public frmBRefundDetail(DataTable dtItems)
        {
            InitializeComponent();
            gcItems.DataSource = dtItems;
        }
        private void frmBRefundDetail_Load(object sender, EventArgs e)
        {

        }
    }
}