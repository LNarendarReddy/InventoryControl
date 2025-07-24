using DataAccess;
using Entity;
using ErrorManagement;
using System;

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
                chkInstantDispatchEnabled.EditValue = ObjSubCategory.InstantDispatchEnabled;
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
                ObjSubCategory.InstantDispatchEnabled = chkInstantDispatchEnabled.EditValue;
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