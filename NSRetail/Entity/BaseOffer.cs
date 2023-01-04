using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BaseOffer : EntityBase
    {
        public object BASEOFFERID { get; set; }
        public object OFFERCODE { get; set; }
        public object OFFERNAME { get; set; }
        public object CATEGORYNAME { get; set; }
        public object CATEGORYID { get; set; }
        public object STARTDATE { get; set; }
        public object ENDDATE { get; set; }
        public DataTable dtBranches { get; set; }

    }
}
