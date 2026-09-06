using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmInvoiceSettings : DevExpress.XtraEditors.XtraForm
    {
        public StockEntry StockEntryObject { get; set; }

        private readonly MasterRepository masterRepository = new MasterRepository();
        private readonly StockRepository stockRepository = new StockRepository();
        private bool isLoading;

        public frmInvoiceSettings(StockEntry stockEntry = null)
        {
            InitializeComponent();
            StockEntryObject = stockEntry ?? new StockEntry();
        }

        private void frmInvoiceSettings_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;
                LoadLookupData();
                BindStockEntryObject();
                lblSupplierInfo.Text = !IsNullValue(StockEntryObject.STOCKENTRYID) && Convert.ToInt32(StockEntryObject.STOCKENTRYID) > 0
                    ? $"Draft Invoice: {StockEntryObject.SUPPLIERINVOICENO}"
                    : "New Invoice";
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
            finally
            {
                isLoading = false;
                try
                {
                    LoadSupplierIndents();
                    SetSupplierGSTIN();
                }
                catch (Exception ex)
                {
                    ErrorMgmt.ShowError(ex);
                }
            }
        }

        private void LoadLookupData()
        {
            cmbSupplier.Properties.DataSource = masterRepository.GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            cmbCategory.Properties.DataSource = Utility.GetCategoryListExceptAll();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
        }

        private void BindStockEntryObject()
        {
            cmbSupplier.EditValue = StockEntryObject.SUPPLIERID;
            txtInvoiceNumber.EditValue = StockEntryObject.SUPPLIERINVOICENO;
            dtpInvoice.EditValue = StockEntryObject.InvoiceDate ?? DateTime.Now;
            cmbCategory.EditValue = StockEntryObject.CATEGORYID ?? Utility.CategoryID;
            cmbSupplierIndent.EditValue = StockEntryObject.SupplierIndentId;
            rgInvoiceType.EditValue = IsNullValue(StockEntryObject.InvoiceType) ? 1 : StockEntryObject.InvoiceType;
            rgPriceEntryMethod.EditValue = IsNullValue(StockEntryObject.PriceEntryMethod) ? 1 : StockEntryObject.PriceEntryMethod;
            rgLorryFrightMode.EditValue = IsNullValue(StockEntryObject.LorryFrightMode) ? 1 : StockEntryObject.LorryFrightMode;
        }

        private bool IsNullValue(object value)
        {
            return value == null || value == DBNull.Value;
        }

        private void SetSupplierGSTIN()
        {
            if (cmbSupplier.EditValue == null)
            {
                txtGSTIN.EditValue = null;
                StockEntryObject.SupplierGSTIN = null;
                StockEntryObject.CalculateIGST = false;
                return;
            }

            txtGSTIN.EditValue = cmbSupplier.GetColumnValue("GSTIN");
            StockEntryObject.SupplierGSTIN = txtGSTIN.EditValue;
            StockEntryObject.SUPPLIERNAME = cmbSupplier.Text;
            StockEntryObject.CalculateIGST = !txtGSTIN.Text.StartsWith("37");
        }

        private void LoadSupplierIndents()
        {
            if (isLoading || cmbSupplier.EditValue == null || cmbCategory.EditValue == null)
                return;

            DataTable dtSupplierIndent = stockRepository.GetSupplierIndentList(cmbSupplier.EditValue, cmbCategory.EditValue);
            string valueMember = GetFirstColumn(dtSupplierIndent, "SUPPLIERINDENTID", "SupplierIndentID", "SupplierIndentId");
            string displayMember = GetFirstColumn(dtSupplierIndent, "SUPPLIERINDENTNO", "SupplierIndentNo", "INDENTNO", "IndentNo");

            cmbSupplierIndent.Properties.DataSource = dtSupplierIndent;
            cmbSupplierIndent.Properties.ValueMember = valueMember ?? string.Empty;
            cmbSupplierIndent.Properties.DisplayMember = displayMember ?? string.Empty;
            cmbSupplierIndent.EditValue = StockEntryObject.SupplierIndentId;
        }

        private string GetFirstColumn(DataTable dataTable, params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (dataTable.Columns.Contains(columnName))
                    return columnName;
            }

            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.EditValue == null)
            {
                XtraMessageBox.Show("Supplier cannot be empty");
                return;
            }

            if (cmbCategory.EditValue == null)
            {
                XtraMessageBox.Show("Category cannot be empty");
                return;
            }

            if (txtInvoiceNumber.EditValue == null || string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                XtraMessageBox.Show("Supplier invoice # cannot be empty");
                return;
            }

            if (dtpInvoice.EditValue == null)
            {
                XtraMessageBox.Show("Invoice date cannot be empty");
                return;
            }

            StockEntryObject.SUPPLIERID = cmbSupplier.EditValue;
            StockEntryObject.SUPPLIERNAME = cmbSupplier.Text;
            StockEntryObject.SupplierGSTIN = txtGSTIN.EditValue;
            StockEntryObject.SUPPLIERINVOICENO = txtInvoiceNumber.EditValue;
            StockEntryObject.InvoiceDate = dtpInvoice.EditValue;
            StockEntryObject.CATEGORYID = cmbCategory.EditValue;
            StockEntryObject.SupplierIndentId = cmbSupplierIndent.EditValue;
            StockEntryObject.SupplierIndentNo = cmbSupplierIndent.Text;
            StockEntryObject.InvoiceType = rgInvoiceType.EditValue;
            StockEntryObject.PriceEntryMethod = rgPriceEntryMethod.EditValue;
            StockEntryObject.LorryFrightMode = rgLorryFrightMode.EditValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmbSupplier_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetSupplierGSTIN();
                LoadSupplierIndents();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSupplierIndents();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }
    }
}
