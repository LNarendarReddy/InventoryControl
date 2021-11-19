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
    public partial class frmUOMList : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        UOM ObjUOM = null;
        public frmUOMList()
        {
            InitializeComponent();
        }

        private void frmUOMList_Load(object sender, EventArgs e)
        {
            try
            {
                gcUOM.DataSource = objMasterRep.GetUOM();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ObjUOM = new UOM();
            frmUOM obj = new frmUOM(ObjUOM);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
            if (ObjUOM.IsSave)
            {
                gcUOM.DataSource = objMasterRep.GetUOM();
                Utility.Setfocus(gvUOM, "UOMID", ObjUOM.UOMID);
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
                if (gvUOM.FocusedRowHandle >= 0)
                {
                    ObjUOM = new  UOM();
                    ObjUOM.UOMID = gvUOM.GetFocusedRowCellValue("UOMID");
                    ObjUOM.DISPLAYVALUE = gvUOM.GetFocusedRowCellValue("DISPLAYVALUE");
                    ObjUOM.MULTIPLIER = gvUOM.GetFocusedRowCellValue("MULTIPLIER");
                    ObjUOM.BASEUOMID = gvUOM.GetFocusedRowCellValue("BASEUOMID");
                    frmUOM obj = new frmUOM(ObjUOM);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (ObjUOM.IsSave)
                    {
                        gcUOM.DataSource = objMasterRep.GetUOM();
                        Utility.Setfocus(gvUOM, "UOMID", ObjUOM.UOMID);
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
                if (Convert.ToString(dlgResult) == "OK" && gvUOM.FocusedRowHandle >= 0)
                {
                    ObjUOM = new UOM();
                    ObjUOM.UOMID = gvUOM.GetFocusedRowCellValue("UOMID");
                    ObjUOM.UserID = Utility.UserID;
                    objMasterRep.DeleteUOM(ObjUOM);
                    gvUOM.DeleteRow(gvUOM.FocusedRowHandle);
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