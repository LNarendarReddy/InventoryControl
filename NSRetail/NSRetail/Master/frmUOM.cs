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
    public partial class frmUOM : DevExpress.XtraEditors.XtraForm
    {
        UOM ObjUOM = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmUOM(UOM _ObjUOM)
        {
            InitializeComponent();
            ObjUOM = _ObjUOM;
        }

        private void frmUOM_Load(object sender, EventArgs e)
        {
            cmbBaseUOM.Properties.DataSource = objMasterRep.GetUOM();
            cmbBaseUOM.Properties.DisplayMember = "DISPLAYVALUE";
            cmbBaseUOM.Properties.ValueMember = "UOMID";
            if (Convert.ToInt32(ObjUOM.UOMID) > 0)
            {
                this.Text = "Edit Counter";
                txtDisplayValue.EditValue = ObjUOM.DISPLAYVALUE;
                cmbBaseUOM.EditValue = ObjUOM.BASEUOMID;
                txtMultipler.EditValue = ObjUOM.MULTIPLIER;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjUOM.DISPLAYVALUE = txtDisplayValue.EditValue;
                ObjUOM.BASEUOMID = cmbBaseUOM.EditValue;
                ObjUOM.MULTIPLIER = cmbBaseUOM.EditValue;
                ObjUOM.UserID = Utility.UserID;
                objMasterRep.SaveUOM(ObjUOM);
                ObjUOM.IsSave = true;
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
            ObjUOM.IsSave = false;
            this.Close();
        }
    }
}