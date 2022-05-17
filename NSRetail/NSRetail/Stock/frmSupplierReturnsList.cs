using DataAccess;
using DevExpress.XtraEditors;
using Entity;
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
    public partial class frmSupplierReturnsList : DevExpress.XtraEditors.XtraForm
    {
        SupplierRepository supplierRepository = new SupplierRepository();
        public frmSupplierReturnsList()
        {
            InitializeComponent();
        }

        private void frmSupplierReturnsList_Load(object sender, EventArgs e)
        {
            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.EditValue == null)
                return;
            gcSupplierReturns.DataSource = supplierRepository.GetSupplierReturns(cmbSupplier.EditValue);
        }

        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvSupplierReturns.FocusedRowHandle < 0)
                return;
            SupplierReturns supplierReturns = new SupplierReturns();
            supplierReturns.SupplierReturnsID = gvSupplierReturns.GetFocusedRowCellValue("SUPPLIERRETURNSID");
            supplierReturns.SupplierID = gvSupplierReturns.GetFocusedRowCellValue("SUPPLIERID");
            frmSupplierReturns obj = new frmSupplierReturns(supplierReturns);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.MdiParent = this.MdiParent;
            obj.Show();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcSupplierReturns.ShowRibbonPrintPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}