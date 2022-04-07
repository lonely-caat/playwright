using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class DriverLicence : ObjectGraphType<Zip.CustomerProfile.Contracts.DriverLicence>
    {
        public DriverLicence()
        {
            Field("Id", d => d.Id, true).Description("Driver Licence Number");
            Field(d => d.State, true, typeof(State)).Description("Issuing State");
        }
    }
}