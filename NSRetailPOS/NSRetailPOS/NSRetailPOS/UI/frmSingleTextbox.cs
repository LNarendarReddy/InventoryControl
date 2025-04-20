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

namespace NSRetailPOS.UI
{
    public partial class frmSingleTextbox : DevExpress.XtraEditors.XtraForm
    {
        public bool isSave = false;
        public object newValue { get { return txtValue.EditValue; } private set; }

        public frmSingleTextbox(string caption)
        {
            InitializeComponent();
            lcValue.Text = caption;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(!dxValidationProvider1.Validate())
                return;
            isSave = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}