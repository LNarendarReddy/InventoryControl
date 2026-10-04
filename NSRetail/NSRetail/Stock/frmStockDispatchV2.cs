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
    public partial class frmStockDispatchV2 : XtraForm
    {
        private readonly StockRepository stockRepository = new StockRepository();
        private StockDispatch stockDispatch;

        public frmStockDispatchV2()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        internal StockDispatch StockDispatchObject => stockDispatch;

        private void frmStockDispatchV2_Load(object sender, EventArgs e)
        {
            try
            {
                stockDispatch = new StockDispatch
                {
                    UserID = Utility.UserID,
                    CATEGORYID = Utility.CategoryID
                };
                stockRepository.GetDispatchDraft(stockDispatch);
                EnsureDispatchTable();

                if (Convert.ToInt32(stockDispatch.STOCKDISPATCHID) <= 0)
                {
                    using (var info = new frmStockDispatchInfoV2(stockDispatch))
                    {
                        if (info.ShowDialog(this) != DialogResult.OK)
                        {
                            BeginInvoke(new Action(Close));
                            return;
                        }
                    }
                }

                gcDispatch.DataSource = stockDispatch.dtDispatch;
                UpdateHeader();
                btnAddItem.Focus();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
                BeginInvoke(new Action(Close));
            }
        }

        internal bool EnsureDispatchSaved()
        {
            try
            {
                if (Convert.ToInt32(stockDispatch?.STOCKDISPATCHID) > 0)
                    return true;

                if (stockDispatch?.FROMBRANCHID == null || stockDispatch.TOBRANCHID == null || stockDispatch.CATEGORYID == null)
                    return false;

                stockDispatch.UserID = Utility.UserID;
                stockRepository.SaveDispatch(stockDispatch);
                UpdateHeader();
                return Convert.ToInt32(stockDispatch.STOCKDISPATCHID) > 0;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
                return false;
            }
        }

        internal void RefreshGrid(StockDispatchItemResult item)
        {
            DataRow row = null;
            foreach (DataRow candidate in stockDispatch.dtDispatch.Rows)
            {
                if (Convert.ToString(candidate["STOCKDISPATCHDETAILID"]) == Convert.ToString(item.STOCKDISPATCHDETAILID))
                {
                    row = candidate;
                    break;
                }
            }

            if (row == null)
            {
                row = stockDispatch.dtDispatch.NewRow();
                stockDispatch.dtDispatch.Rows.Add(row);
            }

            decimal quantity = Convert.ToDecimal(item.DISPATCHQUANTITY ?? 0);
            decimal weight = Convert.ToDecimal(item.WEIGHTINKGS ?? 0);
            if (row["STOCKDISPATCHDETAILID"] != DBNull.Value)
            {
                quantity += Convert.ToDecimal(row["DISPATCHQUANTITY"] == DBNull.Value ? 0 : row["DISPATCHQUANTITY"]);
                weight += Convert.ToDecimal(row["WEIGHTINKGS"] == DBNull.Value ? 0 : row["WEIGHTINKGS"]);
            }

            row["STOCKDISPATCHDETAILID"] = item.STOCKDISPATCHDETAILID;
            row["ITEMID"] = item.ITEMID ?? DBNull.Value;
            row["ITEMCODEID"] = item.ITEMCODEID ?? DBNull.Value;
            row["ITEMPRICEID"] = item.ITEMPRICEID ?? DBNull.Value;
            row["SKUCODE"] = item.SKUCODE ?? DBNull.Value;
            row["ITEMCODE"] = item.ITEMCODE ?? DBNull.Value;
            row["ITEMNAME"] = item.ITEMNAME ?? DBNull.Value;
            row["MRP"] = item.MRP ?? DBNull.Value;
            row["SALEPRICE"] = item.SALEPRICE ?? DBNull.Value;
            row["DISPATCHQUANTITY"] = quantity;
            row["WEIGHTINKGS"] = weight;
            row["TRAYNUMBER"] = item.TRAYNUMBER ?? DBNull.Value;
            row["TRAYINFOID"] = item.TRAYINFOID ?? DBNull.Value;
            gvDispatch.RefreshData();
            gvDispatch.FocusedRowHandle = gvDispatch.LocateByValue("STOCKDISPATCHDETAILID", item.STOCKDISPATCHDETAILID);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            using (var itemForm = new frmStockDispatchItemV2(stockDispatch, this))
                itemForm.ShowDialog(this);
        }

        private void btnDispatchInfo_Click(object sender, EventArgs e)
        {
            using (var info = new frmStockDispatchInfoV2(stockDispatch))
            {
                if (info.ShowDialog(this) == DialogResult.OK)
                    UpdateHeader();
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDispatch.FocusedRowHandle < 0 ||
                XtraMessageBox.Show("Are you sure want to delete item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                stockRepository.DeleteDispatchDetail(gvDispatch.GetFocusedRowCellValue("STOCKDISPATCHDETAILID"));
                gvDispatch.DeleteRow(gvDispatch.FocusedRowHandle);
                gvDispatch.GridControl.BindingContext = new BindingContext();
                gvDispatch.GridControl.DataSource = stockDispatch.dtDispatch;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            if (gvDispatch.RowCount == 0)
            {
                XtraMessageBox.Show("Add at least one item before dispatching");
                return;
            }

            if (!EnsureDispatchSaved() ||
                XtraMessageBox.Show($"Are you sure want to submit dispatch from {lblFromBranchValue.Text} to {lblToBranchValue.Text}?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                stockRepository.UpdateDispatch(stockDispatch);
                DataSet data = stockRepository.GetDispatch(stockDispatch.STOCKDISPATCHID);
                if (data == null || data.Tables.Count < 2 || data.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show("No data returned from database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                var report = new rptDispatch(data.Tables[0], data.Tables[1]) { ShowPrintMarginsWarning = false };
                report.ShowRibbonPreview();
                Close();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to discard dispatch?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                if (Convert.ToInt32(stockDispatch?.STOCKDISPATCHID) > 0)
                    stockRepository.DiscardStockDispatch(stockDispatch.STOCKDISPATCHID, Utility.UserID);
                Close();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EnsureDispatchTable()
        {
            if (stockDispatch.dtDispatch == null)
                stockDispatch.dtDispatch = new DataTable();

            AddColumn("STOCKDISPATCHDETAILID", typeof(int));
            AddColumn("ITEMID", typeof(int));
            AddColumn("ITEMCODEID", typeof(int));
            AddColumn("ITEMPRICEID", typeof(int));
            AddColumn("SKUCODE", typeof(string));
            AddColumn("ITEMCODE", typeof(string));
            AddColumn("ITEMNAME", typeof(string));
            AddColumn("MRP", typeof(decimal));
            AddColumn("SALEPRICE", typeof(decimal));
            AddColumn("DISPATCHQUANTITY", typeof(decimal));
            AddColumn("WEIGHTINKGS", typeof(decimal));
            AddColumn("TRAYNUMBER", typeof(string));
            AddColumn("TRAYINFOID", typeof(int));
        }

        private void AddColumn(string name, Type type)
        {
            if (!stockDispatch.dtDispatch.Columns.Contains(name))
                stockDispatch.dtDispatch.Columns.Add(name, type);
        }

        private void UpdateHeader()
        {
            lblFromBranchValue.Text = GetLookupText(Utility.GetBranchList(), "BRANCHID", stockDispatch.FROMBRANCHID, "BRANCHNAME");
            lblToBranchValue.Text = GetLookupText(Utility.GetBranchList(), "BRANCHID", stockDispatch.TOBRANCHID, "BRANCHNAME");
            lblCategoryValue.Text = GetLookupText(Utility.GetCategoryList(), "CATEGORYID", stockDispatch.CATEGORYID, "CATEGORYNAME");
            Text = Convert.ToInt32(stockDispatch.STOCKDISPATCHID) > 0 ? $"Stock Dispatch New - Draft #{stockDispatch.STOCKDISPATCHID}" : "Stock Dispatch New";
        }

        private string GetLookupText(DataTable table, string keyColumn, object key, string textColumn)
        {
            if (table == null || key == null)
                return string.Empty;
            foreach (DataRow row in table.Rows)
                if (Convert.ToString(row[keyColumn]) == Convert.ToString(key))
                    return Convert.ToString(row[textColumn]);
            return Convert.ToString(key);
        }
    }

    internal sealed class StockDispatchItemResult
    {
        public object STOCKDISPATCHDETAILID { get; set; }
        public object ITEMID { get; set; }
        public object ITEMCODEID { get; set; }
        public object ITEMPRICEID { get; set; }
        public object SKUCODE { get; set; }
        public object ITEMCODE { get; set; }
        public object ITEMNAME { get; set; }
        public object MRP { get; set; }
        public object SALEPRICE { get; set; }
        public object DISPATCHQUANTITY { get; set; }
        public object WEIGHTINKGS { get; set; }
        public object TRAYNUMBER { get; set; }
        public object TRAYINFOID { get; set; }
    }
}
