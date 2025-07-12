using System;
using System.Data;

namespace Entity
{
    public abstract class EntityBase 
    {
        public bool IsSave { get; set; }
        public object Description { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedBDate { get; set; }
        public object UpdatedBy { get; set; }
        public object UpdatedDate { get; set; }
        public object UserID { get; set; }

        public object IsNewToggleSwitched { get; set; }

    }

    public class Branch : EntityBase
    {
        public object BRANCHID { get; set; }
        public object BRANCHNAME { get; set; }
        public object BRANCHCODE { get; set; }
        public object ADDRESS { get; set; }
        public object STATEID { get; set; }
        public object PHONENO { get; set; }
        public object LANDLINE { get; set; }
        public object EMAILID { get; set; }
        public object ISWAREHOUSE { get; set; }
        public object SUPERVISERID { get; set; }

        public object ENABLEDRAFTBILLS { get; set; }
    }

    public class Category : EntityBase
    {
        public object CATEGORYID { get; set; }
        public object CATEGORYNAME { get; set; }
        public object AllowOpenItems { get; set; }
    }
    public class SubCategory : EntityBase
    {
        public object SUBCATEGORYID { get; set; }
        public object SUBCATEGORYNAME { get; set; }
        public object CATEGORYID { get; set; }
    }

    public class User : EntityBase
    {
        public object USERID { get; set; }
        public object ROLEID { get; set; }
        public object REPORTINGLEADID { get; set; }
        public object CATEGORYID { get; set; }
        public object BRANCHID { get; set; }
        public object USERNAME { get; set; }
        public object PASSWORDSTRING { get; set; }
        public object FULLNAME { get; set; }
        public object CNUMBER { get; set; }
        public object EMAIL { get; set; }
        public object ISOTP { get; set; }
        public object GENDER { get; set; }
        public object DOB { get; set; }
        public object CUSERID { get; set; }
        public object SUBCATEGORYID { get; set; }
    }

   public class Dealer:EntityBase
    {
        public object DEALERID { get; set; }
        public object DEALERNAME { get; set; }
        public object ADDRESS { get; set; }
        public object PHONENO { get; set; }
        public object GSTIN { get; set; }
        public object PANNUMBER { get; set; }
        public object EMAILID { get; set; }
        public object STATEID { get;set; }
        public object VendorCode { get; set; }
    }

    public class Counter:EntityBase
    {
        public object COUNTERID { get; set; }
        public object COUNTERNAME { get; set; }
        public object BRANCHID { get; set; }
        public object ISMOBILECOUNTER {  get; set; }
        public object StoreID { get; set; }
        public object ClientID { get; set; }
    }
    public class MOP : EntityBase
    {
        public object MOPID { get; set; }
        public object MOPNAME { get; set; }
    }

    public class UOM : EntityBase
    {
        public object UOMID { get; set; }
        public object DISPLAYVALUE { get; set; }
        public object BASEUOMID { get; set; }
        public object MULTIPLIER { get; set; }
    }
    public class GST : EntityBase
    {
        public object GSTID { get; set; }
        public object GSTCODE { get; set; }
        public object CGST { get; set; }
        public object SGST { get; set; }
        public object IGST { get; set; }
        public object CESS { get; set; }
    }
    public class PrinterSettings : EntityBase
    {
        public object PRINTERSETTINGSID { get; set; }
        public object PRINTERTYPEID { get; set; }
        public object PRINTERNAME { get; set; }
    }

    public class GSTInfo
    {
        public int GSTID { get; set; }
        public string GSTCODE { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CESS { get; set; }
        public decimal TAXPercent { get; private set; }

        public void UpdateGST(DataRow dr)
        {
            GSTID = Convert.ToInt32(dr["GSTID"]);
            GSTCODE = Convert.ToString(dr["GSTCODE"]);
            CGST = Convert.ToDecimal(dr["CGST"]);
            SGST = Convert.ToDecimal(dr["SGST"]);
            IGST = Convert.ToDecimal(dr["IGST"]);
            CESS = Convert.ToDecimal(dr["CESS"]);
            TAXPercent = Math.Round((CGST + SGST + CESS) / 100, 3);

        }
        public void UpdateGST(DataRowView dr)
        {
            UpdateGST(dr.Row);
        }
    }

    public class ItemClassification : EntityBase
    {
        public object ItemClassificationID { get; set; }

        public object CategoryID { get; set; }

        public object ItemClassificationName { get; set; }

        public object CategoryName { get; set; }
    }

    public class ItemSubClassification : EntityBase
    {
        public object ItemSubClassificationID { get; set; }

        public object ItemClassificationID { get; set; }

        public object ItemSubClassificationName { get; set; }
    }
}
