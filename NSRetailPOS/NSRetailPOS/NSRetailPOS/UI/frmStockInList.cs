using DevExpress.XtraEditors;
using NSRetailPOS.Data;

namespace NSRetailPOS.UI
{
    public partial class frmStockInList : XtraForm
    {
        public frmStockInList()
        {
            InitializeComponent();
        }

        private void frmStockIn_Load(object sender, System.EventArgs e)
        {
            gcStockInList.DataSource = new StockInRepository().GetStockDispatches(Utility.branchinfo.BranchID);
        }

        private void btnStockIn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}
