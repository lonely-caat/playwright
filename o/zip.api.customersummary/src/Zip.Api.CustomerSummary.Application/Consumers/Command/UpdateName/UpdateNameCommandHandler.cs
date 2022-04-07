using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateName
{
    public class UpdateNameCommandHandler : IRequestHandler<UpdateNameCommand>
    {
        private readonly IApplicationEventService _applicationEventService;
        private readonly IConsumerContext _consumerContext;

        public UpdateNameCommandHandler(IApplicationEventService applicationEventService, IConsumerContext consumerContext)
        {
            _applicationEventService = applicationEventService ?? throw new ArgumentNullException(nameof(applicationEventService));
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
        }

        public async Task<Unit> Handle(UpdateNameCommand request, CancellationToken cancellationToken)
        {
            await _consumerContext.UpdateNameAsync(request.ConsumerId, request.FirstName, request.LastName, null, null);

            if (request.AccountInfo == null)
            {
                await _applicationEventService.AddApplicationEventAndPublish(
                    new ApplicationDetailsUpdatedEvent()
                    {
                        ApplicationId = request.PersonalInfo.ApplicationId,
                        CustomerId = request.PersonalInfo.CustomerId,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Timestamp = DateTime.Now,
                        Source = CustomerDetailsUpdatedEvent.CustomerDetailsUpdatedEventSource.Admin,
                    },
                    request.PersonalInfo.CustomerId.ToString(),
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
                        FirstName = request.FirstName,
                        LastName = request.LastName,
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
