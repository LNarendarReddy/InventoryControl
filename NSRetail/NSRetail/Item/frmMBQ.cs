using DataAccess;
using DevExpress.XtraEditors;
using Entity;
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
    public partial class frmMBQ : DevExpress.XtraEditors.XtraForm
    {
        public Item Item { get; }

        public frmMBQ(Item item)
        {
            InitializeComponent();
            Item = item;
        }

        private void btnApplyToSelected_Click(object sender, EventArgs e)
        {
            if(!ValidateInput()) return;

            if (gvIndentLimits.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("No Branches selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach(int rowHandle in gvIndentLimits.GetSelectedRows())
            {
                gvIndentLimits.SetRowCellValue(rowHandle, "THRESHOLD", txtThreshold.EditValue);
                gvIndentLimits.SetRowCellValue(rowHandle, "DESIREDQUANTITY", txtDesiredQty.EditValue);
            }
        }

        private void btnApplyToAll_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            for (int rowHandle = 0; rowHandle < gvIndentLimits.DataRowCount; rowHandle++)
            {
                gvIndentLimits.SetRowCellValue(rowHandle, "THRESHOLD", txtThreshold.EditValue);
                gvIndentLimits.SetRowCellValue(rowHandle, "DESIREDQUANTITY", txtDesiredQty.EditValue);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to save?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            DataTable dtIndentLimit = ((DataTable)gcIndentLimits.DataSource).GetChanges();

            List<string> allowedColums = new List<string>()
            {
                "INDENTLIMITID"
                , "BRANCHID"
                , "ITEMID"
                , "THRESHOLD"
                , "DESIREDQUANTITY"
            };

            List<string> columnsToRemove = new List<string>();

            foreach (DataColumn column in dtIndentLimit.Columns)
            {
                if(allowedColums.Contains(column.ColumnName)) continue;
                columnsToRemove.Add(column.ColumnName);
            }

            foreach(string col in columnsToRemove)
            {
                dtIndentLimit.Columns.Remove(col);
            }

            new DataRepository().ExecuteNonQuery("USP_CU_INDENTLIMIT", false
                , new Dictionary<string, object> 
                { 
                    { "IndentLimits",  dtIndentLimit }
                    , { "UserID", Utility.UserID }
                });

            XtraMessageBox.Show("Save success");

            gcIndentLimits.DataSource = new ReportRepository().GetReportData("USP_R_INDENTLIMIT_BY_ITEM"
                , new Dictionary<string, object>() { { "ItemID", Item.ItemID } });
        }

        private void frmMBQ_Load(object sender, EventArgs e)
        {
            lblItemName.Text = $"{Item.ItemName} ({Item.SKUCode})";
            gcIndentLimits.DataSource = new ReportRepository().GetReportData("USP_R_INDENTLIMIT_BY_ITEM"
                , new Dictionary<string, object>() { { "ItemID", Item.ItemID } });
        }

        private bool ValidateInput()
        {
            if (txtThreshold.EditValue == null)
            {
                XtraMessageBox.Show("Threshold cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtThreshold.Focus();
                return false;
            }

            if (txtDesiredQty.EditValue == null)
            {
                XtraMessageBox.Show("Desired Qty. cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDesiredQty.Focus();
                return false;
            }
            
            return true;
        }
    }
}