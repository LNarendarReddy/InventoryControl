using DataAccess;
using DevExpress.XtraEditors;
using System;

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class frmBranchWiseStock : XtraForm
    {
        private readonly object itemPriceID;

        public frmBranchWiseStock(object itemPriceID)
        {
            InitializeComponent();
            this.itemPriceID = itemPriceID;
        }

        private void frmBranchWiseStock_Load(object sender, EventArgs e)
        {
            gcBranchWiseStock.DataSource = new ItemCodeRepository().GetStockByBranch(itemPriceID);
        }
    }
}