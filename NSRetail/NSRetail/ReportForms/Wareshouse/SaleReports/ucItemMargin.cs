using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucItemMargin : SearchCriteriaBase
    {
        public ucItemMargin()
        {
            InitializeComponent();

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.EditValue = 13;

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "IPGSTCODE", "Sale Price GST %" },
                { "IPCREATEDBY", "Sale Price Created By" },
                { "IPCREATEDDATE", "Sale Price Created Date" },
                { "IPDELETEDBY", "Sale Price Deleted By" },
                { "IPDELETEDDATE", "Sale Price Deleted Date" },
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "COSTPRICEWT", "Cost Price WT" },
                { "ICPGSTCODE", "Cost Price GST %" },
                { "ICPCREATEDBY", "Cost Price Created By" },
                { "ICPCREATEDDATE", "Cost Price Created Date" },
                { "ICPDELETEDBY", "Cost Price Deleted By" },
                { "ICPDELETEDDATE", "Cost Price Deleted Date" },
                { "MARGINWOT", "Margin WOT" },
                { "MARGINWT", "Margin WT" }
            };

            SetFocusControls(cmbCategory, cmbCategory, specificColumnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_ITEM_MARGIN", parameters);
        }
    }
}
