using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateGender
{
    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand>
    {
        private readonly IConsumerContext _consumerContext;

        public UpdateGenderCommandHandler(IConsumerContext personalInfoContext)
        {
            _consumerContext = personalInfoContext ?? throw new ArgumentNullException(nameof(personalInfoContext));
        }

        public async Task<Unit> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            await _consumerContext.UpdateGenderAsync(request.ConsumerId, request.Gender);
            return Unit.Value;
        }
    }
}
