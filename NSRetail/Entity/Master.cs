using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Master
    {}
    public class Branch
    {
        public object BRANCHID { get; set; }
        public object BRANCHNAME { get; set; }
        public object BRANCHCODE { get; set; }
        public object DESCRIPTION { get; set; }
        public object ADDRESS { get; set; }
        public object PHONENO { get; set; }
        public object EMAILID { get; set; }
        public object USERID { get; set; }
        public bool IsSave { get; set; }
    }
    public class Category
    {
        public object CATEGORYID { get; set; }
        public object CATEGORYNAME { get; set; }
        public object USERID { get; set; }
        public bool IsSave { get; set; }
    }
    public class User
    {
        public object BRANCHID { get; set; }
        public object BRANCHNAME { get; set; }
        public object BRANCHCODE { get; set; }
        public object DESCRIPTION { get; set; }
        public object ADDRESS { get; set; }
        public object PHONENO { get; set; }
        public object EMAILID { get; set; }
        public object USERID { get; set; }
        public bool IsSave { get; set; }
        public DataTable dtBranch { get; set; }
    }
}
