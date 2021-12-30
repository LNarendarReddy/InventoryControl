using System.Data;
using DevExpress.XtraEditors;
using NSRetailPOS.Data;

namespace NSRetailPOS.UI
{
    public partial class frmStockInDetail : XtraForm
    {
        DataRowView selectedDispatch;

        public frmStockInDetail(object selectedStockDispatch)
        {
            InitializeComponent();
            selectedDispatch = selectedStockDispatch as DataRowView;
        }

        private void frmStockInDetail_Load(object sender, System.EventArgs e)
        {
            txtDispatchNumber.EditValue = selectedDispatch["DISPATCHNUMBER"];
            dtCreatedDate.EditValue = selectedDispatch["CREATEDDATE"];
            txtApprovedBy.EditValue = selectedDispatch["STATUSAPPROVEDBY"];
            dtApprovedDate.EditValue = selectedDispatch["STATUSAPPROVEDDATE"];

            gcDispatchDetail.DataSource = new StockInRepository().GetStockDispatchDetail(selectedDispatch["STOCKDISPATCHID"]);
        }

        private void btnAddStock_Click(object sender, System.EventArgs e)
        {
            DataTable dtDispatchDetail = (gcDispatchDetail.DataSource as DataTable).Copy();
            dtDispatchDetail.Columns.Remove("ITEMNAME");
            dtDispatchDetail.Columns.Remove("ITEMCODE");
            dtDispatchDetail.Columns.Remove("MRP");
            dtDispatchDetail.Columns.Remove("SALEPRICE");
            dtDispatchDetail.Columns.Remove("GSTCODE");
            dtDispatchDetail.Columns.Remove("TRAYNUMBER");
            dtDispatchDetail.Columns.Remove("DISPATCHQUANTITY");
            new StockInRepository().AcceptDispatch(selectedDispatch["STOCKDISPATCHID"], dtDispatchDetail);
            this.Close();
        }
    }
}