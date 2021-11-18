using System;
using DataAccess;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmItem : DevExpress.XtraEditors.XtraForm
    {
        Item itemObj;

        public frmItem(Item item)
        {
            InitializeComponent();
            itemObj = item;
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            if(Convert.ToInt32(itemObj.ItemID) > 0)
            {
                Text = "Edit Item - " + itemObj.ItemName;
                txtItemCode.EditValue = itemObj.ItemCode;
                txtItemName.EditValue = itemObj.ItemName;
                txtDescription.EditValue = itemObj.Description;
                txtHSCNo.EditValue = itemObj.HSCNO;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxItemValidationProvider.Validate())
                    return;
                itemObj.ItemCode = txtItemCode.EditValue;
                itemObj.ItemName = txtItemName.EditValue;
                itemObj.Description = txtDescription.EditValue;
                itemObj.HSCNO = txtHSCNo.EditValue;
                itemObj.UserID = Utility.UserID;
                new ItemRepository().SaveItem(itemObj);
                itemObj.IsSave = true;
                this.Close();

            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}
