using DataAccess;
using DevExpress.Office.NumberConverters;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Entity;
using ErrorManagement;
using NSRetail.Login;
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
        public frmAddStockRecord(StockEntry _ObjStockEntry, frmStockEntry _frmparent, StockEntryDetail _ObjStockEntryDetail)
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
                if (frmparent.MdiParent != null)
                    ((frmMain)frmparent.MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
                else
                    ((frmMain)frmparent.parent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;

                txtQuantity.ConfirmBarCodeScan();

                cmbItemCode.Properties.DataSource = !Utility.IsOpenCategory ?
                    Utility.GetItemCodeListFiltered() : ObjItemRep.GetParentItems(Utility.CategoryID);
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";

                cmbLookupView.GridControl.BindingContext = new BindingContext();
                cmbLookupView.GridControl.DataSource = cmbItemCode.Properties.DataSource;

                cmbGST.Properties.DataSource = Utility.GetGSTInfoList();
                cmbGST.Properties.ValueMember = "GSTID";
                cmbGST.Properties.DisplayMember = "GSTCODE";

                bool enabled = ObjStockEntry.PriceEntryMethod.Equals(2);
                txtGrossCPWOTax.Enabled = !enabled;
                txtGrossCPWTax.Enabled = enabled;

                if (int.TryParse(Convert.ToString(ObjStockEntryDetail.STOCKENTRYDETAILID), out int Ivalue) && Ivalue > 0)
                {
                    cmbItemCode.Enabled = false;
                    txtItemName.Enabled = false;
                    IsEditMode = true;
                    IsLoading = true;
                    cmbItemCode.EditValue = ObjStockEntryDetail.ITEMCODEID;
                    txtItemName.EditValue = ObjStockEntryDetail.ITEMNAME;
                    txtQuantity.EditValue = ObjStockEntryDetail.QUANTITY;
                    txtWeightInKGs.EditValue = ObjStockEntryDetail.WEIGHTINKGS;
                    txtMRP.EditValue = ObjStockEntryDetail.MRP;
                    txtSalePrice.EditValue = ObjStockEntryDetail.SALEPRICE;
                    txtGrossCPWOTax.EditValue = ObjStockEntryDetail.GROSSCOSTPRICEWOT;
                    txtGrossCPWTax.EditValue = ObjStockEntryDetail.GROSSCOSTPRICEWT;
                    txtNetCostPriceWOT.EditValue = ObjStockEntryDetail.COSTPRICEWOT;
                    txtNetCostPriceWT.EditValue = ObjStockEntryDetail.COSTPRICEWT;
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
                    txtFinalPriceWithOutTax.EditValue = ObjStockEntryDetail.FinalPriceWOTax;
                    txtFinalPrice.EditValue = ObjStockEntryDetail.FinalPrice;
                    chkFreeItem.EditValue = Convert.ToBoolean(ObjStockEntryDetail.IsFreeItem);
                    IsLoading = false;
                    ViewCostPriceList();
                }
                else
                {
                    txtDiscountFlat.EditValue = 0.00;
                    txtDiscountPer.EditValue = 0.00;
                    txtSchemePer.EditValue = 0.00;
                    txtSchemeFlat.EditValue = 0.00;
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

            cmbItemCode.EditValue = selectedItemValue;
            cmbGST.EditValue = selectedGSTValue;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;

                if ((IsOpenItem ? txtWeightInKGs.EditValue : txtQuantity.EditValue) == null ||
                        Convert.ToInt32(IsOpenItem ? txtWeightInKGs.EditValue : txtQuantity.EditValue) <= 0)
                    return;

                if (decimal.TryParse(Convert.ToString(txtMRP.EditValue), out decimal MRP) &&
                    decimal.TryParse(Convert.ToString(txtGrossCPWTax.EditValue), out decimal CostPriceWT) &&
                    decimal.TryParse(Convert.ToString(txtGrossCPWOTax.EditValue), out decimal CostPriceWOT) &&
                    decimal.TryParse(Convert.ToString(txtSalePrice.EditValue), out decimal salePrice))
                {
                    string message = string.Empty;

                    message = MRP < salePrice ? "MRP cannot be less than sale price" : message;
                    message = salePrice < CostPriceWT ? "Cost Price cannot be greater than sale price" : message;
                    if (!chkFreeItem.Checked)
                    {
                        message = CostPriceWOT == 0 ? "Cost price with out tax cannot be zero" : message;
                        message = CostPriceWT == 0 ? "Cost price with tax cannot be zero" : message;
                    }

                    if (!string.IsNullOrEmpty(message))
                    {
                        XtraMessageBox.Show(message);
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
                ObjStockEntryDetail.GROSSCOSTPRICEWT = txtGrossCPWTax.EditValue;
                ObjStockEntryDetail.GROSSCOSTPRICEWOT = txtGrossCPWOTax.EditValue;
                ObjStockEntryDetail.COSTPRICEWT = txtNetCostPriceWT.EditValue;
                ObjStockEntryDetail.COSTPRICEWOT = txtNetCostPriceWOT.EditValue;
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
                ObjStockEntryDetail.TotalPriceWT = txtTotalPriceWT.EditValue;
                ObjStockEntryDetail.TotalPriceWOT = txtTotalPriceWOT.EditValue;
                ObjStockEntryDetail.AppliedDiscount = txtAppliedDiscount.EditValue;
                ObjStockEntryDetail.AppliedScheme = txtAppliedScheme.EditValue;
                ObjStockEntryDetail.AppliedGST = txtAppliedGST.EditValue;
                ObjStockEntryDetail.FinalPriceWOTax = txtFinalPriceWithOutTax.EditValue;
                ObjStockEntryDetail.FinalPrice = txtFinalPrice.EditValue;
                ObjStockEntryDetail.CGST = txtCGST.EditValue;
                ObjStockEntryDetail.SGST = txtSGST.EditValue;
                ObjStockEntryDetail.IGST = txtIGST.EditValue;
                ObjStockEntryDetail.CESS = txtCESS.EditValue;
                ObjStockEntryDetail.HSNCODE = txtHSNCode.EditValue;
                ObjStockEntryDetail.IsFreeItem = Convert.ToBoolean(chkFreeItem.CheckState);
                ObjStockRep.SaveInvoiceDetail(ObjStockEntryDetail);
                frmparent.RefreshGrid(ObjStockEntryDetail);
                ObjStockEntryDetail.STOCKENTRYDETAILID = 0;
                cmbItemCode.EditValue = null;
                txtItemName.EditValue = null;
                txtMRP.EditValue = null;
                txtSalePrice.EditValue = null;
                txtQuantity.EditValue = null;
                txtWeightInKGs.EditValue = null;
                txtGrossCPWOTax.EditValue = null;
                txtGrossCPWTax.EditValue = null;
                txtNetCostPriceWOT.EditValue = null;
                txtNetCostPriceWT.EditValue = null;
                txtTotalPriceWT.EditValue = null;
                txtTotalPriceWOT.EditValue = null;
                cmbGST.EditValue = null;
                txtHSNCode.EditValue = null;    
                txtDiscountPer.EditValue = 0.00;
                txtDiscountFlat.EditValue = 0.00;
                txtSchemePer.EditValue = 0.00;
                txtSchemeFlat.EditValue = 0.00;
                txtCGST.EditValue = 0.00;
                txtSGST.EditValue = 0.00;
                txtIGST.EditValue = 0.00;
                txtCESS.EditValue = 0.00;
                txtAppliedGST.EditValue = 0.00;
                txtFinalPriceWithOutTax.EditValue = 0.00;
                txtFinalPrice.EditValue = 0.00;
                txtHSNCode.EditValue = null;
                IsEditMode = false;
                chkFreeItem.EditValue = false;

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
                    txtHSNCode.EditValue = cmbLookupView.GetRowCellValue(rowhandle, "HSNCODE");

                    if (int.TryParse(Convert.ToString(ObjStockEntryDetail.STOCKENTRYDETAILID), out int IValue) &&  IValue > 0)
                    {
                        return;
                    }
                    IsLoading = true;
                    txtItemName.EditValue = cmbLookupView.GetRowCellValue(rowhandle, "ITEMNAME");

                    if (IsOpenItem)
                        txtWeightInKGs.EditValue = 1;
                    else
                        txtQuantity.EditValue = 1;

                    
                    ViewCostPriceList();

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
        private void ViewCostPriceList(bool skipCPCheck = false)
        {
            //Check if in edit mode and if cost prices are not zero, skip dialog
            if (int.TryParse(Convert.ToString(ObjStockEntryDetail.STOCKENTRYDETAILID), out int detailID) &&
                detailID > 0 && !skipCPCheck)
            {
                decimal grossCPWOT = ObjStockEntryDetail.GROSSCOSTPRICEWOT != null ?
                    Convert.ToDecimal(ObjStockEntryDetail.GROSSCOSTPRICEWOT) : 0;
                decimal grossCPWT = ObjStockEntryDetail.GROSSCOSTPRICEWT != null ?
                    Convert.ToDecimal(ObjStockEntryDetail.GROSSCOSTPRICEWT) : 0;

                // If both cost prices are not zero, skip the dialog
                if (grossCPWOT != 0 || grossCPWT != 0)
                {
                    return;
                }
            }

            DataTable dtCPList = null;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                dtCPList = ObjItemRep.GetCostPriceList(cmbItemCode.EditValue);
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                throw ex;
            }

            if (dtCPList.Rows.Count == 0)
                throw new Exception("No cost prices exists under selected itemcode");

            if (dtCPList.Rows.Count > 1 || skipCPCheck)
            {
                frmCostPriceList obj = new frmCostPriceList(dtCPList);
                obj.ShowDialog();

                if (obj._IsSave)
                {
                    cmbGST.EditValue = obj.drSelected["GSTID"];
                    txtMRP.EditValue = obj.drSelected["MRP"];
                    txtSalePrice.EditValue = obj.drSelected["SALEPRICE"];
                    ItemPriceID = obj.drSelected["ITEMPRICEID"];
                    txtGrossCPWOTax.EditValue = obj.drSelected["GROSSCOSTPRICEWOT"] == DBNull.Value ? 0.00 : obj.drSelected["GROSSCOSTPRICEWOT"];
                    txtGrossCPWTax.EditValue = obj.drSelected["GROSSCOSTPRICEWT"] == DBNull.Value ? 0.00 : obj.drSelected["GROSSCOSTPRICEWT"];
                    txtNetCostPriceWOT.EditValue = obj.drSelected["COSTPRICEWOT"] == DBNull.Value ? 0.00 : obj.drSelected["COSTPRICEWOT"];
                    txtNetCostPriceWT.EditValue = obj.drSelected["COSTPRICEWT"] == DBNull.Value ? 0.00 : obj.drSelected["COSTPRICEWT"];
                }
            }
            else if (dtCPList.Rows.Count > 0)
            {
                cmbGST.EditValue = dtCPList.Rows[0]["GSTID"];
                txtMRP.EditValue = dtCPList.Rows[0]["MRP"];
                txtSalePrice.EditValue = dtCPList.Rows[0]["SALEPRICE"];
                ItemPriceID = dtCPList.Rows[0]["ITEMPRICEID"];
                txtGrossCPWOTax.EditValue = dtCPList.Rows[0]["GROSSCOSTPRICEWOT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["GROSSCOSTPRICEWOT"];
                txtGrossCPWTax.EditValue = dtCPList.Rows[0]["GROSSCOSTPRICEWT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["GROSSCOSTPRICEWT"];
                txtNetCostPriceWOT.EditValue = dtCPList.Rows[0]["COSTPRICEWOT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["COSTPRICEWOT"];
                txtNetCostPriceWT.EditValue = dtCPList.Rows[0]["COSTPRICEWT"] == DBNull.Value ? 0.00 : dtCPList.Rows[0]["COSTPRICEWT"];
            }
        }
        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var value = IsOpenItem ? txtWeightInKGs.EditValue : txtQuantity.EditValue;
                if (value == null || !decimal.TryParse(value.ToString(), out var result) || result <= 0)
                    return;

                if (!IsLoading)
                {
                    if (ObjStockEntry.PriceEntryMethod.Equals(2))
                        txtCostPriceWT_EditValueChanged(null, null);
                    else
                        txtCostPriceWOT_EditValueChanged(null, null);
                }
                else
                {
                    txtTotalPriceWOT.EditValue = txtGrossCPWOTax.EditValue;
                    txtTotalPriceWT.EditValue = txtGrossCPWTax.EditValue;
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

                if (txtGrossCPWOTax.EditValue != null && 
                    ObjStockEntry.PriceEntryMethod.Equals(1)
                    && cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
                {
                    decimal cpWOT = 0;
                    if (decimal.TryParse(Convert.ToString(txtGrossCPWOTax.EditValue), out cpWOT))
                    {
                        txtNetCostPriceWOT.EditValue = cpWOT;
                        txtGrossCPWTax.EditValue = Math.Round(cpWOT + (cpWOT * gstInfo.TAXPercent), 4);
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

                if (txtGrossCPWTax.EditValue != null 
                    && ObjStockEntry.PriceEntryMethod.Equals(2)
                    && cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
                {
                    decimal cpWT = 0;
                    if (decimal.TryParse(Convert.ToString(txtGrossCPWTax.EditValue), out cpWT))
                    {
                        txtNetCostPriceWT.EditValue = cpWT;
                        txtGrossCPWOTax.EditValue = Math.Round(cpWT  / (1 + gstInfo.TAXPercent), 4);
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

            if (ObjStockEntry.PriceEntryMethod.Equals(2))
                txtCostPriceWT_EditValueChanged(null, null);
            else
                txtCostPriceWOT_EditValueChanged(null, null);
        }
        private void CalculateReadOnlyFields()
        {
            decimal quantity = Convert.ToDecimal((IsOpenItem ? txtWeightInKGs : txtQuantity).EditValue);
            if (quantity <= 0) return;

            decimal grossCPWT = Convert.ToDecimal(txtGrossCPWTax.EditValue);
            decimal grossCPWOT = Convert.ToDecimal(txtGrossCPWOTax.EditValue);

            decimal totalPriceWT = Math.Round(grossCPWT * quantity, 2);
            decimal totalPriceWOT = Math.Round(grossCPWOT * quantity, 2);

            decimal discountPer = txtDiscountPer.EditValue != null ? Convert.ToDecimal(txtDiscountPer.EditValue) : 0;
            decimal discountFlat = (discountPer == 0 && txtDiscountFlat.EditValue != null)
                ? Convert.ToDecimal(txtDiscountFlat.EditValue) : 0;

            decimal schemePer = txtSchemePer.EditValue != null ? Convert.ToDecimal(txtSchemePer.EditValue) : 0;
            decimal schemeFlat = (schemePer == 0 && txtSchemeFlat.EditValue != null)
                ? Convert.ToDecimal(txtSchemeFlat.EditValue) : 0;

            // 👉 Calculate per-unit discount & scheme
            decimal discountPerUnit = discountFlat > 0
                ? discountFlat / quantity
                : discountPer > 0
                    ? Math.Round(grossCPWOT * (discountPer / 100), 4)
                    : 0;

            decimal schemePerUnit = schemeFlat > 0
                ? schemeFlat / quantity
                : schemePer > 0
                    ? Math.Round(grossCPWOT * (schemePer / 100), 4)
                    : 0;

            // 👉 Net CP without tax (per unit)
            decimal netCPWOT = grossCPWOT - discountPerUnit - schemePerUnit;

            decimal cGST = 0.0M, sGST = 0.0M, iGST = 0.0M, cess = 0.0M;
            decimal appliedGSTPerUnit = 0.0M;

            if (cmbGST.GetSelectedDataRow() is GSTInfo gstInfo)
            {
                if (!ObjStockEntry.CalculateIGST)
                {
                    cGST = Math.Round(netCPWOT * gstInfo.CGST / 100, 4);
                    sGST = Math.Round(netCPWOT * gstInfo.SGST / 100, 4);
                    cess = Math.Round(netCPWOT * gstInfo.CESS / 100, 4);
                    appliedGSTPerUnit = cGST + sGST + cess;
                }
                else
                {
                    iGST = Math.Round(netCPWOT * gstInfo.IGST / 100, 4);
                    cess = Math.Round(netCPWOT * gstInfo.CESS / 100, 4);
                    appliedGSTPerUnit = iGST + cess;
                }
            }

            // 👉 Net CP with tax (per unit)
            decimal netCPWT = netCPWOT + appliedGSTPerUnit;

            // 👉 Final totals
            decimal finalPriceWOTax = Math.Round(netCPWOT * quantity, 2);
            decimal finalPrice = Math.Round(netCPWT * quantity, 2);
            decimal appliedDiscount = Math.Round(discountPerUnit * quantity, 2);
            decimal appliedScheme = Math.Round(schemePerUnit * quantity, 2);
            decimal appliedGST = Math.Round(appliedGSTPerUnit * quantity, 2);

            // 👉 Assign values
            txtNetCostPriceWOT.EditValue = Math.Round(netCPWOT, 4);
            txtNetCostPriceWT.EditValue = Math.Round(netCPWT, 4);

            txtCGST.EditValue = Math.Round(cGST * quantity, 2);
            txtSGST.EditValue = Math.Round(sGST * quantity, 2);
            txtIGST.EditValue = Math.Round(iGST * quantity, 2);
            txtCESS.EditValue = Math.Round(cess * quantity, 2);

            txtTotalPriceWT.EditValue = totalPriceWT;
            txtTotalPriceWOT.EditValue = totalPriceWOT;
            txtAppliedDiscount.EditValue = appliedDiscount;
            txtAppliedScheme.EditValue = appliedScheme;
            txtAppliedGST.EditValue = appliedGST;
            txtFinalPriceWithOutTax.EditValue = finalPriceWOTax;
            txtFinalPrice.EditValue = finalPrice;
        }
        private void txtDiscountValue_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                (sender == txtDiscountPer && txtDiscountPer.EditValue != null 
                    && Convert.ToDecimal(txtDiscountPer.EditValue) > 0
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
        private void btnViewCostPrices_Click(object sender, EventArgs e)
        {
            ViewCostPriceList(true);
            if(ObjStockEntry.PriceEntryMethod.Equals(1))
                txtGrossCPWOTax.Focus();
            else
                txtGrossCPWTax.Focus();
        }
    }
}
