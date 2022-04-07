using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class AddressType : EnumerationGraphType
    {
        public AddressType()
        {
            AddValue("Business", "Business", 0);
            AddValue("Postal", "Postal", 1);
            AddValue("Residential", "Residential", 2);
            AddValue("Temporary", "Temporary", 3);
        }
    }
}