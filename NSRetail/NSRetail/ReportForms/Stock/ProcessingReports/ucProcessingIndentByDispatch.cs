﻿using DataAccess;
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
                , { "SubCategoryID", cmbSubCat.EditValue}
            };

            DataSet dsData = (DataSet)GetReportData("USP_RPT_BULK_PROCESSING_INDENT_BY_DISPATCH", parameters);
            //dsData.Relations.Add("Item wise", dsData.Tables[0].Columns["ITEMID"], dsData.Tables[1].Columns["PARENTITEMID"]);
            //dsData.Relations.Add("Item by Branch wise", dsData.Tables[1].Columns["ITEMID"], dsData.Tables[2].Columns["ITEMID"]);
            dsData.Relations.Add("Item by Branch wise", dsData.Tables[0].Columns["ITEMID"], dsData.Tables[1].Columns["ITEMID"]);
            return dsData;
        }

        //public override void DataBoundCompleted()
        //{
        //    base.DataBoundCompleted();
        //    ExpandAllMasterRows();
        //}

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            DataView dtSubcategory = new MasterRepository().GetSubCategory().Copy().DefaultView;
            dtSubcategory.RowFilter = $"CATEGORYID = {cmbCategory.EditValue}";

            cmbSubCat.Properties.DataSource = dtSubcategory;
            cmbSubCat.Properties.ValueMember = "SUBCATEGORYID";
            cmbSubCat.Properties.DisplayMember = "SUBCATEGORYNAME";
        }
    }
}
