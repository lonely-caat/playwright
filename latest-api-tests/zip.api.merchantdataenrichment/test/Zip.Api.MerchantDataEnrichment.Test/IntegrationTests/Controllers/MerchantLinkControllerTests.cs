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
using Zip.MerchantDataEnrichment.Application.MerchantLinks.Command.CreateMerchantLink;
using Zip.MerchantDataEnrichment.Application.MerchantLinks.Command.UpdateMerchantLink;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests.Controllers
{
    public class MerchantLinkControllerTests : CommonTestsFixture, IClassFixture<ApiFactory<TestStartup>>
    {
        private const string BaseUrl = "/api/v1/merchantlink";
        private readonly HttpClient _client;

        public MerchantLinkControllerTests(ApiFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_Valid_Request_CreateMerchantLinkAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantLinkCommand>()
                                 .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId3))
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<CreateMerchantLinkResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.MerchantLink.Should().NotBeNull();
            actual.MerchantLink.MerchantDetailId.Should().Be(new Guid(Constants.MerchantDetailId3));
        }

        [Fact]
        public async Task Given_MerchantDetail_Not_Exists_CreateMerchantLinkAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantLinkCommand>()
                                 .With(x => x.MerchantDetailId, Guid.NewGuid)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Given_Unique_Constraint_Violated_CreateMerchantLinkAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantLinkCommand>()
                                 .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                 .With(x => x.ZipMerchantId, 1)
                                 .With(x => x.ZipBranchId, 1)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Given_Valid_Request_UpdateMerchantLinkAsync_Should_Work_Correctly()
        {
            // Arrange
            var merchantAccountHash = Guid.NewGuid().ToString();
            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .With(x => x.MerchantLinkId, new Guid(Constants.MerchantLinkId))
                                 .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                 .With(x => x.ZipMerchantId, long.MaxValue)
                                 .With(x => x.ZipBranchId, long.MaxValue)
                                 .With(x => x.ZipCompanyId, long.MaxValue)
                                 .With(x => x.MerchantAccountHash, merchantAccountHash)
                                 .With(x => x.Active, false)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PatchAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<UpdateMerchantLinkResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.MerchantLink.Should().NotBeNull();
            actual.MerchantLink.Id.Should().Be(new Guid(Constants.MerchantLinkId));
            actual.MerchantLink.MerchantDetailId.Should().Be(new Guid(Constants.MerchantDetailId));
            actual.MerchantLink.ZipMerchantId.Should().Be(long.MaxValue);
            actual.MerchantLink.ZipBranchId.Should().Be(long.MaxValue);
            actual.MerchantLink.ZipCompanyId.Should().Be(long.MaxValue);
            actual.MerchantLink.MerchantAccountHash.Should().Be(merchantAccountHash);
            actual.MerchantLink.Active.Should().BeFalse();
        }

        [Fact]
        public async Task Given_MerchantLink_Not_Exists_UpdateMerchantLinkAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .With(x => x.MerchantLinkId, Guid.NewGuid)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PatchAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Given_Unique_Constraint_Violated_UpdateMerchantLinkAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .With(x => x.MerchantLinkId, Guid.NewGuid)
                                 .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                 .With(x => x.ZipMerchantId, 1)
                                 .With(x => x.ZipBranchId, 1)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PatchAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}