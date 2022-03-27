using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ErrorManagement;
using DataAccess;

namespace NSRetail
{
    public partial class frmMRPList : DevExpress.XtraEditors.XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;
        public frmMRPList(DataTable _dtMRP,bool IsItemListCall = false)
        {
            InitializeComponent();
            gcDelete.Visible = IsItemListCall;
            gcSalePrice.OptionsColumn.AllowEdit = IsItemListCall;
            gcMRPList.DataSource = _dtMRP;
            
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
            if (gvMRPList.FocusedRowHandle < 0)
                return;
            try
            {
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