using GraphQL.Types;
using Zip.Api.CustomerProfile.Extensions;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class VerificationResult : ObjectGraphType<Zip.CustomerProfile.Contracts.VerificationResult>
    {
        public VerificationResult()
        {
            Field(d => d.Status, true);
            Field(d => d.VerifiedSource, true);
            Field(d => d.VerifiedBy, true);
            Field(d => d.Active, true);
            Field("CreatedTimestamp", d => d.CreatedTimestamp.ToUniversalTime(), true);
            Field("LastUpdatedTimestamp", d => d.LastUpdatedTimestamp.ToUniversalTime(), true);
        }
    }
}