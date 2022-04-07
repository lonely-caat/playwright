using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Consumers
{
    public class GetPersonalInfoQueryHandlerTest
    {

        [Fact]
        public async Task Given_ValidConsumerId_Should_Pass()
        {
            var mockContext = new Mock<IConsumerContext>();
            mockContext.Setup(x => x.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Consumer());
            var mockAddress = new Mock<IAddressContext>();
            var mockAccount = new Mock<IMerchantContext>();
            var handler = new GetConsumerQueryHandler(mockContext.Object, mockAddress.Object, mockAccount.Object);

            var result = await handler.Handle(new GetConsumerQuery(1), CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
