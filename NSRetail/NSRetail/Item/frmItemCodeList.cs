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
                dtItemCodes = new ItemCodeRepository().GetItemCodes();
                btnEdit.Enabled = false;
                btnVisualize.Enabled = false;
                dtItems = new DataView(dtItemCodes).ToTable(true, "ITEMID", "ITEMNAME", "SKUCODE", "DESCRIPTION");
                gcItemList.DataSource = dtItemCodes;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                Utility.Setfocus(gvItemList, "ITEMID", itemObj.ItemID);
            }
        }

        private void gcItemList_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void gvItemList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool enabled = gvItemList.FocusedRowHandle >= 0;
            btnEdit.Enabled = enabled;
            btnVisualize.Enabled = enabled;
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
    }
}
