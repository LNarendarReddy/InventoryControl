using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucBranchIndentByAVG : SearchCriteriaBase
    {
        bool valueChanged = false;

        public ucBranchIndentByAVG()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WAREHOUSEQUANTITY", "Warehouse Quantity" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "SALEQUANTITY", "90 Days sale qty" }
                , { "INDENTQUANTITY", "Indent Quantity" }
                , { "SUBCATEGORYNAME", "Sub Category" }
                , { "LASTDISPATCHDATE", "Last dispatch date" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, cmbCategory, txtIndentDays };

            HiddenColumns = new List<string>() { "AVGSALES", "NOOFDAYSSALES", "WHSTOCK" };

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            luManfacturer.Properties.DataSource = Utility.GetManufacturer();
            luManfacturer.Properties.ValueMember = "MANUFACTURERID";
            luManfacturer.Properties.DisplayMember = "MANUFACTURERNAME";


            cmbDealer.Properties.DataSource = new MasterRepository().GetDealer();
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.Properties.DisplayMember = "DEALERNAME";

            txtIndentDays.EditValue = 1;

            SetFocusControls(cmbBranch, luManfacturer, columnHeaders);
            chkIsDSD_CheckedChanged(null, null);

            AccessUtility.SetStatusByAccess(btnGenerateSupplierIndent);
        }

        public override object GetData()
        {
            if (chkIsDSD.Checked)
            {
                List<string> missingFields = new List<string>();

                if (luManfacturer.EditValue == null) missingFields.Add("Manufacturer");
                if (cmbDealer.EditValue == null) missingFields.Add("Supplier");

                if (missingFields.Count > 0)
                {
                    string message = "Following mandatory values are missing \n\n";
                    missingFields.ForEach(x => message += $"\r\r * {x}\n");

                    XtraMessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
                , { "NoOfDays", txtIndentDays.EditValue }
                , { "IsDSD", chkIsDSD.EditValue }
                , { "ManufacturerID", luManfacturer.EditValue }
                , { "SupplierID", cmbDealer.EditValue }
            };

            object dtResults = GetReportData("USP_RPT_BRANCHINDENT_AVG", parameters);
            valueChanged = false;
            return dtResults;
        }

        private void btnPrintToDM_Click(object sender, EventArgs e)
        {
            DotMatrixPrintHelper.PrintBranchIndent(
                cmbBranch.Text
                , cmbCategory.Text
                , "Average Indent"
                , txtIndentDays.Text
                , DotMatrixPrintHelper.GetDataTableWYSIWYG(ResultGridView));
        }

        private void btnSaveIndent_Click(object sender, EventArgs e)
        {
            if (ResultGrid.DataSource == null) return;

            try
            {
                DataTable indentTable = GetFilteredData();

                if (indentTable == null || indentTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No data to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = XtraMessageBox.Show("Are you sure want to save indent?",
                    "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Convert.ToString(result).ToLower() == "yes")
                {
                    new IndentRepository().SaveBranchIndent(
                    0, Utility.BranchID, cmbBranch.EditValue, cmbCategory.EditValue,
                    txtIndentDays.EditValue, "6M sale days AVG", Utility.UserID, indentTable);
                    XtraMessageBox.Show("Indent saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void chkIsDSD_CheckedChanged(object sender, EventArgs e)
        {
            luManfacturer.EditValue = null;
            cmbDealer.EditValue = null;
            luManfacturer.Enabled = chkIsDSD.Checked;
            cmbDealer.Enabled = chkIsDSD.Checked;
        }

        private void btnGenerateSupplierIndent_Click(object sender, EventArgs e)
        {
            if (ResultGrid.DataSource == null) return;

            if (valueChanged)
            {
                XtraMessageBox.Show("Manufacturer or supplier changed after the report. Please run search again to get relevant data before generating supplier indent"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DataTable indentTable = GetFilteredData();

                if (indentTable == null || indentTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No data to generate supplier indent", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (XtraMessageBox.Show("Are you sure want to generate supplier indent?",
                    "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                DealerIndent dealerIndent = new DealerIndent
                {
                    supplierID = cmbDealer.EditValue,
                    CategoryID = cmbCategory.EditValue,
                    IndentDays = txtIndentDays.EditValue,
                    SafetyDays = 0,
                    UserID = Utility.UserID,
                    dtSupplierIndent = GetFilteredData(),
                    BranchID = cmbBranch.EditValue
                };

                dealerIndent.dtSupplierIndent.Columns["BRANCHSTOCK"].ColumnName = "BranchQuantity";
                dealerIndent.dtSupplierIndent.Columns["DesiredQuantity"].ColumnName = "REQUIREDBRANCHSTOCK";
                dealerIndent.dtSupplierIndent.Columns["INDENTQUANTITY"].ColumnName = "REQUIREDITEMINDENT";

                dealerIndent.dtSupplierIndent.Columns.Add("DESIREDINDENT", typeof(decimal));
                dealerIndent.dtSupplierIndent.Columns.Add("SUPPLIERINDENTDETAILID", typeof(int));

                foreach (DataRow dr in dealerIndent.dtSupplierIndent.Rows)
                    dr["DESIREDINDENT"] = dr["REQUIREDITEMINDENT"];

                new ReportRepository().SaveSupplierIndent(dealerIndent, "DSD Indent by MBQ");
                XtraMessageBox.Show("Supplier Indent generated successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResultGrid.DataSource = null;
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void luManfacturer_EditValueChanged(object sender, EventArgs e)
        {
            valueChanged = true;
        }

        private void cmbDealer_EditValueChanged(object sender, EventArgs e)
        {
            valueChanged = true;
        }

        private void cmbBranch_EditValueChanged(object sender, EventArgs e)
        {
            valueChanged = true;
        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            valueChanged = true;
        }
    }
}
