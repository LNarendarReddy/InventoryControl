using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Doc;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucSales : SearchCriteriaBase
    {
        public ucSales()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TOTALSALEPRICEWOT", "Total Sale Price WOT" },
                { "TOTALSALETAX", "Total Sale Price Tax" },
                { "TOTALSALEPRICEWT", "Total Sale Price WT" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICETAX", "Sale Price Tax" },
                { "SALEQUANTITY", "Sale Quantity" },
                { "ISOFFER", "Item Offer applied" },
                { "WHSTOCK", "Warehouse Stock" },
                { "BRANCHSTOCK", "Branch Stock" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE", "SALEPRICEWOT", "SALEPRICETAX", "SALEQUANTITY" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" },true)
                , new IncludeSettings("Counter", "IncludeCounter", new List<string>{ "COUNTERNAME" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("Sub Classification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })
                , new IncludeSettings("Stock & Is offer", "IncludeStock", new List<string>{ "ISOFFER", "WHSTOCK", "BRANCHSTOCK" })
            };

            SetFocusControls(cmbPeriodicity, dtpToDate, specificColumnHeaders);
        }
        private void ucSales_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbBranch.Properties.DataSource = masterRepo.GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.CheckAll();

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
            };

            return GetReportData("USP_RPT_SALES", parameters);
        }

        private void cmbBranch_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => {
                cmbBranch.ShowPopup();
            }));
        }
    }
}
