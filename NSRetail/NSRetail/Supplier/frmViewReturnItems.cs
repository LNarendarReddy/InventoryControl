using DataAccess;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using NSRetail.Supplier;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmViewReturnItems : XtraForm
    {
        object SupplierReturnsID = null;
        private int? _selectedCreditNoteId = null;
        private string _status;
        private string _supplierName;

        public frmViewReturnItems(DataTable dtItems,object supplierName,object supplierReturnsId,object status,object returnValue)
        {
            InitializeComponent();

            _supplierName = Convert.ToString(supplierName);
            _status = Convert.ToString(status);

            SupplierReturnsID = supplierReturnsId;

            this.Text = $"{Text} - {supplierName} - {SupplierReturnsID}";
            gvSupplierReturns.ViewCaption =
                $"Credit Note : {supplierName}-{SupplierReturnsID}";

            gcSupplierReturns.DataSource = dtItems;

            ApplyUIStateByStatus(Convert.ToString(status));
        }
        private void ApplyUIStateByStatus(string status)
        {
            switch (status)
            {
                case "Open":
                case "Partially Closed":
                    gcReturnstatus.Visible = true;
                    gcReturnstatus.OptionsColumn.AllowEdit = true;
                    break;

                case "Closed":
                case "Write Off":
                    DisableAllEditing();
                    break;
            }
        }
        private void DisableAllEditing()
        {
            gcReturnstatus.OptionsColumn.AllowEdit = false;
            btnCloseDN.Enabled = false;
            btnWriteOffStock.Enabled = false;
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

        private bool ValidateCreditNoteForClose()
        {
            if (_status.Equals("open", StringComparison.OrdinalIgnoreCase))
            {
                if (_selectedCreditNoteId == null || _selectedCreditNoteId == 0)
                {
                    XtraMessageBox.Show("Credit Note is mandatory");
                    return false;
                }
            }

            return true;
        }

        private void CloseDebitNote(object status)
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
                    status // Status ID for Closed
                );

                Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnCloseDN_Click(object sender, EventArgs e)
        {
            if (!ValidateCreditNoteForClose())
                return;
            CloseDebitNote(3);
        }

        private void btbWriteOffStock_Click(object sender, EventArgs e)
        {
            CloseDebitNote(4);
        }
    }
}