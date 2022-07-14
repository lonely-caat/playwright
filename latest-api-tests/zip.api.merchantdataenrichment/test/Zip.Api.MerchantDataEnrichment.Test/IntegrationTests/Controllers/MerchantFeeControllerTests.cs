using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.Api.MerchantDataEnrichment.Test.Factories;
using Zip.MerchantDataEnrichment.Application.MerchantFees.Command.CreateFeeReference;
using Zip.MerchantDataEnrichment.Application.MerchantFees.Command.PatchFeeReference;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests.Controllers
{
    public class MerchantFeeControllerTests : CommonTestsFixture, IClassFixture<ApiFactory<TestStartup>>
    {
        private const string BaseUrl = "/api/v1/merchantfee";

        private readonly HttpClient _client;

        public MerchantFeeControllerTests(ApiFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_Valid_Request_CreateFeeReferenceAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateFeeReferenceCommand>()
                                 .With(x => x.WebhookId, Guid.NewGuid().ToString)
                                 .With(x => x.TransactionAmount, "1.23456")
                                 .With(x => x.CardAcceptorName, Constants.CardAcceptorName)
                                 .With(x => x.CardAcceptorCity, Constants.CardAcceptorCity)
                                 .Create();

            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<CreateFeeReferenceResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.Should().NotBeNull();
            actual.MerchantId.Should().Be(1);
            actual.BranchId.Should().Be(1);
            actual.CompanyId.Should().Be(1);
            actual.WebhookId.Should().Be(request.WebhookId);
            actual.MerchantAccountHash.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Given_FeeReference_PatchFeeReferenceAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<PatchFeeReferenceCommand>()
                                 .With(x => x.WebhookId, Constants.WebhookId1)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PatchAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<PatchFeeReferenceResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.FeeReferenceId.Should().Be(Constants.FeeReferenceId1);
        }

        [Fact]
        public async Task Given_FeeReference_Not_Exists_PatchFeeReferenceAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<PatchFeeReferenceCommand>()
                                 .With(x => x.WebhookId, "SOMETHING NOT EXISTS")
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PatchAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}