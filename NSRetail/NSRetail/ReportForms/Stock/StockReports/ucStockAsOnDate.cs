using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucStockAsOnDate : SearchCriteriaBase
    {
        private enum StockAsOnDateReportType
        {
            Branch,
            Warehouse,
            DamageAndWastage
        }

        private readonly StockAsOnDateReportType reportType;
        private readonly string procedureName;

        public ucStockAsOnDate() : this("USP_RPT_STOCK_ASOFDATE_WH", StockAsOnDateReportType.Warehouse)
        {
        }

        public ucStockAsOnDate(string procedureName, string reportType)
            : this(procedureName, (StockAsOnDateReportType)Enum.Parse(typeof(StockAsOnDateReportType), reportType))
        {
        }

        private ucStockAsOnDate(string procedureName, StockAsOnDateReportType reportType)
        {
            InitializeComponent();
            this.procedureName = procedureName;
            this.reportType = reportType;

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
            MandatoryFields = new List<BaseEdit> { dtAsOnDate, cmbBranch, cmbCategory };
            HiddenColumns = new List<string>
            {
                "CATEGORYNAME",
                "LATESTCOSTPRICEWOT",
                "LATESTCOSTPRICETAX",
                "TOTALLATESTCOSTPRICEWOT",
                "AVGCOSTPRICEWOT",
                "AVGCOSTPRICETAX",
                "TOTALAVGCOSTPRICEWOT"
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Latest Cost price & totals", "IncludeLatestCP"
                    , new List<string>{ "LATESTCOSTPRICEWOT", "LATESTCOSTPRICETAX", "LATESTCOSTPRICEWT", "TOTALLATESTCOSTPRICEWOT", "TOTALLATESTCOSTPRICEWT"
                    }, true),
                new IncludeSettings("Avg. Cost price & totals", "IncludeAvgCP"
                    , new List<string>{ "AVGCOSTPRICEWOT", "AVGCOSTPRICETAX", "AVGCOSTPRICEWT", "TOTALAVGCOSTPRICEWOT", "TOTALAVGCOSTPRICEWT"
                    }, true),
                new IncludeSettings("Brand & Manufacturer", "IncludeBrand", new List<string>{ "BRANDNAME", "MANUFACTURERNAME" }, false),
            };
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            object itemID = rowhandle >= 0 ? searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID") : 0;

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue },
                { "AsOnDate", dtAsOnDate.EditValue },
                { "ITEMID", itemID },
                { "CategoryID", cmbCategory.EditValue },
            };
            return GetReportData(procedureName, parameters);
        }

        public override void ControlBoundCompleted()
        {
            DataTable dtBranches = Utility.GetBranchList().Copy();
            DataView dvBranches = dtBranches.DefaultView;

            switch (reportType)
            {
                case StockAsOnDateReportType.Warehouse:
                    dvBranches.RowFilter = "BRANCHID = 45";
                    break;
                case StockAsOnDateReportType.DamageAndWastage:
                    dvBranches.RowFilter = "BRANCHID = 100";
                    break;
                default:
                    dvBranches.RowFilter = "BRANCHID NOT IN (45, 91, 92, 97, 100, 103)";
                    break;
            }

            cmbBranch.Properties.DataSource = dvBranches.ToTable();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.CheckAll();

            if (reportType == StockAsOnDateReportType.Branch && !string.IsNullOrEmpty(Convert.ToString(cmbBranch.EditValue)))
            {
                List<int> branchIDs = cmbBranch.EditValue.ToString().Split(',').Select(x => int.Parse(x)).ToList();
                excludedBranches.Where(x => branchIDs.Contains(x)).ToList().ForEach(x => branchIDs.Remove(x));
                cmbBranch.EditValue = string.Join(",", branchIDs);
            }

            RemoveCheckedComboBoxEnter(cmbBranch);
            AddCheckedComboBoxEnter(cmbBranch);
            cmbBranch.EnterMoveNextControl = true;
        }
    }
}
