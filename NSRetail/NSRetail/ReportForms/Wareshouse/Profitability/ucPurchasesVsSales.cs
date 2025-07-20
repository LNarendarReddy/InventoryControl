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

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class ucPurchasesVsSales : SearchCriteriaBase
    {
        public ucPurchasesVsSales()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TOTALCPWOT", "Total Cost Price WOT" },
                { "TOTALCOSTPRICETAX", "Total Cost Price Tax" },
                { "TOTALCPWT", "Total Cost Price WT" },
                { "AVGCOSTPRICEWOT", "Avg. Cost Price WOT" },
                { "AVGCOSTPRICETAX", "Avg. Cost Price Tax" },
                { "AVGCOSTPRICEWT", "Avg. Cost Price WT" },
                { "TOTALFINALCOSTPRICE", "Total Final Cost Price" },
                { "PURCHASEQUANTITY", "Purchase Quantity" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" },
                { "GSTIN", "Supplier GSTIN" },
                { "SALEQUANTITY", "Sale Quantity" },
                { "TOTALSALEPRICEWOT", "Total Sale Price WOT" },
                { "TOTALSALEPRICEWT", "Total Sale Price WT" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "PURCHASEQUANTITY", "SALEQUANTITY" })
                , new IncludeSettings("Item price details", "IncludeItemPriceDetails", new List<string>{ "ITEMCODE", "MRP", "AVGCOSTPRICEWOT", "AVGCOSTPRICEWT", "AVGCOSTPRICETAX" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" })
                , new IncludeSettings("SubManufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })
            };

            SetFocusControls(cmbSupplier, cmbCategory, specificColumnHeaders);
        }

        private void ucPurchasesVsSales_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbSupplier.Properties.DataSource = masterRepo.GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;
        }

        public override object GetData()
        {
            if (IncludeSettingsCollection.First(x => x.ParameterName == "IncludeItemPriceDetails").Included)
                IncludeSettingsCollection.First(x => x.ParameterName == "IncludeItem").Included = true;

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbSupplier.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_PURCHASES_VS_SALES", parameters);
        }
    }
}
