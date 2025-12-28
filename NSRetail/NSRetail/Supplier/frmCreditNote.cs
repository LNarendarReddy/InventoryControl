using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Supplier
{
    public partial class frmCreditNote : XtraForm
    {
        private int _creditNoteId = 0;

        public frmCreditNote()
        {
            InitializeComponent();
        }

        public frmCreditNote(int creditNoteId) : this()
        {
            _creditNoteId = creditNoteId;
        }

        private void frmCreditNote_Load(object sender, EventArgs e)
        {
            SetMandatoryValidation();
            SetNumericMask();
            LoadLookups();

            if (_creditNoteId > 0)
                LoadCreditNote();
        }

        private void SetMandatoryValidation()
        {
            var rule = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule
            {
                ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank,
                ErrorText = "Mandatory",
                ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical
            };

            dxValidationProvider11.SetValidationRule(cmbSupplier, rule);
            dxValidationProvider11.SetValidationRule(cmbCreditAdjustmentType, rule);
            dxValidationProvider11.SetValidationRule(txtCNNumber, rule);
            dxValidationProvider11.SetValidationRule(txtCNValue, rule);
        }

        private void SetNumericMask()
        {
            txtCNValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtCNValue.Properties.Mask.EditMask = "n2";
            txtCNValue.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void LoadLookups()
        {
            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(false);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            // Credit Adjustment Type
            cmbCreditAdjustmentType.Properties.DataSource =
                new CreditNoteRepository().GetCreditNoteAdjustmentTypes();
            cmbCreditAdjustmentType.Properties.DisplayMember = "Description";
            cmbCreditAdjustmentType.Properties.ValueMember = "CreditNoteAdjustmentTypeId";

            cmbPurchaseInvoiceNumber.Properties.DisplayMember = "SUPPLIERINVOICENO";
            cmbPurchaseInvoiceNumber.Properties.ValueMember = "StockEntryId";
        }

        private void LoadCreditNote()
        {
            DataTable dt = new CreditNoteRepository().GetCreditNoteById(_creditNoteId);
            if (dt.Rows.Count == 0) return;

            DataRow dr = dt.Rows[0];
            this.Tag = _creditNoteId;

            cmbSupplier.EditValue = dr["SupplierId"];
            cmbCreditAdjustmentType.EditValue = dr["CreditNoteAdjustmentTypeId"];
            cmbPurchaseInvoiceNumber.EditValue = dr["StockEntryId"];
            txtCNNumber.EditValue = dr["CNNumber"];
            txtCNValue.EditValue = dr["CreditValue"];
            txtDescription.EditValue = dr["Description"];

            if (cmbPurchaseInvoiceNumber.EditValue != null)
                txtCNNumber.Enabled = false;
        }

        private void cmbPurchaseInvoiceNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPurchaseInvoiceNumber.EditValue != null)
            {
                txtCNNumber.EditValue = cmbPurchaseInvoiceNumber.Text;
                txtCNNumber.Enabled = false;
            }
            else
            {
                txtCNNumber.Enabled = true;
                txtCNNumber.EditValue = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider11.Validate())
                return;

            try
            {
                CreditNote creditNote = new CreditNote
                {
                    CreditNoteId = this.Tag ?? 0,
                    CNNumber = txtCNNumber.EditValue,
                    Description = txtDescription.EditValue,
                    SupplierId = cmbSupplier.EditValue,
                    CreditValue = txtCNValue.EditValue,
                    CreditNoteAdjustmentTypeId = cmbCreditAdjustmentType.EditValue,
                    StockEntryId = cmbPurchaseInvoiceNumber.EditValue,
                    UserId = Utility.UserID
                };

                new CreditNoteRepository().SaveCreditNote(creditNote);

                XtraMessageBox.Show(
                    "Credit Note saved successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSupplier_EditValueChanged(object sender, EventArgs e)
        {
            cmbPurchaseInvoiceNumber.Properties.DataSource =
                new CreditNoteRepository().GetPurchaseInvoices(cmbSupplier.EditValue);

            cmbPurchaseInvoiceNumber.EditValue = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbPurchaseInvoiceNumber.EditValue = null;
        }
    }
}
