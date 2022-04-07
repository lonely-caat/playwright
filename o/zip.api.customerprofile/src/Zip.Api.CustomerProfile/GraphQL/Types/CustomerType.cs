using GraphQL.Types;
using Zip.Api.CustomerProfile.Extensions;
using Zip.CustomerProfile.Contracts;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Field("Id", d => d.Id.ToString()).Description("The id of the customer.");
            Field(d => d.GivenName, true).Description("The name of the customer.");
            Field(d => d.FamilyName, true);
            Field(d => d.MiddleName, true);
            Field(d => d.OtherName, true);
            Field(d => d.Gender, true, typeof(Gender));
            Field(d => d.Title, true);
            Field(d => d.DeviceIds, true);
            Field(d => d.VedaReferenceNumber, true);
            Field(d => d.Email, true).Description("The email of the customer.");
            Field(d => d.DateOfBirth, true).Description("date of birth");
            Field(d => d.SocialMediaData, true, typeof(SocialMediaData)).Description("Social media data");
            Field(d => d.DriverLicence, true, typeof(DriverLicence)).Description("Driver Licence");
            Field(d => d.Medicare, true, typeof(Medicare)).Description("Medicare");
            Field(d => d.Applications, true, typeof(ListGraphType<Application>)).Description("Linked applications");
            Field(d => d.Accounts, true, typeof(ListGraphType<Account>)).Description("Linked accounts");
            Field(d => d.ResidentialAddress, true, typeof(Address)).Description("Residential address of customer");
            Field(d => d.MobilePhone, true, typeof(Phone)).Description("Mobile number of customer");
            Field(d => d.VerificationRequests, true, typeof(ListGraphType<VerificationRequest>));
            Field("CreatedTimestamp", d => d.CreatedTimestamp.ToUniversalTime(), true);
            Field("LastUpdatedTimestamp", d => d.LastUpdatedTimestamp.ToUniversalTime(), true);
        }
    }
}