using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
    public partial class frmInvoiceList : XtraForm
    {
        StockRepository ObjStockRep = new StockRepository();
        public frmInvoiceList()
        {
            InitializeComponent();
        }

        private void btnView_ButtonClick(object sender, ButtonPressedEventArgs e)
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
            cmbDealer.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbDealer.Properties.DisplayMember = "DEALERNAME";
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.EditValue = 0;
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gvInvoice.ShowRibbonPrintPreview();
        }

        private void btnShowResult_Click(object sender, EventArgs e)
        {
            gcInvoice.DataSource = ObjStockRep.GetInvoiceList(cmbDealer.EditValue,
                dtpFromDate.EditValue,dtpToDate.DateTime.AddDays(1));
        }
    }
}