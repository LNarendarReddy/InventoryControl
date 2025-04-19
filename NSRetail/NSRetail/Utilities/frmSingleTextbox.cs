using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Utilities
{
    public partial class frmSingleTextbox : DevExpress.XtraEditors.XtraForm
    {
        public object newValue 
        { 
            get { return txtValue.EditValue; } 
            set { txtValue.EditValue = value; } 
        }
        public bool isSave = false;
        public frmSingleTextbox(string lcCaption, bool isnumber = true)
        {
            InitializeComponent();
            lcValue.Text = lcCaption;
            if(isnumber)
            {
                this.txtValue.Properties.DisplayFormat.FormatString = "n0";
                this.txtValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                this.txtValue.Properties.EditFormat.FormatString = "n0";
                this.txtValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                this.txtValue.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
                this.txtValue.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
                this.txtValue.Properties.MaskSettings.Set("mask", "n0");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            isSave =true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}