using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetLoginStatus
{
    public class GetLoginStatusQuery : IRequest<LoginStatusResponse>
    {
        public string ConsumerEmail { get; set; }
    }
}