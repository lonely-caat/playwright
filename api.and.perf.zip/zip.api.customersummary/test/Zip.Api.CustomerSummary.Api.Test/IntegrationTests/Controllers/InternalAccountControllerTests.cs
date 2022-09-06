using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.CustomerSummary.Api.Constants;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Api.Test.Factories;
using Zip.Api.CustomerSummary.Api.Test.Middleware;

namespace Zip.Api.CustomerSummary.Api.Test.IntegrationTests.Controllers
{
    public class InternalAccountControllerTests : CommonTestsFixture
    {
        private const string TestEmail = "test@gmail.com";
        private const string TestReason = "test reason";
        private const string BaseUrl = "/api/internalaccount";

        public InternalAccountControllerTests(ApiFactory factory) : base(factory)
        {
            Client.DefaultRequestHeaders.Add(AuthenticatedTestRequestMiddleware.TestingHeader, AuthenticatedTestRequestMiddleware.TestingHeaderValue);
            Client.DefaultRequestHeaders.Add(CustomClaimTypes.Email, TestEmail);
        }

        [Fact]
        public async Task Given_CloseAccountAsync_ShouldReturn_Success()
        {
            // Act
            var request = JsonConvert.SerializeObject(new
            {
                ConsumerId = 15076,
                AccountId = 294465,
                Reason = TestReason,
                ChangedBy = TestEmail
            });

            using var content = new StringContent(request, Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(new Uri($"{BaseUrl}/lock", UriKind.Relative), content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var actual = await response.Content.ReadAsStringAsync();
            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task Given_CloseAccountAsync_ShouldReturn_Error()
        {
            // Act
            var request = JsonConvert.SerializeObject(new
            {
                ConsumerId = 1,
                AccountId = int.MaxValue,
                Reason = TestReason,
                ChangedBy = TestEmail
            });

            using var content = new StringContent(request, Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(new Uri($"{BaseUrl}/lock", UriKind.Relative), content);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_CloseAccountAsync_ShouldReturn_BadRequest()
        {
            // Act
            var request = JsonConvert.SerializeObject(new
            {
                ConsumerId = -1,
                AccountId = -1,
                Reason = TestReason,
                ChangedBy = TestEmail
            });

            using var content = new StringContent(request, Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(new Uri($"{BaseUrl}/lock", UriKind.Relative), content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}