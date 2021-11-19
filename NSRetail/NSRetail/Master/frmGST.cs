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
    public partial class frmGST : DevExpress.XtraEditors.XtraForm
    {
        GST ObjGST = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmGST(GST _ObjGST)
        {
            InitializeComponent();
            ObjGST = _ObjGST;
        }

        private void frmGST_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ObjGST.GSTID) > 0)
            {
                this.Text = "Edit GST";
                txtGSTCode.EditValue = ObjGST.GSTCODE;
                txtCGST.EditValue = ObjGST.CGST;
                txtSGST.EditValue = ObjGST.SGST;
                txtIGST.EditValue = ObjGST.IGST;
                txtCESS.EditValue = ObjGST.CESS;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjGST. GSTCODE = txtGSTCode.EditValue;
                ObjGST.CGST = txtCGST.EditValue;
                ObjGST.SGST= txtSGST.EditValue;
                ObjGST.IGST= txtIGST.EditValue;
                ObjGST.CESS = txtCESS.EditValue;
                ObjGST.UserID = Utility.UserID;
                objMasterRep.SaveGST(ObjGST);
                ObjGST.IsSave = true;
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
            ObjGST.IsSave = false;
            this.Close();
        }
    }
}