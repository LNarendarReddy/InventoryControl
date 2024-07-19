using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using Entity;
using ErrorManagement;
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
    public partial class frmDealerIndent : DevExpress.XtraEditors.XtraForm
    {
        DealerIndent dealerIndent = null;
        DataRow drSelectedPrice;

        public frmDealerIndent(DealerIndent _dealerIndent, string DealerName)
        {
            InitializeComponent();
            dealerIndent = _dealerIndent;
            txtDealerName.EditValue = DealerName;
            txtMobileNo.EditValue = _dealerIndent.MobileNo;
            lcgIndentDetails.Text = $"Indent details - {_dealerIndent.IndentNo}";
            btnReject.Enabled = Utility.Role.ToLower() == "admin";
        }

        private void frmDealerIndent_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = btnReject.Enabled = dealerIndent.Status.Equals("DRAFT");
            btnSave.Text = Utility.Role == "Admin" ? "Approve" : "Save";
            txtIndentDays.EditValue = dealerIndent.IndentDays;
            txtSafetyDays.EditValue = dealerIndent.SafetyDays;
            gcSupplierIndent.DataSource = dealerIndent.dtSupplierIndent;

            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMCODE";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSupplierIndent(Utility.Role.ToLower() == "admin" ? 1 :0);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvSupplierIndent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                gvSupplierIndent.MoveNext();
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null) { return; }
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
            txtItemName.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMNAME");
            bool isOpenItem = Convert.ToBoolean(sluItemCodeView.GetRowCellValue(rowHandle, "ISOPENITEM"));
            DataTable dtPrices = new ItemCodeRepository().GetMRPList(sluItemCode.EditValue);
            if (dtPrices.Rows.Count > 1)
            {
                frmMRPList mRPSelection = new frmMRPList(dtPrices, sluItemCode.EditValue)
                { StartPosition = FormStartPosition.CenterScreen };
                mRPSelection.ShowDialog();
                if (!mRPSelection._IsSave)
                {
                    ClearItemData(true);
                    return;
                }

                drSelectedPrice = (mRPSelection.drSelected as DataRowView)?.Row;
            }
            else if (dtPrices.Rows.Count == 1)
            {
                drSelectedPrice = dtPrices.Rows[0];
            }

            if (drSelectedPrice == null)
            {
                return;
            }

            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtQtyOrWghtInKGs.Focus();
        }

        private void ClearItemData(bool focusItemCode = false)
        {
            txtItemName.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            
            txtQtyOrWghtInKGs.EditValue = null;
            //txtWeightInKgs.EditValue = 0.00;
            drSelectedPrice = null;
            if (focusItemCode)
                sluItemCode.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(drSelectedPrice == null || txtQtyOrWghtInKGs.EditValue == null)
            {
                XtraMessageBox.Show("Enter item and quantity to add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            try
            {
                new SupplierRepository().AddSupplierIndentDetail(dealerIndent.SupplierIndentID, drSelectedPrice["ITEMPRICEID"], txtQtyOrWghtInKGs.EditValue, Utility.UserID);

                dealerIndent.dtSupplierIndent = new ReportRepository().GetSupplierIndentDetail(dealerIndent.SupplierIndentID);
                gcSupplierIndent.DataSource = dealerIndent.dtSupplierIndent;
                ClearItemData(true);
            }
            catch (Exception ex) { ErrorMgmt.ShowError(ex); }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSupplierIndent(2);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }

        private void SaveSupplierIndent(int status)
        {
            dealerIndent.IsApproved = status;
            dealerIndent.MobileNo = txtMobileNo.EditValue;
            new ReportRepository().SaveSupplierIndent(dealerIndent);
            dealerIndent.IsSave = true;
            XtraMessageBox.Show("Saved successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}