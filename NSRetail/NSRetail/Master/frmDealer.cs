using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Master
{
    public partial class frmDealer : DevExpress.XtraEditors.XtraForm
    {
        Dealer ObjDealer = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmDealer(Dealer _ObjDealer)
        {
            InitializeComponent();
            ObjDealer = _ObjDealer;
        }

        private void frmDealer_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ObjDealer.DEALERID) > 0)
            {
                this.Text = "Edit Dealer";
                txtDelearName.EditValue = ObjDealer.DEALERNAME;
                txtAddress.EditValue = ObjDealer. ADDRESS;
                txtPhoneNumber.EditValue = ObjDealer.PHONENO;
                txtEmail.EditValue = ObjDealer.EMAILID;
                txtGSTIN.EditValue = ObjDealer.GSTIN;
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
    }
}