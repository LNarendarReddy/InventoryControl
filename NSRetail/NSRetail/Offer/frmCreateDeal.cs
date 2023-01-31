using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmCreateDeal : XtraForm
    {
        public Offer offer { get; set; }
        public bool IsSave = false;
        
        public frmCreateDeal()
        {
            InitializeComponent();
        }

        private void frmAddDeal_Load(object sender, EventArgs e)
        {
            cmbFreeItemCode.Enabled = false;
            cmbFreeItemCode.EditValue = null;
            txtNumberOfItems.Enabled = false;
            txtNumberOfItems.EditValue = null;
            txtOfferValue.Enabled = false;
            txtOfferValue.EditValue = null;

            cmbOfferType.Properties.DataSource = new OfferRepository().GetOfferType(2);
            cmbOfferType.Properties.ValueMember = "OFFERTYPEID";
            cmbOfferType.Properties.DisplayMember = "OFFERTYPENAME";

            cmbFreeItemCode.Properties.DataSource = new ItemCodeRepository().GetItemPriceList();
            cmbFreeItemCode.Properties.ValueMember = "ITEMPRICEID";
            cmbFreeItemCode.Properties.DisplayMember = "ITEMNAME";

            if (Convert.ToInt32(offer.OfferID) > 0)
            {
                txtOfferCode.EditValue = offer.OfferCode;
                txtOfferName.EditValue = offer.OfferName;
                dtpStartDate.EditValue = offer.StartDate;
                dtpEndDate.EditValue = offer.EndDate;
                cmbOfferType.EditValue = offer.OfferTypeID;
                txtOfferValue.EditValue = offer.OfferValue;
                cmbFreeItemCode.EditValue = offer.FreeItemPriceID;
                txtNumberOfItems.EditValue = offer.NumberOfItems;

                txtOfferCode.Enabled = false;
                txtOfferName.Enabled = false;
                cmbOfferType.Enabled = false;
                txtOfferValue.Enabled = false;
                cmbFreeItemCode.Enabled = false;
                txtNumberOfItems.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate()) return;

                if(txtOfferValue.Enabled && txtOfferValue.EditValue == null)
                {
                    XtraMessageBox.Show("Deal value is required", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOfferValue.Focus();
                    return;
                }

                if (cmbFreeItemCode.Enabled && cmbFreeItemCode.EditValue == null)
                {
                    XtraMessageBox.Show("Free item is required", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbFreeItemCode.Focus();
                    return;
                }

                if (txtNumberOfItems.Enabled && txtNumberOfItems.EditValue == null)
                {
                    XtraMessageBox.Show("Number of items is required", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNumberOfItems.Focus();
                    return;
                }

                offer.OfferCode = txtOfferCode.EditValue;
                offer.OfferName = txtOfferName.EditValue;
                offer.StartDate = dtpStartDate.EditValue;
                offer.EndDate = dtpEndDate.EditValue;
                offer.OfferTypeID = cmbOfferType.EditValue;
                DataRowView dataRow = cmbOfferType.GetSelectedDataRow() as DataRowView;
                offer.OfferTypeCode = dataRow["OFFERTYPECODE"];
                offer.OfferTypeName = cmbOfferType.Text;
                offer.AppliesToID = 1;
                offer.AppliesToName = "ITEM";
                offer.OfferValue = txtOfferValue.EditValue;
                offer.IsActive = true;
                if (cmbFreeItemCode.Enabled)
                {
                    offer.FreeItemPriceID = cmbFreeItemCode.EditValue;
                    DataRowView idataRow = cmbFreeItemCode.GetSelectedDataRow() as DataRowView;
                    offer.SKUcode = idataRow["SKUCODE"];
                    offer.ItemCode = idataRow["ITEMCODE"];
                    offer.ItemName = idataRow["ITEMNAME"];
                }
                offer.NumberOfItems = txtNumberOfItems.EditValue;
                offer.UserID = Utility.UserID;
                offer.OfferID = new OfferRepository().SaveOffer(offer);
                this.IsSave = true;
                this.Close();
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

        private void cmbOfferType_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(offer.OfferID) > 0)
                return;
                txtOfferValue.Enabled = true;
            layoutControlItem14.Text = "Offer Value";
            cmbFreeItemCode.Enabled = false;
            cmbFreeItemCode.EditValue = null;
            txtNumberOfItems.Enabled = false;
            txtNumberOfItems.EditValue = null;
            txtOfferValue.Enabled = false;
            txtOfferValue.EditValue = null;

            if (cmbOfferType.EditValue.Equals(1) ||
                cmbOfferType.EditValue.Equals(2) ||
                cmbOfferType.EditValue.Equals(3))
            {
                txtOfferValue.Enabled = true;
            }
            else if (cmbOfferType.EditValue.Equals(1004))
            {
                layoutControlItem14.Text = "Bill Value";
                txtOfferValue.Enabled = true;
                cmbFreeItemCode.Enabled = true;
                txtNumberOfItems.Enabled = false;
            }
            else if (cmbOfferType.EditValue.Equals(1005))
            {
                layoutControlItem14.Text = "Bill Value of sub items";
                txtOfferValue.Enabled = true;
                cmbFreeItemCode.Enabled = true;
                txtNumberOfItems.Enabled = true;
            }
            else if (cmbOfferType.EditValue.Equals(1006))
            {
                txtOfferValue.Enabled = false;
                cmbFreeItemCode.Enabled = true;
                txtNumberOfItems.Enabled = true;
            }
        }
    }
}