using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public interface IPaymentGateway { }

    public class PineLabsPaymentGateway : IPaymentGateway
    {
        public object ClientID { get; set; }
        public object StoreID { get; set; }

    }

    public class BharathPePaymentGateway : IPaymentGateway
    {
        public object UserName { get; set; }
        public object Password { get; set; }
    }
}
