using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdatePhoneStatus
{
    public class UpdatePhoneStatusCommandHandler : IRequestHandler<UpdatePhoneStatusCommand, Phone>
    {
        private readonly IPhoneContext _phoneContext;
        private readonly IApplicationEventService _applicationEventService;
        private readonly IConsumerContext _consumerContext;

        public UpdatePhoneStatusCommandHandler(IPhoneContext phoneContext, IApplicationEventService applicationEventService, IConsumerContext consumerContext)
        {
            _phoneContext = phoneContext ?? throw new ArgumentNullException(nameof(phoneContext));
            _applicationEventService = applicationEventService ?? throw new ArgumentNullException(nameof(applicationEventService));
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
        }

        public async Task<Phone> Handle(UpdatePhoneStatusCommand request, CancellationToken cancellationToken)
        {
            if(await _phoneContext.GetConsumerPhoneAsync(request.ConsumerId, request.PhoneId) == null)
            {
                throw new ConsumerPhoneNotFoundException(request.ConsumerId, request.PhoneId);
            }

            var phone = await _phoneContext.GetAsync(request.PhoneId);
            if(phone == null)
            {
                throw new PhoneNotFoundException(request.PhoneId);
            }

            if (request.Active.HasValue)
            {
                phone.Active = request.Active.Value;
            }

            if (request.Preferred.HasValue)
            {
                phone.Preferred = request.Preferred.Value;
            }

            if (request.Deleted.HasValue)
            {
                if (request.Deleted.Value && phone.Preferred)
                {
                    throw new DeletePreferredPhoneException($"Preferred phone number can't be deleted.");
                }
                else
                {
                    phone.Deleted = request.Deleted.Value;
                }
            }

            if (phone.Deleted)
            {
                phone.Preferred = false;
                phone.Active = false;
            }

            var phoneUpdated = await _phoneContext.UpdateStatusAsync(request.ConsumerId, phone);

            if (phoneUpdated.Preferred && phoneUpdated.PhoneType == PhoneType.Mobile)
            {
                var accountInfo = await _consumerContext.GetAccountInfoAsync(request.ConsumerId);

                await _applicationEventService.AddApplicationEventAndPublish(
                      new AccountDetailsUpdatedEvent()
                      {
                          ApplicationId = accountInfo.ApplicationId,
                          AccountId = accountInfo.AccountId,
                          CustomerId = accountInfo.CustomerId,
                          PhoneNumber = phoneUpdated.PhoneNumber,
                          IsActivePhoneNumber = true,
                          Timestamp = DateTime.Now,
                          Source = CustomerDetailsUpdatedEvent.CustomerDetailsUpdatedEventSource.Admin,
                      },
                      accountInfo.CustomerId.ToString(),
                      AggregateEventType.ConsumerApplication);
            }

            return phoneUpdated;
        }
    }
}
