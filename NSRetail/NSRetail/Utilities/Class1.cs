using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetail.Utilities
{
    public class Class1
    {
        public void Print() 
        {
            //LPrinter MyPrinter = new LPrinter();
            //if (!MyPrinter.Open("TVS MSP 250/Champion/XL Classic")) return;
            //MyPrinter.Print("                              NEWLOOKS HYPER MART\r\n");
            //MyPrinter.Print("                        Stock Add Reort Of Oils Items\r\n");
            //MyPrinter.Print("--------------------------------------------------------------------------------\r\n");
            //MyPrinter.Print("Date              : " + dtpBillDate.Value.ToString("dd/MM/yyyy hh:mm:ss tt") + "\r\n");
            //MyPrinter.Print("Dlealer Name      : " + (txtDealerName.Text).ToString() + "\r\n");
            //MyPrinter.Print("Stock Adding User : " + frmVictoryParent.UserFullName + "\r\n");
            //MyPrinter.Print("Bill Number       : " + txtBillNumber.Text + "\r\n");
            //MyPrinter.Print("--------------------------------------------------------------------------------\r\n");
            //MyPrinter.Print("SNo  ItemName                               MRP     No/Ctns  Pcs/Crtn    NetBill\r\n");
            //MyPrinter.Print("--------------------------------------------------------------------------------\r\n");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    MyPrinter.Print(FormatedSpaceForString(Convert.ToString(i + 1), 6)
            //        + FormatedSpaceForString(dt.Rows[i]["Item_Name"].ToString(), 31).Substring(0, 30)
            //        + FormatedSpaceForNumeric(dt.Rows[i]["Item_MRP"].ToString(), 13).Substring(0, 12)
            //        + FormatedSpaceForNumeric(dt.Rows[i]["No_Of_Crtns"].ToString(), 13).Substring(0, 12)
            //        + FormatedSpaceForNumeric(dt.Rows[i]["Pieces_Per_Crtn"].ToString(), 11).Substring(0, 10)
            //        + FormatedSpaceForNumeric(dt.Rows[i]["Net_Bill"].ToString(), 12).Substring(0, 11)
            //        + "\r\n");
            //}
            //MyPrinter.Print("--------------------------------------------------------------------------------\r\n");
            //MyPrinter.Print("         Vat : 5.00%  = " + FormatedSpaceForString(Convert.ToString(vat5), 12) + "Total Amount Excluding Vat : " + FormatedSpaceForNumeric(Convert.ToString(sum), 12) + "\r\n");
            //MyPrinter.Print("         Vat : 14.50% = " + FormatedSpaceForString(Convert.ToString(vat145), 12) + "Total discount : " + FormatedSpaceForNumeric(Convert.ToString(Discountamount), 12) + "\r\n");
            //MyPrinter.Print("         Total Vat    = " + FormatedSpaceForString(Convert.ToString(totalvat), 12) + "\r\n");
            //MyPrinter.Print("--------------------------------------------------------------------------------\r\n");
            //MyPrinter.Print("         Total Net Bill : " + FormatedSpaceForNumeric((txtThisBillTotal.Text), 12) + "\r\n");
            //MyPrinter.Print("\x0C");
            //MyPrinter.Close();
        }
        public static string FormatedSpaceForString(string val, int fixedLen)

        {
            int len = 0;
            string retVal = string.Empty;
            try
            {
                len = val.Length;
                retVal = val;
                for (int cnt = 0; cnt < fixedLen - len - 1; cnt++)
                {
                    retVal = retVal + " ";
                }
                //for (int cnt = 0; cnt > fixedLen; cnt++)
                //{
                //    retVal = retVal + char(13);
                //}
            }
            catch (Exception)
            {
                throw;
            }
            return retVal;
        }
        public static string FormatedSpaceForNumeric(string val, int fixedLen)
        {
            int len = 0;
            string retVal = string.Empty;
            try
            {
                len = val.Length;
                retVal = val;
                for (int cnt = 0; cnt < fixedLen - len - 1; cnt++)
                {
                    retVal = " " + retVal;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retVal;
        }
    }
}
