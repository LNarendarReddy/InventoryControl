﻿using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Layout;
using NSRetail;
using NSRetail.ReportForms;
using NSRetailPOS.Data;
using NSRetailPOS.ReportControls.ReportBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucSales : SearchCriteriaBase
    {
        public ucSales()
        {
            InitializeComponent();

            cmbCategory.Properties.DataSource = new ItemRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.EnterMoveNextControl = true;

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "TOTALSALEPRICEWOT", "Total Sale Price WOT" },
                { "TOTALSALETAX", "Total Sale Price Tax" },
                { "TOTALSALEPRICEWT", "Total Sale Price WT" },
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALEPRICETAX", "Sale Price Tax" },
                { "SALEQUANTITY", "Sale Quantity" },
                { "ISOFFER", "Item Offer applied" },
                { "WHSTOCK", "Warehouse Stock" },
                { "BRANCHSTOCK", "Branch Stock" },
                { "GSTCODE", "GST Code" },
                { "OFFERCODE", "Offer Code" },
                { "BASEOFFERCODE", "Base Offer Code" },
                { "BILLCREATEDBY", "Bill Created By" },
                { "CREATEDTIME", "Created Time" },
                { "ISDOORDELIVERY", "Is Door Delivery?" },
                { "CUSTOMERNAME", "Customer Name" },
                { "CUSTOMERNUMBER", "Customer #" },
                { "CUSTOMERGST", "Customer GST" },
                { "HSNCODE", "HSN Code" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>{ "PERIODOCITY" },true)
                , new IncludeSettings("Item details", "IncludeItem", new List<string>{ "SKUCODE", "ITEMNAME", "ITEMCODE", "MRP", "SALEPRICE", "SALEPRICEWOT", "SALEPRICETAX", "SALEQUANTITY", "HSNCODE" }, true)
                , new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "BRANCHNAME" })
                , new IncludeSettings("Counter", "IncludeCounter", new List<string>{ "COUNTERNAME" })
                , new IncludeSettings("Category", "IncludeCategory", new List<string>{ "CATEGORYNAME" })
                , new IncludeSettings("SubCategory", "IncludeSubCategory", new List<string>{ "SUBCATEGORYNAME" })
                , new IncludeSettings("Classification", "IncludeClassification", new List<string>{ "CLASSIFICATIONNAME" })
                , new IncludeSettings("Sub Classification", "IncludeSubClassification", new List<string>{ "SUBCLASSIFICATIONNAME" })
                //, new IncludeSettings("Stock & Is offer", "IncludeStock", new List<string>{ "ISOFFER", "WHSTOCK", "BRANCHSTOCK", "OFFERCODE", "BASEOFFERCODE"  })
                , new IncludeSettings("Tax wise", "IncludeTax", new List<string>{ "GSTCODE" })
                //, new IncludeSettings("Bill details", "IncludeBillDetails", new List<string>
                //    { "BILLCREATEDBY", "BILLNUMBER", "CREATEDTIME", "ISDOORDELIVERY", "CUSTOMERNAME", "CUSTOMERNUMBER", "CUSTOMERGST" })
            };

            HiddenColumns = new List<string> { "WHSTOCK", "BRANCHSTOCK", "ISOFFER", "OFFERCODE", "BASEOFFERCODE"
                , "BILLCREATEDBY", "BILLNUMBER", "CREATEDTIME", "ISDOORDELIVERY", "CUSTOMERNAME", "CUSTOMERNUMBER", "CUSTOMERGST" };

            SetFocusControls(cmbPeriodicity, cmbItemCode, specificColumnHeaders);
        }

        private void ucSales_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            SetPeriodicty(cmbPeriodicity, dtpFromDate, dtpToDate, true);
        }
        public override object GetData()
        {
            int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
            DateTime toDateValue = dtpToDate.DateTime;
            if(toDateValue > DateTime.Now.Date)
            {
                toDateValue = DateTime.Now.Date.AddDays(-1);
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", toDateValue }
                , { "Periodicity", cmbPeriodicity.EditValue }
                , { "ITEMID", searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMID")}
                , { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_SALES", parameters);
        }
    }
}
