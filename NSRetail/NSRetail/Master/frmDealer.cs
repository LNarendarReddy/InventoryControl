using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Data;
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
                txtAddress.EditValue = ObjDealer. ADDRESS;
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
    }
}