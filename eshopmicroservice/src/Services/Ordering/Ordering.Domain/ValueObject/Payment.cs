using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public record Payment
    {
        public string? CardName { get; } = default!;
        public string? CardNumber { get; } = default!;
        public string? Expireation { get; } = default!;
        public string? CVV { get; } = default!;
        public string? PaymentMethod { get; } = default!;

        protected Payment()
        {
                
        }
        private Payment(string cardname,string cardnumber, string expireation , string cvv, string paymentmethod){
            CardName = cardname;
            CardNumber = cardnumber;
            Expireation = expireation;
            CVV = cvv;
            PaymentMethod = paymentmethod;
        
        }
        public static Payment Of(string cardname, string cardnumber, string expireation, string cvv, string paymentmethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardnumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardname);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
            return 


        }
    }
}
