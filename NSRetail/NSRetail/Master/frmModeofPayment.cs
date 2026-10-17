using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Entity;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Master
{
    public partial class frmModeofPayment : DevExpress.XtraEditors.XtraForm
    {
        MOP ObjMOP = new MOP();
        MasterRepository ObjMasterRep = new MasterRepository();
        public frmModeofPayment()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmModeofPayment_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtMOP = ObjMasterRep.GetMOP();
                dtMOP.Columns["ISGATEWAYCONFIGURABLE"].DefaultValue = false;
                dtMOP.AcceptChanges();
                gcMOP.DataSource = dtMOP;
                AccessUtility.SetStatusByAccess(gvMOP);
                AccessUtility.SetStatusByAccess(gcDelete);

                string accessIdentifier = "5A872299-57E3-4DB3-A55A-8BFE9CFB346B";
                
                gvMOP.Tag = $"{accessIdentifier}::Create";
                AccessUtility.SetStatusByAccess(gvMOP);

                gvMOP.Tag = $"{accessIdentifier}::Update";
                AccessUtility.SetStatusByAccess(gvMOP);

                AccessUtility.SetStatusByAccess(gcDelete);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void gvCategory_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, view.Columns["MOPID"], -1);
                view.SetRowCellValue(e.RowHandle, gcGatewayConfigurable, false);
            }
            catch (Exception ex) { }
        }

        private void gvCategory_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = (e.Row as DataRowView)?.Row;
            try
            {
                if (row == null) return;
                ObjMOP.MOPNAME = Convert.ToString(row["MOPNAME"]);
                ObjMOP.MOPID = Convert.ToInt32(row["MOPID"]);
                ObjMOP.ISGATEWAYCONFIGURABLE = !row.IsNull("ISGATEWAYCONFIGURABLE")
                    && Convert.ToBoolean(row["ISGATEWAYCONFIGURABLE"]);
                if (Convert.ToInt32(ObjMOP.MOPID) > 0 && row.HasVersion(DataRowVersion.Original))
                {
                    string originalName = Convert.ToString(row["MOPNAME", DataRowVersion.Original]);
                    object originalGatewayValue = row["ISGATEWAYCONFIGURABLE", DataRowVersion.Original];
                    bool originalGatewayConfigurable = originalGatewayValue != DBNull.Value
                        && Convert.ToBoolean(originalGatewayValue);
                    List<string> changes = new List<string>();
                    if (originalName != Convert.ToString(ObjMOP.MOPNAME))
                        changes.Add($"MOP: {originalName} -> {ObjMOP.MOPNAME}");
                    if (originalGatewayConfigurable != ObjMOP.ISGATEWAYCONFIGURABLE)
                        changes.Add($"Gateway Configurable: {(originalGatewayConfigurable ? "Yes" : "No")} -> {(ObjMOP.ISGATEWAYCONFIGURABLE ? "Yes" : "No")}");

                    if (changes.Count == 0) return;

                    string message = $"Save changes to MOP '{originalName}'?"
                        + Environment.NewLine + Environment.NewLine
                        + string.Join(Environment.NewLine, changes);
                    if (XtraMessageBox.Show(this, message, "Confirm MOP Update",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        row.RejectChanges();
                        return;
                    }
                }
                ObjMOP.UserID = Utility.UserID;
                ObjMasterRep.SaveMOP(ObjMOP);
                row["MOPID"] = ObjMOP.MOPID;
                row.AcceptChanges();
            }
            catch (Exception ex)
            {
                if (row != null && row.RowState == DataRowState.Modified)
                    row.RejectChanges();
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void gvMOP_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvMOP.FocusedColumn != gcGatewayConfigurable) return;

            string action = gvMOP.IsNewItemRow(gvMOP.FocusedRowHandle) ? "Create" : "Update";
            e.Cancel = !AccessUtility.HasAccess($"5A872299-57E3-4DB3-A55A-8BFE9CFB346B::{action}");
        }

        private void frmModeofPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            gvMOP.CloseEditor();
            if (!gvMOP.UpdateCurrentRow())
                e.Cancel = true;
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var dlgResult = XtraMessageBox.Show("Are you sure want to delete?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (Convert.ToString(dlgResult) == "OK" && gvMOP.FocusedRowHandle >= 0)
                {
                    ObjMOP = new MOP();
                    ObjMOP.MOPID = gvMOP.GetFocusedRowCellValue("MOPID");
                    ObjMOP.UserID = Utility.UserID;
                    ObjMasterRep.DeleteMOP(ObjMOP);
                    gvMOP.DeleteRow(gvMOP.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }
    }
}
