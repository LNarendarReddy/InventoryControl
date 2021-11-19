using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmModeofPayment : DevExpress.XtraEditors.XtraForm
    {
        MOP ObjMOP = new MOP();
        MasterRepository ObjMasterRep = new MasterRepository();
        public frmModeofPayment()
        {
            InitializeComponent();
        }

        private void frmModeofPayment_Load(object sender, EventArgs e)
        {
            try
            {
                gcMOP.DataSource = ObjMasterRep.GetMOP();

            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvCategory_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, view.Columns["MOPID"], -1);
            }
            catch (Exception ex) { }
        }

        private void gvCategory_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DataRow row = (e.Row as DataRowView).Row;
                ObjMOP.MOPNAME = Convert.ToString(row["MOPNAME"]);
                ObjMOP.MOPID = Convert.ToInt32(row["MOPID"]);
                ObjMOP.UserID = Utility.UserID;
                ObjMasterRep.SaveMOP(ObjMOP);
                row["MOPID"] = ObjMOP.MOPID;
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
                if (Convert.ToString(dlgResult) == "OK" && gvMOP.FocusedRowHandle >= 0)
                {
                    ObjMOP = new MOP();
                    ObjMOP.MOPID = gvMOP.GetFocusedRowCellValue("MOPID");
                    ObjMOP.UserID = Utility.UserID;
                    ObjMasterRep.DeleteMOP(ObjMOP);
                    gvMOP.DeleteRow(gvMOP.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}