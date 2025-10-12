using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraSplashScreen;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmCreateDiscountedItems : XtraForm
    {
        public frmCreateDiscountedItems()
        {
            InitializeComponent();
        }

        private void frmCreateDiscountedItems_Load(object sender, System.EventArgs e)
        {
            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btnImport_Click(object sender, System.EventArgs e)
        {
            if (cmbBranch.EditValue == null)
            {
                XtraMessageBox.Show("Please select branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }

            XtraOpenFileDialog xtraOpenFileDialog1 = new XtraOpenFileDialog();
            xtraOpenFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            xtraOpenFileDialog1.Filter = "excel files (*.xls,*.xlsx)|*.xls,*.xlsx";
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
                try
                {
                    string filePath = xtraOpenFileDialog1.FileName;
                    DataTable dt = Utility.ImportExcelXLS(filePath);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dtTemp = dt.Copy();
                        List<string> allowedColumns = new List<string> { "ITEMCODE", "MRP", "OLDSALEPRICE", "NEWSALEPRICE", "QUANTITY", "WEIGHTINKGS" };

                        dtTemp.Columns.Cast<DataColumn>().Where(x => !allowedColumns.Contains(x.ColumnName))
                            .ToList().ForEach(x => dtTemp.Columns.Remove(x));
                        int i = 0;
                        foreach (string s in allowedColumns)
                        {
                            if (!dtTemp.Columns.Contains(s))
                                throw new Exception($"{s} column is missed in import file");
                            else
                            {
                                dtTemp.Columns[s].SetOrdinal(i);
                                i++;
                            }
                        }

                        DataSet ds = new StockRepository().ValidateDiscountedItems(dtTemp, cmbBranch.EditValue);

                        if (ds != null && ds.Tables.Count > 0)
                        {
                            DataTable resultTable = ds.Tables[0];
                            gcItems.DataSource = resultTable;  // assuming you have a DevExpress gridControl1
                            gvItems.BestFitColumns();
                            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            {
                                string message = ds.Tables[1].Rows[0][0].ToString();
                                XtraMessageBox.Show(message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                XtraMessageBox.Show("Items validated successfully, Go ahead with submission.", "Success",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        SplashScreenManager.CloseOverlayForm(handle);
                    }
                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseOverlayForm(handle);
                    ErrorMgmt.ShowError(ex);
                    ErrorMgmt.Errorlog.Error(ex);
                }
            }
        }

        private void btnViewReport_Click(object sender, System.EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void frmCreateDiscountedItems_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int focusedRowHandle = gvItems.FocusedRowHandle;
            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("No valid row is selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (XtraMessageBox.Show("Are you sure to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            gvItems.DeleteRow(focusedRowHandle);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(cmbBranch.EditValue == null)
            {
                XtraMessageBox.Show("Please select branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return; 
            }
            try
            {
                DataTable dt = gcItems.DataSource as DataTable;
                dt.AcceptChanges();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable dtTemp = dt.Copy();
                    List<string> allowedColumns = new List<string> { "ITEMCODE", "MRP", "OLDSALEPRICE", "NEWSALEPRICE", "QUANTITY", "WEIGHTINKGS" };

                    dtTemp.Columns.Cast<DataColumn>().Where(x => !allowedColumns.Contains(x.ColumnName))
                        .ToList().ForEach(x => dtTemp.Columns.Remove(x));
                    int i = 0;
                    foreach (string s in allowedColumns)
                    {
                        if (!dtTemp.Columns.Contains(s))
                            throw new Exception($"{s} column is missed in import file");
                        else
                        {
                            dtTemp.Columns[s].SetOrdinal(i);
                            i++;
                        }
                    }
                    new StockRepository().SaveDiscountedItems(dtTemp, Utility.UserID, cmbBranch.EditValue);
                }
                else
                {
                    XtraMessageBox.Show("No items found to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}