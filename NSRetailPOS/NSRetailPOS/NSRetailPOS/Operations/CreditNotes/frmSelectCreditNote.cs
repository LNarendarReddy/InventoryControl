using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using System;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.CreditNotes
{
    public partial class frmSelectCreditNote : DevExpress.XtraEditors.XtraForm
    {
        public int SelectedCreditNoteId { get; private set; }
        public decimal SelectedCreditValue { get; private set; }
        public string SelectedCNNumber { get; private set; }
        public string SelectedAdjustmentType { get; private set; }

        private object _supplierRefId;
        private string _refType;
        private object _supplierId;

        public frmSelectCreditNote(object supplierRefId, string refType, object supplierId)
        {
            InitializeComponent();
            _supplierRefId = supplierRefId;
            _refType = refType;
            _supplierId = supplierId;
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
            gvCreditNotes.BestFitColumns();
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
            
            SelectedCNNumber =
                Convert.ToString(gvCreditNotes.GetFocusedRowCellValue("CNNumber"));

            SelectedCreditValue =
                Convert.ToDecimal(gvCreditNotes.GetFocusedRowCellValue("BalanceCNValueCanBeMapped"));

            SelectedAdjustmentType =
                Convert.ToString(gvCreditNotes.GetFocusedRowCellValue("AdjustmentType"));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnNEWCN_Click(object sender, EventArgs e)
        {
            frmCreditNote frmCreditNote = new frmCreditNote((int)_supplierId, (int)_supplierRefId);
            if (frmCreditNote.ShowDialog() == DialogResult.OK)
            {
                gcCreditNotes.DataSource = new CreditNoteRepository().GetCreditNotesForMapping(_supplierRefId, _refType);
                gvCreditNotes.FocusedRowHandle = gvCreditNotes.LocateByValue("CreditNoteId", frmCreditNote.CreditNoteId);
            }
        }
    }
}