using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucSupplierRefunds : SearchCriteriaBase
    {
        public ucSupplierRefunds()
        {
            InitializeComponent();
            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "CREATEDBY", "Refund By" },
                { "CREATEDDATE", "Refund Date" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" },
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" }, true)

                , new IncludeSettings("Item details", "IncludeItem", new List<string>{
                    "SKUCODE", "VENDORSKUCODE", "ITEMNAME", "ITEMCODE", "HSNCODE", "MRP", "GSTCODE",
                    "QUANTITY" })

                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })

                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })

                , new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" })

                , new IncludeSettings("Manufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })

                , new IncludeSettings("Return Number", "IncludeRefundNumber", new List<string> { "SUPPLIERRETURNSNO" })
                
                , new IncludeSettings("Supplier", "IncludeSupplier", new List<string> { "SupplierName" })

                , new IncludeSettings("User Details", "IncludeUserDetails", new List<string> { "CreatedBy", "CreatedDate" })

            };

            SetFocusControls(cmbPeriodicity, cmbItemCode, specificColumnHeaders);
        }

        private void ucSupplierRefunds_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            cmbReportMode.Properties.DataSource = GetSupplierReturnType();
            cmbReportMode.Properties.DisplayMember = "Text";
            cmbReportMode.Properties.ValueMember = "Value";
            cmbReportMode.EditValue = 0;

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);


        }

        private DataTable GetSupplierReturnType()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Value", typeof(int));
            dt.Columns.Add("Text", typeof(string));

            dt.Rows.Add(0, "Supplier Returns");
            dt.Rows.Add(1, "Write-off/Nullify Only");

            return dt;
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "BranchID", cmbBranch.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
                , { "ReportMode", cmbReportMode.EditValue}
            };

            return GetReportData("USP_RPT_SUPPLIER_RETURNS_REGISTER", parameters);
        }
    }
}
