using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using NSRetail.Supplier;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockEntryPreview : DevExpress.XtraEditors.XtraForm
    {
        StockEntry ObjStockEntry = null;
        StockRepository ObjStockRep = new StockRepository();
        DataTable dtCN;

        public frmStockEntryPreview(StockEntry _ObjStockEntry)
        {
            InitializeComponent();
            ObjStockEntry = _ObjStockEntry;
            dtCN = buildCNDatatable();
        }

        private void frmStockEntryPreview_Load(object sender, EventArgs e)
        {
            DataTable dtBranch = Utility.GetBranchList();
            DataView dvBranch = dtBranch.Copy().DefaultView;
            dvBranch.RowFilter = "ISWAREHOUSE = 0";
            cmbBranch.Properties.DataSource = dvBranch;
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbCategory.Properties.DataSource = Utility.GetCategoryList();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

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
            txtTCS.EditValue = 0.00;
            txtTransport.EditValue = 0.00;
            txtPackingCharges.EditValue = 0.00;
            txtCreditValue.EditValue = 0.00;
            gcCreditNotes.DataSource = dtCN;
            if (ObjStockEntry.SourceBranchID != null && 
                Convert.ToInt32(ObjStockEntry.SourceBranchID) > 0 &&
                Convert.ToInt32(ObjStockEntry.SourceBranchID) != 45)
            {
                cmbBranch.EditValue = ObjStockEntry.SourceBranchID;
                cmbBranch.Enabled = false;
            }

            // Enable credit note mapping button
            btnCreditNoteMapping.Enabled = true;
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

                    if (cmbBranch.EditValue != null)
                    {
                        if (XtraMessageBox.Show($"Are you sure want to dispatch this invoice to {cmbBranch.Text}", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            return;

                        if (cmbCategory.EditValue == null)
                        {
                            XtraMessageBox.Show("Category mandatory for branch invoices");
                            return;
                        }
                    }
                    ObjStockEntry.DISCOUNTFLAT = txtDiscountFlat.EditValue;
                    ObjStockEntry.TCS = txtTCS.EditValue;
                    ObjStockEntry.TRANSPORT = txtTransport.EditValue;
                    ObjStockEntry.EXPENSES = txtExpenses.EditValue;
                    ObjStockEntry.PackingCharges = txtPackingCharges.EditValue;
                    ObjStockEntry.UserID = Utility.UserID;
                    ObjStockEntry.CATEGORYID = cmbCategory.EditValue;
                    ObjStockEntry.SourceBranchID = cmbBranch.EditValue;
                    ObjStockEntry.dtCreditNote = dtCN;
                    ObjStockEntry.Notes = txtNotes.EditValue;
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
                decimal.TryParse(Convert.ToString(txtNetPrice.EditValue), out decimal netPrice);
                decimal.TryParse(Convert.ToString(txtTransport.EditValue), out decimal transport);
                decimal.TryParse(Convert.ToString(txtCreditValue.EditValue), out decimal creditValue);
                decimal.TryParse(Convert.ToString(txtDiscountFlat.EditValue), out decimal discountFlat);
                decimal.TryParse(Convert.ToString(txtPackingCharges.EditValue), out decimal packingCharges);
                decimal.TryParse(Convert.ToString(txtTCS.EditValue), out decimal tcs);
                decimal.TryParse(Convert.ToString(txtExpenses.EditValue), out decimal Expenses);
                lblFinalPrice.Text = "Final Price: " + Convert.ToString(netPrice +
                    (ObjStockEntry.LorryFrightMode.Equals(1) ? transport : -transport) +
                    Expenses + packingCharges + tcs - creditValue - discountFlat);
            }
            catch (Exception ex) { }
        }

        private void btnCreditNoteMapping_Click_1(object sender, EventArgs e)
        {
            using (frmSelectCreditNote frm = new frmSelectCreditNote(ObjStockEntry.STOCKENTRYID, "SE", ObjStockEntry.SUPPLIERID))
            {
                frm.StartPosition = FormStartPosition.CenterParent;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Check if credit note ID already exists
                    bool creditNoteExists = false;
                    foreach (DataRow row in dtCN.Rows)
                    {
                        if (Convert.ToInt32(row["CreditNoteId"]) == frm.SelectedCreditNoteId)
                        {
                            creditNoteExists = true;
                            break;
                        }
                    }

                    if (creditNoteExists)
                    {
                        XtraMessageBox.Show("This credit note is already mapped to this invoice.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    DataRow newRow = dtCN.NewRow();
                    newRow["CreditNoteId"] = frm.SelectedCreditNoteId;
                    newRow["CNNumber"] = frm.SelectedCNNumber;
                    newRow["CreditValue"] = frm.SelectedCreditValue;
                    newRow["AdjustmentType"] = frm.SelectedAdjustmentType;
                    dtCN.Rows.Add(newRow);

                    // Calculate sum of credit values
                    decimal creditValueSum = 0;
                    foreach (DataRow row in dtCN.Rows)
                    {
                        if (decimal.TryParse(Convert.ToString(row["CreditValue"]), out decimal creditValue))
                        {
                            creditValueSum += creditValue;
                        }
                    }
                    // Update UI with sum
                    txtCreditValue.EditValue = creditValueSum;

                    // Refresh grid data source
                    gcCreditNotes.RefreshDataSource();
                }
            }
        }

        private DataTable buildCNDatatable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CreditNoteId", typeof(int));
            dt.Columns.Add("CNNumber", typeof(string));
            dt.Columns.Add("CreditValue", typeof(decimal));
            dt.Columns.Add("AdjustmentType", typeof(string));
            return dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int focusedRowHandle = gvCreditNotes.FocusedRowHandle;
                
                if (focusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a row to delete.", "Information", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (XtraMessageBox.Show("Are you sure you want to delete this credit note?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                // Delete row from DataTable
                DataRow rowToDelete = dtCN.Rows[focusedRowHandle];
                dtCN.Rows.Remove(rowToDelete);

                // Refresh grid
                gcCreditNotes.RefreshDataSource();

                // Recalculate sum of credit values
                decimal creditValueSum = 0;
                foreach (DataRow row in dtCN.Rows)
                {
                    if (decimal.TryParse(Convert.ToString(row["CreditValue"]), out decimal creditValue))
                    {
                        creditValueSum += creditValue;
                    }
                }

                // Update UI with new sum
                txtCreditValue.EditValue = creditValueSum;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}