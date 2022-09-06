using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateName
{
    public class UpdateNameCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ConsumerId { get; set; }

        public AccountInfo AccountInfo { get; set; }
        public Consumer PersonalInfo { get; set; }

        public UpdateNameCommand(long consumerId, string firstName, string lastName, AccountInfo accountInfo, Consumer personalInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            ConsumerId = consumerId;
            AccountInfo = accountInfo;
            PersonalInfo = personalInfo;
        }

        public UpdateNameCommand()
        {

        }
    }
}
