using System.Data;

namespace Entity
{
    public abstract class Master 
    {
        public bool IsSave { get; set; }

        public object Description { get; set; }

        public object CreatedBy { get; set; }

        public object CreatedBDate { get; set; }

        public object UpdatedBy { get; set; }

        public object UpdatedDate { get; set; }

        public object UserID { get; set; }

    }

    public class Branch : Master
    {
        public object BRANCHID { get; set; }
        public object BRANCHNAME { get; set; }
        public object BRANCHCODE { get; set; }
        public object ADDRESS { get; set; }
        public object PHONENO { get; set; }
        public object EMAILID { get; set; }
    }

    public class Category : Master
    {
        public object CATEGORYID { get; set; }
        public object CATEGORYNAME { get; set; }
    }

    public class User : Master
    {
        public object BRANCHID { get; set; }
        public object BRANCHNAME { get; set; }
        public object BRANCHCODE { get; set; }
        public object ADDRESS { get; set; }
        public object PHONENO { get; set; }
        public object EMAILID { get; set; }
        public DataTable dtBranch { get; set; }
    }

    public class Item : Master
    {
        public object ItemID { get; set; }
        public object ItemName { get; set; }
        public object ItemCode { get; set; }
        public object HSCNO { get; set; }
    }
}
