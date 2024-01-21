using DataAccess;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class ucItemMargin : SearchCriteriaBase
    {
        public ucItemMargin()
        {
            InitializeComponent();

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
                { "MRPMARGINWOT", "MRP Margin WOT" },
                { "MRPMARGINWT", "MRP Margin WT" },
                { "MRPMARGINWOTPER", "MRP Margin % WOT" },
                { "MRPMARGINWTPER", "MRP Margin % WT" },
                { "ACTUALMARGINWOT", "Actual Margin WOT" },
                { "ACTUALMARGINWT", "Actual Margin WT" },
                { "ACTUALMARGINWOTPER", "Actual Margin % WOT" },
                { "ACTUALMARGINWTPER", "Actual Margin % WT" },
                { "ACTUALPRICEWT", "Actual Price WT" },
                { "ACTUALPRICEWOT", "Actual Price WOT" },
                { "MRPWOT", "MRP WOT" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "OFFERNAME", "Applied Offer Name" }
            };

            SetFocusControls(cmbCategory, cmbItemCode, specificColumnHeaders);
            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Classification", "IncludeClassification", new List<string>() { "CLASSIFICATIONNAME" }),
                new IncludeSettings("Sub-Classification", "IncludeSubClassification", new List<string>() { "SUBCLASSIFICATIONNAME" }),
            };
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CategoryID", cmbCategory.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
            };

            return GetReportData("USP_RPT_ITEM_MARGIN", parameters);
        }
    }
}
