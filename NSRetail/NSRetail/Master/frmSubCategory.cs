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
    public partial class frmSubCategory : DevExpress.XtraEditors.XtraForm
    {
        SubCategory ObjSubCategory = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmSubCategory(SubCategory _ObjSubCategory)
        {
            InitializeComponent();
            ObjSubCategory = _ObjSubCategory;
        }

        private void frmSubCategory_Load(object sender, EventArgs e)
        {
            cmbCategory.Properties.DataSource = objMasterRep.GetCategory();
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            if (Convert.ToInt32(ObjSubCategory.SUBCATEGORYID) > 0)
            {
                this.Text = "Edit Sub Category";
                txtsubCategoryName.EditValue = ObjSubCategory.SUBCATEGORYNAME;
                cmbCategory.EditValue = ObjSubCategory.CATEGORYID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjSubCategory.SUBCATEGORYNAME = txtsubCategoryName.EditValue;
                ObjSubCategory. CATEGORYID = cmbCategory.EditValue;
                ObjSubCategory.UserID = Utility.UserID;
                objMasterRep.SaveSubCategory(ObjSubCategory);
                ObjSubCategory.IsSave = true;
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
            ObjSubCategory.IsSave = false;
            this.Close();
        }
    }
}