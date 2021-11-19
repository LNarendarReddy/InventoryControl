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
    public partial class frmItemList : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtItems;

        public frmItemList()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Item itemObj = new Item();
            new frmItem(itemObj).ShowDialog();
            UpdateDataTable(itemObj, true);
        }

        private void frmItemList_Load(object sender, EventArgs e)
        {
            try
            {
                dtItems = new ItemRepository().GetItems();
                gcItemList.DataSource = dtItems;
                btnEdit.Enabled = false;
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
                    Item itemObj = new Item();
                    itemObj.ItemID = gvItemList.GetFocusedRowCellValue("ITEMID");
                    new frmItem(itemObj).ShowDialog();
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
                updateRow = dtItems.NewRow();
            }
            else
            {
                int rowHandle = gvItemList.LocateByValue("ITEMID", itemObj.ItemID);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    updateRow = dtItems.Rows[rowHandle];
                }
                else
                {
                    XtraMessageBox.Show("Refresh of grid failed");
                    return;
                }
            }

            updateRow["ITEMID"] = itemObj.ItemID;
            updateRow["ITEMNAME"] = itemObj.ItemName;
            updateRow["ITEMCODE"] = itemObj.ItemCode;
            updateRow["DESCRIPTION"] = itemObj.Description;
            updateRow["HSNCODE"] = itemObj.HSNCode;

            if (isNew)
            {
                dtItems.Rows.Add(updateRow);
                Utility.Setfocus(gvItemList, "ITEMID", itemObj.ItemID);
            }
        }

        private void gcItemList_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void gvItemList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnEdit.Enabled = gvItemList.FocusedRowHandle >= 0;
        }
    }
}
