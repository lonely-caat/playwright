using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.Vcn;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Consumers
{
    public class GetConsumerForVcnQueryHandlerTest
    {
        private readonly GetConsumerForVcnQueryHandler _target;

        private readonly Mock<IConsumerContext> _consumerContext;

        private readonly Fixture _fixture;

        public GetConsumerForVcnQueryHandlerTest()
        {
            _consumerContext = new Mock<IConsumerContext>();
            _fixture = new Fixture();

            _target = new GetConsumerForVcnQueryHandler(_consumerContext.Object);
        }

        [Fact]
        public void Given_ConsumerContextInjectionNull_Should_have_error()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetConsumerForVcnQueryHandler(null);
            });
        }

        [Fact]
        public async Task When_ConsumerNotFound_Should_throw()
        {
            _consumerContext.Setup(x => x.GetByCustomerIdAndProductAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                .ReturnsAsync(null as Consumer);

            await Assert.ThrowsAsync<ConsumerNotFoundException>(
                async () =>
                {
                    await _target.Handle(new GetConsumerForVcnQuery(Guid.NewGuid(), ProductClassification.zipPay.ToString()), CancellationToken.None);
                });
        }

        [Fact]
        public async Task Happy_Path()
        {
            // Arrange 
            var consumer = _fixture.Build<Consumer>().With(x => x.ConsumerId, 123456).Without(x => x.LinkedConsumer).Create();
            var request = _fixture.Build<GetConsumerForVcnQuery>()
                                  .With(x => x.CustomerId, consumer.CustomerId)
                                  .With(x => x.Product, ProductClassification.zipPay.ToString())
                                  .Create();

            _consumerContext.Setup(x => x.GetByCustomerIdAndProductAsync(request.CustomerId, 0))
                            .ReturnsAsync(consumer);

            // Action
            var actual = await _target.Handle(request, CancellationToken.None);

            // Assert
            actual.CustomerId.Should().Be(consumer.CustomerId);
            actual.ConsumerId.Should().Be(consumer.ConsumerId);
        }
    }
}
