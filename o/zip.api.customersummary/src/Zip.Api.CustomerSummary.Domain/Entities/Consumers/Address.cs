using System;
using System.Text;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;

namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public sealed class Address : IEquatable<Address>
    {
        public long Id { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }

        public string UnitNumber { get; set; }

        public string CountryCode { get; set; }
        
        public Country Country { get; set; }
        
        public string FullAddress => ComposeFullAddress();

        private string ComposeFullAddress()
        {
            var fa = new StringBuilder();
            if (!string.IsNullOrEmpty(this.UnitNumber))
            {
                fa.Append($"{this.UnitNumber} ");
            }

            fa.Append($"{this.StreetNumber} {this.StreetName} {this.Suburb} {this.State} {this.PostCode}");

            return fa.ToString();
        }

        public override string ToString()
        {
            return $"{UnitNumber} {StreetNumber} {StreetName}, {Suburb} {State} {PostCode}";
        }

        public bool Equals(Address other)
        {
            if (other == null)
            {
                return false;
            }

            return Suburb == other.Suburb &&
                State == other.State &&
                PostCode == other.PostCode &&
                StreetNumber == other.StreetNumber &&
                StreetName == other.StreetName &&
                UnitNumber == other.UnitNumber &&
                CountryCode == other.CountryCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Address);
        }

        public static bool operator ==(Address address1, Address address2)
        {
            if (address1 is null)
            {
                return address2 is null;
            }

            return address1.Equals(address2);
        }

        public static bool operator !=(Address address1, Address address2)
        {
            return !(address1 == address2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 13;
                hash = (hash * 7) + Suburb.GetHashCode();
                hash = (hash * 7) + State.GetHashCode();
                hash = (hash * 7) + PostCode.GetHashCode();
                hash = (hash * 7) + StreetNumber.GetHashCode();
                hash = (hash * 7) + StreetName.GetHashCode();
                hash = (hash * 7) + UnitNumber.GetHashCode();
                hash = (hash * 7) + CountryCode.GetHashCode();

                return hash;
            }
        }
    }
}
