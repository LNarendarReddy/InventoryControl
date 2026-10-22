using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public sealed partial class frmDynamicQr : XtraForm
    {
        bool completed;
        readonly Action cancel;

        public frmDynamicQr()
        {
            InitializeComponent();
        }

        public frmDynamicQr(string data, decimal amount, string reference, Action cancel)
            : this()
        {
            this.cancel = cancel;
            Text = "Scan to Pay - " + amount.ToString("0.00");
            barcode.Text = data;
            order.Text = reference;
        }

        private void button_Click(object sender, EventArgs e)
        {
            cancel?.Invoke();
            button.Enabled = false;
        }

        private void frmDynamicQr_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (completed || cancel == null) return;
            cancel();
            e.Cancel = true;
            button.Enabled = false;
        }

        public void Complete() { completed = true; Close(); Dispose(); }
    }
}
