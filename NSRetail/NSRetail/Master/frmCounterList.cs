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
    public partial class frmCounterList : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        Counter ObjCounter = null;
        public frmCounterList()
        {
            InitializeComponent();
        }

        private void frmCounterList_Load(object sender, EventArgs e)
        {
            try
            {
                gcCounter.DataSource = objMasterRep.GetCounter();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ObjCounter = new Counter();
            frmCounter obj = new frmCounter(ObjCounter);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
            if (ObjCounter.IsSave)
            {
                gcCounter.DataSource = objMasterRep.GetCounter();
                Utility.Setfocus(gvCounter, "COUNTERID", ObjCounter.COUNTERID);
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
                if (gvCounter.FocusedRowHandle >= 0)
                {
                    ObjCounter = new Counter();
                    ObjCounter.COUNTERID = gvCounter.GetFocusedRowCellValue("COUNTERID");
                    ObjCounter.COUNTERNAME = gvCounter.GetFocusedRowCellValue("COUNTERNAME");
                    ObjCounter.BRANCHID = gvCounter.GetFocusedRowCellValue("BRANCHID");
                    frmCounter obj = new frmCounter(ObjCounter);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (ObjCounter.IsSave)
                    {
                        gcCounter.DataSource = objMasterRep.GetCounter();
                        Utility.Setfocus(gvCounter, "COUNTERID", ObjCounter.COUNTERID);
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
                if (Convert.ToString(dlgResult) == "OK" && gvCounter.FocusedRowHandle >= 0)
                {
                    ObjCounter = new Counter();
                    ObjCounter.COUNTERID = gvCounter.GetFocusedRowCellValue("COUNTERID");
                    ObjCounter.UserID = Utility.UserID;
                    objMasterRep.DeleteCounter(ObjCounter);
                    gvCounter.DeleteRow(gvCounter.FocusedRowHandle);
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