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
            try
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception) { throw ex; }
        }
        /// <summary>
        /// show custom error messages
        /// </summary>
        public static void ShowErrorMessage(string ex)
        {
            try
            {
                XtraMessageBox.Show(ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception) { throw; }
        }
    }
}
