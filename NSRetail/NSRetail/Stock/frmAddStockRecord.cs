using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using NSRetail;
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
        bool IsEditMode = false;

        frmStockEntry frmparent = null;
        public frmAddStockRecord(StockEntry _ObjStockEntry,frmStockEntry _frmparent,
            StockEntryDetail _ObjStockEntryDetail)
        {
            InitializeComponent();
            ObjStockEntry = _ObjStockEntry;
            frmparent = _frmparent;
            ObjStockEntryDetail = _ObjStockEntryDetail;
        }
        private void frmAddStockRecord_Load(object sender, EventArgs e)
        {
            try
            {
                ((frmMain)frmparent.MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
                
                cmbItemCode.Properties.DataSource = !Utility.IsOpenCategory ?
                    Utility.GetItemCodeList() : ObjItemRep.GetParentItems(Utility.CategoryID);
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";

                sluFreeItem.Properties.DataSource = Utility.GetItemCodeList();
                sluFreeItem.Properties.ValueMember = "ITEMCODEID";
                sluFreeItem.Properties.DisplayMember = "ITEMNAME";

                cmbGST.Properties.DataSource = Utility.GetGSTInfoList();
                cmbGST.Properties.ValueMember = "GSTID";
                cmbGST.Properties.DisplayMember = "GSTCODE";

                if (Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                    txtCostPriceWOT.Enabled = false;
                else
                    txtCostPriceWT.Enabled = false;

                int Ivalue = 0;
                if (ObjStockEntryDetail.STOCKENTRYDETAILID != null && 
                    int.TryParse(Convert.ToString(ObjStockEntryDetail.STOCKENTRYDETAILID), out Ivalue) && Ivalue > 0)
                {
                    IsEditMode = true;
                    cmbItemCode.EditValue = ObjStockEntryDetail.ITEMCODEID;
                    txtItemName.EditValue = ObjStockEntryDetail.ITEMNAME;
                    txtQuantity.EditValue = ObjStockEntryDetail.QUANTITY;
                    txtWeightInKGs.EditValue = ObjStockEntryDetail.WEIGHTINKGS;
                    txtMRP.EditValue = ObjStockEntryDetail.MRP;
                    txtSalePrice.EditValue = ObjStockEntryDetail.SALEPRICE;
                    txtCostPriceWOT.EditValue = ObjStockEntryDetail.COSTPRICEWOT;
                    txtCostPriceWT.EditValue = ObjStockEntryDetail.COSTPRICEWT;
                    txtFreeQuantity.EditValue = ObjStockEntryDetail.FreeItemMRPID;
                    txtDiscountPer.EditValue = ObjStockEntryDetail.DiscountPercentage;
                    txtDiscountFlat.EditValue = ObjStockEntryDetail.DiscountFlat;
                    txtSchemePer.EditValue = ObjStockEntryDetail.SchemePercentage;
                    txtSchemeFlat.EditValue = ObjStockEntryDetail.SchemeFlat;
                    cmbGST.EditValue = ObjStockEntryDetail.GSTID;
                    txtIGST.EditValue = ObjStockEntryDetail.IGST;
                    txtSGST.EditValue = ObjStockEntryDetail.SGST;
                    txtCGST.EditValue = ObjStockEntryDetail.CGST;
                    txtCESS.EditValue = ObjStockEntryDetail.CESS;
                    txtAppliedGST.EditValue = ObjStockEntryDetail.AppliedGST;
                    txtTotalPriceWOT.EditValue = ObjStockEntryDetail.TotalPriceWOT;
                    txtTotalPriceWT.EditValue = ObjStockEntryDetail.TotalPriceWT;
                    txtAppliedDiscount.EditValue = ObjStockEntryDetail.AppliedDiscount;
                    txtAppliedScheme.EditValue = ObjStockEntryDetail.AppliedScheme;
                    txtFinalPrice.EditValue = ObjStockEntryDetail.FinalPrice;

                }
                else
                {
                    txtDiscountFlat.EditValue = 0.00;
                    txtDiscountPer.EditValue = 0.00;
                    txtSchemePer.EditValue = 0.00;
                    txtSchemeFlat.EditValue = 0.00;
                    txtFreeQuantity.EditValue = 0.00;
                }
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

            sluFreeItem.Properties.DataSource = Utility.GetItemCodeList();
            sluFreeItem.Properties.ValueMember = "ITEMCODEID";
            sluFreeItem.Properties.DisplayMember = "ITEMNAME";

            cmbItemCode.EditValue = selectedItemValue;
            cmbGST.EditValue = selectedGSTValue;
            sluFreeItem.EditValue = selectedItemValue;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;

                if(sluFreeItem.EditValue!= null)
                {
                    if (txtFreeQuantity.EditValue == null)
                    {
                        XtraMessageBox.Show("Free Quantity is Mandatory!");
                        return;
                    }
                }
                if (Convert.ToInt32(ObjStockEntry.STOCKENTRYID) == 0)
                    frmparent.SaveInvoice();
                if (!IsEditMode)
                    ObjStockEntryDetail = new StockEntryDetail();
                ObjStockEntryDetail.STOCKENTRYID = ObjStockEntry.STOCKENTRYID;
                ObjStockEntryDetail.ITEMCODEID = cmbItemCode.EditValue;
                ObjStockEntryDetail.ITEMID = ((DataRowView)cmbItemCode.GetSelectedDataRow())["ITEMID"];
                ObjStockEntryDetail.ITEMPRICEID = ItemPriceID;
                ObjStockEntryDetail.SKUCODE = ((DataRowView)cmbItemCode.GetSelectedDataRow())["SKUCODE"];
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
                ObjStockEntryDetail.CGST = txtCGST.EditValue;
                ObjStockEntryDetail.SGST = txtSGST.EditValue;
                ObjStockEntryDetail.IGST = txtIGST.EditValue;
                ObjStockEntryDetail.CESS = txtCESS.EditValue;
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
                txtDiscountPer.EditValue = 0.00;
                txtDiscountFlat.EditValue = 0.00;
                txtSchemePer.EditValue = 0.00;
                txtSchemeFlat.EditValue = 0.00;
                txtFreeQuantity.EditValue = 0.00;
                txtCGST.EditValue = 0.00;
                txtSGST.EditValue = 0.00;
                txtIGST.EditValue = 0.00;
                txtCESS.EditValue = 0.00;
                IsEditMode = false;
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
                    int IValue = 0;
                    if (int.TryParse(Convert.ToString(ObjStockEntryDetail.STOCKENTRYDETAILID), out IValue) && IValue > 0)
                    { 
                        return;
                        }
                    IsLoading = true;
                    txtItemName.EditValue = cmbLookupView.GetFocusedDataRow()["ITEMNAME"];
                    DataTable dtCPList = ObjItemRep.GetCostPriceList(cmbItemCode.EditValue);
                    if (dtCPList.Rows.Count > 1)
                    {
                        frmCostPriceList obj = new frmCostPriceList(dtCPList);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {
                            txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                            ItemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                            cmbGST.EditValue = ((DataRowView)obj.drSelected)["GSTID"];
                            txtCostPriceWOT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWOT"] == DBNull.Value ? 0.00 : ((DataRowView)obj.drSelected)["COSTPRICEWOT"];
                            txtCostPriceWT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWT"] == DBNull.Value ? 0.00 : ((DataRowView)obj.drSelected)["COSTPRICEWT"];
                        }
                    }
                    else if(dtCPList.Rows.Count > 0)
                    {
                        txtMRP.EditValue = dtCPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtCPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtCPList.Rows[0]["ITEMPRICEID"];
                        cmbGST.EditValue = dtCPList.Rows[0]["GSTID"];
                        txtCostPriceWOT.EditValue = dtCPList.Rows[0]["COSTPRICEWOT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["COSTPRICEWOT"];
                        txtCostPriceWT.EditValue = dtCPList.Rows[0]["COSTPRICEWT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["COSTPRICEWT"];
                    }

                    int ParentID = 0;
                    if (int.TryParse(Convert.ToString(cmbLookupView.GetFocusedDataRow()["PARENTITEMID"]), out ParentID)
                        && ParentID > 0)
                    {
                        IsParentExist = true;
                        if (bool.TryParse(Convert.ToString(cmbLookupView.GetFocusedDataRow()["ISOPENITEM"]), out IsOpenItem)
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
                    if (decimal.TryParse(Convert.ToString(cmbLookupView.GetFocusedDataRow()["MULTIPLIER"]), out decimal Multi)
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
                    !Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE) 
                    && cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
                {
                    decimal cpWT = 0;
                    if (decimal.TryParse(Convert.ToString(txtCostPriceWOT.EditValue), out cpWT))
                    {
                        txtCostPriceWT.EditValue = cpWT +
                            Math.Round(cpWT * gstInfo.TAXPercent, 4);
                        CalculateReadOnlyFields();
                    }
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

                if (txtCostPriceWT.EditValue != null 
                    && Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE) 
                    && cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
                {
                    decimal cpWT = 0;
                    if (decimal.TryParse(Convert.ToString(txtCostPriceWT.EditValue), out cpWT))
                    {
                        txtCostPriceWOT.EditValue = cpWT - Math.Round(cpWT * gstInfo.TAXPercent, 4);
                        CalculateReadOnlyFields();
                    }
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
                        ? Math.Round(totalPriceWOT * (discountPer / 100), 4)
                        : 0;
            decimal appliedScheme = schemeFlat > 0
                    ? schemeFlat
                    : schemePer > 0
                        ? Math.Round(totalPriceWOT * (schemePer / 100), 4)
                        : 0;
            decimal finalPriceWOT = totalPriceWOT - appliedDiscount - appliedScheme;
            decimal appliedGST = 0.0M;
            decimal cGST = 0.0M;
            decimal sGST = 0.0M;
            decimal iGST = 0.0M;
            decimal cess = 0.0M;
            if (cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
            {
                cGST = Math.Round(finalPriceWOT * gstInfo.CGST / 100, 2);
                sGST = Math.Round(finalPriceWOT * gstInfo.SGST / 100, 2);
                iGST = Math.Round(finalPriceWOT * gstInfo.IGST / 100, 2);
                cess = Math.Round(finalPriceWOT * gstInfo.CESS / 100, 2);
                appliedGST = cGST + sGST + iGST + cess;
            }

            decimal finalPrice = finalPriceWOT + appliedGST;

            txtCGST.EditValue = cGST;
            txtSGST.EditValue = sGST;
            txtIGST.EditValue = iGST;
            txtCESS.EditValue = cess;
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
                ? txtDiscountFlat : txtDiscountPer).EditValue = 0.00;
            CalculateReadOnlyFields();
        }
        private void txtSchemeValue_EditValueChanged(object sender, EventArgs e)
        {
            (sender == txtSchemePer && txtSchemePer.EditValue != null && Convert.ToDecimal(txtSchemePer.EditValue) > 0
                ? txtSchemeFlat : txtSchemePer).EditValue = 0.00;
            CalculateReadOnlyFields();
        }
        private void sluFreeItem_EditValueChanged(object sender, EventArgs e)
        {
            if (sluFreeItem.EditValue != null)
                txtFreeQuantity.Enabled = true;  
           else
                txtFreeQuantity.Enabled = false;
        }
    }
}