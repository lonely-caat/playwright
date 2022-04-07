using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateMobile;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateMobileCommandHandlerTest
    {
        private readonly Mock<ICustomersServiceProxy> customersServiceProxy = new Mock<ICustomersServiceProxy>();
        private readonly Mock<IApplicationEventService> applicationEventService = new Mock<IApplicationEventService>();


        [Fact]
        public void Given_NullProxy_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateMobileCommandHandler(null, applicationEventService.Object);
            });
        }

        [Fact]
        public void Given_NullService_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateMobileCommandHandler(customersServiceProxy.Object, null);
            });
        }

        private UpdateMobileCommand GetCommand()
        {
            return new UpdateMobileCommand()
            {
                Consumer = new Consumer()
            };
        }

        [Fact]
        public async Task Given_ValidateCustomerMobileFailed_ShouldThrow_MobileValidationException()
        {
            customersServiceProxy.Setup(x =>
                x.ValidateCustomerMobile(
                    It.IsAny<string>(),
                    It.IsAny<ValidateCustomerMobileRequest>())).ReturnsAsync(new ValidateCustomerMobileResponse()
                    {
                        Success = false
                    });

            var handler = new UpdateMobileCommandHandler(customersServiceProxy.Object, applicationEventService.Object);

            await Assert.ThrowsAsync<MobileValidationException>(async () =>
            {
                await handler.Handle(GetCommand(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_UpdateCustomerMobileFailed_ShouldThrow_MobileValidationException()
        {
            customersServiceProxy.Setup(x =>
                x.ValidateCustomerMobile(
                    It.IsAny<string>(),
                    It.IsAny<ValidateCustomerMobileRequest>())).ReturnsAsync(new ValidateCustomerMobileResponse()
                    {
                        Success = true
                    });

            customersServiceProxy.Setup(x =>
                x.UpdateCustomerMobile(
                    It.IsAny<string>(),
                    It.IsAny<UpdateCustomerMobileRequest>()))
                .ReturnsAsync(new UpdateCustomerMobileResponse()
                {
                    Success = false
                });

            var handler = new UpdateMobileCommandHandler(customersServiceProxy.Object, applicationEventService.Object);

            await Assert.ThrowsAsync<MobileValidationException>(async () =>
            {
                await handler.Handle(GetCommand(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_NullAccountInfo_ShouldAdd_ApplicationDetailsUpdateEvent()
        {
            customersServiceProxy.Setup(x =>
                x.ValidateCustomerMobile(
                    It.IsAny<string>(),
                    It.IsAny<ValidateCustomerMobileRequest>())).ReturnsAsync(new ValidateCustomerMobileResponse()
                    {
                        Success = true
                    });

            customersServiceProxy.Setup(x =>
                x.UpdateCustomerMobile(
                    It.IsAny<string>(),
                    It.IsAny<UpdateCustomerMobileRequest>()))
                .ReturnsAsync(new UpdateCustomerMobileResponse()
                {
                    Success = true
                });

            applicationEventService.Setup(x =>
                x.AddApplicationEventAndPublish(
                    It.IsAny<ApplicationDetailsUpdatedEvent>(),
                    It.IsAny<string>(),
                    It.IsAny<AggregateEventType>()));

            applicationEventService.Setup(x =>
                  x.AddApplicationEventAndPublish(
                      It.IsAny<AccountDetailsUpdatedEvent>(),
                      It.IsAny<string>(),
                      It.IsAny<AggregateEventType>())).ThrowsAsync(new Exception());

            var handler = new UpdateMobileCommandHandler(customersServiceProxy.Object, applicationEventService.Object);

            var result = await handler.Handle(GetCommand(), CancellationToken.None);
            Assert.IsType<UpdateCustomerMobileResponse>(result);
        }

        [Fact]
        public async Task Given_NotNullAccountInfo_ShouldAdd_AccountDetailsUpdatedEvent()
        {
            customersServiceProxy.Setup(x =>
                x.ValidateCustomerMobile(
                    It.IsAny<string>(),
                    It.IsAny<ValidateCustomerMobileRequest>())).ReturnsAsync(new ValidateCustomerMobileResponse()
                    {
                        Success = true
                    });

            customersServiceProxy.Setup(x =>
                x.UpdateCustomerMobile(
                    It.IsAny<string>(),
                    It.IsAny<UpdateCustomerMobileRequest>()))
                .ReturnsAsync(new UpdateCustomerMobileResponse()
                {
                    Success = true
                });

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

            var handler = new UpdateMobileCommandHandler(customersServiceProxy.Object, applicationEventService.Object);

            var command = GetCommand();
            command.AccountInfo = new AccountInfo();
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.IsType<UpdateCustomerMobileResponse>(result);
        }
    }
}
