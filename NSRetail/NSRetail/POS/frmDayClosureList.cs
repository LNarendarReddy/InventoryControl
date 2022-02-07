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
    public partial class frmDayClosureList : DevExpress.XtraEditors.XtraForm
    {
        public frmDayClosureList()
        {
            InitializeComponent();
        }

        private void frmDayClosureList_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            gcDayClosure.DataSource = new POSRepository().GetDayClosure(cmbBranch.EditValue);
        }

        private void btnViewSummary_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDayClosure.FocusedRowHandle < 0)
                return;
            DataTable dtDayClosureSummary = new POSRepository().GetDayClosureDetail(gvDayClosure.GetFocusedRowCellValue("DAYCLOSUREID"),
                gvDayClosure.GetFocusedRowCellValue("BRANCHCOUNTERID"));
            frmDayClosureSummary obj = new frmDayClosureSummary(dtDayClosureSummary);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnViewBills_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnViewRefunds_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}