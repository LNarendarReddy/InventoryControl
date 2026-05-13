using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Entity;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucDealerIndentByMBQ : SearchCriteriaBase
    {       
        MasterRepository masterRepository = new MasterRepository();

        public ucDealerIndentByMBQ()
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
                , { "INDENTDAYSSALEQUANTITY", "Indent days Sale qty" }
            };

            EditableColumns = new List<string>() { "DesiredIndent" };
            MandatoryFields = new List<BaseEdit>() { cmbDealer, cmbCategory };

            cmbDealer.Properties.DataSource = masterRepository.GetDealer();
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.Properties.DisplayMember = "DEALERNAME";

            cmbCategory.Properties.DataSource = Utility.GetCategoryListExceptAll();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            SetFocusControls(cmbDealer, cmbCategory, columnHeaders);

            btnSave.Visible = false;
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbDealer.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            return GetReportData("USP_R_DEALERINDENT_MBQ", parameters);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateMandatoryFields()
                    || XtraMessageBox.Show("Are you sure you want to save?"
                        , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) 
                    return;

                if (ResultGridView?.RowCount == 0)
                { 
                    XtraMessageBox.Show("No records to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                DataTable dtDetails = DotMatrixPrintHelper.GetDataTableWYSIWYG(ResultGridView, true).Copy();
                dtDetails.Columns["RecentMRP"].ColumnName = "MRP";
                dtDetails.Columns["RecentCostPriceWT"].ColumnName = "COSTPRICEWT";
                //dtDetails.Columns["WHStock"].ColumnName = "WAREHOUSEQUANTITY";
                dtDetails.Columns["MBQDesiredQuantity"].ColumnName = "REQUIREDBRANCHSTOCK";
                dtDetails.Columns["CalculatedIndent"].ColumnName = "REQUIREDITEMINDENT";
                dtDetails.Columns["DesiredIndent"].ColumnName = "DESIREDINDENT";                                

                DealerIndent dealerIndent = new DealerIndent
                {
                    supplierID = cmbDealer.EditValue,
                    CategoryID = cmbCategory.EditValue,
                    UserID = Utility.UserID,
                    dtSupplierIndent = dtDetails
                };
                new ReportRepository().SaveSupplierIndent(dealerIndent, "Indent By MBQ");
                XtraMessageBox.Show("Indent saved successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResultGrid.DataSource = null;
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}
