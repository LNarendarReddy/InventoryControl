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
                    itemObj.ItemName = gvItemList.GetFocusedRowCellValue("ITEMNAME");
                    itemObj.ItemCode = gvItemList.GetFocusedRowCellValue("ITEMCODE");
                    itemObj.Description = gvItemList.GetFocusedRowCellValue("DESCRIPTION");
                    itemObj.HSCNO = gvItemList.GetFocusedRowCellValue("HSCNO");
                    new frmItem(itemObj).ShowDialog();
                    UpdateDataTable(itemObj);

                    if (itemObj.IsSave)
                    {
                        //gcBranch.DataSource = objMasterRep.GetBranch();
                        //Utility.Setfocus(gvItemList, "BRANCHID", ObjBranch.BRANCHID);
                    }

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
            if (itemObj.IsSave)
            {
                DataRow addedItemRow = dtItems.NewRow();
                addedItemRow["ITEMID"] = itemObj.ItemID;
                addedItemRow["ITEMNAME"] = itemObj.ItemName;
                addedItemRow["ITEMCODE"] = itemObj.ItemCode;
                addedItemRow["DESCRIPTION"] = itemObj.Description;
                addedItemRow["HSCNO"] = itemObj.HSCNO;
                dtItems.Rows.Add(addedItemRow);
            }
        }
    }
}
