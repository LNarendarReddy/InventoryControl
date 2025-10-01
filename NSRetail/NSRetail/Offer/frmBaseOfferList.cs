using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Entity;
using NSRetail.Utilities;
using System;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmBaseOfferList : DevExpress.XtraEditors.XtraForm
    {
        OfferRepository offerRepository = new OfferRepository();
        BaseOffer baseOffer = null;
        public frmBaseOfferList()
        {
            InitializeComponent();
        }

        private void frmBaseOfferList_Load(object sender, EventArgs e)
        {
            gcOffer.DataSource = offerRepository.GetBaseOffer();
            gvOffer.Columns["ISACTIVE"].FilterInfo = new ColumnFilterInfo("ISACTIVE = 'YES'");

            AccessUtility.SetStatusByAccess(btnNewOffer);
            AccessUtility.SetStatusByAccess(gcEdit, gcDelete, gcBranches, gcOffers);
        }

        private void btnNewOffer_Click(object sender, EventArgs e)
        {
            baseOffer = new BaseOffer();
            baseOffer.BASEOFFERID = 0;
            frmCreateBaseOffer obj = new frmCreateBaseOffer(baseOffer);
            obj.ShowInTaskbar= false;
            obj.IconOptions.ShowIcon= false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
            if(baseOffer.IsSave)
            {
                gvOffer.AddNewRow();
            }
        }

        private void gvOffer_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["BASEOFFERID"], baseOffer.BASEOFFERID);
            view.SetRowCellValue(e.RowHandle, view.Columns["OFFERCODE"], baseOffer.OFFERCODE);
            view.SetRowCellValue(e.RowHandle, view.Columns["OFFERNAME"], baseOffer.OFFERNAME);
            view.SetRowCellValue(e.RowHandle, view.Columns["CATEGORYNAME"], baseOffer.CATEGORYNAME);
            view.SetRowCellValue(e.RowHandle, view.Columns["CATEGORYID"], baseOffer.CATEGORYID);
            view.SetRowCellValue(e.RowHandle, view.Columns["STARTDATE"], baseOffer.STARTDATE);
            view.SetRowCellValue(e.RowHandle, view.Columns["ENDDATE"], baseOffer.ENDDATE);
            view.SetRowCellValue(e.RowHandle, view.Columns["CREATEDBY"], Utility.UserID);
            view.SetRowCellValue(e.RowHandle, view.Columns["CREATEDDATE"], DateTime.Now);
            view.SetRowCellValue(e.RowHandle, view.Columns["ISACTIVE"], "YES");
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvOffer.FocusedRowHandle < 0)
                return; 
            BaseOffer baseOffer = new BaseOffer();
            baseOffer.BASEOFFERID = gvOffer.GetFocusedRowCellValue("BASEOFFERID");
            baseOffer.OFFERCODE = gvOffer.GetFocusedRowCellValue("OFFERCODE");
            baseOffer.OFFERNAME = gvOffer.GetFocusedRowCellValue("OFFERNAME");
            baseOffer.CATEGORYID = gvOffer.GetFocusedRowCellValue("CATEGORYID");
            baseOffer.STARTDATE = gvOffer.GetFocusedRowCellValue("STARTDATE");
            baseOffer.ENDDATE = gvOffer.GetFocusedRowCellValue("ENDDATE");
            frmCreateBaseOffer obj = new frmCreateBaseOffer(baseOffer);
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
            if (baseOffer.IsSave)
            {
                int rowhdnle = gvOffer.FocusedRowHandle;
                gvOffer.SetFocusedRowCellValue("OFFERCODE", baseOffer.OFFERCODE);
                gvOffer.SetFocusedRowCellValue("OFFERNAME", baseOffer.OFFERNAME);
                gvOffer.SetFocusedRowCellValue("STARTDATE", baseOffer.STARTDATE);
                gvOffer.SetFocusedRowCellValue("ENDDATE", baseOffer.ENDDATE);
                gvOffer.SetFocusedRowCellValue("UPDATEDBY", Utility.UserID);
                gvOffer.SetFocusedRowCellValue("UPDATEDDATE", DateTime.Now);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBranches_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmOfferBranch obj = new frmOfferBranch(gvOffer.GetFocusedRowCellValue("OFFERNAME"),
                gvOffer.GetFocusedRowCellValue("BASEOFFERID"), OfferType.Base, true)
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }

        private void btnOfferList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmCreateOfferList obj = new frmCreateOfferList(gvOffer.GetFocusedRowCellValue("BASEOFFERID"),
                gvOffer.GetFocusedRowCellValue("CATEGORYID"))
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvOffer.FocusedRowHandle < 0 ||
                Convert.ToString(gvOffer.GetFocusedRowCellValue("ISACTIVE")) == "NO" ||
                XtraMessageBox.Show("Are you sure to delete the offer?", "Delete Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            new OfferRepository().DeleteBaseOffer(gvOffer.GetFocusedRowCellValue("BASEOFFERID"),
                Utility.UserID);
            gvOffer.SetFocusedRowCellValue("ISACTIVE", "NO");
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcOffer.ShowRibbonPrintPreview();
        }
    }
}