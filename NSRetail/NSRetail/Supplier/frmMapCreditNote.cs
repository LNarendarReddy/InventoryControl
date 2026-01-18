using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace NSRetail.Supplier
{
    public partial class frmMapCreditNote : DevExpress.XtraEditors.XtraForm
    {
        public int SelectedCreditNoteId { get; private set; }
        public decimal SelectedCreditValue { get; private set; }

        public string SelectedCNNumber { get; private set; }

        private object _supplierRefId;
        private string _refType;

        public frmMapCreditNote(object supplierRefId, string refType)
        {
            InitializeComponent();
            _supplierRefId = supplierRefId;
            _refType = refType;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCreditNote();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmMapCreditNote_Load(object sender, EventArgs e)
        {
            gcCreditNotes.DataSource = new CreditNoteRepository().GetCreditNotesForMapping(_supplierRefId, _refType);
            gvCreditNotes.Focus();
        }

        private void frmMapCreditNote_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectCreditNote();
                e.Handled = true;
            }
        }

        private void SelectCreditNote()
        {
            if (gvCreditNotes.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Please select a credit note");
                return;
            }

            SelectedCreditNoteId =
                Convert.ToInt32(gvCreditNotes.GetFocusedRowCellValue("CreditNoteId"));

            SelectedCreditValue =
                Convert.ToDecimal(gvCreditNotes.GetFocusedRowCellValue("CreditValue"));

            SelectedCNNumber =
                Convert.ToString(gvCreditNotes.GetFocusedRowCellValue("CNNumber"));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}