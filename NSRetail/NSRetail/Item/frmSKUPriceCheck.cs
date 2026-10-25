using DataAccess;
using DevExpress.XtraEditors;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmSKUPriceCheck : XtraForm
    {
        internal const string ViewAccessKey = "5F2FA89D-11D2-4FE5-A612-846364CD8540::View";

        private readonly string initialCode;
        private bool loading;

        public frmSKUPriceCheck(string skuCode, string itemName)
        {
            InitializeComponent();
            initialCode = skuCode;
            Text = skuCode + " - " + itemName;
        }

        private void frmSKUPriceCheck_Load(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(ViewAccessKey))
            {
                Close();
                return;
            }

            try
            {
                loading = true;
                DataTable branches = Utility.GetBranchList();
                cmbBranch.Properties.DataSource = branches;
                cmbBranch.Properties.ValueMember = "BRANCHID";
                cmbBranch.Properties.DisplayMember = "BRANCHNAME";
                DataRow[] currentBranch = branches.Select("BRANCHID = " + Utility.BranchID);
                if (currentBranch.Length > 0)
                    cmbBranch.EditValue = Utility.BranchID;
                else if (branches.Rows.Count > 0)
                    cmbBranch.EditValue = branches.Rows[0]["BRANCHID"];
                txtScanCode.Text = initialCode;
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
            txtScanCode.Focus();
            txtScanCode.SelectAll();
        }

        private void LoadPrices()
        {
            if (loading) return;
            gcPrices.DataSource = null;
            lblStatus.Text = string.Empty;
            if (!AccessUtility.HasAccess(ViewAccessKey))
            {
                lblStatus.Text = "You do not have access to SKU Price Check.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtScanCode.Text) || cmbBranch.EditValue == null) return;

            try
            {
                UseWaitCursor = true;
                DataSet result = new ItemCodeRepository().GetSKUPriceCheck(
                    txtScanCode.Text.Trim(), Convert.ToInt32(cmbBranch.EditValue));
                if (result.Tables["ITEM"].Rows.Count == 0)
                {
                    Text = "SKU Price Check";
                    lblStatus.Text = "SKU / Itemcode not found.";
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
                txtScanCode.Focus();
                txtScanCode.SelectAll();
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

        private void txtScanCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;
            LoadPrices();
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
