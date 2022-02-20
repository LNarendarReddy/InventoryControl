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
    public partial class frmBRefundList : DevExpress.XtraEditors.XtraForm
    {
        public frmBRefundList()
        {
            InitializeComponent();
        }

        private void frmBRefundList_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            gcBRefund.DataSource = new POSRepository().GetBRefund(cmbBranch.EditValue);
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
    }
}