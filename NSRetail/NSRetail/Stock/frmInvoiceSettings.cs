using Entity;
using System;

namespace NSRetail.Stock
{
    public partial class frmInvoiceSettings : DevExpress.XtraEditors.XtraForm
    {
        public StockEntry StockEntryObject { get; set; }

        public frmInvoiceSettings(StockEntry stockEntry = null)
        {
            InitializeComponent();
            StockEntryObject = stockEntry;
        }

        private void frmInvoiceSettings_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(StockEntryObject.STOCKENTRYID) > 0)
            {
                lblSupplierInfo.Text = 
                    $"Supplier: {StockEntryObject.SUPPLIERNAME} | Invoice #: {StockEntryObject.SUPPLIERINVOICENO}";
                rgInvoiceType.EditValue = StockEntryObject.InvoiceType == DBNull.Value ? 1 : StockEntryObject.InvoiceType;
                rgPriceEntryMethod.EditValue = StockEntryObject.PriceEntryMethod == DBNull.Value ? 1 : StockEntryObject.PriceEntryMethod;
                rgLorryFrightMode.EditValue = StockEntryObject.LorryFrightMode == DBNull.Value ? 1 : StockEntryObject.LorryFrightMode;
            }
            else
            {
                lblSupplierInfo.Text = "New Invoice";
                rgInvoiceType.EditValue = 1;
                rgPriceEntryMethod.EditValue = 1;
                rgLorryFrightMode.EditValue = 1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            StockEntryObject.InvoiceType = rgInvoiceType.EditValue;
            StockEntryObject.PriceEntryMethod = rgPriceEntryMethod.EditValue;
            StockEntryObject.LorryFrightMode = rgLorryFrightMode.EditValue;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}