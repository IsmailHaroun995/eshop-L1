
namespace Ordering.Domain.ValueObject
{
    public record OrderId
    {
        public Guid Vaule  { get; }

        private OrderId(Guid vaule) => Vaule = vaule;
        public static OrderId Of(Guid vaule)
        {
            //ArgumentException.ThrowIfNull(vaule);
            if (vaule == Guid.Empty)
            {
                throw new DomainException("OrderId connot ne empty");
            }
            return new OrderId(vaule);

        }
    }
}
