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
    public partial class frmTaxwisesale : DevExpress.XtraEditors.XtraForm
    {
        public frmTaxwisesale()
        {
            InitializeComponent();
        }
        private void frmTaxwisesale_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            gcTaxwiseSale.DataSource = new POSRepository().GetTaxWiseSales(cmbBranch.EditValue);
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcTaxwiseSale.ShowRibbonPrintPreview();
        }
    }
}