using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucSupplierWiseSales : SearchCriteriaBase
    {
        MasterRepository masterRepo = new MasterRepository();
        Dictionary<string, string> procedures = new Dictionary<string, string>()
        {
            {"S", "USP_RPT_SUPPLIERWISESALES" },
            {"D", "USP_RPT_SUPPLIERWISEDISPATCHES" },
            {"B", "USP_RPT_SUPPLIERWISEBREFUNDS" },
            {"C", "USP_RPT_SUPPLIERWISECREFUNDS" }
        };

        string reporttype = string.Empty;

        public ucSupplierWiseSales(string _reporttype)
        {
            reporttype = _reporttype;
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "DEALERNAME", "Supplier" }
                , { "BILLDATE", "Bill Date" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP",
                    "COSTPRICEWOT", "COSTPRICEWT", "COSTPRICETAX", "SALEPRICEWT","SALEPRICEWOT", "SALEPRICETAX", "QUANTITY" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" })
                , new IncludeSettings("Date", "IncludeDate", new List<string>{ "BILLDATE" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("Sub Category", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
            };    

            //if(reporttype == "B") IncludeSettingsCollection.Add(new IncludeSettings("Reason", ""))

            cmbSupplier.Properties.DataSource = masterRepo.GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            cmbBranch.Properties.DataSource = masterRepo.GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbSupplier, dtpToDate, columnHeaders);
            MandatoryFields = new List<BaseEdit> { cmbSupplier };
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbSupplier.EditValue }
                , { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
           
            DataTable dt = GetReportData(procedures[reporttype], parameters);
            return dt;
            
        }
    }
}
