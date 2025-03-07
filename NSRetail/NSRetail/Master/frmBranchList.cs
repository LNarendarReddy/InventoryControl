﻿using System;
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
using DevExpress.Utils.Menu;
using NSRetail.Utilities;

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
                cmbState.DataSource = objMasterRep.GetStates();
                cmbState.DisplayMember = "STATENAME";
                cmbState.ValueMember = "STATEID";
                gcBranch.DataSource = objMasterRep.GetBranch();
                gvBranch.BestFitColumns();

                AccessUtility.SetStatusByAccess(btnNew);
                AccessUtility.SetStatusByAccess(gcEdit, gcDelete);
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
                    ObjBranch.STATEID = gvBranch.GetFocusedRowCellValue("STATEID");
                    ObjBranch.PHONENO = gvBranch.GetFocusedRowCellValue("PHONENO");
                    ObjBranch.EMAILID = gvBranch.GetFocusedRowCellValue("EMAILID");
                    ObjBranch.ISWAREHOUSE = gvBranch.GetFocusedRowCellValue("ISWAREHOUSE");
                    ObjBranch.LANDLINE = gvBranch.GetFocusedRowCellValue("LANDLINE");
                    ObjBranch.SUPERVISERID = gvBranch.GetFocusedRowCellValue("SUPERVISORID");
                    ObjBranch.ENABLEDRAFTBILLS = gvBranch.GetFocusedRowCellValue("ENABLEDRAFTBILLS");
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

        private void gvBranch_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                e.Menu.Items.Add(new DXMenuItem("Initiate Stock Counting", new EventHandler(On_Click)));
                string menu = "Enable Stock Dispatch";
                if (gvBranch.GetFocusedRowCellValue("StockDispatchEnabled").Equals(true))
                    menu = "Disable Stock Dispatch";
                e.Menu.Items.Add(new DXMenuItem(menu, new EventHandler(OnDispatch_Click)));

            }
        }

        void On_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvBranch.FocusedRowHandle >= 0)
                {
                    new MasterRepository().InitiateStockCounting(gvBranch.GetFocusedRowCellValue("BRANCHID"),Utility.UserID);
                    new CloudRepository().InitiateCounting(gvBranch.GetFocusedRowCellValue("BRANCHID"), true);
                    XtraMessageBox.Show("Old sheets are archived, please wait for 10 mins and continue with stock counting");
                    gcBranch.DataSource = objMasterRep.GetBranch();
                    gvBranch.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        void OnDispatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvBranch.FocusedRowHandle >= 0)
                {
                    bool stats = Convert.ToBoolean(gvBranch.GetFocusedRowCellValue("StockDispatchEnabled"));
                    new MasterRepository().UpdateStockDispatchStatus(gvBranch.GetFocusedRowCellValue("BRANCHID"),Utility.UserID,!stats);
                    XtraMessageBox.Show("Stock dispatch status updated");
                    gvBranch.SetFocusedRowCellValue("StockDispatchEnabled", !stats);
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
            gcBranch.ShowRibbonPrintPreview();
        }

        private void gcBranch_Click(object sender, EventArgs e)
        {
           
        }
    }
}