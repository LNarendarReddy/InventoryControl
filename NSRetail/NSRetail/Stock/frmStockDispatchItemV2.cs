using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockDispatchItemV2 : XtraForm, IBarcodeReceiver
    {
        private readonly ItemCodeRepository itemRepository = new ItemCodeRepository();
        private readonly StockRepository stockRepository = new StockRepository();
        private readonly StockDispatch stockDispatch;
        private readonly frmStockDispatchV2 parentForm;
        private object itemPriceID;
        private bool isOpenItem;
        private bool isParentItem;
        private bool isLoading;
        private frmMain mainForm;

        public frmStockDispatchItemV2()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        internal frmStockDispatchItemV2(StockDispatch stockDispatch, frmStockDispatchV2 parentForm) : this()
        {
            this.stockDispatch = stockDispatch;
            this.parentForm = parentForm;
        }

        private void frmStockDispatchItemV2_Load(object sender, EventArgs e)
        {
            try
            {
                txtQuantity.ConfirmBarCodeScan();
                txtWeightInKgs.ConfirmBarCodeScan();
                mainForm = parentForm.MdiParent as frmMain;
                if (mainForm != null)
                    mainForm.RefreshBaseLineData += MainForm_RefreshBaseLineData;

                BindItemCodes();
                BindTrayNumbers();
                cmbItemCode.Focus();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void frmStockDispatchItemV2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mainForm != null)
                mainForm.RefreshBaseLineData -= MainForm_RefreshBaseLineData;
        }

        private void MainForm_RefreshBaseLineData(object sender, EventArgs e)
        {
            object selected = cmbItemCode.EditValue;
            BindItemCodes();
            cmbItemCode.EditValue = selected;
        }

        private void BindItemCodes()
        {
            DataTable items = Utility.GetItemCodeListFiltered().Copy();
            DataView view = items.DefaultView;
            view.RowFilter = "CATEGORYID = " + stockDispatch.CATEGORYID;
            cmbItemCode.Properties.DataSource = view.ToTable();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
        }

        private void BindTrayNumbers()
        {
            if (Convert.ToInt32(stockDispatch.STOCKDISPATCHID) <= 0)
            {
                cmbTrayNumber.Properties.DataSource = null;
                return;
            }

            cmbTrayNumber.Properties.DataSource = stockRepository.GetTrayInfo(stockDispatch.STOCKDISPATCHID, false);
            cmbTrayNumber.Properties.ValueMember = "TRAYINFOID";
            cmbTrayNumber.Properties.DisplayMember = "TRAYNUMBER";
        }

        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading || cmbItemCode.EditValue == null)
                return;

            try
            {
                int rowHandle = cmbLookupView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
                if (rowHandle < 0)
                    return;

                txtItemName.EditValue = cmbLookupView.GetRowCellValue(rowHandle, "ITEMNAME");
                int parentID = 0;
                isParentItem = int.TryParse(Convert.ToString(cmbLookupView.GetRowCellValue(rowHandle, "PARENTITEMID")), out parentID) && parentID > 0;
                isOpenItem = bool.TryParse(Convert.ToString(cmbLookupView.GetRowCellValue(rowHandle, "ISOPENITEM")), out bool openItem) && openItem;
                txtQuantity.Enabled = !isOpenItem;
                txtWeightInKgs.Enabled = isOpenItem;

                DataTable prices = itemRepository.GetMRPList(cmbItemCode.EditValue);
                if (prices.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No MRP exists for the selected item");
                    ClearItem();
                    return;
                }

                DataRow priceRow = null;
                if (prices.Rows.Count > 1)
                {
                    using (var mrpList = new frmMRPList(prices, cmbItemCode.EditValue))
                    {
                        mrpList.ShowDialog(this);
                        if (!mrpList._IsSave)
                        {
                            ClearItem();
                            return;
                        }
                        priceRow = ((DataRowView)mrpList.drSelected).Row;
                    }
                }
                else
                {
                    priceRow = prices.Rows[0];
                }

                txtMRP.EditValue = priceRow["MRP"];
                txtSalePrice.EditValue = priceRow["SALEPRICE"];
                itemPriceID = priceRow["ITEMPRICEID"];
                LoadCurrentStock(parentID);
                txtQuantity.EditValue = 1;
                txtWeightInKgs.EditValue = 0.00M;
                SendKeys.Send("{ENTER}");
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void LoadCurrentStock(int parentID)
        {
            DataTable stock = stockRepository.GetCurrentStock(stockDispatch.FROMBRANCHID, stockDispatch.TOBRANCHID, cmbItemCode.EditValue, parentID);
            DataRow row = stock != null && stock.Rows.Count > 0 ? stock.Rows[0] : null;
            txtWarehouseStock.EditValue = row?[0];
            txtBranchStock.EditValue = row?[1];
        }

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading || txtQuantity.EditValue == null || !isParentItem || isOpenItem)
                return;

            int rowHandle = cmbLookupView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            if (rowHandle >= 0 && decimal.TryParse(Convert.ToString(cmbLookupView.GetRowCellValue(rowHandle, "MULTIPLIER")), out decimal multiplier) &&
                decimal.TryParse(Convert.ToString(txtQuantity.EditValue), out decimal quantity))
                txtWeightInKgs.EditValue = multiplier * quantity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateItem())
                return;

            try
            {
                if (!parentForm.EnsureDispatchSaved())
                    return;

                var detail = new StockDispatchDetail
                {
                    STOCKDISPATCHDETAILID = 0,
                    STOCKDISPATCHID = stockDispatch.STOCKDISPATCHID,
                    ITEMPRICEID = itemPriceID,
                    TRAYNUMBER = cmbTrayNumber.Text,
                    TRAYINFOID = cmbTrayNumber.EditValue,
                    DISPATCHQUANTITY = txtQuantity.EditValue,
                    WEIGHTINKGS = txtWeightInKgs.EditValue,
                    UserID = Utility.UserID
                };
                stockRepository.SaveDispatchDetail(detail);

                int rowHandle = cmbLookupView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
                parentForm.RefreshGrid(new StockDispatchItemResult
                {
                    STOCKDISPATCHDETAILID = detail.STOCKDISPATCHDETAILID,
                    ITEMID = cmbLookupView.GetRowCellValue(rowHandle, "ITEMID"),
                    ITEMCODEID = cmbItemCode.EditValue,
                    ITEMPRICEID = itemPriceID,
                    SKUCODE = cmbLookupView.GetRowCellValue(rowHandle, "SKUCODE"),
                    ITEMCODE = cmbItemCode.Text,
                    ITEMNAME = txtItemName.EditValue,
                    MRP = txtMRP.EditValue,
                    SALEPRICE = txtSalePrice.EditValue,
                    DISPATCHQUANTITY = txtQuantity.EditValue,
                    WEIGHTINKGS = txtWeightInKgs.EditValue,
                    TRAYNUMBER = cmbTrayNumber.Text,
                    TRAYINFOID = cmbTrayNumber.EditValue
                });

                ClearItem();
                cmbItemCode.Focus();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private bool ValidateItem()
        {
            if (cmbItemCode.EditValue == null || itemPriceID == null)
            {
                XtraMessageBox.Show("Item is mandatory");
                return false;
            }
            if (cmbTrayNumber.EditValue == null)
            {
                XtraMessageBox.Show("Tray number is mandatory");
                return false;
            }
            if (!decimal.TryParse(Convert.ToString(txtQuantity.EditValue), out decimal quantity) || quantity <= 0)
            {
                XtraMessageBox.Show("Quantity should be greater than zero");
                return false;
            }
            if (isOpenItem && (!decimal.TryParse(Convert.ToString(txtWeightInKgs.EditValue), out decimal weight) || weight <= 0))
            {
                XtraMessageBox.Show("Weight should be greater than zero");
                return false;
            }
            return true;
        }

        private void btnAddTray_Click(object sender, EventArgs e)
        {
            try
            {
                using (var tray = new frmSingleTextbox("Tray Number"))
                {
                    tray.ShowInTaskbar = false;
                    tray.StartPosition = FormStartPosition.CenterParent;
                    tray.ShowDialog(this);
                    if (!tray.isSave || !parentForm.EnsureDispatchSaved())
                        return;

                    object trayInfoID = stockRepository.SaveTrayInfo(stockDispatch.STOCKDISPATCHID, tray.newValue, Utility.UserID);
                    BindTrayNumbers();
                    cmbTrayNumber.EditValue = trayInfoID;
                    cmbItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnDeleteTray_Click(object sender, EventArgs e)
        {
            if (cmbTrayNumber.EditValue == null ||
                XtraMessageBox.Show("Are you sure want to delete this tray?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                stockRepository.DeleteTrayInfo(stockDispatch.STOCKDISPATCHID, cmbTrayNumber.EditValue, Utility.UserID);
                BindTrayNumbers();
                cmbTrayNumber.EditValue = null;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void ClearItem()
        {
            isLoading = true;
            cmbItemCode.EditValue = null;
            txtItemName.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = null;
            txtWeightInKgs.EditValue = null;
            txtWarehouseStock.EditValue = null;
            txtBranchStock.EditValue = null;
            itemPriceID = null;
            isOpenItem = false;
            isParentItem = false;
            isLoading = false;
        }

        public void ReceiveBarCode(string data)
        {
            cmbItemCode.Text = data;
        }
    }
}
