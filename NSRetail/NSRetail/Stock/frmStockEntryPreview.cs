using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Entity;
using ErrorManagement;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockEntryPreview : DevExpress.XtraEditors.XtraForm
    {
        StockEntry ObjStockEntry = null;
        StockRepository ObjStockRep = new StockRepository();
        public frmStockEntryPreview(StockEntry _ObjStockEntry)
        {
            InitializeComponent();
            ObjStockEntry = _ObjStockEntry;
        }

        private void frmStockEntryPreview_Load(object sender, EventArgs e)
        {
            DataTable dtSupplier = new MasterRepository().GetDealer();
            cmbSupplier.Properties.DataSource = dtSupplier;
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            cmbSupplier.EditValue = ObjStockEntry.SUPPLIERID;
            txtInvoiceNumber.EditValue = ObjStockEntry.SUPPLIERINVOICENO;
            dtpInvoice.EditValue = ObjStockEntry.InvoiceDate;
            txtTotalPriceWT.EditValue = ObjStockEntry.SumTotalPriceWT;
            txtTotalPriceWOT.EditValue = ObjStockEntry.SumTotalPriceWOT;
            txtGSTValue.EditValue = ObjStockEntry.SumGSTValue;
            txtTaxValue.EditValue = ObjStockEntry.SumTaxValue;
            txtCessValue.EditValue = ObjStockEntry.SumCessValue;
            txtNetPrice.EditValue = ObjStockEntry.SumFinalPrice;
            txtExpenses.EditValue = 0.00;
            txtDiscountFlat.EditValue = 0.00;
            txtDiscountPer.EditValue = 0.00;
            txtTCS.EditValue = 0.00;
            txtTransport.EditValue = 0.00;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int iValue = 0;
                if (int.TryParse(Convert.ToString(ObjStockEntry.STOCKENTRYID), out iValue) && iValue > 0)
                {
                    if (!dxValidationProvider1.Validate() ||
                        XtraMessageBox.Show("Are you sure want to save invoice?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                    ObjStockEntry.DISCOUNTPER = txtDiscountPer.EditValue;
                    ObjStockEntry.DISCOUNTFLAT = txtDiscountFlat.EditValue;
                    ObjStockEntry.TCS = txtTCS.EditValue;
                    ObjStockEntry.TRANSPORT = txtTransport.EditValue;
                    ObjStockEntry.EXPENSES = txtExpenses.EditValue;
                    
                    ObjStockRep.UpdateInvoice(ObjStockEntry);
                    ObjStockEntry.IsSave = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtExpenses_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                TextEdit txt = sender as TextEdit;
                decimal.TryParse(Convert.ToString(txtNetPrice.EditValue), out decimal NetPrice);
                decimal.TryParse(Convert.ToString(txtTCS.EditValue), out decimal TCS);
                decimal.TryParse(Convert.ToString(txtTransport .EditValue), out decimal Transport);
                decimal.TryParse(Convert.ToString(txtDiscountPer.EditValue), out decimal DiscountPer);
                decimal.TryParse(Convert.ToString(txtDiscountFlat.EditValue), out decimal DiscountFlat);
                decimal.TryParse(Convert.ToString(txtExpenses.EditValue), out decimal Expenses);
                txtFinalPrice.EditValue = NetPrice + TCS + Transport + Expenses - DiscountFlat - ((NetPrice * DiscountPer) / 100); ;
            }
            catch (Exception ex){}
        }
    }
}