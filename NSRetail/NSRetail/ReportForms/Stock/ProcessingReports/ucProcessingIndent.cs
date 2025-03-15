using DataAccess;
using DevExpress.XtraEditors;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucProcessingIndent : SearchCriteriaBase
    {
        public ucProcessingIndent()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WHSTOCK", "Warehouse Stock" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "PROCESSINGREQUIREDWGHTINKGS", "Processing needed wght. (In KG(s))" }
                , { "BRANCHREQUIREDWGHTINKGS", "Branch needed wght. (In KG(s))" }
                , { "BRANCHREQUIREDINDENT", "Branch Indent Quantity" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbCategory, txtIndentDays };

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

            DataSet dsData = (DataSet)GetReportData("USP_RPT_BULK_PROCESSING_INDENT", parameters);
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
