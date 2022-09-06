using System;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateDateOfBirth
{
    public class UpdateDateOfBirthCommand : IRequest
    {
        public DateTime DateOfBirth { get; set; }
        public Consumer PersonalInfo { get; set; }
        public AccountInfo AccountInfo { get; set; }

        public UpdateDateOfBirthCommand(Consumer personalInfo, AccountInfo accountInfo, DateTime dateOfBirth)
        {
            PersonalInfo = personalInfo;
            AccountInfo = accountInfo;
            DateOfBirth = dateOfBirth;
        }

        public UpdateDateOfBirthCommand()
        {

        }
    }
}
