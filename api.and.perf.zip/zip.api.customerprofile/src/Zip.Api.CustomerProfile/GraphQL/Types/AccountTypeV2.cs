using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class AccountTypeV2 : ObjectGraphType<Zip.CustomerProfile.Contracts.AccountTypeV2>
    {
        public AccountTypeV2()
        {
            Field(d => d.Id, true).Description("Account Type Id");
            Field(d => d.Description, true).Description("Account Type Description");
        }
    }
}