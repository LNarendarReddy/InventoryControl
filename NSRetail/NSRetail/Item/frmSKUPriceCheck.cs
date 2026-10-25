using DataAccess;
using DevExpress.XtraEditors;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmSKUPriceCheck : XtraForm
    {
        private readonly int? initialItemCodeId;
        private bool loading;

        public frmSKUPriceCheck()
        {
            InitializeComponent();
        }

        public frmSKUPriceCheck(int itemCodeId, string skuCode, string itemName) : this()
        {
            initialItemCodeId = itemCodeId;
            Text = skuCode + " - " + itemName;
        }

        private void frmSKUPriceCheck_Load(object sender, EventArgs e)
        {
            try
            {
                loading = true;
                DataTable branches = Utility.GetBranchList();
                cmbBranch.Properties.DataSource = branches;
                cmbBranch.Properties.ValueMember = "BRANCHID";
                cmbBranch.Properties.DisplayMember = "BRANCHNAME";
                sluItem.Properties.DataSource = Utility.GetItemCodeListFiltered();
                sluItem.Properties.ValueMember = "ITEMCODEID";
                sluItem.Properties.DisplayMember = "ITEMCODE";

                if (initialItemCodeId.HasValue)
                {
                    sluItem.EditValue = initialItemCodeId.Value;
                    DataRow[] currentBranch = branches.Select("BRANCHID = " + Utility.BranchID);
                    if (currentBranch.Length > 0)
                        cmbBranch.EditValue = Utility.BranchID;
                    else if (branches.Rows.Count > 0)
                        cmbBranch.EditValue = branches.Rows[0]["BRANCHID"];
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
            finally
            {
                loading = false;
            }
            LoadPrices();
            sluItem.Focus();
        }

        private void LoadPrices()
        {
            if (loading) return;
            gcPrices.DataSource = null;
            lblStatus.Text = string.Empty;
            DataRowView selectedItem = sluItem.GetSelectedDataRow() as DataRowView;
            Text = selectedItem == null ? "SKU Price Check"
                : selectedItem["SKUCODE"] + " - " + selectedItem["ITEMNAME"];
            if (selectedItem == null || cmbBranch.EditValue == null || cmbBranch.EditValue == DBNull.Value)
            {
                lblStatus.Text = "Select an item and branch to check prices.";
                return;
            }

            try
            {
                UseWaitCursor = true;
                DataSet result = new ItemCodeRepository().GetSKUPriceCheck(
                    Convert.ToInt32(selectedItem["ITEMID"]), Convert.ToInt32(cmbBranch.EditValue));
                if (result.Tables["ITEM"].Rows.Count == 0)
                {
                    Text = "SKU Price Check";
                    lblStatus.Text = "Item not found.";
                    return;
                }

                DataRow item = result.Tables["ITEM"].Rows[0];
                Text = item["SKUCODE"] + " - " + item["ITEMNAME"];
                gcPrices.DataSource = result.Tables["PRICES"];
                gvPrices.ExpandAllGroups();
                gvPrices.BestFitColumns();
                lblStatus.Text = result.Tables["PRICES"].Rows.Count == 0
                    ? "No active prices found for this SKU."
                    : "All active MRPs under this SKU. Conditional deals require the qualifying basket; prices are shown per offer.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Unable to load SKU prices.";
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
            finally
            {
                UseWaitCursor = false;
                sluItem.Focus();
            }
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            LoadPrices();
        }

        private void cmbBranch_EditValueChanged(object sender, EventArgs e)
        {
            LoadPrices();
        }

        private void sluItem_EditValueChanged(object sender, EventArgs e)
        {
            LoadPrices();
        }

        private void sluItem_Popup(object sender, EventArgs e)
        {
            sluItemView.BestFitColumns();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
