using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent.Interfaces;
using Zip.Api.CustomerSummary.Domain.Entities.Events;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class ApplicationEventServiceTest
    {
        private readonly Mock<IApplicationEventContext> _applicationEventContext;
        private readonly Mock<IEventBus> _eventBus;

        public ApplicationEventServiceTest()
        {
            _applicationEventContext = new Mock<IApplicationEventContext>();
            _eventBus = new Mock<IEventBus>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationEventService(null, _eventBus.Object));
            Assert.Throws<ArgumentNullException>(() => new ApplicationEventService(_applicationEventContext.Object, null));
        }

        [Fact]
        public async Task Should_AddApplicationEventAndPublish()
        {

            _applicationEventContext.Setup(x => x.CreateAsync(It.IsAny<ApplicationEvent>()))
                .ReturnsAsync(new ApplicationEvent());

            _eventBus.Setup(x => x.PublishAsync(It.IsAny<IEvent>()));

            var svc = new ApplicationEventService(_applicationEventContext.Object, _eventBus.Object);

            await svc.AddApplicationEventAndPublish<TestEvent>(new TestEvent(), "test-123", AggregateEventType.FundingProgram);

            _applicationEventContext.Verify(x => x.CreateAsync(It.IsAny<ApplicationEvent>()), Times.Once);
            _eventBus.Verify(x => x.PublishAsync(It.IsAny<IEvent>()), Times.Once);
        }

        [Fact]
        public async Task Given_ErrorInCreateEvent_ShouldNot_Throw_Ex()
        {
            _applicationEventContext.Setup(x => x.CreateAsync(It.IsAny<ApplicationEvent>()))
                .ThrowsAsync(new Exception());

            var svc = new ApplicationEventService(_applicationEventContext.Object, _eventBus.Object);
            await svc.AddApplicationEventAndPublish<TestEvent>(new TestEvent(), "test-123", AggregateEventType.FundingProgram);

            _applicationEventContext.Verify(x => x.CreateAsync(It.IsAny<ApplicationEvent>()), Times.Once);
        }

        [Fact]
        public async Task Given_ErrorInPublish_ShouldCall_MarkAsUnpublished()
        {
            _applicationEventContext.Setup(x => x.CreateAsync(It.IsAny<ApplicationEvent>()))
                .ReturnsAsync(new ApplicationEvent());

            _eventBus.Setup(x => x.PublishAsync(It.IsAny<IEvent>()))
                .ThrowsAsync(new Exception());

            _applicationEventContext.Setup(x => x.MarkAsUnpublishedAsync(It.IsAny<long>()));

            var svc = new ApplicationEventService(_applicationEventContext.Object, _eventBus.Object);
            await svc.AddApplicationEventAndPublish<TestEvent>(new TestEvent(), "test-123", AggregateEventType.FundingProgram);

            _applicationEventContext.Verify(x => x.MarkAsUnpublishedAsync(It.IsAny<long>()), Times.Once);
            _eventBus.Verify(x => x.PublishAsync(It.IsAny<IEvent>()), Times.Once);

        }

        public class TestEvent : Event
        {

        }
    }
}
