using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class AccountStatus : EnumerationGraphType
    {
        public AccountStatus()
        {
            AddValue("Pending", "Pending", 0);
            AddValue("Active", "Active", 1);
            AddValue("Operational", "Operational", 2);
            AddValue("Dormant", "Dormant", 3);
            AddValue("Closed", "Closed", 4);
            AddValue("ChargedOff", "ChargedOff", 5);
            AddValue("ContractAccepted", "ContractAccepted", 6);
            AddValue("Locked", "Locked", 7);
        }
    }
}