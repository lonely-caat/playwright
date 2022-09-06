using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateEmail;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateEmailCommandHandlerTest
    {
        private readonly Mock<ICustomersServiceProxy> customersServiceProxy = new Mock<ICustomersServiceProxy>();
        private readonly Mock<IApplicationEventService> applicationEventService = new Mock<IApplicationEventService>();

        [Fact]
        public void Given_NullProxy_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateEmailCommandHandler(null, applicationEventService.Object);
            });
        }

        [Fact]
        public void Given_NullService_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateEmailCommandHandler(customersServiceProxy.Object, null);
            });
        }

        [Fact]
        public async Task Given_ValidateCustomerEmailFailed_ShouldThrow_EmailValidationException()
        {
            customersServiceProxy.Setup(x => x.ValidateCustomerEmail(It.IsAny<string>(), It.IsAny<ValidateCustomerEmailRequest>())).ReturnsAsync(
                new ValidateCustomerEmailResponse()
                {
                    Success = false
                });

            var handler = new UpdateEmailCommandHandler(customersServiceProxy.Object, applicationEventService.Object);

            await Assert.ThrowsAsync<EmailValidationException>(
                async () => 
                    await handler.Handle(
                        new UpdateEmailCommand() { 
                            Consumer = new Consumer()
                            {

                            }
                        }, 
                        CancellationToken.None));
        }

        [Fact]
        public async Task Given_UpdateEmailFailed_ShouldThrow_EmailValidationException()
        {
            customersServiceProxy.Setup(x => x.ValidateCustomerEmail(It.IsAny<string>(), It.IsAny<ValidateCustomerEmailRequest>())).ReturnsAsync(
                new ValidateCustomerEmailResponse()
                {
                    Success = true
                });

            customersServiceProxy.Setup(x => 
                x.UpdateCustomerEmail(
                    It.IsAny<string>(), 
                    It.IsAny<UpdateCustomerEmailRequest>()))
                .ReturnsAsync(
                    new UpdateCustomerResponse()
                    {
                        Success = false
                    });

            var handler = new UpdateEmailCommandHandler(customersServiceProxy.Object, applicationEventService.Object);
            await Assert.ThrowsAsync<EmailValidationException>(async () =>
            {
                await handler.Handle(new UpdateEmailCommand() { Consumer = new Consumer()}, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_NullAccountInfo_ShouldInsert_ApplicationDetailsUpdatedEvent()
        {
            customersServiceProxy.Setup(x => x.ValidateCustomerEmail(It.IsAny<string>(), It.IsAny<ValidateCustomerEmailRequest>())).ReturnsAsync(
                new ValidateCustomerEmailResponse()
                {
                    Success = true
                });

            customersServiceProxy.Setup(x =>
                x.UpdateCustomerEmail(
                    It.IsAny<string>(),
                    It.IsAny<UpdateCustomerEmailRequest>()))
                .ReturnsAsync(
                    new UpdateCustomerResponse()
                    {
                        Success = true
                    });

            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish<ApplicationDetailsUpdatedEvent>(
                    It.IsAny<ApplicationDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>()));

            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish<AccountDetailsUpdatedEvent>(
                    It.IsAny<AccountDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>())).ThrowsAsync(new Exception());

            var handler = new UpdateEmailCommandHandler(customersServiceProxy.Object, applicationEventService.Object);
            var result = await handler.Handle(new UpdateEmailCommand() { Consumer = new Consumer()}, CancellationToken.None);

            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task Given_NotNullAccountInfo_ShouldInsert_AccountDetailsUpdatedEvent()
        {
            customersServiceProxy.Setup(x => x.ValidateCustomerEmail(It.IsAny<string>(), It.IsAny<ValidateCustomerEmailRequest>())).ReturnsAsync(
                new ValidateCustomerEmailResponse()
                {
                    Success = true
                });

            customersServiceProxy.Setup(x =>
                x.UpdateCustomerEmail(
                    It.IsAny<string>(),
                    It.IsAny<UpdateCustomerEmailRequest>()))
                .ReturnsAsync(
                    new UpdateCustomerResponse()
                    {
                        Success = true
                    });

            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish<ApplicationDetailsUpdatedEvent>(
                    It.IsAny<ApplicationDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>())).ThrowsAsync(new Exception());

            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish<AccountDetailsUpdatedEvent>(
                    It.IsAny<AccountDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>()));

            var handler = new UpdateEmailCommandHandler(customersServiceProxy.Object, applicationEventService.Object);
            var result = await handler.Handle(new UpdateEmailCommand() { Consumer = new Consumer(), AccountInfo = new AccountInfo()}, CancellationToken.None);

            Assert.Equal(Unit.Value, result);
        }
    }
}
