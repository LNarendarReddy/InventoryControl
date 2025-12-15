using DataAccess;
using DevExpress.XtraEditors;
using NSRetail.ReportForms.Supplier.SupplierReports;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NSRetail.Supplier
{
    public partial class frmSupplierItemsByBranchStock : DevExpress.XtraEditors.XtraForm
    {
        public event Action<SupplierRowModel> RowSelected;
        public SupplierRowModel SelectedRowData { get; private set; }
        public frmSupplierItemsByBranchStock(DataTable dt)
        {
            InitializeComponent();
            gcSupplierItems.DataSource = dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            gcSupplierItems.ShowRibbonPrintPreview();
        }

        private void gvSupplierItems_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gvSupplierItems.SetFocusedRowCellValue("IsAdded", 1);
                gvSupplierItems.PostEditor();
                gvSupplierItems.UpdateCurrentRow();
                gvSupplierItems.CloseEditor();
                e.Handled = true;
                SendSelectedRow();
            }
        }

        private void SendSelectedRow()
        {
            int rowHandle = gvSupplierItems.FocusedRowHandle;
            if (rowHandle < 0) return;

            var item = new SupplierRowModel
            {
                ItemCostPriceID = Convert.ToInt32(gvSupplierItems.GetRowCellValue(rowHandle, "ITEMCOSTPRICEID")),
                ItemCode = gvSupplierItems.GetRowCellValue(rowHandle, "ITEMCODE")?.ToString(),
                ItemName = gvSupplierItems.GetRowCellValue(rowHandle, "ITEMNAME")?.ToString(),
                MRP = Convert.ToDecimal(gvSupplierItems.GetRowCellValue(rowHandle, "MRP")),
                CostPrice = Convert.ToDecimal(gvSupplierItems.GetRowCellValue(rowHandle, "COSTPRICEWT")),
                Quantity = Convert.ToDecimal(gvSupplierItems.GetRowCellValue(rowHandle, "RETURNQUANTITY")),
                ReasonID = 2
            };

            RowSelected?.Invoke(item);
        }

        private void gvSupplierItems_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return; // Ignore header / group rows

            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            var isAdded = view.GetRowCellValue(e.RowHandle, "IsAdded");

            if (isAdded != null && isAdded.ToString() == "1")
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
                e.HighPriority = true;
            }
        }

    }

    public class SupplierRowModel
    {
        public  int ItemCostPriceID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal MRP { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Quantity { get; set; }

        public int ReasonID { get; set; }
    }
}