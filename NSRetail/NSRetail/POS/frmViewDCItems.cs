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
    public partial class frmViewDCItems : DevExpress.XtraEditors.XtraForm
    {
        public frmViewDCItems(DataTable dtItems, bool IsBilldetail = false,bool IsCustomerRefund = false)
        {
            InitializeComponent();
            gcGSTCode.Visible = IsBilldetail;
            gcGSTValue.Visible = IsBilldetail;
            gcDiscount.Visible = IsCustomerRefund;
            gcItems.DataSource = dtItems;
        }
        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Add(new DXMenuItem("View Report", new EventHandler(OnResetPassword_Click)));
        }
        private void OnResetPassword_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }
        private void frmViewDCItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}