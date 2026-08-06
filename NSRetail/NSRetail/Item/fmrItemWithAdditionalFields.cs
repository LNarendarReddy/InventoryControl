using DataAccess;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
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
    public partial class fmrItemWithAdditionalFields : DevExpress.XtraEditors.XtraForm
    {
        bool isRevertingCellValue = false;
        readonly Dictionary<string, bool> cellSaveStatus = new Dictionary<string, bool>();
        readonly Dictionary<string, object> cellPreviousValues = new Dictionary<string, object>();

        public fmrItemWithAdditionalFields()
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

        private void fmrItemWithAdditionalFields_Load(object sender, EventArgs e)
        {
            ricmbBrand.DataSource = Utility.GetBrand();
            ricmbBrand.ValueMember = "BRANDID";
            ricmbBrand.DisplayMember = "BRANDNAME";

            ricmbManufa.DataSource = Utility.GetManufacturer();
            ricmbManufa.ValueMember = "MANUFACTURERID";
            ricmbManufa.DisplayMember = "MANUFACTURERNAME";

            ricmbRefundPath.DataSource = Utility.GetRefundPathData();
            ricmbRefundPath.ValueMember = "REFUNDPATHID";
            ricmbRefundPath.DisplayMember = "REFUNDPATHTEXT";

            ricmbIndentCategory.DataSource = Utility.GetEnumList("Supplier Indent Item Type");
            ricmbIndentCategory.ValueMember = "ENUMID";
            ricmbIndentCategory.DisplayMember = "ENUMVALUE";

            gcItemList.DataSource = new ItemCodeRepository().GetItemListWithAdditionalFields();
        }

        private void gvItemList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (isRevertingCellValue || e.RowHandle < 0 || !IsAdditionalFieldColumn(e.Column.FieldName)) return;

            try
            {
                new DataRepository().ExecuteNonQuery("USP_U_ITEM_ADDITIONAL_FIELDS", true,
                    new Dictionary<string, object>
                    {
                        { "ItemID", gvItemList.GetRowCellValue(e.RowHandle, "ITEMID") },
                        { "SupplietIndentItemTypeID", GetDbValue(gvItemList.GetRowCellValue(e.RowHandle, "SUPPLIERINDENTITEMTYPEID")) },
                        { "InnerCaseQty", GetDbValue(gvItemList.GetRowCellValue(e.RowHandle, "INNERCASEQTY")) },
                        { "OuterCaseQty", GetDbValue(gvItemList.GetRowCellValue(e.RowHandle, "OUTERCASEQTY")) },
                        { "ProductRank", GetDbValue(gvItemList.GetRowCellValue(e.RowHandle, "PRODUCTRANK")) },
                        { "BrandID", GetDbValue(gvItemList.GetRowCellValue(e.RowHandle, "BRANDID")) },
                        { "ManufacturerID", GetDbValue(gvItemList.GetRowCellValue(e.RowHandle, "MANUFACTURERID")) },
                        { "RefundPathID", GetDbValue(gvItemList.GetRowCellValue(e.RowHandle, "REFUNDPATHID")) },
                        { "UserID", Utility.UserID }
                    });

                gvItemList.FocusedRowHandle = e.RowHandle;
                gvItemList.FocusedColumn = e.Column;
                cellSaveStatus[GetCellKey(e.RowHandle, e.Column.FieldName)] = true;
                cellPreviousValues.Remove(GetCellKey(e.RowHandle, e.Column.FieldName));
                gvItemList.GetDataRow(e.RowHandle)?.AcceptChanges();
                gvItemList.InvalidateRowCell(e.RowHandle, e.Column);
            }
            catch (Exception ex)
            {
                RevertCellValue(e.RowHandle, e.Column.FieldName);
                gvItemList.FocusedRowHandle = e.RowHandle;
                gvItemList.FocusedColumn = e.Column;
                cellSaveStatus[GetCellKey(e.RowHandle, e.Column.FieldName)] = false;
                gvItemList.InvalidateRowCell(e.RowHandle, e.Column);
                new frmErrorDetails(ex).ShowDialog();
            }
        }

        private void gvItemList_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (isRevertingCellValue || e.RowHandle < 0 || !IsAdditionalFieldColumn(e.Column.FieldName)) return;

            string cellKey = GetCellKey(e.RowHandle, e.Column.FieldName);
            if (!cellPreviousValues.ContainsKey(cellKey))
            {
                cellPreviousValues[cellKey] = gvItemList.GetRowCellValue(e.RowHandle, e.Column);
            }
        }

        private void gvItemList_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || !IsAdditionalFieldColumn(e.Column.FieldName)) return;
            if (!cellSaveStatus.TryGetValue(GetCellKey(e.RowHandle, e.Column.FieldName), out bool isSaved)) return;

            e.DefaultDraw();

            Image statusImage = isSaved ? Properties.Resources.apply_16x16 : Properties.Resources.cancel_16x16;
            Rectangle imageBounds = new Rectangle(
                e.Bounds.Right - statusImage.Width - 3,
                e.Bounds.Top + ((e.Bounds.Height - statusImage.Height) / 2),
                statusImage.Width,
                statusImage.Height);

            e.Cache.DrawImage(statusImage, imageBounds);
            e.Handled = true;
        }

        private void gvItemList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            ClearCellSaveStatus(gvItemList.FocusedRowHandle, e.PrevFocusedColumn);
        }

        private void gvItemList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ClearCellSaveStatus(e.PrevFocusedRowHandle, gvItemList.FocusedColumn);
        }

        private bool IsAdditionalFieldColumn(string fieldName)
        {
            return fieldName == "SUPPLIERINDENTITEMTYPEID"
                || fieldName == "INNERCASEQTY"
                || fieldName == "OUTERCASEQTY"
                || fieldName == "PRODUCTRANK"
                || fieldName == "BRANDID"
                || fieldName == "MANUFACTURERID"
                || fieldName == "REFUNDPATHID";
        }

        private object GetDbValue(object value)
        {
            return value == null || string.IsNullOrWhiteSpace(Convert.ToString(value))
                ? DBNull.Value
                : value;
        }

        private string GetCellKey(int rowHandle, string fieldName)
        {
            return Convert.ToString(gvItemList.GetRowCellValue(rowHandle, "ITEMID")) + "|" + fieldName;
        }

        private void ClearCellSaveStatus(int rowHandle, DevExpress.XtraGrid.Columns.GridColumn column)
        {
            if (rowHandle < 0 || column == null || !IsAdditionalFieldColumn(column.FieldName)) return;

            string cellKey = GetCellKey(rowHandle, column.FieldName);
            if (cellSaveStatus.Remove(cellKey))
            {
                gvItemList.InvalidateRowCell(rowHandle, column);
            }
        }

        private void RevertCellValue(int rowHandle, string fieldName)
        {
            DataRow row = gvItemList.GetDataRow(rowHandle);
            if (row == null || !row.Table.Columns.Contains(fieldName)) return;

            string cellKey = GetCellKey(rowHandle, fieldName);
            object originalValue = cellPreviousValues.TryGetValue(cellKey, out object previousValue)
                ? previousValue
                : row.HasVersion(DataRowVersion.Original)
                    ? row[fieldName, DataRowVersion.Original]
                    : DBNull.Value;

            try
            {
                isRevertingCellValue = true;
                row[fieldName] = originalValue;
            }
            finally
            {
                cellPreviousValues.Remove(cellKey);
                isRevertingCellValue = false;
            }
        }
    }
}
