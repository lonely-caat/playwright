using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateDateOfBirth
{
    public class UpdateDateOfBirthCommandHandler : IRequestHandler<UpdateDateOfBirthCommand>
    {
        private readonly IConsumerContext _consumerContext;
        private readonly IApplicationEventService _applicationEventService;

        public UpdateDateOfBirthCommandHandler(IConsumerContext consumerContext, IApplicationEventService applicationEventService)
        {
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
            _applicationEventService = applicationEventService ?? throw new ArgumentNullException(nameof(applicationEventService));
        }

        public async Task<Unit> Handle(UpdateDateOfBirthCommand request, CancellationToken cancellationToken)
        {
            await _consumerContext.UpdateDateOfBirthAsync(request.PersonalInfo.ConsumerId, request.DateOfBirth);

            if (request.AccountInfo == null)
            {
                var applicationDOBChangedEvent = new ApplicationDetailsUpdatedEvent
                {
                    ApplicationId = request.PersonalInfo.ApplicationId,
                    CustomerId = request.PersonalInfo.CustomerId,
                    DateOfBirth = request.DateOfBirth,
                    Timestamp = DateTime.Now,
                    Source = CustomerDetailsUpdatedEvent.CustomerDetailsUpdatedEventSource.Admin
                };

                await _applicationEventService.AddApplicationEventAndPublish(
                    applicationDOBChangedEvent,
                    request.PersonalInfo.ConsumerId.ToString(),
                    AggregateEventType.ConsumerApplication);
            }
            else
            {
                var accountDOBChangedEvent = new AccountDetailsUpdatedEvent
                {
                    ApplicationId = request.PersonalInfo.ApplicationId,
                    AccountId = request.AccountInfo.AccountId,
                    CustomerId = request.PersonalInfo.CustomerId,
                    DateOfBirth = request.DateOfBirth,
                    Timestamp = DateTime.Now,
                    Source = CustomerDetailsUpdatedEvent.CustomerDetailsUpdatedEventSource.Admin
                };

                await _applicationEventService.AddApplicationEventAndPublish(
                    accountDOBChangedEvent,
                    request.PersonalInfo.ConsumerId.ToString(),
                    AggregateEventType.Consumer);
            }

            return Unit.Value;
        }
    }
}
