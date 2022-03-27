using DataAccess;
using DevExpress.Utils.Menu;
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
    public partial class frmViewDCBills : DevExpress.XtraEditors.XtraForm
    {
        object CounterID = null;
        public frmViewDCBills(DataTable dtBills,object _CounterID)
        {
            InitializeComponent();
            gcBills.DataSource = dtBills;
            CounterID = _CounterID;
        }
        private void gvBills_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvBills.FocusedRowHandle < 0)
                return; 
            e.Menu.Items.Add(new DXMenuItem("View Report", new EventHandler(OnResetPassword_Click)));
        }
        private void OnResetPassword_Click(object sender, EventArgs e)
        {
            gcBills.ShowRibbonPrintPreview();
        }
        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvBills.FocusedRowHandle < 0)
                return;
            DataTable dtItems = new POSRepository().GetBillDetailByID(CounterID,
                gvBills.GetFocusedRowCellValue("BILLID"));
            frmViewDCItems obj = new frmViewDCItems(dtItems,true);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void frmViewDCBills_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}