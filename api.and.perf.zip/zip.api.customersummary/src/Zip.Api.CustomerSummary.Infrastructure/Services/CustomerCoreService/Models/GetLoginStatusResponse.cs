using System.Net;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models
{

    public class GetLoginStatusResponse
    {
        public HttpStatusCode Status { get; set; }

        public bool IsSuccess { get; set; }

        public LoginStatusResponse Data { get; set; }
    }

    public class LoginStatusResponse
    {
        public LoginStatusType LoginStatus { get; set; }
    }
}