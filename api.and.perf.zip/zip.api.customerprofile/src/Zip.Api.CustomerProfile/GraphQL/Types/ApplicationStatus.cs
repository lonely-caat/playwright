using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class ApplicationStatus : EnumerationGraphType
    {
        public ApplicationStatus()
        {
            AddValue("ApplicationInProgress", "ApplicationInProgress", 0);
            AddValue("ApplicationCompleted", "ApplicationCompleted", 1);
            AddValue("Refer1", "Refer1", 2);
            AddValue("Refer2", "Refer2", 3);
            AddValue("Refer3", "Refer3", 4);
            AddValue("Approved", "Approved", 5);
            AddValue("Active", "Active", 6);
            AddValue("Declined", "Declined", 7);
            AddValue("InActive", "InActive", 8);
            AddValue("Expired", "Expired", 9);
            AddValue("Registered", "Registered", 10);
            AddValue("Cancelled", "Cancelled", 11);
            AddValue("Verify", "Verify", 12);
        }
    }
}