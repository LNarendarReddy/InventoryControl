using NSRetailPOS.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetailPOS.Operations.Stock
{
    public partial class frmDraftStockEntries : DevExpress.XtraEditors.XtraForm
    {
        public object StockEntryID { get; set; }

        public frmDraftStockEntries()
        {
            InitializeComponent();
        }

        private void frmDraftStockEntries_Load(object sender, EventArgs e)
        {
            DataTable dtDraftEntries = new ReportRepository().GetReportData("USP_R_DRAFTSTOCKENTRIES"
                , new Dictionary<string, object>() { { "UserID", Utility.loginInfo.UserID } });
            gcDraftEntries.DataSource = dtDraftEntries;
        }

        private void gcDraftEntries_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            StockEntryID = gvDraftEntries.GetFocusedRowCellValue("STOCKENTRYID");
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }
}