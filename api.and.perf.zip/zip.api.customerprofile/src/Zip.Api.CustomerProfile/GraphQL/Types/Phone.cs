using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class Phone : ObjectGraphType<Zip.CustomerProfile.Contracts.Phone>
    {
        public Phone()
        {
            Field(d => d.Active, true);
            Field(d => d.AreaCode, true);
            Field(d => d.CountryCode, true);
            Field(d => d.CreatedTimestamp, true);
            Field(d => d.LastUpdatedTimestamp, true);
            Field(d => d.IsPrimary, true);
            Field(d => d.PhoneNumber, true);
            Field(d => d.PhoneType, true, typeof(PhoneType));
        }
    }
}