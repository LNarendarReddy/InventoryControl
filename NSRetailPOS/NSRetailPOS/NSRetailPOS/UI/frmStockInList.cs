using DevExpress.XtraEditors;
using NSRetailPOS.Data;

namespace NSRetailPOS.UI
{
    public partial class frmStockInList : XtraForm
    {
        public frmStockInList()
        {
            InitializeComponent();
            this.gvStockInList.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvStockInList.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvStockInList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvStockInList.Appearance.FocusedCell.Options.UseFont = true;
            this.gvStockInList.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvStockInList.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvStockInList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvStockInList.Appearance.FocusedRow.Options.UseFont = true;
            this.gvStockInList.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvStockInList.Appearance.FooterPanel.Options.UseFont = true;
            this.gvStockInList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvStockInList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvStockInList.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvStockInList.Appearance.Row.Options.UseFont = true;
        }

        private void frmStockIn_Load(object sender, System.EventArgs e)
        {
            gcStockInList.DataSource = new StockInRepository().GetStockDispatches(Utility.branchinfo.BranchID);
        }

        private void btnStockIn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(gvStockInList.FocusedRowHandle < 0)
            { 
                return;
            }

            frmStockInDetail stockInDetailForm = new frmStockInDetail(gvStockInList.GetFocusedRow());
            stockInDetailForm.ShowDialog();
            if(stockInDetailForm.IsSave)
            {
                gcStockInList.DataSource = new StockInRepository().GetStockDispatches(Utility.branchinfo.BranchID);
            }
        }
    }
}
