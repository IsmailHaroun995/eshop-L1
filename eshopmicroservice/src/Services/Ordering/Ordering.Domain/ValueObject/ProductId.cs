using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public  record ProductId
    {
        public Guid Value { get; }

        private ProductId(Guid vaule) => Value = vaule;
        public static ProductId Of(Guid vaule)
        {
            //ArgumentException.ThrowIfNullOrEmpty(vaule);
            if (vaule == Guid.Empty)
            {
                throw new DomainException("ProductId connot be empty");
            }
            return new ProductId(vaule);

        }

    }
}
