using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Entity;
using ErrorManagement;
using NSRetail.Reports;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockEntry : XtraForm
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        ItemCodeRepository ObjItemRep = new ItemCodeRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockEntry ObjStockEntry = null;
        StockEntryDetail ObjStockEntryDetail = null;
        public frmStockEntry()
        {
            InitializeComponent();
        }

        private void frmStockEntry_Load(object sender, EventArgs e)
        {
            try
            {
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
                    dtpInvoice.EditValue = ObjStockEntry.InvoiceDate;
                    gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
                    cmbSupplier.Enabled = false;
                    txtInvoiceNumber.Enabled = false;
                    chkTaxInclusive.Enabled = false;
                    dtpInvoice.Enabled = false;
                }
                else
                {
                    dtpInvoice.EditValue = DateTime.Now;
                    ObjStockEntry.dtStockEntry = new DataTable();
                    ObjStockEntry.dtStockEntry.Columns.Add("STOCKENTRYDETAILID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODEID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMPRICEID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("SKUCODE", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODE", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMNAME", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("COSTPRICEWT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("COSTPRICEWOT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("MRP", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SALEPRICE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("QUANTITY", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("WEIGHTINKGS", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("FREEQUANTITY", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("DISCOUNTFLAT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("DISCOUNTPERCENTAGE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SCHEMEPERCENTAGE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SCHEMEFLAT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("TOTALPRICEWT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("TOTALPRICEWOT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDDISCOUNT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDSCHEME", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDDGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("FINALPRICE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("CGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("IGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("CESS", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("GSTID", typeof(int));
                    gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
                }

            }
            catch (Exception) { }
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            if (gvStockEntry.RowCount == 0) return;
            try
            {
                int iValue = 0;
                if (int.TryParse(Convert.ToString(ObjStockEntry.STOCKENTRYID), out iValue) && iValue > 0)
                {
                    if (!dxValidationProvider1.Validate() ||
                        XtraMessageBox.Show("Are you sure want to save invoice?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;

                    gvStockEntry.GridControl.BindingContext = new BindingContext();
                    gvStockEntry.GridControl.DataSource = ObjStockEntry.dtStockEntry;

                    ObjStockEntry.SumTotalPriceWT = gvStockEntry.Columns["TOTALPRICEWT"].SummaryItem.SummaryValue;
                    ObjStockEntry.SumTotalPriceWOT = gvStockEntry.Columns["TOTALPRICEWOT"].SummaryItem.SummaryValue;

                    decimal CGST =  Convert.ToDecimal(gvStockEntry.Columns["CGST"].SummaryItem.SummaryValue);
                    decimal SGST = Convert.ToDecimal(gvStockEntry.Columns["SGST"].SummaryItem.SummaryValue);
                    decimal IGST = Convert.ToDecimal(gvStockEntry.Columns["IGST"].SummaryItem.SummaryValue);
                    decimal CESS = Convert.ToDecimal(gvStockEntry.Columns["CESS"].SummaryItem.SummaryValue);

                    ObjStockEntry.SumGSTValue = CGST + SGST + IGST + CESS;
                    ObjStockEntry.SumFinalPrice = gvStockEntry.Columns["FINALPRICE"].SummaryItem.SummaryValue;

                    frmStockEntryPreview obj = new frmStockEntryPreview(ObjStockEntry);
                    obj.ShowInTaskbar = false;
                    obj.IconOptions.ShowIcon = false;
                    obj.WindowState = FormWindowState.Normal;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.ShowDialog();
                    if (ObjStockEntry.IsSave)
                    {
                        DataSet ds = ObjStockRep.GetInvoice(ObjStockEntry.STOCKENTRYID);
                        rptInvoice rpt = new rptInvoice(ds.Tables[0], ds.Tables[1]);
                        rpt.ShowPrintMarginsWarning = false;
                        rpt.ShowRibbonPreview();
                        cmbSupplier.EditValue = null;
                        txtInvoiceNumber.EditValue = null;
                        dtpInvoice.EditValue = DateTime.Now;
                        cmbSupplier.Enabled = true;
                        txtInvoiceNumber.Enabled = true;
                        dtpInvoice.Enabled = true;
                        ObjStockEntry.STOCKENTRYID = 0;
                        ObjStockEntry.dtStockEntry.Rows.Clear();
                        gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
                        cmbSupplier.Focus();
                    }
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
            if (XtraMessageBox.Show("Are you sure to delete?", "Confirm", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) 
                return;

            try
            {                
                ObjStockRep.DeleteInvoiceDetail(gvStockEntry.GetFocusedRowCellValue("STOCKENTRYDETAILID"),
                    Utility.UserID);
                gvStockEntry.DeleteRow(gvStockEntry.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvStockEntry_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, "STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                view.SetRowCellValue(e.RowHandle, "ITEMID", ObjStockEntryDetail.ITEMID);
                view.SetRowCellValue(e.RowHandle, "ITEMCODEID", ObjStockEntryDetail.ITEMCODEID);
                view.SetRowCellValue(e.RowHandle, "ITEMPRICEID", ObjStockEntryDetail.ITEMPRICEID);
                view.SetRowCellValue(e.RowHandle, "SKUCODE", ObjStockEntryDetail.SKUCODE);
                view.SetRowCellValue(e.RowHandle, "ITEMCODE", ObjStockEntryDetail.ITEMCODE);
                view.SetRowCellValue(e.RowHandle, "ITEMNAME", ObjStockEntryDetail.ITEMNAME);
                view.SetRowCellValue(e.RowHandle, "COSTPRICEWT", ObjStockEntryDetail.COSTPRICEWT);
                view.SetRowCellValue(e.RowHandle, "COSTPRICEWOT", ObjStockEntryDetail.COSTPRICEWOT);
                view.SetRowCellValue(e.RowHandle, "MRP", ObjStockEntryDetail.MRP);
                view.SetRowCellValue(e.RowHandle, "SALEPRICE", ObjStockEntryDetail.SALEPRICE);
                view.SetRowCellValue(e.RowHandle, "QUANTITY", ObjStockEntryDetail.QUANTITY);
                view.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
                view.SetRowCellValue(e.RowHandle, "FREEQUANTITY", ObjStockEntryDetail.FreeQuantity);
                view.SetRowCellValue(e.RowHandle, "DISCOUNTFLAT", ObjStockEntryDetail.DiscountFlat);
                view.SetRowCellValue(e.RowHandle, "DISCOUNTPERCENTAGE", ObjStockEntryDetail.DiscountPercentage);
                view.SetRowCellValue(e.RowHandle, "SCHEMEPERCENTAGE", ObjStockEntryDetail.SchemePercentage);
                view.SetRowCellValue(e.RowHandle, "SCHEMEFLAT", ObjStockEntryDetail.SchemeFlat);
                view.SetRowCellValue(e.RowHandle, "TOTALPRICEWT", ObjStockEntryDetail.TotalPriceWT);
                view.SetRowCellValue(e.RowHandle, "TOTALPRICEWOT", ObjStockEntryDetail.TotalPriceWOT);
                view.SetRowCellValue(e.RowHandle, "APPLIEDDISCOUNT", ObjStockEntryDetail.AppliedDiscount);
                view.SetRowCellValue(e.RowHandle, "APPLIEDSCHEME", ObjStockEntryDetail.AppliedScheme);
                view.SetRowCellValue(e.RowHandle, "APPLIEDDGST", ObjStockEntryDetail.AppliedGST);
                view.SetRowCellValue(e.RowHandle, "FINALPRICE", ObjStockEntryDetail.FinalPrice);
                view.SetRowCellValue(e.RowHandle, "CGST", ObjStockEntryDetail.CGST);
                view.SetRowCellValue(e.RowHandle, "SGST", ObjStockEntryDetail.SGST);
                view.SetRowCellValue(e.RowHandle, "IGST", ObjStockEntryDetail.IGST);
                view.SetRowCellValue(e.RowHandle, "CESS", ObjStockEntryDetail.CESS);
                view.SetRowCellValue(e.RowHandle, "GSTID", ObjStockEntryDetail.GSTID);
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.EditValue != null &&
                txtInvoiceNumber.EditValue != null &&
                dtpInvoice.EditValue != null)
            {
                ObjStockEntry.SUPPLIERID = cmbSupplier.EditValue;
                ObjStockEntry.SUPPLIERINVOICENO = txtInvoiceNumber.EditValue;
                ObjStockEntry.TAXINCLUSIVE = chkTaxInclusive.EditValue;
                ObjStockEntry.InvoiceDate = dtpInvoice.EditValue;
                ObjStockEntryDetail = new StockEntryDetail();
                frmAddStockRecord obj = new frmAddStockRecord(ObjStockEntry, this,ObjStockEntryDetail);
                obj.ShowInTaskbar = false;
                obj.StartPosition = FormStartPosition.CenterParent;
                obj.IconOptions.ShowIcon = false;
                obj.ShowDialog();
            }
        }
        
        public void RefreshGrid(StockEntryDetail _ObjStockEntryDetail)
        {
            try
            {
                ObjStockEntryDetail = _ObjStockEntryDetail;
                int rowhandle = gvStockEntry.LocateByValue("STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                if (rowhandle >= 0)
                {
                    gvStockEntry.SetRowCellValue(rowhandle, "STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMID", ObjStockEntryDetail.ITEMID);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMCODEID", ObjStockEntryDetail.ITEMCODEID);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMPRICEID", ObjStockEntryDetail.ITEMPRICEID);
                    gvStockEntry.SetRowCellValue(rowhandle, "SKUCODE", ObjStockEntryDetail.SKUCODE);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMCODE", ObjStockEntryDetail.ITEMCODE);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMNAME", ObjStockEntryDetail.ITEMNAME);
                    gvStockEntry.SetRowCellValue(rowhandle, "COSTPRICEWT", ObjStockEntryDetail.COSTPRICEWT);
                    gvStockEntry.SetRowCellValue(rowhandle, "COSTPRICEWOT", ObjStockEntryDetail.COSTPRICEWOT);
                    gvStockEntry.SetRowCellValue(rowhandle, "MRP", ObjStockEntryDetail.MRP);
                    gvStockEntry.SetRowCellValue(rowhandle, "SALEPRICE", ObjStockEntryDetail.SALEPRICE);
                    gvStockEntry.SetRowCellValue(rowhandle, "QUANTITY", ObjStockEntryDetail.QUANTITY);
                    gvStockEntry.SetRowCellValue(rowhandle, "WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
                    gvStockEntry.SetRowCellValue(rowhandle, "FREEQUANTITY", ObjStockEntryDetail.FreeQuantity);
                    gvStockEntry.SetRowCellValue(rowhandle, "DISCOUNTFLAT", ObjStockEntryDetail.DiscountFlat);
                    gvStockEntry.SetRowCellValue(rowhandle, "DISCOUNTPERCENTAGE", ObjStockEntryDetail.DiscountPercentage);
                    gvStockEntry.SetRowCellValue(rowhandle, "SCHEMEPERCENTAGE", ObjStockEntryDetail.SchemePercentage);
                    gvStockEntry.SetRowCellValue(rowhandle, "SCHEMEFLAT", ObjStockEntryDetail.SchemeFlat);
                    gvStockEntry.SetRowCellValue(rowhandle, "TOTALPRICEWT", ObjStockEntryDetail.TotalPriceWT);
                    gvStockEntry.SetRowCellValue(rowhandle, "TOTALPRICEWOT", ObjStockEntryDetail.TotalPriceWOT);
                    gvStockEntry.SetRowCellValue(rowhandle, "APPLIEDDISCOUNT", ObjStockEntryDetail.AppliedDiscount);
                    gvStockEntry.SetRowCellValue(rowhandle, "APPLIEDSCHEME", ObjStockEntryDetail.AppliedScheme);
                    gvStockEntry.SetRowCellValue(rowhandle, "APPLIEDDGST", ObjStockEntryDetail.AppliedGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "FINALPRICE", ObjStockEntryDetail.FinalPrice);
                    gvStockEntry.SetRowCellValue(rowhandle, "CGST", ObjStockEntryDetail.CGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "SGST", ObjStockEntryDetail.SGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "IGST", ObjStockEntryDetail.IGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "CESS", ObjStockEntryDetail.CESS);
                    gvStockEntry.SetRowCellValue(rowhandle, "GSTID", ObjStockEntryDetail.GSTID);
                }
                else
                    gvStockEntry.AddNewRow();

                gvStockEntry.GridControl.BindingContext = new BindingContext();
                gvStockEntry.GridControl.DataSource = gcStockEntry.DataSource;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveInvoice()
        {
            try
            {
                ObjStockEntry = ObjStockEntry ?? new StockEntry() { STOCKENTRYID = 0 };
                ObjStockEntry.SUPPLIERID = cmbSupplier.EditValue;
                ObjStockEntry.SUPPLIERINVOICENO = txtInvoiceNumber.EditValue;
                ObjStockEntry.TAXINCLUSIVE = chkTaxInclusive.EditValue;
                ObjStockEntry.InvoiceDate = dtpInvoice.EditValue;
                ObjStockEntry.CATEGORYID = Utility.CategoryID;
                ObjStockEntry.UserID = Utility.UserID;
                ObjStockRep.SaveInvoice(ObjStockEntry);
                cmbSupplier.Enabled = false;
                dtpInvoice.Enabled = false;
                txtInvoiceNumber.Enabled = false;
                chkTaxInclusive.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cmbSupplier_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupplier.EditValue != null)
                {
                    txtGSTIN.EditValue = cmbSupplier.GetColumnValue("GSTIN");
                    ObjStockEntry.CalculateIGST = !txtGSTIN.Text.StartsWith("37");
                }
                else
                    txtGSTIN.EditValue = null;
            }
            catch (Exception ex){}
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvStockEntry.FocusedRowHandle < 0)
            {
                return;
            }

            ObjStockEntry.SUPPLIERID = cmbSupplier.EditValue;
            ObjStockEntry.SUPPLIERINVOICENO = txtInvoiceNumber.EditValue;
            ObjStockEntry.TAXINCLUSIVE = chkTaxInclusive.EditValue;
            ObjStockEntry.InvoiceDate = dtpInvoice.EditValue;

            ObjStockEntryDetail = new StockEntryDetail();
            ObjStockEntryDetail.STOCKENTRYDETAILID = gvStockEntry.GetFocusedRowCellValue("STOCKENTRYDETAILID");
            ObjStockEntryDetail.ITEMID = gvStockEntry.GetFocusedRowCellValue("ITEMID");
            ObjStockEntryDetail.ITEMCODEID = gvStockEntry.GetFocusedRowCellValue("ITEMCODEID");
            ObjStockEntryDetail.ITEMPRICEID = gvStockEntry.GetFocusedRowCellValue("ITEMPRICEID");
            ObjStockEntryDetail.SKUCODE = gvStockEntry.GetFocusedRowCellValue("SKUCODE");
            ObjStockEntryDetail.ITEMCODE = gvStockEntry.GetFocusedRowCellValue("ITEMCODE");
            ObjStockEntryDetail.ITEMNAME = gvStockEntry.GetFocusedRowCellValue("ITEMNAME");
            ObjStockEntryDetail.COSTPRICEWT = gvStockEntry.GetFocusedRowCellValue("COSTPRICEWT");
            ObjStockEntryDetail.COSTPRICEWOT = gvStockEntry.GetFocusedRowCellValue("COSTPRICEWOT");
            ObjStockEntryDetail.MRP = gvStockEntry.GetFocusedRowCellValue("MRP");
            ObjStockEntryDetail.SALEPRICE = gvStockEntry.GetFocusedRowCellValue("SALEPRICE");
            ObjStockEntryDetail.QUANTITY = gvStockEntry.GetFocusedRowCellValue("QUANTITY");
            ObjStockEntryDetail.WEIGHTINKGS = gvStockEntry.GetFocusedRowCellValue("WEIGHTINKGS");
            ObjStockEntryDetail.FreeQuantity = gvStockEntry.GetFocusedRowCellValue("FREEQUANTITY");
            ObjStockEntryDetail.DiscountFlat = gvStockEntry.GetFocusedRowCellValue("DISCOUNTFLAT");
            ObjStockEntryDetail.DiscountPercentage = gvStockEntry.GetFocusedRowCellValue("DISCOUNTPERCENTAGE");
            ObjStockEntryDetail.SchemePercentage = gvStockEntry.GetFocusedRowCellValue("SCHEMEPERCENTAGE");
            ObjStockEntryDetail.SchemeFlat = gvStockEntry.GetFocusedRowCellValue("SCHEMEFLAT");
            ObjStockEntryDetail.TotalPriceWT = gvStockEntry.GetFocusedRowCellValue("TOTALPRICEWT");
            ObjStockEntryDetail.TotalPriceWOT = gvStockEntry.GetFocusedRowCellValue("TOTALPRICEWOT");
            ObjStockEntryDetail.AppliedDiscount = gvStockEntry.GetFocusedRowCellValue("APPLIEDDISCOUNT");
            ObjStockEntryDetail.AppliedScheme = gvStockEntry.GetFocusedRowCellValue("APPLIEDSCHEME");
            ObjStockEntryDetail.AppliedGST = gvStockEntry.GetFocusedRowCellValue("APPLIEDDGST");
            ObjStockEntryDetail.FinalPrice = gvStockEntry.GetFocusedRowCellValue("FINALPRICE");
            ObjStockEntryDetail.CGST = gvStockEntry.GetFocusedRowCellValue("CGST");
            ObjStockEntryDetail.SGST = gvStockEntry.GetFocusedRowCellValue("SGST");
            ObjStockEntryDetail.IGST = gvStockEntry.GetFocusedRowCellValue("IGST");
            ObjStockEntryDetail.CESS = gvStockEntry.GetFocusedRowCellValue("CESS");
            ObjStockEntryDetail.GSTID = gvStockEntry.GetFocusedRowCellValue("GSTID");
            frmAddStockRecord obj = new frmAddStockRecord(ObjStockEntry, this, ObjStockEntryDetail);
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterParent;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnDiscardInvoice_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to discard invoice?", "Confirm?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                int Ivalue = 0;
                if (int.TryParse(Convert.ToString(ObjStockEntry.STOCKENTRYID), out Ivalue) && Ivalue > 0)
                {
                    ObjStockRep.DiscardStockEntry(ObjStockEntry.STOCKENTRYID, Utility.UserID);
                    cmbSupplier.EditValue = null;
                    txtInvoiceNumber.EditValue = null;
                    dtpInvoice.EditValue = DateTime.Now;
                    ObjStockEntry.dtStockEntry = new DataTable();
                    ObjStockEntry.dtStockEntry.Columns.Add("STOCKENTRYDETAILID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODEID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMPRICEID", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("SKUCODE", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODE", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("ITEMNAME", typeof(string));
                    ObjStockEntry.dtStockEntry.Columns.Add("COSTPRICEWT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("COSTPRICEWOT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("MRP", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SALEPRICE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("QUANTITY", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("WEIGHTINKGS", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("FREEQUANTITY", typeof(int));
                    ObjStockEntry.dtStockEntry.Columns.Add("DISCOUNTFLAT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("DISCOUNTPERCENTAGE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SCHEMEPERCENTAGE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SCHEMEFLAT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("TOTALPRICEWT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("TOTALPRICEWOT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDDISCOUNT", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDSCHEME", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDDGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("FINALPRICE", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("SGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("CGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("IGST", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("CESS", typeof(decimal));
                    ObjStockEntry.dtStockEntry.Columns.Add("GSTID", typeof(int));
                    gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
                    ObjStockEntry.STOCKENTRYID = 0;
                    cmbSupplier.Enabled = true;
                    txtInvoiceNumber.Enabled = true;
                    dtpInvoice.Enabled = true;
                    chkTaxInclusive.Enabled = true;
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