using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucCostPriceAudit : SearchCriteriaBase
    {
        public ucCostPriceAudit()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "CREATEDBY", "Created By" }
                , { "DELETEDBY", "Deleted By" }
                , { "DELETEDDATE", "Deleted Date" }
                , { "CREATEDTIME", "Created Time" }
                , { "DELETEDTIME", "Deleted Time" }
                , { "SUPPLIERINVOICENO", "Invoice #" }
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            sluSKUCode.Properties.DataSource = Utility.GetItemCodeList();
            sluSKUCode.Properties.DisplayMember = "ITEMNAME";
            sluSKUCode.Properties.ValueMember = "ITEMID";

            SetFocusControls(cmbSupplier, cmbSupplier, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "ItemID", sluSKUCode.EditValue }
                , { "DealerID", cmbSupplier.EditValue}
            };

            return GetReportData("USP_RPT_AUDIT_COSTPRICE", parameters);
        }
    }
}
