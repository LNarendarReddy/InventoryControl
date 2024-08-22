using DevExpress.XtraEditors;
using DevExpress.XtraReports.Wizards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucSupplierRefunds : SearchCriteriaBase
    {
        public ucSupplierRefunds()
        {
            InitializeComponent();
            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "BREFUNDNUMBER", "Branch Refund #" },
                { "RETURNSTATUS", "Refund Status" },
                { "CREATEDBY", "Refund By" },
                { "CREATEDDATE", "Refund Date" },
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                //, new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP",
                //    "QUANTITY","RETURNSTATUS", "CREATEDBY", "CREATEDDATE" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Reason", "IncludeReason", new List<string>{ "Reason" })
                , new IncludeSettings("BRefund Number", "IncludeBRefundNumber", new List<string>{ "BREFUNDNUMBER" })
                , new IncludeSettings("SRefund Number", "IncludeSRefundNumber", new List<string>{ "Supplier Returns #", "Supplier Name" })
            };

            SetFocusControls(cmbPeriodicity, cmbItemCode, specificColumnHeaders);
        }

        private void ucSupplierRefunds_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
                , { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_SUPPLIERREFUNDS", parameters);
        }
    }
}
