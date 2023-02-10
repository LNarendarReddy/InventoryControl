using DataAccess;
using Entity;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Supplier.SupplierWiseReports
{
    public partial class ucSupplierItemMargin : SearchCriteriaBase
    {
        public ucSupplierItemMargin()
        {
            InitializeComponent();
            cmbsupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbsupplier.Properties.ValueMember = "DEALERID";
            cmbsupplier.Properties.DisplayMember = "DEALERNAME";

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

            SetFocusControls(cmbsupplier, cmbsupplier, specificColumnHeaders);
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "SupplierID", cmbsupplier.EditValue }
            };

            return GetReportData("USP_RPT_ITEM_MARGIN_SUPPLIER", parameters);
        }
    }
}
