using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmItemGroupList : DevExpress.XtraEditors.XtraForm
    {
        object ItemGroupID = 0;
        public frmItemGroupList()
        {
            InitializeComponent();
        }
        private void frmItemGroup_Load(object sender, EventArgs e)
        {
            gcItemGroup.DataSource = new OfferRepository().GetItemGroup();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtGroupName.EditValue == null) return;
            new OfferRepository().SaveItemGroup(ItemGroupID, txtGroupName.EditValue,
                chkIsActive.EditValue, Utility.UserID);
            int rowhandle = gvItemGroup.LocateByValue("ITEMGROUPID", ItemGroupID);
            if (rowhandle >= 0)
            {
                gvItemGroup.SetRowCellValue(rowhandle, "GROUPNAME", txtGroupName.EditValue);
                if (Convert.ToBoolean(chkIsActive.EditValue))
                    gvItemGroup.SetRowCellValue(rowhandle, "ISACTIVE", "YES");
                else
                    gvItemGroup.SetRowCellValue(rowhandle, "ISACTIVE", "NO");
                gvItemGroup.FocusedRowHandle = rowhandle;
                ClearGroupData();
            }
            else
                gvItemGroup.AddNewRow();
        }
        private void ClearGroupData()
        {
            ItemGroupID = 0;
            txtGroupName.EditValue = null;
            chkIsActive.EditValue = false;
            txtGroupName.Focus();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvItemGroup.FocusedRowHandle < 0) return;
            ItemGroupID = gvItemGroup.GetFocusedRowCellValue("ITEMGROUPID");
            txtGroupName.EditValue = gvItemGroup.GetFocusedRowCellValue("GROUPNAME");
            if (Convert.ToString(gvItemGroup.GetFocusedRowCellValue("ISACTIVE")) == "YES")
                chkIsActive.EditValue = true;
            else
                chkIsActive.EditValue = false;
        }
        private void btnViewItems_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmGroupItems obj = new frmGroupItems(gvItemGroup.GetFocusedRowCellValue("GROUPNAME"),
                gvItemGroup.GetFocusedRowCellValue("ITEMGROUPID"))
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }
        private void gvItemGroup_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvItemGroup.SetRowCellValue(e.RowHandle, "ITEMGROUPID", ItemGroupID);
            gvItemGroup.SetRowCellValue(e.RowHandle, "GROUPNAME", txtGroupName.EditValue);
            if (Convert.ToBoolean(chkIsActive.EditValue))
                gvItemGroup.SetRowCellValue(e.RowHandle, "ISACTIVE", "YES");
            else
                gvItemGroup.SetRowCellValue(e.RowHandle, "ISACTIVE", "NO");
            gvItemGroup.SetRowCellValue(e.RowHandle, "CREATEDBY", Utility.FullName);
            gvItemGroup.SetRowCellValue(e.RowHandle, "CREATEDDATE", DateTime.Now);
            ClearGroupData();
        }
        private void frmItemGroupList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                ClearGroupData();
        }
    }
}