using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Command.GetOrUpsertMerchantDetail;
using Zip.MerchantDataEnrichment.Domain.Configurations;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Interfaces;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Models;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.MerchantDetails
{
    public class GetOrUpsertMerchantDetailCommandHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IMerchantLookupService> _merchantLookupService;
        private readonly GetOrUpsertMerchantDetailCommandHandler _target;

        public GetOrUpsertMerchantDetailCommandHandlerTests()
        {
            var logger = new Mock<ILogger<GetOrUpsertMerchantDetailCommandHandler>>();
            var lookWhosChargingApiOptions = new Mock<IOptions<LookWhosChargingApiOptions>>();
            var options = Fixture.Build<LookWhosChargingApiOptions>()
                                 .With(x => x.RefreshIntervalInHours, 24)
                                 .Create();
            _merchantLookupService = new Mock<IMerchantLookupService>();

            lookWhosChargingApiOptions.Setup(x => x.Value).Returns(options);

            _target = new GetOrUpsertMerchantDetailCommandHandler(logger.Object,
                                                                  Mapper,
                                                                  lookWhosChargingApiOptions.Object,
                                                                  DbContext,
                                                                  _merchantLookupService.Object);
        }

        [Fact]
        public async Task Given_Valid_MerchantDetail_Exists_Handle_Should_Return_Correctly()
        {
            // Arrange
            var sameDay = DateTime.Now.AddHours(-23);

            var request = Fixture.Build<GetOrUpsertMerchantDetailCommand>()
                                 .With(x => x.CardAcceptorName, Constants.CardAcceptorName)
                                 .With(x => x.CardAcceptorCity, Constants.CardAcceptorCity)
                                 .Create();

            var id = Guid.NewGuid();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .With(x => x.Id, id)
                                        .With(x => x.CreatedDateTime, sameDay)
                                        .With(x => x.UpdatedDateTime, sameDay)
                                        .With(x => x.MerchantCardAcceptorLocators,
                                            () => Fixture.Build<MerchantCardAcceptorLocator>()
                                                .With(r => r.CardAcceptorLocator, request.CardAcceptorLocator)
                                                .With(r => r.MerchantDetailId, id)
                                                .CreateMany<MerchantCardAcceptorLocator>(1).ToList())
                                        .Create();

            var merchantDetailEntity = (await DbContext.MerchantDetails.AddAsync(merchantDetail)).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.MerchantDetail.Id.Should().Be(merchantDetailEntity.Id);
            actual.MerchantDetail.CreatedDateTime.Should().Be(sameDay);
            actual.MerchantDetail.UpdatedDateTime.Should().Be(sameDay);
            _merchantLookupService.Verify(
                x => x.LookupMerchantDetailAsync(It.IsAny<MerchantLookupDetail>(), CancellationToken),
                Times.Never);
        }

        [Fact]
        public async Task Given_MerchantDetail_Not_Exist_Handle_Should_Insert_Correctly()
        {
            // Arrange
            var today = DateTime.Now;
            var request = Fixture.Create<GetOrUpsertMerchantDetailCommand>();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .With(x => x.CardAcceptorLocator, request.CardAcceptorLocator)
                                        .With(x => x.CreatedDateTime, today)
                                        .With(x => x.UpdatedDateTime, today)
                                        .Create();

            _merchantLookupService.Setup(x => x.LookupMerchantDetailAsync(
                                             It.Is<MerchantLookupDetail>(
                                                 y => y.CardAcceptorLocator == request.CardAcceptorLocator),
                                             CancellationToken))
                                  .ReturnsAsync(merchantDetail);

            // Act
            var actual = await _target.Handle(request, CancellationToken);
            var actualMerchantDetail = await DbContext.MerchantDetails.FindAsync(actual.MerchantDetail.Id);

            // Assert
            actualMerchantDetail.Should().BeEquivalentTo(merchantDetail);
        }

        [Fact]
        public async Task Given_MerchantDetail_Exists_But_Obsoletes_Handle_Should_Update_Correctly()
        {
            // Arrange
            var lastMonth = DateTime.Now.AddMonths(-1);
            var today = DateTime.Now;

            var request = Fixture.Build<GetOrUpsertMerchantDetailCommand>()
                                 .With(x => x.CardAcceptorName, Constants.CardAcceptorName)
                                 .With(x => x.CardAcceptorCity, Constants.CardAcceptorCity)
                                 .Create();
            var id = Guid.NewGuid();

            var oldMerchantDetail = Fixture.Build<MerchantDetail>()
                                           .With(x => x.Id, id)
                                           .With(x => x.CardAcceptorLocator, request.CardAcceptorLocator)
                                           .With(x => x.CreatedDateTime, lastMonth)
                                           .With(x => x.UpdatedDateTime, lastMonth)
                                           .With(x => x.MerchantCardAcceptorLocators,
                                               () => Fixture.Build<MerchantCardAcceptorLocator>()
                                                   .With(r => r.CardAcceptorLocator, request.CardAcceptorLocator)
                                                   .With(r => r.MerchantDetailId, id)
                                                   .CreateMany<MerchantCardAcceptorLocator>(1).ToList())
                                           .Without(x => x.MerchantLinks)
                                           .Create();

            var newMerchantDetail = Fixture.Build<MerchantDetail>()
                                           .With(x => x.Id, id)
                                           .With(x => x.CardAcceptorLocator, request.CardAcceptorLocator)
                                           .With(x => x.CreatedDateTime, today)
                                           .With(x => x.UpdatedDateTime, today)
                                           .Without(x => x.VcnTransactions)
                                           .Without(x => x.MerchantLinks)
                                           .Create();

            newMerchantDetail.MerchantCardAcceptorLocators = new List<MerchantCardAcceptorLocator> { oldMerchantDetail.MerchantCardAcceptorLocators.First() };

            _merchantLookupService.Setup(x => x.LookupMerchantDetailAsync(It.IsAny<MerchantLookupDetail>(), CancellationToken))
                                  .ReturnsAsync(newMerchantDetail);

            await DbContext.MerchantDetails.AddAsync(oldMerchantDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            await _target.Handle(request, CancellationToken);
            var actualMerchantDetail = await DbContext.MerchantDetails.FindAsync(oldMerchantDetail.Id);

            // Assert
            actualMerchantDetail.Should()
                                .BeEquivalentTo(newMerchantDetail,
                                                opt => opt.Excluding(x => x.CreatedDateTime)
                                                          .Excluding(x => x.VcnTransactions)
                                                          .Excluding(x => x.Id)
                                                          .Excluding(x => x.MerchantOpenLoopProducts)
                                                          .Excluding(x => x.MerchantCardAcceptorLocators));
            actualMerchantDetail.CreatedDateTime.Should().Be(oldMerchantDetail.CreatedDateTime);
            actualMerchantDetail.UpdatedDateTime.Should().Be(today);
        }
    }
}
