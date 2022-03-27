using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{    
    public class Item : EntityBase
    {        
        public object ItemCodeID { get; set; }

        public object ItemID { get; set; }

        public object ItemCode { get; set; }

        public object ItemName { get; set; }

        public object SKUCode { get; set; }

        public object HSNCode { get; set; }

        public object IsEAN { get; set; }

        public object CostPriceWT { get; set; }

        public object CostPriceWOT { get; set; }

        public object SalePrice { get; set; }

        public object MRP { get; set; }

        public object GSTID { get; set; }

        public object CategoryID { get; set; }

        public object CategoryName { get; set; }

        public object SubCategoryID { get; set; }
        public object SubCategoryName { get; set; }

        public object IsOpenItem { get; set; }

        public object ParentItemID { get; set; }

        public object UOMID { get; set; }

        public object FreeItemCodeID { get; set; }

    }

    public class ItemNewCode : EntityBase
    {

        public object Code { get; set; }

        public bool isSKUorItem { get; set; }
    }
}

