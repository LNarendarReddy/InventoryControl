﻿using DataAccess;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucZeroStock : SearchCriteriaBase
    {
        public ucZeroStock()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch Name" }
                , { "SKUCODE", "SKU Code" }
                , { "ITEMCODE", "Item Code" }
                , { "ITEMNAME", "Item Name" }
                , { "WAREHOUSESTOCK", "Warehouse Stock" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "CATEGORYNAME", "Category" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "BRANDNAME", "Brand" }
                , { "MANUFACTURERNAME", "Manufacturer" }
            };

            SetFocusControls(cmbBranch, cmbCategory, columnHeaders);

            txtThresholdValue.EditValue = 0;
            txtThresholdValue.Enabled = false;
            MandatoryFields = new List<BaseEdit> { cmbBranch, cmbCategory };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue },
                { "CategoryID", cmbCategory.EditValue },
                { "ThresholdValue", txtThresholdValue.EditValue }
            };
            return GetReportData("USP_RPT_ZEROSTOCK", parameters);
        }
    }
}
