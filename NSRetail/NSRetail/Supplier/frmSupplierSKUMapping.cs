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

namespace NSRetail.Supplier
{
    public partial class frmSupplierSKUMapping : DevExpress.XtraEditors.XtraForm
    {
        public event Action<SupplierSKUMapping, int> RowSelected;

        public SupplierSKUMapping SelectedRowData { get; private set; }
        public frmSupplierSKUMapping(DataTable dataTable)
        {
            InitializeComponent();
            gcSupplierItems.DataSource = dataTable;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvSupplierItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                int rowHandle = gvSupplierItems.FocusedRowHandle;
                if (rowHandle < 0) return;

                string itemName = gvSupplierItems.GetRowCellValue(rowHandle, "ITEMNAME")?.ToString();

                var result = XtraMessageBox.Show($"Do you want to map the item:\n\n{itemName} ?","Confirm Mapping",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SendSelectedRow();
                }
            }
        }

        private void SendSelectedRow()
        {
            int rowHandle = gvSupplierItems.FocusedRowHandle;
            if (rowHandle < 0) return;

            var item = new SupplierSKUMapping
            {
                ItemID = Convert.ToInt32(gvSupplierItems.GetRowCellValue(rowHandle, "ITEMID")),
                SKUCode = gvSupplierItems.GetRowCellValue(rowHandle, "SKUCODE")?.ToString(),
                ItemName = gvSupplierItems.GetRowCellValue(rowHandle, "ITEMNAME")?.ToString(),
                CategoryName = gvSupplierItems.GetRowCellValue(rowHandle, "CATEGORYNAME")?.ToString(),
                SubCategoryName = gvSupplierItems.GetRowCellValue(rowHandle, "SUBCATEGORYNAME")?.ToString(),
                BrandName = gvSupplierItems.GetRowCellValue(rowHandle, "BRANDNAME")?.ToString(),
                ManufacturerName = gvSupplierItems.GetRowCellValue(rowHandle, "MANUFACTURERNAME")?.ToString()
            };

            RowSelected?.Invoke(item, rowHandle);
        }

        public void DeleteRow(int rowHandle)
        {
            if (rowHandle < 0) return;

            gvSupplierItems.DeleteRow(rowHandle);

            int newHandle = rowHandle - 1;

            if (newHandle < 0 && gvSupplierItems.RowCount > 0)
                newHandle = 0;

            if (newHandle >= 0 && newHandle < gvSupplierItems.RowCount)
                gvSupplierItems.FocusedRowHandle = newHandle;
        }



    }
    public class SupplierSKUMapping
    {
        public int ItemID { get; set; }
        public string SKUCode { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public string ManufacturerName { get; set; }

    }
}