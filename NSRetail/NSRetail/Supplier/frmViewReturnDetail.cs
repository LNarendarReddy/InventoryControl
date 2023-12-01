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

namespace NSRetail.Supplier
{
    public partial class frmViewReturnDetail : DevExpress.XtraEditors.XtraForm
    {
        public frmViewReturnDetail(DataTable dtItems, object SupplierName, object SupplierReturnsID)
        {
            InitializeComponent();
            this.Text = $"{Text} - {SupplierName} - {SupplierReturnsID}";
            gvSupplierReturns.ViewCaption = $"Return Items : {SupplierName}-{SupplierReturnsID}";
            gcSupplierReturns.DataSource = dtItems;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            gcSupplierReturns.ShowRibbonPrintPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmViewReturnDetail_Load(object sender, EventArgs e)
        {
            cmbReason.DataSource = new SupplierRepository().GetReason();
            cmbReason.ValueMember = "REASONID";
            cmbReason.DisplayMember = "REASONNAME";
        }
    }
}