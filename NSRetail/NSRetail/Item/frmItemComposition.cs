using DataAccess;
using DevExpress.XtraEditors;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmItemComposition : XtraForm
    {
        private readonly int destinationItemId;
        private DataTable ingredients;
        private int revision;
        private bool isDirty;
        private bool saved;

        public frmItemComposition(int destinationItemId)
        {
            InitializeComponent();
            this.destinationItemId = destinationItemId;
        }

        private void frmItemComposition_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = btnAdd.Enabled = btnRemove.Enabled = false;
            try
            {
                DataSet data = new ItemCompositionRepository().GetComposition(destinationItemId);
                DataRow destination = data.Tables[0].Rows[0];
                txtDestination.Text = destination["SKUCODE"] + " - " + destination["ITEMNAME"];
                revision = Convert.ToInt32(destination["REVISION"]);
                ingredients = data.Tables[1];
                gcIngredients.DataSource = ingredients;
                sluIngredient.Properties.DataSource = data.Tables[2];
                sluIngredient.Properties.ValueMember = "ITEMID";
                sluIngredient.Properties.DisplayMember = "SKUCODE";
                btnSave.Enabled = btnAdd.Enabled = btnRemove.Enabled = true;
                AccessUtility.SetStatusByAccess(btnSave, btnAdd, btnRemove);
                sluIngredient.Enabled = btnAdd.Enabled;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnAdd.Tag?.ToString()) || ingredients == null) return;
            DataRowView selected = sluIngredient.GetSelectedDataRow() as DataRowView;
            if (selected == null)
            {
                XtraMessageBox.Show("Select an ingredient SKU.", "Composition Setup");
                sluIngredient.Focus();
                return;
            }

            int sourceItemId = Convert.ToInt32(selected["ITEMID"]);
            if (sourceItemId == destinationItemId || ingredients.AsEnumerable()
                .Any(row => Convert.ToInt32(row["SOURCEITEMID"]) == sourceItemId))
            {
                XtraMessageBox.Show("The ingredient must be different from the destination and cannot be added twice.", "Composition Setup");
                return;
            }

            DataRow ingredient = ingredients.NewRow();
            ingredient["SOURCEITEMID"] = sourceItemId;
            ingredient["SKUCODE"] = selected["SKUCODE"];
            ingredient["ITEMNAME"] = selected["ITEMNAME"];
            ingredient["UOM"] = selected["UOM"];
            ingredient["ISDELETED"] = false;
            ingredient["PERCENTAGE"] = 0m;
            ingredients.Rows.Add(ingredient);
            isDirty = true;
            sluIngredient.EditValue = null;
            sluIngredient.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnRemove.Tag?.ToString())) return;
            DataRow row = gvIngredients.GetFocusedDataRow();
            if (row == null) return;
            ingredients.Rows.Remove(row);
            isDirty = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AccessUtility.HasAccess(btnSave.Tag?.ToString()) || ingredients == null) return;
            if (ingredients.AsEnumerable().Any(row => Convert.ToBoolean(row["ISDELETED"])))
            {
                XtraMessageBox.Show("Remove deleted ingredient SKUs before saving.", "Composition Setup");
                return;
            }
            if (ingredients.Rows.Count == 0 && XtraMessageBox.Show(
                "Save an empty ingredient list and clear this composition setup?", "Composition Setup",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                DataTable details = new DataTable();
                details.Columns.Add("SOURCEITEMID", typeof(int));
                details.Columns.Add("SORTORDER", typeof(int));
                details.Columns.Add("PERCENTAGE", typeof(decimal));
                for (int index = 0; index < ingredients.Rows.Count; index++)
                    details.Rows.Add(ingredients.Rows[index]["SOURCEITEMID"], index + 1, 0m);

                new ItemCompositionRepository().SaveComposition(destinationItemId, revision, details,
                    Convert.ToInt32(Utility.UserID));
                saved = true;
                XtraMessageBox.Show("Composition setup saved successfully.", "Composition Setup");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmItemComposition_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDirty && !saved)
                e.Cancel = XtraMessageBox.Show("Discard the unsaved composition changes?", "Composition Setup",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }
    }
}
