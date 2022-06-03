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
    public partial class ucTaxWiseSales : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;
        public ucTaxWiseSales()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "CLOSUREDATE", "Closure Date" }
                , { "GSTCODE", "GST Code" }
                , { "BILLEDVALUE", "Billed Value" }
            };

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
            };
            return GetReportData("USP_RPT_BRANCHSALEBYGST", parameters);
        }
    }
}
