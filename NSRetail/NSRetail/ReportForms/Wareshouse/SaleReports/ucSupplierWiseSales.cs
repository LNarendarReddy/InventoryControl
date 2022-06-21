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

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucSupplierWiseSales : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override IEnumerable<BaseEdit> MandatoryFields => new List<BaseEdit> { cmbSupplier };

        public override Control FirstControl => cmbSupplier;
        public override Control LastControl => dtpToDate;
        public ucSupplierWiseSales()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
            {
                { "DEALERNAME", "Supplier" }
                , { "BILLDATE", "Bill Date" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE", "SALEPRICEWOT", "SALEPRICETAX", "SALEQUANTITY" })
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" })
                , new IncludeSettings("Date", "IncludeDate", new List<string>{ "BILLDATE" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("Sub Category", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
            };    

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }
        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbSupplier.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            DataTable dt = GetReportData("USP_RPT_SUPPLIERWISESALES", parameters);
            return dt;
            
        }
    }
}
