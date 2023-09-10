using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucNonMovingStock : SearchCriteriaBase
    {
        public ucNonMovingStock()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "STOCKQTYORWGHT", "Stock Qty or Weight in KGs" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-30);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch,cmbItemCode , columnHeaders);
            MandatoryFields = new List<BaseEdit> { cmbBranch, dtpFromDate, dtpToDate };
            AllowedRoles = new List<string> { "Division Manager", "IT User", "Division User" };
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
                , { "CategoryID", cmbCategory.EditValue }}; 
            return GetReportData("USP_RPT_NONMOVINGSTOCK", parameters);
        }
    }
}
