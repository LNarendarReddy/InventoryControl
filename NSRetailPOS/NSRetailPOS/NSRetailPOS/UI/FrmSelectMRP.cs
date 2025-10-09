using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class FrmSelectMRP : XtraForm
    {
        public DataRow SelectedRow { get; private set; }

        public FrmSelectMRP(DataView dvItems)
        {
            InitializeComponent();
            gcMRPList.DataSource = dvItems;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var rowHandle = gvMRPList.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                SelectedRow = gvMRPList.GetDataRow(rowHandle);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("Please select an item.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}