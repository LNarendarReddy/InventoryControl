using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
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
        }

        private void btnGeneratePickList_Click(object sender, EventArgs e)
        {
            if(luCategory.EditValue == null) return; 

            gcPickList.DataSource = new ReportRepository().GetReportData("USP_RPT_PICKLIST", new Dictionary<string, object>()
            {
                { "CategoryID", luCategory.EditValue }
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
    }
}