using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.DomainEvents.MerchantLinking;
using Zip.MerchantDataEnrichment.Application.Services.DomainEventService;
using Zip.MerchantDataEnrichment.Application.Transaction.Command.CreateVcnTransaction;
using Zip.MerchantDataEnrichment.Domain.Configurations;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MessageTypes.Merchant;
using Zip.Partners.Core.Kafka.MessageBus;
using MerchantDetail = Zip.MerchantDataEnrichment.Domain.Entities.MerchantDetail;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.Transaction
{
    public class CreateVcnTransactionCommandHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IDomainEventService> _domainEventService;
        private readonly CreateVcnTransactionCommandHandler _target;
        private readonly Mock<IBus> _bus;

        public CreateVcnTransactionCommandHandlerTests()
        {
            var logger = new Mock<ILogger<CreateVcnTransactionCommandHandler>>();
            var dataEnrichmentOptions = new Mock<IOptions<DataEnrichmentOptions>>();
            var options = Fixture.Build<DataEnrichmentOptions>()
                                 .With(x => x.MerchantDetailRefreshIntervalInDays, 14)
                                 .With(x => x.CustomerDetailRefreshIntervalInDays, 7)
                                 .Create();
            dataEnrichmentOptions.Setup(x => x.Value).Returns(options);
            _domainEventService = new Mock<IDomainEventService>();
            _bus = new Mock<IBus>();

            _target = new CreateVcnTransactionCommandHandler(logger.Object,
                                                             Mapper,
                                                             dataEnrichmentOptions.Object,
                                                             DbContext,
                                                             _domainEventService.Object,
                                                             _bus.Object);
        }

        [Fact]
        public async Task Given_MerchantDetail_Exists_And_Valid_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .With(x => x.CardAcceptorLocator, request.CardAcceptorLocator)
                                        .With(x => x.UpdatedDateTime, DateTime.Now)
                                        .Without(x => x.VcnTransactions)
                                        .Create();
            var merchantCardAcceptorLocator = Fixture.Build<MerchantCardAcceptorLocator>()
                                                     .With(x => x.CardAcceptorLocator, merchantDetail.CardAcceptorLocator)
                                                     .With(x => x.MerchantDetailId, merchantDetail.Id)
                                                     .Without(x => x.MerchantDetail)
                                                     .Create();

            var merchantDetailEntity = (await DbContext.MerchantDetails.AddAsync(merchantDetail, CancellationToken)).Entity;
            await DbContext.MerchantCardAcceptorLocators.AddAsync(merchantCardAcceptorLocator, CancellationToken);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var result = await _target.Handle(request, CancellationToken);
            var vcnTransactionEntity = await DbContext.VcnTransactions.FindAsync(result.TransactionId);

            // Assert
            result.Should().NotBeNull();
            vcnTransactionEntity.Should().NotBeNull();
            _domainEventService.Verify(x => x.Publish(It.IsAny<MerchantLinkingEvent>(), CancellationToken), Times.Never);
            _bus.Verify(x => x.Publish(It.IsAny<MerchantDataOfVcnTransactionEnriched>(), CancellationToken), Times.Once);

            // Cleanup
            DbContext.VcnTransactions.Remove(vcnTransactionEntity);
            DbContext.MerchantDetails.Remove(merchantDetailEntity);
        }

        [Fact]
        public async Task Given_MerchantDetail_Exists_But_Require_Update_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .With(x => x.CardAcceptorLocator, request.CardAcceptorLocator)
                                        .With(x => x.UpdatedDateTime, DateTime.Now.AddMonths(-2))
                                        .Without(x => x.VcnTransactions)
                                        .Create();
            var merchantDetailEntity = (await DbContext.MerchantDetails.AddAsync(merchantDetail, CancellationToken)).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var result = await _target.Handle(request, CancellationToken);
            var vcnTransactionEntity = await DbContext.VcnTransactions.FindAsync(result.TransactionId);

            // Assert
            result.Should().NotBeNull();
            vcnTransactionEntity.Should().NotBeNull();
            _domainEventService.Verify(x => x.Publish(It.IsAny<MerchantLinkingEvent>(), CancellationToken), Times.Once);
            _bus.Verify(x => x.Publish(It.IsAny<MerchantDataOfVcnTransactionEnriched>(), CancellationToken), Times.Never);

            // Cleanup
            DbContext.VcnTransactions.Remove(vcnTransactionEntity);
            DbContext.MerchantDetails.Remove(merchantDetailEntity);
        }

        [Fact]
        public async Task Given_MerchantDetail_Not_Exist_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();

            // Act
            var result = await _target.Handle(request, CancellationToken);
            var entity = await DbContext.VcnTransactions.FindAsync(result.TransactionId);

            // Assert
            result.Should().NotBeNull();
            entity.Should().NotBeNull();
            _domainEventService.Verify(x => x.Publish(It.Is<MerchantLinkingEvent>(
                                                          y => y.TransactionId == entity.Id &&
                                                               y.CardAcceptorName == entity.CardAcceptorName &&
                                                               y.CardAcceptorCity == entity.CardAcceptorCity),
                                                      CancellationToken),
                                       Times.Once);

            // Cleanup
            DbContext.VcnTransactions.Remove(entity);
        }

        [Fact]
        public async Task Given_CustomerDetail_Exists_And_Valid_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();
            var customerDetail = Fixture.Build<CustomerDetail>()
                                        .With(x => x.AccountId, request.AccountId)
                                        .With(x => x.CustomerId, request.CustomerId)
                                        .With(x => x.UpdatedDateTime, DateTime.Now)
                                        .Without(x => x.VcnTransactions)
                                        .Create();
            var customerDetailEntity = (await DbContext.CustomerDetails.AddAsync(customerDetail, CancellationToken)).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var result = await _target.Handle(request, CancellationToken);
            var vcnTransactionEntity = await DbContext.VcnTransactions.FindAsync(result.TransactionId);

            // Assert
            result.Should().NotBeNull();
            vcnTransactionEntity.Should().NotBeNull();
            vcnTransactionEntity.CustomerDetail.Should().NotBeNull();
            _bus.Verify(x => x.Publish(It.IsAny<CustomerDetailEnrichmentRequested>(), CancellationToken), Times.Never);

            // Cleanup
            DbContext.VcnTransactions.Remove(vcnTransactionEntity);
            DbContext.CustomerDetails.Remove(customerDetailEntity);
        }

        [Fact]
        public async Task Given_CustomerDetail_Exists_But_Require_Update_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();
            var customerDetail = Fixture.Build<CustomerDetail>()
                                        .With(x => x.AccountId, request.AccountId)
                                        .With(x => x.CustomerId, request.CustomerId)
                                        .With(x => x.UpdatedDateTime, DateTime.Now.AddMonths(-2))
                                        .Without(x => x.VcnTransactions)
                                        .Create();
            var customerDetailEntity = (await DbContext.CustomerDetails.AddAsync(customerDetail, CancellationToken)).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var result = await _target.Handle(request, CancellationToken);
            var vcnTransactionEntity = await DbContext.VcnTransactions.FindAsync(result.TransactionId);

            // Assert
            result.Should().NotBeNull();
            vcnTransactionEntity.Should().NotBeNull();
            vcnTransactionEntity.CustomerDetail.Should().NotBeNull();
            _bus.Verify(
                x => x.Publish(
                    It.Is<CustomerDetailEnrichmentRequested>(y => y.AccountId == request.AccountId &&
                                                                  y.CustomerId == request.CustomerId.ToString() &&
                                                                  y.VcnTransactionId == vcnTransactionEntity.Id.ToString()),
                    CancellationToken), Times.Once);

            // Cleanup
            DbContext.VcnTransactions.Remove(vcnTransactionEntity);
            DbContext.CustomerDetails.Remove(customerDetailEntity);
        }

        [Fact]
        public async Task Given_CustomerDetail_Not_Exist_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();

            // Act
            var result = await _target.Handle(request, CancellationToken);
            var vcnTransactionEntity = await DbContext.VcnTransactions.FindAsync(result.TransactionId);

            // Assert
            result.Should().NotBeNull();
            vcnTransactionEntity.Should().NotBeNull();
            _bus.Verify(
                x => x.Publish(
                    It.Is<CustomerDetailEnrichmentRequested>(y => y.AccountId == request.AccountId &&
                                                                  y.CustomerId == request.CustomerId.ToString() &&
                                                                  y.VcnTransactionId == vcnTransactionEntity.Id.ToString()),
                    CancellationToken), Times.Once);

            // Cleanup
            DbContext.VcnTransactions.Remove(vcnTransactionEntity);
        }
    }
}
