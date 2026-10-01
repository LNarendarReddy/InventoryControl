using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Entity;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockDispatchInfoV2 : XtraForm
    {
        private readonly StockRepository stockRepository = new StockRepository();
        private readonly StockDispatch stockDispatch;

        public frmStockDispatchInfoV2()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public frmStockDispatchInfoV2(StockDispatch stockDispatch) : this()
        {
            this.stockDispatch = stockDispatch ?? throw new ArgumentNullException(nameof(stockDispatch));
        }

        private void frmStockDispatchInfoV2_Load(object sender, EventArgs e)
        {
            DataTable branches = Utility.GetBranchList();

            DataView warehouse = branches.Copy().DefaultView;
            warehouse.RowFilter = $"BRANCHID = {Utility.BranchID}";
            cmbFromBranch.Properties.DataSource = warehouse;
            cmbFromBranch.Properties.ValueMember = "BRANCHID";
            cmbFromBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbFromBranch.Properties.Columns.Add(new LookUpColumnInfo("BRANCHNAME", "Branch"));

            DataView destinations = branches.Copy().DefaultView;
            destinations.RowFilter = "ISWAREHOUSE = 0";
            cmbToBranch.Properties.DataSource = destinations;
            cmbToBranch.Properties.ValueMember = "BRANCHID";
            cmbToBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbToBranch.Properties.Columns.Add(new LookUpColumnInfo("BRANCHNAME", "Branch"));

            cmbCategory.Properties.DataSource = Utility.GetCategoryList();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.Properties.Columns.Add(new LookUpColumnInfo("CATEGORYNAME", "Category"));

            bool isDraft = Convert.ToInt32(stockDispatch.STOCKDISPATCHID) > 0;
            cmbFromBranch.EditValue = stockDispatch.FROMBRANCHID ??
                (warehouse.ToTable().Rows.Count > 0 ? warehouse.ToTable().Rows[0]["BRANCHID"] : null);
            cmbToBranch.EditValue = stockDispatch.TOBRANCHID ??
                (destinations.ToTable().Rows.Count == 1 ? destinations.ToTable().Rows[0]["BRANCHID"] : null);
            cmbCategory.EditValue = stockDispatch.CATEGORYID ?? Utility.CategoryID;
            txtNotes.EditValue = stockDispatch.Description;

            cmbFromBranch.Enabled = !isDraft;
            cmbToBranch.Enabled = true;
            cmbCategory.Enabled = !isDraft && string.Equals(Utility.Category, "ALL", StringComparison.OrdinalIgnoreCase);
            lblHeading.Text = isDraft ? "Dispatch Information - Existing Draft" : "Dispatch Information - New Dispatch";
            btnContinue.Text = isDraft ? "Save" : "Continue";
            cmbToBranch.Focus();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (cmbFromBranch.EditValue == null)
            {
                XtraMessageBox.Show("From branch cannot be empty");
                return;
            }

            if (cmbToBranch.EditValue == null)
            {
                XtraMessageBox.Show("To branch cannot be empty");
                return;
            }

            if (cmbCategory.EditValue == null)
            {
                XtraMessageBox.Show("Category cannot be empty");
                return;
            }

            if (Convert.ToInt32(cmbCategory.EditValue) == 13)
            {
                XtraMessageBox.Show("Dispatch cannot be created with selected category");
                return;
            }

            bool isExistingDispatch = Convert.ToInt32(stockDispatch.STOCKDISPATCHID) > 0;
            bool isDestinationChanged = Convert.ToInt32(stockDispatch.TOBRANCHID) != Convert.ToInt32(cmbToBranch.EditValue);
            if (isExistingDispatch && isDestinationChanged &&
                XtraMessageBox.Show(
                    "You are about to change the dispatch destination branch. Are you sure you want to continue?",
                    "Confirm Branch Change",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            object previousToBranchID = stockDispatch.TOBRANCHID;
            object previousDescription = stockDispatch.Description;

            try
            {
                stockDispatch.FROMBRANCHID = cmbFromBranch.EditValue;
                stockDispatch.TOBRANCHID = cmbToBranch.EditValue;
                stockDispatch.CATEGORYID = cmbCategory.EditValue;
                stockDispatch.Description = txtNotes.EditValue;

                if (isExistingDispatch)
                {
                    stockDispatch.UserID = Utility.UserID;
                    stockRepository.UpdateDispatchDraftInfo(stockDispatch);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                stockDispatch.TOBRANCHID = previousToBranchID;
                stockDispatch.Description = previousDescription;
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
