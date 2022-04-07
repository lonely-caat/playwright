using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateName;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateNameCommandHandlerTest
    {
        private readonly Mock<IApplicationEventService> applicationEventService = new Mock<IApplicationEventService>();
        private readonly Mock<IConsumerContext> consumerContext = new Mock<IConsumerContext>();

        [Fact]
        public void Given_NullService_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateNameCommandHandler(null, consumerContext.Object);
            });
        }

        [Fact]
        public void Given_NullContext_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateNameCommandHandler(applicationEventService.Object, null);
            });
        }

        [Fact]
        public async Task Given_NullAccountInfo_ShouldAdd_ApplicationDetailsUpdatedEvent()
        {
            applicationEventService.Setup(x => 
                x.AddApplicationEventAndPublish(
                    It.IsAny<ApplicationDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>()));

            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish(
                    It.IsAny<AccountDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>()))
                .ThrowsAsync(new Exception());

            var handler = new UpdateNameCommandHandler(applicationEventService.Object, consumerContext.Object);
            var result = await handler.Handle(new UpdateNameCommand() { 
            PersonalInfo = new Consumer()}, CancellationToken.None);

            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task Given_NotNullAccountInfo_ShouldAdd_AccountDetailsUpdatedEvent()
        {
            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish(
                    It.IsAny<ApplicationDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>())).ThrowsAsync(new Exception());

            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish(
                    It.IsAny<AccountDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>()));


            var handler = new UpdateNameCommandHandler(applicationEventService.Object, consumerContext.Object);
            var result = await handler.Handle(new UpdateNameCommand() { AccountInfo = new AccountInfo() }, CancellationToken.None);

            Assert.Equal(Unit.Value, result);
        }
    }
}
