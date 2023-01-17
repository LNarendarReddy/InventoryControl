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
    public partial class ucPurchases : SearchCriteriaBase
    {
        public ucPurchases()
        {
            InitializeComponent();
            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TOTALCPEWOT", "Total Cost Price WOT" },
                { "TOTALTAX", "Total Tax" },
                { "TOTALCPWT", "Total Cost Price WT" },
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "COSTPRICETAX", "Tax" },
                { "COSTPRICEWT", "Cost Price WT" },
                { "FINALPRICE", "Final Price" },
                { "PURCHASEQUANTITY", "Purchase Quantity" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "COSTPRICEWOT", "COSTPRICEWT", "COSTPRICETAX", "PURCHASEQUANTITY" })
                , new IncludeSettings("Supplier", "IncludeSupplier", new List<string>{ "DEALERNAME" },true)
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
            };

            SetFocusControls(cmbPeriodicity, dtpToDate, specificColumnHeaders);
        }

        private void ucPurchases_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbSupplier.Properties.DataSource = masterRepo.GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);
        }
        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbSupplier.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
            };

            return GetReportData("USP_RPT_PURCHASES", parameters);
        }
    }
}
