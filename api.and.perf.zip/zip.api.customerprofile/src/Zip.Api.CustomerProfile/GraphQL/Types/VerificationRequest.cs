using GraphQL.Types;
using Zip.Api.CustomerProfile.Extensions;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class VerificationRequest : ObjectGraphType<Zip.CustomerProfile.Contracts.VerificationRequest>
    {
        public VerificationRequest()
        {
            Field(d => d.VerificationRequestType, true, typeof(VerificationRequestType));
            Field(d => d.ValueJson, true);
            Field(d => d.Verified, true);
            Field(d => d.Active, true);
            Field(d => d.Urls, true, typeof(ListGraphType<StringGraphType>));
            Field(d => d.VerificationResult, true, typeof(VerificationResult));
            Field("CreatedTimestamp", d => d.CreatedTimestamp.ToUniversalTime(), true);
            Field("LastUpdatedTimestamp", d => d.LastUpdatedTimestamp.ToUniversalTime(), true);
        }
    }
}