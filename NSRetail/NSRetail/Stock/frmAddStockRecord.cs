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
        MasterRepository ObjMasterRep = new MasterRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockEntry ObjStockEntry = null;
        StockEntryDetail ObjStockEntryDetail = null;
        object ItemPriceID = null;
        bool IsParentExist = false;
        bool IsOpenItem = false;
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

                cmbGST.Properties.DataSource = Utility.GetGSTInfoList();
                cmbGST.Properties.ValueMember = "GSTID";
                cmbGST.Properties.DisplayMember = "GSTCODE";

                if (Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                    txtCostPriceWOT.Enabled = false;
                else
                    txtCostPriceWT.Enabled = false;

                txtDiscountFlat.EditValue = 0;
                txtDiscountPer.EditValue = 0;
                txtSchemePer.EditValue = 0;
                txtSchemeFlat.EditValue = 0;
                txtFreeQuantity.EditValue = 0;
            }
            catch (Exception ex)
            {
                
            }
        }
        private void FrmStockDispatch_RefreshBaseLineData(object sender, EventArgs e)
        {
            object selectedItemValue = cmbItemCode.EditValue;
            object selectedGSTValue = cmbGST.EditValue;

            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";

            cmbGST.Properties.DataSource = Utility.GetGSTInfoList();
            cmbGST.Properties.ValueMember = "GSTID";
            cmbGST.Properties.DisplayMember = "GSTCODE";

            cmbItemCode.EditValue = selectedItemValue;
            cmbGST.EditValue = selectedGSTValue;
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
                ObjStockEntryDetail.GSTID = cmbGST.EditValue;
                ObjStockEntryDetail.DiscountPercentage = txtDiscountPer.EditValue;
                ObjStockEntryDetail.DiscountFlat = txtDiscountFlat.EditValue;
                ObjStockEntryDetail.SchemePercentage = txtSchemePer.EditValue;
                ObjStockEntryDetail.SchemeFlat = txtSchemeFlat.EditValue;
                ObjStockEntryDetail.FreeQuantity = txtFreeQuantity.EditValue;
                ObjStockEntryDetail.TotalPriceWT = txtTotalPriceWT.EditValue;
                ObjStockEntryDetail.TotalPriceWOT = txtTotalPriceWOT.EditValue;
                ObjStockEntryDetail.AppliedDiscount = txtAppliedDiscount.EditValue;
                ObjStockEntryDetail.AppliedScheme = txtAppliedScheme.EditValue;
                ObjStockEntryDetail.AppliedGST = txtAppliedGST.EditValue;
                ObjStockEntryDetail.FinalPrice = txtFinalPrice.EditValue;
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
                cmbGST.EditValue = null;
                txtDiscountPer.EditValue = 0;
                txtDiscountFlat.EditValue = 0;
                txtSchemePer.EditValue = 0;
                txtSchemeFlat.EditValue = 0;
                txtFreeQuantity.EditValue = 0;
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
                            cmbGST.EditValue = ((DataRowView)obj.drSelected)["GSTID"];
                            txtCostPriceWOT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWOT"];
                            txtCostPriceWT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWT"];
                        }
                    }
                    else
                    {
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                        cmbGST.EditValue = dtMRPList.Rows[0]["GSTID"];
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
                    txtWeightInKGs.EditValue = 0;
                    if (decimal.TryParse(Convert.ToString(cmbLookupView.GetFocusedRowCellValue("MULTIPLIER")), out decimal Multi)
                        && int.TryParse(Convert.ToString(txtQuantity.EditValue), out int Quantity))
                    {
                        txtWeightInKGs.EditValue = Multi * Quantity;
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
                if (IsLoading) 
                {
                    CalculateReadOnlyFields();
                    return; 
                }

                if (txtCostPriceWOT.EditValue != null && 
                    !Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE) && cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
                {
                    txtCostPriceWT.EditValue = Convert.ToDecimal(txtCostPriceWOT.EditValue) +
                        Convert.ToDecimal(txtCostPriceWOT.EditValue) * gstInfo.TAXPercent;
                    CalculateReadOnlyFields();
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
                if (IsLoading)
                {
                    CalculateReadOnlyFields();
                    return;
                }

                if (txtCostPriceWT.EditValue != null && Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE) && cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
                {
                    txtCostPriceWOT.EditValue = Convert.ToDecimal(txtCostPriceWT.EditValue) -
                        Convert.ToDecimal(txtCostPriceWT.EditValue) * gstInfo.TAXPercent;
                    CalculateReadOnlyFields();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void cmbGST_EditValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                CalculateReadOnlyFields();
                return;
            }

            if (Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                txtCostPriceWT_EditValueChanged(null, null);
            else
                txtCostPriceWOT_EditValueChanged(null, null);
        }

        private void CalculateReadOnlyFields()
        
        {
            decimal totalPriceWT = Convert.ToDecimal(txtCostPriceWT.EditValue)
                        * Convert.ToInt32(txtQuantity.EditValue);
            decimal totalPriceWOT = (Convert.ToDecimal(txtCostPriceWOT.EditValue))
                * Convert.ToInt32(txtQuantity.EditValue);
            decimal discountPer = txtDiscountPer.EditValue != null ? Convert.ToDecimal(txtDiscountPer.EditValue) : 0;
            decimal discountFlat = txtDiscountPer.EditValue != null && discountPer == 0 && txtDiscountFlat.EditValue != null
                ? Convert.ToDecimal(txtDiscountFlat.EditValue) : 0;             
            decimal schemePer = txtSchemePer.EditValue != null ? Convert.ToDecimal(txtSchemePer.EditValue) : 0;
            decimal schemeFlat = txtSchemeFlat.EditValue != null && schemePer == 0 && txtSchemeFlat.EditValue != null
                ? Convert.ToDecimal(txtSchemeFlat.EditValue) : 0;
            decimal appliedDiscount = discountFlat > 0
                    ? discountFlat
                    : discountPer > 0
                        ? (totalPriceWOT * (discountPer / 100))
                        : 0;
            decimal appliedScheme = schemeFlat > 0
                    ? schemeFlat
                    : schemePer > 0
                        ? (totalPriceWOT * (schemePer / 100))
                        : 0;
            decimal finalPriceWOT = totalPriceWOT - appliedDiscount - appliedScheme;
            decimal appliedGST = cmbGST.GetSelectedDataRow() is GSTInfo gstInfo ?
                finalPriceWOT * gstInfo.TAXPercent : 0;
            decimal finalPrice = finalPriceWOT + appliedGST;

            txtTotalPriceWT.EditValue = totalPriceWT;
            txtTotalPriceWOT.EditValue = totalPriceWOT;
            txtAppliedDiscount.EditValue = appliedDiscount;
            txtAppliedScheme.EditValue = appliedScheme;
            txtFinalPrice.EditValue = finalPrice;
            txtAppliedGST.EditValue = appliedGST;
        }

        private void txtDiscountValue_EditValueChanged(object sender, EventArgs e)
        {
            (sender == txtDiscountPer && txtDiscountPer.EditValue != null && Convert.ToDecimal(txtDiscountPer.EditValue) > 0
                ? txtDiscountFlat : txtDiscountPer).EditValue = 0;
            CalculateReadOnlyFields();
        }

        private void txtSchemeValue_EditValueChanged(object sender, EventArgs e)
        {
            (sender == txtSchemePer && txtSchemePer.EditValue != null && Convert.ToDecimal(txtSchemePer.EditValue) > 0
                ? txtSchemeFlat : txtSchemePer).EditValue = 0;
            CalculateReadOnlyFields();
        }
    }
}