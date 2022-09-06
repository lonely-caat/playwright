using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Consumers
{
    public class GetConsumerQueryHandlerTest
    {
        private readonly GetConsumerQueryHandler _target;

        private readonly Mock<IConsumerContext> _consumerContext;
        
        private readonly Mock<IAddressContext> _addressContext;
        
        private readonly Mock<IMerchantContext> _merchantContext;

        private readonly Fixture _fixture;

        public GetConsumerQueryHandlerTest()
        {
            _consumerContext = new Mock<IConsumerContext>();
            _addressContext = new Mock<IAddressContext>();
            _merchantContext = new Mock<IMerchantContext>();
            _fixture = new Fixture();

            _target = new GetConsumerQueryHandler(_consumerContext.Object, _addressContext.Object, _merchantContext.Object);
        }

        [Fact]
        public void Given_ConsumerContextInjectionNull_Should_have_error()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetConsumerQueryHandler(null, _addressContext.Object, _merchantContext.Object);
            });
        }

        [Fact]
        public void Given_AddressContextInjectionNull_Should_have_error()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetConsumerQueryHandler(_consumerContext.Object, null, _merchantContext.Object);
            });
        }

        [Fact]
        public void Given_MerchantContextInjectionNull_Should_have_error()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetConsumerQueryHandler(_consumerContext.Object, _addressContext.Object, null);
            });
        }
        
        [Fact]
        public async Task When_ConsumerNotFound_Should_throw()
        {
            _consumerContext.Setup(x => x.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(null as Consumer);

            await Assert.ThrowsAsync<ConsumerNotFoundException>(
                async () =>
                {
                    await _target.Handle(new GetConsumerQuery(), CancellationToken.None);
                });
        }

        [Fact]
        public async Task Happy_Path()
        {
            // Arrange 
            var merchantId = 1234;
            var linkedConsumer = _fixture.Build<Consumer>()
                                         .Without(x => x.LinkedConsumer)
                                         .Create();
            var linkedAccountInfo = _fixture.Create<AccountInfo>();
            var consumer = _fixture.Build<Consumer>()
                                   .With(x => x.LinkedConsumer, linkedConsumer)
                                   .With(x=> x.OriginationMerchantId, merchantId)
                                   .With(x => x.ReferrerId, merchantId)
                                   .Without(x => x.ReferredBy)
                                   .Create();
            var merchant = _fixture.Build<Merchant>()
                                   .With(x => x.Id, merchantId)
                                   .Create();
            var address = _fixture.Create<Address>();
            var request = _fixture.Build<GetConsumerQuery>()
                                  .With(x => x.ConsumerId, consumer.ConsumerId)
                                  .Create();

            _consumerContext.Setup(x => x.GetAsync(request.ConsumerId))
                            .ReturnsAsync(consumer);

            _merchantContext.Setup(x => x.GetAsync(consumer.OriginationMerchantId.Value))
                            .ReturnsAsync(merchant);

            _addressContext.Setup(x => x.GetAsync(consumer.ConsumerId))
                           .ReturnsAsync(address);

            _consumerContext.Setup(x => x.GetLinkedConsumerIdAsync(consumer.ConsumerId))
                            .ReturnsAsync(linkedConsumer.ConsumerId);

            _consumerContext.Setup(x => x.GetAsync(linkedConsumer.ConsumerId))
                            .ReturnsAsync(linkedConsumer);

            _consumerContext.Setup(x => x.GetAccountInfoAsync(linkedConsumer.ConsumerId))
                            .ReturnsAsync(linkedAccountInfo);

            // Action
            var actual = await _target.Handle(request, CancellationToken.None);

            // Assert
            actual.ReferredBy.Should().BeEquivalentTo(merchant);
            actual.IsExclusiveAccount.Should().Be(merchant.Exclusive);
            actual.Address.Should().BeEquivalentTo(address);
            actual.LinkedConsumer.Should().BeEquivalentTo(linkedConsumer);
            actual.LinkedAccount.Should().BeEquivalentTo(linkedAccountInfo);
        }

        [Fact]
        public async Task Given_Consumer_Exists_And_OriginationMerchantId_Has_Value_When_Handle_GetConsumerQuery_Should_Have_Correct_ReferredBy_And_Exclusivity()
        {
            // Arrange
            var merchantId = 1234;
            var expect = _fixture.Build<Merchant>()
                                    .With(x => x.Id, merchantId)
                                    .With(x => x.Exclusive, true)
                                    .Create();
            var consumer = _fixture.Build<Consumer>()
                                   .With(x => x.ReferrerId, merchantId)
                                   .With(x => x.OriginationMerchantId, merchantId)
                                   .Without(x => x.LinkedConsumer)
                                   .Create();
            var request = _fixture.Build<GetConsumerQuery>()
                                  .With(x => x.ConsumerId, consumer.ConsumerId)
                                  .Create();

            _consumerContext.Setup(x => x.GetAsync(consumer.ConsumerId))
                            .ReturnsAsync(consumer);

            _merchantContext.Setup(x => x.GetAsync(consumer.OriginationMerchantId.Value))
                            .ReturnsAsync(expect);
            
            // Action
            var actual = await _target.Handle(request, new CancellationToken());
            
            // Assert
            actual.ReferredBy.Should().BeEquivalentTo(expect);
            actual.IsExclusiveAccount.Should().BeTrue();
        }

        [Fact]
        public async Task Given_Consumer_Exists_And_OriginationMerchantId_Is_Null_And_ReferrerId_Has_Value_When_Handle_GetConsumerQuery_Should_Have_Correct_ReferredBy_And_No_Exclusivity()
        {
            // Arrange
            var merchantId = 1234;
            var expect = _fixture.Build<Merchant>()
                                    .With(x => x.Id, merchantId)
                                    .With(x => x.Exclusive, true)
                                    .Create();
            var consumer = _fixture.Build<Consumer>()
                                   .With(x => x.ReferrerId, merchantId)
                                   .Without(x => x.LinkedConsumer)
                                   .Without(x => x.OriginationMerchantId)
                                   .Create();
            var request = _fixture.Build<GetConsumerQuery>()
                                  .With(x => x.ConsumerId, consumer.ConsumerId)
                                  .Create();

            _consumerContext.Setup(x => x.GetAsync(consumer.ConsumerId))
                            .ReturnsAsync(consumer);

            _merchantContext.Setup(x => x.GetAsync(consumer.ReferrerId.Value))
                            .ReturnsAsync(expect);

            // Action
            var actual = await _target.Handle(request, new CancellationToken());

            // Assert
            actual.ReferredBy.Should().BeEquivalentTo(expect);
            actual.IsExclusiveAccount.Should().BeFalse();
        }

        [Fact]
        public async Task Given_Consumer_Exists_And_OriginationMerchantId_Has_Value_And_ReferrerId_Has_Different_Value_When_Handle_GetConsumerQuery_Should_Have_Correct_ReferredBy_And_No_Exclusivity()
        {
            // Arrange
            var merchantId = 1234;
            var expect = _fixture.Build<Merchant>()
                .With(x => x.Id, merchantId)
                .With(x => x.Exclusive, true)
                .Create();
            var consumer = _fixture.Build<Consumer>()
                .With(x => x.ReferrerId, merchantId + 1)
                .With(x => x.OriginationMerchantId, merchantId)
                .Without(x => x.LinkedConsumer)
                .Create();
            var request = _fixture.Build<GetConsumerQuery>()
                .With(x => x.ConsumerId, consumer.ConsumerId)
                .Create();

            _consumerContext.Setup(x => x.GetAsync(consumer.ConsumerId))
                .ReturnsAsync(consumer);

            _merchantContext.Setup(x => x.GetAsync(consumer.OriginationMerchantId.Value))
                .ReturnsAsync(expect);

            // Action
            var actual = await _target.Handle(request, new CancellationToken());

            // Assert
            actual.ReferredBy.Should().BeEquivalentTo(expect);
            actual.IsExclusiveAccount.Should().BeTrue();
        }
        
        [Fact]
        public async Task Given_Consumer_Exists_And_OriginationMerchantId_And_ReferrerId_Are_Null_When_Handle_GetConsumerQuery_Should_Have_Correct_ReferredBy_Name()
        {
            // Arrange
            var expect = new Merchant { Name = "Applied direct" };
            var consumer = _fixture.Build<Consumer>()
                                   .Without(x => x.LinkedConsumer)
                                   .Without(x => x.OriginationMerchantId)
                                   .Without(x => x.ReferrerId)
                                   .Without(x => x.ReferredBy)
                                   .Create();
            var request = _fixture.Build<GetConsumerQuery>()
                                  .With(x => x.ConsumerId, consumer.ConsumerId)
                                  .Create();

            _consumerContext.Setup(x => x.GetAsync(consumer.ConsumerId))
                            .ReturnsAsync(consumer);

            // Action
            var actual = await _target.Handle(request, new CancellationToken());

            // Assert
            actual.ReferredBy.Name.Should().Be(expect.Name);
            actual.IsExclusiveAccount.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, "2020-02-02", "2020-02-02")]
        [InlineData("2020-02-02", null, "2020-02-02")]
        [InlineData("2019-09-09", "2020-02-02", "2020-02-02")]
        [InlineData("2020-02-02", "2019-09-09", "2020-02-02")]
        public async Task Given_CreationDate_And_ActivationDate_Then_RegistrationDate_Should_Be_Correct(
            string creationDateStr,
            string activationDateStr,
            string registrationDateStr)
        {
            // Arrange
            var creationDate = !string.IsNullOrEmpty(creationDateStr) ? DateTime.Parse(creationDateStr) : (DateTime?) null;
            var activationDate = !string.IsNullOrEmpty(activationDateStr) ? DateTime.Parse(activationDateStr) : (DateTime?)null;
            var registrationDate = !string.IsNullOrEmpty(registrationDateStr) ? DateTime.Parse(registrationDateStr) : (DateTime?)null;
            
            var linkedConsumer = _fixture.Build<Consumer>()
                                         .Without(x => x.LinkedConsumer)
                                         .Create();
            var consumer = _fixture.Build<Consumer>()
                                   .With(x => x.LinkedConsumer, linkedConsumer)
                                   .With(x=>x.CreationDate, creationDate)
                                   .With(x => x.ActivationDate, activationDate)
                                   .Without(x => x.OriginationMerchantId)
                                   .Without(x => x.ReferrerId)
                                   .Without(x => x.ReferredBy)
                                   .Create();
            var request = _fixture.Build<GetConsumerQuery>()
                                  .With(x => x.ConsumerId, consumer.ConsumerId)
                                  .Create();

            _consumerContext.Setup(x => x.GetAsync(request.ConsumerId))
                            .ReturnsAsync(consumer);

            // Action
            var actual = await _target.Handle(request, new CancellationToken());
            
            // Assert
            actual.RegistrationDate.Should()
                  .Be(registrationDate);
        }
    }
}
