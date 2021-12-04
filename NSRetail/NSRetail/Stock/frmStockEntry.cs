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
        bool IsEdit = false;
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
                    dtpInvoice.EditValue = DateTime.Now;
                    cmbSupplier.Enabled = true;
                    txtInvoiceNumber.Enabled = true;
                    dtpInvoice.Enabled = true;
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

        private void gvStockEntry_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
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
                view.SetRowCellValue(e.RowHandle, "COSTPRICE", ObjStockEntryDetail.COSTPRICE);
                view.SetRowCellValue(e.RowHandle, "MRP", ObjStockEntryDetail.MRP);
                view.SetRowCellValue(e.RowHandle, "SALEPRICE", ObjStockEntryDetail.SALEPRICE);
                view.SetRowCellValue(e.RowHandle, "QUANTITY", ObjStockEntryDetail.QUANTITY);
                view.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
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
                frmAddStockRecord obj = new frmAddStockRecord(ObjStockEntry, this);
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
                gvStockEntry.GridControl.BindingContext = new BindingContext();
                gvStockEntry.GridControl.DataSource = gvStockEntry.DataSource;
                int rowhandle = gvStockEntry.LocateByValue("STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                if (rowhandle >= 0)
                {
                    if (IsEdit)
                    {
                        gvStockEntry.SetRowCellValue(rowhandle, "QUANTITY", ObjStockEntryDetail.QUANTITY);
                        gvStockEntry.SetRowCellValue(rowhandle, "WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
                    }
                    else
                    {
                        int Qnty = 0;
                        decimal WeightInKGS = 0;
                        if (int.TryParse(Convert.ToString(gvStockEntry.GetRowCellValue(rowhandle, "QUANTITY")), out Qnty))
                            gvStockEntry.SetRowCellValue(rowhandle, "QUANTITY", Qnty + Convert.ToInt32(ObjStockEntryDetail.QUANTITY));
                        if (decimal.TryParse(Convert.ToString(gvStockEntry.GetRowCellValue(rowhandle, "WEIGHTINKGS")), out WeightInKGS))
                            gvStockEntry.SetRowCellValue(rowhandle, "WEIGHTINKGS", WeightInKGS + Convert.ToDecimal(ObjStockEntryDetail.WEIGHTINKGS));

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

        public void SaveInvoice()
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
    }
}