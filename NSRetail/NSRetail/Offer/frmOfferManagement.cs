﻿using DataAccess;
using DevExpress.XtraEditors;
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
    public partial class frmOfferManagement : DevExpress.XtraEditors.XtraForm
    {
        public Offer offer { get; set; }
        public bool IsSave = false;
        private DataTable dtAppliesTo = null;
        public frmOfferManagement()
        {
            InitializeComponent();
        }

        private void frmOfferManagement_Load(object sender, EventArgs e)
        {
            cmbAppliesto.Properties.DataSource = dtAppliesTo = Utility.AppliesTo().Copy();
            cmbAppliesto.Properties.ValueMember = "AppliesToID";
            cmbAppliesto.Properties.DisplayMember = "AppliesToName";

            cmbOfferType.Properties.DataSource = new OfferRepository().GetOfferType();
            cmbOfferType.Properties.ValueMember = "OFFERTYPEID";
            cmbOfferType.Properties.DisplayMember = "OFFERTYPENAME";

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            cmbItemGroup.Properties.DataSource = new OfferRepository().GetItemGroup();
            cmbItemGroup.Properties.ValueMember = "ITEMGROUPID";
            cmbItemGroup.Properties.DisplayMember = "GROUPNAME";

            if (Convert.ToInt32(offer.OfferID) > 0)
            {
                txtOfferCode.EditValue = offer.OfferCode;
                txtOfferName.EditValue = offer.OfferName;
               dtpStartDate.EditValue = offer.StartDate;
                dtpEndDate.EditValue = offer.EndDate;
                cmbOfferType.EditValue = offer.OfferTypeID;
                chkIsActive.EditValue = offer.IsActive;
                txtOfferValue.EditValue = offer.OfferValue;
                cmbCategory.EditValue = offer.CategoryID;
                cmbItemGroup.EditValue = offer.ItemGroupID;
                cmbAppliesto.EditValue = offer.AppliesToID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate()) return;
                offer.OfferCode = txtOfferCode.EditValue;
                offer.OfferName = txtOfferName.EditValue;
                offer.StartDate = dtpStartDate.EditValue;
                offer.EndDate = dtpEndDate.EditValue;
                offer.OfferTypeID = cmbOfferType.EditValue;
                DataRowView dataRow = cmbOfferType.GetSelectedDataRow() as DataRowView;
                offer.OfferTypeCode = dataRow["OFFERTYPECODE"];
                offer.OfferTypeName = cmbOfferType.Text;
                offer.AppliesToID = cmbAppliesto.EditValue;
                offer.AppliesToName = cmbAppliesto.Text;
                offer.OfferValue = txtOfferValue.EditValue;
                offer.CategoryID = cmbCategory.EditValue;
                offer.CategoryName = cmbCategory.Text;
                offer.ItemGroupID = cmbItemGroup.EditValue;
                offer.GroupName = cmbItemGroup.Text;
                offer.IsActive = chkIsActive.EditValue;
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
            bool filterCategory = false;
            if (cmbOfferType.EditValue.Equals(1) || 
                cmbOfferType.EditValue.Equals(2) || 
                cmbOfferType.EditValue.Equals(3))
            {
                txtOfferValue.Enabled = true;
                if (cmbOfferType.EditValue.Equals(3))
                    filterCategory = true;
            }
            else
            {
                txtOfferValue.Enabled = false;
                txtOfferValue.EditValue = null;
                filterCategory = true;
            }

            if (filterCategory)
            {
                DataView dv = dtAppliesTo.Copy().DefaultView;
                dv.RowFilter = "AppliesToID <> 3";
                cmbAppliesto.Properties.DataSource = dv.ToTable();
            }
        }

        private void cmbAppliesto_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbAppliesto.EditValue.Equals(1))
            {
                cmbCategory.Enabled = false;
                cmbItemGroup.Enabled = false;
                cmbCategory.EditValue = null;
                cmbItemGroup.EditValue = null;
            }
            else if (cmbAppliesto.EditValue.Equals(2))
            {
                cmbItemGroup.Enabled = true;
                cmbCategory.EditValue = null;
                cmbCategory.Enabled = false;
            }
            else
            {
                cmbCategory.Enabled = true;
                cmbItemGroup.Enabled = false;
                cmbItemGroup.EditValue = null;
            }
        }
    }
}