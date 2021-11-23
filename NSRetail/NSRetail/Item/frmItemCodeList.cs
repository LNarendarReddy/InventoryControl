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
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmItemCodeList : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtItemCodes;
        DataTable dtItems;

        public frmItemCodeList()
        {
            InitializeComponent();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Item itemObj = new Item();
            new frmItemCode(itemObj, dtItems).ShowDialog();
            UpdateDataTable(itemObj, true);
        }

        private void frmItemList_Load(object sender, EventArgs e)
        {
            try
            {
                EnableDisableControls(false);
                DataSet dsItemCodes = new ItemCodeRepository().GetItemCodes();
                dtItemCodes = dsItemCodes.Tables["ITEMCODES"];
                dtItems = dsItemCodes.Tables["ITEMS"];
                gcItemList.DataSource = dtItemCodes;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemList.FocusedRowHandle >= 0)
                {
                    Item itemObj = new Item() { ItemCodeID = gvItemList.GetFocusedRowCellValue("ITEMCODEID") };
                    new frmItemCode(itemObj, dtItems).ShowDialog();
                    UpdateDataTable(itemObj);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void UpdateDataTable(Item itemObj, bool isNew = false)
        {
            if (!itemObj.IsSave)
            {
                return;
            }

            DataRow updateRow;
            if (isNew)
            {
                updateRow = dtItemCodes.NewRow();
            }
            else
            {
                int rowHandle = gvItemList.LocateByValue("ITEMCODEID", itemObj.ItemCodeID);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    updateRow = dtItemCodes.Rows[rowHandle];
                }
                else
                {
                    XtraMessageBox.Show("Refresh of grid failed");
                    return;
                }
            }

            updateRow["ITEMCODEID"] = itemObj.ItemCodeID;
            updateRow["ITEMID"] = itemObj.ItemID ?? 0;
            updateRow["ITEMCODE"] = itemObj.ItemCode;
            updateRow["ITEMNAME"] = itemObj.ItemName;
            updateRow["SKUCODE"] = itemObj.SKUCode;
            updateRow["DESCRIPTION"] = itemObj.Description;

            if (isNew)
            {
                dtItemCodes.Rows.Add(updateRow);
                Utility.Setfocus(gvItemList, "ITEMCODEID", itemObj.ItemCodeID);
            }
        }

        private void gcItemList_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void gvItemList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EnableDisableControls(gvItemList.FocusedRowHandle >= 0);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void EnableDisableControls(bool enabled)
        {
            btnEdit.Enabled = enabled;
            btnVisualize.Enabled = enabled;
            btnDelete.Enabled = enabled;
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
    }
}
