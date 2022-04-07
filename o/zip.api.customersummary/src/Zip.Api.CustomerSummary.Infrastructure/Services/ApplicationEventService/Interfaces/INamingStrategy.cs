using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces
{
    public interface INamingStrategy
    {
        string GetTopicName(Type messageType);
    }
}
