using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace NSRetail.Reports
{
    public partial class rptBillA4 : XtraReport
    {
        private readonly DataTable dtItems;

        public rptBillA4(DataTable dtItems, DataTable dtMOP)
        {
            InitializeComponent();
            this.dtItems = dtItems;
            drItems.DataSource = dtItems;
            drGST.DataSource = dtItems;
            drMOP.DataSource = dtMOP;
        }

        private void rptBillA4_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            decimal rounding = Convert.ToDecimal(Parameters["RoundingFactor"].Value ?? 0);
            decimal total = rounding;
            if (dtItems != null)
            {
                foreach (DataRow row in dtItems.Rows)
                {
                    if (row["BILLEDAMOUNT"] != DBNull.Value)
                        total += Convert.ToDecimal(row["BILLEDAMOUNT"]);
                }
            }

            Parameters["AmountInWords"].Value = $"Rupees {NumberToWords((long)Math.Round(total, 0, MidpointRounding.AwayFromZero))} Only";
        }

        private static string NumberToWords(long number)
        {
            if (number == 0) return "Zero";

            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string ConvertBelowThousand(long value)
            {
                string words = "";
                if (value >= 100)
                {
                    words += units[value / 100] + " Hundred";
                    value %= 100;
                    if (value > 0) words += " ";
                }

                if (value >= 20)
                {
                    words += tens[value / 10];
                    value %= 10;
                    if (value > 0) words += " " + units[value];
                }
                else if (value > 0)
                {
                    words += units[value];
                }

                return words;
            }

            string result = "";
            long crores = number / 10000000;
            number %= 10000000;
            long lakhs = number / 100000;
            number %= 100000;
            long thousands = number / 1000;
            number %= 1000;

            if (crores > 0) result += ConvertBelowThousand(crores) + " Crore ";
            if (lakhs > 0) result += ConvertBelowThousand(lakhs) + " Lakh ";
            if (thousands > 0) result += ConvertBelowThousand(thousands) + " Thousand ";
            if (number > 0) result += ConvertBelowThousand(number);

            return result.Trim();
        }
    }
}
