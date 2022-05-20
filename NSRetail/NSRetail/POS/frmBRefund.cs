using DataAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    [Obsolete]
    public partial class frmBRefundList : DevExpress.XtraEditors.XtraForm
    {
        public frmBRefundList()
        {
            InitializeComponent();
        }

        private void frmBRefundList_Load(object sender, EventArgs e)
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
            if (cmbBranch.EditValue == null)
                return;
            gcBRefund.DataSource = new POSRepository().GetBRefund(cmbBranch.EditValue,
                dtpFromDate.EditValue,dtpToDate.DateTime.AddDays(1));
        }

        private void btnView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvBRefund.FocusedRowHandle < 0)
                return;
            DataTable dtItems = new POSRepository().GetBRefundDetail(gvBRefund.GetFocusedRowCellValue("BREFUNDID"),
                gvBRefund.GetFocusedRowCellValue("COUNTERID"));
            frmBRefundDetail obj = new frmBRefundDetail(dtItems, gvBRefund.GetFocusedRowCellValue("COUNTERID"),
                gvBRefund.GetFocusedRowCellValue("BREFUNDID"), 
                Convert.ToBoolean(gvBRefund.GetFocusedRowCellValue("IsAccepted")));
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
            if (obj.IsSave)
            {
                gvBRefund.SetRowCellValue(gvBRefund.FocusedRowHandle, "IsAccepted", true);
                gvBRefund.SetRowCellValue(gvBRefund.FocusedRowHandle, "STATUS", "Accepted");
            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcBRefund.ShowRibbonPrintPreview();
        }
    }
}