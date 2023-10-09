using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Entity;
using ErrorManagement;
using NSRetail.Login;

namespace NSRetail
{
    public partial class frmItemCodeList : DevExpress.XtraEditors.XtraForm
    {
        #region Private Variables 

        DataTable dtItemCodes;
        DataTable dtItems;

        #endregion

        #region Constructor

        public frmItemCodeList()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public GridView ItemCodeListGridView { get { return gvItemList; } }

        #endregion

        #region Grid Button events


        private void btnNew_Click(object sender, EventArgs e)
        {
            Item itemObj = new Item();
            new frmItemCode(itemObj) { Owner = this }.ShowDialog();

            Utility.Setfocus(gvItemList, "ITEMCODEID", itemObj.ItemCodeID);

            if(itemObj.IsSave && Convert.ToBoolean(itemObj.IsNewToggleSwitched))
            {
                btnNew_Click(sender, e);
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemList.FocusedRowHandle >= 0)
                {
                    Item itemObj = new Item() { ItemCode = gvItemList.GetFocusedRowCellValue("ITEMCODE") };
                    new frmItemCode(itemObj) { Owner = this }.ShowDialog();

                    if (Convert.ToBoolean(itemObj.IsNewToggleSwitched))
                    {
                        btnNew_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemList.FocusedRowHandle >= 0 &&
                    XtraMessageBox.Show($"Are you sure you want to delete the Item Code : {gvItemList.GetFocusedRowCellValue("ITEMCODE")}?"
                    , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new ItemCodeRepository().DeleteItemCode(gvItemList.GetFocusedRowCellValue("ITEMCODEID"), Utility.UserID);
                    XtraMessageBox.Show($"Delete completed successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((frmMain)MdiParent).bbiRefreshData_ItemClick(null, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnVisualize_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemList.FocusedRowHandle >= 0)
                {
                    new frmItemVisualize(gvItemList.GetFocusedRowCellValue("ITEMID")).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }


        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void frmItemList_Load(object sender, EventArgs e)
        {
            try
            {
                EnableDisableControls(false);
                dtItemCodes = Utility.GetItemCodeListFiltered();
                dtItems = Utility.GetItemSKUList();
                gcItemList.DataSource = dtItemCodes;
                ((frmMain)this.MdiParent).RefreshBaseLineData += FrmItemCodeList_RefreshBaseLineData;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void FrmItemCodeList_RefreshBaseLineData(object sender, EventArgs e)
        {
            dtItemCodes = Utility.GetItemCodeListFiltered();
            dtItems = Utility.GetItemSKUList();
            gcItemList.DataSource = dtItemCodes;
        }

        private void gcItemList_DoubleClick(object sender, EventArgs e)
        {
            //btnEdit_Click(null, null);
        }

        private void gvItemList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EnableDisableControls(gvItemList.FocusedRowHandle >= 0);
        }

        private void gcItemList_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEdit_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(null, null);
            }
        }

        private void EnableDisableControls(bool enabled)
        {
            btnEdit.Enabled = enabled;
            btnVisualize.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }

        private void gvItemList_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                e.Allow = false;
                pmItemList.ShowPopup(gcItemList.PointToScreen(e.Point));
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnVisualize_Click(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItemList.ShowRibbonPrintPreview();
        }

        private void btnMRPList_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemList.FocusedRowHandle >= 0)
                {
                    DataTable dtMRPList = 
                        new ItemCodeRepository().GetMRPList(gvItemList.GetFocusedRowCellValue("ITEMCODEID"));
                    new frmMRPList(dtMRPList, 
                        gvItemList.GetFocusedRowCellValue("ITEMCODEID"),
                        true, 
                        parentID : Convert.ToInt32(gvItemList.GetFocusedRowCellValue("PARENTITEMID"))).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMRPList_Click(sender, e);
        }

        private void btnExportSKU_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportItemList("ITEM");
        }

        private void btnExportItemCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportItemList("ITEMCODE");
        }

        private void btnExportItemPrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportItemList("ITEMPRICE");
        }

