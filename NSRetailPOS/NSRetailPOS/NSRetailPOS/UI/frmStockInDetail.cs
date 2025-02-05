using System;
using System.Data;
using DevExpress.XtraEditors;
using NSRetailPOS.Data;

namespace NSRetailPOS.UI
{
    public partial class frmStockInDetail : XtraForm
    {
        DataRowView selectedDispatch;
        public bool IsSave { get; private set; }

        public frmStockInDetail(object selectedStockDispatch)
        {
            InitializeComponent();
            selectedDispatch = selectedStockDispatch as DataRowView;

            Utility.SetGridFormatting(gvDispatchDetail);
        }

        private void frmStockInDetail_Load(object sender, System.EventArgs e)
        {
            txtDispatchNumber.EditValue = selectedDispatch["DISPATCHNUMBER"];
            dtCreatedDate.EditValue = selectedDispatch["CREATEDDATE"];
            txtApprovedBy.EditValue = selectedDispatch["STATUSAPPROVEDBY"];
            dtApprovedDate.EditValue = selectedDispatch["STATUSAPPROVEDDATE"];

            gcDispatchDetail.DataSource = new StockInRepository().GetStockDispatchDetail(selectedDispatch["STOCKDISPATCHID"]);
            btnAddStock.Enabled = selectedDispatch["STATUS"].Equals("1");
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
            dtDispatchDetail.Columns.Remove("WEIGHTINKGS");
            new StockInRepository().AcceptDispatch(selectedDispatch["STOCKDISPATCHID"], dtDispatchDetail);
            IsSave = true;
            this.Close();
        }

        private void gvDispatchDetail_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (bool.TryParse(Convert.ToString(gvDispatchDetail.GetFocusedRowCellValue("ISOPENITEM")), out bool bValue) && bValue)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}