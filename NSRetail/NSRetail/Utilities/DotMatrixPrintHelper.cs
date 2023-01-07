using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Configuration;
using System.Data;

namespace NSRetail.Utilities
{
    public static class DotMatrixPrintHelper
    {
        static string DotMatrixPrinterName = Convert.ToString(ConfigurationManager.AppSettings["DotMatrixPrinterName"]);

        public static void PrintDC(DataSet ds)
        {
            DotMatrixPrinter MyPrinter = new DotMatrixPrinter();
            if (!MyPrinter.OpenPrinter(DotMatrixPrinterName)) return;
            MyPrinter.Print("<b>Victory Bazars PVT LTD</b>", 1, Alignment.Center);
            MyPrinter.Print("Dispatch Delivery challan list", 1, Alignment.Center);
            MyPrinter.Print("************************************************************************************************", 1);
            MyPrinter.Print($" Way bill no : <b>{ds.Tables[0].Rows[0]["DISPATCHDCNUMBER"]}</b>");
            MyPrinter.Print($"  Date : {DateTime.Parse(ds.Tables[0].Rows[0]["CREATEDDATE"].ToString()).ToString("dd/MM/yyyy hh:mm:ss tt")}", 2);
            MyPrinter.Print(" GSTIN : 37AAICV7240C1ZC          FSSAI : 10114004000548           CIN : U51390AP2022PTC121579", 2);
            MyPrinter.Print($" Shipping From : {ds.Tables[0].Rows[0]["FROMBRANCHNAME"]}, {ds.Tables[0].Rows[0]["FROMADDRESS"]}, {ds.Tables[0].Rows[0]["FROMSTATENAME"]}, Phone #: {ds.Tables[0].Rows[0]["FROMPHONENO"]}", 2);
            MyPrinter.Print($" Shipping To : <b>{ds.Tables[0].Rows[0]["TOBRANCHNAME"]}</b>, {ds.Tables[0].Rows[0]["TOADDRESS"]}, {ds.Tables[0].Rows[0]["TOSTATENAME"]}, Phone # : {ds.Tables[0].Rows[0]["TOPHONENO"]}", 1);
            MyPrinter.Print("------------------------------------------------------------------------------------------------", 2);
            MyPrinter.Print($"<b>SNo  ItemCode     ItemName                          HSNCode      Quantity   Weight      GST Code</b>", 1);
            MyPrinter.Print("------------------------------------------------------------------------------------------------", 1);
            DataView dvItems = ds.Tables[1].DefaultView;
            foreach (DataRow drTaxSlab in ds.Tables[2].Rows)
            {
                MyPrinter.Print("", 1);
                dvItems.RowFilter = $"GSTCODE = '{drTaxSlab["GSTCODE"]}'";
                MyPrinter.Print($"<b> Tax : {drTaxSlab["GSTCODE"]} :: CGST : {dvItems[0]["CGST"]} SGST : {dvItems[0]["SGST"]} CESS : {dvItems[0]["CESS"]} </b>", 1, Alignment.Center);

                foreach (DataRowView drvItem in dvItems)
                {
                    MyPrinter.Print(FormatString(drvItem.Row, "Sno", 4)
                        + FormatString(drvItem.Row, "ITEMCODE", 14)
                        + FormatString(drvItem.Row, "ITEMNAME", 34)
                        + FormatString(drvItem.Row, "HSNCODE", 12)
                        + FormatString(drvItem.Row, "DISPATCHQUANTITY", 5, true)
                        + FormatString(drvItem.Row, "WEIGHTINKGS", 11, true)
                        + FormatString(drvItem.Row, "GSTCODE", 15, true)
                        , 1);
                }
            }
            
            MyPrinter.Print("------------------------------------------------------------------------------------------------", 2);
            MyPrinter.Print($"<b>Tax break-up</b>", 1, Alignment.Far);

            foreach(DataRow drTaxSlab in ds.Tables[2].Rows)
            {
                MyPrinter.Print($"{drTaxSlab["GSTCODE"]} :: {drTaxSlab["TAXAMOUNT"]}", 1, Alignment.Far);
            }
            MyPrinter.Print("************************************************************************************************");
            MyPrinter.Print("\x0C");
            MyPrinter.Close();
        }

