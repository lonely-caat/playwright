using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdatePhoneStatus;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdatePhoneStatusCommandHandlerTest
    {
        private readonly Mock<IPhoneContext> _phoneContext;
        private readonly Mock<IApplicationEventService> _applicationEventService;
        private readonly Mock<IConsumerContext> _consumerContext;

        public UpdatePhoneStatusCommandHandlerTest()
        {
            _phoneContext = new Mock<IPhoneContext>();
            _applicationEventService = new Mock<IApplicationEventService>();
            _consumerContext = new Mock<IConsumerContext>();
        }

        [Fact]
        public void Given_AnyNullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdatePhoneStatusCommandHandler(null, _applicationEventService.Object, _consumerContext.Object));
            Assert.Throws<ArgumentNullException>(() => new UpdatePhoneStatusCommandHandler(_phoneContext.Object, null, _consumerContext.Object));
            Assert.Throws<ArgumentNullException>(() => new UpdatePhoneStatusCommandHandler(_phoneContext.Object, _applicationEventService.Object, null));
        }

        [Fact]
        public async Task Given_InvalidData_ShouldThrow_ConsumerPhoneNotFoundException()
        {
            _phoneContext.Setup(x => x.GetConsumerPhoneAsync(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(null as ConsumerPhone);

            var handler = new UpdatePhoneStatusCommandHandler(_phoneContext.Object, _applicationEventService.Object, _consumerContext.Object);

            await Assert.ThrowsAsync<ConsumerPhoneNotFoundException>(async () =>
            {
                await handler.Handle(new UpdatePhoneStatusCommand(), default);
            });
        }
    }
}
