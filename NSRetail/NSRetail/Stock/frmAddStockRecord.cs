using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmAddStockRecord : XtraForm
    {
        ItemCodeRepository ObjItemRep = new ItemCodeRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockEntry ObjStockEntry = null;
        StockEntryDetail ObjStockEntryDetail = null;
        object ItemPriceID = null;
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
                    Utility.GetItemCodeListFiltered() : ObjItemRep.GetParentItems(Utility.CategoryID);
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";

                cmbLookupView.GridControl.BindingContext = new BindingContext();
                cmbLookupView.GridControl.DataSource = cmbItemCode.Properties.DataSource;

                sluFreeItem.Properties.DataSource = Utility.GetItemCodeList();
                sluFreeItem.Properties.ValueMember = "ITEMCODEID";
                sluFreeItem.Properties.DisplayMember = "ITEMNAME";

                cmbGST.Properties.DataSource = Utility.GetGSTInfoList();
                cmbGST.Properties.ValueMember = "GSTID";
                cmbGST.Properties.DisplayMember = "GSTCODE";

                bool enabled = Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE);
                txtCostPriceWOT.Enabled = !enabled;
                txtCostPriceWT.Enabled = enabled;

                if (int.TryParse(Convert.ToString(ObjStockEntryDetail.STOCKENTRYDETAILID), out int Ivalue) && Ivalue > 0)
                {
                    IsEditMode = true;
                    IsLoading = true;
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
                    IsLoading = false;
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

                if (decimal.TryParse(Convert.ToString(txtMRP.EditValue), out decimal MRP) &&
                    decimal.TryParse(Convert.ToString(txtCostPriceWT.EditValue), out decimal CostPriceWT) &&
                    decimal.TryParse(Convert.ToString(txtSalePrice.EditValue), out decimal salePrice))
                {
                    string message = String.Empty;

                    message = MRP < salePrice ? "MRP cannot be less than sale price" : message;
                    message = salePrice < CostPriceWT ? "Cost Price cannot be less than sale price" : message;

                    if (!string.IsNullOrEmpty(message))
                    {
                        XtraMessageBox.Show(message);
                        return;
                    }
                }

                if (sluFreeItem.EditValue!= null)
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
                ObjStockEntryDetail.HSNCODE = txtHSNCode.EditValue;
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
                txtHSNCode.EditValue = null;    
                txtDiscountPer.EditValue = 0.00;
                txtDiscountFlat.EditValue = 0.00;
                txtSchemePer.EditValue = 0.00;
                txtSchemeFlat.EditValue = 0.00;
                txtFreeQuantity.EditValue = 0.00;
                txtCGST.EditValue = 0.00;
                txtSGST.EditValue = 0.00;
                txtIGST.EditValue = 0.00;
                txtCESS.EditValue = 0.00;
                txtHSNCode.EditValue = null;
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
                    int rowhandle = cmbLookupView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);

                    IsOpenItem = bool.TryParse(Convert.ToString(cmbLookupView.GetRowCellValue(rowhandle, "ISOPENITEM")),
                        out IsOpenItem) && IsOpenItem;

                    txtQuantity.Enabled = !IsOpenItem;
                    txtWeightInKGs.Enabled = IsOpenItem;

                    if (int.TryParse(Convert.ToString(ObjStockEntryDetail.STOCKENTRYDETAILID), out int IValue) &&  IValue > 0)
                    {
                        return;
                    }
                    IsLoading = true;

                    txtItemName.EditValue = cmbLookupView.GetRowCellValue(rowhandle, "ITEMNAME");
                    txtHSNCode.EditValue = cmbLookupView.GetRowCellValue(rowhandle, "HSNCODE");
                    DataTable dtCPList = ObjItemRep.GetCostPriceList(cmbItemCode.EditValue);
                    if (dtCPList.Rows.Count > 1)
                    {
                        frmCostPriceList obj = new frmCostPriceList(dtCPList);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {
                            cmbGST.EditValue = ((DataRowView)obj.drSelected)["GSTID"];
                            txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                            ItemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                            txtCostPriceWOT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWOT"] == DBNull.Value ? 0.00 : ((DataRowView)obj.drSelected)["COSTPRICEWOT"];
                            txtCostPriceWT.EditValue = ((DataRowView)obj.drSelected)["COSTPRICEWT"] == DBNull.Value ? 0.00 : ((DataRowView)obj.drSelected)["COSTPRICEWT"];
                        }
                    }
                    else if (dtCPList.Rows.Count > 0)
                    {
                        cmbGST.EditValue = dtCPList.Rows[0]["GSTID"];
                        txtMRP.EditValue = dtCPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtCPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtCPList.Rows[0]["ITEMPRICEID"];
                        txtCostPriceWOT.EditValue = dtCPList.Rows[0]["COSTPRICEWOT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["COSTPRICEWOT"];
                        txtCostPriceWT.EditValue = dtCPList.Rows[0]["COSTPRICEWT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["COSTPRICEWT"];
                    }

                    txtQuantity.EditValue = 1;
                    CalculateReadOnlyFields();
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
            
            decimal Quantity = Convert.ToDecimal((IsOpenItem ? txtWeightInKGs : txtQuantity).EditValue);
            decimal totalPriceWT = Math.Round(Convert.ToDecimal(txtCostPriceWT.EditValue) * Quantity, 2);
            decimal totalPriceWOT = Math.Round((Convert.ToDecimal(txtCostPriceWOT.EditValue)) * Quantity, 2);
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
            decimal finaPriceWT = totalPriceWT - appliedDiscount - appliedScheme;
            decimal appliedGST = 0.0M;
            decimal cGST = 0.0M;
            decimal sGST = 0.0M;
            decimal iGST = 0.0M;
            decimal cess = 0.0M;
            if (cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
            {
                decimal finalPriceToConsider = Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE) ? finaPriceWT : finalPriceWOT;
                if (!ObjStockEntry.CalculateIGST)
                {
                    cGST = Math.Round(finalPriceToConsider * gstInfo.CGST / 100, 2);
                    sGST = Math.Round(finalPriceToConsider * gstInfo.SGST / 100, 2);
                    cess = Math.Round(finalPriceToConsider * gstInfo.CESS / 100, 2);
                    appliedGST = cGST + sGST + cess;
                }
                else
                    appliedGST = iGST = Math.Round(finalPriceToConsider * gstInfo.IGST / 100, 2);
            }

            decimal finalPrice = Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE) ? finaPriceWT : finalPriceWOT + appliedGST;

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
            if (!IsLoading)
            {
                (sender == txtDiscountPer && txtDiscountPer.EditValue != null && Convert.ToDecimal(txtDiscountPer.EditValue) > 0
                 ? txtDiscountFlat : txtDiscountPer).EditValue = 0.00;
            }

            CalculateReadOnlyFields();
        }
        private void txtSchemeValue_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                (sender == txtSchemePer && txtSchemePer.EditValue != null && Convert.ToDecimal(txtSchemePer.EditValue) > 0
                 ? txtSchemeFlat : txtSchemePer).EditValue = 0.00;
            }

            CalculateReadOnlyFields();
        }
        private void sluFreeItem_EditValueChanged(object sender, EventArgs e)
        {
            txtFreeQuantity.Enabled = sluFreeItem.EditValue != null;
        }

    }
}