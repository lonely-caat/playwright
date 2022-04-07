using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateEmail
{
    public class UpdateEmailCommand : IRequest
    {
        public string Email { get; set; }
        public Consumer Consumer { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public UpdateEmailCommand(string email, Consumer consumer, AccountInfo accountInfo)
        {
            Email = email;
            Consumer = consumer;
            AccountInfo = accountInfo;
        }

        public UpdateEmailCommand()
        {

        }
    }
}
