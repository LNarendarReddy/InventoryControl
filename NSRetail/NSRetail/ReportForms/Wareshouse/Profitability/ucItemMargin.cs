using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.Profitability
{
    public partial class ucItemMargin : SearchCriteriaBase
    {
        public ucItemMargin()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "IPGSTCODE", "Sale Price GST %" },
                { "IPCREATEDBY", "Sale Price Created By" },
                { "IPCREATEDDATE", "Sale Price Created Date" },
                { "IPDELETEDBY", "Sale Price Deleted By" },
                { "IPDELETEDDATE", "Sale Price Deleted Date" },
                { "COSTPRICEWOT", "Cost Price WOT" },
                { "COSTPRICEWT", "Cost Price WT" },
                { "ICPGSTCODE", "Cost Price GST %" },
                { "ICPCREATEDBY", "Cost Price Created By" },
                { "ICPCREATEDDATE", "Cost Price Created Date" },
                { "ICPDELETEDBY", "Cost Price Deleted By" },
                { "ICPDELETEDDATE", "Cost Price Deleted Date" },
                { "MRPMARGINWOT", "MRP Margin WOT" },
                { "MRPMARGINWT", "MRP Margin WT" },
                { "MRPMARGINWOTPER", "MRP Margin % WOT" },
                { "MRPMARGINWTPER", "MRP Margin % WT" },
                { "ACTUALMARGINWOT", "Actual Margin WOT" },
                { "ACTUALMARGINWT", "Actual Margin WT" },
                { "ACTUALMARGINWOTPER", "Actual Margin % WOT" },
                { "ACTUALMARGINWTPER", "Actual Margin % WT" },
                { "ACTUALPRICEWT", "Actual Price WT" },
                { "ACTUALPRICEWOT", "Actual Price WOT" },
                { "MRPWOT", "MRP WOT" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "OFFERNAME", "Applied Offer Name" },
                { "BRANDNAME", "Brand" },
                { "MANUFACTURERNAME", "Manufacturer" }
            };

            SetFocusControls(cmbCategory, cmbItemCode, specificColumnHeaders);
            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Classification", "IncludeClassification", new List<string>() { "CLASSIFICATIONNAME" }),
                new IncludeSettings("Sub-Classification", "IncludeSubClassification", new List<string>() { "SUBCLASSIFICATIONNAME" }),
                new IncludeSettings("Brand", "IncludeBrand", new List<string>{ "BRANDNAME" }), 
                new IncludeSettings("SubManufacturer", "IncludeManufacturer", new List<string>{ "MANUFACTURERNAME" }),
                new IncludeSettings("Include WOT margins", "IncludeMarginWOT", new List<string>{ "MRPMARGINWOT", "MRPMARGINWOTPER", "ACTUALMARGINWOT"
                    , "ACTUALMARGINWOTPER", "ACTUALPRICEWOT", "MRPWOT", "SALEPRICEWOT" }),
                new IncludeSettings("Include deleted prices", "IncludeDeleted", new List<string>{ "IPDELETEDBY", "IPDELETEDDATE" }),
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                { "Branch wise stock", "" },
                { "Delete MRP", "" }
            };
        }

        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CategoryID", cmbCategory.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
            };

            return GetReportData("USP_RPT_ITEM_MARGIN", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            try
            {
                switch (buttonText)
                {
                    case "Branch wise stock":
                        new frmBranchWiseStock(drFocusedRow["ITEMPRICEID"]).ShowDialog();
                        break;
                    case "Delete MRP":
                        if (drFocusedRow.Table.Columns.Contains("IPDELETEDDATE") && drFocusedRow["IPDELETEDDATE"] != null)
                        {
                            XtraMessageBox.Show("Item price already deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        if (XtraMessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                        new ItemCodeRepository().DeleteItemPrice(drFocusedRow["ITEMPRICEID"], Utility.UserID);
                        XtraMessageBox.Show("Item price deleted, please refresh report", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}
