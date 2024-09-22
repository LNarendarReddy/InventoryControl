using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucSalePriceAudit : SearchCriteriaBase
    {
        public ucSalePriceAudit()
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

            sluSKUCode.Properties.DataSource = Utility.GetItemCodeList();
            sluSKUCode.Properties.DisplayMember = "ITEMNAME";
            sluSKUCode.Properties.ValueMember = "ITEMID";

            SetFocusControls(sluSKUCode,sluSKUCode, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "ItemID", sluSKUCode.EditValue }
            };

            return GetReportData("USP_RPT_AUDIT_SALEPRICE", parameters);
        }
    }
}
