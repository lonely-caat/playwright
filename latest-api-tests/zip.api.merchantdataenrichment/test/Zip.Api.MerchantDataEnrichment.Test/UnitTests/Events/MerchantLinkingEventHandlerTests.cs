using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Exceptions;
using Zip.MerchantDataEnrichment.Application.DomainEvents.Common.Models;
using Zip.MerchantDataEnrichment.Application.DomainEvents.MerchantLinking;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Command.GetOrUpsertMerchantDetail;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MessageTypes.Merchant;
using Zip.Partners.Core.Kafka.MessageBus;
using MerchantDetail = Zip.MerchantDataEnrichment.Domain.Entities.MerchantDetail;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Events
{
    public class MerchantLinkingEventHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IBus> _bus;

        private readonly MerchantLinkingEventHandler _target;

        public MerchantLinkingEventHandlerTests()
        {
            var logger = new Mock<ILogger<MerchantLinkingEventHandler>>();
            _bus = new Mock<IBus>();

            _target = new MerchantLinkingEventHandler(logger.Object,
                                                      Mapper,
                                                      MockMediator.Object,
                                                      DbContext,
                                                      _bus.Object);
        }

        [Fact]
        public async Task Given_Valid_MerchantDetailResponse_And_VcnTransaction_Exists_Handle_Should_Link_Merchant_Correctly()
        {
            // Arrange
            var domainEventNotification = Fixture.Create<DomainEventNotification<MerchantLinkingEvent>>();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .With(x => x.CardAcceptorLocator, domainEventNotification.DomainEvent.CardAcceptorLocator)
                                        .Create();
            var getOrUpsertMerchantDetailResponse = Fixture.Build<GetOrUpsertMerchantDetailResponse>()
                                                           .With(x => x.MerchantDetail, merchantDetail)
                                                           .Create();
            var yesterday = DateTime.Now.AddDays(-1);
            var vcnTransaction = Fixture.Build<VcnTransaction>()
                                        .Without(x => x.MerchantDetailId)
                                        .Without(x => x.MerchantDetail)
                                        .With(x => x.Id, domainEventNotification.DomainEvent.TransactionId)
                                        .With(x => x.CreatedDateTime, yesterday)
                                        .With(x => x.UpdatedDateTime, yesterday)
                                        .Create();

            MockMediator.Setup(x => x.Send(
                                   It.Is<GetOrUpsertMerchantDetailCommand>(
                                       y => y.CardAcceptorLocator == domainEventNotification.DomainEvent.CardAcceptorLocator),
                                   CancellationToken))
                        .ReturnsAsync(getOrUpsertMerchantDetailResponse);

            await DbContext.VcnTransactions.AddAsync(vcnTransaction);
            await DbContext.MerchantDetails.AddAsync(merchantDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            await _target.Handle(domainEventNotification, CancellationToken);
            var actualVcnTransaction = await DbContext.VcnTransactions.FindAsync(vcnTransaction.Id);

            // Assert
            actualVcnTransaction.MerchantDetailId.Should().Be(merchantDetail.Id);
            actualVcnTransaction.CreatedDateTime.Should().Be(yesterday);
            actualVcnTransaction.UpdatedDateTime.Should().BeAfter(yesterday);
            _bus.Verify(x => x.Publish(It.IsAny<MerchantDataOfVcnTransactionEnriched>(), CancellationToken), Times.Once);
        }

        [Fact]
        public async Task Given_Valid_MerchantDetailResponse_When_VcnTransaction_Not_Found_Handle_Should_Throw_Exception()
        {
            // Arrange
            var domainEventNotification = Fixture.Create<DomainEventNotification<MerchantLinkingEvent>>();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .With(x => x.CardAcceptorLocator, domainEventNotification.DomainEvent.CardAcceptorLocator)
                                        .With(x => x.CreatedDateTime, DateTime.Now)
                                        .With(x => x.UpdatedDateTime, DateTime.Now)
                                        .Create();

            var getOrUpsertMerchantDetailResponse = Fixture.Build<GetOrUpsertMerchantDetailResponse>()
                                                           .With(x => x.MerchantDetail, merchantDetail)
                                                           .Create();
            MockMediator.Setup(x => x.Send(
                                   It.Is<GetOrUpsertMerchantDetailCommand>(
                                       y => y.CardAcceptorLocator == domainEventNotification.DomainEvent.CardAcceptorLocator),
                                   CancellationToken))
                        .ReturnsAsync(getOrUpsertMerchantDetailResponse);

            await DbContext.MerchantDetails.AddAsync(merchantDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> func = async () => { await _target.Handle(domainEventNotification, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<VcnTransactionNotFoundException>();
        }
    }
}
