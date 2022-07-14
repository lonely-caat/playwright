using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MerchantDataEnrichment.Infrastructure.Common.Exceptions;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Interfaces;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Models;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Models.Request;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Models.Response;
using Zip.Partners.Core.Utilities.Extensions;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Infrastructure.MerchantLookupServiceTests
{
    public class MerchantLookupServiceTests : CommonTestsFixture
    {
        private readonly Mock<IMerchantLookupApiProxy> _merchantLookupApiProxy;
        private readonly MerchantLookupService _target;

        public MerchantLookupServiceTests()
        {
            var logger = new Mock<ILogger<MerchantLookupService>>();
            _merchantLookupApiProxy = new Mock<IMerchantLookupApiProxy>();

            _target = new MerchantLookupService(logger.Object, Mapper, _merchantLookupApiProxy.Object);
        }

        [Theory]
        [InlineData(ResponseConstants.SampleResponse1)]
        [InlineData(ResponseConstants.SampleResponse2)]
        public void Given_Response_Deserialization_Should_Work(string responseString)
        {
            // Arrange & Act
            var actual = JsonConvert.DeserializeObject<MerchantLookupResponse>(responseString);

            // Assert
            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task Given_Valid_Request_Response_When_LookupMerchantDetailAsync_Should_Work_Correctly()
        {
            // Arrange
            const string transactionCorrelationGuid = "71c13922-0b32-46c7-8499-29cfaa9d5161";
            var request = Fixture.Build<MerchantLookupDetail>()
                                 .With(x => x.TransactionCorrelationGuid, transactionCorrelationGuid)
                                 .With(x => x.CardAcceptorLocator, "Hungry Jacks Circular Quay")
                                 .Create();
            var merchantLookupResponse = JsonConvert.DeserializeObject<MerchantLookupResponse>(ResponseConstants.SampleResponse1);

            var merchantSearchResult = merchantLookupResponse
                                      .SearchResults
                                      .First(x => x.TransactionCorrelationGuid == transactionCorrelationGuid)
                                      .MerchantSearchResults.FirstOrDefault()?
                                      .MerchantDetailResult;

            var expected = Mapper.Map<MerchantDetail>(
                merchantSearchResult,
                opt =>
                {
                    opt.Items[ProxyConstants.CardAcceptorLocator] = merchantLookupResponse.SearchResults.First().Cal.ToUpperInvariant();
                });

            _merchantLookupApiProxy.Setup(x => x.SendMerchantLookupRequestAsync(It.IsAny<MerchantLookupRequest>(), CancellationToken))
                                   .ReturnsAsync(new HttpResponseMessage
                                    {
                                        StatusCode = HttpStatusCode.OK,
                                        Content = new StringContent(ResponseConstants.SampleResponse1)
                                    });
            // Act
            var actual = await _target.LookupMerchantDetailAsync(request, CancellationToken);

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected,
                                           opt => opt.Excluding(x => x.CreatedDateTime)
                                                     .Excluding(x => x.UpdatedDateTime));
        }

        [Theory]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        public async Task Given_Unsuccessfully_Response_When_LookupMerchantDetailAsync_Should_Throw_Exception(HttpStatusCode statusCode)
        {
            // Arrange
            var request = Fixture.Create<MerchantLookupDetail>();
            _merchantLookupApiProxy.Setup(x => x.SendMerchantLookupRequestAsync(It.IsAny<MerchantLookupRequest>(), CancellationToken))
                                   .ReturnsAsync(new HttpResponseMessage
                                    {
                                        StatusCode = statusCode
                                    });

            // Act
            Func<Task> func = async () => { await _target.LookupMerchantDetailAsync(request, CancellationToken); };

            // Assert
            var actual = await func.Should().ThrowAsync<LookWhosChargingApiException>();
            actual.And.HttpStatusCode.Should().Be(statusCode);
        }

        [Fact]
        public async Task Given_No_SearchResults_When_LookupMerchantDetailAsync_Should_Return_Null()
        {
            // Arrange
            var request = Fixture.Create<MerchantLookupDetail>();
            var response = Fixture.Build<MerchantLookupResponse>()
                                  .Without(x => x.SearchResults)
                                  .Create();

            _merchantLookupApiProxy.Setup(x => x.SendMerchantLookupRequestAsync(It.IsAny<MerchantLookupRequest>(), CancellationToken))
                                   .ReturnsAsync(new HttpResponseMessage
                                    {
                                        StatusCode = HttpStatusCode.OK,
                                        Content = new StringContent(response.ToJsonString())
                                    });

            // Act
            var actual = await _target.LookupMerchantDetailAsync(request, CancellationToken);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async Task Given_No_Related_Transaction_When_LookupMerchantDetailAsync_Should_Return_Null()
        {
            // Arrange
            var request = Fixture.Build<MerchantLookupDetail>()
                                 .With(x => x.TransactionCorrelationGuid, Guid.NewGuid().ToString)
                                 .Create();
            var searchResult = Fixture.Build<SearchResult>()
                                      .With(x => x.TransactionCorrelationGuid, Guid.NewGuid().ToString)
                                      .CreateMany();
            var response = Fixture.Build<MerchantLookupResponse>()
                                  .With(x => x.SearchResults, searchResult.ToList)
                                  .Create();

            _merchantLookupApiProxy.Setup(x => x.SendMerchantLookupRequestAsync(It.IsAny<MerchantLookupRequest>(), CancellationToken))
                                   .ReturnsAsync(new HttpResponseMessage
                                    {
                                        StatusCode = HttpStatusCode.OK,
                                        Content = new StringContent(response.ToJsonString())
                                    });

            // Act
            var actual = await _target.LookupMerchantDetailAsync(request, CancellationToken);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async Task Given_No_MerchantSearchResults_When_LookupMerchantDetailAsync_Should_Return_Null()
        {
            // Arrange
            var request = Fixture.Create<MerchantLookupDetail>();
            var searchResult = Fixture.Build<SearchResult>()
                                      .Without(x => x.MerchantSearchResults)
                                      .CreateMany();
            var response = Fixture.Build<MerchantLookupResponse>()
                                  .With(x => x.SearchResults, searchResult.ToList)
                                  .Create();

            _merchantLookupApiProxy.Setup(x => x.SendMerchantLookupRequestAsync(It.IsAny<MerchantLookupRequest>(), CancellationToken))
                                   .ReturnsAsync(new HttpResponseMessage
                                    {
                                        StatusCode = HttpStatusCode.OK,
                                        Content = new StringContent(response.ToJsonString())
                                    });
            // Act
            var actual = await _target.LookupMerchantDetailAsync(request, CancellationToken);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async Task Given_Valid_Request_Response_When_LookupMerchantDetailsAsync_Should_Work_Correctly()
        {
            // Arrange
            var request = new List<MerchantLookupDetail>
            {
                new MerchantLookupDetail
                {
                    TransactionCorrelationGuid = Constants.TransactionCorrelationGuid1,
                    CardAcceptorLocator = Constants.CardAcceptorLocator1
                },
                new MerchantLookupDetail
                {
                    TransactionCorrelationGuid = Constants.TransactionCorrelationGuid2,
                    CardAcceptorLocator = Constants.CardAcceptorLocator2
                },
                new MerchantLookupDetail
                {
                    TransactionCorrelationGuid = Constants.FailedTransactionCorrelationGuid,
                    CardAcceptorLocator = Constants.FailedCardAcceptorLocator
                }
            };

            _merchantLookupApiProxy.Setup(x => x.SendMerchantLookupRequestAsync(It.IsAny<MerchantLookupRequest>(), CancellationToken))
                                   .ReturnsAsync(new HttpResponseMessage
                                   {
                                       StatusCode = HttpStatusCode.OK,
                                       Content = new StringContent(ResponseConstants.SampleResponse2)
                                   });
            // Act
            var actual = await _target.LookupMerchantDetailsAsync(request, CancellationToken);

            // Assert
            actual.Count.Should().Be(request.Count);
            actual.Select(x => x.TransactionCorrelationGuid)
                  .Should()
                  .BeEquivalentTo(request.Select(y => y.TransactionCorrelationGuid));
            actual.Single(x => x.TransactionCorrelationGuid == Constants.TransactionCorrelationGuid1)
                  .MerchantDetail.Should().NotBeNull();
            actual.Single(x => x.TransactionCorrelationGuid == Constants.TransactionCorrelationGuid2)
                  .MerchantDetail.Should().NotBeNull();
            actual.Single(x => x.TransactionCorrelationGuid == Constants.FailedTransactionCorrelationGuid)
                  .MerchantDetail.Should().BeNull();
        }
    }
}
