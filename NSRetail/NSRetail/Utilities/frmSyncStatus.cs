using DataAccess;
using System;

namespace NSRetail.Utilities
{
    public partial class frmSyncStatus : DevExpress.XtraEditors.XtraForm
    {
        CloudRepository cloudRepository = new CloudRepository();

        public frmSyncStatus()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gcSyncStatus.DataSource = cloudRepository.GetSyncStatus();
        }

        private void frmSyncStatus_Load(object sender, EventArgs e)
        {
            btnRefresh_Click(null, null);
        }
    }
}