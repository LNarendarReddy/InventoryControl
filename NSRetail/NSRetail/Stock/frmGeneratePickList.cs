using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace NSRetail.Stock
{
    public partial class frmGeneratePickList : DevExpress.XtraEditors.XtraForm
    {
        public frmGeneratePickList()
        {
            InitializeComponent();
        }

        private void frmGeneratePickList_Load(object sender, EventArgs e)
        {
            luCategory.Properties.DataSource = Utility.GetCategoryListExceptAll();
            luCategory.Properties.ValueMember = "CATEGORYID";
            luCategory.Properties.DisplayMember = "CATEGORYNAME";

            luLocationDivision.Properties.DataSource = new DataRepository().GetDataTable("USP_R_LOCATIONDIVISION", true);
            luLocationDivision.Properties.ValueMember = "LOCATIONDIVISIONID";
            luLocationDivision.Properties.DisplayMember = "LOCATIONDIVISIONNAME";
            luLocationDivision.CascadingOwner = luCategory;
            luLocationDivision.Properties.CascadingMember = "CATEGORYID";

            luSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            luSupplier.Properties.ValueMember = "DEALERID";
            luSupplier.Properties.DisplayMember = "DEALERNAME";
        }

        private void btnGeneratePickList_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            if (gvStockEntry.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("No items are selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string selectedInvoices = string.Join(",", gvStockEntry.GetSelectedRows().Select(x => gvStockEntry.GetRowCellValue(x, "STOCKENTRYID")));
            object returnValue;

            try
            {
                returnValue = new DataRepository().ExecuteScalarWithTransaction("USP_G_PICKLIST", true, new Dictionary<string, object>
                {
                    { "CategoryID", luCategory.EditValue },
                    { "SupplierID", luSupplier.EditValue },
                    { "LocationDivisionID", luLocationDivision.EditValue },
                    { "StockEntryIDs", selectedInvoices },
                    { "UserID", Utility.UserID }
                });
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                return;
            }

            if (int.TryParse(returnValue.ToString(), out int id))
            {
                XtraMessageBox.Show("Picklist generated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            XtraMessageBox.Show(returnValue.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void luSupplier_EditValueChanged(object sender, EventArgs e)
        {
            gcStockEntry.DataSource = null;
            if (luCategory.EditValue == null || luSupplier.EditValue == null) return;

            gcStockEntry.DataSource = new DataRepository().GetDataTable("USP_R_STOCKENTRY_FOR_PICKLIST_GENERATION", true
                , new Dictionary<string, object>
                {
                    { "CategoryID", luCategory.EditValue.ToString() },
                    { "SupplierID", luSupplier.EditValue.ToString() },
                });
        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    rptPickList pickList = new rptPickList(luSupplier.Text, luCategory.Text, (DataTable)gcPickList.DataSource);
        //    pickList.ShowRibbonPreview();
        //}
    }
}