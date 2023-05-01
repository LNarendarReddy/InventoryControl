using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmGroupItems : XtraForm
    {
        object ItemGroupID = null;
        object OfferID = null;
        bool IsGroupItem = false;
        DataTable dtItems = new DataTable();
        bool isExcludeList;

        public frmGroupItems(object GroupName, object _ItemGroupID,
            object OfferName = null, object _OfferID = null, bool _IsGroupItem = true, bool isExclude = false)
        {
            InitializeComponent();
            IsGroupItem = _IsGroupItem;
            if (IsGroupItem)
            {
                lcbtnimport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Text = "Group Items - " + GroupName;
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

            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
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
                int OfferItemID = new OfferRepository().SaveOfferItem(OfferID,
                    cmbItemCode.EditValue, Utility.UserID);
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
        }

        private void frmGroupItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbItemCode.EditValue == null) return;
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
            cmbItemCode.EditValue = null;
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
                        DataTable dtTemp = dt.Clone();
                        List<string> allowedColumns = new List<string> { "ITEMCODE", "OFFERTYPE", "OFFERVALUE" };

                        dtTemp.Columns.Cast<DataColumn>().Where(x => !allowedColumns.Contains(x.ColumnName))
                            .ToList().ForEach(x => dtTemp.Columns.Remove(x));

                        if (!dtTemp.Columns.Contains("OFFERTYPE")) dtTemp.Columns.Add("OFFERTYPE", typeof(string));
                        if (!dtTemp.Columns.Contains("OFFERVALUE")) dtTemp.Columns.Add("OFFERVALUE", typeof(string));

                        new OfferRepository().ImportOfferItems(OfferID, dtTemp, Utility.UserID);
                        gcItems.DataSource = dtItems = new OfferRepository().GetOfferItem(OfferID);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}