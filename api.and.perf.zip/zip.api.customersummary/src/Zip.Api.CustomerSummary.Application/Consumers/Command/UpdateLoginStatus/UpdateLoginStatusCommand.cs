using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus
{
    public class UpdateLoginStatusCommand : IRequest<UpdateLoginStatusResponse>
    {
        public long ConsumerId { get; set; }
        
        public string ChangedBy { get; set; }

        public LoginStatusType LoginStatusType { get; set; }
    }
}