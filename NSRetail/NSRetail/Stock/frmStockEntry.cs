using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Entity;
using ErrorManagement;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace NSRetail.Stock
{
    public partial class frmStockEntry : XtraForm
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        StockRepository ObjStockRep = new StockRepository();
        StockEntry ObjStockEntry = null;
        StockEntryDetail ObjStockEntryDetail = null;
        DataTable dtSupplierIndentItems = null;
        public XtraForm parent = null;

        public frmStockEntry()
        {
            InitializeComponent();
        }

        public frmStockEntry(StockEntry _ObjStockEntry, XtraForm _parent)
        {
            InitializeComponent();
            ObjStockEntry = _ObjStockEntry;
            this.parent = _parent;
        }

        private void frmStockEntry_Load(object sender, EventArgs e)
        {
            try
            {
                if (ObjStockEntry == null)
                    ObjStockEntry = new StockEntry();
                ObjStockEntry.UserID = Utility.UserID;
                ObjStockEntry.CATEGORYID = Utility.CategoryID;
                ObjStockRep.GetInvoiceDraft(ObjStockEntry);

                if (Convert.ToInt32(ObjStockEntry.STOCKENTRYID) > 0)
                {
                    if (!ViewInvoiceSettings())
                    {
                        BeginInvoke(new Action(Close));
                        return;
                    }

                    SaveInvoice();
                    LoadObject();
                }
                else
                {
                    if (!RefreshObject())
                    {
                        BeginInvoke(new Action(Close));
                        return;
                    }

                    SaveInvoice();
                }
                if (gvStockEntry.Columns["STOCKENTRYDETAILID"] != null)
                    gvStockEntry.Columns["STOCKENTRYDETAILID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
            catch (Exception ex) 
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private bool ViewInvoiceSettings()
        {
            frmInvoiceSettings objSettings = new frmInvoiceSettings(ObjStockEntry);
            objSettings.StartPosition = FormStartPosition.CenterScreen;
            return objSettings.ShowDialog() == DialogResult.OK;
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (!EnsureInvoiceHeader())
                    return;

                if (gvStockEntry.RowCount == 0)
                {
                    return;
                }

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

                    ObjStockEntry.SumTaxValue = CGST + SGST + IGST;
                    ObjStockEntry.SumCessValue = CESS;

                    ObjStockEntry.SumGSTValue = CGST + SGST + IGST + CESS;
                    ObjStockEntry.SumFinalPrice = gvStockEntry.Columns["FINALPRICE"].SummaryItem.SummaryValue;

                    frmStockEntryPreview obj = new frmStockEntryPreview(ObjStockEntry)
                    {
                        ShowInTaskbar = false
                    };
                    obj.IconOptions.ShowIcon = false;
                    obj.WindowState = FormWindowState.Normal;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.ShowDialog();
                    if (ObjStockEntry.IsSave)
                    {
                        int seid = Convert.ToInt32(ObjStockEntry.STOCKENTRYID);
                        DataSet ds = ObjStockRep.GetInvoice(seid);
                        rptInvoice rpt = new rptInvoice(ds.Tables[0], ds.Tables[1]);
                        rpt.ShowPrintMarginsWarning = false;
                        rpt.ShowRibbonPreview();
                        ObjStockEntry = new StockEntry()
                        {
                            UserID = Utility.UserID,
                            CATEGORYID = Utility.CategoryID,
                            SourceBranchID = Utility.BranchID
                        };
                        InitializeStockEntryTable();
                        UpdateFormTitle();
                        btnAddItem.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
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
                ObjStockRep.DeleteInvoiceDetail(gvStockEntry.GetFocusedRowCellValue("STOCKENTRYDETAILID"), Utility.UserID);
                gvStockEntry.DeleteRow(gvStockEntry.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void gvStockEntry_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, "STOCKENTRYID", ObjStockEntryDetail.STOCKENTRYID);
                view.SetRowCellValue(e.RowHandle, "STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                view.SetRowCellValue(e.RowHandle, "ITEMID", ObjStockEntryDetail.ITEMID);
                view.SetRowCellValue(e.RowHandle, "ITEMCODEID", ObjStockEntryDetail.ITEMCODEID);
                view.SetRowCellValue(e.RowHandle, "SKUCODE", ObjStockEntryDetail.SKUCODE);
                view.SetRowCellValue(e.RowHandle, "ITEMCODE", ObjStockEntryDetail.ITEMCODE);
                view.SetRowCellValue(e.RowHandle, "ITEMNAME", ObjStockEntryDetail.ITEMNAME);
                view.SetRowCellValue(e.RowHandle, "CPWITHTAX", ObjStockEntryDetail.COSTPRICEWT);
                view.SetRowCellValue(e.RowHandle, "CPWITHOUTTAX", ObjStockEntryDetail.COSTPRICEWOT);
                view.SetRowCellValue(e.RowHandle, "INVOICECPWITHTAX", ObjStockEntryDetail.GROSSCOSTPRICEWT);
                view.SetRowCellValue(e.RowHandle, "INVOICECPWITHOUTTAX", ObjStockEntryDetail.GROSSCOSTPRICEWOT);
                view.SetRowCellValue(e.RowHandle, "MRP", ObjStockEntryDetail.MRP);
                view.SetRowCellValue(e.RowHandle, "SALEPRICE", ObjStockEntryDetail.SALEPRICE);
                view.SetRowCellValue(e.RowHandle, "QUANTITY", ObjStockEntryDetail.QUANTITY);
                view.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
                view.SetRowCellValue(e.RowHandle, "DISCOUNTFLAT", ObjStockEntryDetail.DiscountFlat);
                view.SetRowCellValue(e.RowHandle, "DISCOUNTPERCENTAGE", ObjStockEntryDetail.DiscountPercentage);
                view.SetRowCellValue(e.RowHandle, "SCHEMEPERCENTAGE", ObjStockEntryDetail.SchemePercentage);
                view.SetRowCellValue(e.RowHandle, "SCHEMEFLAT", ObjStockEntryDetail.SchemeFlat);
                view.SetRowCellValue(e.RowHandle, "TOTALPRICEWT", ObjStockEntryDetail.TotalPriceWT);
                view.SetRowCellValue(e.RowHandle, "TOTALPRICEWOT", ObjStockEntryDetail.TotalPriceWOT);
                view.SetRowCellValue(e.RowHandle, "APPLIEDDISCOUNT", ObjStockEntryDetail.AppliedDiscount);
                view.SetRowCellValue(e.RowHandle, "APPLIEDSCHEME", ObjStockEntryDetail.AppliedScheme);
                view.SetRowCellValue(e.RowHandle, "APPLIEDDGST", ObjStockEntryDetail.AppliedGST);
                view.SetRowCellValue(e.RowHandle, "FINALPRICEWOTAX", ObjStockEntryDetail.FinalPriceWOTax);
                view.SetRowCellValue(e.RowHandle, "FINALPRICE", ObjStockEntryDetail.FinalPrice);
                view.SetRowCellValue(e.RowHandle, "CGST", ObjStockEntryDetail.CGST);
                view.SetRowCellValue(e.RowHandle, "SGST", ObjStockEntryDetail.SGST);
                view.SetRowCellValue(e.RowHandle, "IGST", ObjStockEntryDetail.IGST);
                view.SetRowCellValue(e.RowHandle, "CESS", ObjStockEntryDetail.CESS);
                view.SetRowCellValue(e.RowHandle, "GSTID", ObjStockEntryDetail.GSTID);
                view.SetRowCellValue(e.RowHandle, "HSNCODE", ObjStockEntryDetail.HSNCODE);
                view.SetRowCellValue(e.RowHandle, "GSTCODE", ObjStockEntryDetail.GSTCODE);
                view.SetRowCellValue(e.RowHandle, "INDENTQUANTITY", ObjStockEntryDetail.IndentQuantity);
                view.SetRowCellValue(e.RowHandle, "ISFREEITEM", ObjStockEntryDetail.IsFreeItem);
                view.UpdateCurrentRow();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void gvStockEntry_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (EnsureInvoiceHeader())
            {
                ObjStockEntryDetail = new StockEntryDetail();
                new frmAddStockRecord(ObjStockEntry, this,ObjStockEntryDetail).ShowDialog();
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
                    gvStockEntry.SetRowCellValue(rowhandle, "STOCKENTRYID", ObjStockEntryDetail.STOCKENTRYID);
                    gvStockEntry.SetRowCellValue(rowhandle, "STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMID", ObjStockEntryDetail.ITEMID);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMCODEID", ObjStockEntryDetail.ITEMCODEID);
                    gvStockEntry.SetRowCellValue(rowhandle, "SKUCODE", ObjStockEntryDetail.SKUCODE);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMCODE", ObjStockEntryDetail.ITEMCODE);
                    gvStockEntry.SetRowCellValue(rowhandle, "ITEMNAME", ObjStockEntryDetail.ITEMNAME);
                    gvStockEntry.SetRowCellValue(rowhandle, "CPWITHTAX", ObjStockEntryDetail.COSTPRICEWT);
                    gvStockEntry.SetRowCellValue(rowhandle, "CPWITHOUTTAX", ObjStockEntryDetail.COSTPRICEWOT);
                    gvStockEntry.SetRowCellValue(rowhandle, "INVOICECPWITHTAX", ObjStockEntryDetail.GROSSCOSTPRICEWT);
                    gvStockEntry.SetRowCellValue(rowhandle, "INVOICECPWITHOUTTAX", ObjStockEntryDetail.GROSSCOSTPRICEWOT);
                    gvStockEntry.SetRowCellValue(rowhandle, "MRP", ObjStockEntryDetail.MRP);
                    gvStockEntry.SetRowCellValue(rowhandle, "SALEPRICE", ObjStockEntryDetail.SALEPRICE);
                    gvStockEntry.SetRowCellValue(rowhandle, "QUANTITY", ObjStockEntryDetail.QUANTITY);
                    gvStockEntry.SetRowCellValue(rowhandle, "WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
                    gvStockEntry.SetRowCellValue(rowhandle, "DISCOUNTFLAT", ObjStockEntryDetail.DiscountFlat);
                    gvStockEntry.SetRowCellValue(rowhandle, "DISCOUNTPERCENTAGE", ObjStockEntryDetail.DiscountPercentage);
                    gvStockEntry.SetRowCellValue(rowhandle, "SCHEMEFLAT", ObjStockEntryDetail.SchemeFlat);
                    gvStockEntry.SetRowCellValue(rowhandle, "SCHEMEPERCENTAGE", ObjStockEntryDetail.SchemePercentage);
                    gvStockEntry.SetRowCellValue(rowhandle, "TOTALPRICEWT", ObjStockEntryDetail.TotalPriceWT);
                    gvStockEntry.SetRowCellValue(rowhandle, "TOTALPRICEWOT", ObjStockEntryDetail.TotalPriceWOT);
                    gvStockEntry.SetRowCellValue(rowhandle, "APPLIEDDISCOUNT", ObjStockEntryDetail.AppliedDiscount);
                    gvStockEntry.SetRowCellValue(rowhandle, "APPLIEDSCHEME", ObjStockEntryDetail.AppliedScheme);
                    gvStockEntry.SetRowCellValue(rowhandle, "GSTID", ObjStockEntryDetail.GSTID);
                    gvStockEntry.SetRowCellValue(rowhandle, "APPLIEDDGST", ObjStockEntryDetail.AppliedGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "FINALPRICEWOTAX", ObjStockEntryDetail.FinalPriceWOTax);
                    gvStockEntry.SetRowCellValue(rowhandle, "FINALPRICE", ObjStockEntryDetail.FinalPrice);
                    gvStockEntry.SetRowCellValue(rowhandle, "CGST", ObjStockEntryDetail.CGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "SGST", ObjStockEntryDetail.SGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "IGST", ObjStockEntryDetail.IGST);
                    gvStockEntry.SetRowCellValue(rowhandle, "CESS", ObjStockEntryDetail.CESS);
                gvStockEntry.SetRowCellValue(rowhandle, "HSNCODE", ObjStockEntryDetail.HSNCODE);
                gvStockEntry.SetRowCellValue(rowhandle, "GSTCODE", ObjStockEntryDetail.GSTCODE);
                gvStockEntry.SetRowCellValue(rowhandle, "INDENTQUANTITY", ObjStockEntryDetail.IndentQuantity);
                gvStockEntry.SetRowCellValue(rowhandle, "ISFREEITEM", ObjStockEntryDetail.IsFreeItem);
                    gvStockEntry.SetRowCellValue(rowhandle, "CREATEDBY", ObjStockEntryDetail.CreatedBy);
                    gvStockEntry.SetRowCellValue(rowhandle, "CREATEDDATE", ObjStockEntryDetail.CreatedDate);
                    gvStockEntry.FocusedRowHandle = rowhandle;
                }
                else
                {
                    gvStockEntry.AddNewRow();
                }
                gvStockEntry.UpdateCurrentRow();
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
                ObjStockEntry.CATEGORYID = IsNullValue(ObjStockEntry.CATEGORYID) ? Utility.CategoryID : ObjStockEntry.CATEGORYID;
                ObjStockEntry.UserID = Utility.UserID;
                ObjStockEntry.SourceBranchID = IsNullValue(ObjStockEntry.SourceBranchID) ? Utility.BranchID : ObjStockEntry.SourceBranchID;
                ObjStockRep.SaveInvoice(ObjStockEntry);
                LoadSupplierIndentItems();
                UpdateFormTitle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvStockEntry.FocusedRowHandle < 0)
            {
                return;
            }

            ObjStockEntryDetail = new StockEntryDetail();
            ObjStockEntryDetail.STOCKENTRYDETAILID = gvStockEntry.GetFocusedRowCellValue("STOCKENTRYDETAILID");
            ObjStockEntryDetail.ITEMID = gvStockEntry.GetFocusedRowCellValue("ITEMID");
            ObjStockEntryDetail.ITEMCODEID = gvStockEntry.GetFocusedRowCellValue("ITEMCODEID");
            ObjStockEntryDetail.SKUCODE = gvStockEntry.GetFocusedRowCellValue("SKUCODE");
            ObjStockEntryDetail.ITEMCODE = gvStockEntry.GetFocusedRowCellValue("ITEMCODE");
            ObjStockEntryDetail.ITEMNAME = gvStockEntry.GetFocusedRowCellValue("ITEMNAME");
            ObjStockEntryDetail.COSTPRICEWT = gvStockEntry.GetFocusedRowCellValue("CPWITHTAX");
            ObjStockEntryDetail.COSTPRICEWOT = gvStockEntry.GetFocusedRowCellValue("CPWITHOUTTAX");
            ObjStockEntryDetail.GROSSCOSTPRICEWT = gvStockEntry.GetFocusedRowCellValue("INVOICECPWITHTAX");
            ObjStockEntryDetail.GROSSCOSTPRICEWOT = gvStockEntry.GetFocusedRowCellValue("INVOICECPWITHOUTTAX");
            ObjStockEntryDetail.MRP = gvStockEntry.GetFocusedRowCellValue("MRP");
            ObjStockEntryDetail.SALEPRICE = gvStockEntry.GetFocusedRowCellValue("SALEPRICE");
            ObjStockEntryDetail.QUANTITY = gvStockEntry.GetFocusedRowCellValue("QUANTITY");
            ObjStockEntryDetail.WEIGHTINKGS = gvStockEntry.GetFocusedRowCellValue("WEIGHTINKGS");
            ObjStockEntryDetail.DiscountFlat = gvStockEntry.GetFocusedRowCellValue("DISCOUNTFLAT");
            ObjStockEntryDetail.DiscountPercentage = gvStockEntry.GetFocusedRowCellValue("DISCOUNTPERCENTAGE");
            ObjStockEntryDetail.SchemePercentage = gvStockEntry.GetFocusedRowCellValue("SCHEMEPERCENTAGE");
            ObjStockEntryDetail.SchemeFlat = gvStockEntry.GetFocusedRowCellValue("SCHEMEFLAT");
            ObjStockEntryDetail.TotalPriceWT = gvStockEntry.GetFocusedRowCellValue("TOTALPRICEWT");
            ObjStockEntryDetail.TotalPriceWOT = gvStockEntry.GetFocusedRowCellValue("TOTALPRICEWOT");
            ObjStockEntryDetail.AppliedDiscount = gvStockEntry.GetFocusedRowCellValue("APPLIEDDISCOUNT");
            ObjStockEntryDetail.AppliedScheme = gvStockEntry.GetFocusedRowCellValue("APPLIEDSCHEME");
            ObjStockEntryDetail.AppliedGST = gvStockEntry.GetFocusedRowCellValue("APPLIEDDGST");
            ObjStockEntryDetail.FinalPriceWOTax = gvStockEntry.GetFocusedRowCellValue("FINALPRICEWOTAX");
            ObjStockEntryDetail.FinalPrice = gvStockEntry.GetFocusedRowCellValue("FINALPRICE");
            ObjStockEntryDetail.CGST = gvStockEntry.GetFocusedRowCellValue("CGST");
            ObjStockEntryDetail.SGST = gvStockEntry.GetFocusedRowCellValue("SGST");
            ObjStockEntryDetail.IGST = gvStockEntry.GetFocusedRowCellValue("IGST");
            ObjStockEntryDetail.CESS = gvStockEntry.GetFocusedRowCellValue("CESS");
            ObjStockEntryDetail.GSTID = gvStockEntry.GetFocusedRowCellValue("GSTID");
            ObjStockEntryDetail.HSNCODE = gvStockEntry.GetFocusedRowCellValue("HSNCODE");
            ObjStockEntryDetail.GSTCODE = gvStockEntry.GetFocusedRowCellValue("GSTCODE");
            ObjStockEntryDetail.IndentQuantity = gvStockEntry.Columns["INDENTQUANTITY"] != null ? gvStockEntry.GetFocusedRowCellValue("INDENTQUANTITY") : null;
            ObjStockEntryDetail.IsFreeItem = Convert.ToBoolean(gvStockEntry.GetFocusedRowCellValue("ISFREEITEM"));
            new frmAddStockRecord(ObjStockEntry, this, ObjStockEntryDetail).ShowDialog();
        }

        private void btnDiscardInvoice_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to discard invoice?", "Confirm?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                if (int.TryParse(Convert.ToString(ObjStockEntry.STOCKENTRYID), out int Ivalue) && Ivalue > 0)
                {
                    ObjStockRep.DiscardStockEntry(ObjStockEntry.STOCKENTRYID, Utility.UserID);
                    XtraMessageBox.Show("Invoice successfully discarded?", "Success?", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void gvStockEntry_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void btnDraftInvoice_Click(object sender, EventArgs e)
        {
            if(int.TryParse(Convert.ToString(ObjStockEntry.STOCKENTRYID), out int Ivalue) 
                && Ivalue > 0 && XtraMessageBox.Show("Are you sure to draft the current invoice?", "Draft invoice confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                RefreshObject();
            }
        }

        private void btnLoadDraft_Click(object sender, EventArgs e)
        {
            frmDraftStockEntries stockEntries = new frmDraftStockEntries();
            if (stockEntries.ShowDialog() == DialogResult.Yes)
            {
                ObjStockEntry = new StockEntry() { STOCKENTRYID = stockEntries.StockEntryID, CATEGORYID = Utility.CategoryID, UserID = Utility.UserID };
                ObjStockRep.GetInvoiceDraft(ObjStockEntry);
                if (!ViewInvoiceSettings())
                    return;

                SaveInvoice();
                LoadObject();
            }
        }

        private void btnViewIndentItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (!EnsureInvoiceHeader())
                    return;

                if (!HasSupplierIndent())
                {
                    XtraMessageBox.Show("Supplier indent is not selected for this invoice");
                    return;
                }

                if (dtSupplierIndentItems == null)
                    LoadSupplierIndentItems();

                if (dtSupplierIndentItems == null || dtSupplierIndentItems.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No indent items found");
                    return;
                }

                new frmSupplierIndentItems(BuildSupplierIndentStatusTable()).ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private bool RefreshObject()
        {
            ObjStockEntry = new StockEntry();
            ObjStockEntry.UserID = Utility.UserID;
            ObjStockEntry.CATEGORYID = Utility.CategoryID;
            ObjStockEntry.SourceBranchID = Utility.BranchID;
            if (!ViewInvoiceSettings())
                return false;

            InitializeStockEntryTable();
            UpdateFormTitle();
            ObjStockEntry.STOCKENTRYID = 0;
            return true;
        }

        private void InitializeStockEntryTable()
        {
            ObjStockEntry.dtStockEntry = new DataTable();
            ObjStockEntry.dtStockEntry.Columns.Add("STOCKENTRYID", typeof(int));
            ObjStockEntry.dtStockEntry.Columns.Add("STOCKENTRYDETAILID", typeof(int));
            ObjStockEntry.dtStockEntry.Columns.Add("ITEMID", typeof(int));
            ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODEID", typeof(int));
            ObjStockEntry.dtStockEntry.Columns.Add("SKUCODE", typeof(string));
            ObjStockEntry.dtStockEntry.Columns.Add("ITEMCODE", typeof(string));
            ObjStockEntry.dtStockEntry.Columns.Add("ITEMNAME", typeof(string));
            ObjStockEntry.dtStockEntry.Columns.Add("CPWITHTAX", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("CPWITHOUTTAX", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("INVOICECPWITHTAX", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("INVOICECPWITHOUTTAX", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("MRP", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("SALEPRICE", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("QUANTITY", typeof(int));
            ObjStockEntry.dtStockEntry.Columns.Add("WEIGHTINKGS", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("DISCOUNTFLAT", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("DISCOUNTPERCENTAGE", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("SCHEMEPERCENTAGE", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("SCHEMEFLAT", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("TOTALPRICEWT", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("TOTALPRICEWOT", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDDISCOUNT", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDSCHEME", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("GSTID", typeof(int));
            ObjStockEntry.dtStockEntry.Columns.Add("APPLIEDDGST", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("FINALPRICEWOTAX", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("FINALPRICE", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("CGST", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("SGST", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("IGST", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("CESS", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("HSNCODE", typeof(string));
            ObjStockEntry.dtStockEntry.Columns.Add("GSTCODE", typeof(string));
            ObjStockEntry.dtStockEntry.Columns.Add("INDENTQUANTITY", typeof(decimal));
            ObjStockEntry.dtStockEntry.Columns.Add("ISFREEITEM", typeof(bool));
            ObjStockEntry.dtStockEntry.Columns.Add("CREATEDBY", typeof(string));
            ObjStockEntry.dtStockEntry.Columns.Add("CREATEDDATE", typeof(DateTime));
            gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
            gvStockEntry.BestFitColumns();
        }

        private void LoadObject()
        {
            gcStockEntry.DataSource = ObjStockEntry.dtStockEntry;
            gvStockEntry.BestFitColumns();
            UpdateFormTitle();
        }

        private bool EnsureInvoiceHeader()
        {
            if (IsNullValue(ObjStockEntry?.SUPPLIERID) ||
                IsNullValue(ObjStockEntry?.SUPPLIERINVOICENO) ||
                IsNullValue(ObjStockEntry?.InvoiceDate) ||
                IsNullValue(ObjStockEntry?.CATEGORYID))
            {
                if (!ViewInvoiceSettings())
                    return false;

                SaveInvoice();
            }

            UpdateFormTitle();
            return true;
        }

        private void LoadSupplierIndentItems()
        {
            dtSupplierIndentItems = null;

            if (!HasSupplierIndent())
            {
                btnViewIndentItems.Enabled = false;
                return;
            }

            DataSet dsSupplierIndent = new ReportRepository().GetSupplierIndentDetail(ObjStockEntry.SupplierIndentId, true);
            dtSupplierIndentItems = dsSupplierIndent.Tables.Count > 0 ? dsSupplierIndent.Tables[0].Copy() : new DataTable();
            btnViewIndentItems.Enabled = dtSupplierIndentItems.Rows.Count > 0;
        }

        private DataTable BuildSupplierIndentStatusTable()
        {
            DataTable dtStatus = dtSupplierIndentItems.Copy();
            if (!dtStatus.Columns.Contains("ENTEREDQUANTITY"))
                dtStatus.Columns.Add("ENTEREDQUANTITY", typeof(decimal));
            if (!dtStatus.Columns.Contains("STATUS"))
                dtStatus.Columns.Add("STATUS", typeof(string));

            foreach (DataRow drIndentItem in dtStatus.Rows)
            {
                decimal indentQuantity = GetDecimalValue(drIndentItem, "INDENTQUANTITY", "REQUIREDITEMINDENT", "DESIREDINDENT", "DESIREDITEMINDENT", "CALCULATEDITEMINDENT");
                decimal enteredQuantity = GetEnteredStockEntryQuantity(drIndentItem);

                drIndentItem["ENTEREDQUANTITY"] = enteredQuantity;
                drIndentItem["STATUS"] = enteredQuantity <= 0
                    ? "Pending"
                    : enteredQuantity < indentQuantity
                        ? "Short"
                        : enteredQuantity == indentQuantity
                            ? "Matched"
                            : "Excess";
            }

            AddStockEntryItemsOutsideSupplierIndent(dtStatus);
            return dtStatus;
        }

        public bool ValidateStockEntryDetailAgainstSupplierIndent(StockEntryDetail stockEntryDetail, out string validationMessage)
        {
            validationMessage = string.Empty;
            if (!HasSupplierIndent())
                return true;

            if (dtSupplierIndentItems == null)
                LoadSupplierIndentItems();

            DataRow drIndentItem = GetMatchingSupplierIndentItem(stockEntryDetail);
            if (drIndentItem == null)
            {
                validationMessage = $"Selected item does not exist in supplier indent.{Environment.NewLine}{stockEntryDetail.ITEMNAME}";
                return false;
            }

            stockEntryDetail.IndentQuantity = GetDecimalValue(drIndentItem, "INDENTQUANTITY", "REQUIREDITEMINDENT", "DESIREDINDENT", "DESIREDITEMINDENT", "CALCULATEDITEMINDENT");

            return true;
        }

        private decimal GetEnteredStockEntryQuantity(DataRow drIndentItem, object excludeStockEntryDetailID = null)
        {
            if (ObjStockEntry?.dtStockEntry == null)
                return 0;

            decimal enteredQuantity = 0;
            foreach (DataRow drStockEntry in ObjStockEntry.dtStockEntry.Rows)
            {
                if (drStockEntry.RowState == DataRowState.Deleted)
                    continue;

                object stockEntryDetailID = GetColumnValue(drStockEntry, "STOCKENTRYDETAILID");
                if (!IsNullValue(excludeStockEntryDetailID) && Convert.ToString(stockEntryDetailID) == Convert.ToString(excludeStockEntryDetailID))
                    continue;

                if (IsMatchingSupplierIndentItem(drIndentItem, drStockEntry))
                    enteredQuantity += GetStockEntryQuantity(drStockEntry);
            }

            return enteredQuantity;
        }

        private decimal GetStockEntryQuantity(DataRow drStockEntry)
        {
            decimal quantity = GetDecimalValue(drStockEntry, "QUANTITY");
            return quantity > 0 ? quantity : GetDecimalValue(drStockEntry, "WEIGHTINKGS");
        }

        private bool HasStockEntryItemsOutsideSupplierIndent()
        {
            if (ObjStockEntry?.dtStockEntry == null)
                return false;

            foreach (DataRow drStockEntry in ObjStockEntry.dtStockEntry.Rows)
            {
                if (drStockEntry.RowState == DataRowState.Deleted)
                    continue;

                if (GetMatchingSupplierIndentItem(drStockEntry) == null)
                    return true;
            }

            return false;
        }

        private DataRow GetMatchingSupplierIndentItem(DataRow drStockEntry)
        {
            if (dtSupplierIndentItems == null)
                return null;

            foreach (DataRow drIndentItem in dtSupplierIndentItems.Rows)
            {
                if (IsMatchingSupplierIndentItem(drIndentItem, drStockEntry))
                    return drIndentItem;
            }

            return null;
        }

        private DataRow GetMatchingSupplierIndentItem(StockEntryDetail stockEntryDetail)
        {
            if (dtSupplierIndentItems == null)
                return null;

            foreach (DataRow drIndentItem in dtSupplierIndentItems.Rows)
            {
                if (IsMatchingSupplierIndentItem(drIndentItem, stockEntryDetail))
                    return drIndentItem;
            }

            return null;
        }

        private bool IsMatchingSupplierIndentItem(DataRow drIndentItem, DataRow drStockEntry)
        {
            string indentItemCodeID = NormalizeKey(GetColumnValue(drIndentItem, "ITEMCODEID"));
            string stockItemCodeID = NormalizeKey(GetColumnValue(drStockEntry, "ITEMCODEID"));
            if (!string.IsNullOrEmpty(indentItemCodeID) && !string.IsNullOrEmpty(stockItemCodeID))
                return indentItemCodeID == stockItemCodeID;

            string indentSKUCode = NormalizeKey(GetColumnValue(drIndentItem, "SKUCODE"));
            string stockSKUCode = NormalizeKey(GetColumnValue(drStockEntry, "SKUCODE"));
            if (!string.IsNullOrEmpty(indentSKUCode) && !string.IsNullOrEmpty(stockSKUCode))
                return indentSKUCode == stockSKUCode;

            string indentItemID = NormalizeKey(GetColumnValue(drIndentItem, "ITEMID"));
            string stockItemID = NormalizeKey(GetColumnValue(drStockEntry, "ITEMID"));
            return !string.IsNullOrEmpty(indentItemID) &&
                !string.IsNullOrEmpty(stockItemID) &&
                indentItemID == stockItemID;
        }

        private bool IsMatchingSupplierIndentItem(DataRow drIndentItem, StockEntryDetail stockEntryDetail)
        {
            string indentItemCodeID = NormalizeKey(GetColumnValue(drIndentItem, "ITEMCODEID"));
            string stockItemCodeID = NormalizeKey(stockEntryDetail.ITEMCODEID);
            if (!string.IsNullOrEmpty(indentItemCodeID) && !string.IsNullOrEmpty(stockItemCodeID))
                return indentItemCodeID == stockItemCodeID;

            string indentSKUCode = NormalizeKey(GetColumnValue(drIndentItem, "SKUCODE"));
            string stockSKUCode = NormalizeKey(stockEntryDetail.SKUCODE);
            if (!string.IsNullOrEmpty(indentSKUCode) && !string.IsNullOrEmpty(stockSKUCode))
                return indentSKUCode == stockSKUCode;

            string indentItemID = NormalizeKey(GetColumnValue(drIndentItem, "ITEMID"));
            string stockItemID = NormalizeKey(stockEntryDetail.ITEMID);
            return !string.IsNullOrEmpty(indentItemID) &&
                !string.IsNullOrEmpty(stockItemID) &&
                indentItemID == stockItemID;
        }

        private void AddStockEntryItemsOutsideSupplierIndent(DataTable dtStatus)
        {
            if (ObjStockEntry?.dtStockEntry == null)
                return;

            int sno = dtStatus.Rows.Count;
            foreach (DataRow drStockEntry in ObjStockEntry.dtStockEntry.Rows)
            {
                if (drStockEntry.RowState == DataRowState.Deleted || GetMatchingSupplierIndentItem(drStockEntry) != null)
                    continue;

                DataRow drExtraItem = dtStatus.NewRow();
                SetColumnValue(drExtraItem, "SNO", ++sno);
                SetColumnValue(drExtraItem, "ITEMID", GetColumnValue(drStockEntry, "ITEMID"));
                SetColumnValue(drExtraItem, "ITEMCODEID", GetColumnValue(drStockEntry, "ITEMCODEID"));
                SetColumnValue(drExtraItem, "SKUCODE", GetColumnValue(drStockEntry, "SKUCODE"));
                SetColumnValue(drExtraItem, "ITEMNAME", GetColumnValue(drStockEntry, "ITEMNAME"));
                SetColumnValue(drExtraItem, "MRP", GetColumnValue(drStockEntry, "MRP"));
                SetColumnValue(drExtraItem, "COSTPRICEWT", GetColumnValue(drStockEntry, "CPWITHTAX"));
                SetColumnValue(drExtraItem, "DESIREDINDENT", 0);
                SetColumnValue(drExtraItem, "ENTEREDQUANTITY", GetStockEntryQuantity(drStockEntry));
                SetColumnValue(drExtraItem, "STATUS", "Not in Indent");
                dtStatus.Rows.Add(drExtraItem);
            }
        }

        private void SetColumnValue(DataRow row, string columnName, object value)
        {
            if (row.Table.Columns.Contains(columnName))
                row[columnName] = IsNullValue(value) ? DBNull.Value : value;
        }

        private string NormalizeKey(object value)
        {
            if (IsNullValue(value))
                return string.Empty;

            return Convert.ToString(value).Trim();
        }

        private decimal GetDecimalValue(DataRow row, params string[] columnNames)
        {
            object value = null;
            foreach (string columnName in columnNames)
            {
                value = GetColumnValue(row, columnName);
                if (!IsNullValue(value))
                    break;
            }

            decimal.TryParse(Convert.ToString(value), out decimal decimalValue);
            return decimalValue;
        }

        private object GetColumnValue(DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) ? row[columnName] : null;
        }

        private bool HasSupplierIndent()
        {
            int.TryParse(Convert.ToString(ObjStockEntry?.SupplierIndentId), out int supplierIndentID);
            return supplierIndentID > 0;
        }

        private void UpdateFormTitle()
        {
            string supplierName = Convert.ToString(ObjStockEntry?.SUPPLIERNAME);
            string invoiceNumber = Convert.ToString(ObjStockEntry?.SUPPLIERINVOICENO);
            Text = !string.IsNullOrWhiteSpace(supplierName) && !string.IsNullOrWhiteSpace(invoiceNumber)
                ? $"Stock Entry - {supplierName} - {invoiceNumber}"
                : "Stock Entry";
            btnViewIndentItems.Enabled = HasSupplierIndent();
        }

        private bool IsNullValue(object value)
        {
            return value == null || value == DBNull.Value;
        }

        private void frmStockEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}
