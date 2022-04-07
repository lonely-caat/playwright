using GraphQL.Types;
using Zip.Api.CustomerProfile.Extensions;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class Address : ObjectGraphType<Zip.CustomerProfile.Contracts.Address>
    {
        public Address()
        {
            Field(d => d.AddressType, true, typeof(AddressType));
            Field(d => d.City, true);
            Field(d => d.CountryCode, true);
            Field("CreatedTimestamp", d => d.CreatedTimestamp.ToUniversalTime(), true);
            Field("LastUpdatedTimestamp", d => d.LastUpdatedTimestamp.ToUniversalTime(), true);
            Field(d => d.LevelNumber, true);
            Field(d => d.LevelType, true);
            Field(d => d.PostboxNumber, true);
            Field(d => d.PostboxType, true);
            Field(d => d.Postcode, true);
            Field(d => d.State, true);
            Field(d => d.StreetName, true);
            Field(d => d.StreetNumber, true);
            Field(d => d.StreetNumber2, true);
            Field(d => d.StreetSuffix, true);
            Field(d => d.StreetType, true);
            Field(d => d.Suburb, true);
            Field(d => d.UnitNumber, true);
            Field(d => d.UnitType, true);
            Field(d => d.Latitude, true);
            Field(d => d.Longitude, true);
            Field(d => d.GnafSa1, true);
            Field(d => d.ComparableAddress, true);
        }
    }
}