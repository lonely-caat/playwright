using System.Net;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models
{
    public class UpdateLoginStatusResponse
    {
        public HttpStatusCode Status { get; set; }

        public bool IsSuccess { get; set; }
    }
}