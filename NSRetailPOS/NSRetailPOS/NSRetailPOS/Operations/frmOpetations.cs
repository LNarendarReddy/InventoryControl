﻿using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using NSRetail.ReportForms;
using NSRetailPOS.Operations.Reports;
using NSRetailPOS.Operations.Stock;
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
    }
}