using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.SearchMerchantDetails;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Queries.MerchantDetails
{
    public class SearchMerchantDetailsQueryHandlerTests : CommonTestsFixture
    {
        private readonly SearchMerchantDetailsQueryHandler _target;

        public SearchMerchantDetailsQueryHandlerTests()
        {
            var logger = new Mock<ILogger<SearchMerchantDetailsQueryHandler>>();
            _target = new SearchMerchantDetailsQueryHandler(logger.Object,
                                                            Mapper,
                                                            DbContext);
        }

        [Fact]
        public async Task Given_MerchantName_Handle_Should_Work_Correctly()
        {
            // Arrange
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .Without(x => x.MerchantCardAcceptorLocators)
                                        .Create();

            var entity = (await DbContext.MerchantDetails.AddAsync(merchantDetail, CancellationToken)).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            var paginationSetting = new PaginationSetting { PageSize = 1, Page = 0 };
            var request = Fixture.Build<SearchMerchantDetailsQuery>()
                                 .With(x => x.MerchantName, merchantDetail.Name)
                                 .With(x => x.ABN, merchantDetail.ABN)
                                 .With(x => x.ACN, merchantDetail.ACN)
                                 .With(x => x.Postcode, merchantDetail.Postcode)
                                 .With(x => x.SingleLineAddress, merchantDetail.SingleLineAddress)
                                 .With(x => x.PaginationSetting, paginationSetting)
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);
            var actualMerchantLinks = actual.MerchantDetails.Items.SelectMany(x => x.MerchantLinks).ToList();

            // Assert
            actual.MerchantDetails.Items
                  .Single()
                  .Should()
                  .BeEquivalentTo(entity,
                                  opt => opt.Excluding(x => x.CreatedDateTime)
                                            .Excluding(x => x.UpdatedDateTime)
                                            .Excluding(x => x.VcnTransactions)
                                            .Excluding(x => x.MerchantCardAcceptorLocators)
                                            .Excluding(x => x.MerchantLinks)
                                            .Excluding(x => x.MerchantOpenLoopProducts));
            actualMerchantLinks.Should()
                               .BeEquivalentTo(merchantDetail.MerchantLinks,
                                               opt => opt.Excluding(x => x.MerchantDetail));
        }
    }
}