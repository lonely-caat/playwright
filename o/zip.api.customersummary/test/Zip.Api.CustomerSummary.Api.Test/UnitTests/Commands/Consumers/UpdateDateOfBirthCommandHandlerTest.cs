using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateDateOfBirth;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateDateOfBirthCommandHandlerTest
    {
        private readonly Mock<IConsumerContext> _consumerContext;
        private readonly Mock<IApplicationEventService> _applicationEventService;

        public UpdateDateOfBirthCommandHandlerTest()
        {
            _consumerContext = new Mock<IConsumerContext>();
            _applicationEventService = new Mock<IApplicationEventService>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdateDateOfBirthCommandHandler(null, _applicationEventService.Object));
            Assert.Throws<ArgumentNullException>(() => new UpdateDateOfBirthCommandHandler(_consumerContext.Object, null));
        }

        [Fact]
        public async Task Given_AccountInfoNull_ShouldHandle_ApplicationDetailsUpdatedEvent()
        {
            var handler = new UpdateDateOfBirthCommandHandler(_consumerContext.Object, _applicationEventService.Object);

            await handler.Handle(new UpdateDateOfBirthCommand()
            {
                PersonalInfo = new Consumer(),
                AccountInfo = null
            }, CancellationToken.None);

            _consumerContext.Verify(x => x.UpdateDateOfBirthAsync(It.IsAny<long>(), It.IsAny<DateTime>()), Times.Once());
            _applicationEventService.Verify(x => x.AddApplicationEventAndPublish(It.IsAny<ApplicationDetailsUpdatedEvent>(), It.IsAny<string>(), It.IsAny<AggregateEventType>()), Times.Once());
            _applicationEventService.Verify(x => x.AddApplicationEventAndPublish(It.IsAny<AccountDetailsUpdatedEvent>(), It.IsAny<string>(), It.IsAny<AggregateEventType>()), Times.Never());
        }

        [Fact]
        public async Task Given_AccountInfoNotNull_ShouldHandle_AccountDetailsUpdatedEvent()
        {
            var handler = new UpdateDateOfBirthCommandHandler(_consumerContext.Object, _applicationEventService.Object);

            await handler.Handle(new UpdateDateOfBirthCommand()
            {
                PersonalInfo = new Consumer(),
                AccountInfo = new AccountInfo()
            }, CancellationToken.None);

            _consumerContext.Verify(x => x.UpdateDateOfBirthAsync(It.IsAny<long>(), It.IsAny<DateTime>()), Times.Once());
            _applicationEventService.Verify(x => x.AddApplicationEventAndPublish(It.IsAny<ApplicationDetailsUpdatedEvent>(), It.IsAny<string>(), It.IsAny<AggregateEventType>()), Times.Never());
            _applicationEventService.Verify(x => x.AddApplicationEventAndPublish(It.IsAny<AccountDetailsUpdatedEvent>(), It.IsAny<string>(), It.IsAny<AggregateEventType>()), Times.Once());
        }
    }
}
