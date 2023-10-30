using DevExpress.XtraBars;
using NSRetail.ReportForms;
using NSRetailPOS.Operations.Reports;
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
        public frmOpetations()
        {
            InitializeComponent();
        }

        private void bbiReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<ReportHolder> reportList = new List<ReportHolder>();

            ReportHolder POSReports = new ReportHolder() { ReportName = "POS Reports" };
            POSReports.SubCategory.Add(new ReportHolder() { ReportName = "Sales", SearchCriteriaControl = new ucSales() });
            reportList.Add(POSReports);

            frmReportPlaceHolder obj = new frmReportPlaceHolder(reportList, "Branch");
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }
    }
}