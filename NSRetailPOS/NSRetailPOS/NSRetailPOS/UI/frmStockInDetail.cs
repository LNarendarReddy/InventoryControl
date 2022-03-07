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
            this.gvDispatchDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.SaddleBrown;
            this.gvDispatchDetail.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gvDispatchDetail.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvDispatchDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvDispatchDetail.Appearance.FocusedCell.Options.UseFont = true;
            this.gvDispatchDetail.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvDispatchDetail.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvDispatchDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gvDispatchDetail.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvDispatchDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDispatchDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvDispatchDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.gvDispatchDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvDispatchDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDispatchDetail.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvDispatchDetail.Appearance.Row.Options.UseFont = true;
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
            IsSave = true;
            this.Close();
        }
    }
}