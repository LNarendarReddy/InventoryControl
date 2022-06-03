using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms
{
    public partial class ucDealerIndent : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        List<string> editableColumns;
        MasterRepository masterRepository = new MasterRepository();
        List<BaseEdit> mandatoryFields;

        public ucDealerIndent()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "ALLBRANCHSTOCK", "All Branch Stock" }
                , { "SALEQUANTITY", "Sale Quantity" }
                , { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "INDENTQUANTITY", "Calculated Indent" }
                , { "DESIREDINDENT", "Desired Indent" }
            };

            editableColumns = new List<string>() { "DESIREDINDENT" };
            mandatoryFields = new List<BaseEdit>() { cmbDealer, cmbCategory, dtFromDate, dtToDate };

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

            DataTable dtTemp = GetReportData("USP_R_DEALERINDENT", parameters);
            dtTemp.Columns.Add("SUPPLIERINDENTDETAILID", typeof(int));
            dtTemp.Columns["SUPPLIERINDENTDETAILID"].SetOrdinal(0);
            return dtTemp;
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override IEnumerable<BaseEdit> MandatoryFields => mandatoryFields;

        public override IEnumerable<string> EditableColumns => editableColumns;

        public override Control FirstControl => cmbDealer;

        public override Control LastControl => dtToDate;
    }
}
