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

namespace NSRetail
{
    public partial class frmCategory : DevExpress.XtraEditors.XtraForm
    {
        Category ObjCategory = new Category();
        MasterRepository ObjMasterRep = new MasterRepository();
        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            try
            {
                 gcCategory.DataSource = ObjMasterRep.GetCategory();

            }
            catch (Exception ex) {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                ObjCategory.UserID = Utility.UserID;
                ObjMasterRep.SaveCategory(ObjCategory);
                row["CATEGORYID"] = ObjCategory.CATEGORYID;
            }
            catch (Exception ex) {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}