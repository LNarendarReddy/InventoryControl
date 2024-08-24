using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using NSRetail.ReportForms;
using NSRetailPOS.Operations.Items;
using NSRetailPOS.Operations.Reports;
using NSRetailPOS.Operations.Stock;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations
{
    public partial class frmOpetations : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public event EventHandler RefreshBaseLineData;
        public frmOpetations()
        {
            InitializeComponent();
        }

        private void bbiReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();

            ReportHolder POSReports = new ReportHolder() { ReportName = "POS Reports" };
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Invoice List", SearchCriteriaControl = new ucInvoiceList() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Dispatch List", SearchCriteriaControl = new ucDispatchList() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Sales", SearchCriteriaControl = new ucSales() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Non Moving Stock", SearchCriteriaControl = new ucNonMovingStock() });
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Branch Indent", SearchCriteriaControl = new ucBranchIndent() });
            reportList.Add(POSReports);

            frmReportPlaceHolder obj = new frmReportPlaceHolder(reportList, "Branch");
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnStockEntry_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStockEntry obj = new frmStockEntry();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnRefreshData_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(frmProgress), true, true);
            Utility.FillBaseLine();
            RefreshBaseLineData?.Invoke(null, null);
            SplashScreenManager.CloseForm();
        }

        private void btnStockIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmStockInList().ShowDialog();
        }

        private void btnBranchRefund_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBranchRefund obj = new frmBranchRefund()
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }

        private void btnItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmItemCodeList()
            {
                ShowInTaskbar = false,
                MdiParent = this,
                StartPosition = FormStartPosition.CenterParent,
                WindowState = FormWindowState.Maximized
            }.Show();
        }

        private void btnSupplierReturns_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSupplierReturns obj = new frmSupplierReturns();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }
    }
}