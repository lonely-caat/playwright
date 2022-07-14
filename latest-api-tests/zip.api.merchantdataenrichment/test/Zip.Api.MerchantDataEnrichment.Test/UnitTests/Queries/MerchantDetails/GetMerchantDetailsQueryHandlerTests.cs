using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Models;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.GetMerchantDetails;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Interfaces;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Models;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Queries.MerchantDetails
{
    public class GetMerchantDetailsQueryHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IMerchantLookupService> _merchantLookupService;

        private readonly GetMerchantDetailsQueryHandler _target;

        public GetMerchantDetailsQueryHandlerTests()
        {
            _merchantLookupService = new Mock<IMerchantLookupService>();
            var logger = new Mock<ILogger<GetMerchantDetailsQueryHandler>>();

            _target = new GetMerchantDetailsQueryHandler(logger.Object,
                                                         Mapper,
                                                         _merchantLookupService.Object);
        }

        [Fact]
        public async Task Given_Valid_MerchantLookupResults_Handle_Should_Return_Correctly()
        {
            // Arrange
            var query = Fixture.Create<GetMerchantDetailsQuery>();
            var lookupItems = query.MerchantDetailLookupItems.ToList();
            var merchantLookupResults = lookupItems.Select(x => new MerchantLookupResult
            {
                TransactionCorrelationGuid = x.TransactionCorrelationGuid,
                CardAcceptorLocator = x.CardAcceptorLocator,
                MerchantDetail = Fixture.Build<MerchantDetail>().Without(y => y.VcnTransactions).Create()
            }).ToList();

            _merchantLookupService
               .Setup(x => x.LookupMerchantDetailsAsync(It.IsAny<IList<MerchantLookupDetail>>(), CancellationToken))
               .ReturnsAsync(merchantLookupResults);

            // Act
            var actual = await _target.Handle(query, CancellationToken);

            // Assert
            actual.MerchantDetailLookupResults.Count.Should().Be(query.MerchantDetailLookupItems.Count);
            actual.MerchantDetailLookupResults.Select(x => x.TransactionCorrelationGuid)
                  .Should()
                  .BeEquivalentTo(query.MerchantDetailLookupItems.Select(y => y.TransactionCorrelationGuid));
            actual.MerchantDetailLookupResults.Select(x => x.CardAcceptorLocator)
                  .Should()
                  .BeEquivalentTo(query.MerchantDetailLookupItems.Select(y => y.CardAcceptorLocator));
            actual.MerchantDetailLookupResults.Select(x => x.MerchantDetailItem)
                  .Should()
                  .BeEquivalentTo(Mapper.Map<IList<MerchantDetailItem>>(merchantLookupResults.Select(x => x.MerchantDetail)));
        }

        [Fact]
        public async Task Given_Empty_MerchantLookupResults_Handle_Should_Return_Correctly()
        {
            // Arrange
            var query = Fixture.Create<GetMerchantDetailsQuery>();

            _merchantLookupService
               .Setup(x => x.LookupMerchantDetailsAsync(It.IsAny<IList<MerchantLookupDetail>>(), CancellationToken))
               .ReturnsAsync(Array.Empty<MerchantLookupResult>());

            // Act
            var actual = await _target.Handle(query, CancellationToken);

            // Assert
            actual.MerchantDetailLookupResults.Should().BeEmpty();
        }

        [Fact]
        public async Task Given_MerchantLookupService_Throws_Exception_Handle_Should_Throw()
        {
            // Arrange
            var query = Fixture.Create<GetMerchantDetailsQuery>();

            _merchantLookupService
               .Setup(x => x.LookupMerchantDetailsAsync(It.IsAny<IList<MerchantLookupDetail>>(), CancellationToken))
               .ThrowsAsync(new Exception());

            // Act
            Func<Task> func = async () => { await _target.Handle(query, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<Exception>();
        }
    }
}