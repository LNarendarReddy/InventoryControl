using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Supplier.SupplierWiseReports
{
    public partial class ucSupplierStockSummary : SearchCriteriaBase
    {
        public ucSupplierStockSummary()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WEIGHTINKGS", "Weight In Kgs" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" },
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            SetFocusControls(cmbSupplier, cmbBranch, columnHeaders);
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "SupplierID", cmbSupplier.EditValue }
                , { "BranchID", cmbBranch.EditValue }
            };

            DataTable dt = (DataTable)GetReportData("USP_R_STOCKSUMMARY_SUPPLIER", parameters);
            return dt;
        }
    }
}
