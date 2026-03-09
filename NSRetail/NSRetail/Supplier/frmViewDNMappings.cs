using System.Data;

namespace NSRetail.Supplier
{
    public partial class frmViewDNMappings : DevExpress.XtraEditors.XtraForm
    {
        public frmViewDNMappings(DataTable dataTable)
        {
            InitializeComponent();
            gcCreditNotes.DataSource = dataTable;
            gvCreditNotes.BestFitColumns();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}