using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmImportMBQ : DevExpress.XtraEditors.XtraForm
    {
        public frmImportMBQ()
        {
            InitializeComponent();
        }

        List<string> allowedColumns = new List<string> { "MBQTARGET", "TARGETTYPE", "SKUCODE", "THRESHOLD", "DESIREDQUANTITY" };

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog xtraOpenFileDialog1 = new XtraOpenFileDialog
            {
                InitialDirectory = Environment.SpecialFolder.Desktop.ToString(),
                Filter = "excel files (*.xls,*.xlsx)|*.xls,*.xlsx"
            };

            if (xtraOpenFileDialog1.ShowDialog() != DialogResult.OK) return;

            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            try
            {
                string filePath = xtraOpenFileDialog1.FileName;
                DataTable dt = Utility.ImportExcelXLS(filePath);
                if (dt == null || dt.Rows.Count == 0) return;

                DataTable dtTemp = dt.Copy();
                                
                int i = 0;
                foreach (string s in allowedColumns)
                {
                    if (!dtTemp.Columns.Contains(s))
                        throw new Exception($"{s} column is missed in import file");
                    else
                    {
                        dtTemp.Columns[s].SetOrdinal(i);
                        i++;
                    }
                }

                gcImportMBQ.DataSource = dtTemp;
                SplashScreenManager.CloseOverlayForm(handle);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseOverlayForm(handle);
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!(gcImportMBQ.DataSource is DataTable)) return;

            DataTable dtTemp = (gcImportMBQ.DataSource as DataTable).Copy();

            dtTemp.Columns.Cast<DataColumn>().Where(x => !allowedColumns.Contains(x.ColumnName))
                    .ToList().ForEach(x => dtTemp.Columns.Remove(x));

            string response = new DataRepository().ExecuteScalarWithTransaction("USP_IMP_MBQ", true
                , new Dictionary<string, object>()
                {
                    { "data", dtTemp },
                    { "USERID", Utility.UserID }
                })?.ToString();

            response = string.Join(Environment.NewLine, response.Split((new List<string>() { "\\n" }).ToArray(), StringSplitOptions.None));
            txtErrors.EditValue = response;
            XtraMessageBox.Show("Import completed, please check the errors\\validations section for more details", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnImport.Enabled = false;
            txtErrors.Focus();
        }
    }
}