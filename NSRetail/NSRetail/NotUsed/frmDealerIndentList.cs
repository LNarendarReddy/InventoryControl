using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    [Obsolete]
    public partial class frmDealerIndentList : DevExpress.XtraEditors.XtraForm
    {
        public frmDealerIndentList()
        {
            InitializeComponent();
        }

        private void frmDealerIndentList_Load(object sender, EventArgs e)
        {
            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }

        private void btnShowResult_Click(object sender, EventArgs e)
        {
            try
            {
                gcSupplierIndent.DataSource = new ReportRepository().GetSupplierIndent(cmbCategory.EditValue,
                    dtpFromDate.EditValue,dtpToDate.EditValue);
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcSupplierIndent.ShowRibbonPrintPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvSupplierIndent.FocusedRowHandle < 0)
                return;
            DealerIndent dealerIndent = new DealerIndent();
            dealerIndent.SupplierIndentID = gvSupplierIndent.GetFocusedRowCellValue("SUPPLIERINDENTID");
            dealerIndent.supplierID = gvSupplierIndent.GetFocusedRowCellValue("SUPPLIERID");
            dealerIndent.FromDate = gvSupplierIndent.GetFocusedRowCellValue("FROMDATE");
            dealerIndent.ToDate = gvSupplierIndent.GetFocusedRowCellValue("TODATE");
            dealerIndent.CategoryID = gvSupplierIndent.GetFocusedRowCellValue("CATEGORYID");
            dealerIndent.UserID = Utility.UserID;
            dealerIndent.dtSupplierIndent = new ReportRepository().GetSupplierIndentDetail(
                gvSupplierIndent.GetFocusedRowCellValue("SUPPLIERINDENTID"));
            frmDealerIndent frmDealerIndentobj = new frmDealerIndent(dealerIndent, 
                Convert.ToString(gvSupplierIndent.GetFocusedRowCellValue("DEALERNAME")));
            frmDealerIndentobj.ShowInTaskbar = false;
            frmDealerIndentobj.IconOptions.ShowIcon = false;
            frmDealerIndentobj.StartPosition = FormStartPosition.CenterScreen;
            frmDealerIndentobj.ShowDialog();
            if (dealerIndent.IsSave && dealerIndent.IsApproved)
            {
                gvSupplierIndent.SetFocusedRowCellValue("STATUS", "APPROVED");
                gvSupplierIndent.SetFocusedRowCellValue("APPROVEDBY", Utility .FullName);
                gvSupplierIndent.SetFocusedRowCellValue("APPROVEDDATE", DateTime.Now);
            }
            else if (dealerIndent.IsSave && !dealerIndent.IsApproved)
                gvSupplierIndent.SetFocusedRowCellValue("STATUS", "DRAFT");
        }

        private void btnPrintAndExport_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvSupplierIndent.FocusedRowHandle < 0 ||
                    Convert.ToString(gvSupplierIndent.GetFocusedRowCellValue("STATUS")) != "APPROVED")
                    return;
                rptDealerIndent rpt = new rptDealerIndent(new ReportRepository().GetSupplierIndentDetail(
                                gvSupplierIndent.GetFocusedRowCellValue("SUPPLIERINDENTID")));
                rpt.Parameters["IndentID"].Value = gvSupplierIndent.GetFocusedRowCellValue("SUPPLIERINDENTID");
                rpt.Parameters["SupplierName"].Value = gvSupplierIndent.GetFocusedRowCellValue("DEALERNAME");
                rpt.Parameters["ApprovedUser"].Value = gvSupplierIndent.GetFocusedRowCellValue("APPROVEDBY");
                rpt.Parameters["UserName"].Value = Utility.FullName;
                rpt.ShowRibbonPreview();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}