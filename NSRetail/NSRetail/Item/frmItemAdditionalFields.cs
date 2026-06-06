using DataAccess;
using DevExpress.XtraEditors;
using ErrorManagement;
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
    public partial class frmItemAdditionalFields : DevExpress.XtraEditors.XtraForm
    {
        private readonly object itemID;

        public frmItemAdditionalFields(object itemID)
        {
            InitializeComponent();
            this.itemID = itemID;
        }

        private void frmItemAdditionalFields_Load(object sender, EventArgs e)
        {
            DataTable dtItemAdditionDetails = new ReportRepository().GetReportData(
                "USP_R_ITEM_ADDITIONAL_FIELDS",
                new Dictionary<string, object>
                {
                    { "ItemID", itemID }
                });

            if (dtItemAdditionDetails == null || dtItemAdditionDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("Error loading item data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbSupplierIndentType.Properties.DataSource = Utility.GetEnumList("Supplier Indent Item Type");
            cmbSupplierIndentType.Properties.DisplayMember = "ENUMVALUE";
            cmbSupplierIndentType.Properties.ValueMember = "ENUMID";

            lblItemName.Text = dtItemAdditionDetails.Rows[0]["ITEMNAME"].ToString();
            txtSKUCode.EditValue = dtItemAdditionDetails.Rows[0]["SKUCODE"];
            cmbSupplierIndentType.EditValue = dtItemAdditionDetails.Rows[0]["SUPPLIERINDENTITEMTYPEID"];
            txtInnerCaseQty.EditValue = dtItemAdditionDetails.Rows[0]["INNERCASEQTY"];
            txtOuterCaseQty.EditValue = dtItemAdditionDetails.Rows[0]["OUTERCASEQTY"];
            txtProductRank.EditValue = dtItemAdditionDetails.Rows[0]["PRODUCTRANK"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to save?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                new DataRepository().ExecuteNonQuery("USP_U_ITEM_ADDITIONAL_FIELDS", true,
                    new Dictionary<string, object>
                    {
                    { "ItemID", itemID }
                    , { "SupplietIndentItemTypeID", cmbSupplierIndentType.EditValue }
                    , { "InnerCaseQty", txtInnerCaseQty.EditValue }
                    , { "OuterCaseQty", txtOuterCaseQty.EditValue }
                    , { "ProductRank", txtProductRank.EditValue }
                    , { "UserID", Utility.UserID }
                    });

                XtraMessageBox.Show("Item additional details saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                new frmErrorDetails(ex).ShowDialog();                
            }
        }
    }
}