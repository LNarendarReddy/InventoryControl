using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms
{
    public partial class ucDealerIndent : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        MasterRepository masterRepository = new MasterRepository();

        public ucDealerIndent()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "SALEQUANTITY", "Sale Quantity" }
                , { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
            };

            cmbDealer.Properties.DataSource = masterRepository.GetDealer();
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.Properties.DisplayMember = "DEALERNAME";

            cmbCategory.Properties.DataSource = masterRepository.GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbDealer.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            return GetReportData("USP_R_DEALERINDENT", parameters);
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override List<BaseEdit> MandatoryFields => new List<BaseEdit>() { cmbDealer, cmbCategory, dtFromDate, dtToDate };
    }
}
