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
            MyPrinter.Print($"                                    <b>Victory Bazars PVT LTD</b>                                         \r\n");
            MyPrinter.Print("                                 Dispatch Delivery challan list                                    \r\n");
            MyPrinter.Print("************************************************************************************************\r\n");
            MyPrinter.Print($" Way bill no : <b>{ds.Tables[0].Rows[0]["DISPATCHDCNUMBER"]}</b>");
            MyPrinter.Print($"  Date : {DateTime.Parse(ds.Tables[0].Rows[0]["CREATEDDATE"].ToString()).ToString("dd/MM/yyyy hh:mm:ss tt")}\r\n\r\n");
            MyPrinter.Print(" GSTIN : 37AAICV7240C1ZC          FSSAI : 10114004000548           CIN : U51390AP2022PTC121579\r\n\r\n");
            MyPrinter.Print($" Shipping From : {ds.Tables[0].Rows[0]["FROMBRANCHNAME"]}, {ds.Tables[0].Rows[0]["FROMADDRESS"]}, {ds.Tables[0].Rows[0]["FROMSTATENAME"]}, Phone #: {ds.Tables[0].Rows[0]["FROMPHONENO"]} \r\n\r\n");
            MyPrinter.Print($" Shipping To : <b>{ds.Tables[0].Rows[0]["TOBRANCHNAME"]}</b>, {ds.Tables[0].Rows[0]["TOADDRESS"]}, {ds.Tables[0].Rows[0]["TOSTATENAME"]}, Phone # : {ds.Tables[0].Rows[0]["TOPHONENO"]}\r\n");
            MyPrinter.Print("------------------------------------------------------------------------------------------------\r\n\r\n");
            MyPrinter.Print($"<b>SNo  ItemCode     ItemName                          HSNCode      Quantity   Weight      GST Code</b>\r\n");
            MyPrinter.Print("------------------------------------------------------------------------------------------------\r\n");
            DataTable dtItems = ds.Tables[1];
            for (int i = 0; i < dtItems.Rows.Count; i++)
            {
                MyPrinter.Print(FormatString(Convert.ToString(i + 1), 4)
                    + FormatString(dtItems.Rows[i], "ITEMCODE", 14)
                    + FormatString(dtItems.Rows[i], "ITEMNAME", 34)
                    + FormatString(dtItems.Rows[i], "HSNCODE", 12)
                    + FormatString(dtItems.Rows[i], "DISPATCHQUANTITY", 5, true)
                    + FormatString(dtItems.Rows[i], "WEIGHTINKGS", 11, true)
                    + FormatString(dtItems.Rows[i], "GSTCODE", 15, true)
                    + "\r\n");
            }
            MyPrinter.Print("------------------------------------------------------------------------------------------------\r\n");
            MyPrinter.Print($"{FormatString($"<b>Tax break-up</b>    ", 96, true)}\r\n");

            foreach(DataRow drTaxSlab in ds.Tables[2].Rows)
            {
                MyPrinter.Print($"{FormatString($"{drTaxSlab["GSTCODE"]} :: {drTaxSlab["TAXAMOUNT"]}", 96, true)}\r\n");
            }
            MyPrinter.Print("************************************************************************************************");
            MyPrinter.Print("\x0C");
            MyPrinter.Close();
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
