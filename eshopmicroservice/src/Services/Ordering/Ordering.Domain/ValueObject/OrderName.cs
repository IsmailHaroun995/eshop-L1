using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public record OrderName
    {
        private const int DefaultLenght = 5;
        public string Value { get;  }


        private OrderName(string vaule) => Value = vaule;

        public static OrderName Of(string vaule)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(vaule);
            ArgumentOutOfRangeException.ThrowIfNotEqual(vaule.Length,DefaultLenght);

            return new OrderName(vaule);
        }
    }
}
