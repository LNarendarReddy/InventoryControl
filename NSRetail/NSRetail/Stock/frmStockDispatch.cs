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

namespace NSRetail.Stock
{
    public partial class frmStockDispatch : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        ItemCodeRepository ObjItemRep = new ItemCodeRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockDispatch ObjStockDispatch = null;
        StockDispatchDetail ObjStockDispatchDetail = null;
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

                cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";

                if (ObjStockDispatch == null)
                    ObjStockDispatch = new StockDispatch();
                ObjStockDispatch.UserID = Utility.UserID;
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
            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
            cmbItemCode.EditValue = selectedValue;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public static class ItemDetails
        {
            public static object ItemPriceID = null;
            public static object ItemID = null;
            public static object ItemCodeID = null;
            public static object SKUCode = null;
        }
        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbItemCode.EditValue != null)
                {
                    txtItemName.EditValue = cmbLookupView.GetFocusedRowCellValue("ITEMNAME");
                    ItemDetails.ItemID = cmbLookupView.GetFocusedRowCellValue("ITEMID");
                    ItemDetails.ItemCodeID = cmbLookupView.GetFocusedRowCellValue("ITEMCODEID");
                    ItemDetails.SKUCode = cmbLookupView.GetFocusedRowCellValue("SKUCODE");
                    DataTable dtMRPList = ObjItemRep.GetMRPList(cmbItemCode.EditValue);
                    if (dtMRPList.Rows.Count > 1)
                    {
                        frmMRPList obj = new frmMRPList(dtMRPList);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {
                            txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                            ItemDetails.ItemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                            txtWeightInKgs.EditValue = 0;
                        }
                    }
                    else
                    {
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                        ItemDetails.ItemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                        txtWeightInKgs.EditValue = 0;
                    }
                    txtQuantity.EditValue = 1;
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
                    if (ObjStockDispatch.STOCKDISPATCHID == null)
                        SaveDispatch();
                    ObjStockDispatchDetail = new StockDispatchDetail();
                    ObjStockDispatchDetail.STOCKDISPATCHDETAILID = 0;
                    ObjStockDispatchDetail.STOCKDISPATCHID = ObjStockDispatch.STOCKDISPATCHID;
                    ObjStockDispatchDetail.ITEMPRICEID = ItemDetails.ItemPriceID;
                    ObjStockDispatchDetail.TRAYNUMBER = txtTrayNumber.EditValue;
                    ObjStockDispatchDetail.DISPATCHQUANTITY = txtQuantity.EditValue;
                    ObjStockDispatchDetail.WEIGHTINKGS = txtWeightInKgs.EditValue;
                    ObjStockDispatchDetail.UserID = Utility.UserID;
                    ObjStockRep.SaveDispatchDetail(ObjStockDispatchDetail);
                    DataRow dr = ObjStockDispatch.dtDispatch.NewRow();
                    dr["STOCKDISPATCHDETAILID"] = ObjStockDispatchDetail.STOCKDISPATCHDETAILID;
                    dr["ITEMID"] = ItemDetails.ItemID;
                    dr["ITEMCODEID"] = ItemDetails.ItemCodeID;
                    dr["ITEMPRICEID"] = ItemDetails.ItemPriceID;
                    dr["SKUCODE"] = ItemDetails.SKUCode;
                    dr["ITEMCODE"] = cmbItemCode.Text;
                    dr["ITEMNAME"] = txtItemName.EditValue;
                    dr["MRP"] = txtMRP.EditValue;
                    dr["SALEPRICE"] = txtSalePrice.EditValue;
                    dr["DISPATCHQUANTITY"] = txtQuantity.EditValue;
                    dr["WEIGHTINKGS"] = txtWeightInKgs.EditValue;
                    dr["TRAYNUMBER"] = txtTrayNumber.EditValue;
                    ObjStockDispatch.dtDispatch.Rows.Add(dr);
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
    }
}