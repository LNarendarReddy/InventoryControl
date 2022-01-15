using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Offer : EntityBase
    {
        public object OfferID { get; set; }
        public object OfferName { get; set; }
        public object OfferCode { get; set; }
        public object StartDate { get; set; }
        public object EndDate { get; set; }
        public object DiscountFlat { get; set; }
        public object DiscountPer { get; set; }
        public object AppliesToID { get; set; }
        public object OfferTypeID { get; set; }
        public object OfferTypeCode { get; set; }
        public object OfferTypeName { get; set; }
        public object CategoryID { get; set; }
        public object CategoryName { get; set; }
        public object ItemGroupID { get; set; }
        public object GroupName { get; set; }
        public object IsActive { get; set; }
    }
}

