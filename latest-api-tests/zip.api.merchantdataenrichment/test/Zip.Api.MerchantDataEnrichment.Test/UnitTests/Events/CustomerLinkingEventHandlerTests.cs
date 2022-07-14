using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Exceptions;
using Zip.MerchantDataEnrichment.Application.DomainEvents.Common.Models;
using Zip.MerchantDataEnrichment.Application.DomainEvents.CustomerLinking;
using Zip.MerchantDataEnrichment.Domain.Configurations;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Events
{
    public class CustomerLinkingEventHandlerTests : CommonTestsFixture
    {
        private readonly CustomerLinkingEventHandler _target;

        public CustomerLinkingEventHandlerTests()
        {
            var logger = new Mock<ILogger<CustomerLinkingEventHandler>>();
            var dataEnrichmentOptions = new Mock<IOptions<DataEnrichmentOptions>>();
            var options = Fixture.Build<DataEnrichmentOptions>()
                                 .With(x => x.MerchantDetailRefreshIntervalInDays, 14)
                                 .With(x => x.CustomerDetailRefreshIntervalInDays, 7)
                                 .With(x => x.BackFillingCustomerDetailEnabled, true)
                                 .Create();
            dataEnrichmentOptions.Setup(x => x.Value).Returns(options);

            _target = new CustomerLinkingEventHandler(logger.Object,
                                                      dataEnrichmentOptions.Object,
                                                      DbContext);
        }

        [Fact]
        public async Task Given_CustomerDetail_Is_Not_Available_Handle_Should_Throw_Exception()
        {
            // Arrange
            var domainEvent = Fixture.Create<CustomerLinkingEvent>();

            // Act
            Func<Task> func = async () =>
            {
                await _target.Handle(new DomainEventNotification<CustomerLinkingEvent>(domainEvent),
                                     CancellationToken);
            };

            // Assert
            await func.Should().ThrowAsync<CustomerDetailNotFoundException>();
        }

        [Fact]
        public async Task Given_Valid_VcnTransactionId_And_CustomerDetail_Is_Available_Handle_Should_Link_CustomerDetail_Correctly()
        {
            // Arrange
            var vcnTransaction = Fixture.Build<VcnTransaction>()
                                        .Without(x => x.CustomerDetailId)
                                        .Without(x => x.CustomerDetail)
                                        .Create();
            var customerDetail = Fixture.Build<CustomerDetail>()
                                        .With(x => x.AccountId, vcnTransaction.AccountId)
                                        .With(x => x.CustomerId, vcnTransaction.CustomerId)
                                        .Without(x => x.VcnTransactions)
                                        .Create();
            var domainEvent = Fixture.Build<CustomerLinkingEvent>()
                                     .With(x => x.CustomerDetailId, customerDetail.Id)
                                     .With(x => x.VcnTransactionId, vcnTransaction.Id)
                                     .With(x => x.AccountId, vcnTransaction.AccountId)
                                     .With(x => x.CustomerId, vcnTransaction.CustomerId)
                                     .Create();

            await DbContext.VcnTransactions.AddAsync(vcnTransaction);
            await DbContext.CustomerDetails.AddAsync(customerDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            await _target.Handle(new DomainEventNotification<CustomerLinkingEvent>(domainEvent), CancellationToken);
            var actualVcnTransaction = await DbContext.VcnTransactions.FindAsync(vcnTransaction.Id);

            // Assert
            actualVcnTransaction.CustomerDetailId.Should().Be(customerDetail.Id);
            actualVcnTransaction.CustomerDetail.Should().BeEquivalentTo(customerDetail);
        }

        [Fact]
        public async Task Given_No_VcnTransactionId_And_CustomerDetail_Is_Available_And_BackFilling_Enabled_Handle_Should_Link_CustomerDetail_Correctly()
        {
            // Arrange
            var domainEvent = Fixture.Build<CustomerLinkingEvent>()
                                     .Without(x => x.VcnTransactionId)
                                     .Create();
            var vcnTransactions = Fixture.Build<VcnTransaction>()
                                        .With(x => x.AccountId, domainEvent.AccountId)
                                        .With(x => x.CustomerId, domainEvent.CustomerId)
                                        .Without(x => x.CustomerDetailId)
                                        .Without(x => x.CustomerDetail)
                                        .CreateMany(3);
            var customerDetail = Fixture.Build<CustomerDetail>()
                                        .With(x => x.Id, domainEvent.CustomerDetailId)
                                        .With(x => x.AccountId, domainEvent.AccountId)
                                        .With(x => x.CustomerId, domainEvent.CustomerId)
                                        .Without(x => x.VcnTransactions)
                                        .Create();

            await DbContext.VcnTransactions.AddRangeAsync(vcnTransactions);
            await DbContext.CustomerDetails.AddAsync(customerDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            await _target.Handle(new DomainEventNotification<CustomerLinkingEvent>(domainEvent), CancellationToken);
            var actualVcnTransaction = await DbContext
                                            .VcnTransactions
                                            .Where(x => x.AccountId == customerDetail.AccountId &&
                                                        x.CustomerId == customerDetail.CustomerId)
                                            .ToListAsync(CancellationToken);

            // Assert
            actualVcnTransaction.Should().HaveCount(3);
            actualVcnTransaction.Select(x => x.CustomerDetailId).Should().AllBeEquivalentTo(customerDetail.Id);
        }
    }
}