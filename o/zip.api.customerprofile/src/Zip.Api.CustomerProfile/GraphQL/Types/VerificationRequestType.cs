using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class VerificationRequestType : EnumerationGraphType
    {
        public VerificationRequestType()
        {
            AddValue("Medicare", "Medicare", 0);
            AddValue("DrivingLicense", "DrivingLicense", 1);
        }
    }
}