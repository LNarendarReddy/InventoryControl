using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.POS
{
    public partial class ucInstalledVersions : SearchCriteriaBase
    {
        public ucInstalledVersions()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>() 
                { 
                    { "BRANCHCODE", "Branch Code" }
                    , { "APPVERSION", "Application Version" }
                    , { "DBVERSION", "Database Version" }
                    , { "CREATEDATE", "Created Date" }
                    , { "LASTVERSIONCHECK", "Last known version check" }
                    , { "ISUPTODATE", "Is up-to date?" }                    
                };                        

            SetFocusControls(null, null, specificColumnHeaders);
        }

        public override object GetData()
        {            
            return GetReportData("USP_RPT_INSTALLEDVERSION", new Dictionary<string, object>(), true);
        }

        public override void DataBoundCompleted()
        {
            GridColumn gcLastKnownVC = ResultGridView.Columns.ColumnByFieldName("LASTVERSIONCHECK");
            if (gcLastKnownVC != null)
            {
                gcLastKnownVC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gcLastKnownVC.DisplayFormat.FormatString = "dd-MM-yyyy hh:mm:ss tt";
            }
        }
    }
}
