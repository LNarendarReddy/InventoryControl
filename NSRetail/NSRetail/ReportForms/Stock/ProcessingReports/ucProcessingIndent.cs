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
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "SALEQUANTITY", "90 Days sale qty" }
                , { "AVGSALEQUANTITY", "Average Sale Quantity" }                
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
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

            DataSet dsData = (DataSet)GetReportData("USP_RPT_BULK_PROCESSING_INDENT", parameters);
            dsData.Relations.Add("Bulk Processing - item wise", dsData.Tables[0].Columns["ITEMID"], dsData.Tables[1].Columns["PARENTITEMID"]);
            dsData.Relations.Add("Bulk Processing - item branch wise", dsData.Tables[1].Columns["ITEMID"], dsData.Tables[2].Columns["ITEMID"]);
            return dsData;
        }
    }
}
