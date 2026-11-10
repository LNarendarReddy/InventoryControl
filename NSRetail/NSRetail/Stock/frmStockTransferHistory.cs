using DataAccess;
using DevExpress.XtraEditors;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockTransferHistory : XtraForm
    {
        private readonly int branchId;
        private readonly int? transferId;

        public frmStockTransferHistory(int branchId, int? transferId = null)
        {
            InitializeComponent();
            this.branchId = branchId;
            this.transferId = transferId;
            Text = transferId.HasValue ? "Stock Transfer #" + transferId + " - Details" : "Stock Transfer Batches";
            detailsItem.Visibility = transferId.HasValue
                ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        private void frmStockTransferHistory_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable data = transferId.HasValue ? new StockTransferRepository().GetDetails(transferId.Value)
                    : new StockTransferRepository().GetBatches(branchId);
                gcHistory.DataSource = data;
                gvHistory.PopulateColumns();
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in gvHistory.Columns)
                    column.Caption = Caption(column.FieldName);
                gvHistory.BestFitColumns();
            }
            catch (Exception ex) { ErrorMgmt.ShowError(ex); AppLog.Error(ex); }
        }

        private static string Caption(string field)
        {
            switch (field)
            {
                case "STOCKTRANSFERID": return "Batch ID";
                case "STOCKADJUSTMENTTYPE": return "Adjustment Type";
                case "BRANCHNAME": return "Warehouse";
                case "CREATEDBY": return "Created By";
                case "CREATEDDATE": return "Created Date";
                case "TRANSFERTYPE": return "Transfer Type";
                case "ROWNO": return "Pair";
                default:
                    return field.Replace("SOURCE", "Source ").Replace("DESTINATION", "Destination ")
                        .Replace("WEIGHTINKGS", "Weight (kg)").Replace("QUANTITY", "Quantity")
                        .Replace("ADJUSTMENTID", "Adjustment ID").Replace("ITEM", "Item ").Replace("EAN", "EAN")
                        .Replace("FACTOR", "Factor").Replace("PAIRS", "Pairs").Replace("REMARKS", "Remarks");
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (transferId.HasValue || gvHistory.FocusedRowHandle < 0) return;
            using (frmStockTransferHistory form = new frmStockTransferHistory(branchId,
                Convert.ToInt32(gvHistory.GetFocusedRowCellValue("STOCKTRANSFERID"))))
                form.ShowDialog(this);
        }

        private void gvHistory_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (transferId.HasValue || !e.HitInfo.InRow || e.Menu == null) return;
            gvHistory.FocusedRowHandle = e.HitInfo.RowHandle;
            e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("View Details", btnDetails_Click));
        }

        private void btnClose_Click(object sender, EventArgs e) { Close(); }
    }
}
