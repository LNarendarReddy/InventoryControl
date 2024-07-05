using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucInvoiceList : SearchCriteriaBase
    {       
        public ucInvoiceList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
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

            ButtonColumns = new List<string>() { "View", "Delete" };
            HiddenColumns = new List<string>() { "TAXINCLUSIVE", "TCS", "DISCOUNTPER", "DISCOUNT", "EXPENSES", "TRANSPORT" };

            cmbDealer.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbDealer.Properties.DisplayMember = "DEALERNAME";
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.EditValue = 0;
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbDealer, dtpToDate,columnHeaders);
            AllowedRoles = new List<string> { "IT User" };
        }

        public override object GetData()
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

            if (drFocusedRow["STATUS"].ToString() == "Draft")
            {
                XtraMessageBox.Show("Draft bills cannot be viewed, printed or deleted. The operation is cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            switch (buttonText)
            {
                case "View":
                    DataSet ds = new StockRepository().GetInvoice(drFocusedRow["STOCKENTRYID"]);
                    if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count <= 0)
                    {
                        XtraMessageBox.Show("No data returned from database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        rptInvoice rpt = new rptInvoice(ds.Tables[0], ds.Tables[1]);
                        rpt.ShowPrintMarginsWarning = false;
                        rpt.ShowRibbonPreview();
                    }
                    break;
                case "Delete":
                    break;
            }
        }
    }
}
