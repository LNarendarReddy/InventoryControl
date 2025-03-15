using DataAccess;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucLeftOverPrices : SearchCriteriaBase
    {
        public ucLeftOverPrices()
        {
            InitializeComponent();

            SetFocusControls(cmbCategory, cmbCategory, null);
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
