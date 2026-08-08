using DataAccess;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmItemHistory : XtraForm
    {
        private readonly object itemID;
        private readonly string skuCode;
        private readonly string itemName;

        public frmItemHistory(object itemID, string skuCode, string itemName)
        {
            InitializeComponent();

            this.itemID = itemID;
            this.skuCode = skuCode;
            this.itemName = itemName;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmItemHistory_Load(object sender, EventArgs e)
        {
            Text = $"Item History - {skuCode} - {itemName}";
            LoadHistory();
        }

        private void LoadHistory()
        {
            try
            {
                DataTable dtHistory = new ItemCodeRepository().GetItemHistory(itemID);
                gcItemHistory.DataSource = dtHistory;
                gvItemHistory.BestFitColumns();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItemHistory.ShowRibbonPrintPreview();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
