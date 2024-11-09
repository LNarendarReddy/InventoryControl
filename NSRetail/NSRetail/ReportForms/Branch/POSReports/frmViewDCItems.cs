using DevExpress.Data;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class frmViewDCItems : DevExpress.XtraEditors.XtraForm
    {
        public frmViewDCItems(DataSet dsItems, bool IsBilldetail = false, bool IsCustomerRefund = false, bool IsVoidIems = false)
        {
            InitializeComponent();
            gcGSTCode.Visible = IsBilldetail;
            gcGSTValue.Visible = IsBilldetail;
            gcDiscount.Visible = IsCustomerRefund;
            if (!IsBilldetail)
                lciMOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            gcItems.DataSource = dsItems.Tables[0];
            if (dsItems.Tables.Count > 1)
                gcMOP.DataSource = dsItems.Tables[1];
            gcBillNumber.Visible = IsVoidIems || IsCustomerRefund;
            gcCreatedBy.Visible = IsVoidIems;
            gcCreatedDate.Visible = true;
            gcDeletedBy.Visible = IsVoidIems;
            gcDeletedDate.Visible = IsVoidIems;
            gcBilledAmount.Caption = IsCustomerRefund ? "Refund Amount" : gcBilledAmount.Caption;
            gcQuantity.Caption = IsCustomerRefund ? "Refund Quantity" : gcQuantity.Caption;
            gcCreatedBy.Caption = IsCustomerRefund ? "Refund User" : gcCreatedBy.Caption;
            gcCreatedDate.Caption = IsCustomerRefund ? "Refund Date" : gcCreatedDate.Caption;

            if(IsBilldetail && IsVoidIems)
            {
                gvItems.RowStyle += GvItems_RowStyle;
            }
        }

        private void GvItems_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns.Any(x => x.FieldName == "DELETEDDATE")
                    && view.GetRowCellValue(e.RowHandle, "DELETEDDATE") != DBNull.Value)
            {
                //e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Strikeout);
                e.Appearance.BackColor = Color.Maroon;
                //e.Appearance.Options.UseForeColor = true;
                e.Appearance.Options.UseBackColor = true;
                //e.Appearance.Options.UseFont = true;
                e.HighPriority = true;
                return;
            }
        }

        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0)
                return;
            e.Menu.Items.Add(new DXMenuItem("View Report", new EventHandler(ViewReport_Click)));
        }

        private void ViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void frmViewDCItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void frmViewDCItems_Load(object sender, EventArgs e)
        {

        }

        decimal sum = 0;

        private void gvItems_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "BILLEDAMOUNT")
            //{
            //    GridSummaryItem item = e.Item as GridSummaryItem;
            //    if (item.FieldName == "BILLEDAMOUNT")
            //    {
            //        switch (e.SummaryProcess)
            //        {
            //            case CustomSummaryProcess.Start:
            //                sum = 0;
            //                break;
            //            case CustomSummaryProcess.Calculate:
            //                if (view.GetRowCellValue(e.RowHandle, "DELETEDDATE").Equals(DBNull.Value) && e.FieldValue != null)
            //                {
            //                    sum += (decimal)e.FieldValue;
            //                }
            //                break;
            //            case CustomSummaryProcess.Finalize:
            //                e.TotalValue = sum;
            //                break;
            //        }
            //    }
            //}
        }
    }
}