using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class Medicare : ObjectGraphType<Zip.CustomerProfile.Contracts.Medicare>
    {
        public Medicare()
        {
            Field("Id", d => d.Id, true).Description("Driver Licence Number");
            Field(d => d.IndividualReferenceNumber, true).Description("Individual Reference Number");
            Field(d => d.FullName, true).Description("Printed name on medicare card");
            Field(d => d.ExpiryDate, true).Description("Expiry Date");
            Field(d => d.MiddleName, true).Description("Middle Name");
        }
    }
}