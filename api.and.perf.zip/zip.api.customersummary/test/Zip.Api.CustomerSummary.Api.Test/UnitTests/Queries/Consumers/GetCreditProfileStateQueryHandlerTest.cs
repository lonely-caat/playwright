using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetCreditProfileState;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Consumers
{
    public class GetCreditProfileStateQueryHandlerTest
    {
        private readonly Mock<ICreditProfileContext> _creditProfileContext;

        public GetCreditProfileStateQueryHandlerTest()
        {
            _creditProfileContext = new Mock<ICreditProfileContext>();
        }

        [Fact]
        public void Given_InjectionNull_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetCreditProfileStateQueryHandler(null);
            });
        }

        [Fact]
        public async Task Should_return()
        {
            _creditProfileContext.Setup(x => x.GetStateTypeByConsumerIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new CreditProfileState() { Id = 222 });


            var handler = new GetCreditProfileStateQueryHandler(_creditProfileContext.Object);
            var result = await handler.Handle(new GetCreditProfileStateQuery(), CancellationToken.None);

            Assert.Equal(222, result.Id);
        }
    }
}
