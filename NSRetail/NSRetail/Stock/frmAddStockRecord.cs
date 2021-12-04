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
                ObjStockEntryDetail.ITEMNAME = txtItemName.Text;
                ObjStockEntryDetail.COSTPRICE = txtCostPrice.EditValue;
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
                txtCostPrice.EditValue = null;
                txtMRP.EditValue = null;
                txtSalePrice.EditValue = null;
                txtQuantity.EditValue = null;
                txtWeightInKGs.EditValue = null;
                txtCPWithoutTax.EditValue = null;
                txtCPWithTax.EditValue = null;
                txtTPWithTax.EditValue = null;
                txtTPWithoutTax.EditValue = null;
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
                            txtCostPrice.EditValue = ((DataRowView)obj.drSelected)["COSTPRICE"];

                        }
                    }
                    else
                    {
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                        gSTInfo.UpdateGST(dtMRPList.Rows[0]);
                        txtGSTCode.EditValue = gSTInfo.GSTCODE;
                        txtCostPrice.EditValue = dtMRPList.Rows[0]["COSTPRICE"];
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
                if (txtQuantity.EditValue != null && IsParentExist && !IsOpenItem)
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
                txtCostPrice_EditValueChanged(null, null);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void txtCostPrice_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjStockEntry.TAXINCLUSIVE))
                {
                    txtCPWithTax.EditValue = txtCostPrice.EditValue;
                    txtCPWithoutTax.EditValue =  Convert.ToDecimal(txtCostPrice.EditValue) -
                        Convert.ToDecimal(txtCostPrice.EditValue) *  gSTInfo.TAXPercent;
                    txtTPWithTax.EditValue = Convert.ToDecimal(txtCostPrice.EditValue) * Convert.ToInt32(txtQuantity.EditValue);
                    txtTPWithoutTax.EditValue = (Convert.ToDecimal(txtCostPrice.EditValue) -
                        Convert.ToDecimal(txtCostPrice.EditValue) * gSTInfo.TAXPercent)
                        * Convert.ToInt32(txtQuantity.EditValue);
                }
                else
                {
                    txtCPWithoutTax.EditValue = txtCostPrice.EditValue;
                    txtCPWithTax.EditValue = Convert.ToDecimal(txtCostPrice.EditValue) +
                        Convert.ToDecimal(txtCostPrice.EditValue) * gSTInfo.TAXPercent;
                    txtTPWithoutTax.EditValue = Convert.ToDecimal(txtCostPrice.EditValue) * Convert.ToInt32(txtQuantity.EditValue);
                    txtTPWithTax.EditValue = (Convert.ToDecimal(txtCostPrice.EditValue) +
                        Convert.ToDecimal(txtCostPrice.EditValue) * gSTInfo.TAXPercent)
                        * Convert.ToInt32(txtQuantity.EditValue);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        public class GSTInfo
        {
            public int GSTID { get; set; }
            public string GSTCODE { get; set; }
            public decimal CGST { get; set; }
            public decimal SGST { get; set; }
            public decimal IGST { get; set; }
            public decimal CESS { get; set; }
            public decimal TAXPercent { get; private set; }

            public void UpdateGST (DataRow dr)
            {
                GSTID = Convert.ToInt32(dr["GSTID"]);
                GSTCODE = Convert.ToString(dr["GSTCODE"]);
                CGST = Convert.ToDecimal(dr["CGST"]);
                SGST = Convert.ToDecimal(dr["SGST"]);
                IGST = Convert.ToDecimal(dr["IGST"]);
                CESS = Convert.ToDecimal(dr["CESS"]);
                TAXPercent = (CGST + SGST + IGST + CESS) / 100;

            }
            public void UpdateGST(DataRowView dr)
            {
                UpdateGST(dr.Row);
            }
        }
    }
}