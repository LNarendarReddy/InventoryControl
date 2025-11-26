using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucStockSummaryByPrice : SearchCriteriaBase
    {
        public ucStockSummaryByPrice()
        {
            InitializeComponent();

            MasterRepository masterRepo = new MasterRepository();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "QTYORWGHT", "Qty or Weight in KGs" }
                , { "BRANDNAME", "Brand" }
                , { "MANUFACTURERNAME", "Manufacturer" }
                , { "INTRANSITQTYORWGHT", "In-Transit Qty or Weight in KGs" }
            };

            MandatoryFields = new List<BaseEdit> { cmbBranch };

            IncludeSettingsCollection = new List<IncludeSettings>
            {
                new IncludeSettings("Item Code", "IncludeItemCode", new List<string>() { "ITEMCODE" }, false),
                new IncludeSettings("Item price", "IncludeMRP", new List<string>() { "MRP", "SALEPRICE" }, false),
                new IncludeSettings("Positives", "IncludePositives", new List<string>(), true),
                new IncludeSettings("Zero", "IncludeZeros", new List<string>(), false),
                new IncludeSettings("Negatives", "IncludeNegatives", new List<string>(), false)
            };

            SetFocusControls(cmbBranch, cmbItemCode, columnHeaders);

            btnImportMBQ.Enabled = AccessUtility.HasAccess("D2883DFC-597C-4E46-943D-84FC26541F25::Execute");
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
            };

            return GetReportData("USP_RPT_STOCKSUMMARY", parameters);
        }

        private void btnImportMBQ_Click(object sender, System.EventArgs e)
        {
            new frmImportMBQ().ShowDialog();
        }
    }
}
