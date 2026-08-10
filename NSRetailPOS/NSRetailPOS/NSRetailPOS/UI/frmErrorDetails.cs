using DevExpress.XtraEditors;
using System;
using System.Text;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmErrorDetails : XtraForm
    {
        private readonly Exception exception;
        private readonly string errorMessage;
        private readonly string details;

        public frmErrorDetails(Exception exception)
        {
            InitializeComponent();
            this.exception = exception;
        }

        public frmErrorDetails(string errorMessage, string details = null)
        {
            InitializeComponent();
            this.errorMessage = errorMessage;
            this.details = details;
        }

        private void frmErrorDetails_Load(object sender, EventArgs e)
        {
            lblError.Text = exception?.Message ?? errorMessage ?? "Unexpected error";
            txtInfo.Text = exception != null ? GetExceptionDetails(exception) : GetMessageDetails();
        }

        private void frmErrorDetails_Shown(object sender, EventArgs e)
        {
            txtInfo.SelectionStart = 0;
            txtInfo.SelectionLength = 0;
            btnOk.Focus();
        }

        private void btnCopyDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInfo.Text))
            {
                Clipboard.SetText(txtInfo.Text);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string GetExceptionDetails(Exception sourceException)
        {
            StringBuilder stringBuilderErrors = new StringBuilder();
            StringBuilder stringBuilderStackTrace = new StringBuilder();

            Exception processException = sourceException;
            int i = 1;
            while (processException != null)
            {
                stringBuilderErrors.Append($"{i} Error: {processException.Message}{Environment.NewLine}{Environment.NewLine}");
                stringBuilderStackTrace.Append($"{i} : {processException.StackTrace}{Environment.NewLine}{Environment.NewLine}");

                processException = processException.InnerException;
                i++;
            }

            return stringBuilderErrors
                + $"{Environment.NewLine}{Environment.NewLine}Stack traces:{Environment.NewLine}{Environment.NewLine}"
                + stringBuilderStackTrace;
        }

        private string GetMessageDetails()
        {
            if (string.IsNullOrWhiteSpace(details))
            {
                return errorMessage ?? string.Empty;
            }

            return $"{errorMessage}{Environment.NewLine}{Environment.NewLine}{details}";
        }
    }
}
