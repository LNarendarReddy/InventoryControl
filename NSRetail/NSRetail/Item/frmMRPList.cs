using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NSRetail
{
    public partial class frmMRPList : DevExpress.XtraEditors.XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;
        DataTable dtMRP;
        public frmMRPList(DataTable _dtMRP)
        {
            InitializeComponent();
            dtMRP = _dtMRP;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            _IsSave = true;
            drSelected = gvMRPList.GetFocusedRow();
        }
    }
}