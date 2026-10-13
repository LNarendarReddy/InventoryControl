using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmCarryBagSelection : XtraForm
    {
        public DataRow SelectedItem { get; private set; }

        public frmCarryBagSelection(DataTable items)
        {
            InitializeComponent();
            Utility.SetGridFormatting(gvItems);
            gcItems.DataSource = items;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            gvItems.FocusedRowHandle = 0;
            gcItems.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                if (ActiveControl == btnCancel)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                {
                    SelectItem();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SelectItem()
        {
            DataRow item = gvItems.GetFocusedDataRow();
            if (item == null)
                return;

            SelectedItem = item;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectItem();
        }

        private void gvItems_DoubleClick(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse == null)
                return;

            var hit = gvItems.CalcHitInfo(mouse.Location);
            if (hit.InRow || hit.InRowCell)
                SelectItem();
        }

    }
}
