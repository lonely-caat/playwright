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
    public class CrmCommentsControllerTests : CommonTestsFixture
    {
        private const string TestEmail = "test@gmail.com";
        private const string TesDetail = "test detail";
        private const string BaseUrl = "/api/crmcomments";

        public CrmCommentsControllerTests(ApiFactory factory) : base(factory)
        {
            Client.DefaultRequestHeaders.Add(AuthenticatedTestRequestMiddleware.TestingHeader, AuthenticatedTestRequestMiddleware.TestingHeaderValue);
            Client.DefaultRequestHeaders.Add(CustomClaimTypes.Email, TestEmail);
        }

        [Fact]
        public async Task Given_CreateAsync_ShouldReturn_Success()
        {
            // Act
            var request = JsonConvert.SerializeObject(new
            {
                ReferenceId = 15076,
                Category = 0,
                Type = 0,
                Detail = TesDetail,
                CommentBy = TestEmail
            });

            using var content = new StringContent(request, Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var actual = await response.Content.ReadAsStringAsync();
            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task Given_CreateAsync_ShouldReturn_BadRequest()
        {
            // Act
            var request = JsonConvert.SerializeObject(new
            {
                ReferenceId = -1,
                Category = -1,
                Type = -1,
                Detail = TesDetail,
                CommentBy = TestEmail
            });

            using var content = new StringContent(request, Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}