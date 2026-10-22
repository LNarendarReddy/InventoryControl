using DataAccess;
using Entity;
using ErrorManagement;
using Newtonsoft.Json;
using NSRetail.Utilities;
using System;
using System.Data;

namespace NSRetail.Master
{
    public partial class frmCounterLegacy : DevExpress.XtraEditors.XtraForm
    {
        readonly Counter ObjCounter = null;
        readonly MasterRepository objMasterRep = new MasterRepository();
        readonly DataRepository objDataRep = new DataRepository();
        bool loaded;

        public frmCounterLegacy(Counter _ObjCounter)
        {
            InitializeComponent();
            ObjCounter = _ObjCounter;
        }

        private void frmCounterLegacy_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            try
            {
                cmbBranch.Properties.DataSource = objMasterRep.GetBranch();
                cmbBranch.Properties.DisplayMember = "BRANCHNAME";
                cmbBranch.Properties.ValueMember = "BRANCHID";

                DataTable dtPaymentGateways = objDataRep.GetDataTable("USP_R_PAYMENTGATEWAYINFO", true);
                DataRow drEmpty = dtPaymentGateways.NewRow();
                dtPaymentGateways.Rows.InsertAt(drEmpty, 0);
                dtPaymentGateways.DefaultView.RowFilter = "PAYMENTGATEWAYINFOTYPE IS NULL OR PAYMENTGATEWAYINFOTYPE IN ('PineLabs', 'BharathPe')";
                luPaymentGateway.Properties.DataSource = dtPaymentGateways.DefaultView;
                luPaymentGateway.Properties.DisplayMember = "PAYMENTGATEWAYINFONAME";
                luPaymentGateway.Properties.ValueMember = "PAYMENTGATEWAYINFOID";

                luPaymentGateway.ItemIndex = 0;

                if (Convert.ToInt32(ObjCounter.COUNTERID ?? 0) > 0)
                {
                    this.Text = "Edit Counter";
                    txtCounterName.EditValue = ObjCounter.COUNTERNAME;
                    cmbBranch.EditValue = ObjCounter.BRANCHID;
                    chkIsMobileCounter.EditValue = ObjCounter.ISMOBILECOUNTER;
                    luPaymentGateway.EditValue = ObjCounter.PAYMENTGATEWAYINFOID;

                    string settings = Convert.ToString(ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG);
                    if (string.IsNullOrWhiteSpace(settings)) settings = "{}";
                    switch (Convert.ToString(luPaymentGateway.GetColumnValue("PAYMENTGATEWAYINFOTYPE")).ToLowerInvariant())
                    {
                        case "pinelabs":
                            PineLabsPaymentGateway pineLabsPaymentGateway = JsonConvert.DeserializeObject<PineLabsPaymentGateway>(settings);
                            txtSetting1.EditValue = pineLabsPaymentGateway?.ClientID;
                            txtSetting2.EditValue = pineLabsPaymentGateway?.StoreID;
                            ObjCounter.PaymentGatewayInstance = pineLabsPaymentGateway;
                            break;
                        case "bharathpe":
                            BharathPePaymentGateway bharatPePaymentGateway = JsonConvert.DeserializeObject<BharathPePaymentGateway>(settings);
                            txtSetting1.EditValue = bharatPePaymentGateway?.UserName;
                            txtSetting2.EditValue = bharatPePaymentGateway?.Password;
                            ObjCounter.PaymentGatewayInstance = bharatPePaymentGateway;
                            break;
                    }
                }
                else Text = "New Counter";
                loaded = true;
                UpdateGatewayLabels();
                string action = Convert.ToInt32(ObjCounter.COUNTERID ?? 0) > 0 ? "Update" : "Create";
                btnSave.Enabled = AccessUtility.HasAccess($"D7371CA4-24FF-44FB-911B-7801FC5180F3::{action}");
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!loaded || !btnSave.Enabled) return;
                if (!dxValidationProvider1.Validate())
                    return;
                if (string.IsNullOrWhiteSpace(txtCounterName.Text))
                    throw new InvalidOperationException("Enter a counter name.");
                ObjCounter.COUNTERNAME = txtCounterName.Text.Trim();
                ObjCounter.BRANCHID = cmbBranch.EditValue;
                ObjCounter.UserID = Utility.UserID;
                ObjCounter.ISMOBILECOUNTER = chkIsMobileCounter.EditValue;
                if (luPaymentGateway.EditValue != null && luPaymentGateway.EditValue != DBNull.Value)
                {
                    string type = Convert.ToString(luPaymentGateway.GetColumnValue("PAYMENTGATEWAYINFOTYPE")).ToLowerInvariant();
                    if (type != "pinelabs" && type != "bharathpe")
                        throw new InvalidOperationException("Select a supported payment gateway or clear the selection.");
                    if (string.IsNullOrWhiteSpace(txtSetting1.Text) || string.IsNullOrWhiteSpace(txtSetting2.Text))
                        throw new InvalidOperationException("Complete the payment gateway settings.");
                    ObjCounter.PAYMENTGATEWAYINFOID = luPaymentGateway.EditValue;

                    switch (type)
                    {
                        case "pinelabs":
                            PineLabsPaymentGateway pineLabsPaymentGateway = new PineLabsPaymentGateway()
                            {
                                ClientID = txtSetting1.EditValue != null ? Convert.ToString(txtSetting1.EditValue).Trim() : txtSetting1.EditValue,
                                StoreID = txtSetting2.EditValue != null ? Convert.ToString(txtSetting2.EditValue).Trim() : txtSetting2.EditValue,
                            };
                            ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG = JsonConvert.SerializeObject(pineLabsPaymentGateway);
                            break;
                        case "bharathpe":
                            BharathPePaymentGateway bharathPePaymentGateway = new BharathPePaymentGateway()
                            {
                                UserName = txtSetting1.EditValue != null ? Convert.ToString(txtSetting1.EditValue).Trim() : txtSetting1.EditValue,
                                Password = txtSetting2.EditValue
                            };
                            ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG = JsonConvert.SerializeObject(bharathPePaymentGateway);
                            break;
                    }
                }
                else
                {
                    ObjCounter.PAYMENTGATEWAYINFOID = DBNull.Value;
                    ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG = DBNull.Value;
                }

