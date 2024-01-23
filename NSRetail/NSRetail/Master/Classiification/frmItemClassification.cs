using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Master.Classiification
{
    public partial class frmItemClassification : XtraForm
    {
        ItemClassification itemClassificationObj;
        public frmItemClassification(ItemClassification itemClassification)
        {
            InitializeComponent();
            itemClassificationObj = itemClassification;
        }

        private void frmItemClassification_Load(object sender, EventArgs e)
        {
            DataView dvCategory = Utility.GetCategoryList().Copy().DefaultView;
            dvCategory.RowFilter = "CATEGORYNAME <> 'ALL'";
            luCategory.Properties.DataSource = dvCategory;
            luCategory.Properties.DisplayMember = "CATEGORYNAME";
            luCategory.Properties.ValueMember = "CATEGORYID";
            if (dvCategory.Count == 1)
            {
                luCategory.EditValue = dvCategory[0]["CATEGORYID"];
            }

            luCategory.EditValue = itemClassificationObj.CategoryID;
            txtClassificationName.EditValue = itemClassificationObj.ItemClassificationName;
            this.Text = string.Format(Text, itemClassificationObj.ItemClassificationID == null ? "New" : itemClassificationObj.ItemClassificationName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            itemClassificationObj.CategoryID = luCategory.EditValue;
            itemClassificationObj.ItemClassificationName = txtClassificationName.EditValue;
            itemClassificationObj.UserID = Utility.UserID;
            try
            {
                new MasterRepository().SaveItemClassification(itemClassificationObj);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (itemClassificationObj.IsSave) Close();
            }
        }
    }
}