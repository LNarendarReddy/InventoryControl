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
    public partial class ucItemWiseSales : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;
        public ucItemWiseSales()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "SALEQUANTITY", "Sale Quantity" }
                , { "WEIGHTINKGS", "Weight In Kgs" }
                , { "BILLDATE", "Bill Date" }
                , { "DEALERNAME", "Supplier" }
                , { "ACTUALBILLEDAMOUNT", "Billed Amount" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }
        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "IncludeBillDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_RPT_ITEMWISESALE", parameters);
        }
    }
}
