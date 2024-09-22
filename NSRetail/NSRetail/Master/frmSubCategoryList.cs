using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using NSRetail.Utilities;
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
    public partial class frmSubCategoryList : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        SubCategory ObjSubCategory = null;
        public frmSubCategoryList()
        {
            InitializeComponent();
        }

        private void frmSubCategoryList_Load(object sender, EventArgs e)
        {
            try
            {
                gcSubCategory.DataSource = objMasterRep.GetSubCategory();

                AccessUtility.SetStatusByAccess(btnNew);
                AccessUtility.SetStatusByAccess(gcEdit, gcDelete);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ObjSubCategory = new SubCategory();
            frmSubCategory obj = new frmSubCategory(ObjSubCategory);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
            if (ObjSubCategory.IsSave)
            {
                gcSubCategory.DataSource = objMasterRep.GetSubCategory();
                Utility.Setfocus(gvSubCategory, "SUBCATEGORYID", ObjSubCategory.SUBCATEGORYID);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvSubCategory.FocusedRowHandle >= 0)
                {
                    ObjSubCategory = new SubCategory();
                    ObjSubCategory.SUBCATEGORYID = gvSubCategory.GetFocusedRowCellValue("SUBCATEGORYID");
                    ObjSubCategory.SUBCATEGORYNAME = gvSubCategory.GetFocusedRowCellValue("SUBCATEGORYNAME");
                    ObjSubCategory.CATEGORYID = gvSubCategory.GetFocusedRowCellValue("CATEGORYID");
                    frmSubCategory obj = new frmSubCategory(ObjSubCategory);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (ObjSubCategory.IsSave)
                    {
                        gcSubCategory.DataSource = objMasterRep.GetSubCategory();
                        Utility.Setfocus(gvSubCategory, "SUBCATEGORYID", ObjSubCategory.SUBCATEGORYID);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var dlgResult = XtraMessageBox.Show("Are you sure want to delete?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (Convert.ToString(dlgResult) == "OK" && gvSubCategory.FocusedRowHandle >= 0)
                {
                    ObjSubCategory = new SubCategory();
                    ObjSubCategory.SUBCATEGORYID = gvSubCategory.GetFocusedRowCellValue("SUBCATEGORYID");
                    ObjSubCategory.UserID = Utility.UserID;
                    objMasterRep.DeleteSubCategory(ObjSubCategory);
                    gvSubCategory.DeleteRow(gvSubCategory.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcSubCategory.ShowRibbonPrintPreview();
        }
    }
}