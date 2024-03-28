using DataAccess;
using DevExpress.Utils.About;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
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

namespace NSRetail.Stock
{
    public partial class frmAddProcessing : XtraForm
    {
        object itemPriceID;
        double? multiplier;
        object addBulkProcessingID;
        bool isOpenItem;

        ReportRepository reportRepository = new ReportRepository();
        StockRepository stockRepository = new StockRepository();

        public frmAddProcessing()
        {
            InitializeComponent();
        }

        private void frmAddProcessing_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMCODE";

            txtQtyOrWeight.ConfirmBarCodeScan();

            BindData();
        }

        private void sluItemCode_Popup(object sender, EventArgs e)
        {
            (sender as SearchLookUpEdit).Properties.PopupView.ActiveFilterString = "[PARENTITEMID] <> 0";
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null)
            {
                ClearObjects();
                return;
            }

            try
            {
                int rowhandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
                txtItemName.EditValue = sluItemCodeView.GetRowCellValue(rowhandle, "ITEMNAME");
                DataTable dtMRPList = new ItemCodeRepository().GetMRPList(sluItemCode.EditValue);
                if(dtMRPList.Rows.Count == 0)
                {
                    XtraMessageBox.Show("MRP not found!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearObjects();
                    return;
                }
                else if (dtMRPList.Rows.Count > 1)
                {
                    frmMRPList obj = new frmMRPList(dtMRPList, sluItemCode.EditValue);
                    obj.ShowDialog();
                    if (obj._IsSave)
                    {
                        txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                        txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                        itemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                    }
                }
                else
                {
                    txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                    txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                    itemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                }

                multiplier = double.Parse(sluItemCodeView.GetRowCellValue(rowhandle, "MULTIPLIER").ToString());
                object parentItemID = sluItemCodeView.GetRowCellValue(rowhandle, "PARENTITEMID");
                int parentRowHandle = sluItemCodeView.LocateByValue("ITEMID", parentItemID);
                if (parentRowHandle == -1)
                {
                    XtraMessageBox.Show("Parent item not found!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearObjects();
                    return;
                }

                txtParentSKU.EditValue = $"{sluItemCodeView.GetRowCellValue(parentRowHandle, "ITEMNAME")} ( {sluItemCodeView.GetRowCellValue(parentRowHandle, "SKUCODE")} )";

                isOpenItem = bool.Parse(sluItemCodeView.GetRowCellValue(rowhandle, "ISOPENITEM").ToString());
                SetQtyOrWeightFormat();

                txtQtyOrWeight.Focus();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!dxValidationProvider11.Validate())
            {
                return; 
            }

            if(txtQtyOrWeight.EditValue.Equals(0))
            {
                XtraMessageBox.Show("Quantity cannot be zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
                txtQtyOrWeight.Focus();
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(addBulkProcessingID?.ToString())) 
                {
                    BindData(true);
                }

                stockRepository.AddBulkProcessingDetail(addBulkProcessingID, 0, itemPriceID, isOpenItem, txtQtyOrWeight.EditValue);

                BindData();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }

            ClearObjects();
        }

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtQtyOrWeight.EditValue?.ToString(), out double quantity) && multiplier.HasValue)
                txtCalculatedWeight.EditValue = quantity * multiplier.Value;
        }

        private void ClearObjects()
        {
            sluItemCode.EditValue =
            txtItemName.EditValue =
            txtParentSKU.EditValue =
            txtMRP.EditValue =
            txtSalePrice.EditValue =
            txtQtyOrWeight.EditValue =
            txtCalculatedWeight.EditValue = null;

            itemPriceID = multiplier = null;
            isOpenItem = false;
            SetQtyOrWeightFormat();
            sluItemCode.Focus();
        }

        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to delete?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                stockRepository.DeleteBulkProcessingDetail(gvBulkProcessing.GetFocusedRowCellValue("ADDBULKPROCESSINGDETAILID"), addBulkProcessingID);
                gvBulkProcessing.DeleteRow(gvBulkProcessing.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvBulkProcessing_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY") return;

            stockRepository.AddBulkProcessingDetail(addBulkProcessingID
                , gvBulkProcessing.GetRowCellValue(e.RowHandle, "ADDBULKPROCESSINGDETAILID")
                , gvBulkProcessing.GetRowCellValue(e.RowHandle, "ITEMPRICEID")
                , bool.Parse(gvBulkProcessing.GetRowCellValue(e.RowHandle, "ISOPENITEM").ToString())
                , gvBulkProcessing.GetRowCellValue(e.RowHandle, "QUANTITY"));            
        }

        private void BindData(bool createBulk = false)
        {
            DataSet dsAddBulkProcessing = reportRepository.GetReportDataset("USP_CR_ADDBULKPROCESSING"
                , new Dictionary<string, object>
                {
                    { "UserID", Utility.UserID }
                    , { "CreateBulk", createBulk }
                });

            addBulkProcessingID = dsAddBulkProcessing.Tables[0].Rows[0][0];

            gcBulkProcessing.DataSource = dsAddBulkProcessing.Tables.Count > 1 ? dsAddBulkProcessing.Tables[1] : null;
        }

        private void SetQtyOrWeightFormat()
        {
            if (isOpenItem)
            {
                txtQtyOrWeight.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings => settings.MaskExpression = "n2");
                txtQtyOrWeight.Properties.EditFormat.FormatString = "n2";
                txtQtyOrWeight.EditValue = 0.00;
                lciQtyOrWght.Text = "Weight (in KGs)";
            }
            else
            {
                txtQtyOrWeight.Properties.MaskSettings.Configure<MaskSettings.Numeric>(settings => settings.MaskExpression = "d");
                txtQtyOrWeight.Properties.EditFormat.FormatString = "d";
                txtQtyOrWeight.EditValue = 0;
                lciQtyOrWght.Text = "Quantity (# of packets)";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to submit bulk processing? The operation cannot be reversed", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                stockRepository.SubmitBulkProcessing(addBulkProcessingID, Utility.UserID);
                XtraMessageBox.Show("Submit completed successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                addBulkProcessingID = null;
                ClearObjects();
                gcBulkProcessing.DataSource = null;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvBulkProcessing_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if(e.Column.FieldName != "QUANTITY") return;

            RepositoryItemTextEdit inPlaceEditor = new RepositoryItemTextEdit();
            string maskString = bool.Parse(gvBulkProcessing.GetRowCellValue(e.RowHandle, "ISOPENITEM").ToString()) ? "n2" : "d";

            inPlaceEditor.MaskSettings.Configure<MaskSettings.Numeric>(settings => settings.MaskExpression = maskString);
            inPlaceEditor.EditFormat.FormatString = maskString;

            e.RepositoryItem = inPlaceEditor;
        }
    }
}
