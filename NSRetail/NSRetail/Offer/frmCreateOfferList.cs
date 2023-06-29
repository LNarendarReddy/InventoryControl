using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmCreateOfferList : XtraForm
    {
        object BaseOfferID = null;
        object CategoryID = null;
        OfferRepository offerRepository = new OfferRepository();

        public frmCreateOfferList(object _BaseOfferID, object _CategoryID)
        {
            InitializeComponent();
            BaseOfferID = _BaseOfferID;
            CategoryID = _CategoryID;
        }
        private void frmCreateOfferList_Load(object sender, EventArgs e)
        {
            cmbOffertype.DataSource = offerRepository.GetOfferType(1);
            cmbOffertype.ValueMember = "OFFERTYPEID";
            cmbOffertype.DisplayMember = "OFFERTYPENAME";

            cmbItemCode.DataSource = new ItemCodeRepository().GetItemCodeByCategory(CategoryID);
            cmbItemCode.ValueMember = "ITEMCODEID";
            cmbItemCode.DisplayMember = "ITEMCODE";

            gcOffer.DataSource = offerRepository.GetOfferByBaseOffer(BaseOfferID);
        }
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvOffer.FocusedRowHandle < 0 ||
                    !int.TryParse(Convert.ToString(gvOffer.GetFocusedRowCellValue("OFFERID")), out int ivalue) ||
                    ivalue <= 0 ||
                XtraMessageBox.Show("Are you sure to delete the offer?", "Delete Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;

                new OfferRepository().DeleteOfferFromBase(
                    gvOffer.GetFocusedRowCellValue("OFFERID"),
                    gvOffer.GetFocusedRowCellValue("ITEMCODEID"),
                    Utility.UserID);
                gvOffer.DeleteRow(gvOffer.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void frmCreateOfferList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
        private void gvOffer_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, view.Columns["OFFERID"], 0);
            }
            catch (Exception ex) { }
        }
        private void gvOffer_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;     
                DataRow row = (e.Row as DataRowView).Row;
                Offer offer = new Offer();
                offer.OfferID = 0;
                offer.OfferTypeID = Convert.ToInt32(row["OFFERTYPEID"]);
                offer.OfferValue = Convert.ToInt32(row["OFFERVALUE"]);
                offer.ItemCodeID = Convert.ToInt32(row["ITEMCODEID"]);
                offer.OfferThreshold = Convert.ToInt32(row["NUMBEROFPIECES"]);
                offer.BaseOfferID = BaseOfferID;
                offer.UserID = Utility.UserID;
                new OfferRepository().SaveOfferFromBaseOffer(offer);
                row["OFFERID"] = offer.OfferID;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit editor = (SearchLookUpEdit)sender;
            DataRowView dvRow = editor.GetSelectedDataRow() as DataRowView;
            DataRow row = dvRow?.Row;
            gvOffer.SetFocusedRowCellValue("ITEMNAME", row?["ITEMNAME"]);
        }
        private void gvOffer_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvOffer.FocusedColumn != gcDelete &&
                int.TryParse(Convert.ToString(gvOffer.GetFocusedRowCellValue("OFFERID")), out int offerid) &&
                offerid > 0)
                e.Cancel = true;
        }
        private void gvOffer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.GetRowCellValue(e.RowHandle, gcItemCode) == DBNull.Value)
            {
                e.Valid = false;
                view.SetColumnError(gcItemCode, "Itemcode is mandatory");
            }

            if (view.GetRowCellValue(e.RowHandle, gcOfferType) == DBNull.Value)
            {
                e.Valid = false;
                view.SetColumnError(gcOfferType, "Offertype is mandatory");
            }
            
            if (view.GetRowCellValue(e.RowHandle, gcPieces) == DBNull.Value)
            {
                e.Valid = false;
                view.SetColumnError(gcOfferType, "No of Pieces is mandatory");
            }
            if (e.Valid)
                view.ClearColumnErrors();
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog xtraOpenFileDialog1 = new XtraOpenFileDialog();
            xtraOpenFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            xtraOpenFileDialog1.Filter = "excel files (*.xls,*.xlsx)|*.xls,*.xlsx";
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
                try
                {
                    string filePath = xtraOpenFileDialog1.FileName;
                    DataTable dt = Utility.ImportExcelXLS(filePath);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dtTemp = dt.Copy();
                        List<string> allowedColumns = new List<string> { "ITEMCODE", "OFFERTYPE", "OFFERVALUE" };

                        dtTemp.Columns.Cast<DataColumn>().Where(x => !allowedColumns.Contains(x.ColumnName))
                            .ToList().ForEach(x => dtTemp.Columns.Remove(x));
                                                
                        new OfferRepository().ImportOffer(BaseOfferID, CategoryID, dtTemp, Utility.UserID);
                        gcOffer.DataSource = offerRepository.GetOfferByBaseOffer(BaseOfferID);
                        SplashScreenManager.CloseOverlayForm(handle);
                    }
                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseOverlayForm(handle);
                    ErrorMgmt.ShowError(ex);
                    ErrorMgmt.Errorlog.Error(ex);
                }
            }
        }
    }
}