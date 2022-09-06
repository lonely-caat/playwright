using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class SocialMediaData : ObjectGraphType<Zip.CustomerProfile.Contracts.SocialMediaData>
    {
        public SocialMediaData()
        {
            Field(d => d.FacebookDataAccesses, true).Description("Facebook data");
            Field(d => d.PaypalDataAccesses, true).Description("Paypal data");
        }
    }
}