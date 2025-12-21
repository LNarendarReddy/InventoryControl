using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using NSRetail.Supplier;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.Master
{
    public partial class frmDealer : DevExpress.XtraEditors.XtraForm
    {
        Dealer ObjDealer = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmDealer(Dealer _ObjDealer, DataTable dataTable = null)
        {
            InitializeComponent();
            ObjDealer = _ObjDealer;
            gcSupplierItems.DataSource = dataTable;
            gvSupplierItems.BestFitColumns();
        }

        private void frmDealer_Load(object sender, EventArgs e)
        {

            cmbState.Properties.DataSource = objMasterRep.GetStates();
            cmbState.Properties.ValueMember = "STATEID";
            cmbState.Properties.DisplayMember = "STATENAME";
            if (Convert.ToInt32(ObjDealer.DEALERID) > 0)
            {
                this.Text = "Edit Supplier";
                txtDelearName.EditValue = ObjDealer.DEALERNAME;
                txtAddress.EditValue = ObjDealer.ADDRESS;
                txtShippingAddress.EditValue = ObjDealer.SHIPPINGADDRESS;
                txtPhoneNumber.EditValue = ObjDealer.PHONENO;
                txtEmail.EditValue = ObjDealer.EMAILID;
                txtGSTIN.EditValue = ObjDealer.GSTIN;
                txtPanNumber.EditValue = ObjDealer.PANNUMBER;
                txtVendorCode.EditValue = ObjDealer.VendorCode;
                cmbState.EditValue = ObjDealer.STATEID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjDealer.DEALERNAME = txtDelearName.EditValue;
                ObjDealer.ADDRESS = txtAddress.EditValue;
                ObjDealer.SHIPPINGADDRESS = txtShippingAddress.EditValue;
                ObjDealer.PHONENO = txtPhoneNumber.EditValue;
                ObjDealer.EMAILID = txtEmail.EditValue;
                ObjDealer.GSTIN = txtGSTIN.EditValue;
                ObjDealer.PANNUMBER = txtPanNumber.EditValue;
                ObjDealer.VendorCode = txtVendorCode.EditValue;
                ObjDealer.STATEID = cmbState.EditValue;
                ObjDealer.UserID = Utility.UserID;
                objMasterRep.SaveDealer(ObjDealer);
                ObjDealer.IsSave = true;
                this.Close();

            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjDealer.IsSave = false;
            this.Close();
        }

        private void btnMapSKU_Click(object sender, EventArgs e)
        {
            DataTable dt = new SupplierRepository().GetItemsForSupplierSKUMapping(ObjDealer.DEALERID);
            frmSupplierSKUMapping obj = new frmSupplierSKUMapping(dt);
            obj.RowSelected += OnPopupRowSelected;
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int rowHandle = gvSupplierItems.FocusedRowHandle;
            if (rowHandle < 0) return;

            var result = XtraMessageBox.Show("Are you sure you want to delete this item?","Confirm Delete",
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            if (!new SupplierRepository().DeleteSupplierItem(gvSupplierItems.GetRowCellValue(rowHandle, "ITEMSUPPLIERMAPID"), Utility.UserID))
            {
                XtraMessageBox.Show("This item cannot be deleted due to business rules.","Delete Failed",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            gvSupplierItems.DeleteRow(rowHandle);
            gvSupplierItems.RefreshData();
        }

        private void chkSameAsBillingAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameAsBillingAddress.Checked)
            {
                txtShippingAddress.EditValue = txtAddress.EditValue;
                txtShippingAddress.Enabled = false;
            }
            else
            {
                txtShippingAddress.Enabled = true;
            }
        }

        private void OnPopupRowSelected(SupplierSKUMapping selected, int popupRowHandle)
        {
            InsertRowFromPopup(selected, popupRowHandle);
        }

        private void InsertRowFromPopup(SupplierSKUMapping selected, int popupRowHandle)
        {
            if (selected == null || selected.ItemID <= 0)
                return;

            if (IsItemAlreadyMapped(selected.ItemID))
            {
                XtraMessageBox.Show(
                    "Item already mapped.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                var repo = new SupplierRepository();

                object id = repo.SupplierSKUMap(
                    ObjDealer.DEALERID,
                    selected.ItemID,
                    Utility.UserID);

                AddRowToParentGrid(selected, id);

                int handle = gvSupplierItems.RowCount - 1;
                if (handle >= 0)
                {
                    gvSupplierItems.FocusedRowHandle = handle;
                    gvSupplierItems.MakeRowVisible(handle);
                }

                var popup = Application.OpenForms
                    .OfType<frmSupplierSKUMapping>()
                    .FirstOrDefault();

                popup?.DeleteRow(popupRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private bool IsItemAlreadyMapped(int itemId)
        {
            var dt = gvSupplierItems.GridControl.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
                return false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                if (row.RowState == DataRowState.Deleted)
                    continue;

                if (Convert.ToInt32(row["ITEMID"]) == itemId)
                {
                    gvSupplierItems.FocusedRowHandle = gvSupplierItems.GetRowHandle(i);
                    gvSupplierItems.MakeRowVisible(gvSupplierItems.FocusedRowHandle);
                    return true;
                }
            }

            return false;
        }

        private void AddRowToParentGrid(SupplierSKUMapping selected, object supplierItemMapId)
        {
            var dt = gvSupplierItems.GridControl.DataSource as DataTable;
            DataRow dr = dt.NewRow();

            dr["ITEMID"] = selected.ItemID;
            dr["SKUCODE"] = selected.SKUCode;
            dr["ITEMNAME"] = selected.ItemName;
            dr["CATEGORYNAME"] = selected.CategoryName;
            dr["SUBCATEGORYNAME"] = selected.SubCategoryName;
            dr["BRANDNAME"] = selected.BrandName;
            dr["MANUFACTURERNAME"] = selected.ManufacturerName;
            dr["ITEMSUPPLIERMAPID"] = supplierItemMapId; // from SP

            dt.Rows.Add(dr);
        }
    }
}