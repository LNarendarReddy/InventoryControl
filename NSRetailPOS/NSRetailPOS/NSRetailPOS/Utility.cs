using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS
{
    public static class Utility
    {
        public static Bill GetBill(DataSet dsBillDetails)
        {
            Bill billObj = new Bill();
            billObj.BillID = dsBillDetails.Tables["BILL"].Rows[0]["BILLID"];
            billObj.BillNumber = dsBillDetails.Tables["BILL"].Rows[0]["BILLNUMBER"];
            billObj.LastBilledAmount = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDAMOUNT"];
            billObj.LastBilledQuantity = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDQUANTITY"];
            billObj.MRP = dsBillDetails.Tables["BILL"].Rows[0]["MRP"];
            billObj.SalePrice = dsBillDetails.Tables["BILL"].Rows[0]["SALEPRICE"];
            billObj.Savings = dsBillDetails.Tables["BILL"].Rows[0]["SAVINGS"];
            billObj.CGST = dsBillDetails.Tables["BILL"].Rows[0]["CGST"];
            billObj.SGST = dsBillDetails.Tables["BILL"].Rows[0]["SGST"];
            billObj.IGST = dsBillDetails.Tables["BILL"].Rows[0]["IGST"];
            billObj.CESS = dsBillDetails.Tables["BILL"].Rows[0]["CESS"];
            billObj.GSTSUM = dsBillDetails.Tables["BILL"].Rows[0]["GSTSUM"];

            billObj.dtBillDetails = dsBillDetails.Tables["BILLDETAILS"];
            return billObj;
        }
    }
}
