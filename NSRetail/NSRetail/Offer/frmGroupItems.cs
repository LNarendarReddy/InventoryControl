using DataAccess;
using DevExpress.XtraEditors;
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
    public partial class frmGroupItems : DevExpress.XtraEditors.XtraForm
    {
        object ItemGroupID = null;
        object OfferID = null;
        bool IsGroupItem = false;
        DataTable dtItems = new DataTable();
        public frmGroupItems(object GroupName, object _ItemGroupID, 
            object OfferName = null, object _OfferID = null, bool _IsGroupItem = true)
        {
            InitializeComponent();
            IsGroupItem = _IsGroupItem;
            if (IsGroupItem)
            {
                this.Text = "Group Items - " + GroupName;
                ItemGroupID = _ItemGroupID;
                gcItems.DataSource = dtItems = new OfferRepository().GetItemGroupDetail(ItemGroupID);
            }
            else
            {
                this.Text = "Offer Items - " + OfferName;
                OfferID = _OfferID;
                gcItems.DataSource = dtItems = new OfferRepository().GetOfferItem(OfferID);
            }

            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
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

        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbItemCode.EditValue == null) return;
            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            if (gvItems.LocateByValue("ITEMCODEID", cmbItemCode.EditValue) >= 0)
            {
                XtraMessageBox.Show("Item Already Exists!");
                cmbItemCode.EditValue = null;
                cmbItemCode.Focus();
            }
            else
                gvItems.AddNewRow();
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
            cmbItemCode.EditValue = null;
            cmbItemCode.Focus();
            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
        }

        private void frmGroupItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void frmGroupItems_Load(object sender, EventArgs e)
        {

        }
    }
}