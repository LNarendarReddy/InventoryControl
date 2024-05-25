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
    public partial class frmExport : DevExpress.XtraEditors.XtraForm
    {
        public frmExport(DataTable dtItems)
        {
            InitializeComponent();
            gcItems.DataSource = dtItems;
        }

        private void frmExport_Load(object sender, EventArgs e)
        {
            
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}