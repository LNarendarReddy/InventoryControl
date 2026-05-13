using DevExpress.DataAccess.Native;
using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Branch
{
    public partial class frmLiquidation : XtraForm
    {
        Liquidation _liquidation;
        DataRow drSelectedPrice = null;
        bool _isLoading = false;
        int _liquidationId = 0;

        public frmLiquidation(Liquidation liquidation)
        {
            InitializeComponent();
            _liquidation = liquidation;
        }

        private void frmBranchExpense_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = new ItemRepository().GetItemCodes();
            sluItemCode.Properties.DisplayMember = "ITEMNAME";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;

            Utility.SetGridFormatting(sluItemCodeView);

            luReason.Properties.DataSource = new ReportRepository().GetReportData("USP_R_LIQUIDATIONREASON");
            luReason.Properties.DisplayMember = "LIQUIDATIONREASONTEXT";
            luReason.Properties.ValueMember = "LIQUIDATIONREASONID";

            int.TryParse(_liquidation?.LiquidationID?.ToString(), out _liquidationId);
            Text = _liquidationId > 0 ? $"Edit Liquidation - {_liquidation.ItemCode}" : "Add liquidation";

            if (_liquidationId == 0) return;

            _isLoading = true;

            txtNotes.EditValue = _liquidation.Description;
            txtQty.EditValue = _liquidation.QtyOrWghtInKGs;
            sluItemCode.EditValue = _liquidation.ItemCodeID;
            txtMRP.EditValue = _liquidation.MRP;
            txtSalePrice.EditValue = _liquidation.SalePrice;
            txtSKUCode.EditValue = _liquidation.SKUCode;
            txtItemCode.EditValue = _liquidation.ItemCode;
            DataTable dtItemPrice = new();
            dtItemPrice.Columns.Add("ITEMPRICEID", typeof(object));
            drSelectedPrice = dtItemPrice.NewRow();
            drSelectedPrice["ITEMPRICEID"] = _liquidation.ItemPriceID;

            luReason.EditValue = _liquidation.ReasonID;
            dtpExpiryDate.EditValue = _liquidation.ExpiryDate;
            dtpMfgDate.EditValue = _liquidation.ManufactureDate;

            _isLoading = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            if (drSelectedPrice == null)
            {
                XtraMessageBox.Show("Item price not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                sluItemCode.Focus();
                return;
            }

            _liquidation.Description = txtNotes.EditValue;
            _liquidation.QtyOrWghtInKGs = txtQty.EditValue;
            _liquidation.ItemPriceID = drSelectedPrice["ITEMPRICEID"];
            _liquidation.ReasonID = luReason.EditValue;
            _liquidation.ManufactureDate = dtpMfgDate.EditValue;
            _liquidation.ExpiryDate = dtpExpiryDate.EditValue;

            try
            {
                new OperationsRepository().SaveLiquidation(_liquidation);
                XtraMessageBox.Show("Liquidation saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!toggleSwitch1.IsOn)
                {
                    this.Close();
                    return;
                }

                ClearItemData();
                _liquidation = new();
                txtQty.EditValue = 1;
                txtNotes.EditValue = null;
                luReason.EditValue = null;
                dtpMfgDate.EditValue = null;
                dtpExpiryDate.EditValue = null;

                sluItemCode.Focus();
                sluItemCode.ShowPopup();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            try
            {
                if (sluItemCode.EditValue == null)
                {
                    ClearItemData();
                    return;
                }

                int rowHandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
                txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODE");
                txtSKUCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "SKUCODE");

                DataTable dtPrices = new ItemRepository().GetMRPList(sluItemCode.EditValue);
                drSelectedPrice = null;
                if (dtPrices.Rows.Count > 1)
                {
                    frmMRPSelection mRPSelection = new frmMRPSelection(dtPrices, txtItemCode.EditValue, sluItemCode.Text)
                    { StartPosition = FormStartPosition.CenterScreen };
                    mRPSelection.ShowDialog();
                    if (!mRPSelection._IsSave)
                    {
                        ClearItemData();
                        return;
                    }

                    drSelectedPrice = (mRPSelection.drSelected as DataRowView)?.Row;
                }
                else if (dtPrices.Rows.Count == 1)
                {
                    drSelectedPrice = dtPrices.Rows[0];
                }
                else if (dtPrices.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Item code or stock not found for the scan. please contact administrator");
                    ClearItemData();
                    return;
                }
                
                //txtItemName.EditValue = (sluItemCode.GetSelectedDataRow() as DataRowView)?.Row["ITEMNAME"];
                txtMRP.EditValue = drSelectedPrice["MRP"];
                txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];
                txtQty.EditValue = 1;
                txtQty.Focus();
                txtQty.SelectAll();

                luReason.EditValue = null;
                dtpMfgDate.EditValue = null;
                dtpExpiryDate.EditValue = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void ClearItemData()
        {
            sluItemCode.EditValue = null;
            txtSKUCode.EditValue = null;
            txtItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
        }

        private void frmLiquidation_Shown(object sender, EventArgs e)
        {
            if (_liquidationId > 0) return;

            sluItemCode.Focus();
            sluItemCode.ShowPopup();
        }
    }
}