using DataAccess;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using NSRetail.Supplier;
using System;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmViewReturnItems : XtraForm
    {
        object SupplierReturnsID = null;
        private int? _selectedCreditNoteId = null;

        public frmViewReturnItems(DataTable dtItems,object supplierName,object supplierReturnsId,
            object status,object returnValue,object creditNoteId,object cnNumber)
        {
            InitializeComponent();

            SupplierReturnsID = supplierReturnsId;

            if (creditNoteId == DBNull.Value)
                _selectedCreditNoteId = null;
            else
                _selectedCreditNoteId = Convert.ToInt32(creditNoteId);

            this.Text = $"{Text} - {supplierName} - {SupplierReturnsID}";
            gvSupplierReturns.ViewCaption =
                $"Credit Note : {supplierName}-{SupplierReturnsID}";

            gcSupplierReturns.DataSource = dtItems;

            txtReturnValue.EditValue = returnValue;
            txtCNNumber.EditValue = cnNumber;

            ApplyUIStateByStatus(Convert.ToString(status));
        }
        private void ApplyUIStateByStatus(string status)
        {
            switch (status)
            {
                case "Open":
                    gcReturnstatus.Visible = true;
                    gcReturnstatus.OptionsColumn.AllowEdit = true;
                    btnCreditNoteMapping.Enabled = true;
                    txtReturnValue.Enabled = true;
                    break;

                case "Partially Closed":
                    gcReturnstatus.Visible = true;
                    gcReturnstatus.OptionsColumn.AllowEdit = true;
                    btnCreditNoteMapping.Enabled = false; // 🔒 no remap
                    txtReturnValue.Enabled = true;
                    break;

                case "Closed":
                    DisableAllEditing();
                    break;
            }
        }
        private void DisableAllEditing()
        {
            gcReturnstatus.OptionsColumn.AllowEdit = false;
            btnCreditNoteMapping.Enabled = false;
            txtReturnValue.Enabled = false;
            btnPartiallyCloseDN.Enabled = false;
            btnCloseDN.Enabled = false;
        }
        private void frmViewReturnItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        decimal selectedprice;
        decimal acceptedsumm;

        private void gridView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);

            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                selectedprice = 0;
                acceptedsumm = 0;
            }

            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        bool isSelected = Convert.ToBoolean(e.FieldValue);
                        if (isSelected)
                            selectedprice += Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "TOTALCOSTPRICE"));
                        break;
                    case 2:
                        int isaccepted = Convert.ToInt16(e.FieldValue);
                        if (isaccepted == 1)
                            acceptedsumm += Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "TOTALCOSTPRICE"));
                        break;
                }
            }

            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = selectedprice;
                        break;
                    case 2:
                        e.TotalValue = acceptedsumm;
                        break;
                }
            }
        }

        private void frmViewReturnItems_Load(object sender, EventArgs e)
        {

            cmbReturnStatus.DataSource = Utility.Returnstatus();
            cmbReturnStatus.DisplayMember = "RETURNSTATUSNAME";
            cmbReturnStatus.ValueMember = "RETURNSTATUSID";

            cmbReason.DataSource = new SupplierRepository().GetReason();
            cmbReason.ValueMember = "REASONID";
            cmbReason.DisplayMember = "REASONNAME";
        }

        private void gcSupplierReturns_Click(object sender, EventArgs e)
        {

        }

        private void btnCreditNoteMapping_Click(object sender, EventArgs e)
        {
            using (frmMapCreditNote frm = new frmMapCreditNote(SupplierReturnsID))
            {
                frm.StartPosition = FormStartPosition.CenterParent;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Populate Return Value
                    txtReturnValue.EditValue = frm.SelectedCreditValue;

                    txtCNNumber.EditValue = frm.SelectedCNNumber;

                    // Store selected CN ID
                    _selectedCreditNoteId = frm.SelectedCreditNoteId;
                }
            }
        }

        private bool ValidateCreditNoteForClose()
        {
            if (_selectedCreditNoteId == null || _selectedCreditNoteId == 0)
            {
                XtraMessageBox.Show("Credit Note is mandatory");
                return false;
            }

            if (txtReturnValue.EditValue == null)
            {
                XtraMessageBox.Show("CN Value is mandatory");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(txtReturnValue.EditValue)))
            {
                XtraMessageBox.Show("CN Number is mandatory");
                return false;
            }

            return true;
        }

        private void btnCloseDN_Click(object sender, EventArgs e)
        {
            if (!ValidateCreditNoteForClose())
                return;

            UpdateSupplierReturnsWithStatus(3); // Closed
        }

        private void UpdateSupplierReturnsWithStatus(int status)
        {
            if (gvSupplierReturns.RowCount == 0 ||
                XtraMessageBox.Show("Are you sure want to continue?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                new SupplierRepository().UpdateSupplierReturns(
                    SupplierReturnsID,
                    Utility.UserID,
                    (gcSupplierReturns.DataSource as DataTable).Copy(),
                    txtReturnValue.EditValue,
                    _selectedCreditNoteId,
                    status
                );

                Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnPartiallyCloseDN_Click(object sender, EventArgs e)
        {
            if (!ValidateCreditNoteForClose())
                return;
            UpdateSupplierReturnsWithStatus(2); // Partially Closed
        }
    }
}