                objMasterRep.SaveCounterLegacy(ObjCounter);
                ObjCounter.IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjCounter.IsSave = false;
            this.Close();
        }

        private void luPaymentGateway_Leave(object sender, EventArgs e)
        {
            if (!loaded) return;
            txtSetting1.EditValue = txtSetting2.EditValue = null;
            UpdateGatewayLabels();
        }

        private void UpdateGatewayLabels()
        {
            lcgPaymentGatewayConfig.Enabled = false;
            lcgPaymentGatewayConfig.Text = "Payment Gateway Configuration";
            lciSetting1.Text = lciSetting2.Text = "";
            txtSetting2.Properties.UseSystemPasswordChar = false;
            DataRowView drvSelectedPaymentGatewayInfo = luPaymentGateway.GetSelectedDataRow() as DataRowView;

            if (drvSelectedPaymentGatewayInfo == null) return;

            switch (Convert.ToString(drvSelectedPaymentGatewayInfo["PAYMENTGATEWAYINFOTYPE"]).ToLowerInvariant())
            {
                case "pinelabs":
                    lcgPaymentGatewayConfig.Enabled = true;
                    lcgPaymentGatewayConfig.Text = "Payment Gateway configuration - PineLabs";
                    lciSetting1.Text = "Client ID";
                    lciSetting2.Text = "Store ID";
                    ObjCounter.PaymentGatewayInstance = new PineLabsPaymentGateway();
                    break;
                case "bharathpe":
                    lcgPaymentGatewayConfig.Enabled = true;
                    txtSetting2.Properties.UseSystemPasswordChar = true;
                    lcgPaymentGatewayConfig.Text = "Payment Gateway configuration - BharathPe";
                    lciSetting1.Text = "Username";
                    lciSetting2.Text = "Password";
                    ObjCounter.PaymentGatewayInstance = new BharathPePaymentGateway();
                    break;
            }
        }

    }
}
