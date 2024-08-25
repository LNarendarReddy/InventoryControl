using System;
using System.Data;
using System.Windows.Forms;
using ErrorManagement;
using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using static DevExpress.Utils.Diagnostics.GUIResources;
using Entity;
using DevExpress.Utils.Gesture;

namespace NSRetail
{
    public partial class frmMRPList : XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;
        private object _ItemCodeID = null;
        private bool _IsItemListCall = false;

        public frmMRPList(DataTable _dtMRP,object ItemCodeID,
            bool IsItemListCall = false, bool showCostPrice = false,int parentID = 0)
        {
            InitializeComponent();

            _IsItemListCall= IsItemListCall;
            _ItemCodeID = ItemCodeID;

            cmbGST.DataSource = Utility.GetGSTInfoList();
            cmbGST.ValueMember = "GSTID";
            cmbGST.DisplayMember = "GSTCODE";
            gcMRPList.DataSource = _dtMRP;

            gcCostPriceWT.Visible = showCostPrice;
            gcCostPriceWOT.Visible = showCostPrice;
            gcEdit.Visible = IsItemListCall && (Utility.Role == "Admin" || Utility.Role == "IT Manager");
            gcDelete.Visible = IsItemListCall && Utility.Role == "Admin";

            //if (IsItemListCall && Utility.Role == "Admin")
            //{
            //    gcDelete.Visible = true;
            //    gcDelete.VisibleIndex = 7;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _IsSave = true;
                drSelected = gvMRPList.GetFocusedRow();
                if (!_IsItemListCall)
                    this.Close();
            }
            catch (Exception){}
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvMRPList.FocusedRowHandle < 0)
                return;
            try
            {
                if (XtraMessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                if (int.TryParse(Convert.ToString(gvMRPList.GetFocusedRowCellValue("ITEMPRICEID")), out int val) && val > 0)
                {
                    new ItemCodeRepository().DeleteItemPrice(gvMRPList.GetFocusedRowCellValue("ITEMPRICEID"), Utility.UserID);
                    gvMRPList.DeleteRow(gvMRPList.FocusedRowHandle);
                }
                else
                {
                    gvMRPList.DeleteRow(gvMRPList.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        //decimal OldMRP = 0;
        //decimal OldSP = 0;
        //int OldGSTID = 0;
        //private void gvMRPList_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        //{
        //    if (gvMRPList.FocusedRowHandle < 0 || !_IsItemListCall)
        //        return;

        //    decimal.TryParse(Convert.ToString(gvMRPList.GetFocusedRowCellValue("MRP")), out decimal MRP);
        //    decimal.TryParse(Convert.ToString(gvMRPList.GetFocusedRowCellValue("SALEPRICE")), out decimal SALEPRICE);
        //    int.TryParse(Convert.ToString(gvMRPList.GetFocusedRowCellValue("GSTID")), out int GSTID);

        //    if (OldMRP == MRP && OldSP == SALEPRICE && OldGSTID == GSTID)
        //        return;

        //    IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
        //    try
        //    {
        //        if (MRP < SALEPRICE)
        //            throw new Exception("Saleprice cannot be greater than MRP");

        //        int NewItempriceID = new ItemCodeRepository().SaveItemPrice(
        //            _ItemCodeID,
        //            gvMRPList.GetFocusedRowCellValue("ITEMPRICEID"),
        //            MRP,
        //            SALEPRICE,
        //            GSTID,
        //            Utility.UserID);

        //        OldMRP = MRP;
        //        OldSP = SALEPRICE;
        //        OldGSTID  = GSTID;

        //        gvMRPList.SetFocusedRowCellValue("ITEMPRICEID", NewItempriceID);
        //        SplashScreenManager.CloseOverlayForm(handle);
        //    }
        //    catch (Exception ex)
        //    {
        //        SplashScreenManager.CloseOverlayForm(handle);
        //        ErrorMgmt.ShowError(ex);
        //        if (int.TryParse(Convert.ToString(gvMRPList.GetFocusedRowCellValue("ITEMPRICEID")), out int val) && val <= 0)
        //        {
        //            gvMRPList.DeleteRow(gvMRPList.FocusedRowHandle);
        //        }
        //        ErrorMgmt.Errorlog.Error(ex);
        //    }
        //}

        //private void gvMRPList_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    try
        //    {
        //        GridView view = sender as GridView;
        //        view.SetRowCellValue(e.RowHandle, view.Columns["ITEMPRICEID"], -1);
        //    }
        //    catch (Exception ex) { }
        //}

        //private void gvMRPList_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (!_IsItemListCall)
        //        e.Cancel = true;
        //    else if (int.TryParse(Convert.ToString(gvMRPList.GetFocusedRowCellValue("ITEMPRICEID")), out int val) && val > 0)
        //    {
        //        if (gvMRPList.FocusedColumn == gcMRP)
        //            e.Cancel = true;
        //    }
        //}

        //private void gvMRPList_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        //{
        //    if (!_IsItemListCall) return;

        //    GridView view = sender as GridView;
        //    if (view.GetRowCellValue(e.RowHandle, gcMRP) == DBNull.Value)
        //    {
        //        e.Valid = false;
        //        view.SetColumnError(gcMRP, "MRP is mandatory");
        //    }

        //    if (view.GetRowCellValue(e.RowHandle, gcSalePrice) == DBNull.Value)
        //    {
        //        e.Valid = false;
        //        view.SetColumnError(gcSalePrice, "Saleprice is mandatory");
        //    }

        //    if (view.GetRowCellValue(e.RowHandle, gcGSTCode) == DBNull.Value)
        //    {
        //        e.Valid = false;
        //        view.SetColumnError(gcGSTCode, "GST Code is Mandatory");
        //    }
        //    if (e.Valid)
        //        view.ClearColumnErrors();
        //}

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ItemMRP itemMRP = new ItemMRP() 
                {
                    ITEMCODEID = _ItemCodeID
                    , ITEMPRICEID = gvMRPList.GetFocusedRowCellValue("ITEMPRICEID")
                    , MRP = gvMRPList.GetFocusedRowCellValue("MRP")
                    , SalePrice = gvMRPList.GetFocusedRowCellValue("SALEPRICE")
                    , GSTID = gvMRPList.GetFocusedRowCellValue("GSTID")
                    , Immediate = gvMRPList.GetFocusedRowCellValue("IMMEDIATE")
                    , GoLiveDateTime = gvMRPList.GetFocusedRowCellValue("GOLIVEDATE")
                    , MoveStatus = gvMRPList.GetFocusedRowCellValue("GOLIVEDATE")
                    , UserID = Utility.UserID
                };

            frmMRP frmMRP = new frmMRP(itemMRP);
            frmMRP.ShowDialog();
            if (frmMRP.ItemMRPObj.IsSave)
                LoadItemPrices(itemMRP);
        }

        private void btnAddMRP_Click(object sender, EventArgs e)
        {
            ItemMRP itemMRP = new ItemMRP()
            {
                ITEMCODEID = _ItemCodeID
                , Immediate = true
                , UserID = Utility.UserID
                , MoveStatus = false
            };

            frmMRP frmMRP = new frmMRP(itemMRP);
            frmMRP.ShowDialog();
            if (frmMRP.ItemMRPObj.IsSave)
                LoadItemPrices(itemMRP);
        }

        private void LoadItemPrices(ItemMRP itemMRP)
        {            
            gcMRPList.DataSource = new ItemCodeRepository().GetMRPList(_ItemCodeID);
        }

        private void frmMRPList_Load(object sender, EventArgs e)
        {

        }
    }
}