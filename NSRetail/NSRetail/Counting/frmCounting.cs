using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmCounting : DevExpress.XtraEditors.XtraForm
    {
        DataRow drSelectedPrice;
        bool isItemScanned;
        bool isOpenItem;
        bool isEventCall;
        StockCounting stockCounting = new StockCounting();
        CloudRepository cloudRepository = new CloudRepository();
        public frmCounting()
        {
            InitializeComponent();
        }

        private void frmCounting_Load(object sender, EventArgs e)
        {
            ((frmMain)MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            sluItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered().Copy(); 
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;
            InitialLoad();
        }

        private void InitialLoad()
        {
            stockCounting.UserID = Utility.UserID;
            stockCounting = cloudRepository.GetCounting(stockCounting);
            if (!stockCounting.STOCKCOUNTINGID.Equals(0))
            {
                cmbBranch.EditValue = stockCounting.BRANCHID;
                cmbBranch.Enabled = false;
            }
            else
            {
                cmbBranch.Enabled = true;
                cmbBranch.EditValue = null;
            }
            gcCounting.DataSource = stockCounting.dtStockCountning
;
        }

        private void FrmStockDispatch_RefreshBaseLineData(object sender, EventArgs e)
        {
            object selectedValue = sluItemCode.EditValue;
            sluItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered().Copy();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMCODE";
            sluItemCode.EditValue = selectedValue;
        }

        private void SaveStockCounting()
        {
            stockCounting.BRANCHID = cmbBranch.EditValue;
            stockCounting.UserID = Utility.UserID;
            cloudRepository.SaveStockCounting(stockCounting);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gvCounting.RowCount == 0)
                return;
            stockCounting.UserID = Utility.UserID;
            new CloudRepository().UpdateStockCounting(stockCounting);
            stockCounting = new StockCounting();
            InitialLoad();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearItemData(bool focusItemCode = true)
        {
            txtItemCode.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = 1;
            txtWeightInKgs.EditValue = 0.00;

            if (focusItemCode)
                txtItemCode.Focus();
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODE", txtItemCode.EditValue);

            if (rowHandle < 0)
            {
                List<int> rowHandles = sluItemCodeView.LocateAllRowsByValue("SKUCODE", txtItemCode.EditValue);
                if (rowHandles.Count == 1)
                {
                    rowHandle = rowHandles.First();
                }
                else if (rowHandles.Count > 1)
                {
                    sluItemCode.ShowPopup();
                    sluItemCodeView.FindFilterText = txtItemCode.EditValue.ToString();
                    isItemScanned = true;
                }
            }

            if (rowHandle >= 0)
            {
                isItemScanned = true;
                sluItemCode.EditValue = null;
                sluItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODEID");
            }
            else
            {
                ClearItemData(false);
            }

            isItemScanned = false;
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null) { return; }
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
            txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODE");
            isOpenItem = Convert.ToBoolean(sluItemCodeView.GetRowCellValue(rowHandle, "ISOPENITEM"));
            DataTable dtPrices = new ItemCodeRepository().GetMRPList(sluItemCode.EditValue);
            if (dtPrices.Rows.Count > 1)
            {
                frmMRPList mRPSelection = new frmMRPList(dtPrices)
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

            if (drSelectedPrice == null)
            {
                return;
            }

            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];
            txtQuantity.EditValue = 1;
            if (isOpenItem)
            {
                txtWeightInKgs.Enabled = true;
                txtQuantity.EditValue = 1;
                txtQuantity.Enabled = false;
            }
            else
            {
                txtWeightInKgs.EditValue = "0.00";
                txtWeightInKgs.Enabled = false;
                txtQuantity.Enabled = true;
            }
            if (!isItemScanned)
            {
                if (isOpenItem)
                    txtWeightInKgs.Focus();
                else
                    txtQuantity.Focus();
            }
        }

        private void txtItemCode_Enter(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }

        private void txtItemCode_Click(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                return;

            if (sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null
                || txtQuantity.EditValue.Equals(0))
            {
                txtItemCode.Focus();
                return;
            }

            if (!dxValidationProvider1.Validate())
                return;

            if (stockCounting.STOCKCOUNTINGID.Equals(0))
                SaveStockCounting();

            gvCounting.GridControl.BindingContext = new BindingContext();
            gvCounting.GridControl.DataSource = stockCounting.dtStockCountning;
            isEventCall = true;
            int rowHandle = gvCounting.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle < 0)
            {
                gvCounting.AddNewRow();
            }
            else
            {
                int newQuantity = Convert.ToInt32(txtQuantity.EditValue) +
                    Convert.ToInt32(gvCounting.GetRowCellValue(rowHandle, "QUANTITY"));
                if (newQuantity > 0)
                {
                    gvCounting.SetRowCellValue(rowHandle, "QUANTITY", newQuantity);
                }
            }
            isEventCall = false;
            gvCounting.GridControl.BindingContext = new BindingContext();
            gvCounting.GridControl.DataSource =  stockCounting.dtStockCountning;

            rowHandle = gvCounting.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle >= 0)
            {
                SaveStockCountingDetail(rowHandle);
            }
            ClearItemData();
            gvCounting.FocusedRowHandle = rowHandle;
        }

        private void SaveStockCountingDetail(int rowHandle)
        {
            DataRow drDetail = (gvCounting.GetRow(rowHandle) as DataRowView).Row;
            object STOCKCOUNTINGDETAILID = new CloudRepository().SaveStockCountingDetail(drDetail);
            drDetail["STOCKCOUNTINGDETAILID"] = STOCKCOUNTINGDETAILID;
        }

        private void gvCounting_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvCounting.SetRowCellValue(e.RowHandle, "SNO", gvCounting.RowCount + 1);
            gvCounting.SetRowCellValue(e.RowHandle, "STOCKCOUNTINGDETAILID", -1);
            gvCounting.SetRowCellValue(e.RowHandle, "STOCKCOUNTINGID", stockCounting.STOCKCOUNTINGID);
            gvCounting.SetRowCellValue(e.RowHandle, "ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            gvCounting.SetRowCellValue(e.RowHandle, "ITEMNAME", sluItemCode.Text);
            gvCounting.SetRowCellValue(e.RowHandle, "ITEMCODE", txtItemCode.EditValue);
            gvCounting.SetRowCellValue(e.RowHandle, "MRP", drSelectedPrice["MRP"]);
            gvCounting.SetRowCellValue(e.RowHandle, "SALEPRICE", drSelectedPrice["SALEPRICE"]);
            gvCounting.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
            gvCounting.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            TextEdit textedit = sender as TextEdit;
            textedit.SelectAll();
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvCounting.FocusedRowHandle < 0)
                return;
            new CloudRepository().DeleteStockCounting(gvCounting.GetFocusedRowCellValue("STOCKCOUNTINGDETAILID"));
            gvCounting.DeleteRow(gvCounting.FocusedRowHandle);
        }

        private void frmCounting_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frmCounting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                sluItemCode.Focus();
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnSave_Click(null, null);
            }
        }

        private void gvCounting_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY" || isEventCall) return;
            SaveStockCountingDetail(e.RowHandle);
        }
    }
}