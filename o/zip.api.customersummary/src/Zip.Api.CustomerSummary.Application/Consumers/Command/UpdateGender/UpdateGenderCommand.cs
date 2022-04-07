using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateGender
{
    public class UpdateGenderCommand : IRequest
    {
        public Gender Gender { get; set; }
        public long ConsumerId { get; set; }

        public UpdateGenderCommand(long consumerId, Gender gender)
        {
            Gender = gender;
            ConsumerId = consumerId;
        }

        public UpdateGenderCommand()
        {

        }
    }
}
