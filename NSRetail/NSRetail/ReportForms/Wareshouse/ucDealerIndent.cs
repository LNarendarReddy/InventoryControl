using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms
{
    public partial class ucDealerIndent : SearchCriteriaBase
    {       
        MasterRepository masterRepository = new MasterRepository();

        public ucDealerIndent()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "ALLBRANCHSTOCK", "All Branch Stock" }
                , { "SALEQUANTITY", "Sale Quantity" }
                , { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "INDENTQUANTITY", "Calculated Indent" }
                , { "DESIREDINDENT", "Desired Indent" }
            };

            EditableColumns = new List<string>() { "DESIREDINDENT" };
            MandatoryFields = new List<BaseEdit>() { cmbDealer, cmbCategory, dtFromDate, dtToDate };

            cmbDealer.Properties.DataSource = masterRepository.GetDealer();
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.Properties.DisplayMember = "DEALERNAME";

            cmbCategory.Properties.DataSource = masterRepository.GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbDealer, dtToDate, columnHeaders);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {              
                if (ResultGridView?.RowCount == 0 || !ValidateMandatoryFields())
                    return;

                GridControl resultsGrid = ResultGrid;
                DealerIndent dealerIndent = new DealerIndent();
                dealerIndent.supplierID = cmbDealer.EditValue;
                dealerIndent.FromDate = dtFromDate.EditValue;
                dealerIndent.ToDate = dtToDate.EditValue;
                dealerIndent.CategoryID = cmbCategory.EditValue;
                dealerIndent.UserID = Utility.UserID;
                dealerIndent.dtSupplierIndent = ((DataTable)resultsGrid.DataSource).Copy();
                new ReportRepository().SaveSupplierIndent(dealerIndent);
                resultsGrid.DataSource = null;
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}
