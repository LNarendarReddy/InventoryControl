using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmStockCounting : DevExpress.XtraEditors.XtraForm
    {
        public frmStockCounting()
        {
            InitializeComponent();
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }
        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvStockCounting.FocusedRowHandle < 0)
                return;
            frmViewItems obj =
                new frmViewItems(new CountingRepository().GetStockCountingDetail(
                    gvStockCounting.GetFocusedRowCellValue("STOCKCOUNTINGID")));
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }
        private void cmbBranch_Leave(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            gcStockCounting.DataSource = new CountingRepository().GetStockCounting(cmbBranch.EditValue);
        }
        private void btnViewDifferences_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            frmViewItems obj =
                new frmViewItems(new CountingRepository().GetStockCountingDiff(
                    cmbBranch.EditValue));
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }
    }
}