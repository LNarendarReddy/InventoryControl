using DataAccess;
using System;
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
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }

        private void btbGenerate_Click(object sender, EventArgs e)
        {
            gcItems.DataSource = new POSRepository().GetItemwiseSales(cmbBranch.EditValue,
                dtpFromDate.EditValue,dtpToDate.DateTime.AddDays(1), chkIncludeDate.EditValue);
            gColBillDate.Visible = Convert.ToBoolean(chkIncludeDate.EditValue);
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