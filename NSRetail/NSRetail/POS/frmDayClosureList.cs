using DataAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    [Obsolete]
    public partial class frmDayClosureList : DevExpress.XtraEditors.XtraForm
    {
        public frmDayClosureList()
        {
            InitializeComponent();
        }

        private void frmDayClosureList_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            gcDayClosure.DataSource = new POSRepository().GetDayClosure(cmbBranch.EditValue,
                dtpFromDate.EditValue,dtpToDate.DateTime.AddDays(1));
        }

        private void btnViewSummary_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDayClosure.FocusedRowHandle < 0)
                return;
            DataTable dtDayClosureSummary = new POSRepository().GetDayClosureDetail(
                gvDayClosure.GetFocusedRowCellValue("DAYCLOSUREID"),
                gvDayClosure.GetFocusedRowCellValue("BRANCHCOUNTERID"));
            frmDayClosureSummary obj = new frmDayClosureSummary(dtDayClosureSummary);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnViewBills_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDayClosure.FocusedRowHandle < 0)
                return;
            DataTable dtBills = new POSRepository().GetDayClosureBills(
                gvDayClosure.GetFocusedRowCellValue("BRANCHCOUNTERID"), 
                gvDayClosure.GetFocusedRowCellValue("DAYCLOSUREID"));
             frmViewDCBills obj = new frmViewDCBills(dtBills, gvDayClosure.GetFocusedRowCellValue("BRANCHCOUNTERID"));
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDayClosure.FocusedRowHandle < 0)
                return;
            DataSet dsItems = new POSRepository().GetDayClosureItems(
                gvDayClosure.GetFocusedRowCellValue("BRANCHCOUNTERID"),
                gvDayClosure.GetFocusedRowCellValue("DAYCLOSUREID"));
            frmViewDCItems obj = new frmViewDCItems(dsItems, false);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnViewRefunds_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDayClosure.FocusedRowHandle < 0)
                return;
            DataSet dsItems = new POSRepository().GetDayClosureRefund(
                gvDayClosure.GetFocusedRowCellValue("BRANCHCOUNTERID"),
                gvDayClosure.GetFocusedRowCellValue("DAYCLOSUREID"));
            frmViewDCItems obj = new frmViewDCItems(dsItems, false,true);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnVoidItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDayClosure.FocusedRowHandle < 0)
                return;
            DataSet dsItems = new POSRepository().GetDayClosureVoidItems(
                gvDayClosure.GetFocusedRowCellValue("BRANCHCOUNTERID"),
                gvDayClosure.GetFocusedRowCellValue("DAYCLOSUREID"));
            frmViewDCItems obj = new frmViewDCItems(dsItems, false);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void cmbBranch_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gcDayClosure.ShowRibbonPrintPreview();
        }
    }
}