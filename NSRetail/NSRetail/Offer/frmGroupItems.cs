using DataAccess;
using DevExpress.CodeParser;
using DevExpress.XtraCharts.Designer.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Layout.Engine;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmGroupItems : XtraForm
    {
        const string PriceMRP = "MRP";
        const string PriceSale = "SP";
        const string PriceOffer = "OP";

        object OfferID = null;
        DataTable dtItems = new DataTable();
        bool isExcludeList;

        bool IsEditMode = false;
        int EditRowHandle = -1;
        object EditOfferItemMapID = null;

        public frmGroupItems(object OfferTypeId, object OfferName, 
            object _OfferID, bool isExclude, object NOOfFreeItems)
        {
            InitializeComponent();

            this.Text = (isExclude ? "Offer Exclude Items - " : "Offer Items - ") + OfferName;
            OfferID = _OfferID;
            isExcludeList = isExclude;
            gcItems.DataSource = dtItems = new OfferRepository().GetOfferItem(OfferID);
            chkIsFreeItem.Checked = false;
            chkIsFreeItem.Enabled = !NOOfFreeItems.Equals(0) && OfferTypeId.Equals(1006);
            rgPriceBasedOn.Enabled = OfferTypeId.Equals(1006);
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0 ||
                XtraMessageBox.Show("Are you sure to delete the offer?", "Delete Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            if (gvItems.FocusedRowHandle < 0) return;
            new OfferRepository().DeleteOfferitem(gvItems.GetFocusedRowCellValue("OFFERITEMMAPID"), Utility.UserID);
            gvItems.DeleteRow(gvItems.FocusedRowHandle);
        }

        private void gvItems_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            object itemCodeID = GetSelectedItemCodeID();
            int OfferItemID = new OfferRepository().SaveOfferItem(0, OfferID,
                itemCodeID, Utility.UserID, txtNoOfPieces.EditValue, BuildOfferItemConfigJson());
            gvItems.SetRowCellValue(e.RowHandle, "OFFERITEMMAPID", OfferItemID);

            SetSelectedItemGridValues(e.RowHandle);
            gvItems.SetRowCellValue(e.RowHandle, "NUMBEROFPIECES", txtNoOfPieces.EditValue);

            SetOfferGridValues(e.RowHandle);
        }

        private void frmGroupItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            if (!ValidateOfferItemConfig()) return;

            if (IsEditMode)
            {
                object itemCodeID = GetSelectedItemCodeID();
                new OfferRepository().SaveOfferItem(
                    EditOfferItemMapID,
                    OfferID,
                    itemCodeID,
                    Utility.UserID,
                    txtNoOfPieces.EditValue,
                    BuildOfferItemConfigJson());

                SetSelectedItemGridValues(EditRowHandle);
                gvItems.SetRowCellValue(EditRowHandle, "NUMBEROFPIECES", txtNoOfPieces.EditValue);
                SetOfferGridValues(EditRowHandle);
                gvItems.RefreshRow(EditRowHandle);

                ClearEditMode();
                ResetEntryControls();
                cmbItemCode.Focus();
                return;
            }

            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            if (GetExistingItemRowHandle() >= 0)
            {
                XtraMessageBox.Show("Item Already Exists!");
                cmbItemCode.EditValue = null;
                cmbItemCode.Focus();
                return;
            }
            else
                gvItems.AddNewRow();

            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            int rowHandle = GetExistingItemRowHandle();
            ResetEntryControls();
            cmbItemCode.Focus();
            gvItems.FocusedRowHandle = rowHandle;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                XtraOpenFileDialog xtraOpenFileDialog1 = new XtraOpenFileDialog();
                xtraOpenFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                xtraOpenFileDialog1.Filter = "excel files (*.xls,*.xlsx)|*.xls,*.xlsx";

                if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = xtraOpenFileDialog1.FileName;
                    DataTable dt = Utility.ImportExcelXLS(filePath);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dtTemp = dt.Copy();
                        List<string> allowedColumns = new List<string> { "ITEMCODE", "OFFERTYPE", "OFFERVALUE", "OFFERTHRESHOLD" };

                        dtTemp.Columns.Cast<DataColumn>().Where(x => !allowedColumns.Contains(x.ColumnName))
                            .ToList().ForEach(x => dtTemp.Columns.Remove(x));

                        if (!dtTemp.Columns.Contains("OFFERTYPE")) dtTemp.Columns.Add("OFFERTYPE", typeof(string));
                        if (!dtTemp.Columns.Contains("OFFERVALUE")) dtTemp.Columns.Add("OFFERVALUE", typeof(string));

                        int i = 0;
                        foreach (string s in allowedColumns)
                        {
                            if (!dtTemp.Columns.Contains(s))
                                throw new Exception($"{s} column is missed in import file");
                            else
                            {
                                dtTemp.Columns[s].SetOrdinal(i);
                                i++;
                            }
                        }

                        new OfferRepository().ImportOfferItems(OfferID, dtTemp, Utility.UserID);
                        gcItems.DataSource = dtItems = new OfferRepository().GetOfferItem(OfferID);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.AppLog.Error(ex);
            }
        }

        private void frmGroupItems_Load(object sender, EventArgs e)
        {
            cmbItemCode.Properties.DataSource = new ItemCodeRepository().GetItemPriceList();
            cmbItemCode.Properties.ValueMember = "ITEMPRICEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
            cmbItemCodeView.BestFitColumns();

            AccessUtility.SetStatusByAccess(btnAdd, btnImport);
            AccessUtility.SetStatusByAccess(gcDelete);
            ResetEntryControls();
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0) return;

            IsEditMode = true;
            EditRowHandle = gvItems.FocusedRowHandle;
            EditOfferItemMapID = gvItems.GetFocusedRowCellValue("OFFERITEMMAPID");

            chkIsFreeItem.Checked = ToBoolean(gvItems.GetFocusedRowCellValue("IsFreeItem"));
            cmbItemCode.EditValue = gvItems.GetFocusedRowCellValue("ITEMPRICEID");
            txtNoOfPieces.EditValue = gvItems.GetFocusedRowCellValue("NUMBEROFPIECES");
            rgPriceBasedOn.EditValue = gvItems.GetFocusedRowCellValue("PriceBasedOn");
            txtOfferPrice.EditValue = gvItems.GetFocusedRowCellValue("OfferPrice");

            cmbItemCode.Enabled = false;
            btnAdd.Text = "Update";
        }

        private void rgPriceBasedOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isOfferPrice = Convert.ToString(rgPriceBasedOn.EditValue) == PriceOffer;
            txtOfferPrice.Enabled = isOfferPrice;

            if (!isOfferPrice)
            {
                txtOfferPrice.EditValue = null;
            }
        }

        private string BuildOfferItemConfigJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                IsFreeItem = chkIsFreeItem.Checked ? 1 : 0,
                ItemPriceID = cmbItemCode.EditValue,
                PriceBasedOn = rgPriceBasedOn.EditValue,
                OfferPrice = txtOfferPrice.EditValue
            });
        }

        private bool ValidateOfferItemConfig()
        {
            if (Convert.ToString(rgPriceBasedOn.EditValue) == PriceOffer &&
                (txtOfferPrice.EditValue == null || string.IsNullOrWhiteSpace(txtOfferPrice.Text)))
            {
                XtraMessageBox.Show("Offer Price is mandatory when Price Based On is Offer Price.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtOfferPrice.Focus();
                return false;
            }

            if (Convert.ToString(rgPriceBasedOn.EditValue) == PriceOffer)
            {
                decimal offerPrice;
                decimal mrp;
                object mrpValue = GetSelectedItemValue("MRP");

                if (decimal.TryParse(Convert.ToString(txtOfferPrice.EditValue), out offerPrice) &&
                    mrpValue != null && mrpValue != DBNull.Value &&
                    decimal.TryParse(Convert.ToString(mrpValue), out mrp) &&
                    offerPrice > mrp)
                {
                    XtraMessageBox.Show("Offer Price cannot be greater than MRP.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtOfferPrice.Focus();
                    return false;
                }
            }

            return true;
        }

        private string GetPriceBasedOnText(object value)
        {
            string priceBasedOn = Convert.ToString(value);

            return priceBasedOn == PriceMRP ? "MRP"
                : priceBasedOn == PriceSale ? "Sale Price"
                : priceBasedOn == PriceOffer ? "Offer Price"
                : string.Empty;
        }

        private void ResetEntryControls()
        {
            cmbItemCode.EditValue = null;
            rgPriceBasedOn.EditValue = PriceMRP;
            txtOfferPrice.EditValue = null;
        }

        private void ClearEditMode()
        {
            IsEditMode = false;
            EditRowHandle = -1;
            EditOfferItemMapID = null;
            txtNoOfPieces.EditValue = null;
            cmbItemCode.Enabled = true;
            btnAdd.Text = "Add Item";
        }

        private void SetOfferGridValues(int rowHandle)
        {
            gvItems.SetRowCellValue(rowHandle, "IsFreeItem", chkIsFreeItem.Checked);
            gvItems.SetRowCellValue(rowHandle, "OfferItemType", GetOfferItemTypeText());
            gvItems.SetRowCellValue(rowHandle, "PriceBasedOn", rgPriceBasedOn.EditValue);
            gvItems.SetRowCellValue(rowHandle, "PriceBasedOnText", GetPriceBasedOnText(rgPriceBasedOn.EditValue));
            gvItems.SetRowCellValue(rowHandle, "OfferPrice", txtOfferPrice.EditValue);
        }

        private void SetSelectedItemGridValues(int rowHandle)
        {
            gvItems.SetRowCellValue(rowHandle, "ITEMCODEID", GetSelectedItemCodeID());
            gvItems.SetRowCellValue(rowHandle, "ITEMPRICEID", cmbItemCode.EditValue);
            gvItems.SetRowCellValue(rowHandle, "MRP", GetSelectedItemValue("MRP"));
            gvItems.SetRowCellValue(rowHandle, "ITEMCODE", GetSelectedItemValue("ITEMCODE") ?? cmbItemCode.Text);
            gvItems.SetRowCellValue(rowHandle, "ITEMNAME", GetSelectedItemValue("ITEMNAME"));
            gvItems.SetRowCellValue(rowHandle, "HSNCODE", GetSelectedItemValue("HSNCODE"));
            gvItems.SetRowCellValue(rowHandle, "CATEGORYID", GetSelectedItemValue("CATEGORYID"));
            gvItems.SetRowCellValue(rowHandle, "SUBCATEGORYID", GetSelectedItemValue("SUBCATEGORYID"));
            gvItems.SetRowCellValue(rowHandle, "CATEGORYNAME", GetSelectedItemValue("CATEGORYNAME"));
            gvItems.SetRowCellValue(rowHandle, "SUBCATEGORYNAME", GetSelectedItemValue("SUBCATEGORYNAME"));
        }

        private string GetOfferItemTypeText()
        {
            return chkIsFreeItem.Checked ? "Free Item" : "Buying Item";
        }

        private int GetSelectedItemPriceRowHandle()
        {
            return cmbItemCodeView.LocateByValue("ITEMPRICEID", cmbItemCode.EditValue);
        }

        private DataRow GetSelectedItemPriceRow()
        {
            if (cmbItemCode.EditValue == null || cmbItemCode.EditValue == DBNull.Value) return null;

            DataTable itemPriceTable = cmbItemCode.Properties.DataSource as DataTable;
            DataView itemPriceView = cmbItemCode.Properties.DataSource as DataView;

            if (itemPriceTable == null && itemPriceView != null)
                itemPriceTable = itemPriceView.Table;

            if (itemPriceTable == null || !itemPriceTable.Columns.Contains("ITEMPRICEID")) return null;

            return itemPriceTable.AsEnumerable()
                .FirstOrDefault(x => Convert.ToString(x["ITEMPRICEID"]) == Convert.ToString(cmbItemCode.EditValue));
        }

        private object GetSelectedItemValue(string columnName)
        {
            DataRow row = GetSelectedItemPriceRow();

            if (row != null && row.Table.Columns.Contains(columnName))
                return row[columnName];

            int rowhandle = GetSelectedItemPriceRowHandle();
            return rowhandle < 0 ? null : cmbItemCodeView.GetRowCellValue(rowhandle, columnName);
        }

        private object GetSelectedItemCodeID()
        {
            return GetSelectedItemValue("ITEMCODEID");
        }

        private int GetExistingItemRowHandle()
        {
            return chkIsFreeItem.Checked
                ? gvItems.LocateByValue("ITEMPRICEID", cmbItemCode.EditValue)
                : gvItems.LocateByValue("ITEMCODEID", GetSelectedItemCodeID());
        }

        private bool ToBoolean(object value)
        {
            if (value == null || value == DBNull.Value) return false;
            return Convert.ToBoolean(value);
        }
    }
}
