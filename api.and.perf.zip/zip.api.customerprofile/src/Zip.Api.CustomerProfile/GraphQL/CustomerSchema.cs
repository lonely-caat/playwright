using GraphQL;
using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL
{
    public class CustomerSchema : Schema
    {
        public CustomerSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver?.Resolve<CustomerQuery>();
        }
    }
}