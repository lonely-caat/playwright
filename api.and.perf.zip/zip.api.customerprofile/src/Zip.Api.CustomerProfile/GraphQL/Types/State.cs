using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class State : EnumerationGraphType
    {
        public State()
        {
            AddValue("NSW", "NSW", 0);
            AddValue("QLD", "QLD", 1);
            AddValue("SA", "SA", 2);
            AddValue("TAS", "TAS", 3);
            AddValue("VIC", "VIC", 4);
            AddValue("WA", "WA", 5);
            AddValue("ACT", "ACT", 6);
            AddValue("NT", "NT", 7);
        }
    }
}