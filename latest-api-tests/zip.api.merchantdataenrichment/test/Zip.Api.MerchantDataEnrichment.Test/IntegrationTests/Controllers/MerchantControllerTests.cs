using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.Api.MerchantDataEnrichment.Test.Factories;
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Command.GetOrUpsertMerchantDetail;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.GetMerchantDetails;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.GetMerchantDetails.Models;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.SearchMerchantDetails;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests.Controllers
{
    public class MerchantControllerTests : CommonTestsFixture, IClassFixture<ApiFactory<TestStartup>>
    {
        private const string BaseUrl = "/api/v1/merchant";
        private readonly HttpClient _client;

        public MerchantControllerTests(ApiFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_MerchantDetail_Exists_GetOrUpsertMerchantDetailAsync_Should_Work_Correctly()
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["cardAcceptorName"] = $"{Constants.CardAcceptorName}";
            query["cardAcceptorCity"] = $"{Constants.CardAcceptorCity}";
            var merchantDetail = await AddMerchantDetail(query);
            // Act
            using var response = await _client.GetAsync(new Uri($"{BaseUrl}/lookup-merchantdetail?{query}", UriKind.Relative));
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<GetOrUpsertMerchantDetailResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.MerchantDetail.Id.Should().Be($"{merchantDetail.Id}");
            actual.MerchantDetail.MerchantCardAcceptorLocators
                  .FirstOrDefault(x => x.CardAcceptorLocator == $"{Constants.CardAcceptorName} {Constants.CardAcceptorCity}")
                  .Should().NotBeNull();
        }

        private async Task<MerchantDetail> AddMerchantDetail(NameValueCollection query)
        {
            query["cardAcceptorName"] = $"{Constants.CardAcceptorName}";
            query["cardAcceptorCity"] = $"{Constants.CardAcceptorCity}";

            // Act
            using var response = await _client.GetAsync(new Uri($"{BaseUrl}/lookup-merchantdetail?{query}", UriKind.Relative));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<GetOrUpsertMerchantDetailResponse>(await response.Content.ReadAsStringAsync()).MerchantDetail;
        }

        [Fact]
        public async Task Given_MerchantDetail_Not_Exist_GetOrUpsertMerchantDetailAsync_Should_Work_Correctly()
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["cardAcceptorName"] = $"{Constants.CardAcceptorName2}";
            query["cardAcceptorCity"] = $"{Constants.CardAcceptorCity2}";

            // Act
            using var response = await _client.GetAsync(new Uri($"{BaseUrl}/lookup-merchantdetail?{query}", UriKind.Relative));
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<GetOrUpsertMerchantDetailResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.MerchantDetail.Id.Should().Be($"{Constants.MerchantDetailId2}");
            actual.MerchantDetail.MerchantCardAcceptorLocators.First(x =>
                    x.CardAcceptorLocator == $"{Constants.CardAcceptorName2} {Constants.CardAcceptorCity2}").Should()
                .NotBe(null);
        }

        [Fact]
        public async Task Given_MerchantDetails_LookupMerchantDetailsAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = new GetMerchantDetailsQuery
            {
                MerchantDetailLookupItems = new List<MerchantDetailLookupItem>
                {
                    new MerchantDetailLookupItem
                    {
                        TransactionCorrelationGuid = Constants.TransactionCorrelationGuid1,
                        CardAcceptorLocator = Constants.CardAcceptorLocator1
                    },
                    new MerchantDetailLookupItem
                    {
                        TransactionCorrelationGuid = Constants.TransactionCorrelationGuid2,
                        CardAcceptorLocator = Constants.CardAcceptorLocator2
                    },
                    new MerchantDetailLookupItem
                    {
                        TransactionCorrelationGuid = Constants.FailedTransactionCorrelationGuid,
                        CardAcceptorLocator = Constants.FailedCardAcceptorLocator
                    }
                }
            };
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}/lookup-merchantdetails", UriKind.Relative), content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<GetMerchantDetailsResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.MerchantDetailLookupResults.Count.Should().Be(request.MerchantDetailLookupItems.Count);
            actual.MerchantDetailLookupResults.Select(x => x.TransactionCorrelationGuid)
                  .Should()
                  .BeEquivalentTo(request.MerchantDetailLookupItems.Select(y => y.TransactionCorrelationGuid));
            actual.MerchantDetailLookupResults.Single(x => x.TransactionCorrelationGuid == Constants.TransactionCorrelationGuid1)
                  .MerchantDetailItem.Should().NotBeNull();
            actual.MerchantDetailLookupResults.Single(x => x.TransactionCorrelationGuid == Constants.TransactionCorrelationGuid2)
                  .MerchantDetailItem.Should().NotBeNull();
            actual.MerchantDetailLookupResults.Single(x => x.TransactionCorrelationGuid == Constants.FailedTransactionCorrelationGuid)
                  .MerchantDetailItem.Should().BeNull();
        }

        [Fact]
        public async Task Given_MerchantDetail_Not_Exists_SearchMerchantDetailsAsync_Should_Work_Correctly()
        {
            // Arrange
            var paginationSetting = new PaginationSetting { PageSize = 100, Page = 1 };
            var request = Fixture.Build<SearchMerchantDetailsQuery>()
                                 .With(x => x.ABN, "01234567890")
                                 .With(x => x.ACN, "234567890")
                                 .With(x => x.Postcode, "0123")
                                 .With(x => x.PaginationSetting, paginationSetting)
                                 .Create();

            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}/search", UriKind.Relative), content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Given_MerchantDetail_Exists_SearchMerchantDetailsAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = new SearchMerchantDetailsQuery { MerchantName = "name", Postcode = "0870" };
            var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");

            // Act
            using var response = await _client.PostAsync(new Uri($"{BaseUrl}/search", UriKind.Relative), content);
            var actual = JsonConvert.DeserializeObject<SearchMerchantDetailsResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            actual.Should().NotBeNull();
            actual.MerchantDetails.Items.SelectMany(x => x.MerchantLinks).Should().NotBeNullOrEmpty();
        }
    }
}
