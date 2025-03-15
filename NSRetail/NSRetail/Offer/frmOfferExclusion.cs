using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Entity;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Filtering.DataItemsExtension;
using static Dropbox.Api.TeamLog.MemberRemoveActionType;

namespace NSRetail
{
    public partial class frmOfferExclusion : DevExpress.XtraEditors.XtraForm
    {
        object OfferID = null;
        DataTable dtItems = new DataTable();
        bool isCategory = false;
        public frmOfferExclusion(object OfferName, object _OfferID = null)
        {
            InitializeComponent();

            this.Text = ("Offer Exclude Items - ") + OfferName;
            OfferID = _OfferID;
            gcItems.DataSource = dtItems = new OfferRepository().GetOfferExclusion(OfferID);
        }

        private void frmOfferExclusion_Load(object sender, EventArgs e)
        {
            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";

            gluCategory.Properties.DataSource = Utility.GetCategoryListExceptAll();
            gluCategory.Properties.DisplayMember = "CATEGORYNAME";
            gluCategory.Properties.ValueMember = "CATEGORYID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbItemCode.EditValue?.ToString()))
                return;
            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            if (gvItems.LocateByValue("EXCLUSIONID", cmbItemCode.EditValue) >= 0)
            {
                XtraMessageBox.Show("Item Already Exists!");
                cmbItemCode.EditValue = null;
                cmbItemCode.Focus();
                return;
            }
            else
            {
                isCategory = false;
                gvItems.AddNewRow();
            }

            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            int rowHandle = gvItems.LocateByValue("EXCLUSIONID", cmbItemCode.EditValue);
            cmbItemCode.EditValue = null;
            cmbItemCode.Focus();
            gvItems.FocusedRowHandle = rowHandle;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (gluCategory.EditValue == null)
                return;
            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            if (gvItems.LocateByValue("EXCLUSIONID", gluCategory.EditValue) >= 0)
            {
                XtraMessageBox.Show("Category already exists!");
                gluCategory.EditValue = null;
                gluCategory.Focus();
                return;
            }
            else
            {
                isCategory = true;
                gvItems.AddNewRow();
            }

            gvItems.GridControl.BindingContext = new BindingContext();
            gvItems.GridControl.DataSource = dtItems;
            int rowHandle = gvItems.LocateByValue("EXCLUSIONID", gluCategory.EditValue);
            gluCategory.EditValue = null;
            gluCategory.Focus();
            gvItems.FocusedRowHandle = rowHandle;
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0 ||
                XtraMessageBox.Show("Are you sure to delete?", "Delete Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            try
            {
                if (gvItems.FocusedRowHandle < 0) return;
                new OfferRepository().DeleteOfferExclusion(gvItems.GetFocusedRowCellValue("OFFEREXCLUSIONID"), Utility.UserID);
                gvItems.DeleteRow(gvItems.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void frmOfferExclusion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void gvItems_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int OFFEREXCLUSIONID = new OfferRepository().SaveOfferExclusion(
                OfferID,
                isCategory ? gluCategory.EditValue : cmbItemCode.EditValue, 
                Utility.UserID, 
                isCategory);
            gvItems.SetRowCellValue(e.RowHandle, "OFFEREXCLUSIONID", OFFEREXCLUSIONID);
            gvItems.SetRowCellValue(e.RowHandle, "OFFERID", OfferID);
            gvItems.SetRowCellValue(e.RowHandle, "EXCLUSIONID", isCategory ? gluCategory.EditValue : cmbItemCode.EditValue);
            if (isCategory)
            {
                gvItems.SetRowCellValue(e.RowHandle, "EXCLUSIONNAME", gluCategory.Text);
            }
            else
            {
                int rowhandle = cmbItemCodeView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
                gvItems.SetRowCellValue(e.RowHandle, "EXCLUSIONNAME", cmbItemCodeView.GetRowCellValue(rowhandle, "ITEMNAME"));
            }
            gvItems.SetRowCellValue(e.RowHandle, "EXCLUSIONCODE", isCategory ? gluCategory.Text : cmbItemCode.Text);
            gvItems.SetRowCellValue(e.RowHandle, "EXCLUSIONTYPE", isCategory ? "CATEGORY" : "ITEM");
            gvItems.SetRowCellValue(e.RowHandle, "CREATEDBY", Utility.FullName);
            gvItems.SetRowCellValue(e.RowHandle, "CREATEDDATE", DateTime.Now);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }
    }
}