        private void btnExportItemCostPrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportItemList("ITEMCOSTPRICE");
        }

        private void ExportItemList(string ExportType)
        {
            DataTable dt = new ItemCodeRepository().ExportItemList(ExportType);
            XtraReport report = null;
            try
            {
                report = new XtraReport()
                {
                    StyleSheet = {
            new XRControlStyle() { Name = "ReportHeader", Font = new System.Drawing.Font("Helvetica", 10F, System.Drawing.FontStyle.Italic) },
            new XRControlStyle() { Name = "ColumnHeader", Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold) },
            new XRControlStyle() { Name = "Title", Font = new System.Drawing.Font("Helvetica", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))))},
            new XRControlStyle() { Name = "Normal", Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular)},
            new XRControlStyle() { Name = "Footer", Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold)},
                    },
                    DisplayName = "Result file",
                    PaperKind = PaperKind.A4,
                    Margins = new Margins(50, 50, 80, 50)
                };
                report.Bands.Add(CreateReportHeader(dt));
                report.Bands.Add(CreateDetail(dt));
                report.DataSource = dt;
                report.ShowRibbonPreview();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        public ReportHeaderBand CreateReportHeader(DataTable dtColumns)
        {
            ReportHeaderBand reportHeader = null;
            try
            {
                reportHeader = new ReportHeaderBand()
                {
                    Name = "reportHeader",
                    HeightF = 40F
                };
                XRTable tablePageHeader = new XRTable()
                {
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F),
                    Name = "tablePageHeader",
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 10, 10, 96F),
                    SizeF = new System.Drawing.SizeF(720F, 40F),
                    StyleName = "ColumnHeader"
                };

                XRTableRow pageHeaderRow = new XRTableRow() { Weight = 1D };
                XRTableCell cellPageHeader = null;
                foreach (DataColumn dc in dtColumns.Columns)
                {
                    cellPageHeader = new XRTableCell();
                    if (dc.ColumnName.EndsWith("ID"))
                        cellPageHeader.WidthF = 20F;
                    cellPageHeader.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "")});
                    cellPageHeader.Multiline = true;
                    cellPageHeader.TextAlignment = TextAlignment.MiddleLeft;
                    cellPageHeader.Text = Convert.ToString(dc.ColumnName);
                    pageHeaderRow.Cells.Add(cellPageHeader);
                }
                tablePageHeader.Rows.Add(pageHeaderRow);
                reportHeader.Controls.Add(tablePageHeader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reportHeader;
        }
        public DetailBand CreateDetail(DataTable dtColumns)
        {
            DetailBand stddetail = null;
            try
            {
                stddetail = new DetailBand()
                {
                    HeightF = 25F,
                    Name = "stddetail"
                };
                XRTable tableStdDetail = new XRTable()
                {
                    LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F),
                    Name = "tableDetail",
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 96F),
                    SizeF = new System.Drawing.SizeF(720F, 25F),
                    StyleName = "Normal",
                };

                XRTableRow detailRow = new XRTableRow() { Weight = 1D };
                XRTableCell cell = null;
                foreach (DataColumn dc in dtColumns.Columns)
                {
                    cell = new XRTableCell();
                    if (dc.ColumnName.EndsWith("ID"))
                        cell.WidthF = 20F;
                    if (dc.DataType == typeof(System.Decimal))
                        cell.TextFormatString = "{0:N2}";
                    cell.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", dc.ColumnName));
                    cell.TextAlignment = TextAlignment.MiddleLeft;
                    cell.Multiline = true;
                    detailRow.Cells.Add(cell);
                }

                tableStdDetail.Rows.Add(detailRow);
                stddetail.Controls.Add(tableStdDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stddetail;
        }
    }
}
