using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Customer.Command.UpsertCustomerDetail;
using Zip.MerchantDataEnrichment.Application.DomainEvents.CustomerLinking;
using Zip.MerchantDataEnrichment.Application.Services.DomainEventService;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.Customer
{
    public class UpsertCustomerDetailCommandHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IDomainEventService> _domainEventService;

        private readonly UpsertCustomerDetailCommandHandler _target;

        public UpsertCustomerDetailCommandHandlerTests()
        {
            var logger = new Mock<ILogger<UpsertCustomerDetailCommandHandler>>();
            _domainEventService = new Mock<IDomainEventService>();

            _target = new UpsertCustomerDetailCommandHandler(logger.Object,
                                                             Mapper,
                                                             DbContext,
                                                             _domainEventService.Object);
        }

        [Theory]
        [InlineData(Constants.CustomerDetailCustomerId)]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("")]
        public async Task Given_Valid_CustomerDetail_Exists_Handle_Should_Return_Correctly(string customerId)
        {
            // Arrange
            var customerIdValue = string.IsNullOrEmpty(customerId) ? null : new Guid(customerId) as Guid?;
            var sameDay = DateTime.Now.AddHours(-6);
            var customerDetail = Fixture.Build<CustomerDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .With(x => x.CustomerId, customerIdValue)
                                        .With(x => x.CreatedDateTime, sameDay)
                                        .With(x => x.UpdatedDateTime, sameDay)
                                        .Create();
            var request = Fixture.Build<UpsertCustomerDetailCommand>()
                                 .With(x => x.AccountId, customerDetail.AccountId)
                                 .With(x => x.CustomerId, customerId)
                                 .Create();

            await DbContext.CustomerDetails.AddAsync(customerDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.CustomerDetail.AccountId.Should().Be(customerDetail.AccountId);
            actual.CustomerDetail.CustomerId.Should().Be(customerDetail.CustomerId);
            actual.CustomerDetail.CreatedDateTime.Should().Be(sameDay);
            actual.CustomerDetail.UpdatedDateTime.Should().BeAfter(sameDay);
            _domainEventService.Verify(x => x.Publish(It.IsAny<CustomerLinkingEvent>(), CancellationToken), Times.Once);
        }

        [Theory]
        [InlineData("d5b11b6f-820a-481b-92fb-e32fe23b90c4")]
        [InlineData("")]
        public async Task Given_Valid_CustomerDetail_Not_Exists_Handle_Should_Return_Correctly(string customerId)
        {
            // Arrange
            var request = Fixture.Build<UpsertCustomerDetailCommand>()
                                 .With(x => x.CustomerId, customerId)
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.CustomerDetail.AccountId.Should().Be(request.AccountId);
            actual.CustomerDetail.CustomerId.ToString().Should().Be(request.CustomerId);
            actual.CustomerDetail.CreatedDateTime.Should().Be(actual.CustomerDetail.UpdatedDateTime);
            _domainEventService.Verify(x => x.Publish(It.IsAny<CustomerLinkingEvent>(), CancellationToken), Times.Once);
        }
    }
}
