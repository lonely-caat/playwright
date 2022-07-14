using System;
using System.Linq;
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
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.Products.Command.CreateMerchantProduct;
using Zip.MerchantDataEnrichment.Application.Products.Command.DeleteMerchantProduct;
using Zip.MerchantDataEnrichment.Application.Products.Query.GetMerchantProducts;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests.Controllers
{
    public class MerchantProductControllerTests : CommonTestsFixture, IClassFixture<ApiFactory<TestStartup>>
    {
        private const string BaseUrl = "/api/v1/MerchantProduct";

        private readonly HttpClient _client;

        public MerchantProductControllerTests(ApiFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_Valid_Request_CreateMerchantOpenLoopProductAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantProductCommand>()
                                 .With(x => x.ZipMerchantId, 1)
                                 .With(x => x.OpenLoopProductId, new Guid(Constants.OpenLoopProductId1))
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<CreateMerchantProductResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.MerchantOpenLoopProduct.Should().NotBeNull();
            actual.MerchantOpenLoopProduct.OpenLoopProduct.Id.Should().Be(request.OpenLoopProductId);

            // Clean up
            var deleteRequest = Fixture.Build<DeleteMerchantProductCommand>()
                                       .With(x => x.MerchantOpenLoopProductId, actual.MerchantOpenLoopProduct.Id)
                                       .Create();
            var deleteContent = new StringContent(deleteRequest.ToJsonString(), Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{BaseUrl}", UriKind.Relative),
                Content = deleteContent
            };
            _ = await _client.SendAsync(httpRequest, CancellationToken);
        }

        [Fact]
        public async Task Given_OpenLoopProduct_Not_Exists_CreateMerchantOpenLoopProductAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantProductCommand>()
                                 .With(x => x.OpenLoopProductId, Guid.NewGuid)
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}", UriKind.Relative), content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Given_No_MerchantOpenLoopProduct_DeleteMerchantOpenLoopProductAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Create<DeleteMerchantProductCommand>();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{BaseUrl}", UriKind.Relative),
                Content = content
            };

            // Act
            using var response = await _client.SendAsync(httpRequest, CancellationToken);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Given_Data_GetMerchantOpenLoopProductsAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<GetMerchantProductsQuery>()
                                 .With(x => x.ZipMerchantId, 1)
                                 .With(x => x.PaginationSetting, new PaginationSetting { PageSize = 2, Page = 1 })
                                 .Create();
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}/list-merchant-products", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<GetMerchantProductsResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.MerchantOpenLoopProducts.Items.Count().Should().Be(1);
            actual.MerchantOpenLoopProducts.TotalPageCount.Should().Be(1);
            actual.MerchantOpenLoopProducts.TotalRowCount.Should().Be(1);
        }
    }
}