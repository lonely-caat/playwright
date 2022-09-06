using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService
{
    public class EventBusAmazonSns : IEventBus
    {
        private readonly IAmazonSimpleNotificationService _snsService;
        private readonly INamingStrategy _namingStrategy;
        private readonly EventBusSettings _settings;

        public EventBusAmazonSns(
            IAmazonSimpleNotificationService snsService,
            INamingStrategy namingStrategy,
            EventBusSettings eventBusSettings)
        {
            _snsService = snsService ?? new AmazonSimpleNotificationServiceClient();
            _namingStrategy = namingStrategy ?? throw new ArgumentNullException(nameof(namingStrategy));
            _settings = eventBusSettings ?? throw new ArgumentNullException(nameof(eventBusSettings));
        }

        public async Task PublishAsync<T>(T @event) where T : class
        {
            try
            {
                Log.Information($"EventBusAmazonSns :: Start to publish {@event.GetType().Name}.");

                var topicName = GetTopicName(@event);
                var topicArn = $"{_settings.ArnPrefix}{topicName}";
                var msg = JsonConvert.SerializeObject(@event,
                                                      new JsonSerializerSettings
                                                      {
                                                          DateTimeZoneHandling = DateTimeZoneHandling.Local
                                                      });

                Log.Information($"EventBusAmazonSns :: Message body : {msg}");

                var publishRequest = new PublishRequest(topicArn, msg)
                {
                    Subject = @event.GetType().Name
                };
                
                await _snsService.PublishAsync(publishRequest);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "EventBusAmazonSns :: Error in EventBusAmazonSns.PublishAsync");
                
                throw;
            }
        }

        private string GetTopicName<T>(T @event) where T : class
        {
            var messageType = @event.GetType();
            return _namingStrategy.GetTopicName(messageType);
        }
    }
}
