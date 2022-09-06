using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;

namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class Consumer : IEqualityComparer<Consumer>
    {
        public long ConsumerId { get; set; }

        public Guid CustomerId { get; set; }

        public long ApplicationId { get; set; }

        public Consumer LinkedConsumer { get; set; }

        public AccountInfo LinkedAccount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public string CountryId { get; set; }

        public Country Country { get; set; }

        public long? OriginationMerchantId { get; set; }

        public long? ReferrerId { get; set; }

        public bool IsExclusiveAccount { get; set; }
        
        public Merchant ReferredBy { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public IEnumerable<Document> Documents { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public DateTime? ActivationDate { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as Consumer);
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public bool Equals(Consumer x, Consumer y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.FirstName == y.FirstName &&
                    x.LastName == y.LastName &&
                    x.CountryId == y.CountryId &&
                    x.Gender == y.Gender &&
                    x.Address == y.Address &&
                    x.DateOfBirth == y.DateOfBirth;
        }

        public int GetHashCode(Consumer obj)
        {
            unchecked
            {
                var hash = 13;
                hash = (hash * 7) + obj.FirstName.GetHashCode();
                hash = (hash * 7) + obj.LastName.GetHashCode();
                hash = (hash * 7) + obj.CountryId.GetHashCode();
                hash = (hash * 7) + obj.Gender.GetHashCode();
                hash = (hash * 7) + obj.DateOfBirth.GetHashCode();
                hash = (hash * 7) + obj.Address.GetHashCode();

                return hash;
            }
        }
    }
}