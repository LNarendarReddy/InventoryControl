using DataAccess;
using DevExpress.XtraEditors;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucProcessingIndentByDispatch : SearchCriteriaBase
    {
        public ucProcessingIndentByDispatch()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WHSTOCK", "Warehouse Stock" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "INDENTNEEDEDINKGS", "Processing needed wght. (In KG(s))" }
                , { "SIXMONTHAVG", "6 months AVG" }
                , { "MAXOFAVG", "Max AVG value" }
                , { "AVAILABLESTOCK", "Available units" }
                , { "INDENTNEEDED", "Units needed" }
                , { "DISPATCHDATE", "Dispatch date" }
                , { "DISPATCHQUANTITY", "Dispatched units" }
                , { "DISPATCHTYPE", "Dispatch calculation type" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbCategory, txtIndentDays };

            cmbCategory.Properties.DataSource = Utility.GetCategoryList();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            txtIndentDays.EditValue = 5;
            IsDataSet = true;
            SetFocusControls(cmbCategory, txtIndentDays, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CategoryID", cmbCategory.EditValue}
                , { "NoOfDays", txtIndentDays.EditValue}
            };

            DataSet dsData = (DataSet)GetReportData("USP_RPT_BULK_PROCESSING_INDENT_BY_DISPATCH", parameters);
            dsData.Relations.Add("Item wise", dsData.Tables[0].Columns["ITEMID"], dsData.Tables[1].Columns["PARENTITEMID"]);
            dsData.Relations.Add("Item by Branch wise", dsData.Tables[1].Columns["ITEMID"], dsData.Tables[2].Columns["ITEMID"]);
            return dsData;
        }

        public override void DataBoundCompleted()
        {
            base.DataBoundCompleted();
            ExpandAllMasterRows();
        }
    }
}
