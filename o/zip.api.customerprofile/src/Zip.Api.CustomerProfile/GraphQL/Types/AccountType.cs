using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class AccountType : EnumerationGraphType
    {
        public AccountType()
        {
            AddValue("Company", "Company", 0);
            AddValue("Person", "Person", 1);
        }
    }
}