using System;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private const string ConsumerSearchGraphQlQuery = "query($key: String!) { customerProfile(id: $key){ FirstName: givenName LastName: familyName DateOfBirth: dateOfBirth applications { applicationId } driverLicence { id state } Gender: gender Address: residentialAddress { suburb state postcode streetNumber streetName unitNumber countryCode } } }";

        private readonly IGraphQLClient _client;
        private readonly ICountryContext _countryContext;

        public CustomerProfileService(IGraphQLClient client, ICountryContext countryContext)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _countryContext = countryContext ?? throw new ArgumentNullException(nameof(countryContext));
        }

        public async Task<ConsumerPersonalInfo> GetConsumerPersonalInfo(string searchKey)
        {
            try
            {
                var query = new GraphQLRequest
                {
                     Query = ConsumerSearchGraphQlQuery,
                     Variables = new { key = searchKey }
                };

                var response = await _client.SendQueryAsync(query);

                if (response == null)
                {
                    Log.Information("{class} :: {action} : {message}",
                                    nameof(CustomerProfileService),
                                    nameof(GetConsumerPersonalInfo),
                                    $"Unable to find customer with SearchKey:{searchKey} from CustomerProfileApi");

                    return null;
                }

                var customerArray = JsonConvert.DeserializeObject<JArray>(response.Data.customerProfile.ToString());
                var personalInfo = JsonConvert.DeserializeObject<ConsumerPersonalInfo>(customerArray[0].ToString());

                if (personalInfo.Address != null && !string.IsNullOrEmpty(personalInfo.Address.CountryCode))
                {
                    personalInfo.Address.Country = await _countryContext.GetAsync(personalInfo.Address.CountryCode);
                }

                Log.Information("{class} :: {action} : {message}",
                                nameof(CustomerProfileService),
                                nameof(GetConsumerPersonalInfo),
                                $"Successfully retrieved customer with SearchKey:{searchKey} from CustomerProfileApi");

                return personalInfo;

            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{Service} :: {action} : {message}",
                          nameof(CustomerProfileService),
                          nameof(GetConsumerPersonalInfo),
                          ex.Message);
                throw;
            }
        }
    }
}