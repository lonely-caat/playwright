using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateMobile
{
    public class UpdateMobileCommandHandler : IRequestHandler<UpdateMobileCommand, UpdateCustomerMobileResponse>
    {
        private readonly ICustomersServiceProxy _customersServiceProxy;
        private readonly IApplicationEventService _applicationEventService;

        public UpdateMobileCommandHandler(ICustomersServiceProxy customersServiceProxy, IApplicationEventService applicationEventService)
        {
            _customersServiceProxy = customersServiceProxy ?? throw new ArgumentNullException(nameof(customersServiceProxy));
            _applicationEventService = applicationEventService ?? throw new ArgumentNullException(nameof(applicationEventService));
        }

        public async Task<UpdateCustomerMobileResponse> Handle(UpdateMobileCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.Consumer.CustomerId.ToString();

            var validateRequest = new ValidateCustomerMobileRequest
            {
                CustomerId = customerId,
                PhoneNumber = request.Mobile
            };

            var validateResponse = await _customersServiceProxy.ValidateCustomerMobile(customerId, validateRequest);

            if (!validateResponse.Success)
            {
                throw new MobileValidationException(validateResponse.Message);
            }

            var updateRequest = new UpdateCustomerMobileRequest
            {
                CustomerId = customerId,
                PhoneNumber = request.Mobile
            };

            var updateResponse = await _customersServiceProxy.UpdateCustomerMobile(customerId, updateRequest);

            if (!updateResponse.Success)
            {
                throw new MobileValidationException(updateResponse.Message);
            }

            if (request.AccountInfo == null)
            {
                await _applicationEventService.AddApplicationEventAndPublish(
                    new ApplicationDetailsUpdatedEvent()
                    {
                        ApplicationId = request.Consumer.ApplicationId,
                        CustomerId = request.Consumer.CustomerId,
                        PhoneNumber = request.Mobile,
                        IsActivePhoneNumber = true,
                        Timestamp = DateTime.Now,
                        Source = CustomerDetailsUpdatedEvent.CustomerDetailsUpdatedEventSource.Admin,
                    },
                    customerId,
                    AggregateEventType.ConsumerApplication);
            }
            else
            {
                await _applicationEventService.AddApplicationEventAndPublish(
                    new AccountDetailsUpdatedEvent()
                    {
                        ApplicationId = request.AccountInfo.ApplicationId,
                        AccountId = request.AccountInfo.AccountId,
                        CustomerId = request.AccountInfo.CustomerId,
                        PhoneNumber = request.Mobile,
                        IsActivePhoneNumber = true,
                        Timestamp = DateTime.Now,
                        Source = CustomerDetailsUpdatedEvent.CustomerDetailsUpdatedEventSource.Admin,
                    },
                    request.AccountInfo.CustomerId.ToString(),
                    AggregateEventType.ConsumerApplication);
            }

            return updateResponse;
        }
    }
}
