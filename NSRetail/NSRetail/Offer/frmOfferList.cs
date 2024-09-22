using DataAccess;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.Import.WordML;
using Entity;
using NSRetail.Utilities;
using System;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmOfferList : XtraForm
    {
        Offer offer = null;
        bool _IsDeal = false;
        OfferType offerType;

        public frmOfferList(bool IsDeal = false)
        {
            InitializeComponent();
            _IsDeal = IsDeal;            
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            offer = new Offer();
            offer.OfferID = -1;
            if (ShowCreateForm())
                gvOffer.AddNewRow();
            
        }
        private bool ShowCreateForm(bool _IsEdit = false)
        {
            XtraForm xtraForm = _IsDeal ? (XtraForm)new frmCreateDeal() { offer = offer } : new frmCreateOffer() { offer = offer };
            xtraForm.ShowInTaskbar = false;
            xtraForm.StartPosition = FormStartPosition.CenterScreen;
            xtraForm.ShowDialog();

            return _IsDeal ? ((frmCreateDeal)xtraForm).IsSave : ((frmCreateOffer)xtraForm).IsSave;
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
            gvOffer.SetRowCellValue(rowhandle, "CATEGORYID", offer.CategoryID);
            gvOffer.SetRowCellValue(rowhandle, "CATEGORYNAME", offer.CategoryName);
            gvOffer.SetRowCellValue(rowhandle, "AppliesToID", offer.AppliesToID);
            gvOffer.SetRowCellValue(rowhandle, "AppliesToName", offer.AppliesToName);
            gvOffer.SetRowCellValue(rowhandle, "NUMBEROFITEMS", offer.NumberOfItems);
            gvOffer.SetRowCellValue(rowhandle, "OFFERTHRESHOLDPRICE", offer.OfferThresholdPrice);
            gvOffer.SetRowCellValue(rowhandle, "FREEITEMPRICEID", offer.FreeItemPriceID);
            gvOffer.SetRowCellValue(rowhandle, "SKUCODE", offer.SKUcode);
            gvOffer.SetRowCellValue(rowhandle, "ITEMCODE", offer.ItemCode);
            gvOffer.SetRowCellValue(rowhandle, "ITEMNAME", offer.ItemName);
            gvOffer.SetRowCellValue(rowhandle, "ISACTIVE", "YES");
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
            offer.NumberOfItems = gvOffer.GetFocusedRowCellValue("NUMBEROFITEMS");
            offer.FreeItemPriceID = gvOffer.GetFocusedRowCellValue("FREEITEMPRICEID");
            offer.OfferThresholdPrice = gvOffer.GetFocusedRowCellValue("OFFERTHRESHOLDPRICE");
            if (ShowCreateForm())
                UpdateGridRow(gvOffer.FocusedRowHandle, true);
        }
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvOffer.FocusedRowHandle < 0 ||
                Convert.ToString(gvOffer.GetFocusedRowCellValue("ISACTIVE")) == "NO" ||
                XtraMessageBox.Show("Are you sure to delete the offer?", "Delete Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes) 
                return;

            new OfferRepository().DeleteOffer(gvOffer.GetFocusedRowCellValue("OFFERID"),
                Utility.UserID);
            gvOffer.SetFocusedRowCellValue("ISACTIVE", "NO");
        }

        private void btnViewBranches_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmOfferBranch obj = new frmOfferBranch(gvOffer.GetFocusedRowCellValue("OFFERNAME"),
                gvOffer.GetFocusedRowCellValue("OFFERID"), offerType)
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }

        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(gvOffer.GetFocusedRowCellValue("AppliesToID").Equals(2))
                return;

            frmGroupItems obj = new frmGroupItems(null, null, gvOffer.GetFocusedRowCellValue("OFFERNAME"),
                gvOffer.GetFocusedRowCellValue("OFFERID"), false, gvOffer.GetFocusedRowCellValue("AppliesToID").Equals(3))
            { 
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterScreen
            };

            obj.ShowDialog();
        }
        private void frmOfferList_Load(object sender, EventArgs e)
        {
            gcOffer.DataSource = new OfferRepository().GetOffer(_IsDeal);
            gvOffer.Columns["ISACTIVE"].FilterInfo = new ColumnFilterInfo("ISACTIVE = 'YES'");
            gcSKUCode.Visible = _IsDeal;
            gcItemCode.Visible = _IsDeal;
            gcItemName.Visible = _IsDeal;
            gcAppliesto.Visible = !_IsDeal;
            gcCategory.Visible = !_IsDeal;
            gcOfferThresholdPrice.Visible = !_IsDeal;
            gcNoOfItems.Visible = true;

            gcDelete.VisibleIndex = gvOffer.VisibleColumns.Count;
            gcViewBranches.VisibleIndex = gvOffer.VisibleColumns.Count - 1;

            if (_IsDeal)
            {
                gcViewItems.Visible = true;
                gcViewItems.VisibleIndex = gvOffer.VisibleColumns.Count - 2;
                gcEdit.VisibleIndex = gvOffer.VisibleColumns.Count - 3;

                gcOfferName.Caption = "Deal Name";
                gcOfferType.Caption = "Deal Type";
                gcOfferValue.Caption = "Deal Value";

                gcNoOfItems.Caption = "No of Items to Buy";

                offerType = OfferType.Deal;
            }
            else
            {
                gcEdit.VisibleIndex = gvOffer.VisibleColumns.Count - 2;
                offerType = OfferType.Category;


            }

            string accessIdentifier = _IsDeal ? "AE372AD7-249C-4355-8529-1233FEB89C98" : "72F4C2BA-5F86-404F-BC27-3B4970AF5E6A";

            btnNew.Tag = $"{accessIdentifier}::Create";
            gcEdit.Tag = $"{accessIdentifier}::Update";
            gcDelete.Tag = $"{accessIdentifier}::Delete";
            gcViewBranches.Tag = _IsDeal ? "978FF449-7E8D-41F6-AFE7-C136154B8227" : "B076FE8D-4982-4A73-ADC6-58E2EDE1280D";

            AccessUtility.SetStatusByAccess(btnNew);
            AccessUtility.SetStatusByAccess(gcEdit, gcDelete, gcViewBranches);
        }
        private void gvOffer_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = (gvOffer.FocusedColumn == gcEdit ||
                gvOffer.FocusedColumn == gcViewBranches ||
                gvOffer.FocusedColumn == gcViewItems) &&
                !string.IsNullOrEmpty(Convert.ToString(gvOffer.GetFocusedRowCellValue("BASEOFFERID")));
        }
        private void gcOffer_Click(object sender, EventArgs e)
        {
            
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcOffer.ShowRibbonPrintPreview();
        }
    }
}