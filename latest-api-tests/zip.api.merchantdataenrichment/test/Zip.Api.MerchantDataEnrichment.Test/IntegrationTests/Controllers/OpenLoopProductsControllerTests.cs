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
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.Products.Command.CreateOpenLoopProduct;
using Zip.MerchantDataEnrichment.Application.Products.Query.GetOpenLoopProducts;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests.Controllers
{
    public class OpenLoopProductsControllerTests : CommonTestsFixture, IClassFixture<ApiFactory<TestStartup>>
    {
        private const string BaseUrl = "/api/v1/OpenLoopProduct";

        private readonly HttpClient _client;

        public OpenLoopProductsControllerTests(ApiFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_Data_GetOpenLoopProductsAsync_Should_Work_Correctly()
        {
            var paginationSetting = Fixture.Build<PaginationSetting>()
                                           .With(x => x.Page, 1)
                                           .With(x => x.PageSize, 10)
                                           .Create();

            var request = Fixture.Build<GetOpenLoopProductsQuery>()
                                 .With(x => x.PaginationSetting, paginationSetting)
                                 .Create();

            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            var uri = new Uri($"{BaseUrl}/list-products", UriKind.RelativeOrAbsolute);
            using var response = await _client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<GetOpenLoopProductsResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.OpenLoopProducts.Should().NotBeNull();
            actual.OpenLoopProducts.TotalPageCount.Should().BeGreaterOrEqualTo(0);
            actual.OpenLoopProducts.PageSize.Should().Be(10);
        }

        [Fact]
        public async Task Given_Valid_Request_CreateOpenLoopProductAsync_Should_Work_Correctly()
        {
            var request = Fixture.Build<CreateOpenLoopProductCommand>()
                                .With(x => x.Description, Constants.OpenLoopProductDescription)
                                .With(x => x.ServiceFeePercentage, 1.2M)
                                .With(x => x.TransactionAmountLowerLimit, 0.0M)
                                .With(x => x.TransactionAmountUpperLimit, 1000.00M)
                                .With(x => x.TransactionFee, 0.16M)
                                .Create();

            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            var uri = new Uri(BaseUrl, UriKind.RelativeOrAbsolute);
            using var response = await _client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<CreateOpenLoopProductResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.OpenLoopProductId.Should().NotBeEmpty();
            actual.OpenLoopProductId.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task When_An_Active_OpenLoopProduct_Exists_with_Same_Profile_It_Should_Not_Be_Duplicated()
        {
            // Arrange
            var request = Fixture.Build<CreateOpenLoopProductCommand>()
                                 .With(x => x.IsActive, true)
                                 .With(x => x.ServiceFeePercentage, 0.05m)
                                 .With(x => x.TransactionAmountLowerLimit, 0m)
                                 .With(x => x.TransactionAmountUpperLimit, decimal.MaxValue)
                                 .With(x => x.TransactionFee, 0.16m)
                                 .Create();

            // Act
            var actual = await CreateOpenLoopProductAsync(request);

            // Assert
            actual.OpenLoopProductId.Should().Be(new Guid(Constants.OpenLoopProductId1));
        }

        private async Task<CreateOpenLoopProductResponse> CreateOpenLoopProductAsync(CreateOpenLoopProductCommand request)
        {
            var uri = new Uri(BaseUrl, UriKind.RelativeOrAbsolute);

            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            using var response = await _client.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<CreateOpenLoopProductResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}
