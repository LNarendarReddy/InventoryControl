using System.Data;

namespace NSRetailPOS.UI
{
    public partial class frmBillOfferSelection : DevExpress.XtraEditors.XtraForm
    {
        public bool IsItemSelected { get; set; }
        public object SelectedItemPriceID
        {
            get => gvMRPList.GetFocusedRowCellValue("ITEMPRICEID");
            set;
        }
        public frmBillOfferSelection(DataTable data)
        {
            InitializeComponent();
            gcMRPList.DataSource = data;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            IsItemSelected = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}