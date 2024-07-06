using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public  record OrderItemID
    {
        public Guid Vaule { get; }

        private OrderItemID(Guid vaule) => Vaule = vaule;
        public static  OrderItemID Of(Guid vaule)
        {
            //ArgumentException.ThrowIfNullOrEmpty(vaule);
            if(vaule == Guid.Empty)
            {
                throw new DomainException("OrderItemId connot be empty");
            }
            return new OrderItemID(vaule);

        }
    }
}
