using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmManufacturer : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        public frmManufacturer()
        {
            InitializeComponent();
        }

        private void frmManufacturer_Load(object sender, EventArgs e)
        {
            try
            {
                gcManufacturer.DataSource = ObjMasterRep.GetManufacturer();

                //string accessIdentifier = "D81C10D9-FCD1-4E51-8035-20E2150D78A8";

                //gvManufacturer.Tag = $"{accessIdentifier}::Create";
                //AccessUtility.SetStatusByAccess(gvManufacturer);

                //gvManufacturer.Tag = $"{accessIdentifier}::Update";
                //AccessUtility.SetStatusByAccess(gvManufacturer);

                //AccessUtility.SetStatusByAccess(gcDelete);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvManufacturer_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, view.Columns["MANUFACTURERID"], -1);
            }
            catch (Exception ex) { }
        }

        private void gvManufacturer_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DataRow row = (e.Row as DataRowView).Row;
                object rtn = ObjMasterRep.SaveManufacturer(Convert.ToInt32(row["MANUFACTURERID"]), 
                    Convert.ToString(row["MANUFACTURERNAME"]), 
                    Utility.UserID);
                row["MANUFACTURERID"] = rtn;
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
                if (Convert.ToString(dlgResult) == "OK" && gvManufacturer.FocusedRowHandle >= 0)
                {
                    ObjMasterRep.DeleteManufacturer(gvManufacturer.GetFocusedRowCellValue("MANUFACTURERID"), Utility.UserID);
                    gvManufacturer.DeleteRow(gvManufacturer.FocusedRowHandle);
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
            gcManufacturer.ShowRibbonPrintPreview();
        }
    }
}