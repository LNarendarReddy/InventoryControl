using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using NSRetail.ReportForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class SearchCriteriaBase : XtraUserControl
    {
        LookUpEdit cmbPeriodicity;
        Control firstControl;
        Control lastControl;
        DateEdit fromdate;
        DateEdit todate;
        Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>();

        public List<IncludeSettings> IncludeSettingsCollection { get; protected set; }

        public LookUpEdit Periodicity => cmbPeriodicity;

        protected GridView ResultGridView => (ParentForm as frmReportPlaceHolder)?.ResultsGridView;

        protected GridControl ResultGrid => (ParentForm as frmReportPlaceHolder)?.ResultsGrid;

        public bool ShowIncludeSetting => IncludeSettingsCollection != null && IncludeSettingsCollection.Any();

        public SearchCriteriaBase()
        {
            InitializeComponent();
            GenericColumnHeaders = new Dictionary<string, string>()
            {
                // (for performance management) Add only common column names here
                // for specific column names, override SpecificColumnHeaders property in child class and specify the values there
                {"BRANCHNAME", "Branch" }
                , {"ITEMNAME", "Item Name"}
                , {"SKUCODE", "SKU Code"}
                , {"ITEMCODE", "Item Code"}
                , {"CATEGORYNAME", "Category"}
                , {"SUBCATEGORYNAME", "Sub Category"}
                , {"SNO", "SNo"}
                , {"COUNTERNAME", "Counter"}
                , {"BILLNUMBER", "Bill Number"}
                , {"SALEPRICE", "Sale Price"}
                , {"QUANTITY", "Quantity"}
                , {"WEIGHTINKGS", "Weight In Kgs"}
                , {"BILLEDAMOUNT", "Billed Amount"}
                , {"CREATEDBY", "User Name"}
                , {"CREATEDDATE", "Created Date"}
                , {"TIME", "Time"}
                , {"CLASSIFICATIONNAME", "Classification"}
                , {"SUBCLASSIFICATIONNAME", "Sub Classification"}
                , {"BRANCHCODE", "Branch Code"}
                , {"COSTPRICEWOT", "Cost Price w/o Tax"}
                , {"COSTPRICETAX", "Cost Price Tax"}
                , {"COSTPRICEWT", "Cost Price with Tax"}
                , {"TOTALCOSTPRICEWOT", "Total Cost Price w/o Tax"}
                , {"TOTALCOSTPRICEWT", "Total Cost Price with Tax"}
            };

            ButtonColumns = new List<string>();
        }

        public virtual DataTable GetData() => throw new NotImplementedException();

        public Dictionary<string, string> SpecificColumnHeaders => specificColumnHeaders;

        public virtual IEnumerable<string> ForceShowColumns { get; }

        public Dictionary<string, string> GenericColumnHeaders { get; }

        public DataTable GetReportData(string procName, Dictionary<string, object> parameters, bool useCloudConn = false)
        {
            DataTable reportdata = null;
            try
            {
                if (ShowIncludeSetting)
                {
                    IncludeSettingsCollection.ForEach(x => parameters[x.ParameterName] = x.Included);
                }

                reportdata = new ReportRepository().GetReportData(procName, parameters, useCloudConn);

                if (!ShowIncludeSetting || reportdata == null)
                {
                    return reportdata;
                }

                List<string> columnsToRemove = IncludeSettingsCollection.Where(x => !x.Included).SelectMany(x => x.RelatedColumns)
                                                    .Distinct().Where(reportdata.Columns.Contains).ToList();
                columnsToRemove.ForEach(x => reportdata.Columns.Remove(x));
                return reportdata;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return reportdata;
        }

        public IEnumerable<BaseEdit> MandatoryFields { get; protected set; }

        public IEnumerable<string> EditableColumns { get; protected set; }

        public IEnumerable<string> HiddenColumns { get; protected set; }

        public IEnumerable<string> ButtonColumns { get; protected set; }

        public Control FirstControl => firstControl;

        public Control LastControl => lastControl;

        public virtual void ActionExecute(string buttonText, DataRow drFocusedRow) { }

        protected void SetPeriodicty(LookUpEdit cmb,DateEdit _fromdate, DateEdit _todate, bool includeHourly = false)
        {
            fromdate= _fromdate;
            todate= _todate;
            cmbPeriodicity = cmb;
            DataTable dtPeriodicity = new DataTable();
            dtPeriodicity.Columns.Add("Periodicityvalue", typeof(string));

            if(includeHourly) dtPeriodicity.Rows.Add(new []{ "Hourly" });
            dtPeriodicity.Rows.Add(new []{ "Daily" });
            dtPeriodicity.Rows.Add(new []{ "Monthly"});
            dtPeriodicity.Rows.Add(new []{ "Yearly"});

            cmbPeriodicity.Properties.DataSource = dtPeriodicity;
            cmbPeriodicity.Properties.ValueMember = "Periodicityvalue";
            cmbPeriodicity.Properties.DisplayMember = "Periodicityvalue";
            cmbPeriodicity.EditValueChanged += cmbPeriodicity_EditValueChanged;
            cmbPeriodicity.EditValue = "Daily";
        }

        private void cmbPeriodicity_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUpEdit = sender as LookUpEdit;
            if (lookUpEdit.EditValue.Equals("Monthly"))
                SetDateFormats("MMMM");
            else if (lookUpEdit.EditValue.Equals("Yearly"))
                SetDateFormats("yyyy");
            else
                SetDateFormats("d");
        }

        protected void SetDateFormats(string formatstring)
        {
            fromdate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            fromdate.Properties.DisplayFormat.FormatString = formatstring;
            fromdate.Properties.EditMask = formatstring;

            todate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            todate.Properties.DisplayFormat.FormatString = formatstring;
            todate.Properties.EditMask = formatstring;
            
            fromdate.Properties.VistaCalendarViewStyle =
                todate.Properties.VistaCalendarViewStyle =
                formatstring =="MMMM" ? DevExpress.XtraEditors.VistaCalendarViewStyle.YearView : 
                formatstring == "yyyy" ? VistaCalendarViewStyle.YearsGroupView : 
                VistaCalendarViewStyle.Default;
        }

        public virtual bool ValidateMandatoryFields()
        {
            var layoutControlOfSearch = Controls.OfType<LayoutControl>().First();
            var missingValues = MandatoryFields?.Where(x => x.EditValue == null)
                                    .Select(x => $"{Environment.NewLine}\t* " + layoutControlOfSearch.GetItemByControl(x).Text);

            if (missingValues != null && missingValues.Any())
            {
                XtraMessageBox.Show("Please select the values for : " + Environment.NewLine + string.Join(string.Empty, missingValues)
                    , "Mandatoy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MandatoryFields.First(x => x.EditValue == null).Focus();
                return false;
            }

            return true;
        }

        protected void SetFocusControls(Control first, Control last, Dictionary<string, string> columnHeaders)
        {
            firstControl = first;
            lastControl = last;
            specificColumnHeaders = columnHeaders;
        }

        public virtual void DataBoundCompleted() { }
    }
}
