using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmCostPriceList : DevExpress.XtraEditors.XtraForm
    {
        public DataRow drSelected = null;
        public bool _IsSave = false;
        public frmCostPriceList(DataTable _dtCP)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.IconOptions.ShowIcon = false;
            gcCPList.DataSource = _dtCP;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _IsSave = true;
                drSelected = gvCPList.GetDataRow(gvCPList.FocusedRowHandle);
                this.Close();
            }
            catch (Exception) { }
        }
    }
}