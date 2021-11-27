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
        public object PHONENO { get; set; }
        public object EMAILID { get; set; }
        public object ISWAREHOUSE { get; set; }
    }

    public class Category : EntityBase
    {
        public object CATEGORYID { get; set; }
        public object CATEGORYNAME { get; set; }

        public object AllowOpenItems { get; set; }
    }

    public class User:EntityBase
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
    }

   public class Dealer:EntityBase
    {
        public object DEALERID { get; set; }
        public object DEALERNAME { get; set; }
        public object ADDRESS { get; set; }
        public object PHONENO { get; set; }
        public object GSTIN { get; set; }
        public object EMAILID { get; set; }
    }

    public class Counter:EntityBase
    {
        public object COUNTERID { get; set; }
        public object COUNTERNAME { get; set; }
        public object BRANCHID { get; set; }
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
}
