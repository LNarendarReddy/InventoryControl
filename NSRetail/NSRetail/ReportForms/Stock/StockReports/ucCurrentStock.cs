using DataAccess;
using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucCurrentStock : SearchCriteriaBase
    {
        public ucCurrentStock()
        {
            InitializeComponent();

            MasterRepository masterRepo = new MasterRepository();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "STOCKQTYORWGHT", "Stock Qty or Weight in KGs" }
                , { "SALEQTYORWGHT", "Sale Qty or Weight in KGs" }
                , { "CURRENTQTYORWGHT", "Current Qty or Weight in KGs" }                
            };

            MandatoryFields = new List<BaseEdit> { cmbBranch, cmbCategory };

            SetFocusControls(cmbBranch, cmbItemCode, columnHeaders);
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

            return GetReportData("USP_RPT_CURRENTSTOCK", parameters);
        }
    }
}
