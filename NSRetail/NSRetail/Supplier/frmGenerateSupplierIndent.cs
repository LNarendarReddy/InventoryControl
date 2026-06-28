using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using NSRetail.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Supplier
{
    public partial class frmGenerateSupplierIndent : DevExpress.XtraEditors.XtraForm
    {
        BackgroundWorker worker;

        public frmGenerateSupplierIndent()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SetEnabled(false);
            AppendStatus("============ Begin Generation ============");
            new DataRepository().ExecuteNonQuery("USP_G_SUPPLIERINDENT",
                true, new Dictionary<string, object>
                {
                    { "SupplierID", luSupplier.EditValue }
                    , { "CategoryID", luCategory.EditValue }
                    , { "SafetyDays", txtSafetyDays.EditValue }
                    , { "ManufacturerID", luManufacturer.EditValue }
                    , { "IndentItemSelectionType", luIndentType.EditValue }
                    , { "BranchID", luBranch.EditValue }
                    , { "UserID", Utility.UserID }
                }, true, AppendStatus);
            AppendStatus(string.Empty);
            AppendStatus("============ Completed ============");
            AppendStatus(string.Empty);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Worker_RunWorkerCompleted(sender, e)));
                return;
            }

            SetEnabled(true);
        }

        private void SetEnabled(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetEnabled(enabled)));
                return;
            }

            luSupplier.Enabled = enabled;
            luCategory.Enabled = enabled;
            txtSafetyDays.Enabled = enabled;
            luIndentType.Enabled = enabled;
            luBranch.Enabled = enabled;
            luIndentType_EditValueChanged(null, null);
        }

        private void frmGenerateSupplierIndent_Load(object sender, EventArgs e)
        {
            luSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            luSupplier.Properties.ValueMember = "DEALERID";
            luSupplier.Properties.DisplayMember = "DEALERNAME";

            luCategory.Properties.DataSource = Utility.GetCategoryListExceptAll();
            luCategory.Properties.ValueMember = "CATEGORYID";
            luCategory.Properties.DisplayMember = "CATEGORYNAME";

            luIndentType.Properties.DataSource = Utility.GetEnumList("Supplier Indent Item Type");
            luIndentType.Properties.DisplayMember = "ENUMVALUE";
            luIndentType.Properties.ValueMember = "ENUMID";

            luBranch.Properties.DataSource = Utility.GetBranchList();
            luBranch.Properties.DisplayMember = "BRANCHNAME";
            luBranch.Properties.ValueMember = "BRANCHID";

            luManufacturer.Properties.DataSource = Utility.GetManufacturer();
            luManufacturer.Properties.DisplayMember = "MANUFACTURERNAME";
            luManufacturer.Properties.ValueMember = "MANUFACTURERID";

            txtSafetyDays.EditValue = 0;
            luIndentType_EditValueChanged(null, null);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            if (luIndentType.EditValue.Equals(13) && luBranch.EditValue == null)
            {
                XtraMessageBox.Show("Branch is required for direct store dispatch indent generation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            worker.RunWorkerAsync();
        }

        private void luIndentType_EditValueChanged(object sender, EventArgs e)
        {
            luBranch.Enabled = luIndentType.EditValue != null && luIndentType.EditValue.Equals(13);
        }

        private void AppendStatus(string text)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action(() => AppendStatus(text)));
                return;
            }

            text = (string.IsNullOrEmpty(text) ? string.Empty : DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt") + "   " + text)
                + Environment.NewLine;
            txtLog.AppendText(text);
            txtLog.SelectionStart = int.MaxValue;
            txtLog.ScrollToCaret();
        }

        private void btnPickList_Click(object sender, EventArgs e)
        {
            new frmGeneratePickList().ShowDialog();
        }
    }
}