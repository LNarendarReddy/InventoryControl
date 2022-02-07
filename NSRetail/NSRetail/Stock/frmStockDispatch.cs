using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Entity;
using ErrorManagement;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockDispatch : DevExpress.XtraEditors.XtraForm
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
        public frmStockDispatch()
        {
            InitializeComponent();
        }

        private void frmStockDispatch_Load(object sender, EventArgs e)
        {
            try
            {
                ((frmMain)MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
                DataTable dtBranch = ObjMasterRep.GetBranch();
                DataView dvBranch = dtBranch.Copy().DefaultView;
                dvBranch.RowFilter = "ISWAREHOUSE = 0";
                cmbToBranch.Properties.DataSource = dvBranch;
                cmbToBranch.Properties.ValueMember = "BRANCHID";
                cmbToBranch.Properties.DisplayMember = "BRANCHNAME";

                DataView gvWarehouse = dtBranch.Copy().DefaultView;
                gvWarehouse.RowFilter = "ISWAREHOUSE = 1";
                cmbFromBranch.Properties.DataSource = gvWarehouse;
                cmbFromBranch.Properties.ValueMember = "BRANCHID";
                cmbFromBranch.Properties.DisplayMember = "BRANCHNAME";

                cmbItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered();
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";

                if (ObjStockDispatch == null)
                    ObjStockDispatch = new StockDispatch();
                ObjStockDispatch.UserID = Utility.UserID;
                ObjStockDispatch.CATEGORYID = Utility.CategoryID;
                ObjStockRep.GetDispatchDraft(ObjStockDispatch);
                if (Convert.ToInt32(ObjStockDispatch.STOCKDISPATCHID) > 0)
                {
                    cmbFromBranch.EditValue = ObjStockDispatch.FROMBRANCHID;
                    cmbToBranch.EditValue = ObjStockDispatch.TOBRANCHID;
                    gcDispatch.DataSource = ObjStockDispatch.dtDispatch;
                    cmbFromBranch.Enabled = false;
                    cmbToBranch.Enabled = false;
                }
                else
                {
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
            object selectedValue = cmbItemCode.EditValue;
            cmbItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
            cmbItemCode.EditValue = selectedValue;
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            try
            {
                int iValue = 0;
                if (int.TryParse(Convert.ToString(ObjStockDispatch.STOCKDISPATCHID), out iValue) && iValue > 0)
                {
                    if (!dxValidationProvider1.Validate())
                        return;
                    ObjStockRep.UpdateDispatch(ObjStockDispatch);
                    DataSet ds = ObjStockRep.GetDispatch(ObjStockDispatch.STOCKDISPATCHID);
                    rptDispatch rpt = new rptDispatch(ds.Tables[0], ds.Tables[1]);
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowRibbonPreview();
                    cmbFromBranch.EditValue = null;
                    cmbToBranch.EditValue = null;
                    txtTrayNumber.EditValue = null;
                    cmbFromBranch.Enabled = true;
                    cmbToBranch.Enabled = true;
                    ObjStockDispatch.STOCKDISPATCHID = 0;
                    ObjStockDispatch.dtDispatch = new DataTable();
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
                    txtItemName.EditValue = cmbLookupView.GetFocusedRowCellValue("ITEMNAME");
                    DataTable dtMRPList = ObjItemRep.GetMRPList(cmbItemCode.EditValue);
                    if (dtMRPList.Rows.Count > 1)
                    {
                        frmMRPList obj = new frmMRPList(dtMRPList);
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
                    if(int.TryParse(Convert.ToString(cmbLookupView.GetFocusedRowCellValue("PARENTITEMID")),out ParentID) 
                        && ParentID > 0)
                    {
                        IsParentExist = true;
                        if(bool.TryParse(Convert.ToString(cmbLookupView.GetFocusedRowCellValue("ISOPENITEM")), out IsOpenItem) 
                            && IsOpenItem)
                        {
                            txtQuantity.Enabled = false;
                            txtWeightInKgs.Enabled = true;
                        }
                        else
                        {
                            txtQuantity.Enabled = true;
                            txtWeightInKgs.Enabled = false;
                        }
                        DataTable dt = ObjStockRep.GetCurrentStock(cmbFromBranch.EditValue, cmbToBranch.EditValue,
                            cmbItemCode.EditValue, ParentID);
                        if(dt != null && dt.Rows.Count > 0)
                        {
                            txtWarehouseStock.EditValue = dt.Rows[0][0];
                            txtBranchStock.EditValue = dt.Rows[0][1];
                        }
                    }
                    else
                    {
                        txtWeightInKgs.EditValue = 0;
                        txtWeightInKgs.Enabled = false;
                        IsParentExist = false;
                        DataTable dt = ObjStockRep.GetCurrentStock(cmbFromBranch.EditValue, cmbToBranch.EditValue,
                            cmbItemCode.EditValue, ParentID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            txtWarehouseStock.EditValue = dt.Rows[0][0];
                            txtBranchStock.EditValue = dt.Rows[0][1];
                        }
                    }
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
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (!dxValidationProvider2.Validate())
                        return;
                    if (Convert.ToInt32(ObjStockDispatch.STOCKDISPATCHID) == 0)
                        SaveDispatch();
                    ObjStockDispatchDetail = new StockDispatchDetail();
                    ObjStockDispatchDetail.STOCKDISPATCHDETAILID = 0;
                    ObjStockDispatchDetail.STOCKDISPATCHID = ObjStockDispatch.STOCKDISPATCHID;
                    ObjStockDispatchDetail.ITEMPRICEID = ItemPriceID;
                    ObjStockDispatchDetail.TRAYNUMBER = txtTrayNumber.EditValue;
                    ObjStockDispatchDetail.DISPATCHQUANTITY = txtQuantity.EditValue;
                    ObjStockDispatchDetail.WEIGHTINKGS = txtWeightInKgs.EditValue;
                    ObjStockDispatchDetail.UserID = Utility.UserID;
                    ObjStockRep.SaveDispatchDetail(ObjStockDispatchDetail);
                    RefreshGrid();
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
                if (ObjStockDispatch == null)
                {
                    ObjStockDispatch = new StockDispatch();
                    ObjStockDispatch.STOCKDISPATCHID = 0;
                }
                ObjStockDispatch.FROMBRANCHID = cmbFromBranch.EditValue;
                ObjStockDispatch.TOBRANCHID = cmbToBranch.EditValue;
                ObjStockDispatch.CATEGORYID = Utility.CategoryID;
                ObjStockDispatch.UserID = Utility.UserID;
                ObjStockRep.SaveDispatch(ObjStockDispatch);
                cmbFromBranch.Enabled = false;
                cmbToBranch.Enabled = false;
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
    }
}