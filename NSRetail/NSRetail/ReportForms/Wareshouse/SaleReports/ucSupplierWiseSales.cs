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
        public override Control FirstControl => cmbSupplier;

        public override Control LastControl => chkIncludeBranch;

        public ucSupplierWiseSales()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
            {
                { "DEALERNAME", "Supplier" }
                , { "BILLDATE", "Bill Date" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;

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
                , { "IncludeBillDate", chkIncludeDate .EditValue }
                , { "IncludeItem", chkIncludeItem.EditValue }
                , { "IncludeBranch", chkIncludeBranch.EditValue }
            };
            DataTable dt = GetReportData("USP_RPT_SUPPLIERWISESALES", parameters);
            if (chkIncludeItem.EditValue.Equals(false))
            {
                dt.Columns.Remove("SKUCODE");
                dt.Columns.Remove("ITEMCODE");
                dt.Columns.Remove("ITEMNAME");
                dt.Columns.Remove("CATEGORYNAME");
                dt.Columns.Remove("SUBCATEGORYNAME");
                dt.Columns.Remove("MRP");
                dt.Columns.Remove("SALEPRICE");
            }
            if (chkIncludeBranch.EditValue.Equals(false)) dt.Columns.Remove("BRANCHNAME");
            if (chkIncludeDate.EditValue.Equals(false)) dt.Columns.Remove("BILLDATE");
            return dt;
            
        }
    }
}
