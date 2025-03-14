﻿using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid.Data;
using DevExpress.XtraReports.UI;
using Entity;
using ErrorManagement;
using NSRetail.Reports;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockDispatch : XtraForm, IBarcodeReceiver
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        ItemCodeRepository ObjItemRep = new ItemCodeRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockDispatch ObjStockDispatch = null;
        StockDispatchDetail ObjStockDispatchDetail = null;
        object ItemPriceID = null;
        bool IsParentExist = false;
        bool IsOpenItem = false;
        bool IsEdit = false;
        bool isEventCall = false;
        public frmStockDispatch()
        {
            InitializeComponent();
        }

        private void frmStockDispatch_Load(object sender, EventArgs e)
        {
            try
            {
                ((frmMain)MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;

                txtQuantity.ConfirmBarCodeScan();
                txtWeightInKgs.ConfirmBarCodeScan();

                DataTable dtBranch = Utility.GetBranchList();

                DataView gvWarehouse = dtBranch.Copy().DefaultView;
                gvWarehouse.RowFilter = $"BRANCHID = {Utility.BranchID}";
                cmbFromBranch.Properties.DataSource = gvWarehouse;
                cmbFromBranch.Properties.ValueMember = "BRANCHID";
                cmbFromBranch.Properties.DisplayMember = "BRANCHNAME";
                cmbFromBranch.EditValue = gvWarehouse.ToTable().Rows[0]["BRANCHID"];


                DataView dvBranch = dtBranch.Copy().DefaultView;
                dvBranch.RowFilter = "ISWAREHOUSE = 0";
                cmbToBranch.Properties.DataSource = dvBranch;
                cmbToBranch.Properties.ValueMember = "BRANCHID";
                cmbToBranch.Properties.DisplayMember = "BRANCHNAME";
                cmbToBranch.EditValue = dvBranch.ToTable().Rows.Count == 1 ? dvBranch.ToTable().Rows[0]["BRANCHID"] : null;

                cmbCategory.Properties.DataSource = Utility.GetCategoryList();
                cmbCategory.Properties.ValueMember = "CATEGORYID";
                cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

                if (ObjStockDispatch == null)
                    ObjStockDispatch = new StockDispatch();
                ObjStockDispatch.UserID = Utility.UserID;

                ObjStockRep.GetDispatchDraft(ObjStockDispatch);
                if (Convert.ToInt32(ObjStockDispatch.STOCKDISPATCHID) > 0)
                {
                    cmbFromBranch.EditValue = ObjStockDispatch.FROMBRANCHID;
                    cmbToBranch.EditValue = ObjStockDispatch.TOBRANCHID;
                    cmbCategory.EditValue = ObjStockDispatch.CATEGORYID;
                    gcDispatch.DataSource = ObjStockDispatch.dtDispatch;
                    cmbFromBranch.Enabled = false;
                    cmbCategory.Enabled = false;
                }
                else
                {
                    cmbCategory.EditValue = ObjStockDispatch.CATEGORYID = Utility.CategoryID;
                    if (cmbCategory.Text != "ALL")
                        cmbCategory.Enabled = false;
                    ObjStockDispatch.dtDispatch = new DataTable();
                    ObjStockDispatch.dtDispatch.Columns.Add("STOCKDISPATCHDETAILID",typeof(int));
                    ObjStockDispatch.dtDispatch.Columns.Add("ITEMID", typeof(int));
                    ObjStockDispatch.dtDispatch.Columns.Add("ITEMCODEID", typeof(int));
                    ObjStockDispatch.dtDispatch.Columns.Add("ITEMPRICEID", typeof(int));
                    ObjStockDispatch.dtDispatch.Columns.Add("SKUCODE", typeof(string));
                    ObjStockDispatch.dtDispatch.Columns.Add("ITEMCODE", typeof(string));
                    ObjStockDispatch.dtDispatch.Columns.Add("ITEMNAME", typeof(string));
                    ObjStockDispatch.dtDispatch.Columns.Add("MRP", typeof(decimal));
                    ObjStockDispatch.dtDispatch.Columns.Add("SALEPRICE", typeof(decimal));
                    ObjStockDispatch.dtDispatch.Columns.Add("DISPATCHQUANTITY", typeof(int));
                    ObjStockDispatch.dtDispatch.Columns.Add("WEIGHTINKGS", typeof(decimal));
                    ObjStockDispatch.dtDispatch.Columns.Add("TRAYNUMBER", typeof(int));
                    gcDispatch.DataSource = ObjStockDispatch.dtDispatch;
                }
            }
            catch (Exception) { }
        }

        private void FrmStockDispatch_RefreshBaseLineData(object sender, EventArgs e)
        {
            cmbCategory_EditValueChanged(sender, e);
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            try
            {
                int iValue = 0;
                if (int.TryParse(Convert.ToString(ObjStockDispatch.STOCKDISPATCHID), out iValue) && iValue > 0)
                {
                    if (XtraMessageBox.Show($"Are you sure want to submit dispatch from {cmbFromBranch.Text} to {cmbToBranch.Text} ?", "Confirm",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;

                    if (!dxValidationProvider1.Validate() || 
                        XtraMessageBox.Show("Are you sure want to save dispatch?","Confirm",
                        MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                    ObjStockDispatch.TOBRANCHID = cmbToBranch.EditValue;
                    ObjStockRep.UpdateDispatch(ObjStockDispatch);
                    DataSet ds = ObjStockRep.GetDispatch(ObjStockDispatch.STOCKDISPATCHID);

                    if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count <= 0)
                    {
                        XtraMessageBox.Show("No data returned from database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    rptDispatch rpt = new rptDispatch(ds.Tables[0], ds.Tables[1]);
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowRibbonPreview();
                    cmbFromBranch.EditValue = null;
                    cmbToBranch.EditValue = null;
                    txtTrayNumber.EditValue = null;
                    cmbFromBranch.Enabled = true;
                    if (Utility.Category == "ALL")
                        cmbCategory.Enabled = true;
                    cmbCategory.EditValue = Utility.CategoryID;
                    ObjStockDispatch.STOCKDISPATCHID = 0;
                    ObjStockDispatch.dtDispatch = ObjStockDispatch.dtDispatch.Clone();
                    gcDispatch.DataSource = ObjStockDispatch.dtDispatch;
                    cmbFromBranch.Focus();
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

        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbItemCode.EditValue != null)
                {
                    
                    int rowhandle = cmbLookupView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
                    txtItemName.EditValue = cmbLookupView.GetRowCellValue(rowhandle,"ITEMNAME");
                    DataTable dtMRPList = ObjItemRep.GetMRPList(cmbItemCode.EditValue);
                    if (dtMRPList.Rows.Count > 1)
                    {
                        frmMRPList obj = new frmMRPList(dtMRPList, cmbItemCode.EditValue);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {

                            txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                            ItemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                        }
                    }
                    else
                    {
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                    }

                    int ParentID = 0;
                    IsParentExist = int.TryParse(Convert.ToString(cmbLookupView.GetRowCellValue(rowhandle,"PARENTITEMID")), out ParentID)
                        && ParentID > 0;
                    IsOpenItem = bool.TryParse(Convert.ToString(cmbLookupView.GetRowCellValue(rowhandle,"ISOPENITEM")), out IsOpenItem) 
                        && IsOpenItem;

                    txtQuantity.Enabled = !IsOpenItem;
                    txtWeightInKgs.Enabled = IsOpenItem;

                    DataTable dt = ObjStockRep.GetCurrentStock(cmbFromBranch.EditValue, cmbToBranch.EditValue,
                            cmbItemCode.EditValue, ParentID);   
                    DataRow drStockValue = dt?.Rows[0];
                    txtWarehouseStock.EditValue = drStockValue?[0];
                    txtBranchStock.EditValue = drStockValue?[1];

                    txtWeightInKgs.EditValue = 0.00;    
                    txtQuantity.EditValue = 1;

                    SendKeys.Send("{ENTER}");
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter || !dxValidationProvider2.Validate() ||
                txtQuantity.EditValue == null || Convert.ToInt32(txtQuantity.EditValue) <= 0) return;
                        
            try
            {
                if (Convert.ToInt32(ObjStockDispatch?.STOCKDISPATCHID) == 0 ||
                    Convert.ToInt32(cmbToBranch.EditValue) !=  Convert.ToInt32(ObjStockDispatch.TOBRANCHID)) 
                    SaveDispatch();

                // in case the save dispatch failed above
                if (Convert.ToInt32(ObjStockDispatch?.STOCKDISPATCHID) == 0) return;

                ObjStockDispatchDetail = new StockDispatchDetail();
                ObjStockDispatchDetail.STOCKDISPATCHDETAILID = 0;
                ObjStockDispatchDetail.STOCKDISPATCHID = ObjStockDispatch.STOCKDISPATCHID;
                ObjStockDispatchDetail.ITEMPRICEID = ItemPriceID;
                ObjStockDispatchDetail.TRAYNUMBER = txtTrayNumber.EditValue;
                ObjStockDispatchDetail.DISPATCHQUANTITY = txtQuantity.EditValue;
                ObjStockDispatchDetail.WEIGHTINKGS = txtWeightInKgs.EditValue;
                ObjStockDispatchDetail.UserID = Utility.UserID;
                ObjStockRep.SaveDispatchDetail(ObjStockDispatchDetail);
                isEventCall = true;
                RefreshGrid();
                isEventCall = false;
                ObjStockDispatchDetail.STOCKDISPATCHDETAILID = 0;
                cmbItemCode.EditValue = null;
                txtItemName.EditValue = null;
                txtMRP.EditValue = null;
                txtSalePrice.EditValue = null;
                txtQuantity.EditValue = null;
                txtWeightInKgs.EditValue = null;
                cmbItemCode.Focus();

            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        
        private void RefreshGrid()
        {
            try
            {
                gvDispatch.GridControl.BindingContext = new BindingContext();
                gvDispatch.GridControl.DataSource = gvDispatch.DataSource;
                int rowhandle = gvDispatch.LocateByValue("STOCKDISPATCHDETAILID", ObjStockDispatchDetail.STOCKDISPATCHDETAILID);
                if (rowhandle >= 0)
                {
                    if (IsEdit)
                    {
                        gvDispatch.SetRowCellValue(rowhandle, "DISPATCHQUANTITY", txtQuantity.EditValue);
                        gvDispatch.SetRowCellValue(rowhandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
                    }
                    else
                    {
                        int Qnty = 0;
                        decimal WeightInKGS = 0;
                        if (int.TryParse(Convert.ToString(gvDispatch.GetRowCellValue(rowhandle, "DISPATCHQUANTITY")), out Qnty))
                           gvDispatch.SetRowCellValue(rowhandle, "DISPATCHQUANTITY", Qnty + Convert.ToInt32(txtQuantity.EditValue));
                        if (decimal.TryParse(Convert.ToString(gvDispatch.GetRowCellValue(rowhandle, "WEIGHTINKGS")), out WeightInKGS))
                            gvDispatch.SetRowCellValue(rowhandle, "WEIGHTINKGS", WeightInKGS + Convert. ToDecimal(txtWeightInKgs.EditValue));

                    }
                }
                else
                {
                    gvDispatch.AddNewRow();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveDispatch()
        {
            try
            {

                if (XtraMessageBox.Show($"Are you sure want to save dispatch from {cmbFromBranch.Text} to {cmbToBranch.Text} ?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                if (ObjStockDispatch == null)
                {
                    ObjStockDispatch = new StockDispatch();
                    ObjStockDispatch.STOCKDISPATCHID = 0;
                }
                ObjStockDispatch.FROMBRANCHID = cmbFromBranch.EditValue;
                ObjStockDispatch.TOBRANCHID = cmbToBranch.EditValue;
                ObjStockDispatch.CATEGORYID = cmbCategory.EditValue;
                ObjStockDispatch.UserID = Utility.UserID;
                ObjStockRep.SaveDispatch(ObjStockDispatch);
                cmbFromBranch.Enabled = false;
                cmbCategory.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Are you sure want to delete item?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                ObjStockRep.DeleteDispatchDetail(gvDispatch.GetFocusedRowCellValue("STOCKDISPATCHDETAILID"));
                gvDispatch.DeleteRow(gvDispatch.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvDispatch_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, "STOCKDISPATCHDETAILID", ObjStockDispatchDetail.STOCKDISPATCHDETAILID);
                view.SetRowCellValue(e.RowHandle, "ITEMID", cmbLookupView.GetFocusedRowCellValue("ITEMID"));
                view.SetRowCellValue(e.RowHandle, "ITEMCODEID", cmbItemCode.EditValue);
                view.SetRowCellValue(e.RowHandle, "ITEMPRICEID", ItemPriceID);
                view.SetRowCellValue(e.RowHandle, "SKUCODE", cmbLookupView.GetFocusedRowCellValue("SKUCODE"));
                view.SetRowCellValue(e.RowHandle, "ITEMCODE", cmbItemCode.Text);
                view.SetRowCellValue(e.RowHandle, "ITEMNAME", txtItemName.EditValue);
                view.SetRowCellValue(e.RowHandle, "MRP", txtMRP.EditValue);
                view.SetRowCellValue(e.RowHandle, "SALEPRICE", txtSalePrice.EditValue);
                view.SetRowCellValue(e.RowHandle, "DISPATCHQUANTITY", txtQuantity.EditValue);
                view.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
                view.SetRowCellValue(e.RowHandle, "TRAYNUMBER", txtTrayNumber.EditValue);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.EditValue != null && IsParentExist && !IsOpenItem)
                {
                    decimal Multi = 0;
                    if (decimal.TryParse(Convert.ToString(cmbLookupView.GetFocusedRowCellValue("MULTIPLIER")), out Multi))
                    {
                        int Quantity = 0;
                        if (int.TryParse(Convert.ToString(txtQuantity.EditValue), out Quantity))
                        {
                            txtWeightInKgs.EditValue = Multi * Quantity;
                        }
                        else
                            txtWeightInKgs.EditValue = 0;
                    }
                    else
                    {
                        txtWeightInKgs.EditValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvDispatch_DoubleClick(object sender, EventArgs e)
        {

        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCategory.EditValue == null)
                return;
            DataTable dtTemp = Utility.GetItemCodeListFiltered().Copy();
            DataView dv = dtTemp.DefaultView;
            dv.RowFilter = "CATEGORYID = " + cmbCategory.EditValue;
            cmbItemCode.Properties.DataSource = dv.ToTable();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
        }

        private void bntDiscardDispatch_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to discard dispatch?", "Confirm", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes 
                || Convert.ToInt32(ObjStockDispatch.STOCKDISPATCHID) == 0)
                return;
            ObjStockRep.DiscardStockDispatch(ObjStockDispatch.STOCKDISPATCHID, Utility.UserID);
            cmbFromBranch.EditValue = null;
            cmbToBranch.EditValue = null;
            txtTrayNumber.EditValue = null;
            cmbFromBranch.Enabled = true;
            cmbToBranch.Enabled = true;
            if (Utility.Category != "ALL")
                cmbCategory.Enabled = true;
            cmbCategory.EditValue = Utility.CategoryID;
            ObjStockDispatch.STOCKDISPATCHID = 0;
            ObjStockDispatch.dtDispatch = ObjStockDispatch.dtDispatch.Clone();
            gcDispatch.DataSource = ObjStockDispatch.dtDispatch;
            cmbFromBranch.Focus();
        }

        public void ReceiveBarCode(string data)
        {
            cmbItemCode.Text = data;
        }

        private void gvDispatch_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "DISPATCHQUANTITY" || isEventCall) return;
            ObjStockDispatchDetail = new StockDispatchDetail();
            ObjStockDispatchDetail.STOCKDISPATCHDETAILID = gvDispatch.GetFocusedRowCellValue("STOCKDISPATCHDETAILID");
            ObjStockDispatchDetail.STOCKDISPATCHID = ObjStockDispatch.STOCKDISPATCHID;
            ObjStockDispatchDetail.ITEMPRICEID = gvDispatch.GetFocusedRowCellValue("ITEMPRICEID");
            ObjStockDispatchDetail.TRAYNUMBER = gvDispatch.GetFocusedRowCellValue("TRAYNUMBER");
            ObjStockDispatchDetail.DISPATCHQUANTITY = gvDispatch.GetFocusedRowCellValue("DISPATCHQUANTITY");
            ObjStockDispatchDetail.WEIGHTINKGS = gvDispatch.GetFocusedRowCellValue("WEIGHTINKGS");
            ObjStockDispatchDetail.UserID = Utility.UserID;
            ObjStockRep.SaveDispatchDetail(ObjStockDispatchDetail);
            ObjStockDispatchDetail.STOCKDISPATCHDETAILID = 0;
            cmbItemCode.EditValue = null;
            txtItemName.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = null;
            txtWeightInKgs.EditValue = null;
            cmbItemCode.Focus();
        }
    }
}