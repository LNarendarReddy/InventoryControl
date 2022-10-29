using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using System;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmOfferList : XtraForm
    {
        Offer offer = null;
        public frmOfferList()
        {
            InitializeComponent();
            gcOffer.DataSource = new OfferRepository().GetOffer();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            offer = new Offer();
            offer.OfferID = -1;
            frmOfferManagement obj = new frmOfferManagement() 
            { ShowInTaskbar = false,StartPosition = FormStartPosition.CenterScreen,offer = offer };
            obj.ShowDialog();
            if (obj.IsSave)
                gvOffer.AddNewRow();
        }
        private void gvOffer_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            UpdateGridRow(e.RowHandle, false);
        }
        private void UpdateGridRow(int rowhandle, bool IsEdit)
        {
            gvOffer.SetRowCellValue(rowhandle, "OFFERID", offer.OfferID);
            gvOffer.SetRowCellValue(rowhandle, "OFFERCODE", offer.OfferCode);
            gvOffer.SetRowCellValue(rowhandle, "OFFERNAME", offer.OfferName);
            gvOffer.SetRowCellValue(rowhandle, "STARTDATE", offer.StartDate);
            gvOffer.SetRowCellValue(rowhandle, "ENDDATE", offer.EndDate);
            gvOffer.SetRowCellValue(rowhandle, "OFFERVALUE", offer.OfferValue);
            gvOffer.SetRowCellValue(rowhandle, "OFFERTYPEID", offer.OfferTypeID);
            gvOffer.SetRowCellValue(rowhandle, "OFFERTYPECODE", offer.OfferTypeCode);
            gvOffer.SetRowCellValue(rowhandle, "OFFERTYPENAME", offer.OfferTypeName);
            gvOffer.SetRowCellValue(rowhandle, "AppliesToID", offer.AppliesToID);
            gvOffer.SetRowCellValue(rowhandle, "AppliesToName", offer.AppliesToName);
            gvOffer.SetRowCellValue(rowhandle, "CATEGORYID", offer.CategoryID);
            gvOffer.SetRowCellValue(rowhandle, "CATEGORYNAME", offer.CategoryName);
            gvOffer.SetRowCellValue(rowhandle, "ITEMGROUPID", offer.ItemGroupID);
            gvOffer.SetRowCellValue(rowhandle, "GROUPNAME", offer.GroupName);
            if (Convert.ToBoolean(offer.IsActive))
                gvOffer.SetRowCellValue(rowhandle, "ISACTIVE", "YES");
            else
                gvOffer.SetRowCellValue(rowhandle, "ISACTIVE", "NO");
            if (!IsEdit)
            {
                gvOffer.SetRowCellValue(rowhandle, "CREATEDBY", Utility.FullName);
                gvOffer.SetRowCellValue(rowhandle, "CREATEDDATE", DateTime.Now);
            }
            else
            {
                gvOffer.SetRowCellValue(rowhandle, "UPDATEDBY", Utility.FullName);
                gvOffer.SetRowCellValue(rowhandle, "UPDATEDDATE", DateTime.Now);
            }
            offer = null;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvOffer.FocusedRowHandle < 0) return;
            offer = new Offer();
            offer.OfferID = gvOffer.GetFocusedRowCellValue("OFFERID");
            offer.OfferCode = gvOffer.GetFocusedRowCellValue("OFFERCODE");
            offer.OfferName = gvOffer.GetFocusedRowCellValue("OFFERNAME");
            offer.StartDate = gvOffer.GetFocusedRowCellValue("STARTDATE");
            offer.EndDate = gvOffer.GetFocusedRowCellValue("ENDDATE");
            offer.OfferValue = gvOffer.GetFocusedRowCellValue("OFFERVALUE");
            offer.OfferTypeID = gvOffer.GetFocusedRowCellValue("OFFERTYPEID");
            offer.AppliesToID = gvOffer.GetFocusedRowCellValue("AppliesToID");
            offer.OfferTypeCode = gvOffer.GetFocusedRowCellValue("OFFERTYPECODE");
            offer.OfferTypeName = gvOffer.GetFocusedRowCellValue("OFFERTYPENAME");
            offer.CategoryID = gvOffer.GetFocusedRowCellValue("CATEGORYID");
            offer.CategoryName = gvOffer.GetFocusedRowCellValue("CATEGORYNAME");
            offer.ItemGroupID = gvOffer.GetFocusedRowCellValue("ITEMGROUPID");
            offer.GroupName = gvOffer.GetFocusedRowCellValue("GROUPNAME");
            offer.NumberOfItems = gvOffer.GetFocusedRowCellValue("NUMBEROFITEMS");
            offer.FreeItemPriceID = gvOffer.GetFocusedRowCellValue("FREEITEMPRICEID");
            if (Convert.ToString(gvOffer.GetFocusedRowCellValue("ISACTIVE")) == "YES")
                offer.IsActive = true;
            else
                offer.IsActive = false;
            frmOfferManagement obj = new frmOfferManagement()
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen, offer = offer };
            obj.ShowDialog();
            if (obj.IsSave)
                UpdateGridRow(gvOffer.FocusedRowHandle, false);
        }
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvOffer.FocusedRowHandle < 0 ||
                XtraMessageBox.Show("Are you sure to delete the offer?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes) 
                return;

            new OfferRepository().DeleteOffer(gvOffer.GetFocusedRowCellValue("OFFERID"),
                Utility.UserID);
            gvOffer.DeleteRow(gvOffer.FocusedRowHandle);
        }
        private void btnViewBranches_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmOfferBranch obj = new frmOfferBranch(gvOffer.GetFocusedRowCellValue("OFFERNAME"),
                gvOffer.GetFocusedRowCellValue("OFFERID"))
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }
        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(!gvOffer.GetFocusedRowCellValue("AppliesToID").Equals(1))
                return;
            frmGroupItems obj = new frmGroupItems(null,null,gvOffer.GetFocusedRowCellValue("OFFERNAME"),
                gvOffer.GetFocusedRowCellValue("OFFERID"),false)
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }
    }
}