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
using Zip.MerchantDataEnrichment.Application.Transaction.Command.CreateVcnTransaction;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests.Controllers
{
    public class TransactionControllerTests : CommonTestsFixture, IClassFixture<ApiFactory<TestStartup>>
    {
        private const string BaseUrl = "/api/v1/transaction/create-vcn-transaction";
        private readonly HttpClient _client;

        public TransactionControllerTests(ApiFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_MerchantDetail_Exists_CreateVcnTransactionAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateVcnTransactionCommand>()
                                 .With(x => x.CardAcceptorName, Constants.CardAcceptorName)
                                 .With(x => x.CardAcceptorCity, Constants.CardAcceptorCity)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<CreateVcnTransactionResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.TransactionId.Should().NotBeEmpty();
            actual.TransactionId.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Given_Merchant_Detail_Not_Exist_CreateVcnTransactionAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateVcnTransactionCommand>()
                                 .With(x => x.CardAcceptorName, Constants.CardAcceptorName2)
                                 .With(x => x.CardAcceptorCity, Constants.CardAcceptorCity2)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<CreateVcnTransactionResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.TransactionId.Should().NotBeEmpty();
            actual.TransactionId.Should().NotBe(Guid.Empty);
        }
    }
}
