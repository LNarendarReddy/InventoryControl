using DataAccess;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace NSRetail.Stock
{
    public partial class frmGeneratePickList : DevExpress.XtraEditors.XtraForm
    {
        public frmGeneratePickList()
        {
            InitializeComponent();
        }

        private void frmGeneratePickList_Load(object sender, EventArgs e)
        {
            luCategory.Properties.DataSource = Utility.GetCategoryListExceptAll();
            luCategory.Properties.ValueMember = "CATEGORYID";
            luCategory.Properties.DisplayMember = "CATEGORYNAME";

            luSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            luSupplier.Properties.ValueMember = "DEALERID";
            luSupplier.Properties.DisplayMember = "DEALERNAME";
        }

        private void btnGeneratePickList_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            gcPickList.DataSource = new ReportRepository().GetReportData("USP_RPT_PICKLIST", new Dictionary<string, object>()
            {
                { "CategoryID", luCategory.EditValue },
                { "SupplierID", luSupplier.EditValue }
            });

            gcPickList.BestFit();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //gcPickList.OptionsPrint.PageSettings.Landscape = true;
            //gcPickList.ShowPrintPreview();

            PrintingSystem ps = new PrintingSystem();
            PrintableComponentLink link = new PrintableComponentLink(ps)
            {
                Component = gcPickList,
                Landscape = true,
                PaperKind = System.Drawing.Printing.PaperKind.A4,
                Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0)
            };

            // Scale to fit page width
            link.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            link.CreateDocument();
            link.ShowPreviewDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            rptPickList pickList = new rptPickList(luSupplier.Text, luCategory.Text, (DataTable)gcPickList.DataSource);
            pickList.ShowRibbonPreview();
        }
    }
}