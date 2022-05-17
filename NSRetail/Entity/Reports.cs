using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Reports
    {
        
    }
    public class DealerIndent:EntityBase
    {
        public object SupplierIndentID { get; set; }
        public object supplierID { get; set; }
        public object CategoryID { get; set; }
        public object FromDate { get; set; }
        public object ToDate { get; set; }
        public bool IsApproved { get; set; }
        public DataTable dtSupplierIndent { get; set; }
    }
}
