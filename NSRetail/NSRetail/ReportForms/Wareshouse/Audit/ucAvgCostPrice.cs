using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucAvgCostPrice : SearchCriteriaBase
    {
        public ucAvgCostPrice()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANDNAME", "Brand" }
                , { "MANUFACTURERNAME", "Manufacturer" }
                , { "STOCKINCOSTPRICEWOT", "Cur.Mon. Stock in CP WOT" }
                , { "STOCKINCOSTPRICEWT", "Cur.Mon. Stock in CP WT" }
                , { "STOCKINQTY", "Cur.Mon. Stock in Qty" }
                , { "PREVMONCOSTPRICEWOT", "Prev.Mon. Avg CP WOT" }
                , { "PREVMONCOSTPRICEWT", "Prev.Mon. Avg CP WT" }
                , { "PREVMONCUMULATIVEFYQTY", "Prev.Mon. Cumulative Qty" }
                , { "PREVFYCOSTPRICEWOT", "Prev.F.Y. Avg CP WOT" }
                , { "PREVFYCOSTPRICEWT", "Prev.F.Y. Avg CP WT" }
                , { "COSTPRICEWOT", "Final CP WOT" }
                , { "COSTPRICEWT", "Final CP WT" }
                , { "CUMULATIVEFYQTY", "Final Cumulative Qty" }
                , { "AVGCPDATE", "Avg. CP Date" }
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
                { "ItemID", sluSKUCode.EditValue },
                { "AvgCPDate", dtpAvgCPDate.EditValue }
            };

            return GetReportData("USP_RPT_AVG_COSTPRICE", parameters);
        }
    }
}
