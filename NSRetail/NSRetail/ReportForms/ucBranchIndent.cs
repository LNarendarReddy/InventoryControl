using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms
{
    public partial class ucBranchIndent : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;

        public ucBranchIndent()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "WAREHOUSEWEIGHTINKGS", "Warehouse Weight In KGs" }
                , { "BRANCHQUANTITY", "Branch Quantity" }
                , { "BRANCHWEIGHTINKGS", "Branch Weight In KGs" }
                , { "BRANCHINTRANSITQUANTITY", "Branch In-Transit Quantity" }
                , { "BRANCHINTRANSITWEIGHTINKGS", "Branch In-Transit Weight In KGs" }
                , { "SALEQUANTITY", "Sale Quantity" }
                , { "SALEWEIGHTINKGS", "Sale Weight In KGs" }
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "INDENTWEIGHT", "Indent Weight In KGs" }
            };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            return GetReportData("USP_R_BRANCHINDENT", parameters);
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;
    }
}
