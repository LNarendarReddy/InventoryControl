using System.Data;
using System.Linq;

namespace NSRetail.Stock
{
    public partial class frmSupplierIndentItems : DevExpress.XtraEditors.XtraForm
    {
        private readonly DataTable dtIndentItems;

        public frmSupplierIndentItems(DataTable dtIndentItems)
        {
            InitializeComponent();
            this.dtIndentItems = dtIndentItems;
        }

        private void frmSupplierIndentItems_Load(object sender, System.EventArgs e)
        {
            gcIndentItems.DataSource = dtIndentItems;
            HideUnexpectedColumns();
            gvIndentItems.BestFitColumns();
        }

        private void HideUnexpectedColumns()
        {
            string[] visibleColumns =
            {
                "SNO",
                "SKUCODE",
                "ITEMNAME",
                "SUBCATEGORYNAME",
                "MRP",
                "COSTPRICEWT",
                "DESIREDINDENT",
                "ENTEREDQUANTITY",
                "STATUS"
            };

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gvIndentItems.Columns)
            {
                column.Visible = visibleColumns.Contains(column.FieldName);
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
