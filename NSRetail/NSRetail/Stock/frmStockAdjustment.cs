using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NSRetail.Stock
{
    public partial class frmStockAdjustment : DevExpress.XtraEditors.XtraForm
    {
        StockAdjustment stockAdjustment;
        
        public frmStockAdjustment()
        {
            InitializeComponent();
        }

        private void frmStockAdjustment_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbSKUCode.Properties.DataSource = Utility.GetItemSKUList();
            cmbSKUCode.Properties.ValueMember = "ITEMID";
            cmbSKUCode.Properties.DisplayMember = "SKUCODE";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            DataTable dtStock = gcItemStockByMRP.DataSource as DataTable;
            if (dtStock == null) return;

            DataView dvStockChanges = dtStock.Copy().DefaultView;
            dvStockChanges.RowFilter = "SYSTEMSTOCK <> PHYSICALSTOCK";
            if(dvStockChanges.Count == 0)
            {
                XtraMessageBox.Show("No stock changes made, operation cannot proceed?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string msg = $"The following updates will be saved. Are you sure you want to continue?{Environment.NewLine}{Environment.NewLine}";
            msg += $"\t [EAN Code] - [MRP] - [Sale Price] - [Physical Stock]{Environment.NewLine}{Environment.NewLine}";

            foreach(DataRowView rowView in dvStockChanges)
            {
                msg += $"\t\t{rowView["ITEMCODE"]} - {rowView["MRP"]} - {rowView["SALEPRICE"]} - {rowView["PHYSICALSTOCK"]}{Environment.NewLine}";
            }

            if (XtraMessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            DataTable dtStockToSave = dvStockChanges.ToTable();
            List<string> allowedColumns = new List<string>() { "ITEMPRICEID", "STOCKSUMMARYID", "PHYSICALSTOCK" };
            List<string> columnsToRemove = new List<string>();
            for (int i = 0; i < dtStockToSave.Columns.Count; i++)
            {
                if (allowedColumns.Contains(dtStockToSave.Columns[i].ColumnName)) continue;
                columnsToRemove.Add(dtStockToSave.Columns[i].ColumnName);
            }
            columnsToRemove.ForEach(x => dtStockToSave.Columns.Remove(x));


            stockAdjustment = new StockAdjustment
            {
                BranchID = cmbBranch.EditValue,
                UserID = Utility.UserID,
                ItemID = cmbSKUCode.EditValue,
                dtStockSummary = dtStockToSave,
                Description = txtComment.EditValue
            };
            try
            {
                new StockRepository().SaveStockAdjustment(stockAdjustment);
                XtraMessageBox.Show("Stock adjustment completed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch(Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private void ClearFields()
        {
            cmbSKUCode.EditValue = null;
            txtItemName.EditValue = null;
            gcItemStockByMRP.DataSource = null;
            txtComment.EditValue = null;
            cmbBranch.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbSKUCode.EditValue == null) 
            {
                ClearFields();
                cmbSKUCode.Focus();
                return; 
            }

            try
            {
                int rowhandle = cmbLookupView.LocateByValue("ITEMID", cmbSKUCode.EditValue);
                txtItemName.EditValue = cmbLookupView.GetRowCellValue(rowhandle, "ITEMNAME");
                gcItemStockByMRP.DataSource = new ReportRepository().GetReportData(
                    "USP_R_SKUSTOCKBYMRP"
                    , new Dictionary<string, object>()
                        {
                            { "BRANCHID", cmbBranch.EditValue },
                            { "ITEMID", cmbSKUCode.EditValue }
                        });
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void cmbBranch_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue != null && cmbSKUCode.EditValue != null)
            {
                cmbItemCode_EditValueChanged(sender, e);
            }
        }

        private void gvItemStockByMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gvItemStockByMRP.MoveNext();
            }
        }
    }
}