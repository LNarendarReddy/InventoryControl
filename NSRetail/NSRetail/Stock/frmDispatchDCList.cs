using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using ErrorManagement;
using NSRetail.Reports;
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
    public partial class frmDispatchDCList : DevExpress.XtraEditors.XtraForm
    {
        StockRepository ObjStockRep = new StockRepository();
        public frmDispatchDCList()
        {
            InitializeComponent();
        }

        private void frmDispatchDCList_Load(object sender, EventArgs e)
        {
            gcDispatchDC.DataSource = ObjStockRep.GetDCList();
        }

        private void btnView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvDispatchDC.FocusedRowHandle >= 0)
                {
                    DataSet ds = ObjStockRep.GetDispatchDC(gvDispatchDC.GetFocusedRowCellValue("DISPATCHDCID"));
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        rptDispatchDC rpt = new rptDispatchDC(ds.Tables[0], ds.Tables[1]);
                        rpt.ShowPrintMarginsWarning = false;
                        rpt.ShowRibbonPreview();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcDispatchDC.ShowRibbonPrintPreview();
        }
    }
}