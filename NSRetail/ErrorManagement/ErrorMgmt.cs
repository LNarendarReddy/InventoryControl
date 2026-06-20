using DevExpress.XtraEditors;
using Serilog;
using System;
using System.Windows.Forms;


namespace ErrorManagement
{
    public static class ErrorMgmt
    {
        public static Form MainFormInsance { get; set; }

        /// <summary>
        /// show the exceptions throwed
        /// </summary>
        public static void ShowError(Exception ex)
        {
            AppLog.Error(ex, ex.Message);

            if(Form.ActiveForm != null && Form.ActiveForm.InvokeRequired)
            {
                Form.ActiveForm.BeginInvoke((Action)(() => ShowError(ex)));
                return;
            }
            
            if (MainFormInsance != null && MainFormInsance.InvokeRequired)
            {
                MainFormInsance.BeginInvoke((Action)(() => ShowError(ex)));
                return;
            }

            try
            {
                new frmErrorDetails(ex) { Owner = MainFormInsance }.ShowDialog();
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
