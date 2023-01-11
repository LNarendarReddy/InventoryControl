using System;
using System.Data;
using System.Windows.Forms;
using ErrorManagement;
using DataAccess;
using DevExpress.XtraEditors;

namespace NSRetail
{
    public partial class frmMRPList : XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;
        public frmMRPList(DataTable _dtMRP,bool IsItemListCall = false, bool showCostPrice = false)
        {
            InitializeComponent();
            gcDelete.Visible = IsItemListCall && Utility.Role != "Division Manager" && Utility.Role != "Division User"; ;
            //gcSalePrice.OptionsColumn.AllowEdit = IsItemListCall;
            gcMRPList.DataSource = _dtMRP;

            gcCostPriceWT.Visible = showCostPrice;
            gcCostPriceWOT.Visible = showCostPrice;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _IsSave = true;
                drSelected = gvMRPList.GetFocusedRow();
                this.Close();
            }
            catch (Exception){}
        }

        private void frmMRPList_Load(object sender, EventArgs e)
        {

        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvMRPList.FocusedRowHandle < 0 || gvMRPList.RowCount == 1)
                return;
            try
            {
                if (XtraMessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                new ItemCodeRepository().DeleteItemPrice(gvMRPList.GetFocusedRowCellValue("ITEMPRICEID"),Utility.UserID);
                gvMRPList.DeleteRow(gvMRPList.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvMRPList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvMRPList.FocusedRowHandle < 0)
                return;
            try
            {
                new ItemCodeRepository().UpdateItemPrice(gvMRPList.GetFocusedRowCellValue("ITEMPRICEID"), Utility.UserID,
                    gvMRPList.GetFocusedRowCellValue("SALEPRICE"));
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}