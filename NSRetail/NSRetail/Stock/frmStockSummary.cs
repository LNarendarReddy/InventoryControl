using System;
using DataAccess;

namespace NSRetail.Stock
{
    [Obsolete]
    public partial class frmStockSummary : DevExpress.XtraEditors.XtraForm
    {
        public frmStockSummary()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            gcStockSummary.DataSource = new StockRepository().GetStockSummary(luBranch.EditValue, sluItem.EditValue);
        }

        private void frmStockSummary_Load(object sender, EventArgs e)
        {
            luBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            luBranch.Properties.ValueMember = "BRANCHID";
            luBranch.Properties.DisplayMember = "BRANCHNAME";
            luBranch.EditValue = 0;

            sluItem.Properties.DataSource = Utility.GetItemSKUList();
            sluItem.Properties.ValueMember = "ITEMID";
            sluItem.Properties.DisplayMember = "ITEMNAME";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcStockSummary.ShowRibbonPrintPreview();
        }
    }
}