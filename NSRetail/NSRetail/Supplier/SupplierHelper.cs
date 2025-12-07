using DataAccess;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using System;
using System.Data;

namespace NSRetail.Supplier
{
    public class SupplierHelper
    {
        public static void ShowSupplierDebitNote(object supplierReturnsId)
        {
            DataSet ds = new SupplierRepository().GetDebitNote(supplierReturnsId);

            if (ds.Tables.Count < 2 || ds.Tables[0].Rows.Count == 0)
                throw new Exception("Invalid data returned for Supplier Debit Note.");

            rptSupplierDebitNote rpt = new rptSupplierDebitNote();
            DataRow hdr = ds.Tables[0].Rows[0];
            rpt.Parameters["DCDate"].Value = hdr["DCDate"];

            DateTime dcDate = Convert.ToDateTime(hdr["DCDate"]);
            string fy = GetFinancialYear(dcDate);
            string dcNoFormatted = $"DC No.FPR-{hdr["SUPPLIERRETURNSID"]}/{fy}";
            rpt.Parameters["DCNo"].Value = dcNoFormatted;

            rpt.Parameters["WHGSTIN"].Value = hdr["WHGSTIN"];
            rpt.Parameters["WHAddress"].Value = hdr["WHAddress"];
            rpt.Parameters["SupplierAddress"].Value = hdr["SupplierAddress"];
            rpt.Parameters["SupplierGSTIN"].Value = hdr["SupplierGSTIN"];
            rpt.Parameters["UserName"].Value = hdr["USERNAME"];

            rpt.DataSource = ds.Tables[1];
            rpt. ShowRibbonPreview();
        }

        private static string GetFinancialYear(DateTime date)
        {
            int year = date.Year;
            int nextYear = year + 1;
            int prevYear = year - 1;

            if (date.Month <= 3)
                return $"{prevYear % 100:D2}-{year % 100:D2}";
            else
                return $"{year % 100:D2}-{nextYear % 100:D2}";
        }


    }
}
