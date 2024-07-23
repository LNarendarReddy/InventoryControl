using DevExpress.XtraCharts;
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

namespace NSRetail.Utilities
{
    public partial class frmBuildInfo : DevExpress.XtraEditors.XtraForm
    {
        public frmBuildInfo()
        {
            InitializeComponent();
        }

        private void frmBuildInfo_Load(object sender, EventArgs e)
        {
            txtBuildInfo.EditValue = System.IO.File.ReadAllText("BuildInfo.txt");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBuildInfo_Shown(object sender, EventArgs e)
        {
            txtBuildInfo.SelectionStart = 0;
            txtBuildInfo.SelectionLength = 0;
        }
    }
}