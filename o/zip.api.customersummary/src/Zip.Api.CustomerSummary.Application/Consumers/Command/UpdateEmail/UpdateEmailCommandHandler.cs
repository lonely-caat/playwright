using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateEmail
{
    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand>
    {
        private readonly IApplicationEventService _applicationEventService;
        private readonly ICustomersServiceProxy _customersServiceProxy;

        public UpdateEmailCommandHandler(ICustomersServiceProxy customersServiceProxy, IApplicationEventService applicationEventService)
        {
            _customersServiceProxy = customersServiceProxy ?? throw new ArgumentNullException(nameof(customersServiceProxy));
            _applicationEventService = applicationEventService ?? throw new ArgumentNullException(nameof(applicationEventService));
        }

        public async Task<Unit> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.Consumer.CustomerId.ToString();

            var validateRequest = new ValidateCustomerEmailRequest()
            {
                CustomerId = customerId,
                EmailAddress = request.Email
            };

            var validateResponse = await _customersServiceProxy.ValidateCustomerEmail(customerId, validateRequest);

            if (!validateResponse.Success)
            {
                throw new EmailValidationException(validateResponse.Message);
            }

            var updateRequest = new UpdateCustomerEmailRequest()
            {
                CustomerId = customerId,
                EmailAddress = request.Email
            };

            var updateResponse = await _customersServiceProxy.UpdateCustomerEmail(customerId, updateRequest);

            if (!updateResponse.Success)
            {
                throw new EmailValidationException(updateResponse.Message);
            }

            if (request.AccountInfo == null)
            {
                await _applicationEventService.AddApplicationEventAndPublish(
                    new ApplicationDetailsUpdatedEvent()
                    {
                        ApplicationId = request.Consumer.ApplicationId,
                        CustomerId = request.Consumer.CustomerId,
                        Email = request.Email,
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
                        Email = request.Email,
                        Timestamp = DateTime.Now,
                        Source = CustomerDetailsUpdatedEvent.CustomerDetailsUpdatedEventSource.Admin,
                    },
                    request.AccountInfo.CustomerId.ToString(),
                    AggregateEventType.ConsumerApplication);
            }
            return Unit.Value;
        }
    }
}