        public static void PrintDispatch(DataSet ds)
        {
            DotMatrixPrinter MyPrinter = new DotMatrixPrinter();
            if (!MyPrinter.OpenPrinter(DotMatrixPrinterName)) return;
            MyPrinter.Print("<b>Victory Bazars PVT LTD</b>", 1, Alignment.Center);
            MyPrinter.Print("Dispatch details", 1, Alignment.Center);
            MyPrinter.Print("************************************************************************************************", 1);
            MyPrinter.Print($" To Branch : <b>{ds.Tables[0].Rows[0]["TOBRANCHNAME"]}</b>  Dispatch # : <b>{ds.Tables[0].Rows[0]["DISPATCHNUMBER"]}</b> Category : {ds.Tables[0].Rows[0]["CATEGORYNAME"]}", 1);
            MyPrinter.Print($" From Branch : <b>{ds.Tables[0].Rows[0]["FROMBRANCHNAME"]}</b>  Dispatched By {ds.Tables[0].Rows[0]["CREATEDBY"]}");
            MyPrinter.Print($" Dispatched Date : {DateTime.Parse(ds.Tables[0].Rows[0]["CREATEDDATE"].ToString()).ToString("dd/MM/yyyy hh:mm:ss tt")}", 2);
            MyPrinter.Print("------------------------------------------------------------------------------------------------", 2);
            MyPrinter.Print($"<b> EANCode     ItemName                               MRP    SalePrice     Quantity   Weight(KGs) </b>", 1);
            MyPrinter.Print("------------------------------------------------------------------------------------------------", 1);
            DataTable dtTrays = ds.Tables[2];
            foreach(DataRow drTray in dtTrays.Rows)
            {
                MyPrinter.Print("", 1);
                MyPrinter.Print($"     <b> Tray Number : {drTray["TRAYNUMBER"]}               No of Items in tray : {drTray["TRAYCOUNT"]} </b>", 1);
                DataView dvItems = ds.Tables[1].DefaultView;
                dvItems.RowFilter = $"TRAYNUMBER = {drTray["TRAYNUMBER"]}";
                foreach(DataRowView drItem in dvItems)
                {
                    MyPrinter.Print(FormatString(drItem.Row, "ITEMCODE", 14)
                    + FormatString(drItem.Row, "ITEMNAME", 40)
                    + FormatString(drItem.Row, "MRP", 12, true)
                    + FormatString(drItem.Row, "SALEPRICE", 12, true)
                    + FormatString(drItem.Row, "DISPATCHQUANTITY", 5, true)
                    + FormatString(drItem.Row, "WEIGHTINKGS", 11, true)
                    , 1);
                }
            }
           
            MyPrinter.Print("************************************************************************************************");
            MyPrinter.Print("\x0C");
            MyPrinter.Close();
        }

        public static void PrintBranchIndent(object branchName, object Category, object FromDate, object ToDate, DataTable dtItems)
        {
            DotMatrixPrinter MyPrinter = new DotMatrixPrinter();
            if (!MyPrinter.OpenPrinter(DotMatrixPrinterName)) return;
            MyPrinter.Print($"<b>Victory Bazars PVT LTD</b>", 1, Alignment.Center);
            MyPrinter.Print($"Branch Indent : <b>{branchName}</b> Category : {Category} From Date : {FromDate} To Date : {ToDate}", 1, Alignment.Center);
            MyPrinter.Print("------------------------------------------------------------------------------------------------", 2);
            MyPrinter.Print($"<b> SKUCode     ItemName                         Sub Category   Branch Qty   Sale Qty   Indent Qty </b>", 1);
            MyPrinter.Print("------------------------------------------------------------------------------------------------", 1);
            foreach (DataRow drItem in dtItems.Rows)
            {                
                    MyPrinter.Print(FormatString(drItem, "SKUCODE", 9)
                    + FormatString(drItem, "ITEMNAME", 44)
                    + FormatString(drItem, "SUBCATEGORYNAME", 7)
                    + FormatString(drItem, "BRANCHSTOCK", 12, true)
                    + FormatString(drItem, "SALEQUANTITY", 12, true)
                    + FormatString(drItem, "INDENTQUANTITY", 12, true)
                    , 1);
            }

            MyPrinter.Print("************************************************************************************************");
            MyPrinter.Print("\x0C");
            MyPrinter.Close();
        }

        public static DataTable GetDataTableWYSIWYG(GridView xtraGridView)
        {
            DataTable dt = new DataTable();
            foreach (GridColumn column in xtraGridView.VisibleColumns)
            {
                dt.Columns.Add(column.FieldName, column.ColumnType);
            }
            for (int i = 0; i < xtraGridView.DataRowCount; i++)
            {
                DataRow row = dt.NewRow();
                foreach (GridColumn column in xtraGridView.VisibleColumns)
                {
                    row[column.FieldName] = xtraGridView.GetRowCellValue(i, column);
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        private static string FormatString(DataRow dataRow, string columnName, int maxLen, bool isNumeric = false)
        {
            return FormatString(dataRow[columnName].ToString(), maxLen, isNumeric);
        }

        private static string FormatString(string value, int maxLen, bool isNumeric = false)
        {            
            int padLength = maxLen + 1;
            value = isNumeric ? value.PadLeft(padLength) : value.PadRight(padLength);
            value = isNumeric ? value.Substring(value.Length - maxLen, maxLen) : value.Substring(0, maxLen);
            return value;
        }
    }
}
