using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmSKUStockTransfer : XtraForm
    {
        private DataSet lookups;
        private readonly DataTable pairs = CreatePairs();
        private bool loading;
        private bool saved;
        private DataRow editing;

        public frmSKUStockTransfer()
        {
            InitializeComponent();
            gcPairs.DataSource = pairs;
        }

        private static DataTable CreatePairs()
        {
            DataTable table = new DataTable();
            foreach (string name in new[] { "SOURCEITEMID", "SOURCEITEMCODEID", "SOURCEITEMPRICEID", "DESTINATIONITEMID", "DESTINATIONITEMCODEID", "DESTINATIONITEMPRICEID" })
                table.Columns.Add(name, typeof(int));
            foreach (string name in new[] { "SOURCESKU", "SOURCEITEM", "SOURCEEAN", "SOURCEUNIT", "DESTINATIONSKU", "DESTINATIONITEM", "DESTINATIONEAN", "DESTINATIONUNIT" })
                table.Columns.Add(name, typeof(string));
            foreach (string name in new[] { "SOURCEMRP", "DESTINATIONMRP", "SOURCEAMOUNT", "DESTINATIONAMOUNT" })
                table.Columns.Add(name, typeof(decimal));
            return table;
        }

        private void frmSKUStockTransfer_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = btnAdd.Enabled = btnEdit.Enabled = btnRemove.Enabled = btnHistory.Enabled = false;
            try
            {
                loading = true;
                lookups = new StockTransferRepository().GetLookups();
                Bind(cmbWarehouse, lookups.Tables[0], "BRANCHID", "BRANCHNAME");
                if (lookups.Tables[0].AsEnumerable().Any(r => Convert.ToInt32(r["BRANCHID"]) == Utility.BranchID))
                    cmbWarehouse.EditValue = Utility.BranchID;
                Bind(cmbAdjustmentType, lookups.Tables[1], "STOCKADJUSTMENTTYPEID", "STOCKADJUSTMENTTYPE");
                Bind(sluSourceSKU, lookups.Tables[2].Copy(), "ITEMID", "SKUCODE");
                Bind(sluDestinationSKU, lookups.Tables[2].Copy(), "ITEMID", "SKUCODE");
                btnSave.Enabled = btnAdd.Enabled = btnEdit.Enabled = btnRemove.Enabled = btnHistory.Enabled = true;
                AccessUtility.SetStatusByAccess(btnSave, btnAdd, btnEdit, btnRemove, btnHistory);
            }
            catch (Exception ex) { ShowError(ex); }
            finally { loading = false; }
            if (lookups != null)
            {
                LoadEans(true);
                LoadEans(false);
            }
        }

        private static void Bind(LookUpEdit control, DataTable table, string value, string display)
        {
            control.Properties.DataSource = table;
            control.Properties.ValueMember = value;
            control.Properties.DisplayMember = display;
            control.EditValue = table.Rows.Count == 1 ? table.Rows[0][value] : null;
        }

        private static void Bind(SearchLookUpEdit control, DataTable table, string value, string display)
        {
            control.Properties.DataSource = table;
            control.Properties.ValueMember = value;
            control.Properties.DisplayMember = display;
            control.EditValue = table.Rows.Count == 1 ? table.Rows[0][value] : null;
        }

        private static DataTable Filter(DataTable table, string column, object value)
        {
            DataView view = new DataView(table) { RowFilter = column + " = " + (value == null || value == DBNull.Value ? "-1" : Convert.ToInt32(value).ToString()) };
            return view.ToTable();
        }

        private SearchLookUpEdit Sku(bool source) { return source ? sluSourceSKU : sluDestinationSKU; }
        private LookUpEdit Ean(bool source) { return source ? cmbSourceEAN : cmbDestinationEAN; }
        private LookUpEdit Price(bool source) { return source ? cmbSourceMRP : cmbDestinationMRP; }
        private DataRow Item(bool source)
        {
            if (lookups == null || Sku(source).EditValue == null) return null;
            return lookups.Tables[2].AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["ITEMID"]) == Convert.ToInt32(Sku(source).EditValue));
        }

        private void SKU_EditValueChanged(object sender, EventArgs e)
        {
            if (loading || lookups == null) return;
            LoadEans(sender == sluSourceSKU);
        }

        private void LoadEans(bool source)
        {
            loading = true;
            try
            {
                DataRow item = Item(source);
                (source ? txtSourceName : txtDestinationName).Text = Convert.ToString(item?["ITEMNAME"]);
                Bind(Ean(source), Filter(lookups.Tables[3], "ITEMID", Sku(source).EditValue), "ITEMCODEID", "ITEMCODE");
                Bind(Price(source), Filter(lookups.Tables[4], "ITEMCODEID", Ean(source).EditValue), "ITEMPRICEID", "MRP");
            }
            finally { loading = false; }
            Recalculate();
        }

        private void EAN_EditValueChanged(object sender, EventArgs e)
        {
            if (loading || lookups == null) return;
            bool source = sender == cmbSourceEAN;
            loading = true;
            try { Bind(Price(source), Filter(lookups.Tables[4], "ITEMCODEID", Ean(source).EditValue), "ITEMPRICEID", "MRP"); }
            finally { loading = false; }
            Recalculate();
        }

        private void Quantity_EditValueChanged(object sender, EventArgs e)
        {
            if (!loading) Recalculate();
        }

        private decimal Calculate()
        {
            DataRow source = Item(true), destination = Item(false);
            if (source == null || destination == null) throw new ArgumentException("Select source and destination SKUs.");
            return StockTransferCalculation.Calculate(Convert.ToDecimal(txtSourceAmount.EditValue ?? 0),
                Convert.ToBoolean(source["ISOPENITEM"]), Convert.ToBoolean(destination["ISOPENITEM"]),
                source.IsNull("FACTOR") ? 0 : Convert.ToDecimal(source["FACTOR"]),
                destination.IsNull("FACTOR") ? 0 : Convert.ToDecimal(destination["FACTOR"]),
                source.IsNull("BASEUOMID") ? 0 : Convert.ToInt32(source["BASEUOMID"]),
                destination.IsNull("BASEUOMID") ? 0 : Convert.ToInt32(destination["BASEUOMID"]));
        }

        private void Recalculate()
        {
            sourceAmountItem.Text = Item(true) != null && Convert.ToBoolean(Item(true)["ISOPENITEM"]) ? "Weight (kg)" : "Quantity";
            destinationAmountItem.Text = Item(false) != null && Convert.ToBoolean(Item(false)["ISOPENITEM"]) ? "Weight (kg) - calculated" : "Quantity - calculated";
            try
            {
                txtDestinationAmount.EditValue = Calculate();
                lblStatus.Text = "Destination amount is calculated from the source amount and configured UOMs.";
            }
            catch (ArgumentException ex) { txtDestinationAmount.EditValue = null; lblStatus.Text = ex.Message; }
            catch (OverflowException) { txtDestinationAmount.EditValue = null; lblStatus.Text = "The quantity is too large."; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnAdd.Tag?.ToString())) return;
            try
            {
                decimal destinationAmount = Calculate();
                if (Ean(true).EditValue == null || Ean(false).EditValue == null || Price(true).EditValue == null || Price(false).EditValue == null)
                    throw new ArgumentException("Select the EAN code and MRP on both sides.");
                if (Convert.ToInt32(Price(true).EditValue) == Convert.ToInt32(Price(false).EditValue))
                    throw new ArgumentException("Source and destination must have different item prices.");
                DataRow row = pairs.NewRow();
                FillSide(row, true);
                FillSide(row, false);
                row["SOURCEAMOUNT"] = Convert.ToDecimal(txtSourceAmount.EditValue);
                row["DESTINATIONAMOUNT"] = destinationAmount;
                if (editing == null) pairs.Rows.Add(row);
                else editing.ItemArray = row.ItemArray;
                editing = null;
                btnAdd.Text = "Add to Batch";
                txtSourceAmount.EditValue = 0m;
                lblStatus.Text = pairs.Rows.Count + " pair(s) in this batch. Not saved.";
            }
            catch (ArgumentException ex) { XtraMessageBox.Show(ex.Message, "SKU Transfer"); }
            catch (Exception ex) { ShowError(ex); }
        }

        private void FillSide(DataRow row, bool source)
        {
            string prefix = source ? "SOURCE" : "DESTINATION";
            DataRow item = Item(source);
            row[prefix + "ITEMID"] = item["ITEMID"];
            row[prefix + "SKU"] = item["SKUCODE"];
            row[prefix + "ITEM"] = item["ITEMNAME"];
            row[prefix + "ITEMCODEID"] = Ean(source).EditValue;
            row[prefix + "EAN"] = lookups.Tables[3].AsEnumerable().First(r =>
                Convert.ToInt32(r["ITEMCODEID"]) == Convert.ToInt32(Ean(source).EditValue))["ITEMCODE"];
            row[prefix + "ITEMPRICEID"] = Price(source).EditValue;
            row[prefix + "MRP"] = lookups.Tables[4].AsEnumerable().First(r =>
                Convert.ToInt32(r["ITEMPRICEID"]) == Convert.ToInt32(Price(source).EditValue))["MRP"];
            row[prefix + "UNIT"] = Convert.ToBoolean(item["ISOPENITEM"]) ? "kg" : "units";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnEdit.Tag?.ToString())) return;
            DataRow row = gvPairs.GetFocusedDataRow();
            if (row == null) return;
            editing = row;
            foreach (bool source in new[] { true, false })
            {
                string prefix = source ? "SOURCE" : "DESTINATION";
                Sku(source).EditValue = row[prefix + "ITEMID"];
                LoadEans(source);
                Ean(source).EditValue = row[prefix + "ITEMCODEID"];
                // Explicit load also handles reselecting the same EAN.
                Bind(Price(source), Filter(lookups.Tables[4], "ITEMCODEID", Ean(source).EditValue), "ITEMPRICEID", "MRP");
                Price(source).EditValue = row[prefix + "ITEMPRICEID"];
            }
            txtSourceAmount.EditValue = row["SOURCEAMOUNT"];
            btnAdd.Text = "Update Pair";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnRemove.Tag?.ToString())) return;
            DataRow row = gvPairs.GetFocusedDataRow();
            if (row == null) return;
            if (row == editing) { editing = null; btnAdd.Text = "Add to Batch"; }
            pairs.Rows.Remove(row);
            lblStatus.Text = pairs.Rows.Count + " pair(s) in this batch. Not saved.";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnSave.Tag?.ToString())) return;
            if (editing != null || Convert.ToDecimal(txtSourceAmount.EditValue ?? 0) > 0)
            {
                XtraMessageBox.Show("Add or update the current pair before saving the batch.", "SKU Transfer");
                return;
            }
            if (cmbWarehouse.EditValue == null || cmbAdjustmentType.EditValue == null || pairs.Rows.Count == 0)
            {
                XtraMessageBox.Show("Select a warehouse, stock adjustment type and add at least one pair.", "SKU Transfer");
                return;
            }
            if (XtraMessageBox.Show("Save all " + pairs.Rows.Count + " pair(s) as one stock transfer batch?", "SKU Transfer",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            btnSave.Enabled = false;
            try
            {
                DataTable payload = new DataTable();
                payload.Columns.Add("ROWNO", typeof(int));
                payload.Columns.Add("SOURCEITEMPRICEID", typeof(int));
                payload.Columns.Add("DESTINATIONITEMPRICEID", typeof(int));
                payload.Columns.Add("SOURCEAMOUNT", typeof(decimal));
                payload.Columns.Add("DESTINATIONAMOUNT", typeof(decimal));
                for (int index = 0; index < pairs.Rows.Count; index++)
                {
                    DataRow row = pairs.Rows[index];
                    payload.Rows.Add(index + 1, row["SOURCEITEMPRICEID"], row["DESTINATIONITEMPRICEID"], row["SOURCEAMOUNT"], row["DESTINATIONAMOUNT"]);
                }
                int id = new StockTransferRepository().Save(Convert.ToInt32(cmbWarehouse.EditValue),
                    Convert.ToInt32(cmbAdjustmentType.EditValue), Utility.UserID, txtRemarks.Text, payload);
                saved = true;
                XtraMessageBox.Show("Stock transfer batch #" + id + " saved successfully.", "SKU Transfer");
                Close();
            }
            catch (Exception ex) { ShowError(ex); }
            finally { if (!IsDisposed) btnSave.Enabled = AccessUtility.HasAccess(btnSave.Tag?.ToString()); }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnHistory.Tag?.ToString()) || cmbWarehouse.EditValue == null) return;
            try
            {
                using (frmStockTransferHistory form = new frmStockTransferHistory(Convert.ToInt32(cmbWarehouse.EditValue)))
                    form.ShowDialog(this);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnCancel_Click(object sender, EventArgs e) { Close(); }
        private void frmSKUStockTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved && (pairs.Rows.Count > 0 || Convert.ToDecimal(txtSourceAmount.EditValue ?? 0) > 0))
                e.Cancel = XtraMessageBox.Show("Discard the unsaved transfer batch?", "SKU Transfer", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes;
        }
        private static void ShowError(Exception ex) { ErrorMgmt.ShowError(ex); AppLog.Error(ex); }
    }
}
