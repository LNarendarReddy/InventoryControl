using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucDispatches : SearchCriteriaBase
    {
        private string ReportType = string.Empty;
        private Dictionary<string, string> procedures = new Dictionary<string, string>()
        {
            { "D","USP_RPT_DISPATCH"}
            ,{ "B","USP_RPT_BREFUND"}
            ,{ "C","USP_RPT_CREFUND"}
        };
        public ucDispatches(string _ReportType)
        {
            ReportType = _ReportType;
            InitializeComponent();
            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "COSTPRICEWT", "Cost Price WT" },
                { "COSTPRICETAX", "cost Price Tax" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICEWT", "Sale Price WT" },
                { "SALEPRICETAX", "Sale Price TAX" },
                { "TOTALCPEWOT", "Total CP WOT" },
                { "TOTALCPWT", "Total CP WT" },
                { "TOTALCPTAX", "Total CP TAX" },
                { "TOTALSPWOT", "Total SP WOT" },
                { "TOTALSPWT", "Total SP WT" },
                { "TOTALSPTAX", "Total SP TAX" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP",
                    "COSTPRICEWOT", "COSTPRICEWT", "COSTPRICETAX", "SALEPRICEWOT", "SALEPRICEWT", "SALEPRICETAX", "QUANTITY" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" },true)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
            };

            SetFocusControls(cmbPeriodicity, dtpToDate, specificColumnHeaders);
        }

        private void ucDispatches_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbBranch.Properties.DataSource = masterRepo.GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            SetPeriodicty(cmbPeriodicity, true);
        }
        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
            };

            return GetReportData(procedures[ReportType], parameters);
        }
    }
}
