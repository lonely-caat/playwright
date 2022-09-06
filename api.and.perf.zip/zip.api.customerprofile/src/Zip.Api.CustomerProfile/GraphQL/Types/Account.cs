using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class Account : ObjectGraphType<Zip.CustomerProfile.Contracts.Account>
    {
        public Account()
        {
            Field("AccountId", d => d.AccountId).Description("Account ID");
#pragma warning disable CS0618
            Field(d => d.AccountType, true, typeof(AccountType))
                .Description("Account Type. Obsoleted. Please use Account Type V2");
#pragma warning restore CS0618
            Field(d => d.AccountTypeV2, true, typeof(AccountTypeV2)).Description("Account Type V2");
            Field(d => d.AccountStatus, true, typeof(AccountStatus)).Description("Account Status");
            Field(d => d.ProductCode, true, typeof(ProductClassification)).Description("Product Code");
            Field("CreatedTimestamp", d => d.CreatedTimestamp.ToUniversalTime(), true).Description("Created Timestamp");
        }
    }
}