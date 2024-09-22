using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entity;
using DataAccess;
using ErrorManagement;
using DevExpress.XtraGrid.Views.Grid;
using NSRetail.Utilities;

namespace NSRetail
{
    public partial class frmCategoryList : DevExpress.XtraEditors.XtraForm
    {
        Category ObjCategory = new Category();
        MasterRepository ObjMasterRep = new MasterRepository();
        public frmCategoryList()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            try
            {
                gcCategory.DataSource = ObjMasterRep.GetCategory();

                string accessIdentifier = "D81C10D9-FCD1-4E51-8035-20E2150D78A8";
                
                gvCategory.Tag = $"{accessIdentifier}::Create";
                AccessUtility.SetStatusByAccess(gvCategory);

                gvCategory.Tag = $"{accessIdentifier}::Update";
                AccessUtility.SetStatusByAccess(gvCategory);
                
                AccessUtility.SetStatusByAccess(gcDelete);
            }
            catch (Exception ex) {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvCategory_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, view.Columns["CATEGORYID"], -1);
            }
            catch (Exception ex) { }
        }

        private void gvCategory_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DataRow row = (e.Row as DataRowView).Row;
                ObjCategory.CATEGORYNAME = Convert.ToString(row["CATEGORYNAME"]);
                ObjCategory.CATEGORYID = Convert.ToInt32(row["CATEGORYID"]);
                ObjCategory.AllowOpenItems = Convert.ToBoolean(row["ALLOWOPENITEMS"]);
                ObjCategory.UserID = Utility.UserID;
                ObjMasterRep.SaveCategory(ObjCategory);
                row["CATEGORYID"] = ObjCategory.CATEGORYID;
            }
            catch (Exception ex) {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var dlgResult = XtraMessageBox.Show("Are you sure want to delete?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (Convert.ToString(dlgResult) == "OK" && gvCategory.FocusedRowHandle >= 0)
                {
                    ObjCategory = new Category();
                    ObjCategory.CATEGORYID = gvCategory.GetFocusedRowCellValue("CATEGORYID");
                    ObjCategory.UserID = Utility.UserID;
                    ObjMasterRep.DeleteCategory(ObjCategory);
                    gvCategory.DeleteRow(gvCategory.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcCategory.ShowRibbonPrintPreview();
        }
    }
}