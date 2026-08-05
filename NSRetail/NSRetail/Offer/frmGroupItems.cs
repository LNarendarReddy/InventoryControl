using DataAccess;
using DevExpress.CodeParser;
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

        object ItemGroupID = null;
        object OfferID = null;
        bool IsGroupItem = false;
        DataTable dtItems = new DataTable();
        bool isExcludeList;

        bool IsEditMode = false;
        int EditRowHandle = -1;
        object EditOfferItemMapID = null;


        public frmGroupItems(object _groupName, object _ItemGroupID,
            object OfferName = null, object _OfferID = null, bool _IsGroupItem = true, bool isExclude = false)
        {
            InitializeComponent();
            IsGroupItem = _IsGroupItem;      
            ItemGroupID = _ItemGroupID;

            if (IsGroupItem)
            {
                lcbtnimport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Text = "Group Items - " + _groupName;
                ItemGroupID = _ItemGroupID;
                gcItems.DataSource = dtItems = new OfferRepository().GetItemGroupDetail(ItemGroupID);
            }
            else
            {
                this.Text = (isExclude ? "Offer Exclude Items - " : "Offer Items - ") + OfferName;
                OfferID = _OfferID;
                isExcludeList = isExclude;
                gcItems.DataSource = dtItems = new OfferRepository().GetOfferItem(OfferID);
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0 ||
                XtraMessageBox.Show("Are you sure to delete the offer?", "Delete Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            if (IsGroupItem)
            {
                if (gvItems.FocusedRowHandle < 0) return;
                new OfferRepository().DeleteItemGroupDetail(gvItems.GetFocusedRowCellValue("ITEMGROUPDETAILID"), Utility.UserID);
                gvItems.DeleteRow(gvItems.FocusedRowHandle);
            }
            else
            {
                if (gvItems.FocusedRowHandle < 0) return;
                new OfferRepository().DeleteOfferitem(gvItems.GetFocusedRowCellValue("OFFERITEMMAPID"), Utility.UserID);
                gvItems.DeleteRow(gvItems.FocusedRowHandle);
            }
        }

        private void gvItems_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (IsGroupItem)
            {
                int ItemGroupDetailID = new OfferRepository().SaveItemGroupDetail(ItemGroupID,
                    cmbItemCode.EditValue, Utility.UserID);
                gvItems.SetRowCellValue(e.RowHandle, "ITEMGROUPDETAILID", ItemGroupDetailID);
            }
            else
            {
                int OfferItemID = new OfferRepository().SaveOfferItem(0, OfferID,
                    cmbItemCode.EditValue, Utility.UserID, txtNoOfPieces.EditValue, BuildOfferItemConfigJson()
                    );
                gvItems.SetRowCellValue(e.RowHandle, "OFFERITEMMAPID", OfferItemID);
            }

            int rowhandle = cmbItemCodeView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            gvItems.SetRowCellValue(e.RowHandle, "ITEMCODEID", cmbItemCode.EditValue);
            gvItems.SetRowCellValue(e.RowHandle, "ITEMCODE", cmbItemCode.Text);
            gvItems.SetRowCellValue(e.RowHandle, "ITEMNAME", cmbItemCodeView.GetRowCellValue(rowhandle, "ITEMNAME"));
            gvItems.SetRowCellValue(e.RowHandle, "HSNCODE", cmbItemCodeView.GetRowCellValue(rowhandle, "HSNCODE"));
            gvItems.SetRowCellValue(e.RowHandle, "CATEGORYID", cmbItemCodeView.GetRowCellValue(rowhandle, "CATEGORYID"));
            gvItems.SetRowCellValue(e.RowHandle, "SUBCATEGORYID", cmbItemCodeView.GetRowCellValue(rowhandle, "SUBCATEGORYID"));
            gvItems.SetRowCellValue(e.RowHandle, "CATEGORYNAME", cmbItemCodeView.GetRowCellValue(rowhandle, "CATEGORYNAME"));
            gvItems.SetRowCellValue(e.RowHandle, "SUBCATEGORYNAME", cmbItemCodeView.GetRowCellValue(rowhandle, "SUBCATEGORYNAME"));
            gvItems.SetRowCellValue(e.RowHandle, "NUMBEROFPIECES", txtNoOfPieces.EditValue);

            if (!IsGroupItem)
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
                new OfferRepository().SaveOfferItem(
                    EditOfferItemMapID,
                    OfferID,
                    cmbItemCode.EditValue,
                    Utility.UserID,
                    txtNoOfPieces.EditValue,
                    BuildOfferItemConfigJson());

                gvItems.SetRowCellValue(EditRowHandle, "NUMBEROFPIECES", txtNoOfPieces.EditValue);
                SetOfferGridValues(EditRowHandle);

                ClearEditMode();
                ResetEntryControls();
                cmbItemCode.Focus();
                return;
            }

            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            if (gvItems.LocateByValue("ITEMCODEID", cmbItemCode.EditValue) >= 0)
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
            int rowHandle = gvItems.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
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
            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";

            AccessUtility.SetStatusByAccess(btnAdd, btnImport);
            AccessUtility.SetStatusByAccess(gcDelete);
            ResetEntryControls();
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (IsGroupItem || gvItems.FocusedRowHandle < 0) return;

            IsEditMode = true;
            EditRowHandle = gvItems.FocusedRowHandle;
            EditOfferItemMapID = gvItems.GetFocusedRowCellValue("OFFERITEMMAPID");

            cmbItemCode.EditValue = gvItems.GetFocusedRowCellValue("ITEMCODEID");
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
                PriceBasedOn = rgPriceBasedOn.EditValue,
                OfferPrice = txtOfferPrice.EditValue
            });
        }

        private bool ValidateOfferItemConfig()
        {
            if (IsGroupItem) return true;

            if (Convert.ToString(rgPriceBasedOn.EditValue) == PriceOffer &&
                (txtOfferPrice.EditValue == null || string.IsNullOrWhiteSpace(txtOfferPrice.Text)))
            {
                XtraMessageBox.Show("Offer Price is mandatory when Price Based On is Offer Price.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtOfferPrice.Focus();
                return false;
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

            if (!IsGroupItem)
            {
                rgPriceBasedOn.EditValue = PriceMRP;
                txtOfferPrice.EditValue = null;
            }
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
            gvItems.SetRowCellValue(rowHandle, "PriceBasedOn", rgPriceBasedOn.EditValue);
            gvItems.SetRowCellValue(rowHandle, "PriceBasedOnText", GetPriceBasedOnText(rgPriceBasedOn.EditValue));
            gvItems.SetRowCellValue(rowHandle, "OfferPrice", txtOfferPrice.EditValue);
        }
    }
}
