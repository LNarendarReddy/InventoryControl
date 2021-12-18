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

namespace NSRetailPOS.UI
{
    public partial class frmMRPSelection : DevExpress.XtraEditors.XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;

        public frmMRPSelection(DataTable dtMRPList)
        {
            InitializeComponent();
            gcMRPList.DataSource = dtMRPList;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _IsSave = true;
                drSelected = gvMRPList.GetFocusedRow();
                this.Close();
            }
            catch (Exception) { }
        }
    }
}