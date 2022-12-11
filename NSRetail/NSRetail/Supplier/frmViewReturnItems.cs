using DataAccess;
using DevExpress.XtraEditors;
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
        public frmViewReturnItems(DataTable dtItems, object SupplierName, object _SupplierReturnsID, bool _cNGenerated)
        {
            InitializeComponent();
            SupplierReturnsID = _SupplierReturnsID;
            this.Text = $"{Text} - {SupplierName} - {SupplierReturnsID}";
            gvSupplierReturns.ViewCaption = $"Credit Note : {SupplierName}-{SupplierReturnsID}";
            gcSupplierReturns.DataSource = dtItems;
            cNGenerated = _cNGenerated;
            btnGenerateCreditNote.Enabled = !cNGenerated;
            btnPrint.Enabled = cNGenerated;
        }

        private void frmViewReturnItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnGenerateCreditNote_Click(object sender, EventArgs e)
        {
            if (gvSupplierReturns.RowCount == 0 ||
                XtraMessageBox.Show("Are you sure want to submit?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                new SupplierRepository().UpdateSupplierReturns(SupplierReturnsID, Utility.UserID);
                cNGenerated = true;
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
    }
}