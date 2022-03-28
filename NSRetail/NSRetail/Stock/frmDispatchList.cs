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
    public partial class frmDispatchList : DevExpress.XtraEditors.XtraForm
    {
        StockRepository ObjStockRep = new StockRepository();
        public frmDispatchList()
        {
            InitializeComponent();
            gcDispatch.DataSource = ObjStockRep.GetDispatchList();
        }

        private void btnView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvDispatch.FocusedRowHandle >= 0)
                {
                    DataSet ds = ObjStockRep.GetDispatch(gvDispatch.GetFocusedRowCellValue("STOCKDISPATCHID"));
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        rptDispatch rpt = new rptDispatch(ds.Tables[0], ds.Tables[1]);
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
            gcDispatch.ShowRibbonPrintPreview();
        }

        private void frmDispatchList_Load(object sender, EventArgs e)
        {

        }
    }
}