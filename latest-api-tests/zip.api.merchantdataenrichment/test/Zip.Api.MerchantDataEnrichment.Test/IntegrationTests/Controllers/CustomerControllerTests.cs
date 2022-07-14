using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.Api.MerchantDataEnrichment.Test.Factories;
using Zip.MerchantDataEnrichment.Application.Customer.Command.UpsertCustomerDetail;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests.Controllers
{
    public class CustomerControllerTests : CommonTestsFixture, IClassFixture<ApiFactory<TestStartup>>
    {
        private const string BaseUrl = "/api/v1/customer/create-customer-detail";
        private readonly HttpClient _client;

        public CustomerControllerTests(ApiFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_CustomerDetail_Exists_UpsertCustomerDetailAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<UpsertCustomerDetailCommand>()
                                 .With(x => x.AccountId, Constants.CustomerDetailAccountId)
                                 .With(x => x.CustomerId, Constants.CustomerDetailCustomerId)
                                 .With(x => x.VcnTransactionId, new Guid(Constants.VcnTransactionId))
                                 .With(x => x.CountryId, "AU")
                                 .With(x => x.CountryCode, "AU")
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri(BaseUrl, UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<UpsertCustomerDetailResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.CustomerDetail.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Given_CustomerDetail_Not_Exist_UpsertCustomerDetailAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<UpsertCustomerDetailCommand>()
                                 .With(x => x.CustomerId, Guid.NewGuid().ToString())
                                 .With(x => x.VcnTransactionId, new Guid(Constants.VcnTransactionId))
                                 .With(x => x.CountryId, "AU")
                                 .With(x => x.CountryCode, "AU")
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri(BaseUrl, UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<UpsertCustomerDetailResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.CustomerDetail.Id.Should().NotBeEmpty();
        }
    }
}
