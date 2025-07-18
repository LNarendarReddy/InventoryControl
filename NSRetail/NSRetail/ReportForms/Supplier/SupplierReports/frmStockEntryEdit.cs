using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class frmStockEntryEdit : DevExpress.XtraEditors.XtraForm
    {
        private readonly object stockEntryID;

        public frmStockEntryEdit(object stockEntryID)
        {
            InitializeComponent();
            this.stockEntryID = stockEntryID;
        }

        private void frmStockEntryEdit_Load(object sender, EventArgs e)
        {
            DataTable dtStockEntry = new ReportRepository().GetReportData("USP_PR_STOCKENTRY", new Dictionary<string, object> { { "STOCKENTRYID", stockEntryID } });

            cmbSupplierID.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplierID.Properties.DisplayMember = "DEALERNAME";
            cmbSupplierID.Properties.ValueMember = "DEALERID";

            cmbSupplierID.EditValue = dtStockEntry.Rows[0]["SUPPLIERID"];
            txtInvoiceNumber.EditValue = dtStockEntry.Rows[0]["SUPPLIERINVOICENO"];
            dtInvoiceDate.EditValue = dtStockEntry.Rows[0]["INVOICEDATE"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            StockEntry stockEntry = new StockEntry()
            {
                STOCKENTRYID = stockEntryID,
                SUPPLIERID = cmbSupplierID.EditValue,
                SUPPLIERINVOICENO = txtInvoiceNumber.EditValue,
                InvoiceDate = dtInvoiceDate.EditValue,
                UserID = Utility.UserID
            };
            try
            {
                new StockRepository().SavePartialInvoiceData(stockEntry);
                XtraMessageBox.Show("Save succesful", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"error occurred : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}