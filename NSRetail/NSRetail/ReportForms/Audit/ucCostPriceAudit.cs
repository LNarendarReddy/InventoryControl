using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Audit
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
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            sluSKUCode.Properties.DataSource = Utility.GetItemCodeList();
            sluSKUCode.Properties.DisplayMember = "ITEMNAME";
            sluSKUCode.Properties.ValueMember = "ITEMID";

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            SetFocusControls(cmbCategory, cmbSupplier, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "ItemID", sluSKUCode.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
                , { "DealerID", cmbSupplier.EditValue}
            };

            return GetReportData("USP_RPT_AUDIT_COSTPRICE", parameters);
        }
    }
}
