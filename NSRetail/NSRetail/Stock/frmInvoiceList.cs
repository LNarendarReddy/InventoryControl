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
    public partial class frmInvoiceList : DevExpress.XtraEditors.XtraForm
    {
        StockRepository ObjStockRep = new StockRepository();
        public frmInvoiceList()
        {
            InitializeComponent();
            gcInvoice.DataSource = ObjStockRep.GetInvoiceList();
        }

        private void btnView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvInvoice.FocusedRowHandle >= 0)
                {
                    DataSet ds = ObjStockRep.GetInvoice(gvInvoice.GetFocusedRowCellValue("STOCKENTRYID"));
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        rptInvoice rpt = new rptInvoice(ds.Tables[0], ds.Tables[1]);
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

        private void frmInvoiceList_Load(object sender, EventArgs e)
        {

        }
    }
}