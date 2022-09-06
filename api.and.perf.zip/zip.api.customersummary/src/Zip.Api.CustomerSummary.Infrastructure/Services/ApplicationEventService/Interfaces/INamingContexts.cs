using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces
{
    public interface INamingContexts
    {
        Dictionary<string, string> GetEventContexts();
    }
}
