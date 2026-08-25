using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ErrorManagement;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.Master
{
    public partial class frmPOSReleaseManagement : XtraForm
    {
        readonly POSRepository posRepository = new POSRepository();
        string appVersion = string.Empty;
        string dbVersion = string.Empty;

        public frmPOSReleaseManagement()
        {
            InitializeComponent();
            ConfigureForm();
        }

        private void ConfigureForm()
        {
            KeyPreview = true;
            btnCancel.Click += btnCancel_Click;
            btnReleaseBuild.Click += btnReleaseBuild_Click;
            btnReleaseBuild.Enabled = false;
            Load += frmPOSReleaseManagement_Load;

            gvCounters.OptionsSelection.MultiSelect = true;
            gvCounters.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gvCounters.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gvCounters.OptionsSelection.CheckBoxSelectorColumnWidth = 35;
            gvCounters.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvCounters.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvCounters.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            gvCounters.Appearance.FocusedRow.BackColor = Color.FromArgb(62, 109, 165);
            gvCounters.Appearance.FocusedRow.ForeColor = Color.White;
            gvCounters.Appearance.FocusedRow.Options.UseBackColor = true;
            gvCounters.Appearance.FocusedRow.Options.UseForeColor = true;
            gvCounters.OptionsView.ShowAutoFilterRow = false;
            gvCounters.OptionsView.ColumnAutoWidth = true;
            gvCounters.BestFitMaxRowCount = 100;
            gvCounters.SelectionChanged += gvCounters_SelectionChanged;
            gvCounters.PopupMenuShowing += gvCounters_PopupMenuShowing;
        }

        private void frmPOSReleaseManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadReleaseData();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void LoadReleaseData()
        {
            DataSet dsReleaseData = posRepository.GetPOSReleaseManagementData();

            if (dsReleaseData.Tables.Count > 0 && dsReleaseData.Tables[0].Rows.Count > 0)
            {
                appVersion = Convert.ToString(dsReleaseData.Tables[0].Rows[0]["AppVersion"]);
                dbVersion = Convert.ToString(dsReleaseData.Tables[0].Rows[0]["DBVersion"]);
            }
            else
            {
                appVersion = string.Empty;
                dbVersion = string.Empty;
            }

            lblAppVersion.Text = $"Latest App Version : {appVersion}";
            lblDBVersion.Text = $"Latest DB Version : {dbVersion}";
            gcCounters.DataSource = dsReleaseData.Tables.Count > 1 ? dsReleaseData.Tables[1] : null;
            FormatGrid();
            UpdateReleaseBuildButtonState();
        }

        private void FormatGrid()
        {
            Dictionary<string, string> columnCaptions = new Dictionary<string, string>
            {
                { "COUNTERID", "Counter ID" },
                { "BRANCHNAME", "Branch" },
                { "BRANCHCODE", "Branch Code" },
                { "COUNTERNAME", "Counter" },
                { "COUNTERTYPE", "Counter Type" },
                { "INSTALLEDAPPVERSION", "Installed App Version" },
                { "INSTALLEDDBVERSION", "Installed DB Version" },
                { "TARGETAPPVERSION", "Target App Version" },
                { "TARGETDBVERSION", "Target DB Version" },
                { "BUILDRELEASEDATE", "Build Release Time" },
                { "LASTVERSIONCHECK", "Last Version Check" },
                { "INSTALLEDTARGETMATCH", "Installed = Target?" },
                { "LATESTINSTALLEDMATCH", "Latest = Installed?" },
                { "LATESTTARGETMATCH", "Latest = Target?" }
            };

            foreach (GridColumn column in gvCounters.Columns)
            {
                if (columnCaptions.ContainsKey(column.FieldName))
                    column.Caption = columnCaptions[column.FieldName];

                column.OptionsColumn.AllowEdit = false;
            }

            FormatDateColumn("BUILDRELEASEDATE");
            FormatDateColumn("LASTVERSIONCHECK");
            gvCounters.BestFitColumns();
        }

        private void FormatDateColumn(string fieldName)
        {
            GridColumn column = gvCounters.Columns.ColumnByFieldName(fieldName);
            if (column == null)
                return;

            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            column.DisplayFormat.FormatString = "dd-MM-yyyy hh:mm:ss tt";
        }

        private void btnReleaseBuild_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> selectedCounterIDs = GetSelectedCounterIDs();
                if (selectedCounterIDs.Count == 0)
                {
                    XtraMessageBox.Show("Please select at least one counter.", "Release Build", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ReleaseBuildToCounters(selectedCounterIDs, $"{selectedCounterIDs.Count} selected counters");
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void gvCounters_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType != GridMenuType.Row || e.HitInfo.RowHandle < 0)
                return;

            gvCounters.FocusedRowHandle = e.HitInfo.RowHandle;
            e.Menu.Items.Add(new DXMenuItem("Release Build", (o, args) => ReleaseBuildToFocusedCounter()));
        }

        private void gvCounters_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UpdateReleaseBuildButtonState();
        }

        private void UpdateReleaseBuildButtonState()
        {
            btnReleaseBuild.Enabled = gvCounters.SelectedRowsCount > 0;
        }

        private void ReleaseBuildToFocusedCounter()
        {
            try
            {
                if (gvCounters.FocusedRowHandle < 0)
                    return;

                object counterID = gvCounters.GetFocusedRowCellValue("COUNTERID");
                string counterName = Convert.ToString(gvCounters.GetFocusedRowCellValue("COUNTERNAME"));
                ReleaseBuildToCounters(new List<object> { counterID }, counterName);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                AppLog.Error(ex);
            }
        }

        private void ReleaseBuildToCounters(List<object> counterIDs, string counterText)
        {
            if (string.IsNullOrWhiteSpace(appVersion) || string.IsNullOrWhiteSpace(dbVersion))
            {
                XtraMessageBox.Show("Latest App Version or DB Version is not configured.", "Release Build", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dlgResult = XtraMessageBox.Show(
                $"Release App Version {appVersion} and DB Version {dbVersion}{Environment.NewLine}to {counterText}?",
                "Confirmation!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dlgResult != DialogResult.Yes)
                return;

            int updatedCount = posRepository.ReleasePOSBuildToCounters(counterIDs, Utility.UserID);
            LoadReleaseData();

            XtraMessageBox.Show(
                $"Build {appVersion} / DB {dbVersion} released successfully to {updatedCount} counters.",
                "Release Build",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private List<object> GetSelectedCounterIDs()
        {
            return gvCounters.GetSelectedRows()
                .Where(rowHandle => rowHandle >= 0)
                .Select(rowHandle => gvCounters.GetRowCellValue(rowHandle, "COUNTERID"))
                .Where(counterID => counterID != null && counterID != DBNull.Value)
                .Distinct()
                .ToList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
    }
}
