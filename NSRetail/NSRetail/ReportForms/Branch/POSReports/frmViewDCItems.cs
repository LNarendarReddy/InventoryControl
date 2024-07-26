using DevExpress.Utils.Menu;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class frmViewDCItems : DevExpress.XtraEditors.XtraForm
    {
        public frmViewDCItems(DataSet dsItems, bool IsBilldetail = false, bool IsCustomerRefund = false, bool IsVoidIems = false)
        {
            InitializeComponent();
            gcGSTCode.Visible = IsBilldetail;
            gcGSTValue.Visible = IsBilldetail;
            gcDiscount.Visible = IsCustomerRefund;
            if (!IsBilldetail)
                lciMOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            gcItems.DataSource = dsItems.Tables[0];
            if (dsItems.Tables.Count > 1)
                gcMOP.DataSource = dsItems.Tables[1];
            gcBillNumber.Visible = IsVoidIems || IsCustomerRefund;
            gcCreatedBy.Visible = IsVoidIems;
            gcCreatedDate.Visible = true;
            gcDeletedBy.Visible = IsVoidIems;
            gcDeletedDate.Visible = IsVoidIems;
            gcBilledAmount.Caption = IsCustomerRefund ? "Refund Amount" : gcBilledAmount.Caption;
            gcQuantity.Caption = IsCustomerRefund ? "Refund Quantity" : gcQuantity.Caption;
            gcCreatedBy.Caption = IsCustomerRefund ? "Refund User" : gcCreatedBy.Caption;
            gcCreatedDate.Caption = IsCustomerRefund ? "Refund Date" : gcCreatedDate.Caption;
        }

        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0)
                return;
            e.Menu.Items.Add(new DXMenuItem("View Report", new EventHandler(ViewReport_Click)));
        }

        private void ViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void frmViewDCItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void frmViewDCItems_Load(object sender, EventArgs e)
        {

        }

        private void gcItems_Click(object sender, EventArgs e)
        {

        }
    }
}