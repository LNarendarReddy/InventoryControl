using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Entity;
using ErrorManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.Master
{
    public partial class frmCounter : XtraForm
    {
        readonly Counter ObjCounter;
        readonly MasterRepository objMasterRep = new MasterRepository();
        readonly DataRepository objDataRep = new DataRepository();
        readonly Dictionary<int, RepositoryItemLookUpEdit> gatewayEditors = new Dictionary<int, RepositoryItemLookUpEdit>();
        DataTable mappings;
        DataTable gateways;
        bool updatingSettings;
        bool loaded;

        public frmCounter(Counter counter)
        {
            InitializeComponent();
            ObjCounter = counter;
        }

        private void frmCounter_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            try
            {
                cmbBranch.Properties.DataSource = objMasterRep.GetBranch();
                cmbBranch.Properties.DisplayMember = "BRANCHNAME";
                cmbBranch.Properties.ValueMember = "BRANCHID";
                gateways = objDataRep.GetDataTable("USP_R_PAYMENTGATEWAYINFO", true);
                ConfigureGatewayEditor(gatewayEditor, gateways);
                int counterID = Convert.ToInt32(ObjCounter.COUNTERID ?? 0);
                mappings = objMasterRep.GetCounterMopMappings(counterID);
                foreach (DataRow row in mappings.Rows)
                {
                    var editor = new RepositoryItemLookUpEdit();
                    ConfigureGatewayEditor(editor, GetAllowedGateways(row));
                    gatewayEditors.Add(Convert.ToInt32(row["MOPID"]), editor);
                    gcMappings.RepositoryItems.Add(editor);
                }

                if (counterID > 0)
                {
                    Text = "Edit Counter";
                    txtCounterName.EditValue = ObjCounter.COUNTERNAME;
                    cmbBranch.EditValue = ObjCounter.BRANCHID;
                    chkIsMobileCounter.Checked = ObjCounter.ISMOBILECOUNTER != DBNull.Value
                        && Convert.ToBoolean(ObjCounter.ISMOBILECOUNTER ?? false);
                }
                else Text = "New Counter";

                gcMappings.DataSource = mappings;
                loaded = true;
                ShowGatewaySettings();
                string action = counterID > 0 ? "Update" : "Create";
                btnSave.Enabled = AccessUtility.HasAccess($"D7371CA4-24FF-44FB-911B-7801FC5180F3::{action}");
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void ConfigureGatewayEditor(RepositoryItemLookUpEdit editor, DataTable source)
        {
            editor.DataSource = source;
            editor.DisplayMember = "PAYMENTGATEWAYINFONAME";
            editor.ValueMember = "PAYMENTGATEWAYINFOID";
            editor.NullText = "";
            editor.Columns.Clear();
            editor.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PAYMENTGATEWAYINFONAME", "Gateway"));
            editor.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PAYMENTGATEWAYINFOTYPE", "Type"));
        }

        private DataTable GetAllowedGateways(DataRow row)
        {
            return Convert.ToBoolean(row["ISGATEWAYCONFIGURABLE"])
                ? gateways : gateways.Clone();
        }

        private DataRow GetGateway(DataRow row)
        {
            if (row == null || row.IsNull("PAYMENTGATEWAYINFOID") || gateways == null) return null;
            return gateways.AsEnumerable().FirstOrDefault(g =>
                Convert.ToInt32(g["PAYMENTGATEWAYINFOID"]) == Convert.ToInt32(row["PAYMENTGATEWAYINFOID"]));
        }

        private static JObject ReadSettings(DataRow row)
        {
            string json = Convert.ToString(row["PAYMENTGATEWAYADDITIONALCONFIG"]);
            return string.IsNullOrWhiteSpace(json) ? new JObject() : JObject.Parse(json);
        }

        private bool CanConfigure(DataRow row)
        {
            return row != null && Convert.ToBoolean(row["ISAVAILABLE"])
                && Convert.ToBoolean(row["ISGATEWAYCONFIGURABLE"]);
        }

        private void ShowGatewaySettings()
        {
            updatingSettings = true;
            try
            {
                DataRow row = gvMappings.GetFocusedDataRow();
                DataRow gateway = GetGateway(row);
                gatewaySection.Text = row == null ? "Payment Gateway Configuration"
                    : "Payment Gateway Configuration - " + row["MOPNAME"];
                gatewaySection.Enabled = CanConfigure(row) && gateway != null;
                lciSetting1.Text = "";
                lciSetting2.Text = "";
                txtSetting1.EditValue = null;
                txtSetting2.EditValue = null;
                lciSetting2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!gatewaySection.Enabled) return;

                JObject config = ReadSettings(row);
                switch (Convert.ToString(gateway["PAYMENTGATEWAYINFOTYPE"]).ToLowerInvariant())
                {
                    case "pinelabs":
                        lciSetting1.Text = "Client ID";
                        lciSetting2.Text = "Store ID";
                        txtSetting1.EditValue = (string)config["ClientID"];
                        txtSetting2.EditValue = (string)config["StoreID"];
                        lciSetting2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case "bharathpe":
                        lciSetting1.Text = "Username";
                        lciSetting2.Text = "Password";
                        txtSetting1.EditValue = (string)config["UserName"];
                        txtSetting2.EditValue = (string)config["Password"];
                        lciSetting2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case "mobikwik":
                        lciSetting1.Text = "Sub-merchant ID (optional)";
                        txtSetting1.EditValue = (string)config["smid"];
                        break;
                    default:
                        gatewaySection.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                gatewaySection.Enabled = false;
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
            finally { updatingSettings = false; }
        }

        private void gatewaySetting_EditValueChanged(object sender, EventArgs e)
        {
            if (updatingSettings || !loaded) return;
            DataRow row = gvMappings.GetFocusedDataRow();
            DataRow gateway = GetGateway(row);
            if (!CanConfigure(row) || gateway == null) return;
            try
            {
                JObject config = ReadSettings(row);
                string first = Convert.ToString(txtSetting1.EditValue).Trim();
                string second = Convert.ToString(txtSetting2.EditValue);
                switch (Convert.ToString(gateway["PAYMENTGATEWAYINFOTYPE"]).ToLowerInvariant())
                {
                    case "pinelabs": config["ClientID"] = first; config["StoreID"] = second.Trim(); break;
                    case "bharathpe": config["UserName"] = first; config["Password"] = second; break;
                    case "mobikwik":
                        if (first.Length == 0) config.Remove("smid");
                        else config["smid"] = first;
                        break;
                }
                row["PAYMENTGATEWAYADDITIONALCONFIG"] = config.ToString(Formatting.None);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void availableEditor_EditValueChanged(object sender, EventArgs e)
        {
            gvMappings.PostEditor();
        }

        private void gvMappings_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvMappings.FocusedColumn == colGateway)
                e.Cancel = !CanConfigure(gvMappings.GetFocusedDataRow());
        }

        private void gvMappings_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow row = gvMappings.GetDataRow(e.RowHandle);
            if (e.Column == colGateway && row != null)
                e.RepositoryItem = gatewayEditors[Convert.ToInt32(row["MOPID"])];
        }

        private void gvMappings_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (loaded) ShowGatewaySettings();
        }

        private void gvMappings_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!loaded) return;
            if (e.Column == colGateway)
                gvMappings.GetDataRow(e.RowHandle)["PAYMENTGATEWAYADDITIONALCONFIG"] = DBNull.Value;
            if (e.Column == colGateway || e.Column == colAvailable) ShowGatewaySettings();
        }

        private void gvMappings_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == colGateway && e.RowHandle >= 0 && !CanConfigure(gvMappings.GetDataRow(e.RowHandle)))
            {
                e.Appearance.BackColor = SystemColors.Control;
                e.Appearance.ForeColor = SystemColors.GrayText;
            }
        }

        private DataTable BuildMappingParameters()
        {
            DataTable result = new DataTable();
            result.Columns.Add("MOPID", typeof(int));
            result.Columns.Add("PAYMENTGATEWAYINFOID", typeof(int));
            result.Columns.Add("PAYMENTGATEWAYADDITIONALCONFIG", typeof(string));
            foreach (DataRow row in mappings.Rows)
            {
                if (!Convert.ToBoolean(row["ISAVAILABLE"])) continue;
                if (!Convert.ToBoolean(row["ISGATEWAYCONFIGURABLE"]))
                {
                    result.Rows.Add(row["MOPID"], DBNull.Value, DBNull.Value);
                    continue;
                }

                DataRow gateway = GetGateway(row);
                if (gateway == null)
                    throw new InvalidOperationException($"Select an active payment gateway for {row["MOPNAME"]}.");

                JObject config = ReadSettings(row);
                string type = Convert.ToString(gateway["PAYMENTGATEWAYINFOTYPE"]).ToLowerInvariant();
                string[] required = type == "pinelabs" ? new[] { "ClientID", "StoreID" }
                    : type == "bharathpe" ? new[] { "UserName", "Password" } : new string[0];
                if (required.Any(key => string.IsNullOrWhiteSpace((string)config[key])))
                    throw new InvalidOperationException($"Complete the gateway settings for {row["MOPNAME"]}.");
                result.Rows.Add(row["MOPID"], row["PAYMENTGATEWAYINFOID"], config.ToString(Formatting.None));
            }
            return result;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!loaded || !btnSave.Enabled) return;
            try
            {
                gvMappings.CloseEditor();
                if (!gvMappings.UpdateCurrentRow()) return;
                cmbBranch.ErrorText = cmbBranch.EditValue == null || cmbBranch.EditValue == DBNull.Value ? "Select a branch." : "";
                txtCounterName.ErrorText = string.IsNullOrWhiteSpace(txtCounterName.Text) ? "Enter a counter name." : "";
                if (cmbBranch.ErrorText.Length > 0 || txtCounterName.ErrorText.Length > 0) return;

                ObjCounter.MopMappings = BuildMappingParameters();
                ObjCounter.COUNTERNAME = txtCounterName.Text.Trim();
                ObjCounter.BRANCHID = cmbBranch.EditValue;
                ObjCounter.ISMOBILECOUNTER = chkIsMobileCounter.Checked;
                ObjCounter.UserID = Utility.UserID;
                objMasterRep.SaveCounter(ObjCounter);
                ObjCounter.IsSave = true;
                Close();
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
            Close();
        }
    }
}
