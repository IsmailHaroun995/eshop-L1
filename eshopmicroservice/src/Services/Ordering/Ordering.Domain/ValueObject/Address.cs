

namespace Ordering.Domain.ValueObject
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string EmailAddres { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipZode { get; } = default!;
        protected Address()
        {
                
        }
        private Address(string firstName, string lastName , string emailaddres , string addressline ,
            string country , string state , string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddres = emailaddres;
            AddressLine = addressline;
            Country = country;
            State = state;
            ZipZode = zipcode;

        }
        public static Address Of(string firstName, string lastName, string emailaddres, string addressline,
            string country, string state, string zipcode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailaddres);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressline);
            return new Address(firstName,lastName,emailaddres,addressline,country, state, zipcode);
        }
    }
}
