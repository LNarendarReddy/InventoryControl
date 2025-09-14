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
    public partial class ucDispatchVsSales : SearchCriteriaBase
    {
        public ucDispatchVsSales()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TRANSACTIONDATE", "Date" },
                { "HSNCODE", "HSN Code" },
                { "DISPATCHQTY", "Dispatch Qty" },
                { "SALEQTY", "Sale Qty" },
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Branch Name", "IncludeBranch", new List<string>{ "BRANCHNAME"}, true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME"}, true)
                , new IncludeSettings("Item Code & MRP", "IncludeItemPrice", new List<string>{ "ITEMCODE", "MRP", "HSNCODE" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" }, true)
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" })
                , new IncludeSettings("Manufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("SubClassification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })
                , new IncludeSettings("Stock", "IncludeStock", new List<string>{ "CURWHSTOCK", "CURBRANCHSTOCK" })
                , new IncludeSettings("Date", "IncludeDate", new List<string>{ "TRANSACTIONDATE" }, true)
            };

            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate);
            SetFocusControls(cmbBranch, cmbItemCode, specificColumnHeaders);
        }

        private void ucPurchasesVsSales_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }

        public override object GetData()
        {
            if (IncludeSettingsCollection.First(x => x.ParameterName == "IncludeItemPrice").Included)
                IncludeSettingsCollection.First(x => x.ParameterName == "IncludeItem").Included = true;

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "CategoryIDs", cmbCategory.EditValue }
                , { "BranchIDs", cmbBranch.EditValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
            };

            return GetReportData("USP_RPT_DISPATCH_VS_SALES", parameters);
        }
    }
}
