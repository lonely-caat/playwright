using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.CustomerAcquisition.Core.Kafka.MessageBus;
using Zip.CustomerAcquisition.Core.Kafka.MessageBus.Extensions;
using Zip.CustomerAcquisition.Core.Logging.Serilog;
using Zip.MessageTypes.Sync;

namespace Zip.Api.CustomerProfile.Services
{
    public class CustomerProfileBackgroundService : ICustomerProfileBackgroundService
    {
        private readonly IBus _bus;
        private readonly ILogger<CustomerProfileBackgroundService> _logger;
        private readonly string _customerProfileBackgroundServiceName;

        public CustomerProfileBackgroundService(IBus bus, ILogger<CustomerProfileBackgroundService> logger)
        {
            _bus = bus;
            _logger = logger;
            _customerProfileBackgroundServiceName = nameof(CustomerProfileBackgroundService);
        }

        public async Task DeleteCustomerById(Guid id, string correlationId, CancellationToken cancellationToken = default)
        {
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{_customerProfileBackgroundServiceName}.{methodName}:: Rest API delete method by id: {SerilogProperty.Detail}.",
                id);

            var message = new CustomerProfileSyncDelete().WithDefaultEventHeader();
            message.CustomerId = id.ToString();
            message.EventHeader.CorrelationId = correlationId;
            await _bus.Publish(message, cancellationToken);
        }
        
        public async Task UpdateCustomerById(Guid id, string correlationId, CancellationToken cancellationToken = default)
        {
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{_customerProfileBackgroundServiceName}.{methodName}:: Rest API update method by id: {SerilogProperty.Detail}.",
                id);

            var message = new CustomerProfileSyncUpdate().WithDefaultEventHeader();
            message.CustomerId = id.ToString();
            message.EventHeader.CorrelationId = correlationId;
            await _bus.Publish(message, cancellationToken);
        }
    }
}
