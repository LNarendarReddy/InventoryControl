using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
                gcDealer.DataSource = objMasterRep.GetDealerForMeta();

                AccessUtility.SetStatusByAccess(btnNew);
                AccessUtility.SetStatusByAccess(gcEdit, gcDelete);
                gvDealer.Columns["STATUS"].FilterInfo = new ColumnFilterInfo("STATUS = 'ACTIVE'");
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
                    ObjDealer.SHIPPINGADDRESS = gvDealer.GetFocusedRowCellValue("SHIPPINGADDRESS");
                    ObjDealer.STATEID = gvDealer.GetFocusedRowCellValue("STATEID");
                    ObjDealer.PHONENO = gvDealer.GetFocusedRowCellValue("PHONENO");
                    ObjDealer.EMAILID = gvDealer.GetFocusedRowCellValue("EMAILID");
                    ObjDealer.GSTIN = gvDealer.GetFocusedRowCellValue("GSTIN");
                    ObjDealer.PANNUMBER = gvDealer.GetFocusedRowCellValue("PANNUMBER");
                    ObjDealer.VendorCode = gvDealer.GetFocusedRowCellValue("VENDORCODE");
                    DataTable dt = new SupplierRepository().GetSupplierItems(ObjDealer.DEALERID);
                    frmDealer obj = new frmDealer(ObjDealer, dt);
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

        private void gvDealer_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                if (gvDealer.GetFocusedRowCellValue("STATUS").Equals("In-Active"))
                    e.Menu.Items.Add(new DXMenuItem("UnDelete", new EventHandler(On_Click)));

            }
        }

        void On_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvDealer.FocusedRowHandle >= 0)
                {
                    object dealerID = gvDealer.GetFocusedRowCellValue("DEALERID");
                    new MasterRepository().UnDeleteDealer(dealerID, Utility.UserID);
                    XtraMessageBox.Show("Dealer undeleted successfully");
                    gcDealer.DataSource = objMasterRep.GetDealer();
                    gvDealer.BestFitColumns();
                    
                    int rowHandle = gvDealer.LocateByValue("DEALERID", dealerID);
                    if (rowHandle >= 0)
                    {
                        gvDealer.FocusedRowHandle = rowHandle;
                    }
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