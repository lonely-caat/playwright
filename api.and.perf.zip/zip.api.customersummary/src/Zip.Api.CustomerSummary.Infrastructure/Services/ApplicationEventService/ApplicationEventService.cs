using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Domain.Entities.Events;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService
{
    public class ApplicationEventService : IApplicationEventService
    {
        private readonly IApplicationEventContext _applicationEventContext;
        private readonly IEventBus _eventBus;

        public ApplicationEventService(
            IApplicationEventContext applicationEventContext,
            IEventBus eventBus)
        {
            _applicationEventContext = applicationEventContext ?? throw new ArgumentNullException(nameof(applicationEventContext));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task AddApplicationEventAndPublish<T>(
            T message,
            string aggregateId,
            AggregateEventType type = AggregateEventType.MerchantAccount) where T : Event
        {
            var serializedMessage = JsonConvert.SerializeObject(message);

            var appEvent = new ApplicationEvent
            {
                EventId = message.Id,
                Payload = serializedMessage,
                CreatedTimestamp = DateTime.Now,
                Published = true,
                AggregateId = aggregateId,
                AggregateType = type.ToString(),
                EventType = typeof(T).Name
            };
            
            try
            {
                appEvent = await _applicationEventContext.CreateAsync(appEvent);
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName} :: " +
                    $"Failed to save ApplicationEvent for ApplicationEventId:{SerilogProperty.ApplicationEventId}. Detail : {SerilogProperty.Detail}",
                    nameof(ApplicationEventService),
                    nameof(AddApplicationEventAndPublish),
                    message.Id,
                    $"AggregateId: {aggregateId}, Type: {typeof(T)}, message: {serializedMessage}");

                return;
            }

            try
            {
                await _eventBus.PublishAsync(message);

                Log.Information(
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName} :: " +
                    $"Successfully published to AWS for {SerilogProperty.Detail}",
                    nameof(ApplicationEventService),
                    nameof(AddApplicationEventAndPublish),
                    $"AggregateId: {aggregateId} Type: {typeof(T)}, message: {serializedMessage}");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName} :: " +
                    $"Failed to publish to AWS for ApplicationEventId:{SerilogProperty.ApplicationEventId}. Detail : {SerilogProperty.Detail}",
                    nameof(ApplicationEventService),
                    nameof(AddApplicationEventAndPublish),
                    message.Id,
                    $"AggregateId: {aggregateId}, Type: {typeof(T)}, message: {serializedMessage}, exception: {ex.Message} {ex.StackTrace}");

                await _applicationEventContext.MarkAsUnpublishedAsync(appEvent.Id);
            }
        }
    }
}
