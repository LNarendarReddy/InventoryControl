using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmItemCodeList : DevExpress.XtraEditors.XtraForm
    {
        #region Private Variables 

        DataTable dtItemCodes;
        DataTable dtItems;

        #endregion

        #region Constructor

        public frmItemCodeList()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public GridView ItemCodeListGridView { get { return gvItemList; } }

        #endregion

        #region Grid Button events


        private void btnNew_Click(object sender, EventArgs e)
        {
            Item itemObj = new Item();
            new frmItemCode(itemObj) { Owner = this }.ShowDialog();

            Utility.Setfocus(gvItemList, "ITEMCODEID", itemObj.ItemCodeID);

            if(itemObj.IsSave && Convert.ToBoolean(itemObj.IsNewToggleSwitched))
            {
                btnNew_Click(sender, e);
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemList.FocusedRowHandle >= 0)
                {
                    Item itemObj = new Item() { ItemCode = gvItemList.GetFocusedRowCellValue("ITEMCODE") };
                    new frmItemCode(itemObj) { Owner = this }.ShowDialog();

                    if (Convert.ToBoolean(itemObj.IsNewToggleSwitched))
                    {
                        btnNew_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnVisualize_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemList.FocusedRowHandle >= 0)
                {
                    new frmItemVisualize(gvItemList.GetFocusedRowCellValue("ITEMID")).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }


        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void frmItemList_Load(object sender, EventArgs e)
        {
            try
            {
                EnableDisableControls(false);
                dtItemCodes = Utility.GetItemCodeList();
                dtItems = Utility.GetItemSKUList();
                gcItemList.DataSource = dtItemCodes;
                ((frmMain)this.MdiParent).RefreshBaseLineData += FrmItemCodeList_RefreshBaseLineData;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void FrmItemCodeList_RefreshBaseLineData(object sender, EventArgs e)
        {
            dtItemCodes = Utility.GetItemCodeList();
            dtItems = Utility.GetItemSKUList();
            gcItemList.DataSource = dtItemCodes;
        }

        private void gcItemList_DoubleClick(object sender, EventArgs e)
        {
            //btnEdit_Click(null, null);
        }

        private void gvItemList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EnableDisableControls(gvItemList.FocusedRowHandle >= 0);
        }

        private void gcItemList_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEdit_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(null, null);
            }
        }

        private void EnableDisableControls(bool enabled)
        {
            btnEdit.Enabled = enabled;
            btnVisualize.Enabled = enabled;
        }

        private void gvItemList_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                e.Allow = false;
                pmItemList.ShowPopup(gcItemList.PointToScreen(e.Point));
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnVisualize_Click(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItemList.ShowRibbonPrintPreview();
        }
    }
}
