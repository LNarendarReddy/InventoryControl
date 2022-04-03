using DataAccess;
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
    public partial class frmItemwiseSales : DevExpress.XtraEditors.XtraForm
    {
        public frmItemwiseSales()
        {
            InitializeComponent();
        }

        private void frmItemwiseSales_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btbGenerate_Click(object sender, EventArgs e)
        {
            gcItems.DataSource = new POSRepository().GetItemwiseSales(cmbBranch.EditValue);
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void frmItemwiseSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}