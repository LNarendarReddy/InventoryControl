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
    public partial class frmStockEntry : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        ItemCodeRepository ObjItemRep = new ItemCodeRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockEntry ObjStockEntry = null;
        StockEntryDetail ObjStockEntryDetail = null;
        object ItemPriceID = null;
        bool IsParentExist = false;
        bool IsOpenItem = false;
        bool IsEdit = false;
        public frmStockEntry()
        {
            InitializeComponent();
        }

        private void frmStockEntry_Load(object sender, EventArgs e)
        {
            try
            {
                ((frmMain)MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
                cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";

                DataTable dtSupplier = ObjMasterRep.GetDealer();
                cmbSupplier.Properties.DataSource = dtSupplier;
                cmbSupplier.Properties.ValueMember = "DEALERID";
                cmbSupplier.Properties.DisplayMember = "DEALERNAME";

                if (ObjStockEntry == null)
                    ObjStockEntry = new StockEntry();
                ObjStockEntry.UserID = Utility.UserID;
                ObjStockEntry.CATEGORYID = Utility.CategoryID;
                ObjStockRep.GetInvoiceDraft(ObjStockEntry);
                if (Convert.ToInt32(ObjStockEntry.STOCKENTRYID) > 0)
                {
                    cmbSupplier.EditValue = ObjStockEntry.SUPPLIERID;
                    txtInvoiceNumber.EditValue = ObjStockEntry.SUPPLIERINVOICENO;
                    chkTaxInclusive.EditValue = ObjStockEntry.TAXINCLUSIVE;
                    gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
                    cmbSupplier.Enabled = false;
                    txtInvoiceNumber.Enabled = false;
                    chkTaxInclusive.Enabled = false;
                }
                else
                {
                    ObjStockEntry.dtStockEntry = new DataTable();
                    ObjStockEntry.dtStockEntry.Columns.Add("STOCKENTRYDETAILID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODEID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMPRICEID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("SKUCODE", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODE", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMNAME", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("COSTPRICE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("MRP", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SALEPRICE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("QUANTITY", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("WEIGHTINKGS", typeof(decimal));
                    gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
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

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                int iValue = 0;
                if (int.TryParse(Convert.ToString(ObjStockEntry.STOCKENTRYID), out iValue) && iValue > 0)
                {
                    if (!dxValidationProvider1.Validate())
                        return;
                    DataTable dtInvoice = ObjStockRep.UpdateInvoice(ObjStockEntry);
                    rptInvoice rpt = new rptInvoice(dtInvoice, ObjStockEntry.dtStockEntry);
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.ShowRibbonPreview();
                    cmbSupplier.EditValue = null;
                    txtInvoiceNumber.EditValue = null;
                    cmbSupplier.Enabled = true;
                    txtInvoiceNumber.Enabled = true;
                    ObjStockEntry.STOCKENTRYID = 0;
                    ObjStockEntry.dtStockEntry = new DataTable();
                    gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
                    cmbSupplier.Focus();
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

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                ObjStockRep.DeleteInvoiceDetail(gvStockEntry.GetFocusedRowCellValue("STOCKENTRYDETAILID"));
                gvStockEntry.DeleteRow(gvStockEntry.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
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
                            txtCostPrice.EditValue = ((DataRowView)obj.drSelected)["COSTPRICE"];
                            txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                            ItemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                        }
                    }
                    else
                    {
                        txtCostPrice.EditValue = dtMRPList.Rows[0]["COSTPRICE"];
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
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
                            txtWeightInKgs.Enabled = true;
                        }
                        else
                        {
                            txtQuantity.Enabled = true;
                            txtWeightInKgs.Enabled = false;
                        }
                    }
                    else
                    {
                        txtWeightInKgs.EditValue = 0;
                        txtWeightInKgs.Enabled = false;
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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (!dxValidationProvider2.Validate())
                        return;
                    if (Convert.ToInt32(ObjStockEntry.STOCKENTRYID) == 0)
                        SaveInvoice();
                    ObjStockEntryDetail = new StockEntryDetail();
                    ObjStockEntryDetail.STOCKENTRYDETAILID = 0;
                    ObjStockEntryDetail.STOCKENTRYID = ObjStockEntry.STOCKENTRYID;
                    ObjStockEntryDetail.ITEMCODEID = cmbItemCode.EditValue;
                    ObjStockEntryDetail.COSTPRICE = txtCostPrice.EditValue;
                    ObjStockEntryDetail.MRP = txtMRP.EditValue;
                    ObjStockEntryDetail.SALEPRICE = txtSalePrice.EditValue;
                    ObjStockEntryDetail.QUANTITY = txtQuantity.EditValue;
                    ObjStockEntryDetail.WEIGHTINKGS = txtWeightInKgs.EditValue;
                    ObjStockEntryDetail.UserID = Utility.UserID;
                    ObjStockRep.SaveInvoiceDetail(ObjStockEntryDetail);
                    RefreshGrid();
                    ObjStockEntryDetail.STOCKENTRYDETAILID = 0;
                    cmbItemCode.EditValue = null;
                    txtItemName.EditValue = null;
                    txtCostPrice.EditValue = null;
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
                gvStockEntry.GridControl.BindingContext = new BindingContext();
                gvStockEntry.GridControl.DataSource = gvStockEntry.DataSource;
                int rowhandle = gvStockEntry.LocateByValue("STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                if (rowhandle >= 0)
                {
                    if (IsEdit)
                    {
                        gvStockEntry.SetRowCellValue(rowhandle, "QUANTITY", txtQuantity.EditValue);
                        gvStockEntry.SetRowCellValue(rowhandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
                    }
                    else
                    {
                        int Qnty = 0;
                        decimal WeightInKGS = 0;
                        if (int.TryParse(Convert.ToString(gvStockEntry.GetRowCellValue(rowhandle, "QUANTITY")), out Qnty))
                            gvStockEntry.SetRowCellValue(rowhandle, "QUANTITY", Qnty + Convert.ToInt32(txtQuantity.EditValue));
                        if (decimal.TryParse(Convert.ToString(gvStockEntry.GetRowCellValue(rowhandle, "WEIGHTINKGS")), out WeightInKGS))
                            gvStockEntry.SetRowCellValue(rowhandle, "WEIGHTINKGS", WeightInKGS + Convert.ToDecimal(txtWeightInKgs.EditValue));

                    }
                }
                else
                {
                    gvStockEntry.AddNewRow();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveInvoice()
        {
            try
            {
                if (ObjStockEntry == null)
                {
                    ObjStockEntry = new StockEntry();
                    ObjStockEntry.STOCKENTRYID = 0;
                }
                ObjStockEntry.SUPPLIERID = cmbSupplier.EditValue;
                ObjStockEntry.SUPPLIERINVOICENO = txtInvoiceNumber.EditValue;
                ObjStockEntry.TAXINCLUSIVE = chkTaxInclusive.EditValue;
                ObjStockEntry.CATEGORYID = Utility.CategoryID;
                ObjStockEntry.UserID = Utility.UserID;
                ObjStockRep.SaveInvoice(ObjStockEntry);
                cmbSupplier.Enabled = false;
                txtInvoiceNumber.Enabled = false;
                chkTaxInclusive.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void gvStockEntry_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, "STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                view.SetRowCellValue(e.RowHandle, "ITEMID", cmbLookupView.GetFocusedRowCellValue("ITEMID"));
                view.SetRowCellValue(e.RowHandle, "ITEMCODEID", cmbItemCode.EditValue);
                view.SetRowCellValue(e.RowHandle, "ITEMPRICEID", ObjStockEntryDetail.ITEMPRICEID);
                view.SetRowCellValue(e.RowHandle, "SKUCODE", cmbLookupView.GetFocusedRowCellValue("SKUCODE"));
                view.SetRowCellValue(e.RowHandle, "ITEMCODE", cmbItemCode.Text);
                view.SetRowCellValue(e.RowHandle, "ITEMNAME", txtItemName.EditValue);
                view.SetRowCellValue(e.RowHandle, "COSTPRICE", txtCostPrice.EditValue);
                view.SetRowCellValue(e.RowHandle, "MRP", txtMRP.EditValue);
                view.SetRowCellValue(e.RowHandle, "SALEPRICE", txtSalePrice.EditValue);
                view.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
                view.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
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

        private void gvStockEntry_DoubleClick(object sender, EventArgs e)
        {

        }

    }
}