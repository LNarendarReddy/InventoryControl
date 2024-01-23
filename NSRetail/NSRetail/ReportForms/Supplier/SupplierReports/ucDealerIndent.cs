using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucDealerIndent : SearchCriteriaBase
    {       
        MasterRepository masterRepository = new MasterRepository();

        public ucDealerIndent()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse qty" }
                , { "REQUIREDBRANCHSTOCK", "Required Branch Dispatch" }
                , { "BRANCHSTOCK", "Available Branch Stock" }
                , { "AVGSALEQUANTITY", "Avg Sale qty" }
                , { "BRANCHINDENTQUANTITY", "Branch Indent Needed" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "REQUIREDITEMINDENT", "Calculated Indent" }
                , { "DESIREDINDENT", "Desired Indent" }
            };

            EditableColumns = new List<string>() { "DESIREDINDENT" };
            MandatoryFields = new List<BaseEdit>() { cmbDealer, cmbCategory, txtIndentDays, txtSafetyDays };

            cmbDealer.Properties.DataSource = masterRepository.GetDealer();
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.Properties.DisplayMember = "DEALERNAME";

            DataView dvCategory = Utility.GetCategoryList().Copy().DefaultView;
            dvCategory.RowFilter = "CATEGORYNAME <> 'ALL'";
            cmbCategory.Properties.DataSource = dvCategory;
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            txtIndentDays.EditValue = 5;
            txtSafetyDays.EditValue = 2;

            SetFocusControls(cmbDealer, txtSafetyDays, columnHeaders);

            IsDataSet = true;
            AllowedRoles = new List<string> { "Division Manager", "IT User", "Division User" };
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbDealer.EditValue }
                , { "IndentDays", txtIndentDays.EditValue }
                , { "SafetyStockDays", txtSafetyDays.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            DataSet dsData = (DataSet)GetReportData("USP_R_DEALERINDENT", parameters);
            dsData.Relations.Add("Branch wise break-up", dsData.Tables[0].Columns["ITEMID"], dsData.Tables[1].Columns["ITEMID"]);
            DataTable dtTemp = dsData.Tables[0];
            dtTemp.Columns.Add("SUPPLIERINDENTDETAILID", typeof(int));
            dtTemp.Columns["SUPPLIERINDENTDETAILID"].SetOrdinal(0);
            return dsData;
        }

        public override void DataBoundCompleted()
        {
            GridColumn gcIndentDaysSaleQuantity = ResultGridView.Columns.ColumnByFieldName("INDENTDAYSSALEQUANTITY");
            if (gcIndentDaysSaleQuantity != null)
            {
                gcIndentDaysSaleQuantity.Caption = $"Last {txtIndentDays.EditValue} days sale";
            }
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
                //dealerIndent.FromDate = dtFromDate.EditValue;
                //dealerIndent.ToDate = dtToDate.EditValue;
                dealerIndent.CategoryID = cmbCategory.EditValue;
                dealerIndent.UserID = Utility.UserID;
                dealerIndent.dtSupplierIndent = ((DataSet)resultsGrid.DataSource).Tables[0].Copy();
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
