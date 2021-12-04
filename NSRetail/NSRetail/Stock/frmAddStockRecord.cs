using DataAccess;
using Entity;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmAddStockRecord : DevExpress.XtraEditors.XtraForm
    {
        ItemCodeRepository ObjItemRep = new ItemCodeRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockEntry ObjStockEntry = null;
        StockEntryDetail ObjStockEntryDetail = null;
        object ItemPriceID = null;
        bool IsParentExist = false;
        bool IsOpenItem = false;
        GSTInfo gSTInfo = new GSTInfo();
        bool IsLoading = false; 

        frmStockEntry frmparent = null;
        public frmAddStockRecord(StockEntry _ObjStockEntry,frmStockEntry _frmparent)
        {
            InitializeComponent();
            ObjStockEntry = _ObjStockEntry;
            frmparent = _frmparent;
        }
        private void frmAddStockRecord_Load(object sender, EventArgs e)
        {
            try
            {
                ((frmMain)frmparent.MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
                if (!Utility.IsOpenCategory)
                {
                    cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
                    cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                    cmbItemCode.Properties.DisplayMember = "ITEMCODE";
                }
                else
                {
                    cmbItemCode.Properties.DataSource = ObjItemRep.GetParentItems(Utility.CategoryID);
                    cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                    cmbItemCode.Properties.DisplayMember = "ITEMCODE";
                }
                if (Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                    txtCostPriceWOT.Enabled = false;
                else
                    txtCostPriceWT.Enabled = false;
            }
            catch (Exception ex)
            {
                
            }
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
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                if (Convert.ToInt32(ObjStockEntry.STOCKENTRYID) == 0)
                    frmparent.SaveInvoice();
                ObjStockEntryDetail = new StockEntryDetail();
                ObjStockEntryDetail.STOCKENTRYDETAILID = 0;
                ObjStockEntryDetail.STOCKENTRYID = ObjStockEntry.STOCKENTRYID;
                ObjStockEntryDetail.ITEMID = cmbLookupView.GetFocusedRowCellValue("ITEMID");
                ObjStockEntryDetail.ITEMCODEID = cmbItemCode.EditValue;
                ObjStockEntryDetail.ITEMPRICEID = ItemPriceID;
                ObjStockEntryDetail.SKUCODE = cmbLookupView.GetFocusedRowCellValue("SKUCODE");
                ObjStockEntryDetail.ITEMCODE = cmbItemCode.Text;
                ObjStockEntryDetail.ITEMNAME = txtItemName.EditValue;
                ObjStockEntryDetail.COSTPRICEWT = txtCostPriceWT.EditValue;
                ObjStockEntryDetail.COSTPRICEWOT = txtCostPriceWOT.EditValue;
                ObjStockEntryDetail.MRP = txtMRP.EditValue;
                ObjStockEntryDetail.SALEPRICE = txtSalePrice.EditValue;
                ObjStockEntryDetail.QUANTITY = txtQuantity.EditValue;
                ObjStockEntryDetail.WEIGHTINKGS = txtWeightInKGs.EditValue;
                ObjStockEntryDetail.UserID = Utility.UserID;
                ObjStockRep.SaveInvoiceDetail(ObjStockEntryDetail);
                frmparent.RefreshGrid(ObjStockEntryDetail);
                ObjStockEntryDetail.STOCKENTRYDETAILID = 0;
                cmbItemCode.EditValue = null;
                txtItemName.EditValue = null;
                txtMRP.EditValue = null;
                txtSalePrice.EditValue = null;
                txtQuantity.EditValue = null;
                txtWeightInKGs.EditValue = null;
                txtCostPriceWOT.EditValue = null;
                txtCostPriceWT.EditValue = null;
                txtTotalPriceWT.EditValue = null;
                txtTotalPriceWOT.EditValue = null;
                cmbItemCode.Focus();
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
                    IsLoading = true;
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
                            gSTInfo.UpdateGST((DataRowView)obj.drSelected);
                            txtGSTCode.EditValue = gSTInfo.GSTCODE;
                            txtCess.EditValue = gSTInfo.CESS;
                            txtCostPriceWOT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWOT"];
                            txtCostPriceWT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWT"];
                        }
                    }
                    else
                    {
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                        gSTInfo.UpdateGST(dtMRPList.Rows[0]);
                        txtGSTCode.EditValue = gSTInfo.GSTCODE;
                        txtCess.EditValue = gSTInfo.CESS;
                        txtCostPriceWOT.EditValue = dtMRPList.Rows[0]["COSTPRICEWOT"];
                        txtCostPriceWT.EditValue = dtMRPList.Rows[0]["COSTPRICEWT"];
                    }

                    int ParentID = 0;
                    if (int.TryParse(Convert.ToString(cmbLookupView.GetFocusedRowCellValue("PARENTITEMID")), out ParentID)
                        && ParentID > 0)
                    {
                        IsParentExist = true;
                        if (bool.TryParse(Convert.ToString(cmbLookupView.GetFocusedRowCellValue("ISOPENITEM")), out IsOpenItem)
                            && IsOpenItem)
                        {
                            txtQuantity.Enabled = false;
                            txtWeightInKGs.Enabled = true;
                        }
                        else
                        {
                            txtQuantity.Enabled = true;
                            txtWeightInKGs.Enabled = false;
                        }
                    }
                    else
                    {
                        txtWeightInKGs.EditValue = 0;
                        txtWeightInKGs.Enabled = false;
                        IsParentExist = false;
                    }
                    txtQuantity.EditValue = 1;
                    IsLoading = false;
                    
                    SendKeys.Send("{ENTER}");
                }
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
                if (string.IsNullOrEmpty(Convert.ToString(txtQuantity.EditValue))) return;
                
                if (IsParentExist && !IsOpenItem)
                {
                    decimal Multi = 0;
                    if (decimal.TryParse(Convert.ToString(cmbLookupView.GetFocusedRowCellValue("MULTIPLIER")), out Multi))
                    {
                        int Quantity = 0;
                        if (int.TryParse(Convert.ToString(txtQuantity.EditValue), out Quantity))
                        {
                            txtWeightInKGs.EditValue = Multi * Quantity;
                        }
                        else
                            txtWeightInKGs.EditValue = 0;
                    }
                    else
                    {
                        txtWeightInKGs.EditValue = 0;
                    }
                }
                if (!IsLoading)
                {
                    if (Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                        txtCostPriceWT_EditValueChanged(null, null);
                    else
                        txtCostPriceWOT_EditValueChanged(null, null);
                }
                else
                {
                    txtTotalPriceWOT.EditValue = txtCostPriceWOT.EditValue;
                    txtTotalPriceWT.EditValue = txtCostPriceWT.EditValue;
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void txtCostPriceWOT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsLoading) return;
                if (txtCostPriceWOT.EditValue != null && 
                    !Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                {
                    txtCostPriceWT.EditValue = Convert.ToDecimal(txtCostPriceWOT.EditValue) +
                        Convert.ToDecimal(txtCostPriceWOT.EditValue) * gSTInfo.TAXPercent;
                    txtTotalPriceWOT.EditValue = Convert.ToDecimal(txtCostPriceWOT.EditValue)
                        * Convert.ToInt32(txtQuantity.EditValue);
                    txtTotalPriceWT.EditValue = (Convert.ToDecimal(txtCostPriceWT.EditValue))
                        * Convert.ToInt32(txtQuantity.EditValue);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void txtCostPriceWT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsLoading) return;
                if (txtCostPriceWT.EditValue != null && Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                {
                    txtCostPriceWOT.EditValue = Convert.ToDecimal(txtCostPriceWT.EditValue) -
                        Convert.ToDecimal(txtCostPriceWT.EditValue) * gSTInfo.TAXPercent;
                    txtTotalPriceWT.EditValue = Convert.ToDecimal(txtCostPriceWT.EditValue) 
                        * Convert.ToInt32(txtQuantity.EditValue);
                    txtTotalPriceWOT.EditValue = (Convert.ToDecimal(txtCostPriceWOT.EditValue))
                        * Convert.ToInt32(txtQuantity.EditValue);
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