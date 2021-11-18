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
using DataAccess;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmBranchList : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        Branch ObjBranch = null;
        public frmBranchList()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ObjBranch = new Branch();
            frmBranch obj = new frmBranch(ObjBranch);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
            if (ObjBranch.IsSave)
            {
                gcBranch.DataSource = objMasterRep.GetBranch();
                Utility.Setfocus(gvBranch, "BRANCHID", ObjBranch.BRANCHID);
            }
        }

        private void frmBranch_Load(object sender, EventArgs e)
        {
            try
            {
                gcBranch.DataSource = objMasterRep.GetBranch();
            }
            catch (Exception ex){
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvBranch.FocusedRowHandle >= 0)
                {
                    ObjBranch = new Branch();
                    ObjBranch.BRANCHID = gvBranch.GetFocusedRowCellValue("BRANCHID");
                    ObjBranch.BRANCHNAME = gvBranch.GetFocusedRowCellValue("BRANCHNAME");
                    ObjBranch.BRANCHCODE = gvBranch.GetFocusedRowCellValue("BRANCHCODE");
                    ObjBranch.ADDRESS = gvBranch.GetFocusedRowCellValue("ADDRESS");
                    ObjBranch.Description = gvBranch.GetFocusedRowCellValue("DESCRIPTION");
                    ObjBranch.PHONENO = gvBranch.GetFocusedRowCellValue("PHONENO");
                    ObjBranch.EMAILID = gvBranch.GetFocusedRowCellValue("EMAILID");
                    frmBranch obj = new frmBranch(ObjBranch);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (ObjBranch.IsSave)
                    {
                        gcBranch.DataSource = objMasterRep.GetBranch();
                        Utility.Setfocus(gvBranch, "BRANCHID", ObjBranch.BRANCHID);
                    }

                }
            }
            catch (Exception ex){
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var dlgResult = XtraMessageBox.Show("Are you sure want to delete?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (Convert.ToString(dlgResult) == "OK" && gvBranch.FocusedRowHandle >= 0)
                {
                    ObjBranch = new Branch();
                    ObjBranch.BRANCHID = gvBranch.GetFocusedRowCellValue("BRANCHID");
                    ObjBranch.UserID = Utility.UserID;
                    objMasterRep.DeleteBranch(ObjBranch);
                    gvBranch.DeleteRow(gvBranch.FocusedRowHandle);
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