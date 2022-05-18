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
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.EditValue == null)
                return;
            gcSupplierReturns.DataSource = supplierRepository.GetSupplierReturns(cmbSupplier.EditValue,
                dtpFromDate.EditValue,dtpToDate.DateTime);
        }

        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvSupplierReturns.FocusedRowHandle < 0)
                return;
            DataTable dt = supplierRepository.GetSupllierReturnsDetail(gvSupplierReturns.GetFocusedRowCellValue("SUPPLIERRETURNSID"));
            frmViewReturnItems obj = new frmViewReturnItems(dt,
                gvSupplierReturns.GetFocusedRowCellValue("DEALERNAME"),
                gvSupplierReturns.GetFocusedRowCellValue("SUPPLIERRETURNSID"));
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
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