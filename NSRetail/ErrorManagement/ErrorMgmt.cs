using System;
using log4net;
using DevExpress.XtraEditors;
using System.Windows.Forms;


namespace ErrorManagement
{
    public static class ErrorMgmt
    {
        public static readonly ILog Errorlog = LogManager.GetLogger("Errorlog");

        /// <summary>
        /// show the exceptions throwed
        /// </summary>
        public static void ShowError(Exception ex)
        {
            if(Form.ActiveForm != null && Form.ActiveForm.InvokeRequired)
            {
                Form.ActiveForm.BeginInvoke((Action)(() => ShowError(ex)));
                return;
            }

            try
            {
                XtraMessageBox.Show(ex.Message + (ex.InnerException != null ? $" - {ex.InnerException.Message}" : string.Empty)
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception) { throw ex; }
        }

        /// <summary>
        /// show custom error messages
        /// </summary>
        public static void ShowErrorMessage(string ex)
        {
            if (Form.ActiveForm != null && Form.ActiveForm.InvokeRequired)
            {
                Form.ActiveForm.BeginInvoke((Action)(() => ShowErrorMessage(ex)));
                return;
            }

            try
            {
                XtraMessageBox.Show(ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception) { throw; }
        }
    }
}
