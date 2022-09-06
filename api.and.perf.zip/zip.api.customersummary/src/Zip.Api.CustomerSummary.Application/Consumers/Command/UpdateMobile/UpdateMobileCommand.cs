using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateMobile
{
    public class UpdateMobileCommand : IRequest<UpdateCustomerMobileResponse>
    {
        public string Mobile { get; set; }
        public Consumer Consumer { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public UpdateMobileCommand(string mobile, Consumer consumer, AccountInfo accountInfo)
        {
            Mobile = mobile;
            Consumer = consumer;
            AccountInfo = accountInfo;
        }

        public UpdateMobileCommand()
        {

        }
    }
}
