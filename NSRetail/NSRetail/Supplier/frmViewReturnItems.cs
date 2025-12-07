using DataAccess;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmViewReturnItems : XtraForm
    {
        object SupplierReturnsID = null;
        public frmViewReturnItems(DataTable dtItems, object SupplierName, object _SupplierReturnsID, bool _IsOpen)
        {
            InitializeComponent();

            cmbReturnStatus.DataSource = Utility.Returnstatus();
            cmbReturnStatus.DisplayMember = "RETURNSTATUSNAME";
            cmbReturnStatus.ValueMember = "RETURNSTATUSID";

            SupplierReturnsID = _SupplierReturnsID;
            this.Text = $"{Text} - {SupplierName} - {SupplierReturnsID}";
            gvSupplierReturns.ViewCaption = $"Credit Note : {SupplierName}-{SupplierReturnsID}";
            gcSupplierReturns.DataSource = dtItems;
            if (_IsOpen)
            {
                gcReturnstatus.Visible = true;
                gcReturnstatus.OptionsColumn.AllowEdit = true;
                btnCreditNoteMapping.Enabled = true;
                txtReturnValue.Enabled = true;
            }
        }

        private void btnCreditNoteMapping_Click(object sender, EventArgs e) 
        {
            if (gvSupplierReturns.RowCount == 0 ||
                XtraMessageBox.Show("Are you sure want to continue?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                    if (txtReturnValue.EditValue == null)
                    {
                        XtraMessageBox.Show("Return value is mandatory");
                        return;
                    }
                    new SupplierRepository().UpdateSupplierReturns(SupplierReturnsID, Utility.UserID, 
                        (gcSupplierReturns.DataSource as DataTable).Copy(), txtReturnValue.EditValue);
                Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
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
            cmbReason.DataSource = new SupplierRepository().GetReason();
            cmbReason.ValueMember = "REASONID";
            cmbReason.DisplayMember = "REASONNAME";
        }

        private void gcSupplierReturns_Click(object sender, EventArgs e)
        {

        }
    }
}