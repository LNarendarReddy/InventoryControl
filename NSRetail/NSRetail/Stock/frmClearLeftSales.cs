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

namespace NSRetail.Stock
{
    public partial class frmClearLeftSales : XtraForm
    {

        IOverlaySplashScreenHandle handle;
        BackgroundWorker bgwGetData = new BackgroundWorker();

        public frmClearLeftSales()
        {
            InitializeComponent();

            bgwGetData.DoWork += BgwGetData_DoWork;
            bgwGetData.RunWorkerCompleted += BgwGetData_RunWorkerCompleted;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOutput.EditValue?.ToString())) return;

            Clipboard.SetText(txtOutput.EditValue.ToString());
        }

        private void frmClearLeftSales_Load(object sender, EventArgs e)
        {
            handle = SplashScreenManager.ShowOverlayForm(this);
            bgwGetData.RunWorkerAsync();
        }

        private void BgwGetData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (handle == null) return;
            SplashScreenManager.CloseOverlayForm(handle);
            handle = null;
        }

        private void BgwGetData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "USERID", Utility.UserID }
                };
                DataSet dsItems = new ReportRepository().GetReportDataset("USP_P_LEFTSALEDISPATCH", parameters);                               

                InvokeUIOperation(dsItems);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private void InvokeUIOperation(DataSet reportData)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => InvokeUIOperation(reportData)));
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach(DataTable dataTable in reportData.Tables)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i > 0) sb.Append(", ");

                        if(string.IsNullOrEmpty(row[i]?.ToString())) continue;

                        sb.Append(row[i].ToString());
                    }

                    sb.AppendLine();
                }

                sb.AppendLine(Environment.NewLine);
            }

            sb.AppendLine("Processing completed");

            txtOutput.Text = sb.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
