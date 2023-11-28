using DataAccess;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
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
    public partial class frmCreateOffer : DevExpress.XtraEditors.XtraForm
    {
        public Offer offer { get; set; }
        public bool IsSave = false;
        private DataTable dtAppliesTo = null;
        public frmCreateOffer()
        {
            InitializeComponent();
        }

        private void frmCreateOffer_Load(object sender, EventArgs e)
        {
            txtOfferValue.Enabled = false;
            txtOfferValue.EditValue = null;

            cmbAppliesto.Properties.DataSource = dtAppliesTo = Utility.AppliesTo().Copy();
            cmbAppliesto.Properties.ValueMember = "AppliesToID";
            cmbAppliesto.Properties.DisplayMember = "AppliesToName";

            cmbOfferType.Properties.DataSource = new OfferRepository().GetOfferType(1);
            cmbOfferType.Properties.ValueMember = "OFFERTYPEID";
            cmbOfferType.Properties.DisplayMember = "OFFERTYPENAME";

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            if (Convert.ToInt32(offer.OfferID) > 0)
            {
                txtOfferCode.EditValue = offer.OfferCode;
                txtOfferName.EditValue = offer.OfferName;
                dtpStartDate.EditValue = offer.StartDate;
                dtpEndDate.EditValue = offer.EndDate;
                cmbOfferType.EditValue = offer.OfferTypeID;
                txtOfferValue.EditValue = offer.OfferValue;
                cmbCategory.EditValue = offer.CategoryID;
                cmbAppliesto.EditValue = offer.AppliesToID;
                txtOfferThreshold.EditValue = offer.NumberOfItems;
                txtOfferThresholdPrice.EditValue = offer.OfferThresholdPrice;

                txtOfferCode.Enabled = false;
                txtOfferName.Enabled = false;
                cmbOfferType.Enabled = false;
                txtOfferValue.Enabled = false;
                cmbAppliesto.Enabled = false;
                cmbCategory.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cmbCategory.Enabled)
                    dxValidationProvider1.SetValidationRule(cmbCategory, null);
                if (!dxValidationProvider1.Validate()) return;
                offer.OfferCode = txtOfferCode.EditValue;
                offer.OfferName = txtOfferName.EditValue;
                offer.StartDate = dtpStartDate.EditValue;
                offer.EndDate = dtpEndDate.EditValue;
                offer.OfferTypeID = cmbOfferType.EditValue;
                offer. NumberOfItems = txtOfferThreshold.EditValue;
                offer.OfferThresholdPrice = txtOfferThresholdPrice.EditValue;
                DataRowView dataRow = cmbOfferType.GetSelectedDataRow() as DataRowView;
                offer.OfferTypeCode = dataRow["OFFERTYPECODE"];
                offer.OfferTypeName = cmbOfferType.Text;
                offer.AppliesToID = cmbAppliesto.EditValue;
                offer.AppliesToName = cmbAppliesto.Text;
                offer.OfferValue = txtOfferValue.EditValue;
                offer.CategoryID = cmbCategory.EditValue;
                offer.CategoryName = cmbCategory.Text;
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
            txtOfferValue.Enabled = false;
            txtOfferValue.EditValue = null;

            if (cmbOfferType.EditValue.Equals(1) ||
                cmbOfferType.EditValue.Equals(2) ||
                cmbOfferType.EditValue.Equals(3))
            {
                txtOfferValue.Enabled = true;
            }

            if (cmbOfferType.EditValue.Equals(3))
            {
                DataView dv = dtAppliesTo.Copy().DefaultView;
                dv.RowFilter = "AppliesToID <> 3";
                cmbAppliesto.Properties.DataSource = dv.ToTable();
            }
            else
            {
                cmbAppliesto.Properties.DataSource = dtAppliesTo;
            }
        }

        private void cmbAppliesto_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(offer.OfferID) > 0)
                return;
            if (cmbAppliesto.EditValue.Equals(3))
            {
                cmbCategory.Enabled = true;
                txtOfferThresholdPrice.Enabled = true;
                txtOfferThreshold.Enabled = true;
            }
            else
            {
                cmbCategory.EditValue = null;
                cmbCategory.Enabled = false;
                txtOfferThresholdPrice.EditValue = null;
                txtOfferThresholdPrice.Enabled = false;
                txtOfferThreshold.EditValue = null;
                txtOfferThreshold.Enabled = false;
            }
        }
    }
}