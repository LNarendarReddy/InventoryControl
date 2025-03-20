using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucStockAsOnDate : SearchCriteriaBase
    {
        public ucStockAsOnDate()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "QTYORWGHT", "Quantity or Weight in KG(s)" }
                , { "BRANDNAME", "Brand" }
                , { "MANUFACTURERNAME", "Manufacturer" }
                , { "LATESTCOSTPRICEWOT", "Latest CP W\\O Tax" }
                , { "LATESTCOSTPRICETAX", "Latest CP Tax" }
                , { "LATESTCOSTPRICEWT", "Latest CP With Tax" }
                , { "TOTALLATESTCOSTPRICEWOT", "Total Latest CP W\\O Tax" }
                , { "TOTALLATESTCOSTPRICEWT", "Total Latest CP With Tax" }
                , { "AVGCOSTPRICEWOT", "Avg. CP W\\O Tax" }
                , { "AVGCOSTPRICETAX", "Avg. CP Tax" }
                , { "AVGCOSTPRICEWT", "Avg. CP With Tax" }
                , { "TOTALAVGCOSTPRICEWOT", "Total Avg. CP W\\O Tax" }
                , { "TOTALAVGCOSTPRICEWT", "Toatl Avg. CP With Tax" }
            };

            SetFocusControls(dtAsOnDate, cmbItemCode, columnHeaders);

            dtAsOnDate.EditValue = DateTime.Today;
            MandatoryFields = new List<BaseEdit> { dtAsOnDate };
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue },
                { "AsOnDate", dtAsOnDate.EditValue },
                { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")},
                { "CategoryID", cmbCategory.EditValue },
            };
            return GetReportData("USP_RPT_STOCK_ASOFDATE", parameters);
        }
    }
}
