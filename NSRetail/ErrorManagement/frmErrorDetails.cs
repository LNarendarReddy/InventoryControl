using DevExpress.XtraBars;
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

namespace ErrorManagement
{
    public partial class frmErrorDetails : XtraForm
    {
        Exception exception;

        public frmErrorDetails(Exception exception)
        {
            InitializeComponent();
            this.exception = exception;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopyDetails_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtInfo.EditValue.ToString());
        }

        private void frmErrorDetails_Load(object sender, EventArgs e)
        {
            lblError.Text = exception.Message;

            StringBuilder stringBuilderErrors = new StringBuilder();
            StringBuilder stringBuilderStackTrace = new StringBuilder();


            Exception processException = exception;
            int i = 1;
            while (processException != null)
            {
                stringBuilderErrors.Append($"{i} Error: {processException.Message}{Environment.NewLine}{Environment.NewLine}");
                stringBuilderStackTrace.Append($"{i} : {processException.StackTrace}{Environment.NewLine}{Environment.NewLine}");

                processException = processException.InnerException;
                i++;
            }

            txtInfo.Text = stringBuilderErrors.ToString() + $"{Environment.NewLine}{Environment.NewLine}Stack traces:{Environment.NewLine}{Environment.NewLine}" + stringBuilderStackTrace.ToString();            
        }

        private void frmErrorDetails_Shown(object sender, EventArgs e)
        {
            txtInfo.SelectionStart = 0;
            txtInfo.SelectionLength = 0;
        }
    }
}