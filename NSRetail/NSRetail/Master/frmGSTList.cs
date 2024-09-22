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
    public partial class frmGSTList : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        GST ObjGST = null;
        public frmGSTList()
        {
            InitializeComponent();
        }

        private void frmGSTList_Load(object sender, EventArgs e)
        {
            try
            {
                AccessUtility.SetStatusByAccess(btnNew);
                AccessUtility.SetStatusByAccess(gcEdit, gcDelete);
                gcGST.DataSource = objMasterRep.GetGST();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ObjGST = new GST();
            frmGST obj = new frmGST(ObjGST);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
            if (ObjGST.IsSave)
            {
                gcGST.DataSource = objMasterRep.GetGST();
                Utility.Setfocus(gvGST, "GSTID", ObjGST.GSTID);
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
                if (gvGST.FocusedRowHandle >= 0)
                {
                    ObjGST = new GST();
                    ObjGST.GSTID = gvGST.GetFocusedRowCellValue("GSTID");
                    ObjGST.GSTCODE = gvGST.GetFocusedRowCellValue("GSTCODE");
                    ObjGST.CGST = gvGST.GetFocusedRowCellValue("CGST");
                    ObjGST.SGST = gvGST.GetFocusedRowCellValue("SGST");
                    ObjGST.IGST = gvGST.GetFocusedRowCellValue("IGST");
                    ObjGST.CESS = gvGST.GetFocusedRowCellValue("CESS");
                    frmGST obj = new frmGST(ObjGST);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (ObjGST.IsSave)
                    {
                        gcGST.DataSource = objMasterRep.GetGST();
                        Utility.Setfocus(gvGST, "GSTID", ObjGST.GSTID);
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
                if (Convert.ToString(dlgResult) == "OK" && gvGST.FocusedRowHandle >= 0)
                {
                    ObjGST = new GST();
                    ObjGST.GSTID = gvGST.GetFocusedRowCellValue("GSTID");
                    ObjGST.UserID = Utility.UserID;
                    objMasterRep.DeleteGST(ObjGST);
                    gvGST.DeleteRow(gvGST.FocusedRowHandle);
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
            gcGST.ShowRibbonPrintPreview();
        }
    }
}