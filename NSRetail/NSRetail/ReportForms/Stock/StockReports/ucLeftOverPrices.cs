using DataAccess;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucLeftOverPrices : SearchCriteriaBase
    {
        public ucLeftOverPrices()
        {
            InitializeComponent();

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            SetFocusControls(cmbCategory, cmbCategory, null);

            AllowedRoles = new List<string> { "Division Manager", "IT User", "Division User" };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_LEFTOVERPRICES", parameters);
        }
    }
}
