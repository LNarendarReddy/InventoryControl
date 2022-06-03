using DataAccess;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucInvoiceList : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        List<string> buttonColumns;
        List<string> summaryColumns;
        List<string> hiddenColumns;

        public ucInvoiceList()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
            {
                { "SNO", "S No" }
                , { "STOCKENTRYID", "Stock Entry ID" }
                , { "SUPPLIERINVOICENO", "Invoice Number" }
                , { "DEALERNAME", "Supplier" }
                , { "CATEGORYNAME", "Category" }
                , { "INVOICEDATE", "Invoice Date" }
                , { "CREATEDBY", "User Name" }
                , { "CREATEDDATE", "Created Date" }
                , { "FINALPRICE", "Net Amount" }
                , { "STATUS", "Status" }
            };

            buttonColumns = new List<string>() { "View" };
            summaryColumns = new List<string>() { "FINALPRICE"};
            hiddenColumns = new List<string>() { "TAXINCLUSIVE", "TCS", "DISCOUNTPER", "DISCOUNT", "EXPENSES", "TRANSPORT" };

            cmbDealer.Properties.DataSource = new MasterRepository().GetDealer();
            cmbDealer.Properties.DisplayMember = "DEALERNAME";
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.EditValue = 0;
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override IEnumerable<string> ButtonColumns => buttonColumns;

        public override IEnumerable<string> TotalSummaryFields => summaryColumns;

        public override IEnumerable<string> HiddenColumns => hiddenColumns;

        public override Control FirstControl => cmbDealer;

        public override Control LastControl => dtpToDate;

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbDealer.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_R_INVOICELIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "View":
                    DataSet ds = new StockRepository().GetInvoice(drFocusedRow["STOCKENTRYID"]);
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        rptInvoice rpt = new rptInvoice(ds.Tables[0], ds.Tables[1]);
                        rpt.ShowPrintMarginsWarning = false;
                        rpt.ShowRibbonPreview();
                    }
                    break;
            }
        }
    }
}
