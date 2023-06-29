using DataAccess;
using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmViewReturnItems : DevExpress.XtraEditors.XtraForm
    {
        object SupplierReturnsID = null;
        public bool cNGenerated = false;
        public bool cNInitiated = false;
        public frmViewReturnItems(DataTable dtItems, object SupplierName, object _SupplierReturnsID, bool _cNInitiated, bool _cnGenerated)
        {
            InitializeComponent();

            cmbReturnStatus.DataSource = Utility.Returnstatus();
            cmbReturnStatus.DisplayMember = "RETURNSTATUSNAME";
            cmbReturnStatus.ValueMember = "RETURNSTATUSID";

            SupplierReturnsID = _SupplierReturnsID;
            this.Text = $"{Text} - {SupplierName} - {SupplierReturnsID}";
            gvSupplierReturns.ViewCaption = $"Credit Note : {SupplierName}-{SupplierReturnsID}";
            gcSupplierReturns.DataSource = dtItems;
            cNGenerated = _cnGenerated;
            cNInitiated = _cNInitiated;
            if (cNInitiated || cNGenerated)
            {
                gcSelect.Visible = false;
                gcReturnstatus.Visible = true;
            }
            if (!_cNInitiated)
                btnGenerateCreditNote.Text = "Initiate Credit Note";

            if (_cnGenerated)
            {
                btnGenerateCreditNote.Text = "Generate Credit Note";
                btnGenerateCreditNote.Enabled = false;
                gcReturnstatus.OptionsColumn.AllowEdit = false;
            }
        }

        private void btnGenerateCreditNote_Click(object sender, EventArgs e)
        {
            if (gvSupplierReturns.RowCount == 0 ||
                XtraMessageBox.Show("Are you sure want to continue?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                if (btnGenerateCreditNote.Text == "Initiate Credit Note")
                {
                    DataView dv = (gcSupplierReturns.DataSource as DataTable).Copy().DefaultView;
                    dv.RowFilter = "SELECTED = 1";
                    new SupplierRepository().InitiateCreditNote(SupplierReturnsID, Utility.UserID, dv.ToTable());
                    cNInitiated = true;
                }
                else
                {
                    new SupplierRepository().UpdateSupplierReturns(SupplierReturnsID, Utility.UserID, gcSupplierReturns.DataSource as DataTable);
                    cNGenerated = true;
                }
                Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            gcSupplierReturns.ShowRibbonPrintPreview();
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
    }
}