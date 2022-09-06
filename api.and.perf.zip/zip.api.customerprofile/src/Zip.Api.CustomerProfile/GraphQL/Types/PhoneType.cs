using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class PhoneType : EnumerationGraphType
    {
        public PhoneType()
        {
            AddValue("Mobile", "Mobile", 0);
            AddValue("Home", "Home", 1);
            AddValue("Work", "Work", 2);
            AddValue("Other", "Other", 3);
        }
    }
}