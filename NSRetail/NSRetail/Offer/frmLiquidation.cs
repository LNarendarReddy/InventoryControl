using DataAccess;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.ExpressionEditor;
using NSRetail.Utilities;

namespace NSRetail
{
    public partial class frmLiquidation : XtraForm
    {
        BackgroundWorker worker;
        Operation currentOperation;
        Dictionary<int, string> errorData = new Dictionary<int, string>();

        enum Operation
        {            
            Save = 0,            
            Approve = 1,
            Reject = 2,
            Load = 3,
            Idle = 4
        }

        public frmLiquidation()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Worker_RunWorkerCompleted(sender, e)));
                return;
            }

            SetEnabled(true);
            currentOperation = Operation.Idle;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (currentOperation)
            {
                case Operation.Idle:
                    break;
                case Operation.Load:
                    SetEnabled(false);
                    LoadLiquidationData();
                    break;
                case Operation.Save:
                case Operation.Approve:
                case Operation.Reject:
                    SetEnabled(false);
                    SaveLiquidation(currentOperation);
                    break;
            }
        }

        private void frmLiquidation_Load(object sender, EventArgs e)
        {
            luBranch.Properties.DataSource = Utility.GetBranchList();
            luBranch.Properties.ValueMember = "BRANCHID";
            luBranch.Properties.DisplayMember = "BRANCHNAME";

            AccessUtility.SetStatusByAccess(btnSave, btnApprove, btnReject);

            AppendStatus("Message log initiated");

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            currentOperation = Operation.Load;
            worker.RunWorkerAsync();
        }

        private void SetEnabled(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetEnabled(enabled)));
                return;
            }

            if (enabled)
                gvLiquidation.HideLoadingPanel();
            else
                gvLiquidation.ShowLoadingPanel();

            btnRefresh.Enabled = enabled;
            btnApply.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnApprove.Enabled = enabled;
            btnReject.Enabled = enabled;
            txtStatusReason.Enabled = enabled;

            rgDiscount.Enabled = enabled;
            txtValue.Enabled = enabled;
            luBranch.Enabled = enabled;

            gvLiquidation.LayoutChanged();
        }

        private void LoadLiquidationData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", luBranch.EditValue.ToString() }
                , { "FromDate", "1900-01-01" }
                , { "ToDate", DateTime.Now.ToString("yyyy-MM-dd") }
                , { "ShowStock", true }
                , { "ShowAll", false }
                , { "ShowCostPrice", true }
            };

            DataTable liquidationData = new ReportRepository().GetReportData("USP_R_LIQUIDATION", parameters);
            SetLiquidationData(liquidationData);
        }

        private void SetLiquidationData(DataTable liquidationData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetLiquidationData(liquidationData)));
                return;
            }

            gcLiquidation.DataSource = liquidationData;
            gvLiquidation_SelectionChanged(null, null);
            errorData = new Dictionary<int, string>();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();

            if (gvLiquidation.SelectedRowsCount == 0)
                errors.Add("No rows selected");

            if (rgDiscount.EditValue == null)
                errors.Add("Discount type not selected");

            if (txtValue.EditValue == null)
                errors.Add("Value cannot be empty");

            if (errors.Any())
            {
                string message = "Fix the following errors to continue \n";
                errors.ForEach(error => message += "\n\r * " + error);
                XtraMessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double value = double.Parse(txtValue.EditValue?.ToString());
            foreach (int rowId in gvLiquidation.GetSelectedRows())
            {
                double curMRP = double.Parse(gvLiquidation.GetRowCellValue(rowId, "MRP")?.ToString());
                double liquidationSalePrice = rgDiscount.EditValue.ToString() == "FixedRate" ? value : curMRP - (value * curMRP / 100);
                gvLiquidation.SetRowCellValue(rowId, "LIQUIDATIONSALEPRICE", liquidationSalePrice);
            }
        }

        private void gvLiquidation_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            lblRowCount.Text = $"({gvLiquidation.SelectedRowsCount}) Row(s) selected";
        }

        private void AppendStatus(string text, int processingRowId = 0)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action(() => AppendStatus(text, processingRowId)));
                return;
            }

            if (text.StartsWith("error") && processingRowId >= 0)
            {
                errorData[processingRowId] = text.Split('-').Select(x => x.Trim()).Last();                
            }

            text = (string.IsNullOrEmpty(text) ? string.Empty : DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt") + "   " + text) 
                + Environment.NewLine;
            txtStatus.AppendText(text);
            txtStatus.SelectionStart = int.MaxValue;
            txtStatus.ScrollToCaret();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gcLiquidation) return;

            // Determine the grid element under the mouse
            GridView view = gcLiquidation.GetViewAt(e.ControlMousePosition) as GridView;
            if (view == null) return;
            GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);

            // Provide tooltip for rows or cells
            if (!(hi.HitTest == GridHitTest.Row || hi.HitTest == GridHitTest.RowCell)
                || hi.RowHandle < 0
                || !errorData.ContainsKey(hi.RowHandle))
                return;

            // Create unique ID for the object and assign the text
            e.Info = new ToolTipControlInfo(hi.HitTest.ToString() + hi.RowHandle.ToString(), errorData[hi.RowHandle]);
        }

        private void gvLiquidation_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (!errorData.ContainsKey(e.RowHandle)) return;

            e.Appearance.ForeColor = Color.Red;
            e.HighPriority = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();

            if (gvLiquidation.SelectedRowsCount == 0)
                errors.Add("No rows selected");
            
            if (errors.Any())
            {
                string message = "Fix the following errors to continue \n";
                errors.ForEach(error => message += "\n\r * " + error);
                XtraMessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentOperation = Operation.Save;
            worker.RunWorkerAsync();
        }

        private void SaveLiquidation(Operation operation)
        {
            OfferRepository offerRepository = new OfferRepository();

            AppendStatus(string.Empty);
            AppendStatus("============================================================");
            AppendStatus($"Begin processing {gvLiquidation.SelectedRowsCount} rows");
            
            foreach (int rowHandle in gvLiquidation.GetSelectedRows())
            {
                AppendStatus(string.Empty);
                try
                {
                    object itemName = gvLiquidation.GetRowCellValue(rowHandle, "ITEMNAME");
                    object skuCode = gvLiquidation.GetRowCellValue(rowHandle, "SKUCODE");
                    object itemCode = gvLiquidation.GetRowCellValue(rowHandle, "ITEMCODE");
                    object MRP = gvLiquidation.GetRowCellValue(rowHandle, "MRP");
                    object liquidationID = gvLiquidation.GetRowCellValue(rowHandle, "LIQUIDATIONID");
                    object liquidationSalePrice = gvLiquidation.GetRowCellValue(rowHandle, "LIQUIDATIONSALEPRICE");
                    AppendStatus($"Processing item: {itemName} ({skuCode}), EAN: {itemCode}, MRP: {MRP}", rowHandle);

                    offerRepository.UpdateLiquidation(liquidationID, (int)operation, txtStatusReason.EditValue
                        , liquidationSalePrice, Utility.UserID, rowHandle, AppendStatus);
                }
                catch (Exception ex)
                {
                    AppendStatus($"error - {ex.Message}", rowHandle);

                    if (ex.InnerException != null)
                        AppendStatus($"error - {ex.InnerException.Message}", rowHandle);
                }
            }

            AppendStatus(string.Empty);
            AppendStatus($"Completed processing {gvLiquidation.SelectedRowsCount} rows");
            AppendStatus("============================================================");
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            Liquidate(Operation.Approve);
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            Liquidate(Operation.Reject);
        }

        private void Liquidate(Operation operation)
        {
            List<string> errors = new List<string>();

            if (gvLiquidation.SelectedRowsCount == 0)
                errors.Add("No rows selected");

            if (string.IsNullOrEmpty(txtStatusReason.EditValue?.ToString()))
                errors.Add("Status text is mandatory");

            if (errors.Any())
            {
                string message = "Fix the following errors to continue \n";
                errors.ForEach(error => message += "\n\r * " + error);
                XtraMessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentOperation = operation;
            worker.RunWorkerAsync();
        }

        private void luBranch_Leave(object sender, EventArgs e)
        {
            if (luBranch.EditValue == null) return;

            currentOperation = Operation.Load;
            worker.RunWorkerAsync();
        }
    }
}