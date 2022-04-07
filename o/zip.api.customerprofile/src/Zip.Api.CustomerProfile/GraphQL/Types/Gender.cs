using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class Gender : EnumerationGraphType
    {
        public Gender()
        {
            AddValue("Male", "Male", 0);
            AddValue("Female", "Female", 1);
            AddValue("Other", "Other", 2);
        }
    }
}