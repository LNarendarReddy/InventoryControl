using DataAccess;
using DevExpress.PivotGrid.PivotTable;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Entity;
using NSRetail.ReportForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
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
        IEnumerable<string> allowedRoles;
        List<int> excludedBranches = new List<int> { 91, 92, 97, 100, 103 };

        private frmReportPlaceHolder frmReportPlaceHolder => ParentForm as frmReportPlaceHolder;

        public List<IncludeSettings> IncludeSettingsCollection { get; protected set; }

        public bool IsDataSet { get; protected set; }

        public LookUpEdit Periodicity => cmbPeriodicity;

        protected GridView ResultGridView => frmReportPlaceHolder?.ResultsGridView;

        protected GridControl ResultGrid => frmReportPlaceHolder?.ResultsGrid;

        public bool ShowIncludeSetting => IncludeSettingsCollection != null && IncludeSettingsCollection.Any();

        public bool HasAccess { get; private set; }

        protected IEnumerable<string> AllowedRoles 
        {
            get => allowedRoles;
            set
            {
                allowedRoles = value;
                HasAccess = Utility.Role == "Admin" || Utility.Role == "IT Manager" || (AllowedRoles != null && AllowedRoles.Contains(Utility.Role));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            foreach(Control cntrl in this.Controls)
            {
                if(cntrl.GetType() == typeof(LayoutControl))
                {
                    foreach(Control child in cntrl.Controls)
                    {
                        if (child.GetType() == typeof(CheckedComboBoxEdit))
                        {
                            if (child.Name.ToLower().Contains("branch"))
                                BindBranch(child);
                            else if(child.Name.ToLower().Contains("category"))
                                BindCategoryCheckedComboboxEdit(child);
                        }
                        else if (child.GetType() == typeof(SearchLookUpEdit)
                            && child.Name.ToLower().Contains("itemcode"))
                        {
                            BindItemCode(child);
                        }
                        else if (child.GetType() == typeof(LookUpEdit)
                            && child.Name.ToLower().Contains("category"))
                        {
                            BindCategoryLookupedit(child);
                        }
                    }
                    break;
                }
            }
            base.OnLoad(e);
        }

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
                , {"ISOPENITEM", "Is open item"}
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
                , {"TOTALCOSTPRICEtax", "Total Cost Price Tax"}
                , {"TOTALCOSTPRICEWT", "Total Cost Price with Tax"}
                , {"DEALERNAME", "Supplier"}
                , {"AVGCOSTPRICEWOT", "Avg Cost Price w/o Tax"}
                , {"AVGCOSTPRICETAX", "Avg Cost Price Tax"}
                , {"AVGCOSTPRICEWT", "Avg Cost Price with Tax"}
                , {"QTYORWGHTINKGS", "Qty or Weight in KG(s)"}
                , {"PARENTITEMNAME", "Parent Item Name"}
                , {"PARENTSKUCODE", "Parent SKU Code"}
            };

            ButtonColumns = new List<string>();
            IsDataSet = false;
            AllowedRoles = null;
        }

        public virtual object GetData() => throw new NotImplementedException();

        public Dictionary<string, string> SpecificColumnHeaders => specificColumnHeaders;

        public virtual IEnumerable<string> ForceShowColumns { get; }

        public Dictionary<string, string> GenericColumnHeaders { get; }

        public object GetReportData(string procName, Dictionary<string, object> parameters, bool useCloudConn = false)
        {
            object reportData = null;
            try
            {
                if (ShowIncludeSetting)
                {
                    IncludeSettingsCollection.ForEach(x => parameters[x.ParameterName] = x.Included);
                }

                ReportRepository reportRepository = new ReportRepository();
                reportData = IsDataSet ? 
                    (object)reportRepository.GetReportDataset(procName, parameters) 
                    : reportRepository.GetReportData(procName, parameters, useCloudConn);

                if (!ShowIncludeSetting || reportData == null)
                {
                    return reportData;
                }

                List<DataTable> dataTables = IsDataSet ?
                    ((DataSet)reportData).Tables.Cast<DataTable>().ToList()
                    : new List<DataTable> { (DataTable)reportData };

                foreach (DataTable dtTable in dataTables)
                {
                    List<string> columnsToRemove = IncludeSettingsCollection.Where(x => !x.Included).SelectMany(x => x.RelatedColumns)
                                                        .Distinct().Where(dtTable.Columns.Contains).ToList();
                    columnsToRemove.ForEach(x => dtTable.Columns.Remove(x));
                }

                return reportData;
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }

            return reportData;
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
            fromdate = _fromdate;
            todate = _todate;
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

            //fromdate.EditValueChanged += date_EditValueChanged;
            //todate.EditValueChanged += date_EditValueChanged;
        }

        private void date_EditValueChanged(object sender, EventArgs e)
        {
            cmbPeriodicity_EditValueChanged(cmbPeriodicity, e);
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
                formatstring == "MMMM" ? VistaCalendarViewStyle.YearView :
                formatstring == "yyyy" ? VistaCalendarViewStyle.YearsGroupView :
                VistaCalendarViewStyle.Default;

            fromdate.EditValueChanged -= date_EditValueChanged;
            todate.EditValueChanged -= date_EditValueChanged;

            if (formatstring == "MMMM")
            {
                if (fromdate.EditValue != null)
                {
                    DateTime fromDateValue = (DateTime)fromdate.EditValue;
                    fromdate.EditValue = fromDateValue.AddDays(-fromDateValue.Day + 1);
                }

                if (todate.EditValue != null)
                {
                    DateTime toDateValue = (DateTime)todate.EditValue;
                    todate.EditValue = toDateValue.AddMonths(1).AddDays(-toDateValue.Day);
                }
            }
            else if(formatstring == "yyyy")
            {
                if (fromdate.EditValue != null)
                {
                    DateTime fromDateValue = (DateTime)fromdate.EditValue;
                    fromdate.EditValue = DateTime.Parse($"{fromDateValue.Year}-01-01");
                }

                if (todate.EditValue != null)
                {
                    DateTime toDateValue = (DateTime)todate.EditValue;
                    todate.EditValue = DateTime.Parse($"{toDateValue.Year}-12-31");                    
                }
            }

            fromdate.EditValueChanged += date_EditValueChanged;
            todate.EditValueChanged += date_EditValueChanged;
        }

        public virtual bool ValidateMandatoryFields()
        {
            var layoutControlOfSearch = Controls.OfType<LayoutControl>().FirstOrDefault();
            if (layoutControlOfSearch == null) return true;

            var missingValues = MandatoryFields?.Where(x => string.IsNullOrEmpty(x.EditValue?.ToString()))
                                    .Select(x => $"{Environment.NewLine}\t* " + layoutControlOfSearch.GetItemByControl(x).Text);

            if (missingValues != null && missingValues.Any())
            {
                XtraMessageBox.Show("Please select the values for : " + Environment.NewLine + string.Join(string.Empty, missingValues)
                    , "Mandatoy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MandatoryFields.First(x => string.IsNullOrEmpty(x.EditValue?.ToString())).Focus();
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
        
        public void BindBranch(Control cntrl)
        {            
            MasterRepository masterRepo = new MasterRepository();
            CheckedComboBoxEdit cmb = (CheckedComboBoxEdit)cntrl;
            cmb.Properties.DataSource = Utility.GetBranchList(true);
            cmb.Properties.ValueMember = "BRANCHID";
            cmb.Properties.DisplayMember = "BRANCHNAME";
            cmb.CheckAll();
            List<int> branchIDs = cmb.EditValue.ToString().Split(',').Select(x => int.Parse(x)).ToList();
            excludedBranches.Where(x => branchIDs.Contains(x)).ToList().ForEach(x => branchIDs.Remove(x));
            cmb.EditValue = string.Join(",", branchIDs);
            AddCheckedComboBoxEnter(cmb);
            cmb.EnterMoveNextControl = true;
        }

        public void BindItemCode(Control cntrl)
        {
            SearchLookUpEdit cmb = (SearchLookUpEdit)cntrl;
            cmb.Properties.DataSource = Utility.GetItemCodeListFiltered();
            cmb.Properties.ValueMember = "ITEMCODEID";
            cmb.Properties.DisplayMember = "ITEMCODE";
            cmb.EnterMoveNextControl = true;
        }

        public void BindCategoryLookupedit(Control cntrl)
        {
            LookUpEdit cmb = (LookUpEdit)cntrl;
            cmb.Properties.DataSource = Utility.GetCategoryList();
            cmb.Properties.ValueMember = "CATEGORYID";
            cmb.Properties.DisplayMember = "CATEGORYNAME";
            cmb.EditValue = 13;
            cmb.EnterMoveNextControl = true;
        }

        public void BindCategoryCheckedComboboxEdit(Control cntrl)
        {
            CheckedComboBoxEdit cmb = (CheckedComboBoxEdit)cntrl;
            DataView dvCategory = Utility.GetCategoryList().Copy().DefaultView;
            dvCategory.RowFilter = "CATEGORYID <> 13";
            cmb.Properties.DataSource = dvCategory;
            cmb.Properties.ValueMember = "CATEGORYID";
            cmb.Properties.DisplayMember = "CATEGORYNAME";
            cmb.CheckAll();
            AddCheckedComboBoxEnter(cmb);
            cmb.EnterMoveNextControl = true;
        }

        public virtual void DataBoundCompleted() { }

        public void AddCheckedComboBoxEnter(CheckedComboBoxEdit cmb)
        {
            if (cmb == null) return;

            cmb.Enter += cmbBranch_Enter;
        }

        public void RemoveCheckedComboBoxEnter(CheckedComboBoxEdit cmb)
        {
            if (cmb == null) return;

            cmb.Enter -= cmbBranch_Enter;
        }

        public void ExpandAllMasterRows()
        {
            frmReportPlaceHolder?.ExpandAllMasterRows();
        }

        private void cmbBranch_Enter(object sender, EventArgs e)
        {
            try
            {
                CheckedComboBoxEdit cmb = sender as CheckedComboBoxEdit;
                BeginInvoke(new Action(() => {
                    cmb.ShowPopup();
                }));
            }
            catch (Exception ex){}
        }
    }
}
