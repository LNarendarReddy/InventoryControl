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
    public partial class frmDealerList : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        Dealer ObjDealer = null;
        public frmDealerList()
        {
            InitializeComponent();
        }

        private void frmDealerList_Load(object sender, EventArgs e)
        {
            try
            {
                cmbState.DataSource = objMasterRep.GetStates();
                cmbState.DisplayMember = "STATENAME";
                cmbState.ValueMember = "STATEID";
                gcDealer.DataSource = objMasterRep.GetDealer();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ObjDealer = new Dealer();
            frmDealer obj = new frmDealer(ObjDealer);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
            if (ObjDealer.IsSave)
            {
                gcDealer.DataSource = objMasterRep.GetDealer();
                Utility.Setfocus(gvDealer, "DEALERID", ObjDealer.DEALERID);
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
                if (gvDealer.FocusedRowHandle >= 0)
                {
                    ObjDealer = new Dealer();
                    ObjDealer.DEALERID = gvDealer.GetFocusedRowCellValue("DEALERID");
                    ObjDealer.DEALERNAME = gvDealer.GetFocusedRowCellValue("DEALERNAME");
                    ObjDealer.ADDRESS = gvDealer.GetFocusedRowCellValue("ADDRESS");
                    ObjDealer.STATEID = gvDealer.GetFocusedRowCellValue("STATEID");
                    ObjDealer.PHONENO = gvDealer.GetFocusedRowCellValue("PHONENO");
                    ObjDealer.EMAILID = gvDealer.GetFocusedRowCellValue("EMAILID");
                    ObjDealer.GSTIN = gvDealer.GetFocusedRowCellValue("GSTIN");
                    ObjDealer.PANNUMBER = gvDealer.GetFocusedRowCellValue("PANNUMBER");
                    frmDealer obj = new frmDealer(ObjDealer);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (ObjDealer.IsSave)
                    {
                        gcDealer.DataSource = objMasterRep.GetDealer();
                        Utility.Setfocus(gvDealer, "DEALERID", ObjDealer.DEALERID);
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
                if (Convert.ToString(dlgResult) == "OK" && gvDealer.FocusedRowHandle >= 0)
                {
                    ObjDealer = new Dealer();
                    ObjDealer.DEALERID = gvDealer.GetFocusedRowCellValue("DEALERID");
                    ObjDealer.UserID = Utility.UserID;
                    objMasterRep.DeleteDealer(ObjDealer);
                    gvDealer.DeleteRow(gvDealer.FocusedRowHandle);
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
            gcDealer.ShowRibbonPrintPreview();
        }
    }
}