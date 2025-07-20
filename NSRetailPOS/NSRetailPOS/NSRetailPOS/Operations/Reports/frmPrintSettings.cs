using System;

namespace NSRetailPOS.Operations.Reports
{
    public partial class frmPrintSettings : DevExpress.XtraEditors.XtraForm
    {
        public object printSetting
        {
            get { return rgPrintSetting.EditValue; }
            set { rgPrintSetting.EditValue = value; }
        }
        public frmPrintSettings()
        {
            InitializeComponent();
            this.rgPrintSetting.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}