using GraphQL.Types;
using Zip.Api.CustomerProfile.Extensions;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class Application : ObjectGraphType<Zip.CustomerProfile.Contracts.Application>
    {
        public Application()
        {
            Field("ApplicationId", d => d.ApplicationId).Description("Application ID");
            Field(d => d.ProductCode, true, typeof(ProductClassification)).Description("Product Code");
            Field(d => d.Status, true, typeof(ApplicationStatus)).Description("Application Status");
            Field("CreatedTimestamp", d => d.CreatedTimestamp.ToUniversalTime(), true).Description("Created Timestamp");
        }
    }
}