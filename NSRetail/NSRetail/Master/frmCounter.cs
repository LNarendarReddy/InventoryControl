using DataAccess;
using Entity;
using ErrorManagement;
using Newtonsoft.Json;
using System;
using System.Data;

namespace NSRetail.Master
{
    public partial class frmCounter : DevExpress.XtraEditors.XtraForm
    {
        readonly Counter ObjCounter = null;
        readonly MasterRepository objMasterRep = new MasterRepository();
        readonly DataRepository objDataRep = new DataRepository();

        public frmCounter(Counter _ObjCounter)
        {
            InitializeComponent();
            ObjCounter = _ObjCounter;
        }

        private void frmCounter_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = objMasterRep.GetBranch();
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.Properties.ValueMember = "BRANCHID";

            luPaymentGateway.Properties.DataSource = objDataRep.GetDataTable("USP_R_PAYMENTGATEWAYINFO", true);
            luPaymentGateway.Properties.DisplayMember = "PAYMENTGATEWAYINFONAME";
            luPaymentGateway.Properties.ValueMember = "PAYMENTGATEWAYINFOID";

            luPaymentGateway.ItemIndex = 0;

            if (Convert.ToInt32(ObjCounter.COUNTERID) > 0)
            {
                this.Text = "Edit Counter";
                txtCounterName.EditValue = ObjCounter.COUNTERNAME;
                cmbBranch.EditValue = ObjCounter.BRANCHID;
                chkIsMobileCounter.EditValue = ObjCounter.ISMOBILECOUNTER;
                luPaymentGateway.EditValue = ObjCounter.PAYMENTGATEWAYINFOID;

                switch(luPaymentGateway.GetColumnValue("PAYMENTGATEWAYINFOTYPE").ToString().ToLower())
                {
                    case "pinelabs":
                        PineLabsPaymentGateway pineLabsPaymentGateway = JsonConvert.DeserializeObject<PineLabsPaymentGateway>(ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG.ToString());
                        txtSetting1.EditValue = pineLabsPaymentGateway?.ClientID;
                        txtSetting2.EditValue = pineLabsPaymentGateway?.StoreID;
                        ObjCounter.PaymentGatewayInstance = pineLabsPaymentGateway;
                        break;
                    case "bharathpe":
                        BharathPePaymentGateway bharatPePaymentGateway = JsonConvert.DeserializeObject<BharathPePaymentGateway>(ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG.ToString());
                        txtSetting1.EditValue = bharatPePaymentGateway?.UserName;
                        txtSetting2.EditValue = bharatPePaymentGateway?.Password;
                        ObjCounter.PaymentGatewayInstance = bharatPePaymentGateway;
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjCounter.COUNTERNAME = txtCounterName.EditValue;
                ObjCounter.BRANCHID = cmbBranch.EditValue;
                ObjCounter.UserID = Utility.UserID;
                ObjCounter.ISMOBILECOUNTER = chkIsMobileCounter.EditValue;
                ObjCounter.PAYMENTGATEWAYINFOID = luPaymentGateway.EditValue;

                switch (luPaymentGateway.GetColumnValue("PAYMENTGATEWAYINFOTYPE").ToString().ToLower())
                {
                    case "pinelabs":
                        PineLabsPaymentGateway pineLabsPaymentGateway = new PineLabsPaymentGateway()
                        {
                            ClientID = txtSetting1.EditValue,
                            StoreID = txtSetting2.EditValue
                        };
                        ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG = JsonConvert.SerializeObject(pineLabsPaymentGateway);
                        break;
                    case "bharathpe":
                        BharathPePaymentGateway bharathPePaymentGateway = new BharathPePaymentGateway()
                        {
                            UserName = txtSetting1.EditValue,
                            Password = txtSetting2.EditValue
                        };
                        ObjCounter.PAYMENTGATEWAYADDITIONALCONFIG = JsonConvert.SerializeObject(bharathPePaymentGateway);
                        break;
                }

                objMasterRep.SaveCounter(ObjCounter);
                ObjCounter.IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjCounter.IsSave = false;
            this.Close();
        }

        private void luPaymentGateway_Leave(object sender, EventArgs e)
        {
            if (luPaymentGateway.EditValue == null) return;

            DataRowView drvSelectedPaymentGatewayInfo = luPaymentGateway.GetSelectedDataRow() as DataRowView;

            if (drvSelectedPaymentGatewayInfo == null) return;

            switch (drvSelectedPaymentGatewayInfo["PAYMENTGATEWAYINFOTYPE"].ToString())
            {
                case "PineLabs":
                    lcgPaymentGatewayConfig.Text = "Payment Gateway configuration - PineLabs";
                    lciSetting1.Text = "Client ID";
                    lciSetting2.Text = "Store ID";
                    ObjCounter.PaymentGatewayInstance = new PineLabsPaymentGateway();
                    break;
                case "BharathPe":
                    lcgPaymentGatewayConfig.Text = "Payment Gateway configuration - BharathPe";
                    lciSetting1.Text = "Username";
                    lciSetting2.Text = "Password";
                    ObjCounter.PaymentGatewayInstance = new BharathPePaymentGateway();
                    break;
            }
        }

    }
}